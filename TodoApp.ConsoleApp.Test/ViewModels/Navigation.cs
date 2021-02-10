using TodoApp.ConsoleApp.Framework;
using TodoApp.ConsoleApp.Framework.Services;
using V = TodoApp.ConsoleApp.Test.Views;
using P = TodoApp.ConsoleApp.Test.Props;

namespace TodoApp.ConsoleApp.Test.ViewModels
{
    public class Navigation: ViewModel
    {
        Router Router;

        public Navigation(Router router)
        {
            Router = router;
        }

        public void OpenHome()
        {
            Router.Open<V.Home>();
        }
        public void OpenTodoDetails(int id)
        {
            Router.Open<V.TodoDetails, P.Todo>(new P.Todo(id));
        }

        public void Goodbye()
        {
            Router.Open<V.Goodbye>();
        }

        public bool CanGoBack()
        {
            return Router.CanGoTo(-1);
        }

        public void GoBack()
        {
            Router.GoTo(-1);
        }
    }
}
