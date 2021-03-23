using System.Linq;
using TodoApp.Repositories.Models;
using TodoApp.Services;
using P = TodoApp.ConsoleApp.Framework.Examples.Props;

namespace TodoApp.ConsoleApp.Framework.Examples.ViewModels
{
    public class Todo: ViewModel
    {
        private readonly ITodoService TodoService;

        public readonly Navigation Nav;

        public int Id = 1;
        public string Name = "My todo";
        public int TotalCount => TodoService.GetAll().Count();

        public Todo(Navigation nav, P.Todo props, ITodoService todoService)
        {
            Nav = nav;
            Id = props?.Id ?? 0;
            TodoService = todoService;
        }

        public void Update(string name)
        {
            Name = name;

            NotifyPropertyChanged();
        }

        public void Create()
        {
            TodoService.Create(new TodoModel()
            {
                Title = Name,
                Status = TodoStatus.Open
            });

            NotifyPropertyChanged();
        }
    }
}
