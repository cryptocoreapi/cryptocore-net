namespace Cryptocore.Net
{
    public static class CandleIntervalExtension
    {
        public static string ConvertToString(this CandleInterval interval)
        {
            switch (interval)
            {
                case CandleInterval.Minute:
                    return "1MIN";
                case CandleInterval.Minutes5:
                    return "5MIN";
                case CandleInterval.Minutes15:
                    return "15MIN";
                case CandleInterval.Minutes30:
                    return "30MIN";
                case CandleInterval.Hour:
                    return "1HR";
                case CandleInterval.Day:
                    return "1D";
                default:
                    return "unexpected";
            }
        }
    }
}