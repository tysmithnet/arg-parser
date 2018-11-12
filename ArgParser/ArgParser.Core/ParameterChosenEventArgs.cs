using System;

namespace ArgParser.Core
{
    public class ParameterChosenEventArgs : EventArgs
    {
        public Parameter Parameter { get; protected internal set; }
        public ConsumptionRequest Request { get; protected internal set; }

        public ParameterChosenEventArgs(Parameter parameter, ConsumptionRequest request)
        {
            Parameter = parameter ?? throw new ArgumentNullException(nameof(parameter));
            Request = request ?? throw new ArgumentNullException(nameof(request));
        }
    }
}