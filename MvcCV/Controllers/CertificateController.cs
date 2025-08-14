using MvcCV.Models.Entity;
using MvcCV.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace MvcCV.Controllers
{
    public class CertificateController : Controller
    {
        CertificateRepository repository = new CertificateRepository();
        public ActionResult Index()
        {
            var values = repository.GetAllAsList();
            return View(values);
        }

        [HttpGet]
        public ActionResult AddCertificate()
        {
            return View();
        }

        [HttpPost]
        public ActionResult AddCertificate(TBLCertificates responseCertificate)
        {
            repository.Add(responseCertificate);
            return RedirectToAction("Index");
        }

        public ActionResult Delete(int id)
        {
            var value = repository.GetById(id);
            repository.Delete(value);
            return RedirectToAction("Index");
        }

        [HttpGet]
        public ActionResult GetCertificate(int id)
        {
            var certificate = repository.GetById(id);
            return View(certificate);
        }

        [HttpPost]
        public ActionResult GetCertificate(TBLCertificates responseCertificate)
        {
            if (!ModelState.IsValid)
            {
                // Show validation errors and keep user input
                return View(responseCertificate);
            }
            var certificate = repository.Find(x => x.ID == responseCertificate.ID);
            if (certificate != null)
            {
                certificate.Description = responseCertificate.Description;
                certificate.Date = responseCertificate.Date;
                repository.Update(certificate);
            }
            return RedirectToAction("Index");
        }
    }
}