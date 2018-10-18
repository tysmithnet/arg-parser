using System.Collections.Generic;
using System.Text;

namespace ArgParser.Core.Help
{
    public class DefaultHelpBuilder<T> : IHelpBuilder<T>
    {
        protected internal StringBuilder StringBuilder { get; set; } = new StringBuilder();
        protected internal IdentityInformation Identity { get; set; }
        protected internal IList<Switch<T>> Switches { get; set; } = new List<Switch<T>>();
        protected internal IList<Positional<T>> Positionals { get; set; } = new List<Positional<T>>();
        protected internal IList<ISubCommand> SubCommands { get; set; } = new List<ISubCommand>();
        
        /// <inheritdoc />
        public IHelpBuilder<T> AddIdentityInfomation(IdentityInformation information)
        {
            Identity = information;
            return this;
        }

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

        /// <inheritdoc />
        public Node Build()
        {
            StringBuilder
                .AppendLine($"{Identity.Name} - {Identity.Version}")
                .AppendLine($"{Identity.ShortDescription ?? ""}")
                .AppendLine("Sub Commands:");
            foreach (var subCommand in SubCommands)
            {
                StringBuilder
                    .AppendLine($"\t{subCommand.HelpHints?.Synopsis} - {subCommand.HelpHints?.ShortDescription}");
            }

            StringBuilder
                .AppendLine($"Switches:");

            foreach (var @switch in Switches)
            {
                StringBuilder
                    .AppendLine($"\t{@switch.HelpHints?.Synopsis}")
                    .AppendLine($"\t{@switch.HelpHints?.ShortDescription}")
                    .AppendLine();
            }

            StringBuilder.AppendLine("Positionals:");

            foreach (var positional in Positionals)
            {
                StringBuilder
                    .AppendLine($"\t{positional.HelpHints?.Synopsis}")
                    .AppendLine($"\t{positional.HelpHints?.ShortDescription}")
                    .AppendLine();
            }

            return new TextSnippetNode(StringBuilder.ToString());
        }
    }
}