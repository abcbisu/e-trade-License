using etrade;
using etrade.services;
using etrade_server.App_Start;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Web;
using System.Web.Http;

namespace etrade_server.Controllers.ApiService
{
    [SecuredFilter]
    [RoutePrefix("api/v1/Configs")]
    public class ConfigutaionController: BaseController
    {
        [Route("SupportedLanguages")]
        [HttpGet]
        public HttpResponseMessage GetSupportedLanguages()
        {
            var resourceDal = new ResourceService(User.UserId);
            var languages = resourceDal.getSupportedLanguages(SessionContext.DefaultLanguage.Id);
            return  Request.CreateResponse(languages);
        }
        [Route("LocalizedDatas")]
        [HttpPost]
        public HttpResponseMessage GetLocalizedData(List<long>keys)
        {
            var defLang = SessionContext.DefaultLanguage;
            var resourceDal = new ResourceService(User.UserId);
            var languages = resourceDal.GetLocalizedDatas(keys, defLang.Id);
            return Request.CreateResponse(languages);
        }
    }
}