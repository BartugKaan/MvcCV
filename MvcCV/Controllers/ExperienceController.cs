using MvcCV.Models.Entity;
using MvcCV.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using WebGrease.Activities;

namespace MvcCV.Controllers
{
    public class ExperienceController : Controller
    {
        ExperienceRepository experienceRepository = new ExperienceRepository();
        public ActionResult Index()
        {
            var values = experienceRepository.GetAllAsList();
            return View(values);
        }

        [HttpGet]
        public ActionResult AddExperience()
        {
            return View();
        }

        [HttpPost]
        public ActionResult AddExperience(TBLExperience experience)
        {
            experienceRepository.Add(experience);
            return RedirectToAction("Index");
        }

        public ActionResult Delete(int id)
        {
            var experience = experienceRepository.GetById(id);
            experienceRepository.Delete(experience);
            return RedirectToAction("Index");
        }

        [HttpGet]
        public ActionResult GetExperience(int id)
        {
            TBLExperience experience = experienceRepository.Find(x => x.ID == id);
            return View(experience);
        }
    }
}