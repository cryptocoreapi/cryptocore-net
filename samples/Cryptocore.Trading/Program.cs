using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Cryptocore.Net;

namespace Cryptocore.Trading
{
    class Program
    {
        static void Main(string[] args)
        {
            var api = new CryptocoreApi();
            
            // get first symbol from Binance
            var symbol = api.GetSymbolsAsync("BINANCE").Result.First();
            Console.WriteLine($"Try to send order by: {symbol}");
            Console.WriteLine();

            // get quote
            var quote = api.GetLatestQuoteAsync(symbol).Result;
            
            // create user
            // pass api key
            var user = new CryptocoreApiUser("");

            // send order to exchange
            var order = api.PlaceAsync(user, symbol, OrderType.Limit, OrderSide.Buy, quote.Ask, 100).Result;

            Console.WriteLine($"Order Id: {order.Id}");
            Console.WriteLine();
            //
            Console.WriteLine("Press enter...");
            Console.ReadLine();
        }
    }
}
