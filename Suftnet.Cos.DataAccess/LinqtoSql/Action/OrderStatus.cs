namespace Suftnet.Cos.DataAccess.Action
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("OrderStatus")]
    public partial class OrderStatus
    {      
        [StringLength(50)]
        public string Name { get; set; }
      
        [Column(TypeName = "smalldatetime")]
        public DateTime CreatedDt { get; set; }
               
        [StringLength(50)]
        public string CreatedBy { get; set; }
                 
        public int IndexNo { get; set; }
                
        [Column(TypeName = "timestamp")]
        [MaxLength(8)]
        [Timestamp]
        public byte[] TimeStamp { get; set; }
             
        public Guid Id { get; set; }       
        public bool Active { get; set; }
    }
}
