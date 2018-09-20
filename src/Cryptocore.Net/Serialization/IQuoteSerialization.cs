namespace Cryptocore.Net.Serialization
{
    public interface IQuoteSerialization
    {
        Quote DeserializeQuote(string json);
    }
}