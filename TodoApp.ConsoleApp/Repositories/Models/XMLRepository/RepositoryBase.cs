using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Xml;
using System.Xml.Linq;
using TodoApp.ConsoleApp.Repositories.Interfaces;
using TodoApp.Library.Models;



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


        public void Delete(TId id, bool isDeleted)
        {
            throw new NotImplementedException();
        }

        public IEnumerable<TModel> Get()
        {
            throw new NotImplementedException();
        }

        public IEnumerable<TModel> GetAll()
        {
            throw new NotImplementedException();
        }

        public TModel GetById(TId id)
        {
            throw new NotImplementedException();
        }

        public TModel Insert(TModel model, bool isInserted)
        {
            throw new NotImplementedException();
        }

        public bool Save()
        {
            throw new NotImplementedException();
        }

        public void Update(TModel model, bool isUpdated)
        {
            throw new NotImplementedException();
        }
    }
}
