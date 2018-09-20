namespace Cryptocore.Net
{
    public static class OrderSideExtension
    {
        public static string ConvertToString(this OrderSide side)
        {
            switch (side)
            {
                case OrderSide.Buy:
                    return "BUY";
                case OrderSide.Sell:
                    return "SELL";
                default:
                    return "unexpected";
            }
        }
    }
}