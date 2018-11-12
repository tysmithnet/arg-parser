using System.Collections.Generic;
using System.Linq;
using Alba.CsConsoleFormat;

namespace ArgParser.Styles.Help
{
    public static class ColumnLengthExtensions
    {
        public static IEnumerable<ColumnLength> ToAutoColumns(this int numAutoColumns) =>
            Enumerable.Repeat(new ColumnLength(), numAutoColumns);

        public static GridLength ToGridLength(this ColumnLength length)
        {
            if (length.NumStars == 0)
                return GridLength.Auto;
            return GridLength.Star(length.NumStars);
        }
    }
}