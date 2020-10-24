namespace Suftnet.Cos.Common
{
    using System;
    using System.Collections.Generic;
    using System.Text;

    public static class Util
    {
        public static bool IsNullOrEmpty(this string sstring)
         {
             if (string.IsNullOrEmpty(sstring))
             {
                 return true;
             }

             return false;
         }

        public static decimal NullToZeroConverter(decimal? value)
        {
            if (value > 0)
            {
                return (decimal)value;
            }

            return 00;
        }

        public static decimal Balance(decimal? grandTotal, decimal? paymentPaid)
        {
            return NullToZeroConverter(grandTotal) - NullToZeroConverter(paymentPaid);
        }

        public static string JoinString(this List<string> lists)
        {
            StringBuilder stb = new StringBuilder();

            foreach (var list in lists)
            {
                stb.Append(list);
                stb.AppendLine();
            }

            return stb.ToString();
        }

        public static decimal NegativeDecimalToZero(this decimal value)
        {
            if (value < -1)
            {
                return 0;
            }

            return value;
        }

        public static string GetRandomPassword(int count = 10)
        {
            var random = new Random();
            var result = string.Empty;
            for (int i = 2; i < count - 1; i++)
            {
                var number = random.Next(9);
                result += number.ToString();
            }
            return result;
        }

        public static decimal GetPercentage(decimal percentage, decimal value)
        {
            if (percentage < 0 && value < 0)
            {
                return 0;
            }

            var tempValue = value / 100;
            var result = percentage * tempValue;

            return result;
        }

    }
}
