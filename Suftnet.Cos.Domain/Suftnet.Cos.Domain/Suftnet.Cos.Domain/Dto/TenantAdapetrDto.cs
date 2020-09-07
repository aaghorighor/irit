namespace Suftnet.Cos.DataAccess
{
    using System.Collections.Generic;

    public class TenantAdapterDto
    {
        public AddressDto Address { get; set; }
        public UserAccountDto User { get; set; }
        public TenantDto Tenant { get; set; }      
        public string AutoLoginUrl { get; set; }
        public string LoginUrl { get; set; }
        public bool Flag { get; set; }
    }
}