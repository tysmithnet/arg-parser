using ArgParser.Core;

namespace ArgParser.Styles
{
    public interface IIterationInfoFactory
    {
        IterationInfo Create(IterationInfoRequest request);
    }
}