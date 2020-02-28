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

        protected abstract string IdName { get; }

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
            GetElementById(id)
                .Remove();
        }

        public IEnumerable<TModel> Get()
        {
            throw new NotImplementedException();
        }

        public TModel GetById(TId id)
        {
            XElement element = GetElementById(id);

            TModel model = ElementToEntity(element);

            return model;
        }

        public TModel Insert(TModel model)
        {
            XElement newElement = EntityToElement(model);

            Guid id = new Guid();
            newElement.Attribute("id").SetValue(id);

            TModel newModel = ElementToEntity(newElement);

            return newModel;
        }

        public bool Save()
        {
            if (!Directory.Exists(Path))
            {
                return false;
                
            }

            Document.Save(Path);
            return true;
        }

        public void Update(TModel model)
        {
            var oldElement = GetElementById(model.Id);
            var newElement = EntityToElement(model);

            oldElement?.ReplaceWith(newElement);
        }

        XElement GetElementById(TId id)
        {
            XElement element = ContainerElement.Elements()
                .FirstOrDefault(a => a.Attribute("id").Value == id.ToString());

            return element;
        }
    }
}
