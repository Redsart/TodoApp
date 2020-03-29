using System;
using System.Xml.Linq;
using TodoApp.ConsoleApp.Repositories.Models;
using TodoApp.ConsoleApp.Repositories.Interfaces;
using System.IO;
using System.Globalization;

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
            if (element == null)
            {
                
            }

            string title = element.Element("title").Value;
            string description = element.Element("description").Value;
            string start = element.Element("createdOn").Value;
            DateTime dateCreated = DateTime.ParseExact(start, "dd MM yyyy", CultureInfo.InvariantCulture);
            string end = element.Element("dueDate").Value;
            DateTime dueDate= DateTime.ParseExact(end, "dd MM yyyy", CultureInfo.InvariantCulture);
            Guid id = Guid.Parse(element.Attribute("id").Value);

            var model = new TodoModel();
            model.Title = title;
            model.Description = description;
            model.CreatedOn = dateCreated;
            model.DueDate = dueDate;
            model.ID = id;

            return model;
        }

        protected override XElement EntityToElement(TodoModel entity)
        {
            if (!File.Exists(Path))
            {
                
            }
            
            var xmlDoc = XDocument.Load(Path);
            var parentElement = new XElement("todo");
            var title = new XElement("title", entity.Title);
            var description = new XElement("description", entity.Description);
            var dateCreated = new XElement("createdOn", string.Format("{0:dd MM yyyy}", entity.CreatedOn));
            var dueDate = new XElement("dueDate", string.Format("{0:dd MM yyyy}", entity.DueDate));
            var id = new XAttribute("id", entity);

            parentElement.Add(title);
            parentElement.Add(id);
            parentElement.Add(description);
            parentElement.Add(dateCreated);
            parentElement.Add(dueDate);

            var rootElement = xmlDoc.Element("todos");
            rootElement?.Add(parentElement);

            xmlDoc.Save(Path);

            return parentElement;
        }
    }
}
