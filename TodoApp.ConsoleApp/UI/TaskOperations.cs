using System;
using System.Collections.Generic;
using TodoApp.Library.Models;
using System.IO;
using TodoApp.ConsoleApp.Services;


namespace TodoApp.ConsoleApp.UI
{
    public static class TaskOperations
    {
        const string path = "../../tasks.xml";
        static TodoService service = new TodoService();

        public static void ReadOrWrite()
        {
            bool toContinue = true;

            while (toContinue)
            {
                int operation = UserInput.ReadOption("Choose an option!", new string[] { "manipulate task", "read task's" }, true);

                switch (operation)
                {
                    case 1:
                        ManipulateTask();
                        break;
                    case 2:
                        ReadTasks();
                        break;
                    default:
                        Console.WriteLine("Error! Invalid comand!");
                        break;
                }

                toContinue = UserInput.ReadYesNo("Do you want to continue?",true);
            }


            Console.WriteLine("Good bye!");
            return;
        }

        static void ReadTasks()
        {
            if (!File.Exists(path))
            {
                Console.WriteLine("There is no saved task's!");
                return;
            }

            else
            {
                IEnumerable<Task> tasks = service.GetAll();

                foreach (var task in tasks)
                {
                    Console.WriteLine($"Task {task.ID}:\n{task.Title}\nDescription: {task.Message}\nstarted at: {task.StartDate}\nterm to: {task.EndDate}");
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
                    IEnumerable<Task> tasks = service.GetAll();
                    string id = UserInput.ReadText("Select the number of the task, you want to delete: ", true);
                    service.Delete(Guid.Parse(id));
                    break;
                default:
                    Console.WriteLine("Error! Invalid comand!");
                    break;
            }
        }

        static void TaskMaker()
        {
            string title = UserInput.ReadText("Enter a title: ", true);
            string message = UserInput.ReadText("Enter a description: ", true);
            int deadLine = int.Parse(UserInput.ReadText("How many days you need to finish the task: ",true));

            var task = new Task(title, message, deadLine);

            bool isSave = UserInput.ReadYesNo("Do you want to save this task?");
            if (isSave)
            {
                Task savedTask = service.Create(task);
                Console.WriteLine("Save completed!");
                Console.WriteLine(savedTask);
                Console.WriteLine();
                return;
            }

            else
            {
                Console.WriteLine("Good luck!");
                return;
            }
        }
    }
}
