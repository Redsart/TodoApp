using System;
using TODO_Library;

namespace ToDoConsoleApp
{
    class Program
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
