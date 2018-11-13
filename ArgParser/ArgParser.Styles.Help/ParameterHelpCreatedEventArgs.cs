using System;
using ArgParser.Core;

namespace ArgParser.Styles.Help
{
    public class ParameterHelpCreatedEventArgs : EventArgs
    {
        public Parameter Parameter { get; protected internal set; }
        public HelpNode Name { get; protected internal set; }
        public HelpNode Usage { get; protected internal set; }
        public HelpNode Description { get; protected internal set; }

        public ParameterHelpCreatedEventArgs(Parameter parameter, HelpNode name, HelpNode usage, HelpNode description)
        {
            Parameter = parameter ?? throw new ArgumentNullException(nameof(parameter));
            Name = name ?? throw new ArgumentNullException(nameof(name));
            Usage = usage ?? throw new ArgumentNullException(nameof(usage));
            Description = description ?? throw new ArgumentNullException(nameof(description));
        }
    }
}