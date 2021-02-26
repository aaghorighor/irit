namespace Suftnet.Cos.Mobile
{
    using Cos.DataAccess;   
    using System;
    using System.Net;
    using System.Net.Http;
    using System.Web.Http;  
    using Extension; 
  
    [RoutePrefix("api/v1/customerAddress")]
    public class CustomerAddressController : BaseController
    {          
        private readonly ICustomerAddress _customerAddress;      
       
        public CustomerAddressController(ICustomerAddress customerAddress)
        {
            _customerAddress = customerAddress;      
        }        

        [Route("Ping")]

        public IHttpActionResult Get()
        {
            return Ok(DateTime.Now);
        }

        [HttpPost]
        // [JwtAuthenticationAttribute]
        [Route("create")]
        public IHttpActionResult Create([FromBody]CustomerAddressDto customerAddressDto)
        {
            if (!ModelState.IsValid)
            {
                return ResponseMessage(Request.CreateResponse(HttpStatusCode.BadRequest, new { Message = ModelState.Error() }));
            }

            customerAddressDto.CreatedDT = DateTime.UtcNow;
            customerAddressDto.CreatedBy = "Tester";
            customerAddressDto.Id = Guid.NewGuid();
           _customerAddress.Insert(customerAddressDto);

            return Ok(customerAddressDto);            
        }

        [HttpPost]
        // [JwtAuthenticationAttribute]
        [Route("update")]
        public IHttpActionResult Update([FromBody] CustomerAddressDto customerAddressDto)
        {
            if (!ModelState.IsValid)
            {
                return ResponseMessage(Request.CreateResponse(HttpStatusCode.BadRequest, new { Message = ModelState.Error() }));
            }
         
            customerAddressDto.UpdateDate = DateTime.UtcNow;
            customerAddressDto.UpdateBy = "Tester";        
           _customerAddress.Update(customerAddressDto);

            return Ok(true);
        }
       
    }
}