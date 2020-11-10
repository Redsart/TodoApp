using System;
using System.Collections.Generic;
using System.Xml.Linq;
using TodoApp.Repositories.Interfaces;
using TodoApp.Repositories.Models;
using TodoApp.Repositories.XmlRepository.Utils;
using System.Linq;
using System.Data;

namespace TodoApp.Repositories.XmlRepository
{
    public abstract class RepositoryBase<TModel, TId> : IRepository<TModel, TId> where TModel : IModel<TId>
    {
        protected XElement ContainerElement { get; }
        protected abstract string IdName { get; }
        protected IXmlContext Context;

        protected RepositoryBase(IXmlContext context, XName name)
        {
            Context = context;

            ContainerElement = context.GetContainer(name);
        }

        protected XElement GetElementById(TId id)
        {
            XElement element = ContainerElement.Elements()
                .FirstOrDefault(a => a.Attribute(IdName).Value == id.ToString());

            if (element == null)
            {
                return null;
            }

            return element;
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
            //argoutofrange ex name of id
            //assert dontchangecontext
            // assert null and wrong id with theory
        }

        public IEnumerable<TModel> Get(Func<TModel, bool> filter)
        {
            var models = GetAll().Where(filter);
            return models;
        }

        public IEnumerable<TModel> Get<TOrderKey>(Func<TModel, TOrderKey> orderByKey)
        {
            var models = GetAll().OrderBy(orderByKey);

            return models;
        }

        public IEnumerable<TModel> Get<TOrderKey>(Func<TModel, bool> filter, Func<TModel, TOrderKey> orderByKey)
        {
            var models = GetAll().Where(filter).OrderBy(orderByKey);

            return models;
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

            Guid id = Guid.NewGuid();
            newElement.Attribute(IdName).SetValue(id);
            ContainerElement.Add(newElement);

            TModel newModel = ElementToEntity(newElement);

            return newModel;
        }

        public bool Save()
        {
            Context.Save();
            return true;
        }

        public void Update(TModel model)
        {
            var element = EntityToElement(model);
            if (!ContainerElement.Attributes().Contains(element.Attribute("Id")) || model.Id == null)
            {
                return;
            }
            var oldElement = GetElementById(model.Id);
            var newElement = EntityToElement(model);

            oldElement?.ReplaceWith(newElement);
        }
    }
}
