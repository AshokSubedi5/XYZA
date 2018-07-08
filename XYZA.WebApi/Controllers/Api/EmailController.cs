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
using System.Threading;
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
        Models.MailSettings _setting;

        protected Models.MailSettings settings
        {
            get
            {
                if (_setting == null)
                    return _setting = new Models.MailSettings();
                return _setting;
            }
        }

        public EmailController()
        {
            _customerLayer = new CustomerBLL();
            _templateLayer = new EmailTemplateBLL();
        }

        [HttpGet]
        public HttpResponseMessage Get(string id)
        {
            if (id.ToLower() == "start")
            {
                settings.SendMailAsync(_customerLayer.GetAllCustomer(), _templateLayer.GetEmailTemplate());
            }
            else if (id.ToLower() == "stop")
            {
                settings.CancelMailAsync();
            }

            return Request.CreateResponse(HttpStatusCode.OK);
        }



    }
}