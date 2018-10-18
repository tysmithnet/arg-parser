using System;
using System.Collections.Generic;
using System.Text;

namespace ArgParser.Core.Help
{
    public struct IdentityInformation
    {
        /// <inheritdoc />
        public IdentityInformation(string name) : this()
        {
            Name = name ?? throw new ArgumentNullException(nameof(name));
        }

        public string Name { get; set; }
        public string Version { get; set; }
        public string ShortDescription { get; set; }
        public string Url { get; set; }
    }

    public interface IHelpful
    {
        IHelpHints HelpHints { get; }
    }

    public interface IHelpHints
    {
        string Name { get; }
        string ShortDescription { get; }
        string Description { get; }

        /// <summary>
        /// Gets the several character text representation of command template
        /// </summary>
        /// <value>The synopsis.</value>
        string Synopsis { get; }
        string Url { get; }
    }

    public class HelpHints : IHelpHints
    {
        /// <inheritdoc />
        public string Name { get; set; }

        /// <inheritdoc />
        public string ShortDescription { get; set; }

        /// <inheritdoc />
        public string Description { get; set; }

        /// <inheritdoc />
        public string Synopsis { get; set; }

        /// <inheritdoc />
        public string Url { get; set; }
    }

    public interface IHelpBuilder<T>
    {
        IHelpBuilder<T> AddIdentityInfomation(IdentityInformation information);
        IHelpBuilder<T> AddSwitch(Switch<T> @switch);
        IHelpBuilder<T> AddSubCommand<TSub>(ISubCommand subCommand) where TSub : T;
        IHelpBuilder<T> AddPositional(Positional<T> positional);
        Node Build();
    }

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