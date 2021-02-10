
using TodoApp.ConsoleApp.Framework;
using VM = TodoApp.ConsoleApp.Test.ViewModels;
using TodoApp.ConsoleApp.Test.Components;

namespace TodoApp.ConsoleApp.Test.Views
{
    public class Goodbye : View<VM.Goodbye>
    {
        public Goodbye(VM.Goodbye vm)
            : base(vm)
        { }

        public override void Render()
        {
            Output.WriteTitle("Todo app");
         
            Output.WriteParagraph("See you soon!");
            Input.ReadChar();
            DataSource.Exit();
        }

        public override void SetupCommands()
        { }
    }
}
