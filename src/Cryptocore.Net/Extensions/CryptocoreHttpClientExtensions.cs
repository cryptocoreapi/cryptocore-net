using System;
using System.Threading;
using System.Threading.Tasks;

namespace Cryptocore.Net
{
    public static class CryptocoreHttpClientExtensions
    {
        #region Metadata

        /// <summary>
        /// </summary>
        /// <param name="client"></param>
        /// <param name="token"></param>
        /// <returns></returns>
        public static async Task<string> GetExchangesAsync(this ICryptocoreHttpClient client,
            CancellationToken token = default)
        {
            Throw.IfNull(client, nameof(client));

            return await client.GetAsync("/v1/data/exchanges", token)
                .ConfigureAwait(false);
        }

        /// <summary>
        /// </summary>
        /// <param name="client"></param>
        /// <param name="exchange"></param>
        /// <param name="token"></param>
        /// <returns></returns>
        public static async Task<string> GetSymbolsAsync(this ICryptocoreHttpClient client, string exchange = null,
            CancellationToken token = default)
        {
            Throw.IfNull(client, nameof(client));

            var request = new CryptocoreHttpRequest("/v1/data/symbols");
            
            if (!string.IsNullOrEmpty(exchange))
            {
                request.AddParameter("exchange_id", exchange);
            }

            return await client.GetAsync(request, token)
                .ConfigureAwait(false);
        }

        #endregion
        
        #region Market Data

