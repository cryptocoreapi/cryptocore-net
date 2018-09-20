using Cryptocore.Net.Serialization;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;

namespace Cryptocore.Net
{
    public class CryptocoreApi : ICryptocoreApi
    {
        #region Public Properties

        /// <summary>
        /// Get the low-level <see cref="ICryptocoreHttpClient"/> singleton.
        /// </summary>
        public ICryptocoreHttpClient HttpClient { get; }

        #endregion Public Properties

        #region Private Fields

        private readonly IMetadataSerialization _metadataSerialization;
        private readonly ICandleSerialization _candleSerialization;
        private readonly IOrderBookSerialization _orderBookSerialization;
        private readonly IQuoteSerialization _quoteSerialization;
        private readonly ITradeSerialization _tradeSerialization;
        private readonly IBalanceSerialization _balanceSerialization;
        private readonly IOrderSerialization _orderSerialization;
        private readonly ILogger<CryptocoreApi> _logger;

        #endregion Private Fields

        #region Constructors

        /// <summary>
        /// 
        /// </summary>
        public CryptocoreApi()
            : this(CryptocoreHttpClient.Instance)
        { }


        public CryptocoreApi(ICryptocoreHttpClient client, IMetadataSerialization metadataSerialization = null,
            ICandleSerialization candleSerialization = null, IOrderBookSerialization orderBookSerialization = null,
            IQuoteSerialization quoteSerialization = null, ITradeSerialization tradeSerialization = null,
            IBalanceSerialization balanceSerialization = null, IOrderSerialization orderSerialization = null,
            ILogger<CryptocoreApi> logger = null)
        {
            Throw.IfNull(client, nameof(client));

            _metadataSerialization = metadataSerialization ?? new MetadataSerialization();
            _candleSerialization = candleSerialization ?? new CandleSerialization();
            _orderBookSerialization = orderBookSerialization ?? new OrderBookSerialization();
            _quoteSerialization = quoteSerialization ?? new QuoteSerialization();
            _tradeSerialization = tradeSerialization ?? new TradeSerialization();
            _balanceSerialization = balanceSerialization ?? new BalanceSerialization();
            _orderSerialization = orderSerialization ?? new OrderSerialization();

            HttpClient = client;

            _logger = logger;
        }

        #endregion

        #region Metadata

        public async Task<IEnumerable<string>> GetExchangesAsync(CancellationToken token = default)
        {
            var json = await HttpClient.GetExchangesAsync(token)
                .ConfigureAwait(false);

            try
            {
                return _metadataSerialization.DeserializeExchanges(json);
            }
            catch (Exception e)
            {
                throw NewFailedToParseJsonException(nameof(GetExchangesAsync), json, e);
            }
        }

        public async Task<IEnumerable<Symbol>> GetSymbolsAsync(string exchange = "", CancellationToken token = default)
        {
            var json = await HttpClient.GetSymbolsAsync(exchange, token)
                .ConfigureAwait(false);

            try
            {
                return _metadataSerialization.DeserializeSymbols(json);
            }
            catch (Exception e)
            {
                throw NewFailedToParseJsonException(nameof(GetExchangesAsync), json, e);
            }
        }

        #endregion

        #region Market Data

        public async Task<IEnumerable<Candle>> GetHistoricalCandlesAsync(Symbol symbol, CandleInterval interval, DateTime start, DateTime end = default, CancellationToken token = default)
        {
            var json = await HttpClient.GetHistoricalCandlesAsync(symbol, interval, start, end, token)
                .ConfigureAwait(false);

            try
            {
                return _candleSerialization.DeserializeCandles(json);
            }
            catch (Exception e)
            {
                throw NewFailedToParseJsonException(nameof(GetExchangesAsync), json, e);
            }
        }

        public async Task<IEnumerable<Candle>> GetLatestCandlesAsync(Symbol symbol, CandleInterval interval, int limit = 100, CancellationToken token = default)
        {
            var json = await HttpClient.GetLatestCandlesAsync(symbol, interval, limit, token)
                .ConfigureAwait(false);

            try
            {
                return _candleSerialization.DeserializeCandles(json);
            }
            catch (Exception e)
            {
                throw NewFailedToParseJsonException(nameof(GetExchangesAsync), json, e);
            }
        }

        public async Task<OrderBook> GetLatestOrderBookAsync(Symbol symbol, CancellationToken token = default)
        {
            var json = await HttpClient.GetLatestOrderBookAsync(symbol, token)
                .ConfigureAwait(false);

            try
            {
                return _orderBookSerialization.DeserializeOrderBook(json);
            }
            catch (Exception e)
            {
                throw NewFailedToParseJsonException(nameof(GetExchangesAsync), json, e);
            }
        }

