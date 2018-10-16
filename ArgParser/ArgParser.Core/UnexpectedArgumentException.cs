using System;

namespace ArgParser.Core
{
    /// <summary>
    /// Class UnexpectedArgumentException.
    /// </summary>
    /// <seealso cref="System.ArgumentException" />
    public class UnexpectedArgumentException : ArgumentException
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="UnexpectedArgumentException"/> class.
        /// </summary>
        /// <param name="message">The error message that explains the reason for the exception.</param>
        /// <inheritdoc />
        public UnexpectedArgumentException(string message) : base(message)
        {
        }
    }
}