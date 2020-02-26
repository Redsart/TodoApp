using System.Collections.Generic;

namespace TodoApp.ConsoleApp.Repositories.Interfaces
{
    interface IRepository<TModel, TId> where TModel : Imodel<TId>
    {
        IEnumerable<TModel> GetAll();
        IEnumerable<TModel> Get();
        TModel GetById(TId id);
        TModel Insert(TModel model, bool isInserted);
        void Update(TModel model , bool isUpdated);
        void Delete(TId id, bool isDeleted);
        bool Save();
    }
}
