using System.Collections.Generic;
using TodoApp.Library.Data;
using TodoApp.Library.Models;
using System.Linq;
using System;

namespace TodoApp.ConsoleApp.Services
{
    public class TodoService : ITodoService
    {
        public IEnumerable<Task> GetAll()
        {
            XMLTaskReader reader = new XMLTaskReader();

            IEnumerable<Task> tasks = reader.ReadTasks();

            return tasks;
        }

        public Task GetByID(Guid id)
        {
            IEnumerable<Task> tasks = GetAll();

            Task wantedTask = null;

            wantedTask = tasks.FirstOrDefault(task => task.ID == id);

            return wantedTask;
        }

        public bool Update(Task task)
        {
            bool isDeleted = Delete(task.ID);

            if (!isDeleted)
            {
                return false;
            }
            Task newTask = Create(task);



            return newTask != null;
        }

        public Task Create(Task task)
        {
            XMLTaskWriter writer = new XMLTaskWriter();
            writer.Save(task);

            return task;
        }

        public bool Delete(Guid id)
        {
            Task taskToBeDeleted = GetByID(id);
            
            if (taskToBeDeleted == null)
            {
                return false;
            }

            XMLTaskWriter writer = new XMLTaskWriter();
            writer.Delete(taskToBeDeleted);
            return true;
        }

        bool IsFoundId(Guid id)
        {
            IEnumerable<Task> tasks = GetAll();
            Task wantedTask = null;
            bool isFound = false;
            try
            {
                wantedTask = tasks.FirstOrDefault(task => task.ID == id);
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
