using MvcCV.Models.Entity;
using MvcCV.Repositories;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.IO;
using System;

namespace MvcCV.Controllers
{
    public class AboutMeController : Controller
    {
        AboutRepository repository = new AboutRepository();

        [HttpGet]
        public ActionResult Index()
        {
            // Load the single About record if it exists; otherwise provide an empty model for creation
            var about = repository.GetAllAsList().FirstOrDefault() ?? new TBLAbout();
            return View(about);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Index(TBLAbout model, HttpPostedFileBase ImageFile, string ExternalImageUrl)
        {
            if (!ModelState.IsValid)
            {
                return View(model);
            }

            // Determine image value priority: External URL > Uploaded file > Existing hidden value
            string imageValue = model.Image; // hidden keeps existing if nothing new provided

            if (!string.IsNullOrWhiteSpace(ExternalImageUrl))
            {
                imageValue = ExternalImageUrl.Trim();
            }
            else if (ImageFile != null && ImageFile.ContentLength > 0)
            {
                var uploadsRoot = Server.MapPath("~/Uploads/AboutMe");
                if (!Directory.Exists(uploadsRoot))
                {
                    Directory.CreateDirectory(uploadsRoot);
                }

                var ext = Path.GetExtension(ImageFile.FileName);
                var safeExt = string.IsNullOrEmpty(ext) ? ".jpg" : ext; // default
                var fileName = $"about_{Guid.NewGuid():N}{safeExt}";
                var savePath = Path.Combine(uploadsRoot, fileName);
                ImageFile.SaveAs(savePath);

                imageValue = fileName; // store relative filename for local images
            }

            // Try to find by ID first
            var existing = model.ID > 0 ? repository.Find(x => x.ID == model.ID) : null;

            if (existing != null)
            {
                // Update existing record
                existing.Name = model.Name;
                existing.LastName = model.LastName;
                existing.Address = model.Address;
                existing.Phone = model.Phone;
                existing.Mail = model.Mail;
                existing.Description = model.Description;
                existing.Image = imageValue;
                repository.Update(existing);
            }
            else
            {
                // If an About record already exists, update that one instead of adding another
                var first = repository.GetAllAsList().FirstOrDefault();
                if (first != null)
                {
                    first.Name = model.Name;
                    first.LastName = model.LastName;
                    first.Address = model.Address;
                    first.Phone = model.Phone;
                    first.Mail = model.Mail;
                    first.Description = model.Description;
                    first.Image = imageValue;
                    repository.Update(first);
                }
                else
                {
                    // No record exists, create a new one
                    model.Image = imageValue;
                    repository.Add(model);
                }
            }

            return RedirectToAction("Index");
        }
    }
}