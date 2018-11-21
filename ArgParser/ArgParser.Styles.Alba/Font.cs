using System;
using System.Collections.Generic;
using System.IO;
using System.Text;

namespace ArgParser.Styles.Alba
{
    public abstract class Font
    {
        public abstract string Name { get; }
        public abstract string FileName { get; }

        public static readonly Font OneRow = new _1Row();

        private class _1Row : Font
        {
            public override string Name { get; } = "1Row";
            public override string FileName { get; } = "1row.flf";
        }

        private class _3D : Font
        {
            public override string Name { get; } = 
            public override string FileName { get; }
        }
    }
}
