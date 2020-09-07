namespace Suftnet.Cos.Extension
{
    using Core;
    using System.Text;
    using System.Text.RegularExpressions;
    using System.Web.Mvc;

    public static class SEOHelper
    {
        public static string SEOUrlDecode(this string url)
        {
            StringBuilder sb = new StringBuilder(url);
            sb.Replace("?", "");
            sb.Replace("%", "");
            sb.Replace("@", "%40");
            sb.Replace("+", "%2b");
            sb.Replace("'", "%27");
            sb.Replace("/", "$");
            sb.Replace("\"", "$");
            sb.Replace("#", "%23");
            sb.Replace("&", "%26");
            sb.Replace(".", "");
            sb.Replace(":", "@");
            sb.Replace(";", "%3B");
            sb.Replace("=", "~");
            sb.Replace("&lt", "");
            sb.Replace("&gt", "");
            sb.Replace("<", "");
            sb.Replace(">", "");
            sb.Replace("*", "~");
            sb.Replace(" ", "+");
            sb.Replace("é", "%c3%a9");
            sb.Replace("è", "%c3%a8");
            sb.Replace("'", "27");
            sb.Replace(",", "%2c");
            sb.Replace("à", "%c3%a0");
            return System.Web.HttpUtility.UrlEncode(sb.ToString());
        }

        public static string SEOUrlEncode(this string url)
        {
            url = url.ToLower();
            url = url.Trim();
            url = AccentLess(url);
            // Suppression des doubles espaces
            while (true)
            {
                if (url.IndexOf("  ") == -1)
                {
                    break;
                }
                url = url.Replace("  ", " ");
            }
            var sb = new StringBuilder(url);
            sb.Replace(" / ", "/");
            sb.Replace(" /", "/");
            sb.Replace("/ ", "/");
            sb.Replace(" ", "_");
            sb.Replace("?", "");
            sb.Replace("%", "");
            //sb.Replace("@", "");
            // sb.Replace("+", "%2B");
            sb.Replace("\"", "");
            sb.Replace("#", "");
            sb.Replace("&", "");
            sb.Replace(".", "");
            sb.Replace(":", "");
            sb.Replace(";", "");
            // sb.Replace("=", "%3D");
            sb.Replace("&lt", "");
            sb.Replace("&gt", "");
            sb.Replace("<", "");
            sb.Replace(">", "");
            sb.Replace("*", "");
            sb.Replace("+", "");
            //sb.Replace("'", "%27");
            //sb.Replace(",", "%2C");
            var result = sb.ToString();
            result = result.Trim();
            result = result.TrimEnd('+');

            return result;
        }

        public static string AccentLess(this string input)
        {
            string accent = "ŠŒŽšœžŸ¢µÀÁÂÃÄÅÆÇÈÉÊËÌÍÎÏÐÑÒÓÔÕÖ×ØÙÚÛÜÝàáâãäåæçèéêëìíîïðñòóôõöùúûüý";
            string accentless = "SOZsozYcuAAAAAAACEEEEIIIIDNOOOOOxOUUUUYaaaaaaaceeeeiiiionooooouuuuy";

            for (int i = 0; i < accent.Length; i++)
            {
                if (input.IndexOf(accent[i]) > -1)
                {
                    input = input.Replace(accent[i], accentless[i]);
                }
            }

            return input;
        }

        public static string RemoveHTML(this string input)
        {
            if (string.IsNullOrEmpty(input))
            {
                return null;
            }
            string result = System.Text.RegularExpressions.Regex.Replace(input, "<[^>]*>", "");
            result = System.Text.RegularExpressions.Regex.Replace(result, @"&[\w#\d]+;", "");
            return result.Trim();
        }

        public static string FriendlyUrl(this string urlToEncode)
        {
            urlToEncode = (urlToEncode ?? "").Trim().ToLower();
            urlToEncode = Regex.Replace(urlToEncode, @"[^a-z0-9]", "-"); // invalid chars
            urlToEncode = Regex.Replace(urlToEncode, @"-+", "-").Trim(); // convert multiple dashes into one
            return urlToEncode;
        }

        public static string ToFriendlyUrl(this string urlToEncode
       )
        {
            urlToEncode = (urlToEncode ?? "").Trim().ToLower();

            StringBuilder url = new StringBuilder();

            foreach (char ch in urlToEncode)
            {
                switch (ch)
                {
                    case ' ':
                        url.Append('-');
                        break;
                    case '&':
                        url.Append("and");
                        break;
                    case '\'':
                        break;
                    default:
                        if ((ch >= '0' && ch <= '9') ||
                            (ch >= 'a' && ch <= 'z'))
                        {
                            url.Append(ch);
                        }
                        else
                        {
                            url.Append('-');
                        }
                        break;
                }
            }

            return url.ToString();
        }

        public static MvcHtmlString MetaDescription(this HtmlHelper helper, string decription)
        {
            if (decription == null)
            {
                decription += " : " + GeneralConfiguration.Configuration.Settings.General.Company;
            }
            return new MvcHtmlString(string.Format("<meta name=\"description\" content=\"{0}\" />", decription.Replace("\"", "''")));
        }

        public static MvcHtmlString MetaKeywords(this HtmlHelper helper, string keywords)
        {

            if (keywords == null)
            {
                keywords += " : " + GeneralConfiguration.Configuration.Settings.General.Company;
            }

            return new MvcHtmlString(string.Format("<meta name=\"keywords\" content=\"{0}\" />", keywords.Replace("\"", "''")));
        }
    }
}