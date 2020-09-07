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

    }
}
