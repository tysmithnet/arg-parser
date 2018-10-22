using System.Collections.Generic;

namespace ArgParser.Core
{
    public interface IValidationResult
    {
        bool IsSuccess { get; }
        IList<ValidationError> Errors { get; }
    }
}