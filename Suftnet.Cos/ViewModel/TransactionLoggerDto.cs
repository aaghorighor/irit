namespace Suftnet.Cos.Web
{
    using System;

    public class TransactionLoggerDto 
    {
        public int Id { get; set; }
        public int? UserId { get; set; }
        public int? ProductId { get; set; }
        public int? StatusId { get; set; }
        public string Processor { get; set; }
        public DateTime? CreatedDT { get; set; }
        public string CreatedOn { get; set; }
        public string CreatedBy { get; set; }
        public string Product { get; set; }
        public string UserName { get; set; }
        public string Email { get; set; }
        public string PlanId { get; set; }
        public string Plan { get; set; }
        public string Status { get; set; }
        public string Description { get; set; }
    }
}