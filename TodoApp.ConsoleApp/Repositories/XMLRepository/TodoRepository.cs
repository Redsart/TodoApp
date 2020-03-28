using System;
using System.Xml.Linq;
using TodoApp.ConsoleApp.Repositories.Models;
using TodoApp.ConsoleApp.Repositories.Interfaces;

namespace TodoApp.ConsoleApp.Repositories.XMLRepository
{
    public class TodoRepository : RepositoryBase<TodoModel, Guid>, ITodoRepository
    {
        public TodoRepository(string path) : base(path, "todos")
        {

        }

        protected override string IdName => throw new NotImplementedException();

        protected override TodoModel ElementToEntity(XElement element)
        {
            throw new NotImplementedException();
        }

        protected override XElement EntityToElement(TodoModel entity)
        {
            throw new NotImplementedException();
        }
    }
}
