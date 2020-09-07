namespace Suftnet.Cos.DataAccess
{   
    public class LoginDto 
    {                
      public string UserId { get; set; }   
      public int? MemberId { get; set; }     
      public string UserRole { get; set; }
      public int? StatusId { get; set; }
      public string UserName { get; set; }
      public string ExternalId { get; set; }
    }
}
