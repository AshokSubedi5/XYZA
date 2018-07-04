using Microsoft.AspNet.SignalR.Hubs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Threading.Tasks;
using System.Web.Http;
using System.Web.Http.Cors;
using XYZA.BLDA;
using XYZA.Models;

namespace XYZA.WebApi.Controllers.Api
{
    [EnableCors(origins: "*", headers: "*", methods: "*")]
    public class EmailController : ApiController
    {
        private EmailTemplateBLL _templateLayer;
        private CustomerBLL _customerLayer;

        public EmailController() {
            _customerLayer = new CustomerBLL();
            _templateLayer = new EmailTemplateBLL();
        }

        [HttpGet]
        public HttpResponseMessage Get(string id)
        {
            if(id.ToLower() == "start")
            {
                Models.MailSettings setting = new Models.MailSettings();
                setting.SendMailAsync(_customerLayer.GetAllCustomer(),_templateLayer.GetEmailTemplate());
            }
            else //for details
            {

            }
            return Request.CreateResponse(HttpStatusCode.OK);
        }



        
       
    }
}