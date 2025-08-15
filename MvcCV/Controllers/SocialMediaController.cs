using MvcCV.Models.Entity;
using MvcCV.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace MvcCV.Controllers
{
    public class SocialMediaController : Controller
    {
        SocialMediaRepository repository = new SocialMediaRepository();
        public ActionResult Index()
        {
            var values = repository.GetAllAsList();
            return View(values);
        }

        [HttpGet]
        public ActionResult Add()
        {
            return View();
        }
        [HttpPost]
        public ActionResult Add(TBLSocialMedia responseSocialMedia)
        {
            repository.Add(responseSocialMedia);
            return RedirectToAction("Index");
        }

        public ActionResult Delete(int id)
        {
            var value = repository.GetById(id);
            repository.Delete(value);
            return RedirectToAction("Index");
        }

        public ActionResult GetSocialMedia(int id)
        {
            var value = repository.GetById(id);
            return View("GetSocailMedia", value);
        }

        [HttpPost]
        public ActionResult GetSocialMedia(TBLSocialMedia responseSocialMedia)
        {
            var value = repository.GetById(responseSocialMedia.ID);
            value.Name = responseSocialMedia.Name;
            value.Link = responseSocialMedia.Link;
            value.Icon = responseSocialMedia.Icon;
            repository.Update(value);
            return RedirectToAction("Index");
        }
    }
}