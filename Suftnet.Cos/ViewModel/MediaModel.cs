namespace Suftnet.Cos.DataAccess
{
    using System;
 
    public class MediaModel 
    {      
      public int Id { get; set; }     
      public string Title { get; set; }     
      public string Description { get; set; }          
      public string MedialUrl { get; set; }   
      public int MediaTypeId { get; set; }
      public string MediaType { get; set; }
      public int FormatTypeId { get; set; }
      public string ExternalId { get; set; }
      public string Speaker { get; set; }
      public string ImageUrl { get; set; }
      public string Published { get; set; }
    }
}
