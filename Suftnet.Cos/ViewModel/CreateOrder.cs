﻿namespace Suftnet.Cos.DataAccess
{
    using System;
    using System.ComponentModel.DataAnnotations;
    public class CreateOrder
    {
        public DateTime CreatedDT { get; set; }
        public DateTime? UpdateDate { get; set; }
        public string UpdateBy { get; set; }
        public string CreatedBy { get; set; }
        [Required]
        [StringLength(50)]
        public string ExternalId { get; set; }
        [Required]
        [StringLength(50)]
        public string TableId { get; set; }
        [Required]
        [StringLength(50)]
        public string UserName { get; set; }
        public Guid OrderId { get; set; }
        [Required] 
        public int TableFor { get; set; }
    }
    public class QueryParam
    {
        [Required]
        [StringLength(50)]
        public string OrderId { get; set; }        
    }
}