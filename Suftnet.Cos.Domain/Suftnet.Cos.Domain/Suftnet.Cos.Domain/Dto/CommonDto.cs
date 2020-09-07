namespace Suftnet.Cos.DataAccess
{
   using System;
   using System.ComponentModel.DataAnnotations;

    public class CommonDto : BaseDto
    {
        public string Description { get; set; }
        [Required()]
        [StringLength(250)]
        public string Title { get; set; }       
        public bool Active { get; set; }
        public int? Indexno { get; set; }     
        public string code { get; set; }  
        public int CommonId { get; set; }
        [Required()]
        public int SettingId { get; set; }
        public string ImageUrl { get; set; }
    }
}
