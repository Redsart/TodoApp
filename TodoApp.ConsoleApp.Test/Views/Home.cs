using System.Text.RegularExpressions;
using TodoApp.ConsoleApp.Framework;
using VM = TodoApp.ConsoleApp.Test.ViewModels;
using Cmd = TodoApp.ConsoleApp.Test.Commands;
using TodoApp.ConsoleApp.Test.Components;

namespace TodoApp.ConsoleApp.Test.Views
{
    public class Home : View<VM.Navigation>
    {
        public Home(VM.Navigation vm)
            : base(vm)
        { }

        public override void Render()
        {
            Output.WriteTitle("Todo app");

            Output.WriteParagraph("Welcome to your personal task manager. You can use it to create Todo's and track their progress.");
        }

        public override void SetupCommands()
        {
            Commands.Message = "Where to go?";

            Commands.Add(
               "Open First Todo.",
               "f",
               (_) => DataSource.OpenTodoDetails(1)
           );

            Commands.Add(
                "Open Todo by ID.",
                "t [id]",
                new Regex(@"^t \d+$"),
                (input) =>
                {
                    var parts = input.Split();
                    var id = int.Parse(parts[1]);
                    DataSource.OpenTodoDetails(id);
                }
            );

            //Commands.Add(
            //    "Exit the app.",
            //    "e",
            //    (_) => DataSource.Goodbye()
            //);

            Commands.Add<Cmd.Back, VM.Navigation>(DataSource);
            Commands.Add<Cmd.Exit, VM.Navigation>(DataSource);
        }
    }
}
