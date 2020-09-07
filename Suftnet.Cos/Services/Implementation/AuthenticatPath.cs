namespace Suftnet.Cos.Web
{   
    using System.Collections.Generic;
    using System.Linq;   
   
    public class AuthenticatPath
    {
        public AuthenticatPath()
        {

        }

        public static List<string> AuthenticatePathExcludedList
        {
            get
            {
                return new List<string>() 
				{
					"/images",
					"/content",
					"/scripts",
					"/services",
					"/favicon.ico",
					"/api/",
					"/realtime",
					"/bundle",
				};
            }
        }

        public static void AuthenticateRequest(System.Web.HttpContext ctx)
        {
            if (AuthenticatePathExcludedList.Any(i => ctx.Request.RawUrl.ToLower().StartsWith(i)))
            {
                return;
            }
           
        }
    }
}