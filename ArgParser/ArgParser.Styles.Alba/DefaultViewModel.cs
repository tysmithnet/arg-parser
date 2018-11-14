using System;
using System.Collections.Generic;
using System.Text;
using Alba.CsConsoleFormat;
using ArgParser.Core;

namespace ArgParser.Styles.Alba
{
    public class DefaultViewModel : IViewModel
    {
        public IContext Context { get; protected internal set; }

        /// <inheritdoc />
        public void Setup()
        {
            Title = Context.HierarchyRepository.GetRoot();
        }

        public string Title { get; set; } = "Program";
        public Visibility TitleVisibility { get; set; } = Visibility.Visible;
        public string SubTitle { get; set; } = "A program that does something";
        public Visibility SubTitleVisibility { get; set; } = Visibility.Visible;
        public IEnumerable<Switch> OwnSwitches { get; set; }

        public IEnumerable<Parser> SubCommandChain { get; set; } = new List<Parser>()
        {
            new Parser("a"),
            new Parser("b"),
            new Parser("c")
        };

        public static string ToUpper(string value)
        {
            return value?.ToUpper();
        }
    }
}
