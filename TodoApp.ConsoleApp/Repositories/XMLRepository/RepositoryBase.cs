using System;
using System.Collections.Generic;
using System.Xml.Linq;
using TodoApp.ConsoleApp.Repositories.Interfaces;
using System.Linq;
using System.IO;
using System.Data;

namespace TodoApp.ConsoleApp.Repositories.Models.XmlRepository
{
    abstract class RepositoryBase<TModel, TId> : IRepository<TModel, TId> where TModel : IModel<TId>
    {
        protected string Path { get; }
        protected XDocument Document { get; }
        protected XElement ContainerElement { get; }

        protected RepositoryBase(string path, XName containerName)
        {
            Path = path;
            Document = XDocument.Load(path);
            ContainerElement = Document.Element(containerName);
        }

        protected abstract TModel ElementToEntity(XElement element);
        protected abstract XElement EntityToElement(TModel entity);

        public IEnumerable<TModel> GetAll()
        {
            var models = from el
                         in ContainerElement.Elements()
                         select ElementToEntity(el);

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
            XElement element = ContainerElement.Elements().FirstOrDefault(a => a.Attribute("id").Value == id.ToString());

            TModel model = ElementToEntity(element);

            return model;
        }

        public TModel Insert(TModel model)
        {
            Guid id = new Guid();
            XElement element = EntityToElement(model);
            element.Attribute("id").SetValue(id);
            TModel modelwithId = ElementToEntity(element);

            return modelwithId;
        }

        public bool Save()
        {
            bool isPath = false;
            if (Directory.Exists(Path))
            {
                isPath = true;
                isPath = true;
                Document.Save(Path);
            }

            return isPath;
        }

        public void Update(TModel model)
        {
            throw new NotImplementedException();
        }
    }
}
