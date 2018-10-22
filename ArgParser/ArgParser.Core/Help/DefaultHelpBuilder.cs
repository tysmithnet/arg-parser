using System;
using System.Collections.Generic;
using System.Text;

namespace ArgParser.Core.Help
{
    public class DefaultHelpBuilder : IHelpBuilder
    {
        public IGenericHelp Help { get; set; }

        public DefaultHelpBuilder AddGenericHelp(IGenericHelp help)
        {
            Help = help;
            return this;
        }

        /// <inheritdoc />
        public IHelpNode Build()
        {
            return new TextNode("");
        }
    }
}
