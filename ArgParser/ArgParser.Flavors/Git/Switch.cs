using ArgParser.Core;

namespace ArgParser.Flavors.Git
{
    public abstract class Switch : GitParameter
    {
        public override bool CanConsume(object instance, IIterationInfo info)
        {
            var cur = info.Current.ToGitToken();
            if (cur.Letter != null && cur.Letter == Letter)
                return true;
            if (cur.Word != null && cur.Word == Word)
                return true;
            return false;
        }

        public char? Letter { get; set; }
        public string Word { get; set; }
    }
}