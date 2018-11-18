using System.Text;
using Alba.CsConsoleFormat;
using ArgParser.Core;

namespace ArgParser.Styles.Alba
{
    public class ParameterUsage : InlineElement
    {
        public override void GenerateSequence(IInlineSequence sequence)
        {
            if (ViewModel.Parameter is Switch @switch)
                GenerateSwitchSequence(@switch, sequence);
            else if (ViewModel.Parameter is Positional positional)
                GeneratePositionalSequence(positional, sequence);
        }

        private void GeneratePositionalSequence(Positional positional, IInlineSequence seq)
        {
            WritePrimary(seq, "[");
            WriteSecondary(seq, GenerateValueAlias(positional));
            WritePrimary(seq, "]");
        }

        private void GenerateSwitchSequence(Switch @switch, IInlineSequence seq)
        {
            WritePrimary(seq, "[");
            if (@switch.Letter.HasValue && @switch.Word.IsNotNullOrWhiteSpace())
            {
                WriteSecondary(seq, $"-{@switch.Letter}");
                WritePrimary(seq, ", ");
                WriteSecondary(seq, $"--{@switch.Word}");
            }
            else if (@switch.Letter.HasValue)
            {
                WriteSecondary(seq, $"-{@switch.Letter}");
            }
            else if (@switch.Word.IsNotNullOrWhiteSpace())
            {
                WriteSecondary(seq, $"--{@switch.Word}");
            }

            if (ViewModel.Parameter.MaxAllowed > 1) WriteSecondary(seq, $" {GenerateValueAlias(@switch)}");
            WritePrimary(seq, "]");
        }

        private string GenerateValueAlias(Parameter parameter)
        {
            var prefix = "v";
            if (parameter is Positional)
                prefix = "p";
            if (parameter.Help?.ValueAlias.IsNotNullOrWhiteSpace() ?? false) prefix = parameter.Help.ValueAlias;

            var hi = parameter.MaxAllowed == int.MaxValue ? "N" : $"{parameter.MaxAllowed}";
            if (parameter is Switch && parameter.MinRequired == 1) return "";
            if (parameter is Switch && parameter.MaxAllowed == 2) return prefix;

            if (parameter is Positional && parameter.MaxAllowed == 1) return prefix;

            return $"{prefix}1..{prefix}{hi}";
        }

        private void WritePrimary(IInlineSequence sequence, string text)
        {
            sequence.PushColor(ViewModel.Theme.DefaultTextColor);
            sequence.AppendText(text);
            StringBuilder.Append(text);
            sequence.PopFormatting();
        }

        private void WriteSecondary(IInlineSequence sequence, string text)
        {
            sequence.PushColor(ViewModel.Theme.SecondaryTextColor);
            sequence.AppendText(text);
            StringBuilder.Append(text);
            sequence.PopFormatting();
        }

        public override string GeneratedText => StringBuilder.ToString();
        public ParameterViewModel ViewModel { get; set; }
        protected internal StringBuilder StringBuilder { get; set; } = new StringBuilder();
    }
}