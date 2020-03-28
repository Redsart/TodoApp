using System;

namespace TodoApp.ConsoleApp.Repositories.Models
{
    public class TodoModel : IModel<Guid>
    {
        Guid IModel<Guid>.ID { get; set; }

        public string Title { get; set; }

        public string Discription { get; set; }

        public DateTime DueDate { get; set; }

        public DateTime CreatedOn { get; set; }

        public TodoStatus Status { get; set; }
    }
}
