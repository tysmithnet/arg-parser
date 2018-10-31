using System.Collections.Generic;

namespace ArgParser.Core.Validation
{
    public class ValidationResult<T> : ValidationResult, IValidationResult<T>
    {
            
        public new T Instance { get; set; }
    }

    public class ValidationResult : IValidationResult
    {
            
        public IList<ValidationError> Errors { get; set; }

            
        public object Instance { get; set; }

            
        public bool IsSuccess { get; set; }
    }
}