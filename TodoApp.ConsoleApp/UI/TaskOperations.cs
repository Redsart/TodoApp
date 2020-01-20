using System;
using System.Collections.Generic;
using TodoApp.Library.Models;
using System.IO;
using TodoApp.ConsoleApp.Services;
using System.Linq;
using System.Globalization;


namespace TodoApp.ConsoleApp.UI
{
    public static class TaskOperations
    {
        const string path = "../../tasks.xml";
        static ITodoService service = new TodoService();
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
                        Console.WriteLine(UserComments.GoodBye());
                        toContinue = false;
                        break;
                    default:
                        Console.WriteLine(UserComments.InvalidComand());
                        break;
                }
            }
        }

        static void ReadTasks()
        {
            if (!File.Exists(path))
            {
                Console.WriteLine(UserComments.NoSavedTasks());
                return;
            }

            else
            {
                IEnumerable<Task> tasks = service.GetAll();
                int count = 1;
                foreach (var task in tasks)
                {
                    Console.WriteLine($"Task {count++}:\n{task.Title}\nDescription: {task.Message}\nstarted at: {task.StartDate}\nterm to: {task.EndDate}");
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
                    Console.WriteLine(UserComments.InvalidComand());
                    break;
            }
        }

        static void TaskMaker()
        {
            string title = UserInput.ReadText("Enter a title: ", true);
            string message = UserInput.ReadText("Enter a description: ", true);
            int deadLine = int.Parse(UserInput.ReadText("How many days you need to finish the task?: ",true),provider);


            var task = new Task(title, message, deadLine);

            bool isSave = UserInput.ReadYesNo("Do you want to save this task?");
            if (isSave)
            {
                Task savedTask = service.Create(task);
                Console.WriteLine(UserComments.SaveCompleted());
                Console.WriteLine(savedTask);
                Console.WriteLine();
                return;
            }
        }
    }
}
