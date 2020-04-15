using System.Collections.Generic;
using TodoApp.ConsoleApp.Repositories.Models;
using TodoApp.ConsoleApp.Repositories.XmlRepository;
using System.Linq;
using System;

namespace TodoApp.ConsoleApp.Services
{
    public class TodoService : ITodoService
    {
        TodoRepository repo;
        static string Path { get; set; }

        public TodoService(string path)
        {
            Path = path;
            repo = new TodoRepository(Path);
        }   

        
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

            repo.Update(model);
            repo.Save();

            return model != null;
        }

        public TodoModel Create(TodoModel model)
        {
            repo.Insert(model);
            repo.Save();

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
            repo.Save();

            return true;
        }

        public bool DeleteByIndex(int index)
        {
            var models = repo.GetAll();
            var modelId = models.ElementAt(index).Id;
            bool isDeleted = this.Delete(modelId);
            return isDeleted;
        }
    }
}
