namespace Cryptocore.Net
{
    public static class OrderTypeExtension
    {
        public static string ConvertToString(this OrderType type)
        {
            switch (type)
            {
                case OrderType.Limit:
                    return "LIMIT";
                default:
                    return "unexpected";
            }
        }
    }
}