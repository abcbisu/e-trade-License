using etrade;
using etrade_server.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
namespace etrade_server.App_Start
{
    public class MvcErrorHandlerAttr : FilterAttribute, IExceptionFilter
    {
        public void OnException(ExceptionContext filterContext)
        {
            if (filterContext.ExceptionHandled)
                return;
            var appException = new AppException();
            var statusCode = HttpStatusCode.InternalServerError;
            if (filterContext.Exception is HttpException)
            {
                statusCode = (HttpStatusCode)(filterContext.Exception as HttpException).GetHttpCode();
                switch (statusCode)
                {
                    case HttpStatusCode.BadRequest:
                        {
                            appException = new AppException(filterContext.Exception.Message, "ER400", HttpStatusCode.BadRequest);
                            break;
                        }
                    case HttpStatusCode.Unauthorized:
                        {
                            appException = new AuthenticationRequiredException(filterContext.Exception.Message);
                            break;
                        }
                    case HttpStatusCode.Forbidden:
                        {
                            appException = new AccessDeniedException(filterContext.Exception.Message);
                            break;
                        }
                    case HttpStatusCode.InternalServerError:
                        {
                            appException = new AppException(filterContext.Exception.Message);
                            break;
                        }
                    case HttpStatusCode.NotFound:
                        {
                            appException = new EntityNotFoundException(filterContext.Exception.Message);
                            break;
                        }
                }
            }
            else
            {
                try
                {
                    filterContext.Exception.Handle();
                }
                catch (AppException ex)
                {
                    appException = ex;
                }
            }
            filterContext.Exception = appException;
            statusCode = appException.HttpStatus;
            var result = CreateActionResult(filterContext, statusCode);
            filterContext.Result = result;

            // Prepare the response code.
            filterContext.ExceptionHandled = true;
            filterContext.HttpContext.Response.Clear();
            filterContext.HttpContext.Response.StatusCode = (int)statusCode;
            filterContext.HttpContext.Response.TrySkipIisCustomErrors = true;
        }


        protected virtual ActionResult CreateActionResult(ExceptionContext filterContext, HttpStatusCode statusCode)
        {
            var ctx = new ControllerContext(filterContext.RequestContext, filterContext.Controller);
            var statusCodeName = statusCode.ToString();



            var controllerName = (string)filterContext.RouteData.Values["controller"];
            var actionName = (string)filterContext.RouteData.Values["action"];
            var model = new HandleErrorInfo(filterContext.Exception, controllerName, actionName);
            var ex = (model.Exception as AppException) == null ? null : (model.Exception as AppException).ExceptionData;
            string viewName = null;
            try
            {
                viewName = SelectFirstView(ctx,
                                             string.Format("~/Views/Errors/{0}.cshtml", statusCodeName),
                                             "~/Views/Errors/Error.cshtml",
                                             statusCodeName,
                                             "Error");
            }
            catch (InvalidOperationException)
            {
                //view not found
                viewName = null;
            }
            //if ajax request
            if (filterContext.HttpContext.Request.IsAjaxRequest() || string.IsNullOrEmpty(viewName))
            {
                var json = new JsonHttpStatusResult(ex, statusCode);
                json.JsonRequestBehavior = JsonRequestBehavior.AllowGet;
                return json;
            }
            //else render view
            else
            {

                var result = new ViewResult
                {
                    ViewName = viewName,
                    ViewData = new ViewDataDictionary<ExceptionModelBase>(ex),
                };
                result.ViewBag.StatusCode = statusCode;
                return result;
            }

        }
        protected string SelectFirstView(ControllerContext ctx, params string[] viewNames)
        {
            return viewNames.First(view => ViewExists(ctx, view));
        }

        protected bool ViewExists(ControllerContext ctx, string name)
        {
            var result = ViewEngines.Engines.FindView(ctx, name, null);
            return result.View != null;
        }
    }
}