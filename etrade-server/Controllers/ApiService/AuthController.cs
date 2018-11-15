using etrade_server.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Web;
using System.Web.Http;

namespace etrade_server.Controllers.ApiService
{
    [RoutePrefix("user")]
    public class AuthController:ApiController
    {
        [HttpPost]
        public HttpResponseMessage Validate([FromBody]Credenetial credenetial)
        {
            //return unique validation unique code
            throw new NotImplementedException();
        }
        [HttpPost]
        public HttpResponseMessage Login([FromBody]Login login)
        {
            throw new NotImplementedException();
        }
        [HttpPost]
        public HttpResponseMessage RequestPassReco([FromBody]Identity identity)
        {
            throw new NotImplementedException();
        }
        [HttpPost]
        public HttpResponseMessage RecoverPassword([FromBody] RecoPassword data)
        {
            throw new NotImplementedException();
        }

        [HttpPost]
        public HttpResponseMessage SendOtp([FromBody] Identity identity)
        {
            throw new NotImplementedException();
        }

    }
}