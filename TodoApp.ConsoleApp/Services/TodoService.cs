using System.Collections.Generic;
using TodoApp.Library.Data;
using TodoApp.Library.Models;
using System.Linq;

namespace TodoApp.ConsoleApp.Services
{
    public class TodoService : ITodoService
    {
        const string path = "../../tasks.xml";

        public IEnumerable<Task> GetAll()
        {
            XMLTaskReader reader = new XMLTaskReader();

            IEnumerable<Task> tasks = reader.ReadTasks();

            return tasks;
        }

        public Task GetByID(int id)
        {
            IEnumerable<Task> tasks = GetAll();

            var wantedTask = tasks.First(task => int.Parse(task.ID.ToString()) == id);

            return wantedTask;
        }

        public void Update(Task task)
        {

        }

        public void Save(Task task)
        {
            XMLTaskWriter writer = new XMLTaskWriter();
            writer.Save(task);
        }

        public void Delete(int id)
        {
            XMLTaskWriter writer = new XMLTaskWriter();
            Task taskToBeDeleted = GetByID(id);
            writer.Delete(taskToBeDeleted);
        }
    }
}
