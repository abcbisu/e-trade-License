using etrade.services;
using System;
using System.Net.Http;
using System.Web.Http;
using etrade_server.App_Start;
using etrade.models;
using etrade_server.Models;
using etrade_server.App_Codes.Authenticator;
using etrade;

namespace etrade_server.Controllers.ApiService
{
    [SecuredFilter]
    [RoutePrefix("app/v1/users")]
    public class AuthController : ApiController
    {
        public AuthController()
        {
        }
        [Route("check")]
        [HttpPost]
        public HttpResponseMessage check()
        {
            return Request.CreateResponse(new { text="Hello"});
        }
        [HttpPost]
        [Route("ValidateForLogin")]
        public HttpResponseMessage ValidateForLogin([FromBody]Credenetial credenetial)
        {
            //return unique validation unique code
            var _curUserId = SessionContext.CurrentUser.UserId;
            var _us = new UserService(_curUserId);
            var _user = _us.validateUserCredential(new GeneralUserAuthenticator<UserMin>(credenetial.Idntity, credenetial.Password, credenetial.IdType));
            var _otp = new OtpServer(Convert.ToInt64(_user.UserId));
            return Request.CreateResponse(_otp.RequerstOTP(credenetial.Idntity,credenetial.IdType, "lgn"));
            
        }
        [HttpPost]
        [Route("Login")]
        public HttpResponseMessage Login([FromBody]Login login)
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