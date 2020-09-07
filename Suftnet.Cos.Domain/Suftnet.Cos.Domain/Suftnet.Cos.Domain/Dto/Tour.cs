namespace Suftnet.Cos.DataAccess
{
    using System;
    using System.ComponentModel.DataAnnotations;

    public class EditorDTO : BaseDto
    {
      [Required()]
      [StringLength(550)]
      public string Title { get; set; }
      public string Contents { get; set; }
      public string ImageUrl { get; set; }
      public string FileUrl { get; set; }
      [Required()]
      public int ContentTypeid { get; set; }
      public string ContentType { get; set; }   
      public bool Active { get; set; }     
    }
}
