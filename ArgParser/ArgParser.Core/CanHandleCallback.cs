namespace ArgParser.Core
{
    public delegate bool CanHandleCallback<in T>(T instance, IIterationInfo info);
}