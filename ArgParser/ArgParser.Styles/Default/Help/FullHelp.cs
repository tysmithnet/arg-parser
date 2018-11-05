﻿using System.Collections.Generic;

namespace ArgParser.Styles.Default.Help
{
    public class FullHelp : SimpleHelp
    {
        public string LongDescription { get; set; }
        public IList<Example> Examples { get; set; } = new List<Example>();
    }
}