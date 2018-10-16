using System;
using System.Text.RegularExpressions;

namespace ArgParser.Core
{
    /// <summary>
    /// Class PositionalValues.
    /// </summary>
    /// <typeparam name="TOptions">The type of the t options.</typeparam>
    /// <seealso cref="ArgParser.Core.ICommandLineElement" />
    internal class PositionalValues<TOptions> : ICommandLineElement where TOptions : IOptions
    {
        /// <summary>
        /// Gets or sets the count.
        /// </summary>
        /// <value>The count.</value>
        public int Count { get; set; }
        /// <summary>
        /// Gets or sets the regex.
        /// </summary>
        /// <value>The regex.</value>
        public Regex Regex { get; set; }
        /// <summary>
        /// Gets or sets the transformer.
        /// </summary>
        /// <value>The transformer.</value>
        public Action<TOptions, string[]> Transformer { get; set; }

        /// <summary>
        /// Gets or sets a value indicating whether this <see cref="PositionalValues{TOptions}"/> is required.
        /// </summary>
        /// <value><c>true</c> if required; otherwise, <c>false</c>.</value>
        /// <inheritdoc />
        public bool Required { get; set; }

        /// <summary>
        /// Gets or sets the help text.
        /// </summary>
        /// <value>The help text.</value>
        /// <inheritdoc />
        public string HelpText { get; set; }
    }
}