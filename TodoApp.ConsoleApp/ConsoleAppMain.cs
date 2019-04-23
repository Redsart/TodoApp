using System;
using TodoApp.Library.Models;
using TodoApp.Library.Data;


namespace TodoApp.ConsoleApp
{
    class ConsoleAppMain
    {
        static void Main(string[] args)
        {
            Console.Write("Enter a title: ");
            string title = Console.ReadLine();
            Console.Write("Enter a description: ");
            string message = Console.ReadLine();
            int deadLine = int.Parse(Console.ReadLine());

            var task = new Task(title, message, deadLine);
            Console.WriteLine("Do you want to save this task? Yes/No");

            string choice = Console.ReadLine();

            if (choice == "Yes")
            {
                XMLTaskWriter writer = new XMLTaskWriter();
                writer.Save(task);
                Console.WriteLine("Save completed!");
            }
            if (choice == "No")
            {
                Console.WriteLine("Good luck!");
            }
        }
    }
}
