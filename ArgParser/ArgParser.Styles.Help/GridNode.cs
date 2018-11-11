using System.Collections.Generic;
using System.Linq;
using Alba.CsConsoleFormat;

namespace ArgParser.Styles.Help
{
    public class GridNode : HelpNode
    {
        public override T Accept<T>(IHelpNodeVisitor<T> visitor) => visitor.Visit(this);

        public IList<ColumnLength> Columns { get; set; } = new List<ColumnLength>();
    }

    public class ColumnLength
    {
        public int NumStars { get; set; }

        public ColumnLength(int numStars = 0)
        {
            NumStars = numStars;
        }
    }

    public static class ColumnLengthExtensions
    {
        public static GridLength ToGridLength(this ColumnLength length)
        {
            if(length.NumStars == 0)
                return GridLength.Auto;
            return GridLength.Star(length.NumStars);
        }

        public static IEnumerable<ColumnLength> ToAutoColumns(this int numAutoColumns)
        {
            return Enumerable.Repeat(new ColumnLength(), numAutoColumns);
        }
    }
}