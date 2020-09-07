namespace Suftnet.Cos.DataAccess
{
   using System;
   using System.ComponentModel.DataAnnotations;

    public class UnitDto : Base2Dto
    {      
        [Required()]
        [StringLength(250)]
        public string Name { get; set; }       
        public bool Active { get; set; }
        public int IndexNo { get; set; }    
         
    }

    public class MobileUnitDto
    {
        public Guid Id { get; set; }
        public string Name { get; set; }       
    }
}
