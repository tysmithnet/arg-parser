using System;
using System.Collections.Generic;
using ArgParser.Core.Validation;

namespace ArgParser.Flavors.Git
{
    public class RequiredParameterValidator : IValidator
    {
            
        public RequiredParameterValidator(GitParameter parameter)
        {
            Parameter = parameter ?? throw new ArgumentNullException(nameof(parameter));
        }

            
        public bool CanValidate(object instance) => true;

            
        public IValidationResult Validate(object instance)
        {
            if (!Parameter.HasBeenConsumed)
                return new ValidationResult
                {
                    Instance = instance,
                    Errors = new List<ValidationError>
                    {
                        new MissingRequiredParameterError
                        {
                            Message = $"Required parameter not found: {Parameter}", // todo: implement ToString(),
                            RequiredParameter = Parameter
                        }
                    },
                    IsSuccess = false
                };
            return new ValidationResult
            {
                IsSuccess = true
            };
        }

        public GitParameter Parameter { get; set; }
    }
}