namespace Suftnet.Cos.DataAccess.Action
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("MobileLogger")]
    public partial class MobileLogger
    {
        public int Id { get; set; }
        [StringLength(50)]
        public string CreatedBy { get; set; }    
        public string REPORT_ID { get; set; }     
        public string PACKAGE_NAME { get; set; }      
        public string BUILD { get; set; }
        public string STACK_TRACE { get; set; }
        public string ANDROID_VERSION { get; set; }
        public string APP_VERSION_CODE { get; set; }
        public string AVAILABLE_MEM_SIZE { get; set; }
        public string CRASH_CONFIGURATION { get; set; }        

        [Column(TypeName = "smalldatetime")]
        public DateTime CreatedDT { get; set; }

        [Column(TypeName = "timestamp")]
        [DatabaseGenerated(DatabaseGeneratedOption.Computed)]
        [MaxLength(8)]
        public byte[] TimeStamp { get; set; }
    }
}
