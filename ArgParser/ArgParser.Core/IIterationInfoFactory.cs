namespace ArgParser.Core
{
    public interface IIterationInfoFactory
    {
        IIterationInfo Create(string[] args);
    }
}