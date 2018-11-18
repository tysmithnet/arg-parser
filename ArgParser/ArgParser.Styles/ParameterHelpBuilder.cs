﻿using ArgParser.Core;
using ArgParser.Core.Help;

namespace ArgParser.Styles
{
    public class ParameterHelpBuilder
    {
        public ParameterHelpBuilder(Parameter parameter)
        {
            Parameter = parameter.ThrowIfArgumentNull(nameof(parameter));
        }

        public ParameterHelp Build() => Help;

        public ParameterHelpBuilder SetDefaultValue(string value)
        {
            Parameter.Help.DefaultValue = value;
            return this;
        }

        public ParameterHelpBuilder SetName(string name)
        {
            Help.Name = name;
            return this;
        }

        public ParameterHelpBuilder SetShortDescription(string desc)
        {
            Help.ShortDescription = desc;
            return this;
        }

        public ParameterHelpBuilder SetValueAlias(string alias)
        {
            Help.ValueAlias = alias;
            return this;
        }

        protected internal ParameterHelp Help { get; set; } = new ParameterHelp();
        protected internal Parameter Parameter { get; set; }
    }
}