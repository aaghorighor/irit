namespace Suftnet.Cos.DataAccess
{
   using System;

   public class TermDto
    {
       public int Id { get; set; }      
       public int ReportTypeId { get; set; }   
       public Guid OrderTypeId { get; set; }
       public int SubReportTypeId { get; set; }
       public string Status { get; set; }  
       public Guid StatusId { get; set; }             
       public DateTime StartDate { get; set; }
       public string UserName { get; set; }
       public string Cashier { get; set; }
       public int UserId { get; set; }
       public DateTime EndDate { get; set; }     
       public string Title { get; set; }       
       public Guid OrderId { get; set; }     
       public string Currency { get; set; }  
       public string EndDt { get; set; }
       public string StartDt { get; set; }
       public int AccountId { get; set; }
       public string Account { get; set; }
       public Guid TenantId { get; set; }

    }
}
