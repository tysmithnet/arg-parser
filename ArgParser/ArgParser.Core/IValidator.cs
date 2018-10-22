namespace ArgParser.Core
{
    public interface IValidator<in T> : IValidator
    {
        bool CanValidate(T instance);
        IValidationResult Validate(T instance);
    }

    public interface IValidator
    {
        bool CanValidate(object instance);
        IValidationResult Validate(object instance);
    }
}