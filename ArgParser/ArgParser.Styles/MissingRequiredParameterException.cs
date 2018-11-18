using System;
using ArgParser.Core;
using ArgParser.Core.Extensions;

namespace ArgParser.Styles
{
    public class MissingRequiredParameterException : ParseException
    {
        public MissingRequiredParameterException(Parameter requiredParameter, object instance) : base(
            CreateExceptionMessage(requiredParameter))
        {
            RequiredParameter = requiredParameter.ThrowIfArgumentNull(nameof(requiredParameter));
            Instance = instance.ThrowIfArgumentNull(nameof(instance));
        }

        private static string CreateExceptionMessage(Parameter parameter)
        {
            if (parameter is Switch casted)
                return
                    $"Expected to find required switch=[-{casted.Letter?.ToString() ?? "<no letter>"}, --{casted.Word ?? "<no word>"}], but did not.";
            return $"Expected to find required positional but did not.";
        }

        public object Instance { get; protected internal set; }
        public Parameter RequiredParameter { get; protected internal set; }
    }
}