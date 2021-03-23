
using TodoApp.ConsoleApp.Framework;
using VM = TodoApp.ConsoleApp.Framework.Examples.ViewModels;
using TodoApp.ConsoleApp.Framework.Examples.Components;

namespace TodoApp.ConsoleApp.Framework.Examples.Views
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
