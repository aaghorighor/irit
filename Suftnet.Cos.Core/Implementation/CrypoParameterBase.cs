namespace Suftnet.Cos.Core
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;
    using System.Security.Cryptography;

    internal class CrypoParameterBase
    {
        SymmetricCryptography<System.Security.Cryptography.RC2CryptoServiceProvider> m_Crypto;

        public CrypoParameterBase(byte[] cryptoKey, byte[] cryptoIV)
        {
            m_Crypto = new SymmetricCryptography<System.Security.Cryptography.RC2CryptoServiceProvider>(cryptoKey, cryptoIV);
        }

        public string Encrypt()
        {
            var properties = from property
                             in this.GetType().GetProperties(System.Reflection.BindingFlags.Instance | System.Reflection.BindingFlags.Public)
                             select new
                             {
                                 Name = property.Name,
                                 Value = property.GetValue(this, null),
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
            result = result.Substring(0, result.Length - 1);
            result = result.Replace("=", "%3D");
            result = result.Replace("+", "%20");
            return result;
        }

        public static T Decrypt<T>(string encrypted, byte[] cryptoKey, byte[] cryptoIV)
             where T : CrypoParameterBase, new()
        {
            var crypto = new SymmetricCryptography<System.Security.Cryptography.RC2CryptoServiceProvider>(cryptoKey, cryptoIV);

            encrypted = encrypted.Replace(" ", "+");
            encrypted = encrypted.Replace("%3D", "=");
            encrypted = encrypted + "=";
            string input = crypto.Decrypt(encrypted);

            string[] token = input.Split('|');

            // On recupere le premier token pour connaitre le type
            T ticket = new T();

            System.Reflection.PropertyInfo[] properties = ticket.GetType().GetProperties(System.Reflection.BindingFlags.Public | System.Reflection.BindingFlags.Instance);
            for (int i = 0; i < properties.Length; i++)
            {
                System.Reflection.PropertyInfo pi = properties[i];
                object o = null;
                if (pi.PropertyType == typeof(DateTime))
                {
                    string d = token[i];
                    int year = 2000 + Convert.ToInt32(d.Substring(0, 2));
                    int month = Convert.ToInt32(d.Substring(2, 2));
                    int day = Convert.ToInt32(d.Substring(4, 2));
                    o = new DateTime(year, month, day);
                }
                else
                {
                    o = System.Convert.ChangeType(token[i], pi.PropertyType);
                }
                properties[i].SetValue(ticket, o, null);
            }

            return ticket;
        }
    }

}
