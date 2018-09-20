using System;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;

namespace Cryptocore.Net
{
    public interface ICryptocoreApi
    {
        #region Metadata

        /// <summary>
        /// Get all supported exchanges
        /// </summary>
        /// <param name="token"></param>
        /// <returns></returns>
        Task<IEnumerable<string>> GetExchangesAsync(CancellationToken token = default);

        /// <summary>
        /// Get all symbols 
        /// </summary>
        /// <param name="exchange">Optional</param>
        /// <param name="token"></param>
        /// <returns></returns>
        Task<IEnumerable<Symbol>> GetSymbolsAsync(string exchange = "", CancellationToken token = default);

        #endregion

        #region Market Data

        /// <summary>
        /// Get latest candles
        /// </summary>
        /// <param name="symbol"></param>
        /// <param name="interval"></param>
        /// <param name="limit">Amount of items to return (optional, mininum is 1, maximum is 1000, default value is 100)</param>
        /// <param name="token"></param>
        /// <returns></returns>
        Task<IEnumerable<Candle>> GetLatestCandlesAsync(Symbol symbol, CandleInterval interval, int limit = 100,
            CancellationToken token = default);

        /// <summary>
        /// Get historical candles
        /// </summary>
        /// <param name="symbol"></param>
        /// <param name="interval"></param>
        /// <param name="start"></param>
        /// <param name="end">optional, if ncot supplied 100 elements will returned</param>
        /// <param name="token"></param>
        /// <returns></returns>
        Task<IEnumerable<Candle>> GetHistoricalCandlesAsync(Symbol symbol, CandleInterval interval, DateTime start,
            DateTime end = default, CancellationToken token = default);

        /// <summary>
        /// Get latest trades
        /// </summary>
        /// <param name="symbol"></param>
        /// <param name="limit">Amount of items to return (optional, mininum is 1, maximum is 1000, default value is 100)</param>
        /// <param name="token"></param>
        /// <returns></returns>
        Task<IEnumerable<Trade>>
            GetLatestTradesAsync(Symbol symbol, int limit = 100, CancellationToken token = default);

        /// <summary>
        /// Get latest quote
        /// </summary>
        /// <param name="symbol"></param>
        /// <param name="token"></param>
        /// <returns></returns>
        Task<Quote> GetLatestQuoteAsync(Symbol symbol, CancellationToken token = default);

        /// <summary>
        /// Get latest order book
        /// </summary>
        /// <param name="symbol"></param>
        /// <param name="token"></param>
        /// <returns></returns>
        Task<OrderBook> GetLatestOrderBookAsync(Symbol symbol, CancellationToken token = default);

        #endregion

        #region Account

        /// <summary>
        /// 
        /// </summary>
        /// <param name="user"></param>
        /// <param name="exchange"></param>
        /// <param name="token"></param>
        /// <returns></returns>
        Task<IEnumerable<Balance>> GetLatestBalancesAsync(ICryptocoreApiUser user, string exchange, CancellationToken token = default);

        #endregion

        #region Trading

        /// <summary>
        /// Place new order
        /// </summary>
        /// <param name="user"></param>
        /// <param name="symbol"></param>
        /// <param name="type"></param>
        /// <param name="side"></param>
        /// <param name="price"></param>
        /// <param name="quantity"></param>
        /// <param name="token"></param>
        /// <returns></returns>
        Task<Order> PlaceAsync(ICryptocoreApiUser user, Symbol symbol, OrderType type, OrderSide side, decimal price, decimal quantity,
            CancellationToken token = default);

        /// <summary>
        /// Cancel order
        /// </summary>
        /// <param name="user"></param>
        /// <param name="symbol"></param>
        /// <param name="orderId"></param>
        /// <param name="token"></param>
        /// <returns></returns>
        Task<Order> CancelAsync(ICryptocoreApiUser user, Symbol symbol, string orderId, CancellationToken token = default);

        /// <summary>
        /// Get order
        /// </summary>
        /// <param name="user"></param>
        /// <param name="exchange"></param>
        /// <param name="orderId"></param>
        /// <param name="token"></param>
        /// <returns></returns>
        Task<Order> GetOrderAsync(ICryptocoreApiUser user, string exchange, string orderId, CancellationToken token = default);

        /// <summary>
        /// Get active orders
        /// </summary>
        /// <param name="user"></param>
        /// <param name="exchange"></param>
        /// <param name="token"></param>
        /// <returns></returns>
        Task<IEnumerable<Order>> GetActiveOrdersAsync(ICryptocoreApiUser user, string exchange, CancellationToken token = default);

        /// <summary>
        /// 
        /// </summary>
        /// <param name="user"></param>
        /// <param name="exchange"></param>
        /// <param name="start"></param>
        /// <param name="end"></param>
        /// <param name="limit"></param>
        /// <param name="token"></param>
        /// <returns></returns>
        Task<IEnumerable<Order>> GetOrdersHistoryAsync(ICryptocoreApiUser user, string exchange, DateTime start, DateTime end = default, int limit = 100, CancellationToken token = default);

        #endregion
    }
}
