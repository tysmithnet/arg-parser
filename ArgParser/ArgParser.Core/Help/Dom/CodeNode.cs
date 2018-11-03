﻿using System.Collections.Generic;
using System.Linq;
using ArgParser.Core.Help.Dom;

namespace ArgParser.Core.Help
{
    public class CodeNode : TextNode
    {
            
        public CodeNode(string text) : base(text)
        {
        }

    }

    public class GridNode : HelpNode
    {
        public int Columns { get; set; }
        public override T Accept<T>(IHelpNodeVisitor<T> visitor)
        {
            return visitor.Visit(this);
        }

        public new IEnumerable<GridCell> Children => base.Children.Cast<GridCell>();
    }

    public class GridCell : HelpNode
    {
        public override T Accept<T>(IHelpNodeVisitor<T> visitor)
        {
            return visitor.Visit(this);
        }
    }
}