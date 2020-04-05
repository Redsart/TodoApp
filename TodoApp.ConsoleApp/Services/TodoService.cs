using System.Collections.Generic;
using TodoApp.ConsoleApp.Repositories.Models;
using TodoApp.ConsoleApp.Repositories.XMLRepository;
using TodoApp.Library.Data;
using System.Linq;
using System;

namespace TodoApp.ConsoleApp.Services
{
    public class TodoService : ITodoService
    {
        static string Path { get; set; }
        TodoRepository repo = new TodoRepository(Path);

        public IEnumerable<TodoModel> GetAll()
        {
            return repo.GetAll();
        }

        public TodoModel GetByID(Guid id)
        {
            return repo.GetById(id);
        }

        public bool Update(TodoModel model)
        {
            if (model == null)
            {
                throw new ArgumentNullException(nameof(model));
            }

            bool isDeleted = Delete(model.ID);

            if (!isDeleted)
            {
                return false;
            }

            var newModel = Create(model);

            return newModel != null;
        }

        public TodoModel Create(TodoModel model)
        {
            XMLTaskWriter.Save(model);

            return model;
        }

        public bool Delete(Guid id)
        {
            TodoModel modelToBeDeleted = GetByID(id);
            
            if (modelToBeDeleted == null)
            {
                return false;
            }

            repo.Delete(id);
            return true;
        }

        public bool DeleteByIndex(int index)
        {
            var models = this.GetAll();
            var modelId = models.ElementAt(index).ID;
            return this.Delete(modelId);
        }
    }
}
