namespace Suftnet.Cos.DataAccess
{
   using System;

   public class AddonDto : Base2Dto
    {   
       public string Name { get; set; }
       public bool Active { get; set; }
       public Guid MenuId { get; set; }
       public Guid AddonTypeId { get; set; }
       public string AddonType { get; set; }
       public decimal? Price { get; set; }      
    }
}
