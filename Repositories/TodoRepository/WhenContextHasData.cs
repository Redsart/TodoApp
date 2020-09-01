using System;
using Xunit;
using Moq;
using TodoApp.Repositories.XmlRepository.Utils;
using TodoApp.Repositories.XmlRepository;
using TodoApp.Repositories.Models;
using System.Xml.Linq;
using Xml = TodoApp.Repositories.XmlRepository;
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

            Container.Add(firstTodo);
            Container.Add(secondTodo);

            MockXmlContext
                .Setup(ctx => ctx.GetContainer(containerName))
                .Returns(Container);
        }

        [Theory]
        [InlineData("00000000-0000-0000-0000-000000000000")]
        [InlineData("20975aeb-d490-4aa6-95ba-5b7c50b074a4")]
        public void GivenValidID_GetByID_ReturnsTodoEntity(string id)
        {
            var repo = new Xml.TodoRepository(MockXmlContext.Object);
            var guid = new Guid(id);

            var todo = repo.GetById(guid);

            Assert.NotNull(todo);
        }

        //[Theory]
        //[InlineData("00000000-0000-0000-0000-000000000000")]
        //[InlineData("20975aeb-d490-4aa6-95ba-5b7c50b074a4")]
        //public void GivenValidEntity_Update_UpdateEntity(string id)
        //{
        //    var repo = new Xml.TodoRepository(MockXmlContext.Object);

        //    var guid = new Guid(id);

        //    var entity = repo.GetById(guid);


        //}

        [Fact]
        public void GivenValidEntity_Insert_InsertSuccesfully()
        {
            var repo = new Xml.TodoRepository(MockXmlContext.Object);

            var element = new XElement("todo");
            element.Add(new XAttribute("Id", "fb7043c0-fa34-440c-9f7d-97a094f053ae"));
            element.Add(new XElement("Title", "Picnic"));
            element.Add(new XElement("Description", "Go to a picnic with friends"));
            element.Add(new XElement("Status", "Open"));
            element.Add(new XElement("CreatedOn", "2020-05-15T14:29:15.1823029Z"));
            element.Add(new XElement("DueDate", "2020-05-19T21:00:00.0000000Z"));

            var entity = new TodoModel();
            entity.Title = element.Element("Title").Value;
            entity.Description = element.Element("Description").Value;

            string statusStr = element.Element("Status").Value;
            entity.Status = (TodoStatus)Enum.Parse(typeof(TodoStatus), statusStr);

            string createdOnStr = element.Element("CreatedOn").Value;
            entity.CreatedOn = DateTime.Parse(createdOnStr, CultureInfo.InvariantCulture, DateTimeStyles.RoundtripKind);

            string dueDateStr = element.Element("DueDate").Value;
            entity.DueDate = DateTime.Parse(dueDateStr, CultureInfo.InvariantCulture, DateTimeStyles.RoundtripKind);

            entity.Id = Guid.Parse(element.Attribute("Id").Value);

            var todo = repo.Insert(entity);

            Assert.NotNull(todo);
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
