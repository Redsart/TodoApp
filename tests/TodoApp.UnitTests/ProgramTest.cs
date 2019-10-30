using System;
using System.IO;
using System.Text;
using Xunit;

namespace TodoApp.UnitTests
{
    public class ProgramTest
    {
        private const string testTaskName = "TestTask";

        [Fact]
        public void Test_initial_datafile()
        {
            using (var sw = new StringWriter())
            {
                using (var sr = new StringReader(string.Empty))
                {
                    Console.SetOut(sw);
                    Console.SetIn(sr);

                    TodoApp.ConsoleApp.ConsoleAppMain.Main(new string[] { "-path ../../../tasks.xml" });
                    var result = sw.ToString();

                    Assert.Contains("Task 1:\nLearn XML", result);
                    Assert.Contains("Task 2:\nCoffe Time", result);
                }
            }
        }

        [Fact]
        public void Test_add_task_nosave()
        {
            var sb = new StringBuilder();
            sb.AppendLine("1"); // manipulate
            sb.AppendLine("1"); // make
            sb.AppendLine(testTaskName); // title
            sb.AppendLine("MyDescription"); // description
            sb.AppendLine("5"); // days
            sb.AppendLine("no"); // save
            sb.AppendLine("no"); // make another task

            using (var sw = new StringWriter())
            {
                using (var sr = new StringReader(sb.ToString()))
                {
                    Console.SetOut(sw);
                    Console.SetIn(sr);

                    TodoApp.ConsoleApp.ConsoleAppMain.Main(new string[] { "-path ../../../tasks.xml" });
                    var result = sw.ToString();

                    Assert.Contains("Task 1:\nLearn XML", result);
                    Assert.Contains("Task 2:\nCoffe Time", result);
                    Assert.Contains("Good luck!", result);
                    Assert.Contains("Press any key to exit", result);
                }
            }
        }

        [Fact]
        public void Test_add_task_save_and_verify()
        {
            var sb = new StringBuilder();
            sb.AppendLine("1"); // manipulate
            sb.AppendLine("1"); // make
            sb.AppendLine(testTaskName); // title
            sb.AppendLine("MyDescription"); // description
            sb.AppendLine("5"); // days
            sb.AppendLine("yes"); // save
            sb.AppendLine("no"); // make another task

            using (var sw = new StringWriter())
            {
                using (var sr = new StringReader(sb.ToString()))
                {
                    Console.SetOut(sw);
                    Console.SetIn(sr);

                    TodoApp.ConsoleApp.ConsoleAppMain.Main(new string[] { "-path ../../../tasks.xml" });
                    var result = sw.ToString();

                    Assert.Contains("Task 1:\nLearn XML", result);
                    Assert.Contains("Task 2:\nCoffe Time", result);
                    Assert.Contains("Good luck!", result);
                    Assert.Contains("Press any key to exit", result);
                }
            }

            // verify task was saved to file
            using (var sw = new StringWriter())
            {
                using (var sr = new StringReader(string.Empty))
                {
                    Console.SetOut(sw);
                    Console.SetIn(sr);

                    TodoApp.ConsoleApp.ConsoleAppMain.Main(new string[] { "-path ../../../tasks.xml" });
                    var result = sw.ToString();

                    Assert.Contains("Task 1:\nLearn XML", result);
                    Assert.Contains("Task 2:\nCoffe Time", result);
                    Assert.Contains($"Task 3:\n{testTaskName}", result);
                }
            }
        }

        [Fact]
        public void Test_delete_and_verify()
        {
            var sb = new StringBuilder();
            sb.AppendLine("1"); // manipulate
            sb.AppendLine("2"); // delete
            sb.AppendLine("3"); // number of task

            using (var sw = new StringWriter())
            {
                using (var sr = new StringReader(sb.ToString()))
                {
                    Console.SetOut(sw);
                    Console.SetIn(sr);

                    TodoApp.ConsoleApp.ConsoleAppMain.Main(new string[] { "-path ../../../tasks.xml" });
                    var result = sw.ToString();

                    Assert.Contains("Task 1:\nLearn XML", result);
                    Assert.Contains("Task 2:\nCoffe Time", result);
                }
            }

            // verify task was saved to file
            using (var sw = new StringWriter())
            {
                using (var sr = new StringReader(string.Empty))
                {
                    Console.SetOut(sw);
                    Console.SetIn(sr);

                    TodoApp.ConsoleApp.ConsoleAppMain.Main(new string[] { "-path ../../../tasks.xml" });
                    var result = sw.ToString();

                    Assert.Contains("Task 1:\nLearn XML", result);
                    Assert.Contains("Task 2:\nCoffe Time", result);
                    Assert.DoesNotContain($"Task 3:\n{testTaskName}", result);
                }
            }
        }
    }
}
