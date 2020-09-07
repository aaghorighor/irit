namespace Suftnet.Cos.DataAccess
{
      using System;

    public class OrderPaymentDto : PaymentDto
    {
        public new Guid OrderId { get; set; }
        public Guid PaymentId { get; set; }        
    }

}

