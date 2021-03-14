namespace Suftnet.Cos.Mobile
{  
    using Suftnet.Cos.DataAccess;
    using Suftnet.Cos.Extension;  
    using Suftnet.Cos.Web.ActionFilter;    
    using System;
    using System.Net;
    using System.Net.Http;
    using System.Threading.Tasks;
    using System.Web.Http;
      
    [RoutePrefix("api/v1/report")]
    public class ReportController : BaseController
    {
        private readonly IReport _report;
 
        public ReportController(IReport report)
        {
            _report = report;           
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
            var model = await Task.Run(() => _report.FetchPayments(new Guid(ExternalId), 50));
            return Ok(model);
        }

        [HttpGet]
        [JwtAuthenticationAttribute]
        [Route("fetchByDate")]
        public async Task<IHttpActionResult> FetchByDate([FromUri]DateQueryDto dateQueryDto)
        {
            if (!ModelState.IsValid)
            { return ResponseMessage(Request.CreateResponse(HttpStatusCode.BadRequest, new { Message = ModelState.Error() })); }

            dateQueryDto.ExernalId = new Guid(ExternalId);
            var model = await Task.Run(() => _report.FetchPaymentByDates(dateQueryDto));
            return Ok(model);
        }

    }
}