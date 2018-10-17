using System;
using System.Text.RegularExpressions;

namespace ArgParser.Core
{
    /// <summary>
    /// Class PositionalValues.
    /// </summary>
    /// <typeparam name="TOptions">The type of the t options.</typeparam>
    /// <seealso cref="ArgParser.Core.ICommandLineElement" />
    public class PositionalValues<TOptions> : ICommandLineElement where TOptions : IOptions
    {
        public int? Min { get; set; }
        public int? Max { get; set; }

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

        /// <inheritdoc />
        public string Name { get; }

        /// <summary>
        /// Gets or sets a value indicating whether this <see cref="PositionalValues{TOptions}"/> is required.
        /// </summary>
        /// <value><c>true</c> if required; otherwise, <c>false</c>.</value>
        /// <inheritdoc />
        public bool Required { get; set; }

        /// <inheritdoc />
        public Help Help { get; set; }

    }
}