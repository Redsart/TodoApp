using System;

namespace TodoApp.ConsoleApp.Repositories.Models
{
    class TodoModel : IModel<Guid>
    {
        Guid IModel<Guid>.ID { get; set; }

        protected string Title { get; set; }

        protected string Discription { get; set; }

        protected DateTime DueDate { get; set; }

        protected DateTime CreatedOn { get; set; }

        protected TodoStatus Status { get; set; }
    }
}
