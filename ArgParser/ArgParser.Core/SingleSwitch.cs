using System;

namespace ArgParser.Core
{
    /// <summary>
    /// Class SingleSwitch.
    /// </summary>
    /// <typeparam name="TOptions">The type of the t options.</typeparam>
    /// <seealso cref="ArgParser.Core.ValueSwitch{TOptions}" />
    internal class SingleSwitch<TOptions> : ValueSwitch<TOptions> where TOptions : IOptions
    {
        /// <summary>
        /// Gets or sets the transformer.
        /// </summary>
        /// <value>The transformer.</value>
        public Action<TOptions, string> Transformer { get; set; }
    }
}