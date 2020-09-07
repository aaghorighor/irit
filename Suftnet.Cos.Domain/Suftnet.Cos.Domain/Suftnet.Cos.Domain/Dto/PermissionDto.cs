namespace Suftnet.Cos.DataAccess
{ 
    using System.ComponentModel.DataAnnotations;

    public class PermissionDto : Base2Dto
    {
        [Required]
      public int ViewId { get; set; }
      public string View { get; set; }
      public int Create { get; set; }
      public int Edit { get; set; }
      public int Remove { get; set; }
      public int Get { get; set; }
      public int GetAll { get; set; }
      public int? Custom { get; set; }
      public string UserId { get; set; }
      public string IdentityId { get; set; }
    }
}
