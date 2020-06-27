using System.Collections.Generic;
using TodoApp.Repositories.Models;
using System;

namespace TodoApp.Services
{
    public interface ITodoService
    {
        IEnumerable<TodoModel> GetAll();
        TodoModel GetByID(Guid id);
        bool Update(TodoModel model);
        TodoModel Create(TodoModel model);
        bool Delete(Guid id);
        bool DeleteByIndex(int index);
        bool HasTodos();
    }
}
