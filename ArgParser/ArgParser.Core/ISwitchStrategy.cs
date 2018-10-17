using System.Collections.Generic;

namespace ArgParser.Core
{
    public interface ISwitchStrategy<T> : IParsingStrategy<T>
    {
        IterationInfo ConsumeSwitch(IList<TokenSwitch<T>> switches, T instasnce, IterationInfo info);
        bool IsSwitch(IList<TokenSwitch<T>> switches, IterationInfo info);
        bool IsGroup(IList<TokenSwitch<T>> switches, IterationInfo info);
        IterationInfo ConsumeGroup(IList<TokenSwitch<T>> switches, T instance, IterationInfo info);
    }
}