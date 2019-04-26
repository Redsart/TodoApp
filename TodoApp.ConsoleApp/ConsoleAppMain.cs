using System;
using TodoApp.Library.Models;
using TodoApp.Library.Data;
using System.Globalization;
using System.Xml;
using System.Collections;



namespace TodoApp.ConsoleApp
{
    class ConsoleAppMain
    {
        static void Main(string[] args)

        {
            //Console.Write("Enter a title: ");
            //string title = Console.ReadLine();
            //Console.Write("Enter a description: ");
            //string message = Console.ReadLine();
            //int deadLine = int.Parse(Console.ReadLine());

            //var task = new Task(title, message, deadLine);
            //Console.WriteLine("Do you want to save this task? Yes/No");

            //string choice = Console.ReadLine();

            //if (choice == "Yes")
            //{
            //    XMLTaskWriter writer = new XMLTaskWriter();
            //    writer.Save(task);
            //    Console.WriteLine("Save completed!");
            //}
            //if (choice == "No")
            //{
            //    Console.WriteLine("Good luck!");
            //}


            XMLTaskReader reader = new XMLTaskReader();

            System.Collections.Generic.List<Task> tasks = reader.ReadTasks("../../tasks.xml");

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
}
