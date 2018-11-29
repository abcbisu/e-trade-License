using etrade.models;
using etrade_server.App_Start;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Http;

namespace etrade_server.Controllers.ApiService
{
    public class BaseController : ApiController
    {
        public new UserMin User
        {
            get
            {
                return SessionContext.CurrentUser;
            }
        }
    }
}