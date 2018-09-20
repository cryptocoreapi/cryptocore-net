using System;
using System.Collections.Generic;
using System.Linq;
using Newtonsoft.Json.Linq;

namespace Cryptocore.Net.Serialization
{
    public sealed class TradeSerialization : ITradeSerialization
    {
        public IEnumerable<Trade> DeserializeTrades(string json)
        {
            var array = JArray.Parse(json);
            return array.Select(e => new Trade()
            {
                Timestamp = DateTime.Parse(e["timestamp"].ToString()),
                Price = decimal.Parse(e["price"].ToString()),
                Quantity = decimal.Parse(e["size"].ToString())
            }).ToList();
        }
    }
}