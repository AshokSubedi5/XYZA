using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using System.Web.Http.Cors;
using XYZA.BLDA;
using XYZA.Models;

namespace XYZA.WebApi.Controllers.Api
{
    [EnableCors(origins: "*", headers: "*", methods: "*")]
    public class EmailTemplateController : ApiController
    {

        private EmailTemplateBLL _templateLayer;

        public EmailTemplateController()
        {
            _templateLayer = new EmailTemplateBLL();
        }

        [HttpGet]
        public HttpResponseMessage Get()
        {
            return Request.CreateResponse(HttpStatusCode.OK, _templateLayer.GetEmailTemplate());
        }

       
        [HttpPut]
        public HttpResponseMessage Put(EmailTemplate models)
        {
            bool result = _templateLayer.UpdateEmailTemplate(models);
            if (result)
                return Request.CreateResponse(HttpStatusCode.OK);
            else
                return Request.CreateResponse(HttpStatusCode.InternalServerError);
        }

    }
}