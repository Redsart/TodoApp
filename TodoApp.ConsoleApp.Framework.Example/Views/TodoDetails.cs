using System.Text.RegularExpressions;
using TodoApp.ConsoleApp.Framework;
using VM = TodoApp.ConsoleApp.Framework.Examples.ViewModels;
using Cmd = TodoApp.ConsoleApp.Framework.Examples.Commands;
using TodoApp.ConsoleApp.Framework.Examples.Components;

namespace TodoApp.ConsoleApp.Framework.Examples.Views
{
    public class TodoDetails : View<VM.Todo>
    {
        public TodoDetails(VM.Todo vm)
            : base(vm) { }


        public override void Render()
        {
            Output.WriteTitle("Todo Details");

            Output.WriteField("ID", DataSource.Id);
            Output.WriteField("Name", DataSource.Name);
            Output.WriteField("Total count", DataSource.TotalCount);
        }

        public override void SetupCommands()
        {
            Commands.Message = "Where to go?";
            Commands.InvalidMessage = "Ooops... try again!";

            Commands.Add(
                "Update name",
                "name [new name]",
                new Regex(@"^name .+$"),
                (input) =>
                {
                    string name = input.Substring("name ".Length);
                    DataSource.Update(name);
                }
            );

            Commands.Add(
                "Create",
                "c",
                (_) => DataSource.Create()
            );

            Commands.Add(
                "Go home",
                "h",
                (input) => DataSource.Nav.OpenHome()
            );

            Commands.Add<Cmd.Back, VM.Navigation>(DataSource.Nav);
            Commands.Add<Cmd.Exit, VM.Navigation>(DataSource.Nav);
        }
    }
}
