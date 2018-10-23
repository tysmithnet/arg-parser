using System.Collections.Generic;

namespace ArgParser.Core.Validation
{
    public class ValidationResult<T> : ValidationResult, IValidationResult<T>
    {
        /// <inheritdoc />
        public new T Instance { get; set; }
    }

    public class ValidationResult : IValidationResult
    {
        /// <inheritdoc />
        public IList<ValidationError> Errors { get; set; }

        /// <inheritdoc />
        public object Instance { get; set; }

        /// <inheritdoc />
        public bool IsSuccess { get; set; }
    }
}