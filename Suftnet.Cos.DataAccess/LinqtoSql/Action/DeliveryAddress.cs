namespace Suftnet.Cos.DataAccess.Action
{
    using System;   
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;

    [Table("DeliveryAddress")]
    public partial class DeliveryAddress
    {       
        public Guid Id { get; set; }
        public Guid OrderId { get; set; }
        [StringLength(20)]
        public string Latitude { get; set; }
        [StringLength(20)]
        public string Logitude { get; set; }
        [StringLength(200)]
        public string AddressLine { get; set; }      
        public string Duration { get; set; }   
        public string Distance { get; set; }   
        [StringLength(50)]
        public string CreatedBy { get; set; }
        [Column(TypeName = "smalldatetime")]
        public DateTime? CreatedAt { get; set; }       
        [Column(TypeName = "timestamp")]
        [DatabaseGenerated(DatabaseGeneratedOption.Computed)]
        [MaxLength(8)]
        public byte[] TimeStamp { get; set; }
        [ForeignKey("OrderId")]
        public virtual Order Order { get; set; }
      
    }
}
