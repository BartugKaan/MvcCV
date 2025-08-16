using MvcCV.Models.Entity;
using MvcCV.Helpers;
using System;
using System.Linq;

namespace MvcCV.Utilities
{
    public static class PasswordMigrationUtility
    {
        public static int MigrateAllPlainTextPasswords()
        {
            int updatedCount = 0;
            
            try
            {
                using (var db = new DbCvEntities())
                {
                    var allAdmins = db.TBLAdmin.ToList();
                    
                    foreach (var admin in allAdmins)
                    {
                        if (!string.IsNullOrEmpty(admin.Password) && !PasswordHelper.IsValidHash(admin.Password))
                        {
                            admin.Password = PasswordHelper.HashPassword(admin.Password);
                            updatedCount++;
                        }
                    }
                    
                    if (updatedCount > 0)
                    {
                        db.SaveChanges();
                    }
                }
            }
            catch (Exception ex)
            {
                throw new Exception($"�ifre migration s�ras�nda hata olu�tu: {ex.Message}", ex);
            }
            
            return updatedCount;
        }

        public static bool MigrateUserPassword(int userId)
        {
            try
            {
                using (var db = new DbCvEntities())
                {
                    var admin = db.TBLAdmin.FirstOrDefault(x => x.ID == userId);
                    
                    if (admin != null && !string.IsNullOrEmpty(admin.Password) && !PasswordHelper.IsValidHash(admin.Password))
                    {
                        admin.Password = PasswordHelper.HashPassword(admin.Password);
                        db.SaveChanges();
                        return true;
                    }
                }
            }
            catch (Exception ex)
            {
                System.Diagnostics.Debug.WriteLine($"Kullan�c� �ifre migration hatas� (ID: {userId}): {ex.Message}");
                return false;
            }
            
            return false;
        }

        public static bool AreAllPasswordsHashed()
        {
            try
            {
                using (var db = new DbCvEntities())
                {
                    var allAdmins = db.TBLAdmin.ToList();
                    
                    foreach (var admin in allAdmins)
                    {
                        if (!string.IsNullOrEmpty(admin.Password) && !PasswordHelper.IsValidHash(admin.Password))
                        {
                            return false;
                        }
                    }
                    
                    return true;
                }
            }
            catch (Exception)
            {
                return false;
            }
        }
    }
}