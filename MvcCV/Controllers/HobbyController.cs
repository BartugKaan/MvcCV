using MvcCV.Models.Entity;
using MvcCV.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace MvcCV.Controllers
{
    public class HobbyController : Controller
    {
        HobbyRepository repository = new HobbyRepository();
        public ActionResult Index()
        {
            var values = repository.GetAllAsList();
            return View(values);
        }

        [HttpGet]
        public ActionResult AddHobby()
        {
            return View();
        }

        [HttpPost]
        public ActionResult AddHobby(TBLHobby responseHobby)
        {
            repository.Add(responseHobby);
            return RedirectToAction("Index");
        }

        public ActionResult Delete(int id)
        {
            var hobby = repository.GetById(id);
            repository.Delete(hobby);
            return RedirectToAction("Index");
        }

        [HttpGet]
        public ActionResult GetHobby(int id)
        {
            TBLHobby hobby = repository.GetById(id);
            return View(hobby);
        }

        [HttpPost]
        public ActionResult GetHobby(TBLHobby responseHobby)
        {
            TBLHobby hobby = repository.Find(x => x.ID == responseHobby.ID);
            hobby.Description = responseHobby.Description;
            hobby.SubDescription = responseHobby.SubDescription;
            repository.Update(hobby);
            return View(hobby);
        }
    }
}