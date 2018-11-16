using System;
using System.Text;
using Alba.CsConsoleFormat;
using ArgParser.Core;

namespace ArgParser.Styles.Alba
{
    public class ParameterUsage : InlineElement
    {
        public Parameter Parameter { get; set; }
        public ConsoleColor? PrimaryColor { get; set; }
        public ConsoleColor? SecondaryColor { get; set; }
        public ConsoleColor? PrimaryBackground { get; set; }
        public ConsoleColor? SecondaryBackground { get; set; }
        protected internal StringBuilder StringBuilder { get; set; } = new StringBuilder();

        public override void GenerateSequence(IInlineSequence sequence)
        {
            if(Parameter is Switch @switch)
                GenerateSwitchSequence(@switch, sequence);
            else if(Parameter is Positional positional)
                GeneratePositionalSequence(positional, sequence);
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

            if (Parameter.MaxAllowed > 1)
            {
                WriteSecondary(seq, $" {GenerateValueAlias(@switch)}");
            }
            WritePrimary(seq, "]");
        }

        private void GeneratePositionalSequence(Positional positional, IInlineSequence seq)
        {
            WritePrimary(seq, "[");
            WriteSecondary(seq, GenerateValueAlias(positional));
            WritePrimary(seq, "]");
        }

        private string GenerateValueAlias(Parameter parameter)
        {
            string prefix = "v";
            if (parameter is Positional)
                prefix = "p";
            if (parameter.Help?.ValueAlias.IsNotNullOrWhiteSpace() ?? false)
            {
                prefix = parameter.Help.ValueAlias;
            }

            var hi = parameter.MaxAllowed == int.MaxValue ? "N" : $"{parameter.MaxAllowed}";
            if (parameter.MinRequired == 1)
            {
                return "";
            }
            if (parameter.MaxAllowed == 2)
            {
                return prefix;
            }

            return $"{prefix}1..{prefix}{hi}";
        }

        private void WritePrimary(IInlineSequence sequence, string text)
        {
            if(PrimaryColor.HasValue)
                sequence.PushColor(PrimaryColor.Value);
            if(PrimaryBackground.HasValue)
                sequence.PushColor(PrimaryBackground.Value);
            sequence.AppendText(text);
            StringBuilder.Append(text);
            if (PrimaryBackground.HasValue)
                sequence.PopFormatting();
            if (PrimaryColor.HasValue)
                sequence.PopFormatting();

        }

        private void WriteSecondary(IInlineSequence sequence, string text)
        {
            if (SecondaryColor.HasValue)
                sequence.PushColor(SecondaryColor.Value);
            if (SecondaryBackground.HasValue)
                sequence.PushColor(SecondaryBackground.Value);
            sequence.AppendText(text);
            StringBuilder.Append(text);
            if (SecondaryBackground.HasValue)
                sequence.PopFormatting();
            if (SecondaryColor.HasValue)
                sequence.PopFormatting();
        }

        public override string GeneratedText => StringBuilder.ToString();
    }
}