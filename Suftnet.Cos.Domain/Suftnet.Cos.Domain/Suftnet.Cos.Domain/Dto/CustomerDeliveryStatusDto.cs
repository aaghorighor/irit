namespace Suftnet.Cos.DataAccess
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;

    public partial class CustomerDeliveryStatusDto :Base2Dto
    {    

        public Guid OrderId { get; set; }

        public Guid OrderStatusId { get; set; }

        [Column(TypeName = "smalldatetime")]
        public DateTime CreatedAt { get; set; }
        public string Status { get; set; }

    }
}
