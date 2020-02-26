using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TodoApp.ConsoleApp.Repositories.Models;

namespace TodoApp.ConsoleApp.Repositories.Interfaces
{
    interface ITodoRepository : IRepository<TodoModel, Guid>
    {
        
    }
}
