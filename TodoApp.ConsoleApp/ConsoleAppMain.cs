using System;
using TodoApp.Library.Models;
using TodoApp.Library.Data;
using System.Globalization;
using System.Collections;
using System.Collections.Generic;



namespace TodoApp.ConsoleApp
{
    class ConsoleAppMain
    {
        static void Main(string[] args)
        {
            Console.WriteLine("Hello, this program is making a task's and save them to xml document. Enjoy :)");

            Console.Write("If you want to manipulate a task type 1, if you want to read you're task's type 2: ");
            int operation = 0;
            while (int.TryParse(Console.ReadLine(), out operation) || operation != 1 || operation != 2)
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
                    XMLTaskReader reader = new XMLTaskReader();

                    List<Task> tasks = reader.ReadTasks("../../tasks.xml");

                    int count = 1;
                    foreach (var item in tasks)
                    {
                        Console.WriteLine("Task {0}:\n{1}\nDescription: {2}\nstarted at: {3}\nterm to: {4}",
                            count, item.Title, item.Message, item.StartDate, item.EndDate);
                        count++;
                        Console.WriteLine();
                    }
                    Console.Write("When you are done press any key to exit!");
                    return;
                }
            }
        }

        static void ManipulateTask()
        {
            TaskMaker();
            string choice = string.Empty;
            while (true)
            {
                Console.Write("Do you want to another task? yes/no");
                choice = Console.ReadLine();
                if (choice != "yes" || choice != "no")
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
                    Console.WriteLine(Console.ReadKey());
                    return;
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

            string choice = Console.ReadLine();

            while (true)
            {
                if (choice != "yes" || choice != "no")
                {
                    Console.WriteLine("Incorect choice! Please try again!");
                }
                else if (choice == "yes")
                {
                    XMLTaskWriter writer = new XMLTaskWriter();
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
