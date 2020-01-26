using System;
using TodoApp.ConsoleApp.UI;


namespace TodoApp.ConsoleApp
{
    class Program
    {
        static void Main()
        {
            Console.WriteLine(Messages.WelcomeMessage());
            Console.WriteLine();
            
            TaskOperations.ReadOrWrite();
        }
    }
}
