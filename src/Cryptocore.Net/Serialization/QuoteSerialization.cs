using Newtonsoft.Json.Linq;
using System;

namespace Cryptocore.Net.Serialization
{
    public sealed class QuoteSerialization : IQuoteSerialization
    {
        public Quote DeserializeQuote(string json)
        {
            var raw = JObject.Parse(json);
            //]
            return new Quote()
            {
                Timestamp = DateTime.Parse(raw["timestamp"].ToString()),
                Ask = decimal.Parse(raw["ask"].ToString()),
                AskSize = decimal.Parse(raw["ask_size"].ToString()),
                Bid = decimal.Parse(raw["bid"].ToString()),
                BidSize = decimal.Parse(raw["bid_size"].ToString()),
                Last = decimal.Parse(raw["last"].ToString()),
                LastSize = decimal.Parse(raw["last_size"].ToString())
            };
        }
    }
}