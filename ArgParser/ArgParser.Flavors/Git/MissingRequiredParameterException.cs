using ArgParser.Core.Validation;

namespace ArgParser.Flavors.Git
{
    public class MissingRequiredParameterException : ValidationException
    {
        public GitParameter RequiredParameter { get; set; }
    }
}