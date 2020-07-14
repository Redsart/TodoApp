using System;
using Xunit;
using Moq;
using TodoApp.Repositories.XmlRepository.Utils;
using System.Xml.Linq;
using Xml = TodoApp.Repositories.XmlRepository;

namespace TodoApp.Tests.Repositories
{
    public class WhenContextIsDefined
    {
        protected Mock<IXmlContext> MockXmlContext;

        public WhenContextIsDefined()
        {
            // Arrange
            MockXmlContext = new Mock<IXmlContext>();

            var containerName = "todos";
            var container = new XElement(containerName);

            MockXmlContext
                .Setup(ctx => ctx.GetContainer(containerName))
                .Returns(container);
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
            var repo = new Xml.TodoRepository(MockXmlContext.Object);

            var getById = repo.GetById(Guid.Empty);

            Assert.Null(getById);
        }
    }
}
