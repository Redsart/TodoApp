using System.Collections.Generic;
using TodoApp.Library.Data;
using TodoApp.Library.Models;
using System.Linq;

namespace TodoApp.ConsoleApp.Services
{
    static class TodoService
    {
        public static List<Task> GetAll(string path = "")
        {
            XMLTaskReader reader = new XMLTaskReader();

            List<Task> tasks = reader.ReadTasks(path);

            return tasks;
        }

        static Task GetByID(int id)
        {
            List<Task> tasks = GetAll();

            var wantedTask = tasks.First(task => int.Parse(task.ID.ToString()) == id);

            return wantedTask;
        }

        static void Update(Task task)
        {

        }

        public static void Save(Task task)
        {
            XMLTaskWriter writer = new XMLTaskWriter();
            writer.Save(task);
        }

        static void Delete(int id)
        {
            XMLTaskWriter writer = new XMLTaskWriter();
            Task taskToBeDeleted = GetByID(id);
            writer.Delete(taskToBeDeleted);
        }
    }
}
