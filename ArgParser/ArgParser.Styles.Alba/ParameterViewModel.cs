using System;
using ArgParser.Core;

namespace ArgParser.Styles.Alba
{
    public class ParameterViewModel
    {
        public Parameter Parameter { get; set; }

        public string RequiredText
        {
            get
            {
                if (Parameter is IRequirable casted && casted.IsRequired)
                    return "✓";
                return "";
            }
        }

        public Theme Theme { get; set; }

        public ParameterViewModel(Parameter parameter, Theme theme)
        {
            Parameter = parameter ?? throw new ArgumentNullException(nameof(parameter));
            Theme = theme ?? throw new ArgumentNullException(nameof(theme));
        }
    }
}