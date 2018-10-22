using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ArgParser.Core.Help
{
    public class DefaultHelpBuilder : IHelpBuilder
    {
        public DefaultHelpBuilder AddGenericHelp(IGenericHelp help)
        {
            Help = help;
            return this;
        }

        public DefaultHelpBuilder AddParameter(string name, string[] usages, string shortDescription)
        {
            ParameterInfos.Add(new ParameterInfo
            {
                Name = name,
                Usages = usages,
                ShortDescription = shortDescription
            });
            return this;
        }

        /// <inheritdoc />
        public IHelpNode Build()
        {
            var header = BuildHeader();
            var body = BuildBody();
            var footer = BuildFooter();
            var all = new[] {header, body, footer}.Where(x => !x.IsNullOrWhiteSpace()).Join(Environment.NewLine);
            return new TextNode(all);
        }

        private string BuildBody()
        {
            if (!ParameterInfos.Any())
                return null;
            var sb = new StringBuilder();
            foreach (var info in ParameterInfos)
            {
                var firstLine = new[] {info.Name, info.ShortDescription}.JoinNonNullOrWhiteSpace(" - ");
                if (!firstLine.IsNullOrWhiteSpace()) sb.AppendLine(firstLine.Trim());

                foreach (var usage in info.Usages.PreventNull()) sb.AppendLine($"    {usage}");

                sb.AppendLine();
            }

            return sb.ToString();
        }

        private string BuildFooter() => Help?.Author.IsNullOrWhiteSpace() ?? true ? null : $"Author: {Help.Author}";

        private string BuildHeader()
        {
            if (Help == null)
                return null;
            var sb = new StringBuilder();
            var first = new[] {Help.Name, Help.Version, Help.ShortDescription}.JoinNonNullOrWhiteSpace(" - ");
            sb.AppendLine(first);
            if (Help.Description != null)
                sb
                    .AppendLine(Help.Description);

            return sb.ToString();
        }

        public IGenericHelp Help { get; set; }

        public IList<ParameterInfo> ParameterInfos { get; set; } = new List<ParameterInfo>();

        public class ParameterInfo
        {
            public string Name { get; set; }
            public string ShortDescription { get; set; }
            public string[] Usages { get; set; }
        }
    }
}