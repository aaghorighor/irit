namespace Suftnet.Cos.Mobile
{
    using Cos.DataAccess;

    using System;
    using System.Threading.Tasks;
    using System.Web.Http;

    using Web.Mapper;

    [RoutePrefix("api/v2/common")]
    public class CommonController : BaseController
    {
        private readonly ICommon _common;
        public CommonController(ICommon common)
        {
            _common = common;        
        }

        [HttpGet]
        [Route("ping")]
        public IHttpActionResult Ping()
        {
            return Ok(DateTime.Now);
        }

        [HttpGet]
        [Route("{settingId}")]
        public async Task<IHttpActionResult> Get(int settingId)
        {
            var model = await Task.Run(() => _common.GetAll(settingId));           
            return Ok(model);
        }
    }
}