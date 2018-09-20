using System.Collections.Generic;

namespace Cryptocore.Net.Serialization
{
    public interface ICandleSerialization
    {
        IEnumerable<Candle> DeserializeCandles(string json);
    }
}