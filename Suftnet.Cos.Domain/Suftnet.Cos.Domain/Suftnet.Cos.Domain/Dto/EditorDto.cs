namespace Suftnet.Cos.DataAccess
{ 
    using System.ComponentModel.DataAnnotations;

    public class TourDto : BaseDto
    {
      [Required()]
      [StringLength(500)]
      public string Title { get; set; }
      [Required()]
      [StringLength(1000)]
      public string Description { get; set; }
      public string ImageUrl { get; set; } 
      [Required()]
      public int StyleTypeId { get; set; }
      public string StyleType { get; set; }   
      public bool Active { get; set; }    
      public int SortOrder { get; set; }
      public int Index { get; set; }   
    }
}
