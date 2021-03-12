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

            var service = new Service.TodoService(MockRepository.Object);
            service.GetAll();

            MockRepository.Verify(a => a.GetAll(), Times.Once);
        }

        [Fact]
        public void GivenExistingId_GetById_ReturnsCorrectTodo()
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

            var service = new Service.TodoService(MockRepository.Object);
            service.GetByID(Guid.Parse(guid));

            MockRepository.Verify(a => a.GetById(Guid.Parse(guid)), Times.Once);
        }

        [Theory]
        [InlineData("0a000300-0600-0000-0100-0000f0700001", "Picnic", "Go to a picnic with friends", TodoStatus.Open, "2020-05-15T14:29:15.1823029Z", "2020-05-19T21:00:00.0000000Z")]
        public void GivenExistingTodo_Update_UpdateRetursTrue(string id, string title, string description, TodoStatus status, string createdOn, string dueDate)
        {
            var todo = new TodoModel
            {
                Id = Guid.Parse(id),
                Title = title,
                Description = description,
                Status = status,
                CreatedOn = DateTime.Parse(createdOn),
                DueDate = DateTime.Parse(dueDate)
            };

            MockRepository.Setup(a => a.Update(todo));
            todo.Title = "Concert";
            todo.Description = "Go to Metallica concert";
            var service = new Service.TodoService(MockRepository.Object);

            var isUpdated = service.Update(todo);

            Assert.True(isUpdated);
        }

        [Fact]
        public void Update_CallRepositoryUpdate()
        {
            var todo = new TodoModel();
            MockRepository.Setup(a => a.Update(todo));

            var service = new Service.TodoService(MockRepository.Object);
            service.Update(todo);

            MockRepository.Verify(a => a.Update(todo), Times.Once);
        }

        [Fact]
        public void Update_CallsRepositorySave()
        {
            var todo = new TodoModel();
            MockRepository.Setup(a => a.Save());

            var service = new Service.TodoService(MockRepository.Object);
            service.Update(todo);

            MockRepository.Verify(a => a.Save(), Times.Once);
        }

        [Fact]
        public void Create_CallRepositoryInsert()
        {
            var todo = new TodoModel();
            MockRepository.Setup(a => a.Insert(todo));

            var service = new Service.TodoService(MockRepository.Object);
            service.Create(todo);

            MockRepository.Verify(a => a.Insert(todo), Times.Once);
        }

        [Fact]
        public void Create_CallRepositorySave()
        {
            var todo = new TodoModel();
            MockRepository.Setup(a => a.Save());

            var service = new Service.TodoService(MockRepository.Object);
            service.Create(todo);

            MockRepository.Verify(a => a.Save(), Times.Once);
        }

        [Theory]
        [InlineData("0a000300-0600-0000-0100-0000f0700001", "Picnic", "Go to a picnic with friends", TodoStatus.Open, "2020-05-15T14:29:15.1823029Z", "2020-05-19T21:00:00.0000000Z")]
        public void GivenCorrectTodo_Create_ReturnCorrectTodo(string id, string title, string description, TodoStatus status, string createdOn, string dueDate)
        {
            var todo = new TodoModel
            {
                Id = Guid.Parse(id),
                Title = title,
                Description = description,
                Status = status,
                CreatedOn = DateTime.Parse(createdOn),
                DueDate = DateTime.Parse(dueDate)
            };

            var repositoryTodo = new TodoModel();

            MockRepository.Setup(a => a.Insert(todo)).Returns(repositoryTodo);
            var service = new Service.TodoService(MockRepository.Object);
            var serviceTodo = service.Create(todo);

            Assert.Equal(serviceTodo, repositoryTodo);
        }

        [Theory]
        [InlineData("0a000300-0600-0000-0100-0000f0700001", "Picnic", "Go to a picnic with friends", TodoStatus.Open, "2020-05-15T14:29:15.1823029Z", "2020-05-19T21:00:00.0000000Z")]
        public void GivenExistingTodo_Delete_ReturnsTrue(string id, string title, string description, TodoStatus status, string createdOn, string dueDate)
        {
            var todo = new TodoModel
            {
                Id = Guid.Parse(id),
                Title = title,
                Description = description,
                Status = status,
                CreatedOn = DateTime.Parse(createdOn),
                DueDate = DateTime.Parse(dueDate)
            };

            MockRepository.Setup(a => a.GetById(todo.Id)).Returns(todo);
            MockRepository.Setup(a => a.Delete(todo.Id));
            var service = new Service.TodoService(MockRepository.Object);
            var isDeleted = service.Delete(todo.Id);

            Assert.True(isDeleted);
        }

        [Fact]
        public void GivenNotExistingTodo_Delete_ReturnsFalse()
        {
            var todo = new TodoModel();
            
            MockRepository.Setup(a => a.Delete(todo.Id));
            var service = new Service.TodoService(MockRepository.Object);
            var isDeleted = service.Delete(todo.Id);

            Assert.False(isDeleted);
        }

        [Theory]
        [InlineData("0a000300-0600-0000-0100-0000f0700001", "Picnic", "Go to a picnic with friends", TodoStatus.Open, "2020-05-15T14:29:15.1823029Z", "2020-05-19T21:00:00.0000000Z")]
        public void GivenExistingTodo_Delete_CallRepositoryDelete(string id, string title, string description, TodoStatus status, string createdOn, string dueDate)
        {
            var todo = new TodoModel
            {
                Id = Guid.Parse(id),
                Title = title,
                Description = description,
                Status = status,
                CreatedOn = DateTime.Parse(createdOn),
                DueDate = DateTime.Parse(dueDate)
            };

            MockRepository.Setup(a => a.GetById(todo.Id)).Returns(todo);
            MockRepository.Setup(a => a.Delete(todo.Id));
            var service = new Service.TodoService(MockRepository.Object);
            service.Delete(todo.Id);

            MockRepository.Verify(a => a.Delete(todo.Id), Times.Once);
        }

        [Fact]
        public void GivenNotExistingID_Delete_DoesntCallRepositoryDelete()
        {
            Guid id = Guid.Parse("0a000300-0600-0000-0100-0000f0700001");
            MockRepository.Setup(a => a.Delete(id));
            var service = new Service.TodoService(MockRepository.Object);
            service.Delete(id);

            MockRepository.Verify(a => a.Delete(id), Times.Never);
        }

        [Theory]
        [InlineData("0a000300-0600-0000-0100-0000f0700001", "Picnic", "Go to a picnic with friends", TodoStatus.Open, "2020-05-15T14:29:15.1823029Z", "2020-05-19T21:00:00.0000000Z")]
        public void GivenExistingTodo_Delete_CallRepositorySave(string id, string title, string description, TodoStatus status, string createdOn, string dueDate)
        {
            var todo = new TodoModel
            {
                Id = Guid.Parse(id),
                Title = title,
                Description = description,
                Status = status,
                CreatedOn = DateTime.Parse(createdOn),
                DueDate = DateTime.Parse(dueDate)
            };

            MockRepository.Setup(a => a.GetById(todo.Id)).Returns(todo);
            MockRepository.Setup(a => a.Delete(todo.Id));
            var service = new Service.TodoService(MockRepository.Object);
            service.Delete(todo.Id);

            MockRepository.Verify(a => a.Save(), Times.Once);
        }

        [Fact]
        public void GivenNotExistingID_Delete_DoesntCallRepositorySave()
        {
            Guid id = Guid.Parse("0a000300-0600-0000-0100-0000f0700001");
            MockRepository.Setup(a => a.Delete(id));
            var service = new Service.TodoService(MockRepository.Object);
            service.Delete(id);

            MockRepository.Verify(a => a.Save(), Times.Never);
        }

        [Fact]
        public void GivenExistingTodo_DeleteByIndex_ReturnsTrue()
        {
            var mockTodos = new TodoModel[]
            {
                new TodoModel()
                {
                    Id = Guid.Parse("0a000300-0600-0000-0100-0000f0700001"),
                    Title = "Test",
                    Description = "Test Description",
                    Status = TodoStatus.Open,
                    DueDate = new DateTime(2021, 3, 4, 12, 30, 00),
                    CreatedOn = new DateTime(2020, 11, 12, 11, 55, 13),
                },
                new TodoModel()
                {
                    Id = Guid.Parse("0a000300-0600-0020-0100-0000f0300002"),
                    Title = "Second",
                    Description = "Second Test",
                    Status = TodoStatus.InProgress,
                    DueDate = new DateTime(2021, 1, 24, 1, 22, 33),
                    CreatedOn = new DateTime(2020, 10, 11, 12, 13, 14),
                },
            };

            int index = 1;
            MockRepository.Setup(a => a.GetAll()).Returns(mockTodos);
            MockRepository.Setup(a => a.GetById(mockTodos[index].Id)).Returns(mockTodos[index]);
            MockRepository.Setup(a => a.Delete(mockTodos[index].Id));
            var service = new Service.TodoService(MockRepository.Object);

            var isDeleted = service.DeleteByIndex(index);

            Assert.True(isDeleted);
        }

        [Fact]
        public void GivenExistingTodo_DeleteByIndex_RepositoryDeleteRemoveCorrectTodo()
        {
            var mockTodos = new TodoModel[]
{
                new TodoModel()
                {
                    Id = Guid.Parse("0a000300-0600-0000-0100-0000f0700001"),
                    Title = "Test",
                    Description = "Test Description",
                    Status = TodoStatus.Open,
                    DueDate = new DateTime(2021, 3, 4, 12, 30, 00),
                    CreatedOn = new DateTime(2020, 11, 12, 11, 55, 13),
                },
                new TodoModel()
                {
                    Id = Guid.Parse("0a000300-0600-0020-0100-0000f0300002"),
                    Title = "Second",
                    Description = "Second Test",
                    Status = TodoStatus.InProgress,
                    DueDate = new DateTime(2021, 1, 24, 1, 22, 33),
                    CreatedOn = new DateTime(2020, 10, 11, 12, 13, 14),
                },
            };

            int index = 1;
            MockRepository.Setup(a => a.GetAll()).Returns(mockTodos);
            MockRepository.Setup(a => a.GetById(mockTodos[index].Id)).Returns(mockTodos[index]);
            MockRepository.Setup(a => a.Delete(mockTodos[index].Id));
            var service = new Service.TodoService(MockRepository.Object);

            var isDeleted = service.DeleteByIndex(index);

            MockRepository.Verify(a => a.Delete(mockTodos[index].Id), Times.Once);
        }

        [Fact]
        public void GivenNotExistingTodo_DeleteByIndex_ReturnsFalse()
        {
            int index = 3;
            var service = new Service.TodoService(MockRepository.Object);

            var isDeleted = service.DeleteByIndex(index);

            Assert.False(isDeleted);
        }

        [Fact]
        public void GivenFilledRepository_HasTodos_ReturnTrue()
        {
            var mockTodos = new TodoModel[]
            {
                new TodoModel()
                {
                    Id = Guid.Parse("0a000300-0600-0000-0100-0000f0700001"),
                    Title = "Test",
                    Description = "Test Description",
                    Status = TodoStatus.Open,
                    DueDate = new DateTime(2021, 3, 4, 12, 30, 00),
                    CreatedOn = new DateTime(2020, 11, 12, 11, 55, 13),
                },
                new TodoModel()
                {
                    Id = Guid.Parse("0a000300-0600-0020-0100-0000f0300002"),
                    Title = "Second",
                    Description = "Second Test",
                    Status = TodoStatus.InProgress,
                    DueDate = new DateTime(2021, 1, 24, 1, 22, 33),
                    CreatedOn = new DateTime(2020, 10, 11, 12, 13, 14),
                },
            };

            MockRepository.Setup(a => a.GetAll()).Returns(mockTodos);
            var service = new Service.TodoService(MockRepository.Object);

            var hasTodos = service.HasTodos();

            Assert.True(hasTodos);
        }

        [Fact]
        public void GivenEmptyRepository_HasTodos_ReturnsFalse()
        {
            MockRepository.Setup(a => a.GetAll());
            var service = new Service.TodoService(MockRepository.Object);

            var hasTodos = service.HasTodos();

            Assert.False(hasTodos);
        }
    }
}
