using System;
using System.Collections.Generic;
using System.Linq;
using Newtonsoft.Json.Linq;

namespace Cryptocore.Net.Serialization
{
    public sealed class CandleSerialization : ICandleSerialization
    {
        public IEnumerable<Candle> DeserializeCandles(string json)
        {
            var array = JArray.Parse(json);
            return array.Select(ConvertToCandle).ToList();
        }

        private static Candle ConvertToCandle(JToken token)
        {
            return new Candle()
            {
                Timestamp = DateTime.Parse(token["timestamp"].ToString()),
                Open = decimal.Parse(token["open"].ToString()),
                High = decimal.Parse(token["high"].ToString()),
                Low = decimal.Parse(token["low"].ToString()),
                Close = decimal.Parse(token["close"].ToString()),
                Volume = decimal.Parse(token["volume"].ToString())
            };
        }
    }
}