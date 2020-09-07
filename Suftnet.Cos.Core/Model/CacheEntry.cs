namespace Suftnet.Cos.Core
{
    using System;

    public class CacheEntry
    {
        public CacheEntry()
        {
        }

        public string Key { get; set; }
        public object Value { get; set; }
        public DateTime ExpirationDate { get; set; }
    }
}
