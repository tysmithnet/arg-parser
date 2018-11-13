using System;

namespace ArgParser.Styles.Help
{
    public class SubCommandCreatedEventArgs : EventArgs
    {
        public string SubCommandId { get; protected internal set; }
        public HelpNode Name { get; protected internal set; }
        public HelpNode Description { get; protected internal set; }

        public SubCommandCreatedEventArgs(string subCommandId, HelpNode name, HelpNode description)
        {
            SubCommandId = subCommandId ?? throw new ArgumentNullException(nameof(subCommandId));
            Name = name ?? throw new ArgumentNullException(nameof(name));
            Description = description ?? throw new ArgumentNullException(nameof(description));
        }
    }
}