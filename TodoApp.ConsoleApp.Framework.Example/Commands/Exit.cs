using VM = TodoApp.ConsoleApp.Framework.Examples.ViewModels;

namespace TodoApp.ConsoleApp.Framework.Examples.Commands
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
