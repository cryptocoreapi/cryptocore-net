using System;

namespace Cryptocore.Net
{
    public sealed class Symbol : IComparable<Symbol>, IEquatable<Symbol>
    {
        #region Properties

        public string Name => $"{BaseCurrency}-{QuotedCurrency}";

        public string FullName => $"{BaseCurrency}-{QuotedCurrency}-{Exchange}";

        public string BaseCurrency { get; set; }

        public string QuotedCurrency { get; set; }

        public string Exchange { get; set; }

        #endregion

        #region Implicit Operators

        public static bool operator ==(Symbol x, Symbol y) => Equals(x, y);

        public static bool operator !=(Symbol x, Symbol y) => !(x == y);

        public static implicit operator string(Symbol symbol) => symbol?.ToString();

        #endregion Implicit Operators
        
        #region Public Methods

        public override string ToString() => FullName;

        public override bool Equals(object obj)
        {
            if (obj == null)
                return false;

            if (obj is Symbol symbol)
                return Equals(symbol);

            return this.FullName.Equals(obj);
        }

        public override int GetHashCode()
        {
            return FullName.GetHashCode();
        }

        #endregion

        #region Static Methods

        public static Symbol FromString(string value)
        {
            var pieces = value.Split('-');
            if (pieces.Length != 3)
            {
                throw new InvalidCastException($"Incorrect value ({value})");
            }
            //
            return new Symbol()
            {
                BaseCurrency = pieces[0],
                QuotedCurrency = pieces[1],
                Exchange = pieces[2]
            };
        }

        #endregion

        #region IComparable<Symbol>

        public int CompareTo(Symbol other)
        {
            return other == null ? 1 : string.Compare(FullName, other.FullName, StringComparison.Ordinal);
        }

        #endregion

        #region IEquatable<Symbol>

        public bool Equals(Symbol other)
        {
            return CompareTo(other) == 0;
        }

        #endregion IEquatable<Symbol>
    }
}
