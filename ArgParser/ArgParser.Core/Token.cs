using System;

namespace ArgParser.Core
{
    public class Token : IToken, IEquatable<Token>
    {
        /// <inheritdoc />
        public bool Equals(Token other)
        {
            if (ReferenceEquals(null, other)) return false;
            if (ReferenceEquals(this, other)) return true;
            return string.Equals(Raw, other.Raw);
        }

        /// <inheritdoc />
        public override bool Equals(object obj)
        {
            if (ReferenceEquals(null, obj)) return false;
            if (ReferenceEquals(this, obj)) return true;
            if (obj.GetType() != this.GetType()) return false;
            return Equals((Token) obj);
        }

        /// <inheritdoc />
        public override int GetHashCode() => (Raw != null ? Raw.GetHashCode() : 0);

        /// <inheritdoc />
        public Token(string raw)
        {
            Raw = raw ?? throw new ArgumentNullException(nameof(raw));
        }

        /// <inheritdoc />
        public string Raw { get; }
    }
}