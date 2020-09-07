namespace Suftnet.Cos.Common
{
    using System;
    using System.Collections.Generic;
    using System.Globalization;     
  
    public static class Currency
    {
        public static int Default { get { return 265; } } // ₤
        public static CurrencyModel GetCurrencyInfo(string isoCurrency)
        {
            foreach (var c in CultureInfo.GetCultures(CultureTypes.SpecificCultures))
            {
                var reg = new RegionInfo(c.LCID);

                if (String.Compare(isoCurrency, reg.ISOCurrencySymbol, StringComparison.OrdinalIgnoreCase) == 0)
                {
                    var currency = new CurrencyModel
                    {
                        CurrencyEnglishName = reg.CurrencyEnglishName,
                        CurrencyNativeName = reg.CurrencyNativeName,
                        CurrencySymbol = reg.CurrencySymbol,
                        IsoCurrencySymbol = reg.ISOCurrencySymbol
                    };

                    return currency;
                }
            }

            return new CurrencyModel();
        }  
      
        public static Dictionary<string, string> Currencies = new Dictionary<string, string>
        {
            { "United Arab Emirates Dirham", "AED" },
            { "Afghan Afghani", "AFN" },
            { "Albanian Lek", "ALL" },
            { "Armenian Dram", "AMD" },
            { "Netherlands Antillean Gulden", "ANG" },
            { "Angolan Kwanza", "AOA" },
            { "Argentine Peso", "ARS" },
            { "Australian Dollar", "AUD" },
            { "Aruban Florin", "AWG" },
            { "Azerbaijani Manat", "AZN" },
            { "Bosnia And Herzegovina Convertible Mark", "BAM" },
            { "Barbadian Dollar", "BBD" },
            { "Bangladeshi Taka", "BDT" },
            { "Bulgarian Lev", "BGN" },
            { "Burundian Fran", "BIF" },
            { "Bermudian Dollar", "BMD" },
            { "Brunei Dollar", "BND" },
            { "Bolivian Boliviano", "BOB" },
            { "Brazilian Real", "BRL" },
            { "Bahamian Dollar", "BSD" },
            { "Botswana Pula", "BWP" },
            { "Belize Dollar", "BZD" },
            { "Canadian Dollar", "CAD" },
            { "Congolese Franc", "CDF" },
            { "Swiss Franc", "CHF" },
            { "Chilean Peso", "CLP" },
            { "Chinese Renminbi Yuan", "CNY" },
            { "Colombian Peso", "COP" },
            { "Costa Rican Colón", "CRC" },
            { "Cape Verdean Escudo", "CVE" },
            { "Czech Koruna", "CZK" },
            { "Djiboutian Franc", "DJF" },
            { "Danish Krone", "DKK" },
            { "Dominican Peso", "DOP" },
            { "Algerian Dinar", "DZD" },
            { "Estonian Kroon", "EEK" },
            { "Egyptian Pound", "EGP" },
            { "Ethiopian Birr", "ETB" },
            { "Euro", "EUR" },
            { "Fijian Dollar", "FJD" },
            { "Falkland Islands Pound", "FKP" },
            { "British Pound", "GBP" },
            { "Georgian Lari", "GEL" },
            { "Gibraltar Pound", "GIP" },
            { "Gambian Dalasi", "GMD" },
            { "Guinean Franc", "GNF" },
            { "Guatemalan Quetzal", "GTQ" },
            { "Guyanese Dollar", "GYD" },
            { "Hong Kong Dollar", "HKD" },
            { "Honduran Lempira", "HNL" },
            { "Croatian Kuna", "HRK" },
            { "Haitian Gourde", "HTG" },
            { "Hungarian Forint", "HUF" },
            { "Indonesian Rupiah", "IDR" },
            { "Israeli New Sheqel", "ILS" },
            { "Indian Rupee", "INR" },
            { "Icelandic Króna", "ISK" },
            { "Jamaican Dollar", "JMD" },
            { "Japanese Yen", "JPY" },
            { "Kenyan Shilling", "KES" },
            { "Kyrgyzstani Som", "KGS" },
            { "Cambodian Riel", "KHR" },
            { "Comorian Franc", "KMF" },
            { "South Korean Won", "KRW" },
            { "Cayman Islands Dollar", "KYD" },
            { "Kazakhstani Tenge", "KZT" },
            { "Lao Kip", "LAK" },
            { "Lebanese Pound", "LBP" },
            { "Sri Lankan Rupee", "LKR" },
            { "Liberian Dollar", "LRD" },
            { "Lesotho Loti", "LSL" },
            { "Lithuanian Litas", "LTL" },
            { "Latvian Lats", "LVL" },
            { "Moroccan Dirham", "MAD" },
            { "Moldovan Leu", "MDL" },
            { "Malagasy Ariary", "MGA" },
            { "Macedonian Denar", "MKD" },
            { "Mongolian Tögrög", "MNT" },
            { "Macanese Pataca", "MOP" },
            { "Mauritanian Ouguiya", "MRO" },
            { "Mauritian Rupee", "MUR" },
            { "Maldivian Rufiyaa", "MVR" },
            { "Malawian Kwacha", "MWK" },
            { "Mexican Peso", "MXN" },
            { "Malaysian Ringgit", "MYR" },
            { "Mozambican Metical", "MZN" },
            { "Namibian Dollar", "NAD" },
            { "Nigerian Naira", "NGN" },
            { "Nicaraguan Córdoba", "NIO" },
            { "Norwegian Krone", "NOK" },
            { "Nepalese Rupee", "NPR" },
            { "New Zealand Dollar", "NZD" },
            { "Panamanian Balboa", "PAB" },
            { "Peruvian Nuevo Sol", "PEN" },
            { "Papua New Guinean Kina", "PGK" },
            { "Philippine Peso", "PHP" },
            { "Pakistani Rupee", "PKR" },
            { "Polish Złoty", "PLN" },
            { "Paraguayan Guaraní", "PYG" },
            { "Qatari Riyal", "QAR" },
            { "Romanian Leu", "RON" },
            { "Serbian Dinar", "RSD" },
            { "Russian Ruble", "RUB" },
            { "Rwandan Franc", "RWF" },
            { "Saudi Riyal", "SAR" },
            { "Solomon Islands Dollar", "SBD" },
            { "Seychellois Rupee", "SCR" },
            { "Swedish Krona", "SEK" },
            { "Singapore Dollar", "SGD" },
            { "Saint Helenian Pound", "SHP" },
            { "Sierra Leonean Leone", "SLL" },
            { "Somali Shilling", "SOS" },
            { "Surinamese Dollar", "SRD" },
            { "São Tomé and Príncipe Dobra", "STD" },
            { "Salvadoran Colón", "SVC" },
            { "Swazi Lilangeni", "SZL" },
            { "Thai Baht", "THB" },
            { "Tajikistani Somoni", "TJS" },
            { "Tongan Paʻanga", "TOP" },
            { "Turkish Lira", "TRY" },
            { "Trinidad and Tobago Dollar", "TTD" },
            { "New Taiwan Dollar", "TWD" },
            { "Tanzanian Shilling", "TZS" },
            { "Ukrainian Hryvnia", "UAH" },
            { "Ugandan Shilling", "UGX" },
            { "United States Dollar", "USD" },
            { "Uruguayan Peso", "UYU" },
            { "Uzbekistani Som", "UZS" },
            { "Venezuelan Bolívar", "VEF" },
            { "Vietnamese Đồng", "VND" },
            { "Vanuatu Vatu", "VUV" },
            { "Samoan Tala", "WST" },
            { "Central African Cfa Franc", "XAF" },
            { "East Caribbean Dollar", "XCD" },
            { "West African Cfa Franc", "XOF" },
            { "Cfp Franc", "XPF" },
            { "Yemeni Rial", "YER" },
            { "South African Rand", "ZAR" },
        };
    } 
  
}