        /// <summary>
        /// 
        /// </summary>
        /// <param name="client"></param>
        /// <param name="symbol"></param>
        /// <param name="interval"></param>
        /// <param name="start"></param>
        /// <param name="end"></param>
        /// <param name="token"></param>
        /// <returns></returns>
        public static async Task<string> GetHistoricalCandlesAsync(this ICryptocoreHttpClient client, Symbol symbol,
            CandleInterval interval, DateTime start, DateTime end = default,
            CancellationToken token = default)
        {
            Throw.IfNull(client, nameof(client));
            Throw.IfNull(symbol, nameof(symbol));

            var request = new CryptocoreHttpRequest($"/v1/data/ohlcv/{symbol}/history");

            request.AddParameter("period_id", interval.ConvertToString());
            request.AddParameter("time_start", start.ToTimestamp());

            if (end != default)
            {
                request.AddParameter("time_end", end.ToTimestamp());
            }
            
            return await client.GetAsync(request, token)
                .ConfigureAwait(false);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="client"></param>
        /// <param name="symbol"></param>
        /// <param name="interval"></param>
        /// <param name="limit"></param>
        /// <param name="token"></param>
        /// <returns></returns>
        public static async Task<string> GetLatestCandlesAsync(this ICryptocoreHttpClient client, Symbol symbol,
            CandleInterval interval, int limit = 100, CancellationToken token = default)
        {
            Throw.IfNull(client, nameof(client));
            Throw.IfNull(symbol, nameof(symbol));

            var request = new CryptocoreHttpRequest($"/v1/data/ohlcv/{symbol}/latest");

            request.AddParameter("period_id", interval.ConvertToString());

            if (limit > 0)
            {
                request.AddParameter("limit", limit);
            }

            return await client.GetAsync(request, token)
                .ConfigureAwait(false);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="client"></param>
        /// <param name="symbol"></param>
        /// <param name="token"></param>
        /// <returns></returns>
        public static async Task<string> GetLatestOrderBookAsync(this ICryptocoreHttpClient client, Symbol symbol,
            CancellationToken token = default)
        {
            Throw.IfNull(client, nameof(client));
            Throw.IfNull(symbol, nameof(symbol));
            
            return await client.GetAsync($"/v1/data/orderbooks/{symbol}/latest", token)
                .ConfigureAwait(false);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="client"></param>
        /// <param name="symbol"></param>
        /// <param name="token"></param>
        /// <returns></returns>
        public static async Task<string> GetLatestQuoteAsync(this ICryptocoreHttpClient client, Symbol symbol,
            CancellationToken token = default)
        {
            Throw.IfNull(client, nameof(client));
            Throw.IfNull(symbol, nameof(symbol));

            return await client.GetAsync($"/v1/data/quotes/{symbol}/latest", token)
                .ConfigureAwait(false);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="client"></param>
        /// <param name="symbol"></param>
        /// <param name="limit"></param>
        /// <param name="token"></param>
        /// <returns></returns>
        public static async Task<string> GetLatestTradesAsync(this ICryptocoreHttpClient client, Symbol symbol,
            int limit = 100, CancellationToken token = default)
        {
            Throw.IfNull(client, nameof(client));
            Throw.IfNull(symbol, nameof(symbol));

            var request = new CryptocoreHttpRequest($"/v1/data/trades/{symbol}/latest");

            if (limit > 0)
            {
                request.AddParameter("limit", limit);
            }

            return await client.GetAsync(request, token)
                .ConfigureAwait(false);
        }

        #endregion

        #region Account

        /// <summary>
        /// 
        /// </summary>
        /// <param name="client"></param>
        /// <param name="user"></param>
        /// <param name="exchange"></param>
        /// <param name="token"></param>
        /// <returns></returns>
        public static async Task<string> GetLatestBalancesAsync(this ICryptocoreHttpClient client,
            ICryptocoreApiUser user, string exchange, CancellationToken token = default)
        {
            Throw.IfNull(client, nameof(client));
            Throw.IfNull(user, nameof(user));

            var request = new CryptocoreHttpRequest($"/v1/account/balances/{exchange}/latest")
            {
                ApiKey = user.ApiKey
            };

            return await client.GetAsync(request, token)
                .ConfigureAwait(false);
        }

        #endregion

        #region Trading 

        /// <summary>
        /// 
        /// </summary>
        /// <param name="client"></param>
        /// <param name="user"></param>
        /// <param name="exchange"></param>
        /// <param name="token"></param>
        /// <returns></returns>
        public static async Task<string> GetActiveOrdersAsync(this ICryptocoreHttpClient client,
            ICryptocoreApiUser user, string exchange, CancellationToken token = default)
        {
            Throw.IfNull(client, nameof(client));
            Throw.IfNull(user, nameof(user));
            Throw.IfNullOrWhiteSpace(exchange, nameof(exchange));

            var request = new CryptocoreHttpRequest($"/v1/orders/{exchange}/active")
            {
                ApiKey = user.ApiKey
            };

            return await client.GetAsync(request, token)
                .ConfigureAwait(false);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="client"></param>
        /// <param name="user"></param>
        /// <param name="exchange"></param>
        /// <param name="orderId"></param>
        /// <param name="token"></param>
        /// <returns></returns>
        public static async Task<string> GetOrderAsync(this ICryptocoreHttpClient client, ICryptocoreApiUser user,
            string exchange, string orderId, CancellationToken token = default)
        {
            Throw.IfNull(client, nameof(client));
            Throw.IfNull(user, nameof(user));
            Throw.IfNullOrWhiteSpace(exchange, nameof(exchange));
            Throw.IfNullOrWhiteSpace(orderId, nameof(orderId));

            var request = new CryptocoreHttpRequest($"/v1/orders/{exchange}/{orderId}")
            {
                ApiKey = user.ApiKey
            };

            return await client.GetAsync(request, token)
                .ConfigureAwait(false);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="client"></param>
        /// <param name="user"></param>
        /// <param name="exchange"></param>
        /// <param name="start"></param>
        /// <param name="end"></param>
        /// <param name="limit"></param>
        /// <param name="token"></param>
        /// <returns></returns>
        public static async Task<string> GetOrdersHistoryAsync(this ICryptocoreHttpClient client,
            ICryptocoreApiUser user, string exchange, DateTime start, DateTime end = default, int limit = 100,
            CancellationToken token = default)
        {
            Throw.IfNull(client, nameof(client));
            Throw.IfNull(user, nameof(user));
            Throw.IfNullOrWhiteSpace(exchange, nameof(exchange));

            var request =
                new CryptocoreHttpRequest($"/v1/orders/{exchange}/history")
                {
                    ApiKey = user.ApiKey
                };

            request.AddParameter("time_start", start.ToTimestamp());

            if (end != default)
            {
                request.AddParameter("time_end", end.ToTimestamp());
            }

            if (limit > 0)
            {
                request.AddParameter("limit", limit);
            }

            return await client.GetAsync(request, token)
                .ConfigureAwait(false);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="client"></param>
        /// <param name="user"></param>
        /// <param name="symbol"></param>
        /// <param name="orderId"></param>
        /// <param name="token"></param>
        /// <returns></returns>
        public static async Task<string> CancelAsync(this ICryptocoreHttpClient client, ICryptocoreApiUser user, Symbol symbol, string orderId,
            CancellationToken token = default)
        {
            Throw.IfNull(client, nameof(client));
            Throw.IfNull(user, nameof(user));
            Throw.IfNull(symbol, nameof(symbol));
            Throw.IfNullOrWhiteSpace(orderId, nameof(orderId));

            var request = new CryptocoreHttpRequest($"/v1/orders/{symbol.Exchange}/{symbol}/active/{orderId}")
            {
                ApiKey = user.ApiKey
            };

            return await client.DeleteAsync(request, token)
                .ConfigureAwait(false);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="client"></param>
        /// <param name="user"></param>
        /// <param name="symbol"></param>
        /// <param name="type"></param>
        /// <param name="side"></param>
        /// <param name="price"></param>
        /// <param name="quantity"></param>
        /// <param name="token"></param>
        /// <returns></returns>
        public static async Task<string> PlaceAsync(this ICryptocoreHttpClient client, ICryptocoreApiUser user,
            Symbol symbol, OrderType type, OrderSide side, decimal price, decimal quantity,
            CancellationToken token = default)
        {
            Throw.IfNull(client, nameof(client));
            Throw.IfNull(user, nameof(user));
            Throw.IfNull(symbol, nameof(symbol));

            var request = new CryptocoreHttpRequest($"/v1/orders/{symbol.Exchange}/{symbol}")
            {
                ApiKey = user.ApiKey
            };

            request.AddParameter("type", type.ConvertToString());
            request.AddParameter("side", side.ConvertToString());
            request.AddParameter("price", price);
            request.AddParameter("quantity", quantity);

            return await client.PostAsync(request, token)
                .ConfigureAwait(false);
        }

        #endregion
    }
}