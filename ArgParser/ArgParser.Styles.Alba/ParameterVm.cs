using ArgParser.Core;

namespace ArgParser.Styles.Alba
{
    public class ParameterVm : IViewModel
    {
        public Parameter Base { get; set; }
        public string RequiredText { get; set; }
        public Theme Theme { get; set; }
    }
}