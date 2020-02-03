using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;

namespace TodoApp.ConsoleApp.Repositories
{
    abstract class XMLRepository<T> : IRepository<T>
    {
        const string path = "../../tasks.xml";
        public virtual XElement ParentElement { get; protected set; }

        protected XName ElementName { get; private set; }

        protected abstract Func<XElement, T> Selector { get; }

        protected abstract void SetXElementValue(T model, XElement element);

        protected abstract XElement CreateXElement(T model);

        protected abstract object GetEntityId(T entidade);
        public void Add(T entity)
        {
            ParentElement.Add(CreateXElement(entity));
        }

        public T Get(int id)
        {
            return 
        }

        public IEnumerable<T> GetAll()
        {
            return ParentElement.Elements(ElementName).Select(Selector);
        }

        public virtual IEnumerable<T> GetAll(object parentId)
        {
            throw new InvalidOperationException("This entity doesn't contains a parent.");
        }
        public void Remove(T entity)
        {
            ParentElement.Elements().FirstOrDefault(a => a.Attribute("Id").Value == GetEntityId(entity).ToString()).Remove();
        }

        public void Update(T entity)
        {
            SetXElementValue(entity,
                            ParentElement.Elements().FirstOrDefault(a => a.Attribute("Id").Value == GetEntityId(entity).ToString()
));
        }
    }
}
