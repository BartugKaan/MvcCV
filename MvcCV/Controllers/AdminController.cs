using MvcCV.Models.Entity;
using MvcCV.Repositories;
using MvcCV.Helpers;
using MvcCV.Attributes;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace MvcCV.Controllers
{
    [AdminAuth]
    [SecurePage]
    public class AdminController : Controller
    {
        AdminRepository _repository = new AdminRepository();
        
        public ActionResult Index()
        {
            var adminList = _repository.GetAllAsList();
            return View(adminList);
        }

        [HttpGet]
        public ActionResult AddAdmin()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult AddAdmin(TBLAdmin responseAdmin)
        {
            if (ModelState.IsValid)
            {
                try
                {
                    var existingUser = _repository.Find(x => x.Username == responseAdmin.Username);
                    if (existingUser != null)
                    {
                        ViewBag.ErrorMessage = "Bu kullanıcı adı zaten kullanılıyor. Lütfen başka bir kullanıcı adı seçin.";
                        return View(responseAdmin);
                    }

                    if (string.IsNullOrEmpty(responseAdmin.Password) || responseAdmin.Password.Length < 6)
                    {
                        ViewBag.ErrorMessage = "Şifre en az 6 karakter olmalıdır.";
                        return View(responseAdmin);
                    }

                    responseAdmin.Password = PasswordHelper.HashPassword(responseAdmin.Password);
                    _repository.Add(responseAdmin);
                    return RedirectToAction("Index");
                }
                catch (Exception ex)
                {
                    ViewBag.ErrorMessage = "Kullanıcı eklenirken bir hata oluştu: " + ex.Message;
                    return View(responseAdmin);
                }
            }
            return View(responseAdmin);
        }

        public ActionResult Delete(int id)
        {
            try
            {
                int currentUserId = int.Parse(Session["UserId"]?.ToString() ?? "0");
                if (id == currentUserId)
                {
                    ViewBag.ErrorMessage = "Kendi hesabınızı silemezsiniz.";
                    return RedirectToAction("Index");
                }

                var user = _repository.GetById(id);
                if (user != null)
                {
                    _repository.Delete(user);
                }
                return RedirectToAction("Index");
            }
            catch (Exception ex)
            {
                ViewBag.ErrorMessage = "Kullanıcı silinirken bir hata oluştu: " + ex.Message;
                return RedirectToAction("Index");
            }
        }

        [HttpGet]
        public ActionResult GetAdmin(int id)
        {
            TBLAdmin user = _repository.Find(x => x.ID == id);
            if (user == null)
            {
                return RedirectToAction("Index");
            }
            
            user.Password = "";
            return View(user);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult GetAdmin(TBLAdmin responseAdmin)
        {
            if (ModelState.IsValid)
            {
                try
                {
                    TBLAdmin user = _repository.Find(x => x.ID == responseAdmin.ID);
                    if (user == null)
                    {
                        ViewBag.ErrorMessage = "Kullanıcı bulunamadı.";
                        return View(responseAdmin);
                    }

                    var existingUser = _repository.Find(x => x.Username == responseAdmin.Username && x.ID != responseAdmin.ID);
                    if (existingUser != null)
                    {
                        ViewBag.ErrorMessage = "Bu kullanıcı adı zaten başka bir kullanıcı tarafından kullanılıyor.";
                        return View(responseAdmin);
                    }

                    user.Username = responseAdmin.Username;
                    
                    if (!string.IsNullOrEmpty(responseAdmin.Password))
                    {
                        if (responseAdmin.Password.Length < 6)
                        {
                            ViewBag.ErrorMessage = "Şifre en az 6 karakter olmalıdır.";
                            return View(responseAdmin);
                        }
                        user.Password = PasswordHelper.HashPassword(responseAdmin.Password);
                    }
                    
                    _repository.Update(user);
                    ViewBag.SuccessMessage = "Kullanıcı başarıyla güncellendi.";
                    
                    user = _repository.Find(x => x.ID == responseAdmin.ID);
                    user.Password = "";
                    return View(user);
                }
                catch (Exception ex)
                {
                    ViewBag.ErrorMessage = "Kullanıcı güncellenirken bir hata oluştu: " + ex.Message;
                    return View(responseAdmin);
                }
            }
            return View(responseAdmin);
        }
    }
}