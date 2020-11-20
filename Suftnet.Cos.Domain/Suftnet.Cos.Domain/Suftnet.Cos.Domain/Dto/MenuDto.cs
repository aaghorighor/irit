namespace Suftnet.Cos.DataAccess
{
    using Newtonsoft.Json;
    using System;
    using System.Collections.Generic;

    public class MenuDto : Base2Dto
    {       
        public string Name { get; set; }
        public string Description { get; set; }
        public Guid CategoryId { get; set; }       
        public string Discount { get; set; }
        public Guid? DiscountId { get; set; }
        public string Category { get; set; }
        public Guid UnitId { get; set; }
        public string Unit { get; set; }         
        public decimal Price { get; set; }        
        public int Quantity { get; set; }    
        public Guid TaxId { get; set; }
        public int SubStractId { get; set; }
        public int? CutOff { get; set; }
        public string SubStract { get; set; }        
        public string Tax { get; set; }
        public string ImageUrl { get; set; }
        public string TaxRate { get; set; }
        public bool Active { get; set; }
        public List<AddonDto> AddonDto { get; set; }
        public bool? IsKitchen { get; set; }           
    }

    public class MobileMenuDto 
    {
        [JsonIgnore]
        public Guid Id { get; set; }        
        public string ExternalId { get {
                return Id.ToString();
            }
        }
        public string Name { get; set; }
        public string Description { get; set; }          
        public string Category { get; set; }
        public Guid CategoryId { get; set; }
        public string Unit { get; set; }
        public decimal Price { get; set; }
        public int Quantity { get; set; }            
        public string ImageUrl { get; set; }              
    }
}
