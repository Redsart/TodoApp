using System;
using Xunit;
using Moq;
using TodoApp.Repositories.XmlRepository.Utils;
using TodoApp.Repositories.Models;
using System.Xml.Linq;
using Xml = TodoApp.Repositories.XmlRepository;
using System.Linq;
using System.Globalization;

namespace TodoApp.Tests.Repositories.TodoRepositories
{
    public class WhenContextHasData
    {
        protected Mock<IXmlContext> MockXmlContext;

        protected XElement Container;

        public WhenContextHasData()
        {
            // Arrange
            MockXmlContext = new Mock<IXmlContext>();

            var containerName = "todos";
            Container = new XElement(containerName);

            var firstTodo = new XElement("todo");
            firstTodo.Add(new XAttribute("Id", "00000000-0000-0000-0000-000000000000"));
            firstTodo.Add(new XElement("Title", "Unit tests"));
            firstTodo.Add(new XElement("Description", "Learn how to make unit tests"));
            firstTodo.Add(new XElement("Status", "Open"));
            firstTodo.Add(new XElement("CreatedOn", "2020-04-15T14:29:15.1823029Z"));
            firstTodo.Add(new XElement("DueDate", "2020-04-19T21:00:00.0000000Z"));

            var secondTodo = new XElement("todo");
            secondTodo.Add(new XAttribute("Id", "20975aeb-d490-4aa6-95ba-5b7c50b074a4"));
            secondTodo.Add(new XElement("Title", "Football"));
            secondTodo.Add(new XElement("Description", "Play football"));
            secondTodo.Add(new XElement("Status", "Open"));
            secondTodo.Add(new XElement("CreatedOn", "2020-04-16T10:17:33.2653554Z"));
            secondTodo.Add(new XElement("DueDate", "2020-05-29T21:00:00.0000000Z"));

            var thirdTodo = new XElement("todo");
            thirdTodo.Add(new XAttribute("Id", "30000300-4000-0000-0900-00a0000f0050"));
            thirdTodo.Add(new XElement("Title", "Another todo"));
            thirdTodo.Add(new XElement("Description", ""));
            thirdTodo.Add(new XElement("Status", "InProgress"));
            thirdTodo.Add(new XElement("CreatedOn", "2020-09-20T14:29:15.1823029Z"));
            thirdTodo.Add(new XElement("DueDate", "2020-09-30T21:00:00.0000000Z"));

            Container.Add(firstTodo);
            Container.Add(secondTodo);

            MockXmlContext
                .Setup(ctx => ctx.GetContainer(containerName))
                .Returns(Container);
        }

        [Theory]
        [InlineData("00000000-0000-0000-0000-000000000000")]
        [InlineData("20975aeb-d490-4aa6-95ba-5b7c50b074a4")]
        public void GivenValidID_GetById_ReturnsTodoEntity(string id)
        {
            var repo = new Xml.TodoRepository(MockXmlContext.Object);
            var guid = new Guid(id);

            var todo = repo.GetById(guid);

            Assert.NotNull(todo);
        }

        [Fact]
        public void GivenValidID_GetById_ReturnsCorrectTodo()
        {
            //make a todoToBeCompared the same like one of the actual todos and compare the properties of both items
            var expectedTodo = new TodoModel();
            expectedTodo.Id = Guid.Parse("00000000-0000-0000-0000-000000000000");
            expectedTodo.Title = "Unit tests";
            expectedTodo.Description = "Learn how to make unit tests";
            expectedTodo.Status = TodoStatus.Open;
            expectedTodo.CreatedOn = DateTime.Parse("2020-04-15T14:29:15.1823029Z");
            expectedTodo.DueDate = DateTime.Parse("2020-04-19T21:00:00.0000000Z");

            var repo = new Xml.TodoRepository(MockXmlContext.Object);

            var todo = repo.GetById(Guid.Parse("00000000-0000-0000-0000-000000000000"));

            Assert.Equal(todo.Id, expectedTodo.Id);
            Assert.Equal(todo.Title, expectedTodo.Title);
            Assert.Equal(todo.Description, expectedTodo.Description);
            Assert.Equal(todo.Status, expectedTodo.Status);
            Assert.Equal(todo.CreatedOn, expectedTodo.CreatedOn.ToUniversalTime());
            Assert.Equal(todo.DueDate, expectedTodo.DueDate.ToUniversalTime());
        }

