using System;
using TodoApp.ConsoleApp.Framework;
using VM = TodoApp.ConsoleApp.Test.ViewModels;

namespace TodoApp.ConsoleApp.Test.Views
{
    public class TodoDetails : View<VM.Todo>
    {
        public TodoDetails(VM.Todo vm)
            : base(vm) { }


        public override void Draw()
        {
            Console.WriteLine("Todo");
            Console.WriteLine(DataSource.Id);
            Console.WriteLine(DataSource.Name);
            var name = Console.ReadLine();
            DataSource.Update(name);
        }
    }
}
