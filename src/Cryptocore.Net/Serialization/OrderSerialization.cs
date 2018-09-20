using System;
using System.Collections.Generic;
using System.Linq;
using Newtonsoft.Json.Linq;

namespace Cryptocore.Net.Serialization
{
    public sealed class OrderSerialization : IOrderSerialization
    {
        public Order DeserializeOrder(string json)
        {
            var raw = JObject.Parse(json);
            return ConvertToOrder(raw);
        }

        public IEnumerable<Order> DeserializeOrders(string json)
        {
            var array = JArray.Parse(json);
            return array.Select(ConvertToOrder).ToList();
        }

        private static Order ConvertToOrder(JToken token)
        {
            return new Order()
            {
                Id = token["order_id"].ToString(),
                Symbol = Symbol.FromString(token["symbol_id"].ToString()),
                CreationTime = DateTime.Parse(token["creation_time"].ToString()),
                Price = decimal.Parse(token["price"].ToString()),
                Quantity = decimal.Parse(token["quantity"].ToString()),
                ExecutedQuantity = decimal.Parse(token["executed_quantity"].ToString()),
                Type = ConvertToOrderType(token["type"].ToString()),
                Side = ConvertToOrderSide(token["side"].ToString()),
                Status = ConvertToOrderStatus(token["status"].ToString())
            };
        }

        private static OrderType ConvertToOrderType(string value)
        {
            switch (value)
            {
                case "LIMIT":
                    return OrderType.Limit;
            }
            //
            throw new InvalidCastException($"Incorrect Type parameter ({value})");
        }

        private static OrderSide ConvertToOrderSide(string value)
        {
            switch (value)
            {
                case "BUY":
                    return OrderSide.Buy;
                case "SELL":
                    return OrderSide.Sell;
            }
            //
            throw new InvalidCastException($"Incorrect Side parameter ({value})");
        }

        private static OrderStatus ConvertToOrderStatus(string value)
        {
            switch (value)
            {
                case "NEW":
                    return OrderStatus.New;
                case "FILLED":
                    return OrderStatus.Filled;
                case "CANCELLED":
                    return OrderStatus.Cancelled;
                case "PARTIALLY_FILLED":
                    return OrderStatus.PartiallyFilled;
                case "REJECTED":
                    return OrderStatus.Rejected;
            }
            //
            throw new InvalidCastException($"Incorrect Status parameter ({value})");
        }
    }
}