        [Theory]
        [InlineData("Picnic", "Go to a picnic with friends", TodoStatus.Open, "2020-05-15T14:29:15.1823029Z", "2020-05-19T21:00:00.0000000Z")]
        [InlineData("Football", "", TodoStatus.InProgress, "2020-05-15T14:29:15.1823029Z", "2020-05-19T21:00:00.0000000Z")] // without Description
        public void GivenValidEntity_Update_UpdateEntity(string title, string description, TodoStatus status, string createdOn, string dueDate)
        {
            var repo = new Xml.TodoRepository(MockXmlContext.Object);

            var todo = new TodoModel()
            {
                Title = title,
                Description = description,
                Status = status,
                CreatedOn = DateTime.Parse(createdOn),
                DueDate = DateTime.Parse(dueDate)
            };

            todo.Title = "Concert";
            todo.Description = "Go to Metallica concert";
            repo.Update(todo);

            var all = Container.Elements();
            var element = all.First(a => a.Attribute("Id").Value == todo.Id.ToString());

            Assert.Equal(todo.Id.ToString(), element.Attribute("Id").Value);
            Assert.Equal(todo.Title, element.Element("Title").Value);
            Assert.Equal(todo.Description, element.Element("Description").Value);
            Assert.Equal(todo.Status.ToString(), element.Element("Status").Value);
            Assert.Equal(todo.CreatedOn.ToUniversalTime().ToString("o", CultureInfo.InvariantCulture), element.Element("CreatedOn").Value);
            Assert.Equal(todo.DueDate.ToUniversalTime().ToString("o", CultureInfo.InvariantCulture), element.Element("DueDate").Value);
        }

        
        [Theory]
        [InlineData("Picnic", "Go to a picnic with friends", TodoStatus.InProgress, "2020-05-15T14:29:15.1823029Z", "2020-05-19T21:00:00.0000000Z")]
        [InlineData("Football", "", null, "2020-05-15T14:29:15.1823029Z", "2020-05-19T21:00:00.0000000Z")]
        public void GivenInvalidTodoEntity_Update_ThrowsException(string title, string description, TodoStatus status, string createdOn, string dueDate)
        {
            var repo = new Xml.TodoRepository(MockXmlContext.Object);

            var todo = new TodoModel()
            {
                Title = title,
                Description = description,
                Status = status,
                CreatedOn = DateTime.Parse(createdOn),
                DueDate = DateTime.Parse(dueDate)
            };

            todo.Title = "";
            todo.Description = "Go to Metallica concert";

            var ex = Assert.Throws<ArgumentException>(() => repo.Update(todo));

            Assert.Equal("Empty todo!", ex.Message);
        }
        

