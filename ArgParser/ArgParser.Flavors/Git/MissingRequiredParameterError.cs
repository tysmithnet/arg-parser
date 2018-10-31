using ArgParser.Core.Validation;

namespace ArgParser.Flavors.Git
{
    public class MissingRequiredParameterError : ValidationError
    {
        public GitParameter RequiredParameter { get; set; }
    }
}