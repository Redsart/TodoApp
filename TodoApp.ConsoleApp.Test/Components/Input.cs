using System;
using System.Text.RegularExpressions;

namespace TodoApp.ConsoleApp.Test.Components
{
    internal delegate bool TryParse<T>(string input, out T result);

    internal static class Input
    {
        private static T Read<T>(string label, TryParse<T> tryParse, string errorMsg)
        {
            bool isValid = false;
            T result = default;

            while (!isValid)
            {
                var input = ReadText(label);
                isValid = tryParse(input, out result);

                if (!isValid)
                {
                    Output.WriteWarning(errorMsg);
                }
            }

            return result;
        }

        public static char ReadChar()
        {
            return Console.ReadKey().KeyChar;
        }

        public static string ReadText(string label = "")
        {
            if (!string.IsNullOrEmpty(label))
            {
                Output.WriteLabel(label);
            }

            return Console.ReadLine();
        }


        public static int ReadInt(string label = "", string msg = "Please, enter a valid integer number!")
        {
            return Read<int>(label, int.TryParse, msg);
        }
    }
}
