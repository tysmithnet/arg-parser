using ArgParser.Core;

namespace ArgParser.Flavors.Git
{
    public abstract class GitParameter : IParameter
    {
            
        public abstract bool CanConsume(object instance, IIterationInfo info);

            
        public abstract IIterationInfo Consume(object instance, IIterationInfo info);

            
        public virtual void Reset()
        {
            HasBeenConsumed = false;
        }

        public abstract bool HasBeenConsumed { get; set; }
    }
}