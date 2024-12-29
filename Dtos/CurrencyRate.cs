namespace Money.Dtos
{
    public class CurrencyRate
    {
        public string Code { get; set; }
        public string Name { get; set; }
        public decimal RateFloat { get; set; }
        public string UpdatedTime { get; set; }
    }
}
