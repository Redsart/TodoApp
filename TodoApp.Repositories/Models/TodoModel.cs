using System;
using System.Globalization;

namespace TodoApp.Repositories.Models
{
    public class TodoModel : IModel<Guid>
    {
        public Guid Id { get; set; }

        public string Title { get; set; }

        public string Description { get; set; }

        public DateTime DueDate { get; set; }

        public DateTime CreatedOn { get; set; }

        public TodoStatus Status { get; set; }

        public override string ToString()
        {
            return string.Format(CultureInfo.CurrentCulture, $"title:{this.Title}\n{this.Status}\n{this.Description}\n{this.CreatedOn}\n{this.DueDate}");
        }
    }
}
