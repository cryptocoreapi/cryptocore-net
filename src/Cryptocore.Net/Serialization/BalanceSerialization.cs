using System.Collections.Generic;
using System.Linq;
using Newtonsoft.Json.Linq;

namespace Cryptocore.Net.Serialization
{
    public sealed class BalanceSerialization : IBalanceSerialization
    {
        public IEnumerable<Balance> DeserializeBalances(string json)
        {
            var array = JArray.Parse(json);
            return array.Select(ConvertToBalance).ToList();
        }

        private static Balance ConvertToBalance(JToken token)
        {
            return new Balance()
            {
                Currency = token["currency"].ToString(),
                Total = decimal.Parse(token["total"].ToString()),
                Available = decimal.Parse(token["available"].ToString())
            };
        }
    }
}