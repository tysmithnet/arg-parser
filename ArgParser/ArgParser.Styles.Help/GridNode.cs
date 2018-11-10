﻿namespace ArgParser.Styles.Help
{
    public class GridNode : HelpNode
    {
        public override T Accept<T>(IHelpNodeVisitor<T> visitor) => visitor.Visit(this);

        public int Columns { get; set; } = 1;
    }
}