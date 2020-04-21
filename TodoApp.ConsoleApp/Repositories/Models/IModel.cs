using System;

namespace TodoApp.ConsoleApp.Repositories.Models
{
    public interface IModel<TId>
    {
         TId Id { get; set; }
    }
}
