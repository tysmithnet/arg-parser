using System.Collections.Generic;

namespace ArgParser.Core.Validation
{
    public interface IValidationResult
    {
        IList<ValidationError> Errors { get; }
        object Instance { get; }
        bool IsSuccess { get; }
    }

    public interface IValidationResult<out T> : IValidationResult
    {
        new T Instance { get; }
    }
}