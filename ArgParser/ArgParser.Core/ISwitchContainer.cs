namespace ArgParser.Core
{
    public interface ISwitchContainer<out T>
    {
        void AddSwitch(ISwitch<T> svitch);
    }
}