using System.Collections.Generic;
using System.Linq;
using System.Text;
using ArgParser.Core;
using ArgParser.Core.Extensions;

namespace ArgParser.Styles.Alba
{
    public class ParserHelpTemplateViewModel
    {
        public string BannerColor =>
            $"{ParserVm.Theme.DefaultTextColor} 0; {ParserVm.Theme.SecondaryTextColor}; {ParserVm.Theme.CodeColor} 3";

        public IList<ParserViewModel> Chain { get; set; }
        public IList<ParameterViewModel> ParameterVms { get; set; }
        public ParserViewModel ParserVm => Chain.Last();

        public string SubTitle
        {
            get
            {
                var sb = new StringBuilder(ParserVm.Parser.Id);
                if (ParserVm.Parser.Help.Version.IsNotNullOrWhiteSpace())
                    sb.Append($" - {ParserVm.Parser.Help.Version}");
                if (ParserVm.Parser.Help.ShortDescription.IsNotNullOrWhiteSpace())
                    sb.Append($" - {ParserVm.Parser.Help.ShortDescription}");
                return sb.ToString();
            }
        }
    }
}