using etrade.services;
using System;
using System.Net.Http;
using System.Web.Http;
using etrade_server.App_Start;
using etrade.models;
using etrade_server.Models;
using etrade;

namespace etrade_server.Controllers.ApiService
{
    [SecuredFilter]
    [RoutePrefix("api/v1/users")]
    public class AuthController : BaseController
    {
        public AuthController()
        {
        }
        [HttpPost]
        [Route("Login")]
        public HttpResponseMessage ValidateForLogin([FromBody]Credenetial credenetial)
        {
            var _curUserId = SessionContext.CurrentUser.UserId;
            var _us = new UserService(_curUserId);
            var result = _us.TryToLogin(credenetial);
            return Request.CreateResponse(result);
        }
        [HttpPost]
        [Route("LoginWithOtp")]
        public HttpResponseMessage LoginWithOtp([FromBody]Login login)
        {
            var _curUserId = SessionContext.CurrentUser.UserId;
            login = login != null ? login : throw new EntityValidationException(null,"Credential Required");
            var userToken = new UserService(_curUserId).GetNewAuthToken(login.Idntity, login.IdType,login.Otp);
            return Request.CreateResponse(userToken);
        }
        [HttpPost]
        [Route("Logout")]
        public HttpResponseMessage Logout([FromBody]Login login)
        {
            var _curUserId = SessionContext.CurrentUser.UserId;
            var tokenSrv = new TokenServices(_curUserId);
            tokenSrv.Kill();
            return Request.CreateResponse(new { Message = "Succesfully Logout!!" });
        }
        [HttpPost]
        [Route("RequestPassReco")]
        public HttpResponseMessage RequestPassReco([FromBody]Identity identity)
        {
            var _curUserId = SessionContext.CurrentUser.UserId;
            var _otp = new OtpServer(_curUserId);
            _otp.RequerstOTP(identity.Idntity, identity.IdType, "RecvPass");
            return Request.CreateResponse(new { Message = "OTP sent, your OTP is valid for next 1 minute." }); ;
        }
        [HttpPost]
        [Route("RecoverPassword")]
        public HttpResponseMessage RecoverPassword([FromBody] RecoPassword data)
        {
            var _curUserId = SessionContext.CurrentUser.UserId;
            var _us = new UserService(_curUserId);
            var otpsrv = new OtpServer(_curUserId);
            otpsrv.VerifyOtp(data.Idntity.ReplaceNullIfEmpty(), data.IdType, data.Otp.ReplaceNullIfEmpty());
            _us.RecoverPassword(data.Idntity.ReplaceNullIfEmpty(), data.IdType, data.Password.ReplaceNullIfEmpty());
            return Request.CreateResponse(new { Message = "Your password recovered successfully." });
            
        }

        [HttpPost]
        public HttpResponseMessage SendOtp([FromBody] Identity identity)
        {
            throw new NotImplementedException();
        }


        [HttpGet]
        [Route("CheckAuthosrised")]
        public HttpResponseMessage CheckAuthosrised()
        {
            return Request.CreateResponse(new { Message = "CheckAuthosrised successfully execed." });
        }
    }
}