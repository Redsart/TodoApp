using System;
using TodoApp.ConsoleApp.Framework;
using VM = TodoApp.ConsoleApp.ViewModels;
using Cmd = TodoApp.ConsoleApp.Commands;
using TodoApp.ConsoleApp.Components;
using TodoApp.Services;
using TodoApp.Repositories.Models;

namespace TodoApp.ConsoleApp.Views
{
    class Edit : View<VM.Todo>
    {
        private ITodoService TodoService;
        public Edit(VM.Todo vm) 
            : base(vm) 
        { }

        public override void Render()
        {
            Output.WriteTitle("Create or Edit youre Todo:");
            var todo = new TodoModel();
            TodoService = DataSource.TodoService;
            Output.WriteLabel("Enter name: ");
            string name = Input.ReadText();
            Output.WriteLabel("Enter description: ");
            string description=Input.ReadText();
            todo.Title = name;
            todo.Description = description;
            todo.Status = TodoStatus.Open;
            todo.DueDate = DateTime.Now;
            TodoService.Create(todo);
        }

        public override void SetupCommands()
        {
            Commands.Message = "Where to go?";
            Commands.InvalidMessage = "Ooops... try again!";

            Commands.Add<Cmd.Back, VM.Navigation>(DataSource.Nav);
            Commands.Add<Cmd.Exit, VM.Navigation>(DataSource.Nav);
        }
    }
}
