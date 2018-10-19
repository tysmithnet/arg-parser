using System.Collections.Generic;
using System.Text;

namespace ArgParser.Core.Help
{
    public class DefaultHelpBuilder<T> : IHelpBuilder<T>, IHelpful
    {
        protected internal StringBuilder StringBuilder { get; set; } = new StringBuilder();
        protected internal IList<Switch<T>> Switches { get; set; } = new List<Switch<T>>();
        protected internal IList<Positional<T>> Positionals { get; set; } = new List<Positional<T>>();
        protected internal IList<ISubCommand> SubCommands { get; set; } = new List<ISubCommand>();
        
        /// <inheritdoc />
        public IHelpBuilder<T> AddSwitch(Switch<T> @switch)
        {
            Switches.Add(@switch);
            return this;
        }

        /// <inheritdoc />
        public IHelpBuilder<T> AddSubCommand<TSub>(ISubCommand subCommand) where TSub : T
        {
            SubCommands.Add(subCommand);
            return this;
        }

        /// <inheritdoc />
        public IHelpBuilder<T> AddPositional(Positional<T> positional)
        {
            Positionals.Add(positional);
            return this;
        }

        public IHelpBuilder<T> AddHelp(IHelp help)
        {
            Help = help;
            return this;
        }

        /// <inheritdoc />
        public Node Build()
        {
            StringBuilder
                .AppendLine($"{Help.Name} - {Help.Version}")
                .AppendLine($"{Help.ShortDescription}")
                .AppendLine("Sub Commands:");
            foreach (var subCommand in SubCommands)
            {
                StringBuilder
                    .AppendLine($"\t{subCommand.Help?.Synopsis} - {subCommand.Help?.ShortDescription}");
            }

            StringBuilder
                .AppendLine($"Switches:");

            foreach (var @switch in Switches)
            {
                StringBuilder
                    .AppendLine($"\t{@switch.Help?.Synopsis}")
                    .AppendLine($"\t{@switch.Help?.ShortDescription}")
                    .AppendLine();
            }

            StringBuilder.AppendLine("Positionals:");

            foreach (var positional in Positionals)
            {
                StringBuilder
                    .AppendLine($"\t{positional.Help?.Synopsis}")
                    .AppendLine($"\t{positional.Help?.ShortDescription}")
                    .AppendLine();
            }

            return new TextSnippetNode(StringBuilder.ToString());
        }

        /// <inheritdoc />
        public IHelp Help { get; protected internal set; } = new HelpInfo();
    }
}