using System;
using TodoApp.ConsoleApp.Repositories.Models;

namespace TodoApp.ConsoleApp.Repositories.Interfaces
{
    public interface ITodoRepository : IRepository<TodoModel, Guid>
    {
        
    }
}
