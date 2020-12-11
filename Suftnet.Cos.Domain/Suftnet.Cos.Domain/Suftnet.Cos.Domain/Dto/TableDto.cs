using Newtonsoft.Json;
using System;

namespace Suftnet.Cos.DataAccess
{ 
    public class TableDto  : Base2Dto
    {    
      public int Size { get; set; }
      public string Number { get; set; }  
      public int? StatusId { get; set; }  
      public string TimeIn { get; set; }
      public Guid? OrderId { get; set; }
      public bool Active { get; set; }
      public bool IsReset { get; set; }

    }

    public class MobileTableDto
    {      
        public Guid TableId { get; set; }     
        public Guid ExternalId { get; set; }         
        public int Size { get; set; }
        public string Number { get; set; }
        public string TimeIn { get; set; }
        [JsonIgnore]
        public Guid? OrderId { get; set; }
        public string ExternalOrderId { get {

                if (this.OrderId != null) { return OrderId.ToString(); };
                return "0";
            } }        
    }
}
