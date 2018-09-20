namespace Cryptocore.Net
{
    public interface ICryptocoreApiUserProvider
    {
        ICryptocoreApiUser CreateUser(string apiKey);
    }
}