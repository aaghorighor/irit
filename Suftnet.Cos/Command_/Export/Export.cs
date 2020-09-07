namespace Suftnet.Cos.Web.Command
{

    using Core;
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Web;

    public abstract class Export : IDisposable, IExport
    {
        public abstract int TenantId { get; set; }
        protected abstract string Name { get; }
        protected abstract void CreateDocument();
        protected abstract void SaveDocument();   
        protected string QuoteString(string input)
        {
            if (input == null)
            {
                return string.Empty;
            }
            var result = input.Replace("\"", "\"\"");

            return result;
        }
        protected string ToCurrency(decimal value)
        {
            return ToCurrency(value, false);
        }
        protected string ToCurrency(decimal value, bool hideSymbol)
        {
            var result = ToCurrencyInternal(value, hideSymbol);
            return System.Web.HttpUtility.HtmlDecode(result);
        }
        private string ToCurrencyInternal(decimal value, bool hideSymbol)
        {
            string symbol = GeneralConfiguration.Configuration.Settings.FormatSettings.CurrencySymbol;
            if (hideSymbol)
            {
                symbol = string.Empty;
            }
            var pattern = GetPattern(value);
            return string.Format(pattern, value, symbol);
        }
        private string GetPattern(decimal value)
        {
            var pattern = GeneralConfiguration.Configuration.Settings.FormatSettings.NormalPricePattern + "{1}";
            if (value < GeneralConfiguration.Configuration.Settings.FormatSettings.SmallPriceLimit)
            {
                pattern = GeneralConfiguration.Configuration.Settings.FormatSettings.SmallPricePattern + "{1}";
            }
            return pattern;
        }

        #region IDisposable Members

        public abstract void Dispose();

        #endregion
    }
}