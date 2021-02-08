using System;
using System.Text.RegularExpressions;
using TodoApp.ConsoleApp.Framework;
using TodoApp.ConsoleApp.Framework.Commands;
using VM = TodoApp.ConsoleApp.Test.ViewModels;

namespace TodoApp.ConsoleApp.Test.Views
{
    public class Home : View<VM.Navigation>
    {
        public Home(VM.Navigation vm)
            : base(vm)
        { }

        public override void Render()
        {
            Console.WriteLine("Hello!");


        }

        public override void SetupCommands()
        {
            Commands.Message = "Where to go?";

            Commands.Add(new Command(
               "Open First Todo",
               "t",
               (_) => DataSource.OpenTodoDetails(1)
           ));

            Commands.Add(new Command(
                "Open Todo by ID",
                "id [id]",
                new Regex(@"^id \d+$"),
                (input) =>
                {
                    var id = int.Parse(input.Split()[1]);
                    DataSource.OpenTodoDetails(id);
                }
            ));
        }
    }
}
