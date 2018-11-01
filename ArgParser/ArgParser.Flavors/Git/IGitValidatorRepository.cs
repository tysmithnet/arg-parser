using ArgParser.Core.Validation;

namespace ArgParser.Flavors.Git
{
    public interface IGitValidatorRepository
    {
        void AddValidator(IValidator validator);
    }
}