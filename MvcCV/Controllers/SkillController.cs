using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using MvcCV.Models.Entity;
using MvcCV.Repositories;

namespace MvcCV.Controllers
{
    public class SkillController : Controller
    {

        SkillsRepository repository = new SkillsRepository();

        public ActionResult Index()
        {
            var skills = repository.GetAllAsList();
            return View(skills);
        }

        [HttpGet]
        public ActionResult AddSkill()
        {
            return View();
        }

        [HttpPost]
        public ActionResult AddSkill(TBLSkill skillResponse)
        {
            repository.Add(skillResponse);
            return RedirectToAction("Index");
        }

        public ActionResult Delete(int id)
        {
            var skill = repository.GetById(id);
            repository.Delete(skill);
            return RedirectToAction("Index");
        }

        public ActionResult GetSkill(int id)
        {
            TBLSkill skill = repository.Find(x => x.ID == id);
            return View(skill);
        }

        [HttpPost]
        public ActionResult GetSkill(TBLSkill skillResponse)
        {
            TBLSkill skill = repository.Find(x => x.ID == skillResponse.ID);
            skill.Skill = skillResponse.Skill;
            repository.Update(skill);
            return RedirectToAction("Index");
        }
    }
}