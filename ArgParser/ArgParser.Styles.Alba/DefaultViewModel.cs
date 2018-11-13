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

        public string Title { get; set; } = "Program";
        public Visibility TitleVisibility { get; set; } = Visibility.Visible;
        public string SubTitle { get; set; } = "A program that does something";
        public Visibility SubTitleVisibility { get; set; } = Visibility.Visible;
        public IEnumerable<Switch> OwnSwitches { get; set; }
    }
}
