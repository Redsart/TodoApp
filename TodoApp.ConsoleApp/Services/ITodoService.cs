using System.Collections.Generic;
using TodoApp.Library.Models;

namespace TodoApp.ConsoleApp.Services
{
    interface ITodoService
    {
        IEnumerable<Task> GetAll();
        Task GetByID(int id);
        void Update(Task task);
        void Save(Task task);
        void Delete(int id);
    }
}
