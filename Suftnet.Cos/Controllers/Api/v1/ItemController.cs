namespace Suftnet.Cos.Mobile
{
    using Suftnet.Cos.Extension;
    using Suftnet.Cos.Web.ActionFilter;
    using Suftnet.Cos.Web.ViewModel;
    using System;
    using System.Net;
    using System.Net.Http;
    using System.Threading.Tasks;
    using System.Web.Http;
    using Web.Command;
    
    [RoutePrefix("api/v1/menu")]
    public class ItemController : BaseController
    {
        private readonly IItemCommand _itemCommand;
     
        public ItemController(IItemCommand itemCommand)
        {
            _itemCommand = itemCommand;           
        }

        [HttpGet]
        [Route("ping")]
        public IHttpActionResult Ping()
        {
            return Ok(DateTime.Now);
        }

        [HttpGet]
        [JwtAuthenticationAttribute]
        [Route("fetch")]
        public async Task<IHttpActionResult> Fetch()
        {
            if (!ModelState.IsValid)
            {
                return ResponseMessage(Request.CreateResponse(HttpStatusCode.BadRequest, new { Message = ModelState.Error() }));
            }

            _itemCommand.TenantId = new Guid(ExternalId);
             var model = await Task.Run(()=> _itemCommand.Execute());

            return Ok(model);           
        }       

    }
}