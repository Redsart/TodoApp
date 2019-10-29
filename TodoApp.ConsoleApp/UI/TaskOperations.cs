using System;
<<<<<<< HEAD:TodoApp.ConsoleApp/UI/TaskOperations.cs
using System.Collections.Generic;
using TodoApp.Library.Models;
using TodoApp.Library.Data;
using System.IO;
=======
using TodoApp.Library.UI;

>>>>>>> 5d708e9... Add Validate class, move user input into a separate class.:TodoApp.ConsoleApp/ConsoleAppMain.cs

namespace TodoApp.ConsoleApp.UI
{
    public static class TaskOperations
    {
        public static void ReadOrWrite()
        {
<<<<<<< HEAD:TodoApp.ConsoleApp/UI/TaskOperations.cs
            bool choice = true;

            while (choice == true)
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
                        break;
                }

                choice = UserInput.ReadYesNo("Do you want to continue?",true);
            }


            UserInput.ReadText("Good bye!", false, "");
            return;
        }

        static void ReadTasks()
        {
            XMLTaskReader reader = new XMLTaskReader();
            string path = "../../tasks.xml";

            if (!File.Exists(path))
            {
                UserInput.ReadText("There is no saved task's!", false, "");
                return;
            }

            else
            {
                List<Task> tasks = reader.ReadTasks(path);

                int count = 1;
                foreach (var item in tasks)
                {
                    UserInput.ReadText($"Task {count}:\n{item.Title}\nDescription: {item.Message}\nstarted at: {item.StartDate}\nterm to: {item.EndDate}", false, "");
                    count++;
                    Console.WriteLine();
                }
            }
        }

        static void ManipulateTask()
        {
            int operation = UserInput.ReadOption("Choose an option!", new string[] { "make a new task", "delete a task" }, true);

            if (operation == 1)
            {
                TaskMaker();
                bool choice = UserInput.ReadYesNo("Do yoy want to make another task?", true);
                if (choice == true)
                {
                    TaskMaker();
                }
            }

            else if (operation == 2)
            {
                XMLTaskReader reader = new XMLTaskReader();
                List<Task> tasks = reader.ReadTasks("../../tasks.xml");
                int n = int.Parse(UserInput.ReadText("Select the number of the task, you want to delete: ",true));
                XMLTaskWriter writer = new XMLTaskWriter();
                writer.Delete(tasks[n - 1]);
                UserInput.ReadText("Delete completed!", false, "");
            }
        }

        static void TaskMaker()
        {
            string title = UserInput.ReadText("Enter a title: ", true);
            string message = UserInput.ReadText("Enter a description: ", true);
            int deadLine = int.Parse(UserInput.ReadText("How many days you need to finish the task: ",true));

            var task = new Task(title, message, deadLine);

            bool choice = UserInput.ReadYesNo("Do you want to save this task?");

            if (choice == true)
            {
                XMLTaskWriter writer = new XMLTaskWriter();
                writer.Save(task);
                UserInput.ReadText("Save completed!", false, "");
                return;
            }

            else if (choice == false)
            {
                UserInput.ReadText("Good luck!", false, "");
                return;
            }
=======
            Console.WriteLine(string.Compare("Yes", "yes", StringComparison.OrdinalIgnoreCase));
            Console.WriteLine("Hello, this program is making a task's and save them to xml document. Enjoy :)");
            Console.WriteLine();
            
            TaskOperations.ReadOrWrite();
>>>>>>> 5d708e9... Add Validate class, move user input into a separate class.:TodoApp.ConsoleApp/ConsoleAppMain.cs
        }
    }
}
