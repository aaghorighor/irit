namespace Suftnet.Cos.DataAccess
{
    using System;
    using System.ComponentModel.DataAnnotations;

    public class AddressDto : BaseDto
    {
        public new int Id { get; set; }
        public int AddressId { get; set; }       
        public string AddressLine1 { get; set; }
        public string AddressLine2 { get; set; }
        public string AddressLine3 { get; set; }
        public string County { get; set; }
        public string CompleteAddress { get; set; }       
        public string Country { get; set; }
        public string Longitude { get; set; }
        public string Latitude { get; set; }       
        public string PostCode { get; set; }
        public string Town { get; set; }
        public bool Active { get; set; }

        public string AddressLine
        {
            get
            {
                var address = string.Empty;

                if (!string.IsNullOrWhiteSpace(AddressLine1))
                {
                    address += AddressLine1 + ", ";
                }

                if (!string.IsNullOrWhiteSpace(AddressLine2))
                {
                    address += AddressLine2 + ", ";
                }

                if (!string.IsNullOrWhiteSpace(AddressLine3))
                {
                    address += AddressLine3 + ", ";
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
            private set { this.AddressLine = value; }
        
        }

        public string FullAddress
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
        }
    }
}