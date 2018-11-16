using System.Reflection;
using Alba.CsConsoleFormat;

namespace ArgParser.Styles.Alba
{
    public interface IElementFactory
    {
        TElement InflateTempalte<TElement>(string relativePath, object vm, params Assembly[] assemblies) where TElement : Element, new();
    }
}