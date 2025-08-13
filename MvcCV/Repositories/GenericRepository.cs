using MvcCV.Models.Entity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
namespace MvcCV.Repositories
{
    public class GenericRepository<T> where T: class, new()
    {
        DbCvEntities db = new DbCvEntities();

        public List<T> GetAllAsList()
        {
            return db.Set<T>().ToList();
        }

        public void Add(T entity)
        {
            db.Set<T>().Add(entity);
            db.SaveChanges();
        }

        public void Delete(T entit)
        {
            db.Set<T>().Remove(entit);
            db.SaveChanges();
        }

        public T GetById(int id)
        {
            return db.Set<T>().Find(id);
        }

        public void Update(T entity)
        {
            db.SaveChanges();
        }

        public T Find(Expression<Func<T, bool>> filter)
        {
            return db.Set<T>().FirstOrDefault(filter);
        }


    }
}