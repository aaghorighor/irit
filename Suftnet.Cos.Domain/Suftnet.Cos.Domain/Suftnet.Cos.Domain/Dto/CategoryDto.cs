namespace Suftnet.Cos.DataAccess
{
    using Newtonsoft.Json;
    using System;

    public class CategoryDto : Base2Dto
    {     
        public string Name { get; set; }
        public string ImageUrl { get; set; }   
        public string Description { get; set; }
        public bool Status { get; set; }
        public int IndexNo { get; set; }
    }

    public class MobileCategoryDto
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
        public string Name { get; set; }
        public string ImageUrl { get; set; }
        public string Description { get; set; }       
        public int IndexNo { get; set; }
    }
}
