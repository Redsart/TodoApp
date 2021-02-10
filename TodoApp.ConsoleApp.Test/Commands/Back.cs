using TodoApp.ConsoleApp.Framework.Commands;
using VM = TodoApp.ConsoleApp.Test.ViewModels;

namespace TodoApp.ConsoleApp.Test.Commands
{
    public class Back : Command<VM.Navigation>
    {
        public Back()
            : base("Go Back.", "b")
        { }

        protected override bool CanExecute()
        {
            return DataSource.CanGoBack();
        }

        protected override void Execute(string input)
        {
            DataSource.GoBack();
        }
    }
}
