using System;
using System.Collections.Generic;
using System.Text;

namespace ArgParser.Core
{
    public interface IContext
    {
        IParserRepository ParserRepository { get; }
        IHierarchyRepository HierarchyRepository { get; }
    }
}
