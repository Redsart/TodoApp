using System;
using TodoApp.Repositories.Models;

namespace TodoApp.Repositories.Interfaces
{
    public interface ITodoRepository : IRepository<TodoModel, Guid>
    {
        
    }
}
