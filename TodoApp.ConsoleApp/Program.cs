using System;
using TodoApp.ConsoleApp.UI;
using TodoApp.Repositories.XmlRepository.Utils;
using TodoApp.Repositories.XmlRepository;
using TodoApp.Repositories.Interfaces;
using TodoApp.Services;

namespace TodoApp.ConsoleApp
{
    class Program
    {
        static void Main()
        {
            Console.WriteLine(Messages.WelcomeMessage());
            Console.WriteLine();

            string path = "../../data/todos.xml";
            IXmlContext context = new XmlContext(path);
            ITodoRepository repo = new TodoRepository(context);
            ITodoService service = new TodoService(repo);
            var ui = new TaskOperations(service);

            ui.ReadOrWrite();
        }
    }
}
