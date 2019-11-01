using System;

namespace TodoApp.ConsoleApp.UI
{
    public static class UserInput
    {
        const string Yes = "yes";
        const string No = "no";

        private static string ReadInput(bool required = false, string defaultValue = "")
        {
            string text = Console.ReadLine();
            if (required)
            {
                while (string.IsNullOrEmpty(text))
                {
                    Console.WriteLine("This field can't be empty!");
                    text = Console.ReadLine();
                }
            }

            else
            {
                if (string.IsNullOrEmpty(text))
                {
                    text = defaultValue;
                }
            }

            return text;
        }

        public static string ReadText(string question, bool required = false, string defaultValue = "")
        {
            Console.WriteLine(question);
            string text = "";

            if (required)
            {
                text = ReadInput(required);

                if (string.IsNullOrEmpty(text))
                {
                    text = defaultValue;
                }
            }

            else
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

            //if (required)
            //{
               
            //}
            do
            {
                input = ReadInput(required);
                isValid = int.TryParse(input, out index) && (index > 0 && index <= availableOptions.Length);

                if (!isValid)
                {
                    Console.WriteLine($"Please, enter a valid number between 1 and {availableOptions.Length}!");
                }
            }
            while (!isValid);

            //else
            //{
            //    input = ReadInput(required);
            //    isValid = int.TryParse(input, out index) && (index > 0 && index < availableOptions.Length);
            //    if (!isValid)
            //    {
            //        index = defaultValue;
            //    }
            //}

            return index;
        }

        public static bool ReadYesNo(string question, bool required = false, bool defaultValue = false)
        {
            Console.WriteLine(question);
            string choice = "";
            bool isValid = false;
            bool result = false;

            if (required)
            {
                do
                {
                    choice = ReadInput(required);
                    isValid = choice.Equals(Yes, StringComparison.CurrentCultureIgnoreCase) || choice.Equals(No, StringComparison.CurrentCultureIgnoreCase);

                    if (isValid)
                    {
                        if (choice.Equals(Yes, StringComparison.CurrentCultureIgnoreCase))
                        {
                            result = true;
                        }

                        else if (choice.Equals(No, StringComparison.CurrentCultureIgnoreCase))
                        {
                            result = false;
                        }
                    }

                    else
                    {
                        Console.WriteLine("Please, enter either \"yes\" or \"no\"");
                    }
                }
                while (!isValid);
            }

            else
            {
                choice = ReadInput(required);
                isValid = choice.Equals(Yes, StringComparison.CurrentCultureIgnoreCase) || choice.Equals(No, StringComparison.CurrentCultureIgnoreCase);

                if (isValid)
                {
                    if (choice.Equals(Yes, StringComparison.CurrentCultureIgnoreCase))
                    {
                        result = true;
                    }

                    else if (choice.Equals(No, StringComparison.CurrentCultureIgnoreCase))
                    {
                        result = false;
                    }
                }

                else
                {
                    result = defaultValue;
                }
            }

            return result;
        }
    }
}

