using System;
using System.Collections.Generic;
using System.Text;
using TodoApp.ConsoleApp.Framework;
using VM = TodoApp.ConsoleApp.Test.ViewModels;

namespace TodoApp.ConsoleApp.Test.Views
{
    public class Todo : View<VM.Todo>
    {
        public Todo(Renderer renderer, Router router, VM.Todo vm)
            : base(renderer, router, vm) { }

        public override void Draw()
        {
            Console.WriteLine("Todo");
            Console.WriteLine(ViewModel.Name);
            var name = Console.ReadLine();
            ViewModel.Update(name);
        }
    }
}
