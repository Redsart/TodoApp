using System.Collections.Generic;
using TodoApp.Library.Data;
using TodoApp.Library.Models;
using System.Linq;
using System;

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

        public Task GetByID(string id)
        {
            IEnumerable<Task> tasks = GetAll();

            Task wantedTask = null;

            try
            {
                wantedTask = tasks.First(task => task.ID.ToString() == id);
            }
            catch (InvalidOperationException)
            {
                Console.WriteLine("Invalid id!");
            }

            return wantedTask;
        }

        public bool Update(Task task)
        {
            bool isUpdated = false;

            Task updatedTask = null;
            Create(updatedTask);

            if (updatedTask != null)
            {
                Delete(task.ID.ToString());
                isUpdated = true;
            }

            return isUpdated;
        }

        public Task Create(Task task)
        {
            XMLTaskWriter writer = new XMLTaskWriter();
            writer.Save(task);

            return task;
        }

        public bool Delete(string id)
        {
            bool isDeletable = false;
            if (IsFoundId(id))
            {
                XMLTaskWriter writer = new XMLTaskWriter();
                Task taskToBeDeleted = GetByID(id);
                writer.Delete(taskToBeDeleted);
                Console.WriteLine("Delete completed!");
                isDeletable = true;
            }
            else
            {
                Console.WriteLine("Delete not complete!");
            }

            return isDeletable;
        }

        bool IsFoundId(string id)
        {
            IEnumerable<Task> tasks = GetAll();
            Task wantedTask = null;
            bool isFound = false;
            try
            {
                wantedTask = tasks.First(task => task.ID.ToString() == id);
                isFound = true;
            }
            catch (InvalidOperationException)
            {
                Console.WriteLine("Invalid id!");
            }

            return isFound;
        }
    }
}
