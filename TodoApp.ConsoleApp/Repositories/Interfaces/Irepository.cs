using System;
using System.Collections.Generic;
using TodoApp.ConsoleApp.Repositories.Models;

namespace TodoApp.ConsoleApp.Repositories.Interfaces
{
    interface IRepository<TModel, TId> where TModel : IModel<TId>
    {
        IEnumerable<TModel> GetAll();
        IEnumerable<TModel> Get(Func<TModel, bool> filter);
        IEnumerable<TModel> Get<TOrderKey>(Func<TModel, TOrderKey> orderByKey);
        IEnumerable<TModel> Get<TOrderKey>(Func<TModel, bool> filter, Func<TModel, TOrderKey> orderByKey);
        TModel GetById(TId id);
        TModel Insert(TModel model);
        void Update(TModel model);
        void Delete(TId id);
        bool Save();
    }
}
