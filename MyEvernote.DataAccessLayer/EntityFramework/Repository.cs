using MyEvernote.DataAccessLayer;
using MyEvernote.DataAccessLayer.Abstract;
using MyEvernote.Entities;
using MyEverNote.Common;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace MyEvernote.DataAccessLayer.EntityFramework
{
    public class Repository<T>:RepositoryBase,IRepository<T> where T:class
    {
       // private DatabaseContext db = new DatabaseContext();
        private DbSet<T> _objectSet;

        public Repository()
        {
            _objectSet = db.Set<T>(); 
        }
       
        public List<T> List()
        {
            return _objectSet.ToList();
        }
        public List<T> List(Expression<Func<T,bool>> where)
        {
            return _objectSet.Where(where).ToList();
            //db.Categories.Where(x => x.Id == 1).ToList();
        }
        public T Find(Expression<Func<T, bool>> where)
        {
            return _objectSet.FirstOrDefault(where);
            //db.Categories.Where(x => x.Id == 1).ToList();
        }
        public int Insert(T obj)
        {
            _objectSet.Add(obj);

            if(obj is BaseEntity)
            {
                //Base entity türüne bu obj yi casting işlemi yaparak dönüştürüyorum.
                BaseEntity o = obj as BaseEntity;
                DateTime now = DateTime.Now;
                o.CreatedOn = now;
                o.ModifiedOn = now;
                // o.ModifiedUserName = "system";
                o.ModifiedUserName = App.Common.GetCurrentUserName();
            }
            return Save();
        }
        public int Update(T obj)
        {
            //Base entity türüne bu obj yi casting işlemi yaparak dönüştürüyorum.
            BaseEntity o = obj as BaseEntity;
            DateTime now = DateTime.Now;
            o.ModifiedOn = now;
           // o.ModifiedUserName = "system";
            o.ModifiedUserName = App.Common.GetCurrentUserName();
            return Save();
        }
        public int Delete(T obj)
        {
            _objectSet.Remove(obj);
            return Save();

        }
        public int Save()
        {
            return db.SaveChanges();
        }
        public IQueryable<T> ListQueryable()
        {
            return _objectSet.AsQueryable<T>();
        }
    }

}
