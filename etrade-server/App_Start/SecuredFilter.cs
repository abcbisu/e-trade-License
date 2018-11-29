using etrade;
using etrade.services;
using System;
using System.Linq;
using System.Security;
using System.Web.Http.Controllers;
using System.Web.Http.Filters;
using etrade.models;
using System.Configuration;
using System.Text;

namespace etrade_server.App_Start
{
    public class SecuredFilter : ActionFilterAttribute
    {
        private const string tokenKey = "eiipl-token";
        private const string otherConfgKey = "eiipl-other-configs";
        public override void OnActionExecuting(HttpActionContext filterContext)
        {
            var userMin = ConfigurationManager.AppSettings["user-anonymous"].Deserialize<UserMin>() ;
            var provider = new TokenServices(userMin.UserId);
            var resourceService = new ResourceService(userMin.UserId);
            var tokenValue = "";
            var controllerName = filterContext.ActionDescriptor.ControllerDescriptor.ControllerName;
            var actionName = filterContext.ActionDescriptor.ActionName;
            var controllerPackage = GetNameSpace(filterContext);


            try
            {
                try
                {
                    tokenValue = GetToken(filterContext, tokenKey);
                    if (!string.IsNullOrEmpty(tokenValue))
                    {
                        var user = provider.ValidateTokenAndGetUser(tokenValue);
                        userMin = user ?? throw new SecurityException("Token validation failed");
                    }
                    //validate resource grant
                    resourceService.validateUrlPermission(actionName, controllerName, controllerPackage, userMin.UserId);
                    SessionContext.SetUser(userMin);

                    var otherConfig = GetOtherConfigFrmClntSide<OtherConfigs>(filterContext,otherConfgKey);
                    //set language
                    SessionContext.SetDefaultLanguage(otherConfig==null?null: otherConfig.language);
                }
                catch (ArgumentOutOfRangeException)
                {
                    throw new SecurityException("Invalid Authorization data format");
                }
                catch (FormatException)
                {
                    throw new SecurityException("Invalid Authorization data format");
                }
            }
            catch
            {
                throw;
            }
            base.OnActionExecuting(filterContext);
        }
        public override void OnActionExecuted(HttpActionExecutedContext actionExecutedContext)
        {
            base.OnActionExecuted(actionExecutedContext);
        }

        #region private methods
        string GetNameSpace(HttpActionContext context)
        {
            Type t = context.ControllerContext.Controller.GetType();
            return t.Namespace;
        }
        T GetOtherConfigFrmClntSide<T>(HttpActionContext filterContext, string otherConfgKey)
        {
            if (filterContext.Request.Headers.Contains(otherConfgKey))
            {
                var otherConfig = filterContext.Request.Headers.GetValues(otherConfgKey).FirstOrDefault();//base64 string
                if (!string.IsNullOrEmpty(otherConfig))
                {
                    byte[] data = Convert.FromBase64String(otherConfig);
                    string decodedString = Encoding.UTF8.GetString(data);
                    var othConfg = decodedString.Deserialize<T>();//desirialize json
                    return othConfg;
                }
            }
            return default(T);
        }
        public string GetToken(HttpActionContext filterContext,string tokenKey)
        {
            if (filterContext.Request.Headers.Contains(tokenKey))
            {
                var tokn = filterContext.Request.Headers.GetValues(tokenKey).FirstOrDefault();
                if (string.IsNullOrEmpty(tokn))
                    return null;
                else
                    return tokn;
            }
            return null;
        }
        #endregion
    }
}