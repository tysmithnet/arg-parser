namespace ArgParser.Core
{
    public interface IParser<in T>
    {
        bool CanHandle<TSub>(TSub instance, IIterationInfo info) where TSub : T;
        IIterationInfo Handle<TSub>(TSub instance, IIterationInfo info) where TSub : T;
    }

    public interface IParser<in T, in TBase> : IParser<T> where T : TBase
    {
        IParser<TBase> BaseParser { get; }
    }
}