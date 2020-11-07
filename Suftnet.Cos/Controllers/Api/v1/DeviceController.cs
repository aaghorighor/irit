namespace Suftnet.Cos.Mobile
{
    using Cos.DataAccess;
    using Extension;
    using System;
    using System.Net;
    using System.Net.Http;
    using System.Threading.Tasks;
    using System.Web.Http;

    using Web.Mapper;

    [RoutePrefix("api/v2/device")]
    public class DeviceController : BaseController
    {       
        private readonly IDevice _device;
        public DeviceController(IDevice device)
        {
            _device = device;
        }

        [HttpGet]
        [Route("ping")]
        public IHttpActionResult Ping()
        {
            return Ok(DateTime.Now);
        }       
        
        [HttpPost]
        [Route("create")]
        public IHttpActionResult Post(DeviceModel model)
        {
            if (!ModelState.IsValid)
            {
                return ResponseMessage(Request.CreateResponse(HttpStatusCode.BadRequest, ModelState.Error()));
            }

            if(_device.Find(Map.From(model)))
            {
                _device.Update(Map.From(model));
            }
            else
            {
                _device.Insert(Map.From(model));
            }        

            return Ok();
        }

        [HttpPost]
        [Route("Delete")]
        public IHttpActionResult Delete([FromBody]DeviceModel model)
        {
            if (!ModelState.IsValid)
            {
                return ResponseMessage(Request.CreateResponse(HttpStatusCode.BadRequest, ModelState.Error()));
            }

            _device.Delete(Map.From(model));

            return Ok();
        }
    }
}