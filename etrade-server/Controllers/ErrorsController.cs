using etrade;
using etrade_server.Models;
using System.Web.Mvc;

namespace etrade_server.Controllers
{
    public class ErrorsController : Controller
    {
        public ActionResult Er404(string msg)
        {
            var ex = new EntityNotFoundException();
            if (Request.IsAjaxRequest())
            {
                var json = new JsonHttpStatusResult(ex.ExceptionData, ex.HttpStatus);
                json.JsonRequestBehavior = JsonRequestBehavior.AllowGet;
                return json;
            }
            return View(ex.ExceptionData);
        }
    }
}
