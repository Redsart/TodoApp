using System;
using TodoApp.Library.UI;


namespace TodoApp.ConsoleApp
{
    class ConsoleAppMain
    {
        static void Main(string[] args)
        {
            Console.WriteLine(string.Compare("Yes", "yes", StringComparison.OrdinalIgnoreCase));
            Console.WriteLine("Hello, this program is making a task's and save them to xml document. Enjoy :)");
            Console.WriteLine();
            
            TaskOperations.ReadOrWrite();
        }
    }
}
