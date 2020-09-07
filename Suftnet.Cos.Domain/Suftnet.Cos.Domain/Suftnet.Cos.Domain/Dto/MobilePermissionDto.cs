namespace Suftnet.Cos.DataAccess
{
    using System;
    using System.ComponentModel.DataAnnotations;

    public class MobilePermissionDto : Base2Dto
    {     
     
      [Required]
      public int PermissionId { get; set; }
      public string Description { get; set; }  
      public string UserId { get; set; }
      
    }
}
