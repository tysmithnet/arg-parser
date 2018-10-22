using System.Collections.Generic;

namespace ArgParser.Core.Validation
{
    public interface IValidationResult
    {
        bool IsSuccess { get; }
        IList<ValidationError> Errors { get; }
        object Instance { get; }
    }

    public class ValidationResult : IValidationResult
    {
        /// <inheritdoc />
        public bool IsSuccess { get; set; }

        /// <inheritdoc />
        public IList<ValidationError> Errors { get; set; }

        /// <inheritdoc />
        public object Instance { get; set; }
    }

    public interface IValidationResult<out T> : IValidationResult
    {
        new T Instance { get; }
    }

    public class ValidationResult<T> : ValidationResult, IValidationResult<T>
    {
        /// <inheritdoc />
        public new T Instance { get; set; }
    }
}