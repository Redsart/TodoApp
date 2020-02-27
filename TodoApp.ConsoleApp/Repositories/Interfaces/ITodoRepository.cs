using System;
using TodoApp.ConsoleApp.Repositories.Models;

namespace TodoApp.ConsoleApp.Repositories.Interfaces
{
    interface ITodoRepository : IRepository<TodoModel, Guid>
    {
        
    }
}
