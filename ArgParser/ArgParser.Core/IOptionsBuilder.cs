namespace ArgParser.Core
{
    public interface IOptionsBuilder<out T>
    {
        T Build(string[] args);
    }
}