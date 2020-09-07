namespace Suftnet.Cos.DataAccess
{
    using System;
    using System.Collections.Generic;

    public class PaymentAdapterDto
    {
        public PaymentAdapterDto()
        {
            Payments = new List<PaymentDto>();            
        }
     
       public int OrderId { get; set; }
       public decimal Total { get; set; }
       public decimal AmountPaid { get; set; }
       public decimal Balance { get; set; }
       public PaymentDto Payment { get; set; }  
       public List<PaymentDto> Payments { get; set; }   
    }   

}
