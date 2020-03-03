using System;
using System.Collections.Generic;

namespace TodoApp.ConsoleApp.Repositories.Interfaces
{
    interface IRepository<TModel, TId> where TModel : IModel<TId>
    {
        IEnumerable<TModel> GetAll();
        IEnumerable<TModel> Get(Func<TModel, bool> filter);
        TModel GetById(TId id);
        TModel Insert(TModel model);
        void Update(TModel model);
        void Delete(TId id);
        bool Save();
    }
}
