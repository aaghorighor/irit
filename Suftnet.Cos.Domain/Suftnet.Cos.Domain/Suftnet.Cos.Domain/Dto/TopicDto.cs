namespace Suftnet.Cos.DataAccess
{
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;

    public class TopicDto : BaseDto
    {
      [Required()]     
      public string Description { get; set; }     
      [Required()]
      public int TopicId { get; set; }
      public string Topic { get; set; }
      [Required()]
      public int ChapterId { get; set; }
      public string Chapter { get; set; }
      public bool Publish { get; set; } 
      public string ImageUrl { get; set; }
      public int IndexNo { get; set; }
      public IList<SubTopicDto> SubTopics { get; set; }
    }
}
