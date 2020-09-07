namespace Suftnet.Cos.BackOffice
{
    using Common;
    using CommonController.Controllers;
    using Suftnet.Cos.Web.Infrastructure.ActionFilter;
    using System.Text;
    using System.Web.Mvc;

    [AuthorizeActionFilter(Constant.BackOfficeOnly)]   
    public class BackOfficeBaseController : BaseController
    {
        protected override JsonResult Json(object data, string contentType,
           Encoding contentEncoding, JsonRequestBehavior behavior)
        {
            return new JsonFilter
            {
                Data = data,
                ContentType = contentType,
                ContentEncoding = contentEncoding,
                JsonRequestBehavior = behavior
            };
        }
    }
}
