using System;
using System.Collections.Generic;
using System.Linq;

namespace ArgParser.Core
{
    public class IterationInfo : IEquatable<IterationInfo>, IComparable<IterationInfo>, IComparable
    {
        public IterationInfo(string[] args, int index = 0)
        {
            Args = args.ThrowIfArgumentNull(nameof(args));
            Index = index;
        }

        public int CompareTo(IterationInfo other)
        {
            if (ReferenceEquals(this, other)) return 0;
            if (ReferenceEquals(null, other)) return 1;
            if (!Args.SequenceEqual(other.Args))
                throw new InvalidOperationException($"Both IterationInfo instances must have the same args to compare");
            return Index.CompareTo(other.Index);
        }

        public int CompareTo(object obj)
        {
            if (ReferenceEquals(null, obj)) return 1;
            if (ReferenceEquals(this, obj)) return 0;
            if (!(obj is IterationInfo)) throw new ArgumentException($"Object must be of type {nameof(IterationInfo)}");
            return CompareTo((IterationInfo) obj);
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
                var hashCode = (Index + 1) % 31337;
                hashCode = Args.Aggregate(hashCode, (i, s) => i ^= s.GetHashCode());
                return hashCode;
            }
        }

        public static bool operator ==(IterationInfo left, IterationInfo right) => Equals(left, right);

        public static bool operator >(IterationInfo left, IterationInfo right) =>
            Comparer<IterationInfo>.Default.Compare(left, right) > 0;

        public static bool operator >=(IterationInfo left, IterationInfo right) =>
            Comparer<IterationInfo>.Default.Compare(left, right) >= 0;

        public static bool operator !=(IterationInfo left, IterationInfo right) => !Equals(left, right);

        public static bool operator <(IterationInfo left, IterationInfo right) =>
            Comparer<IterationInfo>.Default.Compare(left, right) < 0;

        public static bool operator <=(IterationInfo left, IterationInfo right) =>
            Comparer<IterationInfo>.Default.Compare(left, right) <= 0;

        public string[] Args { get; protected internal set; }

        public string Current => Args[Index];

        public int Index { get; set; }
    }
}