using System;
using Xunit;
using Moq;
using TodoApp.Repositories.XmlRepository.Utils;
using System.Xml.Linq;
using Xml = TodoApp.Repositories.XmlRepository;

namespace TodoApp.Tests.Repositories.TodoRepositories
{
    public class WhenContextIsEmpty
    {
        protected Mock<IXmlContext> MockXmlContext;

        protected XElement Container;

        public WhenContextIsEmpty()
        {
            // Arrange
            MockXmlContext = new Mock<IXmlContext>();

            var containerName = "todos";
            Container = new XElement(containerName);

            MockXmlContext
                .Setup(ctx => ctx.GetContainer(containerName))
                .Returns(Container);
        }

        [Fact]
        public void GetAll_ReturnsEmpty()
        {
            // Arrange
            var repo = new Xml.TodoRepository(MockXmlContext.Object);

            // Act
            var all = repo.GetAll();

            // Assert
            Assert.Empty(all);
        }

        [Fact]
        public void GetById_ReturnsEmpty()
        {
            //Arange
            var repo = new Xml.TodoRepository(MockXmlContext.Object);

            //Act
            var todo = repo.GetById(Guid.Empty);

            //Assert
            Assert.Null(todo);
        }
    }
}
