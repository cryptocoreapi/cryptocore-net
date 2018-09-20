using System.Collections.Generic;

namespace Cryptocore.Net.Serialization
{
    public interface IMetadataSerialization
    {
        IEnumerable<string> DeserializeExchanges(string json);
        IEnumerable<Symbol> DeserializeSymbols(string json);
    }
}