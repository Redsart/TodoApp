using System.Xml.Linq;

namespace TodoApp.Repositories.XmlRepository.Utils
{
    class XmlContext : IXmlContext
    {
        static string Path { get; set; }

        static XDocument doc;

        XmlContext(string path)
        {
            Path = path;
            doc = XDocument.Load(path);
        }

        public XElement GetContainer(XName name)
        {
            XElement containerElement = doc.Element(name);

            return containerElement;
        }

        public void Save()
        {
            doc.Save(Path);
        }
    }
}
