using MvcCV.Utilities;
using System;
using System.Web.Mvc;

namespace MvcCV.Controllers
{
    public class PasswordMigrationController : Controller
    {
        public ActionResult Index()
        {
            ViewBag.Message = "�ifre migration i�lemleri";
            return View();
        }

        public ActionResult MigrateAllPasswords()
        {
            try
            {
                int updatedCount = PasswordMigrationUtility.MigrateAllPlainTextPasswords();
                ViewBag.SuccessMessage = $"Migration ba�ar�l�! {updatedCount} adet �ifre hash'lendi.";
                ViewBag.IsSuccess = true;
            }
            catch (Exception ex)
            {
                ViewBag.ErrorMessage = $"Migration s�ras�nda hata olu�tu: {ex.Message}";
                ViewBag.IsSuccess = false;
            }
            
            return View("Index");
        }

        public ActionResult CheckPasswordStatus()
        {
            try
            {
                bool allHashed = PasswordMigrationUtility.AreAllPasswordsHashed();
                
                if (allHashed)
                {
                    ViewBag.StatusMessage = "? T�m �ifreler g�venli hash format�nda!";
                    ViewBag.StatusClass = "alert-success";
                }
                else
                {
                    ViewBag.StatusMessage = "?? Baz� �ifreler hen�z hash format�nda de�il. Migration gerekiyor.";
                    ViewBag.StatusClass = "alert-warning";
                }
            }
            catch (Exception ex)
            {
                ViewBag.StatusMessage = $"? Kontrol s�ras�nda hata olu�tu: {ex.Message}";
                ViewBag.StatusClass = "alert-danger";
            }
            
            return View("Index");
        }
    }
}