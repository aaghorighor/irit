namespace Suftnet.Cos.DataAccess
{
   using System;
   using System.ComponentModel.DataAnnotations;

    public class DiscountDto : Base2Dto
    {      
        [Required()]
        [StringLength(250)]
        public string Name { get; set; }       
        public bool Active { get; set; }
        public int IndexNo { get; set; }     
        public decimal Rate { get; set; }       
    }

    public class MobileDiscountDto
    {
        public Guid Id { get; set; }
        public string Name { get; set; }    
        public decimal Rate { get; set; }
    }
}
