using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace TodoApp.ConsoleApp.Framework.Commands
{
    public class CommandList
    {
        public string Message { get; set; }


        public string InvalidMessage = "Invalid Command! Please, try again!";

        private IList<Command> Commands { get; set; }

        public bool Available { get => Commands.Count > 0; }

        public CommandList(params Command[] commands)
        {
            Reset();
        }

        internal void Reset()
        {
            Message = "";
            Commands = new List<Command>();
        }

        public void Add(params Command[] cmds)
        {
            foreach (var cmd in cmds)
            {
                Commands.Add(cmd);
            };
        }

        public void Remove(params Command[] cmds)
        {
            foreach (var cmd in cmds)
            {
                Commands.Remove(cmd);
            };
        }

        public void Clear()
        {
            Commands.Clear();
        }

        public Command FindMatch(string input)
        {
            return Commands.FirstOrDefault(cmd => cmd.IsMatch(input));
        }

        public void Run(string input)
        {
            var cmd = FindMatch(input);
            bool success = cmd != null;
            if (!success)
            {
                throw new ArgumentException("Provided input do not match any Command.", nameof(input));
            }

            cmd.Run(input);
        }

        public void TryRun(string input, out bool success)
        {
            var cmd = FindMatch(input);
            success = cmd != null;
            if (success)
            {
                cmd.Run(input);
            }
        }

        public override string ToString()
        {
            var sb = new StringBuilder();

            if (!string.IsNullOrEmpty(Message))
            {
                sb.AppendLine(Message);
            }

            foreach (var cmd in Commands)
            {
                sb.AppendFormat("  {0}", cmd);
                sb.AppendLine();
            }

            return sb.ToString();
        }
    }
}
