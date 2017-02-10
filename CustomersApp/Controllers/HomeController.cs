using System.Web.Mvc;

namespace CustomersApp.Controllers
{
    public class HomeController : Controller
    {
        // GET: Home
        public ActionResult Error(string message)
        {
            ViewBag.Message = message;
            return View("Error", ViewBag);
        }
    }
}