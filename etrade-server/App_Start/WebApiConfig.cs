using System;
using System.Collections.Generic;
using System.Linq;
using System.Web.Http;
using System.Web.Http.Controllers;
using System.Web.Http.Dispatcher;
using System.Web.Http.ExceptionHandling;
using System.Web.Http.Routing;
using System.Web.Http.Cors;
using System.Configuration;
using Newtonsoft.Json;

namespace etrade_server.App_Start
{
    public static class WebApiConfig
    {
        public static void Register(HttpConfiguration config)
        {
            
            EnableCrossSiteRequests(config);
            //config.MapHttpAttributeRoutes(new CentralizedPrefixProvider("api"));
            config.MapHttpAttributeRoutes();
            config.Services.Replace(typeof(IExceptionHandler), new ApiExceptionManagerApi());

            #region handle 404
            //var r = config.Routes.MapHttpRoute(
            //    name: "Error404",
            //    routeTemplate: "{*url}",
            //    defaults: new { controller = "Error", action = "Handle404" }
            //);
            //config.Services.Replace(typeof(IHttpControllerSelector), new HttpNotFoundAwareDefaultHttpControllerSelector(config));
            //config.Services.Replace(typeof(IHttpActionSelector), new HttpNotFoundAwareControllerActionSelector());
            #endregion
        }
        private static void EnableCrossSiteRequests(HttpConfiguration config)
        {
            var corEnabledConfig = ConfigurationManager.AppSettings["cors-config"];
            if (!string.IsNullOrEmpty(corEnabledConfig))
            {
                var configs = JsonConvert.DeserializeObject<Dictionary<string, string>>(corEnabledConfig);
                var cors = new EnableCorsAttribute(
                    origins: (configs.ContainsKey("origins") ?configs["origins"] :"")??"",
                    headers: (configs.ContainsKey("headers") ? configs["headers"] : "") ?? "",
                    methods: (configs.ContainsKey("methods") ? configs["methods"] : "") ?? "");
                config.EnableCors(cors);
            }
        }
    }
    public class CentralizedPrefixProvider : DefaultDirectRouteProvider
    {
        private readonly string _centralizedPrefix;

        public CentralizedPrefixProvider(string centralizedPrefix)
        {
            _centralizedPrefix = centralizedPrefix;
        }

        protected override string GetRoutePrefix(HttpControllerDescriptor controllerDescriptor)
        {
            var existingPrefix = base.GetRoutePrefix(controllerDescriptor);
            if (existingPrefix == null) return _centralizedPrefix;

            return string.Format("{0}/{1}", _centralizedPrefix, existingPrefix);
        }
    }
}
