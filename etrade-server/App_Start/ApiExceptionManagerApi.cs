using etrade;
using etrade_server.Controllers.ApiService;
using System.Net;
using System.Net.Http;
using System.Net.Http.Formatting;
using System.Threading;
using System.Threading.Tasks;
using System.Web.Http;
using System.Web.Http.Controllers;
using System.Web.Http.Dispatcher;
using System.Web.Http.ExceptionHandling;
namespace etrade_server.App_Start
{
    public class ApiExceptionManagerApi : ExceptionHandler//,IExceptionLogger
        {
            public override void Handle(ExceptionHandlerContext context)
            {
                try
                {
                    context.Exception.Handle();
                }
                catch (AppException)
                {
                    AppException ex = (context.Exception as AppException) ?? new AppException(context.Exception.Message);
                    context.Result = new TextPlainErrorResult(Request: context.ExceptionContext.Request, Content: ex.ExceptionData, HttpStatusCode: ex.HttpStatus);
                }
                base.Handle(context);
            }

            public override bool ShouldHandle(ExceptionHandlerContext context)
            {
                return base.ShouldHandle(context);
            }

            public Task LogAsync(ExceptionLoggerContext context, CancellationToken cancellationToken)
            {
                return new Task(null);
            }
        }
        class TextPlainErrorResult : IHttpActionResult
        {
            public HttpRequestMessage _Request { get; private set; }
            public object _Content { get; private set; }
            public HttpStatusCode _HttpStatusCode { get; private set; }
            public TextPlainErrorResult(HttpRequestMessage Request, object Content, HttpStatusCode HttpStatusCode)
            {
                _Request = Request;
                _Content = Content;
                _HttpStatusCode = HttpStatusCode;
            }

            public Task<HttpResponseMessage> ExecuteAsync(CancellationToken cancellationToken)
            {

                HttpResponseMessage response =
                                 new HttpResponseMessage(_HttpStatusCode);
                response.Content = new ObjectContent<object>(_Content, new JsonMediaTypeFormatter());
                response.RequestMessage = _Request;
                return Task.FromResult(response);
            }
        }

        #region 404 api exception
        public class HttpNotFoundAwareControllerActionSelector : ApiControllerActionSelector
        {
            public HttpNotFoundAwareControllerActionSelector()
            {
            }

            public override HttpActionDescriptor SelectAction(HttpControllerContext controllerContext)
            {
                HttpActionDescriptor decriptor = null;
                try
                {
                    decriptor = base.SelectAction(controllerContext);
                }
                catch (HttpResponseException ex)
                {
                    var code = ex.Response.StatusCode;
                    if (code != HttpStatusCode.NotFound && code != HttpStatusCode.MethodNotAllowed)
                        throw;
                    var routeData = controllerContext.RouteData;
                    routeData.Values["action"] = "Handle404";
                    IHttpController httpController = new ErrorController();
                    controllerContext.Controller = httpController;
                    controllerContext.ControllerDescriptor = new HttpControllerDescriptor(controllerContext.Configuration, "Error", httpController.GetType());
                    decriptor = base.SelectAction(controllerContext);
                }
                return decriptor;
            }
        }
        public class HttpNotFoundAwareDefaultHttpControllerSelector : DefaultHttpControllerSelector
        {
            public HttpNotFoundAwareDefaultHttpControllerSelector(HttpConfiguration configuration)
                : base(configuration)
            {
            }
            public override HttpControllerDescriptor SelectController(HttpRequestMessage request)
            {
                HttpControllerDescriptor decriptor = null;
                try
                {
                    decriptor = base.SelectController(request);
                }
                catch (HttpResponseException ex)
                {
                    var code = ex.Response.StatusCode;
                    if (code != HttpStatusCode.NotFound)
                        throw;
                    var routeValues = request.GetRouteData().Values;
                    routeValues["controller"] = "Error";
                    routeValues["action"] = "Handle404";
                    decriptor = base.SelectController(request);
                }
                return decriptor;
            }
        }
        #endregion
    
}