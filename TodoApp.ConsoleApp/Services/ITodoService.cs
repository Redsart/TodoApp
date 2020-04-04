using System.Collections.Generic;
using TodoApp.ConsoleApp.Repositories.Models;
using System;

namespace TodoApp.ConsoleApp.Services
{
    interface ITodoService
    {
        IEnumerable<TodoModel> GetAll();
        TodoModel GetByID(Guid id);
        bool Update(TodoModel model);
        TodoModel Create(TodoModel model);
        bool Delete(Guid id);
        bool DeleteByIndex(int index);
    }
}
