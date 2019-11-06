using System;
using System.Collections.Generic;
using TodoApp.Library.Models;
using TodoApp.Library.Data;
using System.IO;
using TodoApp.ConsoleApp.Services;


namespace TodoApp.ConsoleApp.UI
{
    public static class TaskOperations
    {
        static string path = "../../tasks.xml";

        public static void ReadOrWrite()
        {
            bool toContinue = true;

            while (toContinue)
            {
                int operation = UserInput.ReadOption("Choose an option!", new string[] { "manipulate task", "read task's" }, true);

                switch (operation)
                {
                    case 1:
                        ManipulateTask();
                        break;
                    case 2:
                        ReadTasks();
                        break;
                    default:
                        UserInput.ReadText("Error! Invalid comand!");
                        break;
                }

                toContinue = UserInput.ReadYesNo("Do you want to continue?",true);
            }


            UserInput.ReadText("Good bye!");
            return;
        }

        static void ReadTasks()
        {
            //XMLTaskReader reader = new XMLTaskReader();
            //string path = "../../tasks.xml";

            if (!File.Exists(path))
            {
                UserInput.ReadText("There is no saved task's!");
                return;
            }

            else
            {
                //List<Task> tasks = reader.ReadTasks(path);
                List<Task> tasks = TodoService.GetAll(path);
                //int count = 1;
                foreach (var task in tasks)
                {
                    UserInput.ReadText($"Task {task.ID}:\n{task.Title}\nDescription: {task.Message}\nstarted at: {task.StartDate}\nterm to: {task.EndDate}");
                    //count++;
                    Console.WriteLine();
                }
            }
        }

        static void ManipulateTask()
        {
            int operation = UserInput.ReadOption("Choose an option!", new string[] { "make a new task", "delete a task" }, true);

            switch (operation)
            {
                case 1:
                    TaskMaker();
                    bool choice = UserInput.ReadYesNo("Do yoy want to make another task?", true);
                    if (choice)
                    {
                        TaskMaker();
                    };
                    break;

                case 2:
                    //XMLTaskReader reader = new XMLTaskReader();
                    List<Task> tasks = TodoService.GetAll(path);
                    int n = int.Parse(UserInput.ReadText("Select the number of the task, you want to delete: ", true));
                    XMLTaskWriter writer = new XMLTaskWriter();
                    writer.Delete(tasks[n - 1]);
                    UserInput.ReadText("Delete completed!");
                    break;
                default:
                    UserInput.ReadText("Error! Invalid comand!");
                    break;
            }
        }

        static void TaskMaker()
        {
            string title = UserInput.ReadText("Enter a title: ", true);
            string message = UserInput.ReadText("Enter a description: ", true);
            int deadLine = int.Parse(UserInput.ReadText("How many days you need to finish the task: ",true));

            var task = new Task(title, message, deadLine);

            bool isSave = UserInput.ReadYesNo("Do you want to save this task?");

            if (isSave)
            {
                //XMLTaskWriter writer = new XMLTaskWriter();
                //writer.Save(task);
                TodoService.Save(task);
                UserInput.ReadText("Save completed!");
                return;
            }

            else
            {
                UserInput.ReadText("Good luck!");
                return;
            }
        }
    }
}
