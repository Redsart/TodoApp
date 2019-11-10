using System.Collections.Generic;
using TodoApp.Library.Models;

namespace TodoApp.ConsoleApp.Services
{
    interface ITodoService
    {
        IEnumerable<Task> GetAll();
        Task GetByID(string id);
        bool Update(Task task);
        Task Create(Task task);
        bool Delete(string id);
    }
}
