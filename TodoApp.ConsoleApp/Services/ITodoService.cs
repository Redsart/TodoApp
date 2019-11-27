using System.Collections.Generic;
using TodoApp.Library.Models;
using System;

namespace TodoApp.ConsoleApp.Services
{
    interface ITodoService
    {
        IEnumerable<Task> GetAll();
        Task GetByID(Guid id);
        bool Update(Task task);
        Task Create(Task task);
        bool Delete(Guid id);
        bool DeleteByIndex(int index);
    }
}
