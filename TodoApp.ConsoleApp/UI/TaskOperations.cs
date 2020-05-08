using System;
using System.Collections.Generic;
using TodoApp.Repositories.Models;
using System.IO;
using TodoApp.Services;
using System.Linq;
using System.Globalization;

namespace TodoApp.ConsoleApp.UI
{
    public static class TaskOperations
    {
        const string path = "../../data/todos.xml"; 
        static ITodoService service = new TodoService(path);
        static readonly IFormatProvider provider = CultureInfo.CurrentCulture;

        public static void ReadOrWrite()
        {
            bool toContinue = true;

            while (toContinue)
            {
                int operation = UserInput.ReadOption("Choose an option!", new string[] { "manipulate task", "read task's", "exit program" }, true);

                switch (operation)
                {
                    case 1:
                        ManipulateTask();
                        break;
                    case 2:
                        ReadTasks();
                        break;
                    case 3:
                        Console.WriteLine(Messages.GoodBye());
                        toContinue = false;
                        break;
                    default:
                        Console.WriteLine(Messages.InvalidComand());
                        break;
                }
            }
        }

        static void ReadTasks()
        {
            if (!File.Exists(path))
            {
                Console.WriteLine(Messages.NoSavedTasks());
                return;
            }

            else
            {
                IEnumerable<TodoModel> models = service.GetAll();
                int count = 1;
                foreach (var model in models)
                {
                    Console.WriteLine($"Task {count++}:\n{model.Title}\nDescription: {model.Description}\nstarted at: {model.CreatedOn}\nterm to: {model.DueDate}");
                    Console.WriteLine();
                }
            }
        }

        static void ManipulateTask()
        {
            int operation = UserInput.ReadOption("Choose an option!", new string[] { "make a new task", "delete a task" }, true);

            switch (operation)
            {
                case 1:
                    TaskMaker();
                    bool choice = UserInput.ReadYesNo("Do yoy want to make another task?", true);
                    if (choice)
                    {
                        TaskMaker();
                    };
                    break;

                case 2:
                    int tasksCount = service.GetAll().Count();
                    int index = UserInput.ReadInt("Select the number of the task, you want to delete: ", 1, tasksCount);
                    service.DeleteByIndex(index-1);
                    break;
                default:
                    Console.WriteLine(Messages.InvalidComand());
                    break;
            }
        }

        static void TaskMaker()
        {
            string title = UserInput.ReadText("Enter a title: ", true);
            string description = UserInput.ReadText("Enter a description: ", true);
            DateTime duedate = UserInput.ReadDate();

            var model = new TodoModel() { Title = title, Description = description, CreatedOn = DateTime.Now, DueDate = duedate};

            bool isSave = UserInput.ReadYesNo("Do you want to save this task?");
            if (isSave)
            {
                TodoModel savedModel = service.Create(model);
                Console.WriteLine(Messages.SaveCompleted());
                Console.WriteLine(savedModel);
                Console.WriteLine();
                return;
            }
        }
    }
}
