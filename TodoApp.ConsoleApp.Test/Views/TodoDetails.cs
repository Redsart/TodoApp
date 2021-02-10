using System.Text.RegularExpressions;
using TodoApp.ConsoleApp.Framework;
using VM = TodoApp.ConsoleApp.Test.ViewModels;
using Cmd = TodoApp.ConsoleApp.Test.Commands;
using TodoApp.ConsoleApp.Test.Components;

namespace TodoApp.ConsoleApp.Test.Views
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
        }

        public override void SetupCommands()
        {
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
                "Go home",
                "h",
                (input) => DataSource.OpenHome()
            );

            Commands.Add<Cmd.Back, VM.Navigation>(DataSource);
            Commands.Add<Cmd.Exit, VM.Navigation>(DataSource);
        }
    }
}
