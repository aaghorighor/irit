namespace Suftnet.Cos.DataAccess
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
  
    public partial class CustomerAddressDto : Base2Dto
    {        
        [Required]
        public Guid CustomerId { get; set; }          
     
        [StringLength(20)]
        public string Latitude { get; set; }

        [StringLength(20)]
        public string Longitude { get; set; }

        [Required]
        [StringLength(150)]
        public string AddressLine { get; set; }       

        [StringLength(50)]
        public string Town { get; set; }

        [StringLength(50)]
        public string County { get; set; }

        [StringLength(50)]
        public string Country { get; set; }       

        [StringLength(250)]
        public string CompleteAddress { get; set; }

        [StringLength(50)]
        public string Postcode { get; set; }
        public Guid AddressId
        {
            get
            {
                return this.Id;
            }
        }

    }
}
