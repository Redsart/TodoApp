using System;
using TodoApp.ConsoleApp.UI;


namespace TodoApp.ConsoleApp
{
    class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine("Hello, this program is making a task's and save them to xml document. Enjoy :)");
            Console.WriteLine();
            
            TaskOperations.ReadOrWrite();
        }
    }
}
