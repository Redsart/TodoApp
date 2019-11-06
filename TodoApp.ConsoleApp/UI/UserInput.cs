using System;

namespace TodoApp.ConsoleApp.UI
{
    public static class UserInput
    {
        const string Yes = "yes";
        const string No = "no";

        public static string ReadText(string question = "", bool required = false, string defaultValue = "")
        {
            if (!string.IsNullOrEmpty(question))
            {
                Console.Write($"{question} ");
            }

            string text = Console.ReadLine();
            if (required)
            {
                while (string.IsNullOrEmpty(text))
                {
                    Console.WriteLine("This field can't be empty!");
                    text = Console.ReadLine();
                }
            }

            else if (string.IsNullOrEmpty(text))
            {
                text = defaultValue;
            }

            return text;
        }

        public static int ReadOption(string question, string[] availableOptions, bool required = false, int defaultValue = 0)
        {
            Console.WriteLine(question);
            for (int i = 0; i < availableOptions.Length; i++)
            {
                Console.WriteLine($"\t{i+1}: {availableOptions[i]}");
            }
            
            int index = 0;
            bool isValid = false;
            string input = "";

            do
            {
                input = ReadText("Command", required, defaultValue.ToString());
                isValid = int.TryParse(input, out index) && (index > 0 && index <= availableOptions.Length);

                if (!isValid)
                {
                    Console.WriteLine($"Please, enter a valid number between 1 and {availableOptions.Length}!");
                }
            }
            while (!isValid);

            return index;
        }

        public static bool ReadYesNo(string question, bool required = false, bool defaultValue = false)
        {
            string choice = "";
            bool isValid = false;
            bool result = false;

            do
            {
                choice = ReadText(question,required,defaultValue.ToString());
                bool isYes = choice.Equals(Yes, StringComparison.CurrentCultureIgnoreCase);
                bool isNo = choice.Equals(No, StringComparison.CurrentCultureIgnoreCase);
                isValid = isYes || isNo;

                result = isYes;

                if (!isValid)
                {
                    Console.WriteLine("Please, enter either \"yes\" or \"no\"");
                }
            }
            while (!isValid);

            return result;
        }
    }
}

