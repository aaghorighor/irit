namespace Suftnet.Cos.DataAccess
{   
    using System.ComponentModel.DataAnnotations;

    public class ChapterDto : BaseDto
    {     
      [Required()]
      public string Description { get; set; }     
      [Required()]
      public int SubSectionId { get; set; }
      public string SubSection { get; set; }
      [Required()]
      public int SectionId { get; set; }
      public string Section { get; set; }
      public bool Publish { get; set; }    
      public string ImageUrl { get; set; }
    }
}
