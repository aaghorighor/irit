namespace Suftnet.Cos.DataAccess
{
    using System;
    using System.ComponentModel.DataAnnotations;

    public class DeviceDto : BaseDto
    {    
      public string AppVersion { get; set; }          
      public string OsVersion { get; set; }     
      public string Serial { get; set; }    
      public string DeviceName { get; set; }
      [Required]
      public string DeviceId { get; set; }      
    }
}
