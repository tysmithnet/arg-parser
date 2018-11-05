namespace ArgParser.Core
{
    public interface IInstanceInspector
    {
        void Inspect(object instance, IContext context);
    }
}