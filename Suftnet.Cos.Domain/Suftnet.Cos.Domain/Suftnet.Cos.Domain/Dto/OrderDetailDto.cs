namespace Suftnet.Cos.DataAccess
{
    using System;  
 
    public class OrderDetailDto : Base2Dto
    {     
        public Guid MenuId { get; set; }    
        public Guid CategoryId { get; set; }           
        public string Unit { get; set; }        
        public int Quantity { get; set; }    
        public string Title { get; set; }
        public decimal Price { get; set; }
        public string ItemName { get; set; }        
        public decimal Total { get; set; }    
        public Guid OrderId { get; set; }
        public bool? IsProcessed { get; set; }
        public bool? IsKitchen { get; set; }
        public decimal LineTotal { get; set; }   

        public string AddonIds { get; set; }  
        public string AddonItems { get; set; }
    }
    public class BasketDto 
    {
        public Guid MenuId { get; set; }
        public Guid OrderId { get; set; }            
        public decimal Price { get; set; }
        public string Menu { get; set; }
        public string ItemName { get; set; }
        public int Quantity { get; set; }
        public bool? IsProcessed { get; set; }        
        public string Addons { get; set; }
        public string AddonIds { get; set; }
    }

    public class MobileBasketDto
    {        
        public decimal Price { get; set; }
        public string Menu { get; set; }
        public string Category { get; set; }
        public int Quantity { get; set; }
        public string addonNames { get; set; }
        public string AddonIds { get; set; }
    }

    public class KitchenBasketDto
    {
        public Guid Id { get; set; }     
        public string ItemName { get; set; }
        public string AddonItems { get; set; }
        public bool? IsProcessed { get; set; }
        public string Addons { get; set; }       
    }
}
