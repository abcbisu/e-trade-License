using etrade;
using etrade.services;
using etrade_server.Models;
using System;
using System.Linq;
using System.Security;
using System.Web;
using System.Web.Http.Controllers;
using System.Web.Http.Filters;
using etrade.models;

namespace etrade_server.App_Start
{
    public class SecuredFilter : ActionFilterAttribute
    {
        private const string Token = "Token";
        public override void OnActionExecuting(HttpActionContext filterContext)
        {

            //  Get API key provider
            var userMin = new UserMin() { UserId = 1, LoginType = LoginType.Anonymous };
            var provider = new TokenServices(userMin.UserId);
            var resourceService = new ResourceService(userMin.UserId);
            var tokenValue = "";
            var controllerName = filterContext.ActionDescriptor.ControllerDescriptor.ControllerName;
            var actionName = filterContext.ActionDescriptor.ActionName;
            string controllerPackage = GetNameSpace(filterContext);
            try
            {
                try
                {
                    tokenValue = filterContext.Request.Headers.GetValues(Token).FirstOrDefault();
                }
                catch
                {
                    //skip toke property not supplied
                }
                // Validate Token
                try
                {
                    
                    if (!string.IsNullOrEmpty(tokenValue))
                    {
                        var user = provider.ValidateTokenAndGetUser(tokenValue);
                        userMin = user ?? throw new SecurityException("Token validation failed");
                    }
                    //validate resource grant
                    resourceService.validateUrlPermission(actionName, controllerName, controllerPackage, userMin.UserId);
                    HttpContext.Current.Items.Add(SiteConstants.SessionKey, userMin);
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
        string GetNameSpace(HttpActionContext context)
        {
            Type t = context.ControllerContext.Controller.GetType();
            return t.Namespace;
        }
    }
}