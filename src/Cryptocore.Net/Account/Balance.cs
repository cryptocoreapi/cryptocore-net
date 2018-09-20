namespace Cryptocore.Net
{
    public class Balance
    {
        public string Currency { get; set; }

        public decimal Total { get; set; }

        public decimal Available { get; set; }
        
        public override string ToString()
        {
            return $"{Currency} Total:{Total} Available:{Available}";
        }
    }
}