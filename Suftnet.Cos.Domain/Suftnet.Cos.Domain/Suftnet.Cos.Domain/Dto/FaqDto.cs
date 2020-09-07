namespace Suftnet.Cos.DataAccess
{   
    using System.ComponentModel.DataAnnotations;

    public class FaqDto : BaseDto
    {
      [Required()]
      [StringLength(550)]
      public string Title { get; set; }
      [StringLength(550)]
      public string ShortDescription { get; set; }          
      public bool Publish { get; set; }
      public int? SortOrderId { get; set; }
      public string SortOrder { get; set; }
    }
}
