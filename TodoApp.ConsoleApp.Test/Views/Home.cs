using System;
using System.Collections.Generic;
using System.Text;
using TodoApp.ConsoleApp.Framework;

namespace TodoApp.ConsoleApp.Test.Views
{
    public class Home : View
    {
        public Home(Renderer renderer, Router router)
            : base(renderer, router) { }

        public override void Draw()
        {
            Console.WriteLine("Hello!");
            Console.ReadLine();
            Router.Open<Todo>();
        }
    }
}
