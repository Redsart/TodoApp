using System.Collections.Generic;
using TodoApp.Library.Models;

namespace TodoApp.ConsoleApp.Services
{
    interface ITodoService
    {
        IEnumerable<Task> GetAll();
        Task GetByID(string id);
        void Update(Task task);
        void Create(Task task);
        void Delete(string id);
    }
}
