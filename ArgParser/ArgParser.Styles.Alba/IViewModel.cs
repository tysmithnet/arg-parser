using Alba.CsConsoleFormat;
using ArgParser.Core;

namespace ArgParser.Styles.Alba
{
    public interface IViewModel
    {
        IContext Context { get; }
        void Setup();
        Document Create();
    }
}