using System.Collections.Generic;

namespace ArgParser.Styles.Help
{
    public class GridNode : HelpNode
    {
        public override T Accept<T>(IHelpNodeVisitor<T> visitor) => visitor.Visit(this);

        public IList<ColumnLength> Columns { get; set; } = new List<ColumnLength>();
    }
}