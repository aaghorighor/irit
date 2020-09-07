namespace Suftnet.Cos.Common
{
    using System;
    using System.Collections.Generic;
    using System.Text;

    public static class Extension
    {
        public static bool IsNullOrEmpty(this string sstring)
         {
             if (string.IsNullOrEmpty(sstring))
             {
                 return true;
             }

             return false;
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
    }
}
