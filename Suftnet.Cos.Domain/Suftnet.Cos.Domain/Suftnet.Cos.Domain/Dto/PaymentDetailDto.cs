namespace Suftnet.Cos.DataAccess
{
      using System;

      public class PaymentDetailDto
        {
          public decimal AmountPaid { get; set; }        
          public int PaymentMethodId { get; set; }            
          public string PaymentMethod { get; set; }       
          public string Reference { get; set; }        
          public int Id { get; set; }
          public int PaymentId { get; set; }          
          public DateTime CreatedDT { get; set; }       
          public string CreatedOn { get; set; } 
          public string CreatedBy { get; set; }
        }
}
