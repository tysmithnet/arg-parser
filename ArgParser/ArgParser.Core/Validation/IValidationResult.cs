using System.Collections.Generic;

namespace ArgParser.Core.Validation
{
    public interface IValidationResult
    {
        bool IsSuccess { get; }
        IList<ValidationError> Errors { get; }
    }
}