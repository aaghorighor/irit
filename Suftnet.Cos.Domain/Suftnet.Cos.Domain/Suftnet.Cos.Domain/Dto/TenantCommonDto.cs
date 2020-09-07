namespace Suftnet.Cos.DataAccess
{
    using System;
    using System.ComponentModel.DataAnnotations;

    public class TenantCommonDto : BaseDto
    {
        public string Description { get; set; }
        [Required()]
        [StringLength(250)]
        public string Title { get; set; }       
        public bool Active { get; set; }
        public int? Indexno { get; set; }     
        public string Code { get; set; }       
        [Required()]
        public int SettingId { get; set; }
    }
}
