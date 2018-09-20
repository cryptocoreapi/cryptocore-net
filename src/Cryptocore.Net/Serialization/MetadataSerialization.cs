using System.Collections.Generic;
using System.Linq;
using Newtonsoft.Json.Linq;

namespace Cryptocore.Net.Serialization
{
    public sealed class MetadataSerialization : IMetadataSerialization
    {
        public IEnumerable<string> DeserializeExchanges(string json)
        {
            var array = JArray.Parse(json);
            return array.Select(e => e["exchange_id"].ToString()).ToList();
        }

        public IEnumerable<Symbol> DeserializeSymbols(string json)
        {
            var array = JArray.Parse(json);
            return array.Select(e => e["symbol_id"].ToString())
                .Select(Symbol.FromString)
                .ToList();
        }
    }
}