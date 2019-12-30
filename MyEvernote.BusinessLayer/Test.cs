using MyEvernote.DataAccessLayer;
using MyEvernote.DataAccessLayer.EntityFramework;
using MyEvernote.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MyEvernote.BusinessLayer
{

   public class Test
    {
        private Repository<Category> repo_category = new Repository<Category>();
        private Repository<EverNoteUser> repo_user = new Repository<EverNoteUser>();
        public Test()
        {
            //DatabaseContext db = new DatabaseContext();
            ////db.Database.CreateIfNotExists();
            //db.EverNoteUsers.ToList();
            List<Category> categories = repo_category.List();
        }
        public void InsertTest()
        {
            int result = repo_user.Insert(new EverNoteUser()
            {
                Name = "Muhammet",
                SurName = "Tiryaki",
                Email = "matiryaki@gmail.com",
                ActivateGuid = Guid.NewGuid(),
                IsActive = true,
                IsAdmin = false,
                UserName = "muhoo",
                Password = "12345",
                CreatedOn = DateTime.Now,
                ModifiedOn = DateTime.Now.AddMinutes(5),
                ModifiedUserName = "maho"


            });
        }
        public void UpdateTest()
        {
            EverNoteUser user = repo_user.Find(x => x.Name == "Muhammet");
            if (user!=null)
            {
                user.UserName = "muhoo";
                repo_user.Update(user);
            }
        }
        public void DeleteTest()
        {
            EverNoteUser user = repo_user.Find(x => x.UserName == "muhoo");
            if (user != null)
            {
                repo_user.Delete(user);
            }
        }
        
       
	
    }
}
