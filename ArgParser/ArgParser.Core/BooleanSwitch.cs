using System;

namespace ArgParser.Core
{
    /// <summary>
    /// Class BooleanSwitch.
    /// </summary>
    /// <typeparam name="TOptions">The type of the t options.</typeparam>
    /// <seealso cref="ArgParser.Core.Switch{TOptions}" />
    internal class BooleanSwitch<TOptions> : Switch<TOptions> where TOptions : IOptions
    {
        /// <summary>
        /// Gets or sets the transformer.
        /// </summary>
        /// <value>The transformer.</value>
        public Action<TOptions> Transformer { get; set; }
    }
}