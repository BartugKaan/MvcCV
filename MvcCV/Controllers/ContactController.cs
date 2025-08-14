using MvcCV.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace MvcCV.Controllers
{
    public class ContactController : Controller
    {
        ContactRepository repository = new ContactRepository();

        public ActionResult Index()
        {
            var values = repository.GetAllAsList();
            return View(values);
        }
    }
}