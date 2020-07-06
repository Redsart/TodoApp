using System.Xml.Linq;

namespace TodoApp.Repositories.XmlRepository.Utils
{
    public interface IXmlContext
    {
        XElement GetContainer(XName name);
        void Save();
    }
}
