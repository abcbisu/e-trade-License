using etrade;
using etrade.services;
using etrade_server.Models;
using System;
using System.Linq;
using System.Security;
using System.Web;
using System.Web.Http.Controllers;
using System.Web.Http.Filters;

namespace etrade_server.App_Start
{
    public class SecuredFilter : ActionFilterAttribute
    {
        private const string Token = "Token";
        public override void OnActionExecuting(HttpActionContext filterContext)
        {

            //  Get API key provider
            var provider = new TokenServices();
            var resourceService = new ResourceService();
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
                var arr = tokenValue.Split(':');
                try
                {
                    var userMin = new UserMin() { UserId = 2, UserType = UserType.Anonymous };
                    if (!string.IsNullOrEmpty(tokenValue))
                    {
                        //authorized user
                        string tokenString = arr[1];
                        long userId = Convert.ToInt64(arr[0]);
                        var user = provider.ValidateToken(arr[1], userId);
                        if (user == null)
                        {
                            throw new SecurityException("Token validation failed");
                        }
                        userMin = new UserMin() { UserId = user.UserId, UserType = user.UserType };
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