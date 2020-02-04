using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Xml;
using System.Xml.Linq;
using TodoApp.Library.Models;
using System.IO;
using TodoApp.ConsoleApp.Repositories;

namespace TodoApp.ConsoleApp.Repositories.XMLRepository
{
    abstract class RepositoryBase<T> : IRepository<TModel,TId> where T : Task
    {
        const string path = "../../tasks.xml";
        public static TimeSpan TimeLeft(DateTime startDate, DateTime endDate)
        {
            TimeSpan timeLeft = endDate.Subtract(startDate).Duration();

            return timeLeft;
        }

        public List<Task> ReadTasks()
        {
            XmlDocument doc = new XmlDocument();
            doc.Load(path);
            XmlElement root = doc.DocumentElement;
            XmlNodeList nodes;
            nodes = doc.SelectNodes("tasks/task");
            List<Task> tasks = new List<Task>();

            foreach (XmlNode node in nodes)
            {
                string title = node.SelectSingleNode("title").InnerText;
                string message = node.SelectSingleNode("description").InnerText;
                string start = node.SelectSingleNode("dateCreated").InnerText;
                DateTime startDate = DateTime.ParseExact(start, "dd MM yyyy", CultureInfo.InvariantCulture);
                string end = node.SelectSingleNode("deadline").InnerText;
                DateTime endDate = DateTime.ParseExact(end, "dd MM yyyy", CultureInfo.InvariantCulture);
                int deadline = TimeLeft(startDate, endDate).Days;
                string guid = node.Attributes["id"].Value;

                Task task = new Task(title, message, deadline)
                {
                    ID = Guid.Parse(guid)
                };
                tasks.Add(task);
            }

            return tasks;
        }

        public void Add(Task task)
        {
            if (!File.Exists(path))
            {
                XmlWriterSettings xmlWriterSettings = new XmlWriterSettings();
                xmlWriterSettings.Indent = true;
                Encoding encoding = Encoding.GetEncoding("UTF-8");
                using (XmlWriter writer = XmlWriter.Create(path))
                {
                    writer.WriteStartDocument();
                    writer.WriteStartElement("tasks");
                    writer.WriteEndElement();
                    writer.WriteEndDocument();
                    writer.Close();
                }
            }

            var xmlDoc = XDocument.Load(path);
            var parentElement = new XElement("task");
            var title = new XElement("title", task.Title);
            var description = new XElement("description", task.Message);
            var dateCreated = new XElement("dateCreated", string.Format("{0:dd MM yyyy}", task.StartDate));
            var timeLeft = new XElement("deadline", string.Format("{0:dd MM yyyy}", task.EndDate));
            var id = new XAttribute("id", task.ID);

            parentElement.Add(title);
            parentElement.Add(id);
            parentElement.Add(description);
            parentElement.Add(dateCreated);
            parentElement.Add(timeLeft);

            var rootElement = xmlDoc.Element("tasks");
            rootElement?.Add(parentElement);

            xmlDoc.Save(path);
        }

        public Task Get(int id)
        {
            throw new NotImplementedException();
        }

        public IEnumerable<Task> GetAll()
        {
            var tasks = ReadTasks();

            return tasks.ToList();
        }

        public void Remove(Task task)
        {
            if (!File.Exists(path))
            {
                throw new FileNotFoundException("There is no created task's!");
            }

            XmlDocument doc = new XmlDocument();
            doc.Load(path);
            XmlNodeList nodes;
            nodes = doc.SelectNodes("tasks/task");

            foreach (XmlNode node in nodes)
            {
                if (node.Attributes["id"].Value.ToString() == task.ID.ToString())
                {
                    node.ParentNode.RemoveChild(node);
                }
            }

            doc.Save(path);
        }

        public void Update(Task task)
        {
            throw new NotImplementedException();
        }
    }
}
