namespace Suftnet.Cos.DataAccess
{
    using System;
        
    public partial class DeliveryAddressDto : OrderDto
    {
        public Guid DeliveryId { get; set; }
        public Guid OrderId { get; set; }     
        public string Latitude { get; set; }  
        public string Logitude { get; set; }       
        public string AddressLine { get; set; }
        public string Duration { get; set; }
        public string Distance { get; set; }                
        public DateTime? CreatedAt { get; set; }        

    }
}
