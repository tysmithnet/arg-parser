using System.Collections.Generic;

namespace ArgParser.Core
{
    public interface ISwitchStrategy<T> : IParsingStrategy<T>
    {
        IterationInfo ConsumeSwitch(IList<Switch<T>> switches, T instasnce, IterationInfo info);
        bool IsSwitch(IList<Switch<T>> switches, IterationInfo info);
        bool IsGroup(IList<Switch<T>> switches, IterationInfo info);
        IterationInfo ConsumeGroup(IList<Switch<T>> switches, T instance, IterationInfo info);
    }
}