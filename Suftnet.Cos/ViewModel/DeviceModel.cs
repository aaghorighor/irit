using System.ComponentModel.DataAnnotations;

namespace Suftnet.Cos.DataAccess
{     
    public class DeviceModel
    {      
        public string AppVersion { get; set; }
        public string OsVersion { get; set; }
        public string Serial { get; set; }
        public string DeviceName { get; set; }
        [Required()]
        public string DeviceId { get; set; }
        [Required()]
        public string ExternalId { get; set; }
    }
}
