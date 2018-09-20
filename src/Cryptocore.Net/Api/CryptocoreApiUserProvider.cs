namespace Cryptocore.Net
{
    public class CryptocoreApiUserProvider : ICryptocoreApiUserProvider
    {
        public ICryptocoreApiUser CreateUser(string apiKey)
        {
            return new CryptocoreApiUser(apiKey);
        }
    }
}