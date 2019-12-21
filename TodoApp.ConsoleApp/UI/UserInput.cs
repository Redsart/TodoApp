using System;
using System.Globalization;

namespace TodoApp.ConsoleApp.UI
{
    public static class UserInput
    {
        const string Yes = "yes";
        const string No = "no";
        static readonly IFormatProvider provider = CultureInfo.CurrentCulture;

        public static string ReadText(string question, bool required = false, string defaultValue = "")
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
                    Console.WriteLine(UserComments.FieldCantBeEmpty());
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
            if (availableOptions == null)
            {
                throw new ArgumentNullException($"There is no available options!");
            }
            for (int i = 0; i < availableOptions.Length; i++)
            {
                Console.WriteLine($"\t{i+1}: {availableOptions[i]}");
            }
            
            int index = 0;
            bool isValid = false;
            string input = "";

            do
            {
                input = ReadText("Command", required, defaultValue.ToString(provider));
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
                choice = ReadText(question, required, defaultValue.ToString(provider));
                bool isYes = choice.Equals(Yes, StringComparison.CurrentCultureIgnoreCase);
                bool isNo = choice.Equals(No, StringComparison.CurrentCultureIgnoreCase);
                isValid = isYes || isNo;

                result = isYes;

                if (!isValid)
                {
                    Console.WriteLine(UserComments.YesOrNo());
                }
            }
            while (!isValid);

            return result;
        }

        public static int ReadInt(string question, int min, int max, bool required = true, int defaultValue = 0)
        {
            Console.WriteLine(question);
            int number = defaultValue; 
            bool isValid = false;

            do
            {
                string input = ReadText("", required, defaultValue.ToString(provider));
                isValid = int.TryParse(input, out number) && number >= min && number <= max;

                if (!isValid)
                {
                    Console.WriteLine($"Please enter a valid number in range {min} - {max}");
                }

            }
            while (!isValid);

            return number;
        }
    }
}

