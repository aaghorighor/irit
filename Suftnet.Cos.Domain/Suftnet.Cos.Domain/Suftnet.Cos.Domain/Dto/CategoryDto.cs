namespace Suftnet.Cos.DataAccess
{
    using System;

    public class CategoryDto : Base2Dto
    {     
        public string Name { get; set; }
        public string ImageUrl { get; set; }   
        public string Description { get; set; }
        public bool Status { get; set; }
        public int IndexNo { get; set; }
    }
}
