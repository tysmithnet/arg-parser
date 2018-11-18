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
    }
}