using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using Microsoft.Extensions.DependencyInjection;

namespace TodoApp.ConsoleApp.Framework.Commands
{
    public class CommandList
    {
        private readonly IServiceProvider ServiceProvider;
     
        public string Message { get; set; }

        public string InvalidMessage = "Invalid Command! Please, try again!";

        private IList<Command> Commands { get; set; }

        public bool Available { get => Commands.Count > 0; }

        public CommandList(IServiceProvider serviceProvider)
        {
            ServiceProvider = serviceProvider;

            Reset();
        }

        internal void Reset()
        {
            Message = "";
            Commands = new List<Command>();
        }

        public Command Add<TCmd, TVm>(TVm vm)
            where TCmd: Command<TVm>
            where TVm: ViewModel
        {
            var cmd = ServiceProvider.GetRequiredService<TCmd>();
            cmd.DataSource = vm;

            if (!cmd.CanRun())
            {
                return null;
            }

            Commands.Add(cmd);
            return cmd;
        }

        public Command Add(string name, string match, Action<string> action)
        {
            var cmd = new Command(name, match, action);
            Commands.Add(cmd);
            return cmd;
        }

        public Command Add(string name, string display, Regex match, Action<string> action)
        {
            var cmd = new Command(name, display, match, action);
            Commands.Add(cmd);
            return cmd;
        }

        public Command Add(string name, string display, Func<string, bool> match, Action<string> action)
        {
            var cmd = new Command(name, display, match, action);
            Commands.Add(cmd);
            return cmd;
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
