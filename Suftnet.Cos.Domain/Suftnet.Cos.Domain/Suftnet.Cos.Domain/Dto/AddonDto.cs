namespace Suftnet.Cos.DataAccess
{
    using Newtonsoft.Json;
    using System;

   public class AddonDto : Base2Dto
    {   
       public string Name { get; set; }
       public bool Active { get; set; }
       public Guid MenuId { get; set; }
       public Guid AddonTypeId { get; set; }
       public string AddonType { get; set; }
       public decimal Price { get; set; }      
    }

    public class MobileAddonDto 
    {
        [JsonIgnore]
        public Guid Id { get; set; }
        public string ExternalId
        {
            get
            {
                return Id.ToString();
            }
        }
        public string ExternalMenuId
        {
            get
            {
                return MenuId.ToString();
            }
        }
        public string Name { get; set; }
        [JsonIgnore]
        public Guid MenuId { get; set; }      
        public string AddonType { get; set; }
        public decimal Price { get; set; }
    }
}
