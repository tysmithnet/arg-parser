using System;
using System.Collections.Generic;
using System.Text;

namespace ArgParser.Core
{
    public interface IFlavor : ILexer, IParser, IParseStrategy
    {

    }

    public interface IFlavor<TOptions> : ILexer, IParser<TOptions>, IParseStrategy<TOptions>
    {

    }

    public interface IFlavor<out TToken, TOptions> : ILexer<TToken>, IParser<TOptions>, IParseStrategy<TOptions> where TToken : IToken
    {

    }
}
