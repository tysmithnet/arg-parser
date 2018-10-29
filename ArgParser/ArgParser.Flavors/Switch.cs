using ArgParser.Core;

namespace ArgParser.Flavors
{
    public abstract class Switch : IParameter
    {
        public virtual bool CanConsume(object instance, IIterationInfo info)
        {
            var cur = info.Current.ToGitToken();
            if (cur.Letter != null && cur.Letter == Letter)
                return true;
            if (cur.Word != null && cur.Word == Word)
                return true;
            return false;
        }

        /// <inheritdoc />
        public abstract IIterationInfo Consume(object instance, IIterationInfo info);

        public char Letter { get; set; }
        public string Word { get; set; }
    }
}