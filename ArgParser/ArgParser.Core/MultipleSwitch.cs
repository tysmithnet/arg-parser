using System;

namespace ArgParser.Core
{
    /// <summary>
    /// Class MultipleSwitch.
    /// </summary>
    /// <typeparam name="TOptions">The type of the t options.</typeparam>
    /// <seealso cref="ArgParser.Core.ValueSwitch{TOptions}" />
    internal class MultipleSwitch<TOptions> : ValueSwitch<TOptions> where TOptions : IOptions
    {
        /// <summary>
        /// Gets or sets the count.
        /// </summary>
        /// <value>The count.</value>
        public int Count { get; set; } = -1;
        /// <summary>
        /// Gets or sets the transformer.
        /// </summary>
        /// <value>The transformer.</value>
        public Action<TOptions, string[]> Transformer { get; set; }
    }
}