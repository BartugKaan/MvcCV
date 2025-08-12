using System.Linq;
using System.Web.Mvc;
using MvcCV.Models.Entity;

namespace MvcCV.Controllers
{
    public class DefaultController : Controller
    {
        DbCvEntities db = new DbCvEntities();
        public ActionResult Index()
        {
            var values = db.TBLAbout.ToList();
            return View(values);
        }
    }
}