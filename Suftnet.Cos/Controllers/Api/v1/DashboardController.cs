namespace Suftnet.Cos.Mobile
{
    using Suftnet.Cos.Extension;

    using System;
    using System.Net;
    using System.Net.Http;
    using System.Threading.Tasks;
    using System.Web.Http;
    using Web.ActionFilter;
    using Web.Command;
    
    [RoutePrefix("api/v2/dashboard")]
    public class DashboardController : BaseController
    {
        private readonly IDashboardCommand _dashboardCommand;
        private readonly IMyDashboardCommand _myDashboardCommand;

        public DashboardController(IDashboardCommand dashboardCommand, IMyDashboardCommand myDashboardCommand)
        {
            _dashboardCommand = dashboardCommand;
            _myDashboardCommand = myDashboardCommand;
        }

        [HttpGet]
        [Route("ping")]
        public IHttpActionResult Ping()
        {
            return Ok(DateTime.Now);
        }

        [HttpGet]
        [JwtAuthenticationAttribute]
        [Route("getByTenantId/{externalId}")]
        public async Task<IHttpActionResult> GetByTenantId(string externalId)
        {
            if(string.IsNullOrEmpty(externalId))
            {
                return ResponseMessage(new HttpResponseMessage { StatusCode = HttpStatusCode.BadRequest });
            }
                       
            _dashboardCommand.TenantId = externalId.ToDecrypt().ToInt();
             var model = await System.Threading.Tasks.Task.Run(()=> _dashboardCommand.Execute());

            return Ok(model);           
        }

        [HttpGet]
        [Route("getByMemberId/{externalId}")]
        [JwtAuthenticationAttribute]
        public async Task<IHttpActionResult> GetByMemberId(string externalId)
        {
            if (string.IsNullOrEmpty(externalId))
            {
                return ResponseMessage(new HttpResponseMessage { StatusCode = HttpStatusCode.BadRequest });
            }

            _myDashboardCommand.MemberId = externalId.ToDecrypt().ToInt();
            var model = await System.Threading.Tasks.Task.Run(() => _myDashboardCommand.Execute());

            return Ok(model);
        }

    }
}