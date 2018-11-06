namespace ArgParser.Core
{
    public interface IInstanceInspector
    {
        void Inspect(object instance, IContext context);
    }

    public interface IInstanceInspector<in T> : IInstanceInspector
    {
        void Inspect(T instance, IContext context);
    }
}