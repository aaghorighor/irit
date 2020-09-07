namespace Suftnet.Cos.DataAccess
{   
    public class SliderDto : BaseDto
    {      
        public string Title { get; set; }
        public string Alt { get; set; }
        public string Description { get; set; }
        public string ImageUrl { get; set; }          
        public bool Publish { get; set; }
        public int? SliderTypeId { get; set; }
        public string SliderType { get; set; }

    }
}