using System;

namespace TodoApp.ConsoleApp.Repositories.Models
{
    interface IModel<TId>
    {
         TId ID { get; set; }
    }
}
