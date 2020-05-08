using System;

namespace TodoApp.Repositories.Models
{
    public interface IModel<TId>
    {
         TId Id { get; set; }
    }
}
