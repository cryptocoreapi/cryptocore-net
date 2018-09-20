namespace Cryptocore.Net
{
    public static class StringExtensions
    {
        /// <summary>
        /// Return true if string is a JSON object.
        /// </summary>
        /// <param name="s"></param>
        /// <returns></returns>
        public static bool IsJsonObject(this string s)
        {
            return !string.IsNullOrWhiteSpace(s)
                   && s.StartsWith("{") && s.EndsWith("}");
        }
    }
}