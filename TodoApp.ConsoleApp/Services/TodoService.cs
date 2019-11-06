using System.Collections.Generic;
using TodoApp.Library.Data;
using TodoApp.Library.Models;

namespace TodoApp.ConsoleApp.Services
{
    static class TodoService
    {
        public static List<Task> GetAll(string path)
        {
            XMLTaskReader reader = new XMLTaskReader();

            List<Task> tasks = reader.ReadTasks(path);

            return tasks;
        }

        static void GetByID(int id)
        {

        }

        static void Update()
        {

        }



        static void Delete(int id)
        {

        }
    }
}
