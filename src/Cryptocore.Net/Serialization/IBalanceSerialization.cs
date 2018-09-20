using System.Collections.Generic;

namespace Cryptocore.Net.Serialization
{
    public interface IBalanceSerialization
    {
        IEnumerable<Balance> DeserializeBalances(string json);
    }
}