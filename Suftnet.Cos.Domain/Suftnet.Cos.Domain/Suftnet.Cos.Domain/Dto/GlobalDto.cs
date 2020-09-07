namespace Suftnet.Cos.DataAccess
{
    using System;

    public class GlobalDto : AddressDto
    {          
        public decimal? TaxRate { get; set; }       
        public int? CurrencyId { get; set; }
        public string CurrencySymbol { get; set; }
        public string DateTimeFormat { get; set; }
        public string Email { get; set; }
        public string ServerEmail { get; set; } 
        public string Server { get; set; }
        public string Password { get; set; }
        public string UserName { get; set; }
        public string Company { get; set; }
        public string Telephone { get; set; }
        public string Mobile { get; set; }       
        public string Description { get; set; }         
        public int? Port { get; set; }          
   
    }
}
