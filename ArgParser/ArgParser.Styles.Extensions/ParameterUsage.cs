// ***********************************************************************
// Assembly         : ArgParser.Styles.Extensions
// Author           : @tysmithnet
// Created          : 11-17-2018
//
// Last Modified By : @tysmithnet
// Last Modified On : 11-18-2018
// ***********************************************************************
// <copyright file="ParameterUsage.cs" company="ArgParser.Styles.Extensions">
//     Copyright (c) . All rights reserved.
// </copyright>
// <summary></summary>
// ***********************************************************************

using System.Text;
using Alba.CsConsoleFormat;
using ArgParser.Core;

namespace ArgParser.Styles.Extensions
{
    /// <summary>
    ///     An Element that will render the usage for a Parameter
    /// </summary>
    /// <seealso cref="Alba.CsConsoleFormat.InlineElement" />
    public class ParameterUsage : InlineElement
    {
        /// <summary>
        ///     Generates the positional sequence.
        /// </summary>
        /// <param name="positional">The positional.</param>
        /// <param name="seq">The seq.</param>
        public virtual void GeneratePositionalSequence(Positional positional, IInlineSequence seq)
        {
            WritePrimary(seq, "[");
            WriteSecondary(seq, GenerateValueAlias(positional));
            WritePrimary(seq, "]");
        }

        /// <summary>
        ///     Generates the sequence.
        /// </summary>
        /// <param name="sequence">The sequence.</param>
        public override void GenerateSequence(IInlineSequence sequence)
        {
            if (ViewModel.Parameter is SeparatedSwitch separated)
                GenerateSeparatedSwitchSequence(separated, sequence);
            else if (ViewModel.Parameter is Switch @switch)
                GenerateSwitchSequence(@switch, sequence);
            else if (ViewModel.Parameter is Positional positional)
                GeneratePositionalSequence(positional, sequence);
        }

        public virtual void GenerateSeparatedSwitchSequence(SeparatedSwitch separated, IInlineSequence seq)
        {
            WritePrimary(seq, "[");
            if (separated.Letter.HasValue && separated.Word.IsNotNullOrWhiteSpace())
            {
                WriteSecondary(seq, $"-{separated.Letter}");
                WritePrimary(seq, ", ");
                WriteSecondary(seq, $"--{separated.Word}");
            }
            else if (separated.Letter.HasValue)
            {
                WriteSecondary(seq, $"-{separated.Letter}");
            }
            else if (separated.Word.IsNotNullOrWhiteSpace())
            {
                WriteSecondary(seq, $"--{separated.Word}");
            }

            WriteSecondary(seq, $":{GenerateValueAlias(separated)}");
            WritePrimary(seq, "]");
        }

        /// <summary>
        ///     Generates the switch sequence.
        /// </summary>
        /// <param name="switch">The switch.</param>
        /// <param name="seq">The seq.</param>
        public virtual void GenerateSwitchSequence(Switch @switch, IInlineSequence seq)
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

        /// <summary>
        ///     Generates the value alias.
        /// </summary>
        /// <param name="parameter">The parameter.</param>
        /// <returns>System.String.</returns>
        public virtual string GenerateValueAlias(Parameter parameter)
        {
            var prefix = "v";
            if (parameter is Positional)
                prefix = "p";
            if (parameter.Help?.ValueAlias.IsNotNullOrWhiteSpace() ?? false) prefix = parameter.Help.ValueAlias;
            if (parameter is SeparatedSwitch separated)
            {
                return prefix;
            }

            var sub = 1;
            if (parameter is Positional)
                sub = 0;
            var hi = parameter.MaxAllowed == int.MaxValue ? "N" : $"{parameter.MaxAllowed - sub}";
            if (parameter is Switch && parameter.MaxAllowed == 2) return prefix;
            if (parameter is Switch && parameter.MinRequired == 1 && parameter.MaxAllowed == 1) return "";

            if (parameter is Positional && parameter.MaxAllowed == 1) return prefix;

            return $"{prefix}1..{prefix}{hi}";
        }

        /// <summary>
        ///     Writes something using the primary color
        /// </summary>
        /// <param name="sequence">The sequence.</param>
        /// <param name="text">The text.</param>
        public virtual void WritePrimary(IInlineSequence sequence, string text)
        {
            sequence.PushColor(ViewModel.Theme.DefaultTextColor);
            sequence.AppendText(text);
            StringBuilder.Append(text);
            sequence.PopFormatting();
        }

        /// <summary>
        ///     Writes something using the secondary color
        /// </summary>
        /// <param name="sequence">The sequence.</param>
        /// <param name="text">The text.</param>
        public virtual void WriteSecondary(IInlineSequence sequence, string text)
        {
            sequence.PushColor(ViewModel.Theme.SecondaryTextColor);
            sequence.AppendText(text);
            StringBuilder.Append(text);
            sequence.PopFormatting();
        }

        /// <summary>
        ///     Gets the generated text.
        /// </summary>
        /// <value>The generated text.</value>
        public override string GeneratedText => StringBuilder.ToString();

        /// <summary>
        ///     Gets or sets the string builder.
        /// </summary>
        /// <value>The string builder.</value>
        public StringBuilder StringBuilder { get; set; } = new StringBuilder();

        /// <summary>
        ///     Gets or sets the view model.
        /// </summary>
        /// <value>The view model.</value>
        public ParameterViewModel ViewModel { get; set; }
    }
}