namespace Suftnet.Cos.Mobile
{
    using Suftnet.Cos.Extension;
    using Suftnet.Cos.Web.ViewModel;
    using System;
    using System.Net;
    using System.Net.Http;
    using System.Threading.Tasks;
    using System.Web.Http;
    using Web.ActionFilter;
    using Web.Command;
    
    [RoutePrefix("api/v1/boostrap")]
    public class BoostrapController : BaseController
    {
        private readonly IBoostrapCommand _boostrapCommand;
     
        public BoostrapController(IBoostrapCommand boostrapCommand)
        {
            _boostrapCommand = boostrapCommand;           
        }

        [HttpGet]
        [Route("ping")]
        public IHttpActionResult Ping()
        {
            return Ok(DateTime.Now);
        }

        [HttpGet]
       // [JwtAuthenticationAttribute]
        [Route("getBy")]
        public async Task<IHttpActionResult> Fetch([FromUri]Param param)
        {
            if (!ModelState.IsValid)
            {
                return ResponseMessage(Request.CreateResponse(HttpStatusCode.BadRequest, new { Message = ModelState.Error() }));
            }

             _boostrapCommand.TenantId = new Guid(param.ExternalId);
             var model = await Task.Run(()=> _boostrapCommand.Execute());

            return Ok(model);           
        }       

    }
}