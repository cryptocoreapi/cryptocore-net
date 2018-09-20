using Newtonsoft.Json.Linq;
using System;
using System.Linq;

namespace Cryptocore.Net.Serialization
{
    public sealed class OrderBookSerialization : IOrderBookSerialization
    {
        public OrderBook DeserializeOrderBook(string json)
        {
            var raw = JObject.Parse(json);
            //]
            return new OrderBook()
            {
                Timestamp = DateTime.Parse(raw["timestamp"].ToString()),
                Asks = raw["asks"].Select(ConvertToOrderBookLevel).ToList(),
                Bids = raw["bids"].Select(ConvertToOrderBookLevel).ToList()
            };
        }

        private static OrderBookLevel ConvertToOrderBookLevel(JToken token)
        {
            return new OrderBookLevel()
            {
                Price = decimal.Parse(token["price"].ToString()),
                Size = decimal.Parse(token["size"].ToString())
            };
        }
    }
}