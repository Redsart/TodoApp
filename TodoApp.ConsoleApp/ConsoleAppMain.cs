using System;
using TodoApp.Library;

namespace TodoApp.ConsoleApp
{
    class ConsoleAppMain
    {
        static void Main(string[] args)
        {
            string title = "Do this thing";
            string message = "do something to finish the task";

            var task = new Task(title, message, 5);
            Console.WriteLine(task.StartDate);
            Console.WriteLine(task.EndDate);
            Console.WriteLine(task);
        }
    }
}
