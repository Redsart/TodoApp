using System;

namespace TodoApp.ConsoleApp.UI
{
    public static class UserInput
    {
        const string Yes = "yes";
        const string No = "no";

        private static string ReadInput(bool required = false)
        {
            string text = "";
            if (required == true)
            {
                text = Console.ReadLine();

                while (string.IsNullOrEmpty(text))
                {
                    Console.WriteLine("This field can't be empty!");
                    text = Console.ReadLine();
                }
            }

            return text;
        }

        public static string ReadText(string question, bool required = false, string defaultValue = "")
        {
            Console.WriteLine(question);

            string text = ReadInput(required);

            if (string.IsNullOrEmpty(text))
            {
                text = defaultValue;
            }

            return text;
        }

        public static int ReadOption(string question, string[] availableOptions, bool required = false, int defaultValue = 0)
        {
            
            ReadText("Press the number of youre choice!", false, "");
            int number = 0;
            string input = "";

            if (required == true)
            {
                for (int i = 0; i < availableOptions.Length; i++)
                {
                    ReadText($"{i + 1}: {availableOptions[i]}", false, "");
                }

                input = ReadText(question, true);
                while (!Validate.IsCorectNumber(input, availableOptions.Length))
                {
                    input = ReadText(question, true);
                }

                number = int.Parse(input);

                return number;
            }

            else
            {
                input = ReadText(question, true);
                if (Validate.IsCorectNumber(input, availableOptions.Length))
                {
                    number = int.Parse(input);

                    return number;
                }

                else
                {
                    return defaultValue;
                }
            }
        }

        public static bool ReadYesNo(string question, bool required = false, bool defaultValue = false)
        {
            string choice = ReadText($"{question} {Yes}/{No}", true);

            if (required == true)
            {
                while (!Validate.IsYesOrNo(choice))
                {
                    choice = ReadText($"{question} {Yes}/{No}", true, "Choose an option!");
                }

                return choice.Equals(Yes, StringComparison.CurrentCultureIgnoreCase);
            }

            else
            {
                if (Validate.IsYesOrNo(choice))
                {
                    return choice.Equals(Yes, StringComparison.CurrentCultureIgnoreCase);
                }

                else
                {
                    return defaultValue;
                }
            }
        }
    }
}

