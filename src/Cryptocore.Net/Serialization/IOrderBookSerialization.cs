namespace Cryptocore.Net.Serialization
{
    public interface IOrderBookSerialization
    {
        OrderBook DeserializeOrderBook(string json);
    }
}