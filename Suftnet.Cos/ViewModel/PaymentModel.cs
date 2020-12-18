namespace Suftnet.Cos.Web
{
    using System;
    using System.ComponentModel.DataAnnotations;

    public class PaymentModel
    {
        [Required()]
        [StringLength(50)]
        public string OrderId { get; set; }
        [Required()]
        [StringLength(50)]
        public string TableId { get; set; }
        [Required()]
        [StringLength(50)]
        public string UserName { get; set; }
        [Required()]
        [StringLength(50)]
        public string CreatedDt { get; set; }
        [Required()]
        [StringLength(50)]
        public string ExternalId { get; set; }
        [Required()]      
        public decimal AmountPaid { get; set; }
        [Required()]
        [StringLength(50)]
        public string PaymentMethodId { get; set; }
        [Required()]
        public decimal GrandTotal { get; set; }
        
    }

   
}
