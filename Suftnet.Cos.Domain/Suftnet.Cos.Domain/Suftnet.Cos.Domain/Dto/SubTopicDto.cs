namespace Suftnet.Cos.DataAccess
{   
    using System.ComponentModel.DataAnnotations;

    public class SubTopicDto : BaseDto
    {
      [Required()]     
      public string Description { get; set; }     
      [Required()]
      public int TopicId { get; set; }
        public string ImageUrl { get; set; }
      public int IndexNo { get; set; }
        public string Title { get; set; }
    }
}
