namespace Suftnet.Cos.DataAccess
{
    using System;
   
    public class RequestDto 
    {   
      public int IntervalCount { get; set; }      
      public string CustomerStripeId { get; set; }
      public bool Status { get; set; }
      public Guid StatusId { get; set; }
      public string CreatedBy { get; set; }
      public DateTime CreatedDt { get; set; }
      public DateTime EndDate { get; set; }
      public Guid TenantId { get; set; }
    }
}
