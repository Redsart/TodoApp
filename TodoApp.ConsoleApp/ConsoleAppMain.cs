using System;
using TodoApp.Library.Models;
using TodoApp.Library.Data;
using System.Collections.Generic;
using System.IO;
using System.Linq;


namespace TodoApp.ConsoleApp
{
    public class ConsoleAppMain
    {
        private static string XmlPath { get; set; }

        public static void Main(string[] args)
        {
            Console.WriteLine("Hello, this program is making a task(s) and save them to xml document. Enjoy :)");
            Console.WriteLine();

            if (args.Any(s => s.StartsWith("-path")))
            {
                XmlPath = args.FirstOrDefault(s => s.StartsWith("-path")).Substring(6);
                Console.WriteLine($"XML Path = {XmlPath}");
                Console.WriteLine();
            }

            Console.WriteLine("Youre current task(s) are: ");
            ReadTasks();
            Console.Write("If you want to manipulate a task type 1, if you want to read you're task(s) again type 2: ");
            int operation = 0;
            while (int.TryParse(Console.ReadLine(), out operation))
            {
                if (operation != 1 && operation != 2)
                {
                    Console.WriteLine("Incorect choice! Please try again!");
                }
                else if (operation == 1)
                {
                    ManipulateTask();
                }
                else if (operation == 2)
                {
                    ReadTasks();
                    Console.Write("When you are done press any key to exit!");
                    return;
                }
            }
        }

        static void ReadTasks()
        {
            XMLTaskReader reader = new XMLTaskReader();
            string path = !string.IsNullOrEmpty(XmlPath) ? XmlPath : "../../tasks.xml";
            if (!File.Exists(path))
            {
                Console.WriteLine("There is no saved task(s)!");
                return;
            }
            else
            {
                List<Task> tasks = reader.ReadTasks(path);

                int count = 1;
                foreach (var item in tasks)
                {
                    Console.WriteLine("Task {0}:\n{1}\nDescription: {2}\nstarted at: {3}\nterm to: {4}",
                        count, item.Title, item.Message, item.StartDate, item.EndDate);
                    count++;
                    Console.WriteLine();
                }
            }
        }

        static void ManipulateTask()
        {
            Console.Write("Press 1 to make a new task, or press 2 to delete a task: ");
            string readOrDel = string.Empty;
            while (true)
            {
                readOrDel = Console.ReadLine();
                if (readOrDel != "1" && readOrDel != "2")
                {
                    Console.WriteLine("Incorect choice! Please try again!");
                }
                else if (readOrDel == "1")
                {
                    TaskMaker();
                    string choice = string.Empty;
                    while (true)
                    {
                        Console.Write("Do you want to make another task? yes/no");
                        choice = Console.ReadLine();
                        if (choice != "yes" && choice != "no")
                        {
                            Console.WriteLine("Incorect choice! Please try again!");
                        }
                        else if (choice == "yes")
                        {
                            TaskMaker();
                        }
                        else if (choice == "no")
                        {
                            Console.WriteLine("Good luck!");
                            Console.Write("Press any key to exit:");
                            return;
                        }
                    }
                }
                else if (readOrDel == "2")
                {
                    XMLTaskReader reader = new XMLTaskReader();
                    List<Task> tasks = reader.ReadTasks(!string.IsNullOrEmpty(XmlPath) ? XmlPath : "../../tasks.xml");
                    Console.Write("Select the number of the task, you want to delete: ");
                    int n = int.Parse(Console.ReadLine());
                    XMLTaskWriter writer = new XMLTaskWriter(XmlPath);
                    writer.Delete(tasks[n - 1]);
                    Console.WriteLine("Delete completed!");
                    break;
                }
            }

        }

        static void TaskMaker()
        {
            Console.Write("Enter a title: ");
            string title = Console.ReadLine();
            Console.Write("Enter a description: ");
            string message = Console.ReadLine();
            Console.Write("How many days you need to finish the task: ");
            int deadLine = int.Parse(Console.ReadLine());

            var task = new Task(title, message, deadLine);
            Console.WriteLine("Do you want to save this task? yes/no");

            string choice; 

            while (true)
            {
                choice = Console.ReadLine();
                if (choice != "yes" && choice != "no")
                {
                    Console.WriteLine("Incorect choice! Please try again!");
                }
                else if (choice == "yes")
                {
                    XMLTaskWriter writer = new XMLTaskWriter(XmlPath);
                    writer.Save(task);
                    Console.WriteLine("Save completed!");
                    return;
                }
                else if (choice == "no")
                {
                    Console.WriteLine("Good luck!");
                    return;
                }
            }
        }
    }
}
