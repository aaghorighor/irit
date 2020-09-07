namespace Suftnet.Cos.DataAccess
{   
    public class MobileSliderDto : BaseDto
    {      
        public string Title { get; set; }  
        public string Description { get; set; }
        public string ImageUrl { get; set; }          
        public bool Publish { get; set; }     

    }
}