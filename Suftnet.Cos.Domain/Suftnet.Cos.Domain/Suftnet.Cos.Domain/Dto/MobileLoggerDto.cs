namespace Suftnet.Cos.DataAccess
{
   using System;
    using System.Collections.Generic;
    using System.Runtime.Serialization;

    public class MobileLogDto
    {
       public int Id { get; set; }
       public string REPORT_ID { get; set; }
       public string PACKAGE_NAME { get; set; }
       public string BUILD { get; set; }
       public string STACK_TRACE { get; set; }
       public string ANDROID_VERSION { get; set; }
       public string APP_VERSION_CODE { get; set; }
       public string AVAILABLE_MEM_SIZE { get; set; }
       public string CRASH_CONFIGURATION { get; set; }
       public string CreatedOn
        {
            get
            {
                return this.CreatedDt.ToString();
            }
        }
       public DateTime CreatedDt { get; set; }
       public string CreatedBy { get; set; }
    }

    public class MobileLogModel
    {
        public string STACK_TRACE { get; set; }
        public string REPORT_ID { get; set; }
        public string PACKAGE_NAME { get; set; }
        public object BUILD { get; set; }  
        public string ANDROID_VERSION { get; set; }
        public string APP_VERSION_CODE { get; set; }
        public string AVAILABLE_MEM_SIZE { get; set; }
        public object CRASH_CONFIGURATION { get; set; }   
        public DateTime CreatedDt { get; set; }
        public string CreatedBy { get; set; }
        public string CreatedOn { get; set; }       
    }

    public class MobileLoggerAdapter
    {
        [IgnoreDataMember]
        public int Count { get; set; }
        public IList<MobileLogDto> MobileLogs { get; set; }
    }
}
