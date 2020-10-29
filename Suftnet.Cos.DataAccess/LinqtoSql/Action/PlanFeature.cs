namespace Suftnet.Cos.DataAccess.Action
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("PlanFeature")]
    public partial class PlanFeature
    {
        public int Id { get; set; }

        public int FeatureId { get; set; }

        public int BasicId { get; set; }

        public int AdvanceId { get; set; }

        public int ProfessionId { get; set; }

        [Required]
        [StringLength(50)]
        public string CreatedBy { get; set; }

        [Column(TypeName = "smalldatetime")]
        public DateTime CreatedDt { get; set; }

        public int PlanId { get; set; }

        [StringLength(50)]
        public string BasicValue { get; set; }

        [StringLength(50)]
        public string PremiumValue { get; set; }

        [StringLength(50)]
        public string PremiumPlusValue { get; set; }

        public int? IndexNo { get; set; }

        [Column(TypeName = "timestamp")]
        [DatabaseGenerated(DatabaseGeneratedOption.Computed)]
        [MaxLength(8)]
        public byte[] TimeStamp { get; set; }

        public virtual Plan Plan { get; set; }
    }
}
