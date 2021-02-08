using System;
using System.Collections.Generic;
using System.Text;
using System.Text.RegularExpressions;

namespace TodoApp.ConsoleApp.Framework.Commands
{
    public class Command
    {
        public string Display { get; }

        public string Name { get; }

        private Func<string, bool> Match { get; }

        private Action<string> Action { get; }

        public Command(string name, string match, Action<string> action)
            : this(name, match.Trim(), match.Equals, action)
        { }

        public Command(string name, string display, Regex match, Action<string> action)
            : this(name, display, match.IsMatch, action)
        { }

        public Command(string name, string display, Func<string, bool> match, Action<string> action)
        {
            Name = name;
            Display = display;
            Match = match;
            Action = action;
        }

        public bool IsMatch(string input)
        {
            return Match(input);
        }

        public void Run(string input)
        {
            bool success = IsMatch(input);

            if (!success)
            {
                throw new ArgumentException("Provided input do not match the Command.", nameof(input));
            }

            Action(input);
        }

        public void TryRun(string input, out bool success)
        {
            success = IsMatch(input);

            if (success)
            {
                Action(input);
            }
        }

        public override string ToString()
        {
            if (string.IsNullOrEmpty(Name))
            {
                if (string.IsNullOrEmpty(Display))
                {
                    return "";
                }

                return Display;
            }

            return string.Format("{0} - {1}", Display, Name);
        }
    }
}
