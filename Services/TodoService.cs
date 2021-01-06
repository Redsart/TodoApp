using System;
using Xunit;
using Moq;
using TodoApp.Repositories.Interfaces;
using Service = TodoApp.Services;
using System.Linq;
using TodoApp.Repositories.Models;

namespace TodoApp.Tests.Services
{
    public class TodoService
    {
        protected Mock<ITodoRepository> MockRepository;

        public TodoService()
        {
            MockRepository = new Mock<ITodoRepository>();
        }

        [Fact]
        public void GetAll_ReturnsAllTodos()
        {
            var mockTodos = new TodoModel[]
            {
                new TodoModel()
                {
                    Id = new Guid(),
                    Title = "Test",
                    Description = "Test Description",
                    Status = TodoStatus.Open,
                    DueDate = new DateTime(2021, 3, 4, 12, 30, 00),
                    CreatedOn = new DateTime(2020, 11, 12, 11, 55, 13),
                },
                new TodoModel()
                {
                    Id = new Guid(),
                    Title = "Second",
                    Description = "Second Test",
                    Status = TodoStatus.InProgress,
                    DueDate = new DateTime(2021, 1, 24, 1, 22, 33),
                    CreatedOn = new DateTime(2020, 10, 11, 12, 13, 14),
                },
            };
            MockRepository.Setup(a => a.GetAll()).Returns(mockTodos);
            var service = new Service.TodoService(MockRepository.Object);

            var todos = service.GetAll();

            Assert.Equal(mockTodos, todos);
        }

        [Fact]
        public void GetAll_CallRepositoryGetAll()
        {
            MockRepository.Setup(a => a.GetAll());
            MockRepository.Object.GetAll();

            MockRepository.Verify(a => a.GetAll(), Times.Once);
        }

        [Fact]
        public void GetById_ReturnsCorrectTodo()
        {
            string guid = "00000000-0000-0000-0000-000000000001";
            var todo = new TodoModel
            {
                Id = Guid.Parse(guid),
                Title = "Test",
                Description = "Test Description",
                Status = TodoStatus.InProgress,
                CreatedOn = new DateTime(2020, 10, 11, 12, 13, 14),
                DueDate = new DateTime(2021, 1, 24, 1, 22, 33)
            };

            MockRepository.Setup(a => a.GetById(todo.Id)).Returns(todo);
            var sercice = new Service.TodoService(MockRepository.Object);

            var servicedTodo = sercice.GetByID(Guid.Parse(guid));

            Assert.Equal(servicedTodo, todo);
        }

        [Fact]
        public void GetById_CallRepositoryGetById()
        {
            string guid = "00000000-0000-0000-0000-000000000001";
            MockRepository.Setup(a => a.GetById(Guid.Parse(guid)));
            MockRepository.Object.GetById(Guid.Parse(guid));

            MockRepository.Verify(a => a.GetById(Guid.Parse(guid)));
        }
    }
}
