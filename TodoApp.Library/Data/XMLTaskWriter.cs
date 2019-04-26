using System.Text;
using TodoApp.Library.Models;
using System.IO;
using System.Xml;
using System.Xml.Linq;

namespace TodoApp.Library.Data
{
    public class XMLTaskWriter
    {
        static string path = "../../tasks.xml";
        public void Save(Task task)
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
            var dateCreated = new XElement("dateCreated", string.Format("{0:dd MM yyyy}",task.StartDate));
            var timeLeft = new XElement("deadline", string.Format("{0:dd MM yyyy}", task.EndDate));
            
            parentElement.Add(title);
            parentElement.Add(description);
            parentElement.Add(dateCreated);
            parentElement.Add(timeLeft);

            var rootElement = xmlDoc.Element("tasks");
            rootElement?.Add(parentElement);

            xmlDoc.Save(path);
        }
    }
}
