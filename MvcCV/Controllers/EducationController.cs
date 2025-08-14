using MvcCV.Models.Entity;
using MvcCV.Repositories;
using System;
using System.Web.Mvc;

namespace MvcCV.Controllers
{
    public class EducationController : Controller
    {
        EducationRepository repository = new EducationRepository();

        public ActionResult Index()
        {
            var values = repository.GetAllAsList();
            return View(values);
        }

        [HttpGet]
        public ActionResult AddEducation()
        {
            return View();
        }

        [HttpPost]
        public ActionResult AddEducation(TBLEducation responseEducation)
        {
            if (!ModelState.IsValid)
            {
                // Return the same view with validation messages
                return View("AddEducation", responseEducation);
            }
            repository.Add(responseEducation);
            return RedirectToAction("Index");
        }

        public ActionResult Delete(int id)
        {
            var value = repository.GetById(id);
            repository.Delete(value);
            return RedirectToAction("Index");
        }

        [HttpGet]
        public ActionResult GetEducation(int id)
        {
            TBLEducation education = repository.Find(x => x.ID == id);
            if (education == null)
            {
                return HttpNotFound();
            }
            return View(education);
        }

        [HttpPost]
        public ActionResult GetEducation(TBLEducation responseEducation)
        {
            if (!ModelState.IsValid)
            {
                // Show validation errors and keep user input
                return View(responseEducation);
            }

            TBLEducation education = repository.Find(x => x.ID == responseEducation.ID);
            if (education == null)
            {
                return HttpNotFound();
            }

            education.Title = responseEducation.Title;
            education.Subtitle = responseEducation.Subtitle;
            education.OptionalSubtitle = responseEducation.OptionalSubtitle;
            education.Gpa = responseEducation.Gpa;
            education.Date = responseEducation.Date;
            repository.Update(education);
            return RedirectToAction("Index");
        }
    }
}