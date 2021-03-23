using System.IO;
using System.Xml.Linq;

namespace TodoApp.Repositories.XmlRepository.Utils
{
    public class XmlContext : IXmlContext
    {
        static string Path { get; set; }

        static XDocument Document;

        public XmlContext(string path)
        {
            Path = path;
            if (File.Exists(path))
            {
                Document = XDocument.Load(path);
            }
            else
            {
                Document = new XDocument();
            }
        }

        public XElement GetContainer(XName name)
        {
            XElement containerElement = Document.Element(name);

            if (containerElement == null)
            {
                containerElement = new XElement(name);
                Document.AddFirst(containerElement);
            }

            return containerElement;
        }

        public void Save()
        {
            if (Path.Contains("/"))
            {
                Directory.CreateDirectory(Path.Substring(0, Path.LastIndexOf('/')));
            }
            Document.Save(Path);
        }
    }
}
