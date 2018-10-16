using System;

namespace ArgParser.Core
{
    /// <summary>
    /// Class MissingValueException.
    /// </summary>
    /// <seealso cref="System.ArgumentException" />
    public class MissingValueException : ArgumentException
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="MissingValueException"/> class.
        /// </summary>
        /// <param name="message">The error message that explains the reason for the exception.</param>
        public MissingValueException(string message) : base(message)
        {
        }
    }
}