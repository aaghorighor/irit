namespace Suftnet.Cos.Extension
{
    using Common;

    using Suftnet.Cos.Core;  
    using System;
    using System.Security.Claims;
    using System.Web.Mvc;    
    using System.Linq;

    public static class CurrencyFormatExtensions
    {
        private static System.Globalization.CultureInfo m_Ci
            = new System.Globalization.CultureInfo("en-US");        
              
        internal static string GetPattern(this decimal value)
        {
            var pattern = GeneralConfiguration.Configuration.Settings.FormatSettings.NormalPricePattern;
            if (value < GeneralConfiguration.Configuration.Settings.FormatSettings.SmallPriceLimit)
            {
                pattern = GeneralConfiguration.Configuration.Settings.FormatSettings.SmallPricePattern;
            }

            return string.Format(pattern, value);
        }      
        public static MvcHtmlString ToCurrency(this HtmlHelper helper, decimal amount)
        {           
            TagBuilder span = new TagBuilder("div");
            span.InnerHtml = Constant.DefaultHexCurrencySymbol + " " + amount.GetPattern();

            var test = ((ClaimsIdentity)helper.ViewContext.RequestContext.HttpContext.User.Identity);

            if (test != null)
            {
                var CurrencyCode = test.Claims.Where(x => x.Type == Identity.CurrencyCode).Select(x => x.Value).SingleOrDefault();

                if (!string.IsNullOrEmpty(CurrencyCode))
                {
                    span.InnerHtml = CurrencyCode + " " + amount.GetPattern();
                }               
            }           

            return new MvcHtmlString(span.ToString(TagRenderMode.Normal));
        }
        public static MvcHtmlString FormatNegativeCurrency(this HtmlHelper helper, decimal amount)
        {           
            TagBuilder span = new TagBuilder("span");
            span.InnerHtml = Constant.DefaultHexCurrencySymbol + " - " + amount.GetPattern();

            var test = ((ClaimsIdentity)helper.ViewContext.RequestContext.HttpContext.User.Identity);

            if (test != null)
            {
                var CurrencyCode = test.Claims.Where(x => x.Type == Identity.CurrencyCode).Select(x => x.Value).SingleOrDefault();

                if (!string.IsNullOrEmpty(CurrencyCode))
                {
                    span.InnerHtml = CurrencyCode + " " + amount.GetPattern();
                }
            }

            return new MvcHtmlString(span.ToString(TagRenderMode.Normal));
        }
        public static MvcHtmlString FormatCurrency(this HtmlHelper helper, decimal amount)
        {                     
            TagBuilder span = new TagBuilder("span");
            span.InnerHtml = Constant.DefaultHexCurrencySymbol + " " + amount.GetPattern();

            var test = ((ClaimsIdentity)helper.ViewContext.RequestContext.HttpContext.User.Identity);

            if (test != null)
            {
                var CurrencyCode = test.Claims.Where(x => x.Type == Identity.CurrencyCode).Select(x => x.Value).SingleOrDefault();

                if (!string.IsNullOrEmpty(CurrencyCode))
                {
                    span.InnerHtml = CurrencyCode + " " + amount.GetPattern();
                }
            }

            return new MvcHtmlString(span.ToString(TagRenderMode.Normal));
        }
        public static MvcHtmlString CurrencySymbol(this HtmlHelper helper)
        {                     
            TagBuilder span = new TagBuilder("span");
            span.InnerHtml = Constant.DefaultHexCurrencySymbol;
            var test = ((ClaimsIdentity)helper.ViewContext.RequestContext.HttpContext.User.Identity);

            if (test != null)
            {
                var CurrencyCode = test.Claims.Where(x => x.Type == Identity.CurrencyCode).Select(x => x.Value).SingleOrDefault();

                if (!string.IsNullOrEmpty(CurrencyCode))
                {
                    span.InnerHtml = CurrencyCode;
                }
            }           

            return new MvcHtmlString(span.ToString(TagRenderMode.Normal));
        }
        public static decimal? AmountToZeroDecimal(this HtmlHelper helper, decimal? amount)
        {           
            return amount.HasValue == true ? Math.Round(amount.Value, 0) : 0;
        }
        public static string ToCurrency(this Controller helper, decimal amount)
        {         
            string currencyFormat = string.Empty;

            var test = ((ClaimsIdentity)helper.HttpContext.User.Identity);

            if (test != null)
            {
                var CurrencyCode = test.Claims.Where(x => x.Type == Identity.CurrencyCode).Select(x => x.Value).SingleOrDefault();
              
                if (!string.IsNullOrEmpty(CurrencyCode))
                {
                    currencyFormat = CurrencyCode + " " + amount.GetPattern();
                }
                else
                {
                    currencyFormat = amount.GetPattern();
                }
            }          

            return currencyFormat;
        }
        public static MvcHtmlString Currency(this UrlHelper helper)
        {          
            TagBuilder span = new TagBuilder("div");
            span.InnerHtml = Constant.DefaultHexCurrencySymbol;

            var test = ((ClaimsIdentity)helper.RequestContext.HttpContext.User.Identity);

            if (test != null)
            {
                var CurrencyCode = test.Claims.Where(x => x.Type == Identity.CurrencyCode).Select(x => x.Value).SingleOrDefault();

                if (!string.IsNullOrEmpty(CurrencyCode))
                {
                    span.InnerHtml = CurrencyCode;
                }                
            }           

            return new MvcHtmlString(span.ToString(TagRenderMode.Normal));
        }
    }
}