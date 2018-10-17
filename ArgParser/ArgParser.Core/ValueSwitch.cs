using System.Text.RegularExpressions;

namespace ArgParser.Core
{
    /// <summary>
    /// Class ValueSwitch.
    /// </summary>
    /// <typeparam name="TOptions">The type of the t options.</typeparam>
    /// <seealso cref="ArgParser.Core.Switch{TOptions}" />
    public abstract class ValueSwitch<TOptions> : Switch<TOptions> where TOptions : IOptions
    {
        /// <summary>
        /// Gets or sets the regex.
        /// </summary>
        /// <value>The regex.</value>
        public Regex Regex { get; set; }

    }
}