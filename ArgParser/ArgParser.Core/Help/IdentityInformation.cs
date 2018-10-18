using System;

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
}