        [Theory]
        [InlineData("Picnic","Go to a picnic with friends",TodoStatus.Open,"2020-05-15T14:29:15.1823029Z","2020-05-19T21:00:00.0000000Z")]
        [InlineData("Football", "", TodoStatus.InProgress, "2020-05-15T14:29:15.1823029Z", "2020-05-19T21:00:00.0000000Z")] // without Description
        public void GivenValidTodo_Insert_ReturnsNewTodo(string title,string description, TodoStatus status, string createdOn, string dueDate)
        {
            var repo = new Xml.TodoRepository(MockXmlContext.Object);

            var entity = new TodoModel()
            {
                Title = title,
                Description = description,
                Status = status,
                CreatedOn = DateTime.Parse(createdOn),
                DueDate = DateTime.Parse(dueDate)
            };

            var todo = repo.Insert(entity);

            var all = Container.Elements();
            var element = all.First(a => a.Attribute("Id").Value == todo.Id.ToString());

            Assert.NotNull(todo);
            Assert.NotEmpty(todo.Id.ToString());
            Assert.Equal(todo.Id.ToString(), element.Attribute("Id").Value);
            Assert.Equal(todo.Title, element.Element("Title").Value);
            Assert.Equal(todo.Description, element.Element("Description").Value);
            Assert.Equal(todo.Status.ToString(), element.Element("Status").Value);
            Assert.Equal(todo.CreatedOn.ToUniversalTime().ToString("o", CultureInfo.InvariantCulture), element.Element("CreatedOn").Value);
            Assert.Equal(todo.DueDate.ToUniversalTime().ToString("o", CultureInfo.InvariantCulture), element.Element("DueDate").Value);
        }

        [Theory]
        [InlineData("Picnic", "Go to a picnic with friends", TodoStatus.Open, "2020-05-15T14:29:15.1823029Z", "2020-05-19T21:00:00.0000000Z")]
        [InlineData("Football", "", TodoStatus.InProgress, "2020-05-15T14:29:15.1823029Z", "2020-05-19T21:00:00.0000000Z")] // without Description
        public void GivenValidTodo_Insert_AddsTodo(string title, string description, TodoStatus status, string createdOn, string dueDate)
        {
            var repo = new Xml.TodoRepository(MockXmlContext.Object);

            var entity = new TodoModel()
            {
                Title = title,
                Description = description,
                Status = status,
                CreatedOn = DateTime.Parse(createdOn),
                DueDate = DateTime.Parse(dueDate)
            };

            var todo = repo.Insert(entity);

            var all = Container.Elements();
            var element = all.First(a => a.Attribute("Id").Value == todo.Id.ToString());

            Assert.NotNull(element);
            Assert.Equal(todo.Id.ToString(), element.Attribute("Id").Value);
            Assert.Equal(todo.Title, element.Element("Title").Value);
            Assert.Equal(todo.Description, element.Element("Description").Value);
            Assert.Equal(todo.Status.ToString(), element.Element("Status").Value);
            Assert.Equal(todo.CreatedOn.ToUniversalTime().ToString("o", CultureInfo.InvariantCulture), element.Element("CreatedOn").Value);
            Assert.Equal(todo.DueDate.ToUniversalTime().ToString("o", CultureInfo.InvariantCulture), element.Element("DueDate").Value);
        }

        [Theory]
        [InlineData("Picnic", "Go to a picnic with friends", null, "2020-05-15T14:29:15.1823029Z", "2020-05-19T21:00:00.0000000Z")]
        [InlineData("", "Go to a picnic with friends", TodoStatus.Open, "2020-05-15T14:29:15.1823029Z", "2020-05-19T21:00:00.0000000Z")] // without title
        public void GivenInvalidTodo_Insert_ThrowsArgumentException(string title, string description, TodoStatus? status, string createdOn, string dueDate)
        {
            var repo = new Xml.TodoRepository(MockXmlContext.Object);
            var model = new TodoModel
            {
                Title = title,
                Description = description,
                Status = status,
                CreatedOn = DateTime.Parse(createdOn),
                DueDate = DateTime.Parse(dueDate)
            };

            var ex = Assert.Throws<ArgumentException>(() => repo.Insert(model));

            Assert.Equal("Empty todo!", ex.Message);
        }

        [Fact]
        public void GivenValidElement_Delete_RemoveElement()
        {
            var repo = new Xml.TodoRepository(MockXmlContext.Object);

            var guid = new Guid("00000000-0000-0000-0000-000000000000");
            repo.Delete(guid);

            var todo = repo.GetById(guid);

            Assert.Null(todo);
        }
    }
}
