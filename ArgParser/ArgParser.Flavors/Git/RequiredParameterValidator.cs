using System;
using System.Collections.Generic;
using ArgParser.Core.Validation;

namespace ArgParser.Flavors.Git
{
    public class RequiredParameterValidator : IValidator
    {
        /// <inheritdoc />
        public RequiredParameterValidator(GitParameter parameter)
        {
            Parameter = parameter ?? throw new ArgumentNullException(nameof(parameter));
        }

        public GitParameter Parameter { get; set; }

        /// <inheritdoc />
        public bool CanValidate(object instance)
        {
            return true;
        }

        /// <inheritdoc />
        public IValidationResult Validate(object instance)
        {
            if(!Parameter.HasBeenConsumed)
                return new ValidationResult()
                {
                    Instance = instance,
                    Errors = new List<ValidationError>()
                    {
                        new MissingRequiredParameterError()
                        {
                            Message = $"Required parameter not found: {Parameter}", // todo: implement ToString(),
                            RequiredParameter = Parameter
                        }
                    },
                    IsSuccess = false
                };
            return new ValidationResult()
            {
                IsSuccess = true
            };
        }
    }
}