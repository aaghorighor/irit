namespace Suftnet.Cos.DataAccess
{
   using System;
    using System.Collections.Generic;
    using System.Runtime.Serialization;

    public class LogDto
   {
       public int Id { get; set; }
       public string Description { get; set; }       
       public DateTime CreatedDt { get; set; }
       public string CreatedBy { get; set; }

        public string CreatedOn
        {
            get
            {
                return this.CreatedDt.ToString();
            }
        }
    }

    public class LogAdapter
    {
        [IgnoreDataMember]
        public int Count { get; set; }
        public IList<LogDto> Logs { get; set; }
    }
}
