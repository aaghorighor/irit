namespace Suftnet.Cos.DataAccess
{
    using Newtonsoft.Json;
    using Suftnet.Cos.Model;
    using System;
    using System.Runtime.Serialization;     
    public class BaseDto
    {
        [JsonIgnore]
        public int Id { get; set; }
        public int flag { get; set; }
        [JsonIgnore]
        public Guid TenantId { get; set; }       
        public DateTime CreatedDT { get; set; }
        public string CreatedOn
        {
            get
            {
                return this.CreatedDT.ToShortDateString();
            }           
        }
        public string CreatedBy { get; set; }  
        public string ExternalId { get; set; }         

    }

    public class Base2Dto
    {
        public Guid Id { get; set; }
        public int flag { get; set; }
        [JsonIgnore]
        public Guid TenantId { get; set; }
        public DateTime CreatedDT { get; set; }
        public DateTime? UpdateDate { get; set; }
        public string UpdatedOn
        {
            get
            {
                return this.UpdateDate?.ToShortDateString();
            }
        }
        public string CreatedOn
        {
            get
            {
                return this.CreatedDT.ToShortDateString();
            }
        }
        public string UpdateBy { get; set; }
        public string CreatedBy { get; set; }
        public string ExternalId { get; set; }

    }
}
