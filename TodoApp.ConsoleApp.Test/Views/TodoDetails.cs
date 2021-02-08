using System;
using System.Text.RegularExpressions;
using TodoApp.ConsoleApp.Framework;
using TodoApp.ConsoleApp.Framework.Commands;
using VM = TodoApp.ConsoleApp.Test.ViewModels;

namespace TodoApp.ConsoleApp.Test.Views
{
    public class TodoDetails : View<VM.Todo>
    {
        public TodoDetails(VM.Todo vm)
            : base(vm) { }


        public override void Render()
        {
            Console.WriteLine("Todo");
            Console.WriteLine("ID: {0}", DataSource.Id);
            Console.WriteLine("Name: {0}", DataSource.Name);
        }

        public override void SetupCommands()
        {
            Commands.Add(new Command(
                "Update name",
                "name [new name]",
                new Regex(@"^name .+$"),
                (input) =>
                {
                    string name = input.Substring("name ".Length);
                    DataSource.Update(name);
                }
            ));

            Commands.Add(new Command(
                "Go home",
                "h",
                (input) => DataSource.OpenHome()
            ));
        }
    }
}
