using System;
using System.Collections.Generic;
using System.Text;

namespace ArgParser.Core
{
    public interface IFlavor : ILexer, IParser, IParseStrategy
    {
        IParseResult Parse(string[] args);
    }

    public interface IFlavor<TOptions> : IFlavor, ILexer, IParser<TOptions>, IParseStrategy<TOptions>
    {

    }

    public interface IFlavor<out TToken, TOptions> : ILexer<TToken>, IParser<TOptions>, IParseStrategy<TOptions> where TToken : IToken
    {

    }
}
