namespace Suftnet.Cos.Mobile
{
    using Cos.DataAccess;
    using Extension;
    using System;
    using System.Net;
    using System.Net.Http;   
    using System.Web.Http;
  
    [RoutePrefix("api/v1/mobileLogger")]
    public class MobileLoggerController : BaseController
    {       
        private readonly IMobileLogger _mobileLogger;
        public MobileLoggerController(IMobileLogger mobileLogger)
        {
            _mobileLogger = mobileLogger;
        }

        [HttpGet]
        [Route("ping")]
        public IHttpActionResult Ping()
        {
            return Ok(DateTime.Now);
        }      
               
        [HttpPost]
        [Route("create")]
        public IHttpActionResult Create(MobileLogModel model)
        {
            if (!ModelState.IsValid)
            {
                return ResponseMessage(Request.CreateResponse(HttpStatusCode.BadRequest, new { Message = ModelState.Error() }));
            }

            _mobileLogger.Insert(Map(model));

            return Ok(true);
        }

        #region private function
        private MobileLogDto Map(MobileLogModel model)
        {          
            var MobileLoggerDto = new MobileLogDto
            {
                 REPORT_ID = model.REPORT_ID,
                 ANDROID_VERSION = model.ANDROID_VERSION,
                 APP_VERSION_CODE = model.APP_VERSION_CODE,
                 AVAILABLE_MEM_SIZE = model.AVAILABLE_MEM_SIZE,
                 CRASH_CONFIGURATION = model.CRASH_CONFIGURATION.ToString(),
                 BUILD = model.BUILD.ToString(),
                 PACKAGE_NAME = model.PACKAGE_NAME,
                 STACK_TRACE = model.STACK_TRACE,

                 CreatedDt = DateTime.UtcNow,
                 CreatedBy = model.PACKAGE_NAME
            };

            return MobileLoggerDto;
        }
        #endregion

    }
}