using System;
using TodoApp.ConsoleApp.Repositories.Interfaces;

namespace TodoApp.ConsoleApp.Repositories.Models
{
    class TodoModel : IModel<Guid>
    {
        Guid ID { get; set; }

        string Title { get; set; }

        string Discription { get; set; }

        DateTime DueDate { get; set; }

        DateTime CreatedOn { get; set; }

        TodoStatus Status { get; set; }
    }
}
