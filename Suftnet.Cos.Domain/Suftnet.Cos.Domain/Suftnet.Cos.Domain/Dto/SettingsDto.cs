namespace Suftnet.Cos.DataAccess
{
    using System;
    using System.ComponentModel.DataAnnotations;

    public class SettingsDTO : BaseDto
    {      
        public string Description { get; set; }
        [Required()]
        [StringLength(100)]
        public string Title { get; set; }
        public bool? Active { get; set; }
        [Required()]
        public int? ClassId { get; set; }
        public string Class { get; set; }
        public string ImageUrl { get; set; }
        public string QueryTitle { get; set; }

    }
}
