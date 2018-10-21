namespace ArgParser.Core
{
    public delegate IIterationInfo HandlerCallback<in T>(T instance, IIterationInfo info);
}