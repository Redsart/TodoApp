using System;

namespace TodoApp.ConsoleApp.UI
{
    static class Validate
    {
        public static bool IsCorectNumber(string number, int length)
        {
            bool isTrue = false;
            int n = 0;
            if (!int.TryParse(number,out n))
            {
                Console.WriteLine($"{number} is not a number!");
            }

            else if (n < 1 || n > length)
            {
                Console.WriteLine($"The number must be in range 1 - {length}");
            }

            else
            {
                isTrue = true;
            }

            return isTrue;
        }

        public static bool IsYesOrNo(string text)
        {
            bool isTrue;
            
            if (text.Equals("yes", StringComparison.CurrentCultureIgnoreCase) || (text.Equals("no", StringComparison.CurrentCultureIgnoreCase)))
            {
                isTrue = true;
            }

            else
            {
                isTrue = false;
            }

            return isTrue;
        }
    }
}
