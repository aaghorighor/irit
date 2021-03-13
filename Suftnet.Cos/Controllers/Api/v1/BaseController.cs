namespace Suftnet.Cos.Mobile
{
    using Service;
    using System.Security.Claims;
    using System.Web.Http;
    using System.Linq;

    [LogExceptionFilter]
    public class BaseController : ApiController
    {
        public string UserName
        {

            get
            {
                var test = ((ClaimsIdentity)this.User.Identity);

                if (test != null)
                {
                    return test.Claims.Where(x => x.Type == ClaimTypes.Name).Select(x => x.Value).SingleOrDefault();
                }

                return "UnKnown User";
            }
        }

        public string UserId
        {
            get
            {
                var test = ((ClaimsIdentity)this.User.Identity);

                if (test != null)
                {
                    return test.Claims.Where(x => x.Type == ClaimTypes.NameIdentifier).Select(x => x.Value).SingleOrDefault();
                }

                return "000";
            }
        }

        public string ExternalId
        {

            get
            {
                var test = ((ClaimsIdentity)this.User.Identity);

                if (test != null)
                {
                    return test.Claims.Where(x => x.Type == ClaimTypes.GroupSid).Select(x => x.Value).SingleOrDefault();
                }

                return "000";
            }
        }

    }

}