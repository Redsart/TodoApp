using System;
using System.Xml.Linq;
using TodoApp.ConsoleApp.Repositories.Models;

namespace TodoApp.ConsoleApp.Repositories.XMLRepository
{
    internal class TodoRepository : RepositoryBase<TodoModel, Guid>
    {
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
