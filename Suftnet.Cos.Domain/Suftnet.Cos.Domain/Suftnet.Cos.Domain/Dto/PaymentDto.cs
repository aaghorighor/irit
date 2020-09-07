namespace Suftnet.Cos.DataAccess
{
    using System;

    [Serializable]
    public class PaymentDto : Base2Dto
    { 
        public string Reference { get; set; }
        public string PaymentMethod { get; set; }
        public Guid PaymentMethodId { get; set; }    
        public Guid OrderId { get; set; }
        public int AccountId { get; set; }      
        public string Account { get; set; }  
        public decimal Amount { get; set; }      
      
    }
}


