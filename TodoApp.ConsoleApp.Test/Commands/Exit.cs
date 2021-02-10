using TodoApp.ConsoleApp.Framework.Commands;
using VM = TodoApp.ConsoleApp.Test.ViewModels;

namespace TodoApp.ConsoleApp.Test.Commands
{
    public class Exit : Command<VM.Navigation>
    {
        public Exit()
            : base("Exit the app.", "e")
        { }

        protected override void Execute(string input)
        {
            DataSource.Goodbye();
        }
    }
}
