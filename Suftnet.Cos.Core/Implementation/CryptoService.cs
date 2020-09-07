namespace Suftnet.Cos.Core
{
    using System;
    using System.Collections.Generic;
    using System.Linq;

    public class CryptoService : ICrytoService
    {
        SymmetricCryptography<System.Security.Cryptography.RC2CryptoServiceProvider> m_Crypto;

        public CryptoService()
        {
            m_Crypto = new SymmetricCryptography<System.Security.Cryptography.RC2CryptoServiceProvider>();
        }

       
        public string EncryptString(string data)
        {
            var ticket = new { key = data };
            var result = Encrypt(ticket);
            return result;
        }

        public string DecryptString(string data)
        {
            var ticket = new { Key = data };
            var result = Decrypt(data, ticket);
            return result[0].ToString();
        }

        #region Helper

        public string Encrypt(object subject)
        {
            var properties = from property
                             in subject.GetType().GetProperties(System.Reflection.BindingFlags.Instance | System.Reflection.BindingFlags.Public)
                             select new
                             {
                                 Name = property.Name,
                                 Value = property.GetValue(subject, null),
                                 Type = property.PropertyType
                             };
            string separtator = "";
            string result = null;
            foreach (var item in properties)
            {
                string pattern = "{0}{1}";
                if (item.Type == typeof(DateTime))
                {
                    pattern = "{0}{1:yyMMdd}";
                }
                result += string.Format(pattern, separtator, item.Value);
                separtator = "|";
            }
            result = m_Crypto.Encrypt(result);
            return result; // System.Web.HttpUtility.UrlEncode(result);
        }
        public List<object> Decrypt(string encrypted, object subject)
        {
            if (string.IsNullOrEmpty(encrypted))
            {
                return null;
            }
            // encrypted = System.Web.HttpUtility.UrlDecode(encrypted);
            string input = m_Crypto.Decrypt(encrypted);

            string[] token = input.Split('|');

            var properties = subject.GetType().GetProperties(System.Reflection.BindingFlags.Public | System.Reflection.BindingFlags.Instance);
            var result = new List<object>();
            for (int i = 0; i < properties.Length; i++)
            {
                var pi = properties[i];
                object o = null;
                if (pi.PropertyType == typeof(DateTime))
                {
                    string d = token[i];
                    int year = 2000 + Convert.ToInt32(d.Substring(0, 2));
                    int month = Convert.ToInt32(d.Substring(2, 2));
                    int day = Convert.ToInt32(d.Substring(4, 2));
                    o = new DateTime(year, month, day);
                }
                else if (pi.PropertyType == typeof(bool)
                    && token[i] == "1")
                {
                    o = token[i] == "1" ? "True" : "False";
                }
                else
                {
                    o = System.Convert.ChangeType(token[i], pi.PropertyType);
                }
                result.Add(o);
                if (properties[i].CanWrite)
                {
                    properties[i].SetValue(subject, o, null);
                }
            }
            return result;
        }

        #endregion

    }
}
