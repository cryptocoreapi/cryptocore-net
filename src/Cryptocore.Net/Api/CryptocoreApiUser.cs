using System;

namespace Cryptocore.Net
{
    public class CryptocoreApiUser : ICryptocoreApiUser, IEquatable<ICryptocoreApiUser>
    {
        public string ApiKey { get; }

        public CryptocoreApiUser(string apiKey)
        {
            ApiKey = apiKey;
        }
        
        public bool Equals(ICryptocoreApiUser user)
        {
            if (user == null)
                return false;

            if (ReferenceEquals(this, user))
                return true;

            return user.ApiKey == ApiKey;
        }

        public override bool Equals(object obj)
            => Equals(obj as ICryptocoreApiUser);

        public override int GetHashCode()
        {
            return ApiKey.GetHashCode();
        }

        public override string ToString()
        {
            return ApiKey;
        }
    }
}