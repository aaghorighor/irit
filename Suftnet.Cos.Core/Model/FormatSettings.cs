namespace Suftnet.Cos.Core
{
    public class FormatSettings
    {
        public FormatSettings()
        {
            SmallPricePattern = "{0:#,####0.0000}";
            NormalPricePattern = "{0:#,##0.00}";
            CurrencySymbol = " £";
            SmallPriceLimit = 1;
        }

        public string SmallPricePattern { get; set; }
        public string NormalPricePattern { get; set; }
        public string CurrencySymbol { get; set; }
        public decimal SmallPriceLimit { get; set; }
    }
}
