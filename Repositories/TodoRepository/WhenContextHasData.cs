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
            firstTodo.Add(new XAttribute("Id", "00000000-0000-0000-0000-000000000001"));
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
        [InlineData("00000000-0000-0000-0000-000000000001")]
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
            //arrange
            var expectedTodo = new TodoModel();
            var guid = Guid.Parse("00000000-0000-0000-0000-000000000001");
            expectedTodo.Id = guid;
            expectedTodo.Title = "Unit tests";
            expectedTodo.Description = "Learn how to make unit tests";
            expectedTodo.Status = TodoStatus.Open;
            expectedTodo.CreatedOn = DateTime.Parse("2020-04-15T14:29:15.1823029Z", CultureInfo.InvariantCulture, DateTimeStyles.RoundtripKind);
            expectedTodo.DueDate = DateTime.Parse("2020-04-19T21:00:00.0000000Z", CultureInfo.InvariantCulture, DateTimeStyles.RoundtripKind);

            var repo = new Xml.TodoRepository(MockXmlContext.Object);

            //act
            var todo = repo.GetById(guid);

            //assert
            Assert.Equal(todo.Id, expectedTodo.Id);
            Assert.Equal(todo.Title, expectedTodo.Title);
            Assert.Equal(todo.Description, expectedTodo.Description);
            Assert.Equal(todo.Status, expectedTodo.Status);
            Assert.Equal(todo.CreatedOn, expectedTodo.CreatedOn);
            Assert.Equal(todo.DueDate, expectedTodo.DueDate);
        }



        [Theory]
        [InlineData("0a000300-0600-0000-0100-0000f0700001","Picnic", "Go to a picnic with friends", TodoStatus.Open, "2020-05-15T14:29:15.1823029Z", "2020-05-19T21:00:00.0000000Z")]
        [InlineData("a00e0400-3000-0000-3000-000050000001","Football", "", TodoStatus.InProgress, "2020-05-15T14:29:15.1823029Z", "2020-05-19T21:00:00.0000000Z")] // without Description
        public void GivenValidEntity_Update_UpdateEntity(string id, string title, string description, TodoStatus status, string createdOn, string dueDate)
        {
            //arrange
            var todo = new TodoModel()
            {
                Id = Guid.Parse(id),
                Title = title,
                Description = description,
                Status = status,
                CreatedOn = DateTime.Parse(createdOn),
                DueDate = DateTime.Parse(dueDate)
            };

            var todoAsElement = EntityToElement(todo);
            Container.Add(todoAsElement);

            var repo = new Xml.TodoRepository(MockXmlContext.Object);

            //act
            todo.Title = "Concert";
            todo.Description = "Go to Metallica concert";
            repo.Update(todo);

            //assert
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
        public void GivenInvalidTodoEntity_Update_ThrowsException(string title, string description, TodoStatus? status, string createdOn, string dueDate)
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
        [InlineData("a00e0700-3000-0b00-3000-000050080001", "Picnic", "Go to a picnic with friends", TodoStatus.Open, "2020-05-15T14:29:15.1823029Z", "2020-05-19T21:00:00.0000000Z")]
        [InlineData("600e0400-3c00-0000-3000-020050000001", "Football", "", TodoStatus.InProgress, "2020-05-15T14:29:15.1823029Z", "2020-05-19T21:00:00.0000000Z")] // without Description
        public void GivenNotExistedId_Update_DoesNothing(string id,string title, string description, TodoStatus status, string createdOn, string dueDate)
        {
            //arrange
            var todo = new TodoModel()
            {
                Id = Guid.Parse(id),
                Title = title,
                Description = description,
                Status = status,
                CreatedOn = DateTime.Parse(createdOn),
                DueDate = DateTime.Parse(dueDate)
            };

            var repo = new Xml.TodoRepository(MockXmlContext.Object);

            //act
            todo.Title = "Concert";
            todo.Description = "Go to Metallica concert";
            repo.Update(todo);

            //assert
            var all = Container.Elements();
            var element = all.FirstOrDefault(a => a.Attribute("Id").Value == todo.Id.ToString());

            Assert.Null(element);
        }

        [Theory]
        [InlineData("Picnic","Go to a picnic with friends",TodoStatus.Open,"2020-05-15T14:29:15.1823029Z","2020-05-19T21:00:00.0000000Z")]
        [InlineData("Football", "", TodoStatus.InProgress, "2020-05-15T14:29:15.1823029Z", "2020-05-19T21:00:00.0000000Z")] // without Description
        public void GivenValidTodo_Insert_ReturnsNewTodo(string title,string description, TodoStatus status, string createdOn, string dueDate)
        {
            //arrange
            var repo = new Xml.TodoRepository(MockXmlContext.Object);

            var entity = new TodoModel()
            {
                Title = title,
                Description = description,
                Status = status,
                CreatedOn = DateTime.Parse(createdOn, CultureInfo.InvariantCulture, DateTimeStyles.RoundtripKind),
                DueDate = DateTime.Parse(dueDate, CultureInfo.InvariantCulture, DateTimeStyles.RoundtripKind)
            };

            //act
            var todo = repo.Insert(entity);

            //assert
            Assert.NotNull(todo);
            Assert.NotEmpty(todo.Id.ToString());
            Assert.NotEqual("00000000-0000-0000-0000-000000000000", todo.Id.ToString());
            Assert.Equal(todo.Title, entity.Title);
            Assert.Equal(todo.Description, entity.Description);
            Assert.Equal(todo.Status, entity.Status);
            Assert.Equal(todo.CreatedOn, entity.CreatedOn);
            Assert.Equal(todo.DueDate, entity.DueDate);
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
        public void GetAll_ReturnsAllTodos()
        {
            var repo = new Xml.TodoRepository(MockXmlContext.Object);

            var elements = Container.Elements();
            var todos = repo.GetAll();

            Assert.Equal(elements.Count(), todos.Count());
            foreach (var todo in todos)
            {
                var element = elements.First(x => x.Attribute("Id").Value == todo.Id.ToString());
                Assert.NotNull(todo);
                Assert.NotEmpty(todo.Id.ToString());
                Assert.Equal(todo.Id.ToString(), element.Attribute("Id").Value);
                Assert.Equal(todo.Title, element.Element("Title").Value);
                Assert.Equal(todo.Description, element.Element("Description").Value);
                Assert.Equal(todo.Status.ToString(), element.Element("Status").Value);
                Assert.Equal(todo.CreatedOn.ToUniversalTime().ToString("o", CultureInfo.InvariantCulture), element.Element("CreatedOn").Value);
                Assert.Equal(todo.DueDate.ToUniversalTime().ToString("o", CultureInfo.InvariantCulture), element.Element("DueDate").Value);
            }
        }

        [Fact]
        public void Save_ReturnsTrue()
        {
            var repo = new Xml.TodoRepository(MockXmlContext.Object);

            var isSaved = repo.Save();

            Assert.True(isSaved);
        }

        [Fact]
        public void Save_CallContextSave()
        {
            MockXmlContext.Setup(a => a.Save());
            var repo = new Xml.TodoRepository(MockXmlContext.Object);

            repo.Save();

            MockXmlContext.Verify(a => a.Save(), Times.Once);
        }

        public static TheoryData<(Func<TodoModel, bool>, string[])> FilterTests = new TheoryData<(Func<TodoModel, bool> filter, string[] eptected)>
        {
            ( todo => todo.Status == TodoStatus.Open, new string[] { "00000000-0000-0000-0000-000000000001", "20975aeb-d490-4aa6-95ba-5b7c50b074a4" } ),
            ( todo => todo.Title == "Football", new string[] { "20975aeb-d490-4aa6-95ba-5b7c50b074a4"} ),
        };

        [Theory]
        [MemberData(nameof(FilterTests))]
        public void GivenFilter_Get_ReturnsMatchingTodos((Func<TodoModel, bool> filter, string[] expectedIds) data)
        {
            // Arrange
            var repo = new Xml.TodoRepository(MockXmlContext.Object);

            // Act
            var result = repo.Get(data.filter);

            // Assert
            Assert.Equal(data.expectedIds.Length, result.Count());

            foreach (var expectedId in data.expectedIds)
            {
                Assert.Contains(result, expectedTodo => expectedTodo.Id.ToString() == expectedId);
            }
        }

        public static TheoryData<(Func<TodoModel, object>, string[])> OrderByTests = new TheoryData<(Func<TodoModel, object> orderByKey, string[] eptected)>
        {
            ( todo => todo.Title, new string[] { "20975aeb-d490-4aa6-95ba-5b7c50b074a4", "00000000-0000-0000-0000-000000000001" } ),
        };

        [Theory]
        [MemberData(nameof(OrderByTests))]
        public void GivenOrderBy_Get_ReturnsSortedTodos((Func<TodoModel, object> orderByKey, string[] expectedIds) data)
        {
            // Arrange
            var repo = new Xml.TodoRepository(MockXmlContext.Object);

            // Act
            var result = repo.Get(data.orderByKey);

            // Assert
            Assert.Equal(data.expectedIds.Length, result.Count());

            for (int i = 0; i < data.expectedIds.Length; i++)
            {
                var expectedId = data.expectedIds[i];
                var todo = result.ElementAt(i);
                Assert.Equal(expectedId, todo.Id.ToString());
            }
        }

        public static TheoryData<(Func<TodoModel, bool>, Func<TodoModel, object>, string[])> FilterAndOrderByTests =
            new TheoryData<(Func<TodoModel, bool> filter, Func<TodoModel, object> orderByKey, string[] expected)>
        {
            (todo => todo.Status == TodoStatus.Open, todo => todo.Title, new string[] {"20975aeb-d490-4aa6-95ba-5b7c50b074a4", "00000000-0000-0000-0000-000000000001" }),
        };

        [Theory]
        [MemberData(nameof(FilterAndOrderByTests))]
        public void GivenFilterAndOrderBy_Get_ReturnsMatchingSortedTodos((Func<TodoModel, bool> filter, Func<TodoModel, object> orderByKey, string[] expectedIds) data)
        {
            //Arange
            var repo = new Xml.TodoRepository(MockXmlContext.Object);

            //Act
            var result = repo.Get(data.filter, data.orderByKey);

            //Assert
            Assert.Equal(data.expectedIds.Count(), result.Count());
            for (int i = 0; i < data.expectedIds.Length; i++)
            {
                var expectedId = data.expectedIds[i];
                var todo = result.ElementAt(i);
                Assert.Equal(expectedId, todo.Id.ToString());
            }
        }

        [Theory]
        [InlineData("a00e0700-3000-0b00-3000-000050080001")]
        [InlineData("600e0400-3c00-0000-3000-020050000001")]
        public void GivenNotExistedId_Delete_DoesNothing(string id)
        {
            var repo = new Xml.TodoRepository(MockXmlContext.Object);

            var expected = Container.Elements().Count();
            var guid = new Guid(id);
            repo.Delete(guid);

            var all = Container.Elements();
            var element = all.FirstOrDefault(a => a.Attribute("Id").Value == guid.ToString());

            Assert.Equal(expected, all.Count());
            Assert.Null(element);
        }

        [Fact]
        public void GivenValidElement_Delete_RemoveElement()
        {
            var repo = new Xml.TodoRepository(MockXmlContext.Object);

            var guid = new Guid("00000000-0000-0000-0000-000000000001");
            repo.Delete(guid);

            var all = Container.Elements();
            var element = all.FirstOrDefault(a => a.Attribute("Id").Value == guid.ToString());
            
            Assert.Null(element);
        }

        public XElement EntityToElement(TodoModel todo)
        {
            XElement element = new XElement("todo");
            element.Add(new XAttribute("Id",todo.Id.ToString()));
            element.Add(new XElement("Title", todo.Title));
            element.Add(new XElement("Description", todo.Description));
            element.Add(new XElement("Status"), todo.Status.ToString());
            element.Add(new XElement("CreatedOn", todo.CreatedOn.ToString("o", CultureInfo.InvariantCulture)));
            element.Add(new XElement("DueDate", todo.DueDate.ToString("o", CultureInfo.InvariantCulture)));

            return element;
        }
    }
}
