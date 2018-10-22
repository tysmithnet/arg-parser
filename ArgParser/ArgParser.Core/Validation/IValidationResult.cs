using System.Collections.Generic;

namespace ArgParser.Core.Validation
{
    public interface IValidationResult
    {
        bool IsSuccess { get; }
        IList<ValidationError> Errors { get; }
        object Instance { get; }
    }

    public interface IValidationResult<out T> : IValidationResult
    {
        new T Instance { get; }
    }
}