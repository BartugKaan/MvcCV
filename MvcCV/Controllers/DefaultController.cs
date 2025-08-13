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

        public PartialViewResult Experience()
        {
            var values = db.TBLExperience.ToList();
            return PartialView(values);
        }

        public PartialViewResult Education()
        {
            var values = db.TBLEducation.ToList();
            return PartialView(values);
        }

        public PartialViewResult Skills()
        {
            var values = db.TBLSkill.ToList();
            return PartialView(values);
        }

        public PartialViewResult Hobbies()
        {
            var values = db.TBLHobby.ToList();
            return PartialView(values);
        }

        public PartialViewResult Certificates()
        {
            var values = db.TBLCertificates.ToList();
            return PartialView(values);
        }

        [HttpGet]
        public PartialViewResult Contact()
        {
            return PartialView();
        }

        [HttpPost]
        public PartialViewResult Contact(TBLContact values)
        {
            values.Date = System.DateTime.Now;
            db.TBLContact.Add(values);
            db.SaveChanges();

            return PartialView();
        }

    }
}