﻿namespace ArgParser.Core.Help
{
    public class Example : SimpleHelp
    {
        public string Usage { get; set; }
        public string Result { get; set; }
    }

    public interface IHelpNodeFactory
    {
        RootNode Create(IContext context, string parserId);
    }
}