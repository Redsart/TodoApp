using System.Xml.Linq;

namespace TodoApp.Repositories.XmlRepository.Utils
{
    interface IXmlContext
    {
        XElement GetContainer(XName name);
        void Save();
    }
}
