using System.Collections.Generic;

namespace Cryptocore.Net.Serialization
{
    public interface ITradeSerialization
    {
        IEnumerable<Trade> DeserializeTrades(string json);
    }
}