        public async Task<Quote> GetLatestQuoteAsync(Symbol symbol, CancellationToken token = default)
        {
            var json = await HttpClient.GetLatestQuoteAsync(symbol, token)
                .ConfigureAwait(false);

            try
            {
                return _quoteSerialization.DeserializeQuote(json);
            }
            catch (Exception e)
            {
                throw NewFailedToParseJsonException(nameof(GetExchangesAsync), json, e);
            }
        }

        public async Task<IEnumerable<Trade>> GetLatestTradesAsync(Symbol symbol, int limit = 100, CancellationToken token = default)
        {
            var json = await HttpClient.GetLatestTradesAsync(symbol, limit, token)
                .ConfigureAwait(false);

            try
            {
                return _tradeSerialization.DeserializeTrades(json);
            }
            catch (Exception e)
            {
                throw NewFailedToParseJsonException(nameof(GetExchangesAsync), json, e);
            }
        }

        #endregion

        #region Account

        public async Task<IEnumerable<Balance>> GetLatestBalancesAsync(ICryptocoreApiUser user, string exchange, CancellationToken token = default)
        {
            var json = await HttpClient.GetLatestBalancesAsync(user, exchange, token)
                .ConfigureAwait(false);

            try
            {
                return _balanceSerialization.DeserializeBalances(json);
            }
            catch (Exception e)
            {
                throw NewFailedToParseJsonException(nameof(GetExchangesAsync), json, e);
            }
        }

        #endregion

        #region Trading

        public async Task<Order> CancelAsync(ICryptocoreApiUser user, Symbol symbol, string orderId, CancellationToken token = default)
        {
            var json = await HttpClient.CancelAsync(user, symbol, orderId, token)
                .ConfigureAwait(false);

            try
            {
                return _orderSerialization.DeserializeOrder(json);
            }
            catch (Exception e)
            {
                throw NewFailedToParseJsonException(nameof(GetExchangesAsync), json, e);
            }
        }

        public async Task<IEnumerable<Order>> GetActiveOrdersAsync(ICryptocoreApiUser user, string exchange, CancellationToken token = default)
        {
            var json = await HttpClient.GetActiveOrdersAsync(user, exchange, token)
                .ConfigureAwait(false);

            try
            {
                return _orderSerialization.DeserializeOrders(json);
            }
            catch (Exception e)
            {
                throw NewFailedToParseJsonException(nameof(GetExchangesAsync), json, e);
            }
        }

        public async Task<Order> GetOrderAsync(ICryptocoreApiUser user, string exchange, string orderId, CancellationToken token = default)
        {
            var json = await HttpClient.GetOrderAsync(user, exchange, orderId, token)
                .ConfigureAwait(false);

            try
            {
                return _orderSerialization.DeserializeOrder(json);
            }
            catch (Exception e)
            {
                throw NewFailedToParseJsonException(nameof(GetExchangesAsync), json, e);
            }
        }

        public async Task<IEnumerable<Order>> GetOrdersHistoryAsync(ICryptocoreApiUser user, string exchange,
            DateTime start, DateTime end = default, int limit = 100, CancellationToken token = default)
        {
            var json = await HttpClient.GetOrdersHistoryAsync(user, exchange, start, end, limit, token)
                .ConfigureAwait(false);

            try
            {
                return _orderSerialization.DeserializeOrders(json);
            }
            catch (Exception e)
            {
                throw NewFailedToParseJsonException(nameof(GetExchangesAsync), json, e);
            }
        }

        public async Task<Order> PlaceAsync(ICryptocoreApiUser user, Symbol symbol, OrderType type, OrderSide side, decimal price, decimal quantity, CancellationToken token = default)
        {
            var json = await HttpClient.PlaceAsync(user, symbol, type, side, price, quantity, token)
                .ConfigureAwait(false);

            try
            {
                return _orderSerialization.DeserializeOrder(json);
            }
            catch (Exception e)
            {
                throw NewFailedToParseJsonException(nameof(GetExchangesAsync), json, e);
            }
        }

        #endregion

        #region Private Methods

        /// <summary>
        /// Throw exception when JSON parsing fails.
        /// </summary>
        /// <param name="methodName"></param>
        /// <param name="json"></param>
        /// <param name="e"></param>
        /// <returns></returns>
        private CryptocoreApiException NewFailedToParseJsonException(string methodName, string json, Exception e)
        {
            var message = $"{nameof(CryptocoreApi)}.{methodName} failed to parse JSON api response: \"{json}\"";

            _logger?.LogError(e, message);

            return new CryptocoreApiException(message, e);
        }

        #endregion
    }
}