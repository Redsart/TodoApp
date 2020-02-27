using System.Collections.Generic;

namespace TodoApp.ConsoleApp.Repositories.Interfaces
{
    interface IRepository<TModel, TId> where TModel : Imodel<TId>
    {
        IEnumerable<TModel> GetAll();
        IEnumerable<TModel> Get();
        TModel GetById(TId id);
        TModel Insert(TModel model);
        void Update(TModel model);
        void Delete(TId id);
        bool Save();
    }
}
