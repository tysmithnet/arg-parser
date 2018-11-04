using System;
using System.Linq;

namespace ArgParser.Core
{
    public class IterationInfo : IEquatable<IterationInfo>
    {
        public IterationInfo(string[] args, int index)
        {
            Args = args ?? throw new ArgumentNullException(nameof(args));
            Index = index;
        }

        public IterationInfo Consume(int num) => new IterationInfo(Args, Index + num);

        public bool Equals(IterationInfo other)
        {
            if (ReferenceEquals(null, other)) return false;
            if (ReferenceEquals(this, other)) return true;
            return Index == other.Index && Args.SequenceEqual(other.Args);
        }

        public override bool Equals(object obj)
        {
            if (ReferenceEquals(null, obj)) return false;
            if (ReferenceEquals(this, obj)) return true;
            if (obj.GetType() != GetType()) return false;
            return Equals((IterationInfo) obj);
        }

        public override int GetHashCode()
        {
            unchecked
            {
                int hashCode = (Index + 1) % 31337;
                hashCode = Args.Aggregate(hashCode, (i, s) => i ^= s.GetHashCode());
                return hashCode;
            }
        }

        public static bool operator ==(IterationInfo left, IterationInfo right) => Equals(left, right);

        public static bool operator !=(IterationInfo left, IterationInfo right) => !Equals(left, right);
        public string[] Args { get; protected internal set; }

        public string Current => Args[Index];

        public int Index { get; set; }
    }
}