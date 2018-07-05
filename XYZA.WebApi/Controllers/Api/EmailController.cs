using Microsoft.ApplicationInsights.Extensibility.Implementation;
using Microsoft.AspNet.SignalR.Hubs;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using System.Web.Http;
using System.Web.Http.Cors;
using XYZA.BLDA;
using XYZA.Models;
using static XYZA.WebApi.Models.MailSettings;

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



        [HttpGet]
        public  HttpResponseMessage Get(string id,string type)
        {
            MailResponse rawJsonFromDb = new MailResponse();
            using (StreamReader r = new StreamReader(System.Web.Hosting.HostingEnvironment.MapPath("~/Log/mail_log.json")))
            {
                string json = r.ReadToEnd();
                rawJsonFromDb = JsonConvert.DeserializeObject<MailResponse>(json);
            }
            return Request.CreateResponse(HttpStatusCode.OK, rawJsonFromDb);            
        }



        
       
    }
}