using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
//using System.Web.ModelBinding;
namespace etrade
{
   public static class ExceptionExtensionMethods
    {
        public static void Handle(this Exception ex)
        {
            if (ex.GetType().IsSubclassOf(typeof(AppException)) || ex.GetType() == typeof(AppException))
            {
                throw ex as AppException;
            }
            else
            {
                throw new AppException(ex.Message);
            }
        }
        public static void HandleModelState(this System.Web.Http.ModelBinding.ModelStateDictionary modelState)
        {
            if (!modelState.IsValid)
            {
                var errors = new Dictionary<string, string>();
                modelState.Where(k => k.Value.Errors.Count > 0).ToList().ForEach(i =>
                {
                    var er = string.Join(", ", i.Value.Errors.Select(e => e.ErrorMessage).ToArray());
                    errors.Add(i.Key, er);
                });
                throw new EntityValidationException(errors);
            }
        }
        //public static void HandleModelState(this ModelStateDictionary modelState)
        //{
        //    if (!modelState.IsValid)
        //    {
        //        var errors = new Dictionary<string, string>();
        //        modelState.Where(k => k.Value.Errors.Count > 0).ToList().ForEach(i =>
        //        {
        //            var er = string.Join(", ", i.Value.Errors.Select(e => e.ErrorMessage).ToArray());
        //            errors.Add(i.Key, er);
        //        });
        //        throw new EntityValidationException(errors);
        //    }
        //}
    }
}
