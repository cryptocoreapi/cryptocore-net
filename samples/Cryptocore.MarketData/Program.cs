using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Cryptocore.Net;

namespace Cryptocore.MarketData
{
    class Program
    {
        static void Main(string[] args)
        {
            var api = new CryptocoreApi();
            //
            var symbols = api.GetSymbolsAsync("BINANCE").Result;
            Console.WriteLine("Symbols:");
            foreach (var s in symbols)
            {
                Console.WriteLine($"--> {s}");
            }
            Console.WriteLine();
            //
            var symbol = symbols.First();
            var quote = api.GetLatestQuoteAsync(symbol).Result;
            //
            Console.WriteLine($"Quote by {symbol}:");

            Console.WriteLine($"--> Timestamp: {quote.Timestamp}");
            Console.WriteLine($"--> Ask: {quote.Ask} ({quote.AskSize})");
            Console.WriteLine($"--> Bid: {quote.Bid} ({quote.BidSize})");
            Console.WriteLine($"--> Last: {quote.Last} ({quote.LastSize})");

            Console.WriteLine();
            //
            Console.WriteLine("Press enter...");
            Console.ReadLine();
        }
    }
}
