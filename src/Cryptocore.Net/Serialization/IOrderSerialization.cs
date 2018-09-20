using System.Collections.Generic;

namespace Cryptocore.Net.Serialization
{
    public interface IOrderSerialization
    {
        Order DeserializeOrder(string json);
        IEnumerable<Order> DeserializeOrders(string json);
    }
}