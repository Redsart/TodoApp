using System;

namespace TodoApp.Library.UI
{
    static class Validate
    {
        public static bool IsCorectNumber(string number, int length)
        {
            bool isTrue = false;
            int n = 0;
            if (!int.TryParse(number,out n))
            {
                Console.WriteLine("{0} is not a number!", number);
            }

            else if (n < 1 || n > length)
            {
                Console.WriteLine("The number must be in range 1 - {0}", length);
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

            if (string.Compare(text, "yes", StringComparison.OrdinalIgnoreCase) == 0 || string.Compare(text, "no", StringComparison.OrdinalIgnoreCase) == 0)
            {
                return isTrue = true;
            }
            else
            {
                isTrue = false;
            }

            return isTrue;
        }
    }
}
