namespace Suftnet.Cos.DataAccess
{
    using System;

    public class OrderedItemDto
    {       
        public Guid MenuId { get; set; }
        public bool IsProcessed { get; set; }
        public string Name { get; set; }
        public decimal Price { get; set; }     
       
    }
}
