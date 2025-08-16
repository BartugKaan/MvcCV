using MvcCV.Utilities;
using System;
using System.Web.Mvc;

namespace MvcCV.Controllers
{
    public class PasswordMigrationController : Controller
    {
        public ActionResult Index()
        {
            ViewBag.Message = "Þifre migration iþlemleri";
            return View();
        }

        public ActionResult MigrateAllPasswords()
        {
            try
            {
                int updatedCount = PasswordMigrationUtility.MigrateAllPlainTextPasswords();
                ViewBag.SuccessMessage = $"Migration baþarýlý! {updatedCount} adet þifre hash'lendi.";
                ViewBag.IsSuccess = true;
            }
            catch (Exception ex)
            {
                ViewBag.ErrorMessage = $"Migration sýrasýnda hata oluþtu: {ex.Message}";
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
                    ViewBag.StatusMessage = "? Tüm þifreler güvenli hash formatýnda!";
                    ViewBag.StatusClass = "alert-success";
                }
                else
                {
                    ViewBag.StatusMessage = "?? Bazý þifreler henüz hash formatýnda deðil. Migration gerekiyor.";
                    ViewBag.StatusClass = "alert-warning";
                }
            }
            catch (Exception ex)
            {
                ViewBag.StatusMessage = $"? Kontrol sýrasýnda hata oluþtu: {ex.Message}";
                ViewBag.StatusClass = "alert-danger";
            }
            
            return View("Index");
        }
    }
}