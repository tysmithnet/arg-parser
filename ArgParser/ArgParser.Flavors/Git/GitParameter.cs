using ArgParser.Core;

namespace ArgParser.Flavors.Git
{
    public abstract class GitParameter : IParameter
    {
        /// <inheritdoc />
        public abstract bool CanConsume(object instance, IIterationInfo info);

        /// <inheritdoc />
        public abstract IIterationInfo Consume(object instance, IIterationInfo info);

        /// <inheritdoc />
        public virtual void Reset()
        {
            HasBeenConsumed = false;
        }

        public abstract bool HasBeenConsumed { get; set; }
    }
}