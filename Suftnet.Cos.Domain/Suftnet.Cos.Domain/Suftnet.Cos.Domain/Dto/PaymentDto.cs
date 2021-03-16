namespace Suftnet.Cos.DataAccess
{
    using System;

    [Serializable]
    public class PaymentDto : Base2Dto
    { 
        public string Reference { get; set; }
        public string PaymentMethod { get; set; }
        public Guid PaymentMethodId { get; set; }
        public Guid AccountTypeId { get; set; }
        public string AccountType { get; set; }
        public Guid OrderId { get; set; }    
        public decimal Amount { get; set; }
        public string OrderReference
        {
            get
            {
                return this.Reference.ToString().Substring(0, 8);
            }
        }

    }
     
    public class MobilePaymentDto 
    {
        public string CreatedAt
        {
            get
            {
                return this.CreatedDT.ToShortDateString();
            }
        }

        public string Reference
        {
            get
            {
                return this.OrderId.ToString().Substring(0, 8);
            }
        }
      
        public string OrderId { get; set; }
        public string PaymentMethod { get; set; }
        public string AccountType { get; set; }
        public Guid AccountTypeId { get; set; }
        public decimal Amount { get; set; }
        public DateTime CreatedDT { get; set; }

    }
}


