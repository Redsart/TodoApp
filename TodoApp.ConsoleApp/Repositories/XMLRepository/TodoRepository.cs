using System;
using System.Xml.Linq;
using TodoApp.ConsoleApp.Repositories.Models;
using TodoApp.ConsoleApp.Repositories.Interfaces;

namespace TodoApp.ConsoleApp.Repositories.XMLRepository
{
    public class TodoRepository : RepositoryBase<TodoModel, Guid>, ITodoRepository
    {
        string idName = "Id";
        public TodoRepository(string path) : base(path, "todos")
        {

        }

        protected override string IdName
        {
            get
            {
                return this.idName;
            }
        }

        protected override TodoModel ElementToEntity(XElement element)
        {
            if (element == null)
            {
                return null;
            }

            var entity = new TodoModel();
            entity.Title = XmlParser.GetString(element, "Title");
            entity.Description = XmlParser.GetString(element, "Description");
            entity.CreatedOn = XmlParser.GetDateTime(element, "CreatedOn");
            entity.DueDate = XmlParser.GetDateTime(element, "DueDate");
            entity.ID = XmlParser.GetGuid(element, IdName);

            return entity;
        }

        protected override XElement EntityToElement(TodoModel entity)
        {
            if (entity == null)
            {
                return null;
            }

            var element = new XElement("todo");
            XmlParser.SetString(element, "Title", entity.Title);
            XmlParser.SetString(element, "Description", entity.Description);
            XmlParser.SetDateTime(element, "CreatedOn", entity.CreatedOn);
            XmlParser.SetDateTime(element, "DueDate", entity.DueDate);
            XmlParser.SetGuid(element, IdName, entity.ID);

            return element;
        }
    }
}
