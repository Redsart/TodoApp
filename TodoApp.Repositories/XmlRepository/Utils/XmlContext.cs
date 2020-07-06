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
            Document = XDocument.Load(path);
        }

        public XElement GetContainer(XName name)
        {
            XElement containerElement = Document.Element(name);

            return containerElement;
        }

        public void Save()
        {
            Document.Save(Path);
        }
    }
}
