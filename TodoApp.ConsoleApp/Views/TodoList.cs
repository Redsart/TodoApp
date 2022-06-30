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

        public TodoList(VM.Todo vm)
            : base(vm)  { }

        public override void Render()
        {
            Output.WriteTitle("List of todos");
            var todos = DataSource.TodoService.GetAll();
            var grid = FillGrid();

            for (int i = 0; i < grid.GetLength(0); i++)
            {
                for (int j = 0; j < grid.GetLength(1); j++)
                {
                    Console.Write(grid[i, j]);
                }
                Console.WriteLine();
            }
        }

        public override void SetupCommands()
        {
            Commands.Message = "Where to go?";
            Commands.InvalidMessage = "Ooops... try again!";

            Commands.Add<Cmd.Back, VM.Navigation>(DataSource.Nav);
            Commands.Add<Cmd.Exit, VM.Navigation>(DataSource.Nav);
        }

        private string[,] FillGrid()
        {
            var todos = DataSource.TodoService.GetAll();
            string[] columnsNames = new string[] { "Id", "Name", "Status" };
            int cols = columnsNames.Length;
            int rows = todos.Count()+1;
            int IdLength = DataSource.TotalCount.ToString().Length;

            var longestName = todos.OrderByDescending(x => x.Title).Last();
            int nameLength = longestName.Title.Length;

            var longestStatus= todos.OrderByDescending(x => x.Status).First();
            var statusLength=longestStatus.Status.ToString().Length;

            string[,] grid = new string[rows, cols];
            //grid[0, 0] = FormatColumn(columnsNames[0], IdLength);
            //grid[1,0] = FormatColumn(columnsNames[1], nameLength);
            //grid[2,0] = FormatColumn(columnsNames[2], statusLength);
            int columnLength = 0;
            for (int i = 0; i < rows-1; i++)
            {
                string text = "";
                for (int j = 0; j < cols; j++)
                {
                    if (j == 0)
                    {
                        columnLength = IdLength;
                        text = (i+1).ToString();
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

        
        private string FormatColumn(string text, int number)
        {
            string result = string.Format("|{0,-" + (number+2) + "}",text);

            return result;
        }
    }
}
