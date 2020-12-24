using System;
using Xunit;
using Moq;
using TodoApp.Repositories.Interfaces;
using TodoApp.Repositories.XmlRepository;
using Service = TodoApp.Services;
using TodoApp.Repositories.XmlRepository.Utils;
using System.Xml.Linq;
using System.Linq;
using System.Globalization;

namespace TodoApp.Tests.Services
{
    public class TodoService
    {

        protected Mock<ITodoRepository> MockRepository;

        IXmlContext context = new XmlContext("");

        protected XElement Container;

        public TodoService()
        {
            MockRepository = new Mock<ITodoRepository>();

            string containerName = "todos";
            Container = context.GetContainer(containerName);

     
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


            ITodoRepository repo = new TodoRepository(context);
        }

        [Fact]
        public void GetAll_ReturnsAllTodos()
        {
            var service = new Service.TodoService(MockRepository.Object);

            var elements = Container.Elements();
            var todos = service.GetAll();

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
    }
}
