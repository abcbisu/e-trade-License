using System;
using System.Collections.Generic;
using System.Net;

namespace etrade
{
    public class ExceptionModelBase
    {
        public string Code { get; set; }
        public string Message { get; set; }
    }
    public class ExceptionModelData : ExceptionModelBase
    {
        public Dictionary<string, string> ErrList { get; set; }
        public ExceptionModelData()
        {
            ErrList = new Dictionary<string, string>();
        }
    }
    //ER500
    public class AppException : Exception
    {
        HttpStatusCode _httpStatus = HttpStatusCode.InternalServerError;
        public HttpStatusCode HttpStatus { get { return _httpStatus; } protected set { _httpStatus = value; } }
        public ExceptionModelBase ExceptionData { get; protected set; }
        public AppException(string msg = "Internal server Error", string code = "ER500", HttpStatusCode HttpStatus = HttpStatusCode.InternalServerError) : base(msg)
        {
            ExceptionData = new ExceptionModelBase()
            {
                Code = code,
                Message = msg
            };
            HttpStatus = HttpStatusCode.InternalServerError;
        }
        public AppException(ExceptionModelBase exceptionModel) : base(exceptionModel.Message)
        {
            ExceptionData = exceptionModel;
            HttpStatus = HttpStatusCode.InternalServerError;
        }
        public AppException(ExceptionModelData exceptionModel) : base(exceptionModel.Message)
        {
            ExceptionData = exceptionModel;
            HttpStatus = HttpStatusCode.InternalServerError;
        }
    }
    public class InvalidOTPorIdentity : AppException
    {
        public InvalidOTPorIdentity(): base("Invalid OTP or Identity", "ER510")
        {
            ExceptionData = new ExceptionModelBase()
            {
                Code = "ER510",
                Message = "Invalid OTP or Identity"
            };
            this.HttpStatus = System.Net.HttpStatusCode.Forbidden;
        }
        public InvalidOTPorIdentity(string msg) : base(msg, "ER510")
        {
            HttpStatus = System.Net.HttpStatusCode.Forbidden;
        }
    }
    //"ER403"
    public class AccessDeniedException : AppException
    {
        public AccessDeniedException() : base("Access denied", "ER403")
        {
            this.HttpStatus = System.Net.HttpStatusCode.Forbidden;
        }
        public AccessDeniedException(string msg) : base(msg, "ER403")
        {
            this.HttpStatus = System.Net.HttpStatusCode.Forbidden;
        }
    }
    //ER401
    public class AuthenticationRequiredException : AppException
    {
        public AuthenticationRequiredException() : base("Authentication Required", "ER401")
        {
            ExceptionData = new ExceptionModelBase()
            {
                Code = "ER401",
                Message = "Authentication Required"
            };
            this.HttpStatus = System.Net.HttpStatusCode.Unauthorized;
        }
        public AuthenticationRequiredException(string msg) : base(msg, "ER401")
        {
            ExceptionData = new ExceptionModelBase()
            {
                Code = "ER401",
                Message = msg
            };
            this.HttpStatus = System.Net.HttpStatusCode.Unauthorized;
        }
    }
    //"ER451"
    public class BusinessValidationException : AppException
    {
        public BusinessValidationException() : base("Business logic validation failed", "ER451")
        {
            this.HttpStatus = System.Net.HttpStatusCode.BadRequest;
        }
        public BusinessValidationException(string msg) : base(msg, "ER451")
        {
            this.HttpStatus = System.Net.HttpStatusCode.BadRequest;
        }
    }
    //"ER450"
    public class EntityAlreadyExistException : AppException
    {
        public EntityAlreadyExistException() : base("Entity already Exist", "ER450")
        {
            this.HttpStatus = System.Net.HttpStatusCode.InternalServerError;
        }
        public EntityAlreadyExistException(string msg) : base(msg, "ER450")
        {
            this.HttpStatus = System.Net.HttpStatusCode.InternalServerError;
        }
    }
    //ER404
    public class EntityNotFoundException : AppException
    {
        public EntityNotFoundException() : base("Entity or Resource not found", "ER404")
        {
            this.HttpStatus = System.Net.HttpStatusCode.NotFound;
        }
        public EntityNotFoundException(string msg) : base(msg, "ER404")
        {
            this.HttpStatus = System.Net.HttpStatusCode.NotFound;
        }
    }
    //ER452
    public class EntityValidationException : AppException
    {
        public EntityValidationException() : base(new ExceptionModelData() { ErrList = new Dictionary<string, string>(), Message = "Entity Validation failed", Code = "ER452" })
        {
            this.HttpStatus = System.Net.HttpStatusCode.BadRequest;
        }
        public EntityValidationException(Dictionary<string, string> errors, string msg) : base(new ExceptionModelData() { ErrList = errors, Message = msg, Code = "ER452" })
        {
            this.HttpStatus = System.Net.HttpStatusCode.BadRequest;
        }
        public EntityValidationException(Dictionary<string, string> errors) : base(new ExceptionModelData() { ErrList = errors, Message = "Entity Validation failed", Code = "ER452" })
        {
            this.HttpStatus = System.Net.HttpStatusCode.BadRequest;
        }
    }
}

