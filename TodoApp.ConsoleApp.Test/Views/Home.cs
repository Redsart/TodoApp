using System;
using TodoApp.ConsoleApp.Framework;
using VM = TodoApp.ConsoleApp.Test.ViewModels;

namespace TodoApp.ConsoleApp.Test.Views
{
    public class Home : View<VM.Navigation>
    {
        public Home(VM.Navigation vm)
            : base(vm)
        { }

        public override void Draw()
        {
            Console.WriteLine("Hello!");

            int id;
            while (!int.TryParse(Console.ReadLine(), out id))
            { }

            DataSource.OpenTodoDetails(id);
        }
    }
}
