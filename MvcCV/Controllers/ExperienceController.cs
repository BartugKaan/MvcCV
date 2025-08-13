using MvcCV.Models.Entity;
using MvcCV.Repositories;
using System.Web.Mvc;

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
        public ActionResult AddExperience(TBLExperience responseExperience)
        {
            experienceRepository.Add(responseExperience);
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

        [HttpPost]
        public ActionResult GetExperience(TBLExperience responseExperience)
        {
            TBLExperience experience = experienceRepository.Find(x => x.ID == responseExperience.ID);
            experience.Title = responseExperience.Title;
            experience.Subtitle = responseExperience.Subtitle;
            experience.Description = responseExperience.Description;
            experience.Date = responseExperience.Date;
            experienceRepository.Update(experience);
            return View(experience);
        }
    }
}