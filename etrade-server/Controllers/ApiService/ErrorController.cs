using etrade;
using System.Net.Http;
using System.Web.Http;

namespace etrade_server.Controllers.ApiService
{
    [RoutePrefix("Error")]
    public class ErrorController : ApiController
    {
        [Route("Handle404")]
        [HttpGet, HttpPost, HttpPut, HttpDelete, HttpHead, HttpOptions, AcceptVerbs("PATCH")]
        public HttpResponseMessage Handle404()
        {
            throw new EntityNotFoundException();
        }
    }
}