using System;
using System.Collections.Generic;
using System.Xml.Linq;
using TodoApp.ConsoleApp.Repositories.Interfaces;
using TodoApp.Library.Data;
using System.Xml;
using System.Linq;

namespace TodoApp.ConsoleApp.Repositories.Models.XmlRepository
{
    abstract class RepositoryBase<TModel, TId> : IRepository<TModel, TId> where TModel : Imodel<TId>
    {
        protected string Path { get; }
        protected XDocument Document { get; }
        protected XElement ContainerElement { get; }

        protected RepositoryBase(string path,XName containerName)
        {
            Path = path;
            Document = XDocument.Load(path);
            ContainerElement = Document.Element(containerName);
        }

        protected abstract TModel ElementToEntity(XElement element);
        protected abstract XElement EntityToElement(TModel entity);

        public IEnumerable<TModel> GetAll()
        {
            List<TModel> models = new List<TModel>();

            foreach (var element in XMLContent())
            {
                models.Add(ElementToEntity(element));
            }
            return models;
        }

        public void Delete(TId id)
        {
            ContainerElement.Elements().FirstOrDefault(a => a.Attribute("id").Value == id.ToString()).Remove();
        }

        public IEnumerable<TModel> Get()
        {
            throw new NotImplementedException();
        }

        public TModel GetById(TId id)
        {
            throw new NotImplementedException();
        }

        public TModel Insert(TModel model)
        {
            throw new NotImplementedException();
        }

        public bool Save()
        {
            throw new NotImplementedException();
        }

        public void Update(TModel model)
        {
            throw new NotImplementedException();
        }

        public IEnumerable<XElement> XMLContent()
        {
            IEnumerable<XElement> elements = from el
                                             in ContainerElement.Elements()
                                             select el;
            return elements;
        }
    }
}
