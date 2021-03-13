namespace Suftnet.Cos.Mobile
{
    using Suftnet.Cos.DataAccess;
    using Suftnet.Cos.Extension;
    using Suftnet.Cos.Web.ActionFilter;
    using Suftnet.Cos.Web.ViewModel;
    using System;
    using System.Net;
    using System.Net.Http;
    using System.Threading.Tasks;
    using System.Web.Http;
      
    [RoutePrefix("api/v1/table")]
    public class TableController : BaseController
    {
        private readonly ITable _table;
     
        public TableController(ITable table)
        {
            _table = table;           
        }

        [HttpGet]
        [Route("ping")]
        public IHttpActionResult Ping()
        {
            return Ok(DateTime.Now);
        }

        [HttpGet]
        [JwtAuthenticationAttribute]
        [Route("getBy")]
        public async Task<IHttpActionResult> Fetch([FromUri]Param param)
        {
            if (!ModelState.IsValid)
            {
                return ResponseMessage(Request.CreateResponse(HttpStatusCode.BadRequest, new { Message = ModelState.Error() }));
            }
                        
            var model = await Task.Run(()=> _table.GetBy(new Guid(param.ExternalId)));
            return Ok(model);           
        }       

    }
}