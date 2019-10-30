using System;
using System.IO;
using Xunit;

namespace TodoApp.UnitTests
{
    public class ProgramTest
    {
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
    }
}
