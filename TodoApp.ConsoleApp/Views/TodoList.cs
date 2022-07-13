using System;
using System.Collections.Generic;
using System.Linq;
using TodoApp.ConsoleApp.Framework;
using VM = TodoApp.ConsoleApp.ViewModels;
using Cmd = TodoApp.ConsoleApp.Commands;
using TodoApp.ConsoleApp.Components;
using TodoApp.Repositories.Models;

namespace TodoApp.ConsoleApp.Views
{
    class TodoList : View<VM.Todo>
    {
        int pageIndex = 1;
        int pageSize = 5;
        
        public TodoList(VM.Todo vm)
            : base(vm)  { }

        public override void Render()
        {
            Output.WriteTitle("List of todos");
            var todos = DataSource.TodoService.GetAll();
            int pageTodos = 0;
            bool canStop = false;
            int possition = 0;

            while (canStop == false)
            {
                if (DataSource.TotalCount - (pageIndex * pageSize) <= 0)
                {
                    pageTodos = DataSource.TotalCount;
                    canStop = true;
                }
                else
                {
                    pageTodos = pageSize*pageIndex;
                }

                List<TodoModel> displayedTodos = new List<TodoModel>();

                for (int i = possition; i < pageTodos; i++)
                {
                    displayedTodos.Add(todos.ElementAt(i));
                }
                PrintConsoleGrid(displayedTodos, possition+1);
                possition += pageSize * pageIndex;
                pageIndex++;

                if (DataSource.TotalCount > possition)
                {
                    string message = Input.ReadText("Want to continue: y/n");
                    if (message == "y")
                    {
                        Console.Clear();
                    }
                    else
                    {
                        canStop = true;
                    }
                }
                else
                {
                    canStop = true;
                }
                
            }
        }

        public override void SetupCommands()
        {
            Commands.Message = "Where to go?";
            Commands.InvalidMessage = "Ooops... try again!";

            Commands.Add<Cmd.Back, VM.Navigation>(DataSource.Nav);
            Commands.Add<Cmd.Exit, VM.Navigation>(DataSource.Nav);
        }

        private string[,] FillGrid(IEnumerable<TodoModel> todos, int possition)
        {
            string[] columnsNames = new string[] { "Id", "Name", "Status" };
            int cols = columnsNames.Length;
            int rows = todos.Count()+1;
            int IdLength = DataSource.TotalCount.ToString().Length;

            var longestName = todos.OrderBy(x => x.Title.Length).Last();
            int nameLength = longestName.Title.Length;

            var longestStatus= todos.OrderBy(x => x.Status.ToString().Length).Last();
            var statusLength=longestStatus.Status.ToString().Length;

            string[,] grid = new string[rows, cols];
            int columnLength = 0;
            for (int i = 0; i < rows-1; i++)
            {
                string text = "";
                for (int j = 0; j < cols; j++)
                {
                    if (j == 0)
                    {
                        columnLength = IdLength;
                        text = (possition++).ToString();
                    }
                    else if (j == 1)
                    {
                        columnLength = nameLength;
                        text = todos.Select(x => x.Title).ElementAtOrDefault(i);
                    }
                    else if (j == 2)
                    {
                        columnLength = statusLength;
                        text = todos.Select(x => x.Status).ElementAtOrDefault(i).ToString();
                    }

                    grid[0, j] = FormatColumn(columnsNames[j], columnLength);
                    grid[i + 1, j] = FormatColumn(text, columnLength);
                }
            }

            return grid;
        }

        private void PrintConsoleGrid(IEnumerable<TodoModel> todos, int possition)
        {
            var grid = FillGrid(todos, possition);
            for (int i = 0; i < grid.GetLength(0); i++)
            {
                for (int j = 0; j < grid.GetLength(1); j++)
                {
                    Console.Write(grid[i, j]);
                }
                Console.WriteLine();
            }
        }

        
        private string FormatColumn(string text, int number)
        {
            string result = string.Format("| {0,-" + (number+2) + "}",text);

            return result;
        }
    }
}
