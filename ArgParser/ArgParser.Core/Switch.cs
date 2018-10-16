namespace ArgParser.Core
{
    /// <summary>
    /// Class Switch.
    /// </summary>
    /// <typeparam name="TOptions">The type of the t options.</typeparam>
    /// <seealso cref="ArgParser.Core.ICommandLineElement" />
    internal abstract class Switch<TOptions> : ICommandLineElement where TOptions : IOptions
    {
        /// <summary>
        /// Gets or sets the letter.
        /// </summary>
        /// <value>The letter.</value>
        public char Letter { get; set; }
        /// <summary>
        /// Gets or sets the word.
        /// </summary>
        /// <value>The word.</value>
        public string Word { get; set; }
        /// <summary>
        /// Gets or sets the help text.
        /// </summary>
        /// <value>The help text.</value>
        public string HelpText { get; set; }
        /// <summary>
        /// Gets or sets a value indicating whether this <see cref="Switch{TOptions}"/> is required.
        /// </summary>
        /// <value><c>true</c> if required; otherwise, <c>false</c>.</value>
        public bool Required { get; set; }
    }
}