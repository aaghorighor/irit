namespace Suftnet.Cos.DataAccess.Action
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("Global")]
    public partial class Global
    {
        public int Id { get; set; }

        public int? CurrencyId { get; set; }

        [StringLength(50)]
        public string DateTimeFormat { get; set; }

        [StringLength(50)]
        public string Server { get; set; }

        [StringLength(50)]
        public string Password { get; set; }

        [StringLength(50)]
        public string UserName { get; set; }

        [StringLength(50)]
        public string Email { get; set; }

        public int? Port { get; set; }

        [StringLength(250)]
        public string Company { get; set; }

        [StringLength(50)]
        public string Telephone { get; set; }

        [StringLength(50)]
        public string Mobile { get; set; }

        [StringLength(500)]
        public string Description { get; set; }

        public int? AddressId { get; set; }

        [StringLength(50)]
        public string ServerEmail { get; set; }

        [Column(TypeName = "money")]
        public decimal? TaxRate { get; set; }

        [Column(TypeName = "timestamp")]
        [MaxLength(8)]
        [Timestamp]
        public byte[] TimeStamp { get; set; }
       
    }
}
