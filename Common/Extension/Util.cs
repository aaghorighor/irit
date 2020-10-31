namespace Suftnet.Cos.Common
{
    using System;
    using System.Collections.Generic;
    using System.IO;
    using System.Text;
    using System.Xml;

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

        public static string Random(this string chars, int length = 8)
        {
            var randomString = new StringBuilder();
            var random = new Random();

            for (int i = 0; i < length; i++)
                randomString.Append(chars[random.Next(chars.Length)]);

            return randomString.ToString();
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

        public static string Serialize<T>(T item)
        {
            System.Xml.Serialization.XmlSerializer serializer = new System.Xml.Serialization.XmlSerializer(typeof(T));

            XmlWriterSettings settings = new XmlWriterSettings();
            settings.Encoding = Encoding.Unicode;
            settings.Indent = false;
            settings.OmitXmlDeclaration = false;
            settings.CheckCharacters = true;

            using (StringWriter textWriter = new StringWriter())
            {
                using (XmlWriter xmlWriter = XmlWriter.Create(textWriter, settings))
                {
                    serializer.Serialize(xmlWriter, item);
                }
                return textWriter.ToString();
            }
        }

        public static string SerializeListOfItem<T>(List<T> item)
        {
            System.Xml.Serialization.XmlSerializer serializer = new System.Xml.Serialization.XmlSerializer(typeof(List<T>));

            XmlWriterSettings settings = new XmlWriterSettings();
            settings.Encoding = Encoding.Unicode;
            settings.Indent = false;
            settings.OmitXmlDeclaration = false;
            settings.CheckCharacters = true;

            using (StringWriter textWriter = new StringWriter())
            {
                using (XmlWriter xmlWriter = XmlWriter.Create(textWriter, settings))
                {
                    serializer.Serialize(xmlWriter, item);
                }
                return textWriter.ToString();
            }
        }

        public static string Serializer(object obj)
        {
            System.Xml.XmlDocument xmlDoc = new System.Xml.XmlDocument();
            System.Xml.Serialization.XmlSerializer serializer = new System.Xml.Serialization.XmlSerializer(obj.GetType());
            using (System.IO.MemoryStream ms = new System.IO.MemoryStream())
            {
                serializer.Serialize(ms, obj);
                ms.Position = 0;
                xmlDoc.Load(ms);
                return xmlDoc.InnerXml;
            }
        }

    }
}
