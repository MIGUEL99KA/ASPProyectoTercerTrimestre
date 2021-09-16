using System.Web.Mvc;

namespace ASPProyectoTercerTrimestre.Controllers
{
    public class PruebaController : Controller
    {
        [Authorize]
        // GET: Prueba
        public ActionResult Index()
        {
            return View();
        }
    }
}