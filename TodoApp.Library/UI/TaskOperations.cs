using System;
using System.Collections.Generic;
using TodoApp.Library.Models;
using TodoApp.Library.Data;
using System.IO;

namespace TodoApp.Library.UI
{
    public static class TaskOperations
    {
        public static void ReadOrWrite()
        {
            int operation = ConsoleUserInput.ReadOption("Choose an option!", new string[] { "manipulate task", "read task's" });
            if (operation == 1)
            {
                ManipulateTask();
            }

            else if (operation == 2)
            {
                ReadTasks();
            }

            bool choice = ConsoleUserInput.ReadYesNo("Do you want to continue?");

            if (choice == true)
            {
                ReadOrWrite();
            }
            else
            {
                Console.WriteLine("Good bye!");
                return;
            }
        }

        static void ReadTasks()
        {
            XMLTaskReader reader = new XMLTaskReader();
            string path = "../../tasks.xml";

            if (!File.Exists(path))
            {
                Console.WriteLine("There is no saved task's!");
                return;
            }

            else
            {
                List<Task> tasks = reader.ReadTasks(path);

                int count = 1;
                foreach (var item in tasks)
                {
                    Console.WriteLine("Task {0}:\n{1}\nDescription: {2}\nstarted at: {3}\nterm to: {4}", count, item.Title, item.Message, item.StartDate, item.EndDate);
                    count++;
                    Console.WriteLine();
                }
            }
        }

        static void ManipulateTask()
        {
            int operation = ConsoleUserInput.ReadOption("Choose an option!", new string[] { "make a new task", "delete a task" });

            if (operation == 1)
            {
                TaskMaker();
                bool choice = ConsoleUserInput.ReadYesNo("Do yoy want to make another task?");
                if (choice == true)
                {
                    TaskMaker();
                }

                else if (choice == false)
                {
                    Console.Write("Press any key to exit:");
                    return;
                }
            }

            else if (operation == 2)
            {
                XMLTaskReader reader = new XMLTaskReader();
                List<Task> tasks = reader.ReadTasks("../../tasks.xml");
                Console.Write("Select the number of the task, you want to delete: ");
                int n = int.Parse(ConsoleUserInput.ReadText(true));
                XMLTaskWriter writer = new XMLTaskWriter();
                writer.Delete(tasks[n - 1]);
                Console.WriteLine("Delete completed!");
            }

        }

        static void TaskMaker()
        {
            Console.Write("Enter a title: ");
            string title = ConsoleUserInput.ReadText(true);
            Console.Write("Enter a description: ");
            string message = ConsoleUserInput.ReadText(true);
            Console.Write("How many days you need to finish the task: ");
            int deadLine = int.Parse(ConsoleUserInput.ReadText(true));

            var task = new Task(title, message, deadLine);

            bool choice = ConsoleUserInput.ReadYesNo("Do you want to save this task?");

            if (choice == true)
            {
                XMLTaskWriter writer = new XMLTaskWriter();
                writer.Save(task);
                Console.WriteLine("Save completed!");
                return;
            }

            else if (choice == false)
            {
                Console.WriteLine("Good luck!");
                return;
            }
        }
    }
}
