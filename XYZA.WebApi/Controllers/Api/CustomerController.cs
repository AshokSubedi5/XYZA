using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using XYZA.Models;
using XYZA.BLDA;
using System.Web.Http.Cors;

namespace XYZA.WebApi.Controllers.Api
{
    [EnableCors(origins: "*", headers: "*", methods: "*")]
    public class CustomerController : ApiController
    {
        private CustomerBLL _customerLayer;

        public CustomerController()
        {
            _customerLayer = new CustomerBLL();
        }

       
        [HttpGet]
        public HttpResponseMessage  Get()
        {
           
            return Request.CreateResponse(HttpStatusCode.OK, _customerLayer.GetAllCustomer());      
        }

        [HttpGet]
        public HttpResponseMessage Get(string id)
        {
            return Request.CreateResponse(HttpStatusCode.OK, _customerLayer.GetCustomer(id));
        }

        [HttpPost]
        public HttpResponseMessage Post(CustomerModels models) {
            bool result = _customerLayer.AddCustomer(models);            
            if(result)
                return Request.CreateResponse(HttpStatusCode.OK);
            else
              return  Request.CreateResponse(HttpStatusCode.InternalServerError);
        }

        [HttpPut]
        public HttpResponseMessage Put(CustomerModels models) {
            bool result = _customerLayer.UpdateCustomer(models);
            if (result)
                return Request.CreateResponse(HttpStatusCode.OK);
            else
                return Request.CreateResponse(HttpStatusCode.InternalServerError);
        }

        [HttpDelete]
        public HttpResponseMessage Delete(string id)
        {
            bool result = _customerLayer.DeleteCustomer(id);
            if (result)
                return Request.CreateResponse(HttpStatusCode.OK);
            else
                return Request.CreateResponse(HttpStatusCode.InternalServerError);
        }

       
        [HttpDelete]
        public HttpResponseMessage Delete(CustomerModels models)
        {
            bool result = _customerLayer.DeleteCustomer(models.Id);
            if (result)
                return Request.CreateResponse(HttpStatusCode.OK);
            else
                return Request.CreateResponse(HttpStatusCode.InternalServerError);
        }


    }
}
