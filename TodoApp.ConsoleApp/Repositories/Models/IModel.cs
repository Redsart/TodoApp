using System;

namespace TodoApp.ConsoleApp.Repositories.Models
{
    public interface IModel<TId>
    {
         TId ID { get; set; }
    }
}
