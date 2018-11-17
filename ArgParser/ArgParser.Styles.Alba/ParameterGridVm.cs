using System.Collections.Generic;

namespace ArgParser.Styles.Alba
{
    public class ParameterGridVm : IViewModel
    {
        public IList<ParameterVm> Parameters { get; set; }
        public Theme Theme { get; set; }
    }
}