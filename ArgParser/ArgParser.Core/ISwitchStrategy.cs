using System.Collections.Generic;

namespace ArgParser.Core
{
    public interface ISwitchStrategy<T> : IParsingStrategy<T>
    {
        IterationInfo ConsumeSwitch(IList<Switch<T>> switches, T instasnce, IterationInfo info, ISwitchStrategy<T> parentStrategy = null);
        bool IsSwitch(IList<Switch<T>> switches, IterationInfo info, ISwitchStrategy<T> parentStrategy = null);
        bool IsGroup(IList<Switch<T>> switches, IterationInfo info, ISwitchStrategy<T> parentStrategy = null);
        IterationInfo ConsumeGroup(IList<Switch<T>> switches, T instance, IterationInfo info, ISwitchStrategy<T> parentStrategy = null);
    }
}