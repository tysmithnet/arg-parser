namespace ArgParser.Core
{
    /// <summary>
    /// Interface ICommandLineElement
    /// </summary>
    public interface ICommandLineElement
    {
        /// <summary>
        /// Gets a value indicating whether this <see cref="ICommandLineElement"/> is required.
        /// </summary>
        /// <value><c>true</c> if required; otherwise, <c>false</c>.</value>
        bool Required { get; }
        /// <summary>
        /// Gets the help text.
        /// </summary>
        /// <value>The help text.</value>
        Help Help { get; }
    }
}