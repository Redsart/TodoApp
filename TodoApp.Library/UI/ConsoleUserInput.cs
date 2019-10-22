using System;

namespace TodoApp.Library.UI
{
    public static class ConsoleUserInput
    {
        public static string ReadText(bool required = false)
        {
            string text = Console.ReadLine();

            if (required == true)
            {
                while (string.IsNullOrEmpty(text))
                {
                    Console.WriteLine("This field can't be empty!");
                    text = Console.ReadLine();
                }
            }
            else
            {
                text = Console.ReadLine();
            }

            return text;
        }

        public static int ReadOption(string question, string[] availableOptions)
        {
            Console.WriteLine(question);
            Console.WriteLine("Press the number of youre choice!");
            for (int i = 0; i < availableOptions.Length; i++)
            {
                Console.WriteLine("{0}: {1}", i + 1, availableOptions[i]);
            }

            int number = int.Parse(ReadText(true));
            while (number < 1 && number > availableOptions.Length)
            {
                number = int.Parse(ReadText(true));
            }

            return number;
        }

        public static bool ReadYesNo(string question, bool defaultValue = false)
        {
            Console.WriteLine(question + "yes/no");
            string choice = ReadText(true);

            while (choice != "yes" && choice != "no")
            {
                choice = ReadText(true);
            }

            if (choice == "yes")
            {
                return true;
            }

            return false;
        }
    }
}

