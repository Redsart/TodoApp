using System.Text;
using TodoApp.Library.Models;
using System.IO;
using System.Xml;
using System.Xml.Linq;
using System;

namespace TodoApp.Library.Data
{
    public class XMLTaskWriter
    {
        private readonly string _path;

        public XMLTaskWriter()
        {
            _path = "../../tasks.xml";
        }

        public XMLTaskWriter(string path)
        {
            _path = path;
        }

        public void Save(Task task)
        {

            if (!File.Exists(_path))
            {
                XmlWriterSettings xmlWriterSettings = new XmlWriterSettings();
                xmlWriterSettings.Indent = true;
                Encoding encoding = Encoding.GetEncoding("UTF-8");
                using (XmlWriter writer = XmlWriter.Create(_path))
                {
                    writer.WriteStartDocument();
                    writer.WriteStartElement("tasks");
                    writer.WriteEndElement();
                    writer.WriteEndDocument();
                    writer.Close();
                }
            }

            var xmlDoc = XDocument.Load(_path);
            var parentElement = new XElement("task");
            var title = new XElement("title", task.Title);
            var description = new XElement("description", task.Message);
            var dateCreated = new XElement("dateCreated", string.Format("{0:dd MM yyyy}",task.StartDate));
            var timeLeft = new XElement("deadline", string.Format("{0:dd MM yyyy}", task.EndDate));
            var id = new XAttribute("id", task.ID);

            parentElement.Add(title);
            parentElement.Add(id);
            parentElement.Add(description);
            parentElement.Add(dateCreated);
            parentElement.Add(timeLeft);

            var rootElement = xmlDoc.Element("tasks");
            rootElement?.Add(parentElement);

            xmlDoc.Save(_path);
        }

        public void Delete(Task task)
        {
            if (!File.Exists(_path))
            {
                throw new FileNotFoundException("There is no created task's!");
            }

            XmlDocument doc = new XmlDocument();
            doc.Load(_path);
            XmlNodeList nodes;
            nodes = doc.SelectNodes("tasks/task");

            foreach (XmlNode node in nodes)
            {
                if (node.Attributes["id"].Value.ToString() == task.ID.ToString())
                {
                    node.ParentNode.RemoveChild(node);
                }
            }

            doc.Save(_path);
        }
    }
}
