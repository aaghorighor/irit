namespace Suftnet.Cos.DataAccess
{
    using System;
    public class TenantShortDto : TenantAddressDto
    {       
      public string Name { get; set; }
      public string Denomination { get; set; }    
        public string Address
        {
            get
            {
                var address = string.Empty;

                if (!string.IsNullOrWhiteSpace(CompleteAddress))
                {
                    address += CompleteAddress + ", ";
                }

                if (!string.IsNullOrWhiteSpace(Town))
                {
                    address += Town + ", ";
                }

                if (!string.IsNullOrWhiteSpace(County))
                {
                    address += County + ", ";
                }

                if (!string.IsNullOrWhiteSpace(PostCode))
                {
                    address += PostCode + ", ";
                }

                if (!string.IsNullOrWhiteSpace(Country))
                {
                    address += Country + ", ";
                }

                var lastIndex = address.LastIndexOf(",");

                if (lastIndex != -1)
                {
                    address = address.Substring(0, lastIndex);
                }

                return address;
            }
            set { }

        }
      public string LogoUrl { get; set; }
    }

    public class TenantShortModel 
    {
        public string Name { get; set; }
        public string Denomination { get; set; }
        public string Address { get; set; }
        public string ExternalId { get; set; }
        public string LogoUrl { get; set; }
    }
}
