using FakeData;
using MyEvernote.Entities;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MyEvernote.DataAccessLayer.EntityFramework
{
   public class MyInitiallizer:CreateDatabaseIfNotExists<DatabaseContext>
    {
        protected override void Seed(DatabaseContext context)
        {
            EverNoteUser admin = new EverNoteUser()
            {
                Name="Muhammet",
                SurName="Tiryaki",
                Email="weweew4@gmail.com",
                ActivateGuid=Guid.NewGuid(),
                IsActive=true,
                IsAdmin=true,
                UserName="matiryaki",
                Password="12345",
                CreatedOn=DateTime.Now,
                ModifiedOn=DateTime.Now.AddMinutes(5),
                ModifiedUserName="matiryaki",
                ProfilImageFileName="userImg.jfif"
                

                
            };
            EverNoteUser standartUser = new EverNoteUser()
            {
                Name = "Berat",
                SurName = "Kuru",
                Email = "dfs6134@gmail.com",
                ActivateGuid = Guid.NewGuid(),
                IsActive = true,
                IsAdmin = true,
                UserName = "brtkr",
                Password = "12345",
                CreatedOn = DateTime.Now,
                ModifiedOn = DateTime.Now.AddMinutes(5),
                ModifiedUserName = "matiryaki",
                 ProfilImageFileName = "userImg.jfif"

            };
            context.EverNoteUsers.Add(admin);
            context.EverNoteUsers.Add(standartUser);
            for (int i = 0; i < 8; i++)
            {
                EverNoteUser user = new EverNoteUser()
                {
                    Name = NameData.GetFirstName() ,
                    SurName = NameData.GetSurname(),
                    Email = NetworkData.GetEmail(),
                    ActivateGuid = Guid.NewGuid(),
                    IsActive = true,
                    IsAdmin = true,
                    UserName = $"user{i}",
                    Password = "12345",
                    CreatedOn =DateTimeData.GetDatetime(DateTime.Now.AddYears(-2),DateTime.Now),
                    ModifiedOn = DateTimeData.GetDatetime(DateTime.Now.AddYears(-2), DateTime.Now),
                    ModifiedUserName = "matiryaki",
                    ProfilImageFileName = "userImg.jfif"

                };
                context.EverNoteUsers.Add(user);
            }
            context.SaveChanges();

            //User List For using
            List<EverNoteUser> userList = context.EverNoteUsers.ToList();

            //Ading fake Categories
            for (int i = 0; i < 10; i++)
            {
                Category cat = new Category()
                {
                    Title=PlaceData.GetStreetName(),
                    Description=PlaceData.GetAddress(),
                    CreatedOn=DateTime.Now,
                    ModifiedOn=DateTime.Now,
                    ModifiedUserName="matiryaki"

                };
                context.Categories.Add(cat);
                //Adding fake Notes
                for (int k = 0; k < NumberData.GetNumber(5,9); k++)
                {
                    EverNoteUser owner = userList[NumberData.GetNumber(0, userList.Count - 1)];
                    Note note = new Note()
                    {
                        Title = TextData.GetAlphabetical(NumberData.GetNumber(5,25)),
                        Text = TextData.GetSentences(NumberData.GetNumber(1,3)),
                        Category=cat,
                        IsDraft =false,
                        LikeCount=NumberData.GetNumber(1,9),
                        Owner=owner,
                        CreatedOn = DateTimeData.GetDatetime(DateTime.Now.AddYears(-2), DateTime.Now),
                        ModifiedOn = DateTimeData.GetDatetime(DateTime.Now.AddYears(-2), DateTime.Now),
                        ModifiedUserName=owner.UserName

                    };
                    context.Notes.Add(note);
                    //Adding fake comment
                    for (int j = 0; j < NumberData.GetNumber(3,5); j++)
                    {
                        EverNoteUser comment_owner = userList[NumberData.GetNumber(0, userList.Count - 1)];
                        Comment comment = new Comment()
                        {
                            Text=TextData.GetSentence(),
                            Owner=comment_owner,
                            CreatedOn = DateTimeData.GetDatetime(DateTime.Now.AddYears(-2), DateTime.Now),
                            ModifiedOn = DateTimeData.GetDatetime(DateTime.Now.AddYears(-2), DateTime.Now),
                            ModifiedUserName=comment_owner.UserName,
                           // Note=note

                        };
                        note.Comments.Add(comment);
                        for (int m = 0; m < note.LikeCount; m++)
                        {
                            Liked liked = new Liked()
                            {
                                LikedUser=userList[m]
                            };
                            note.Likes.Add(liked);
                        }

                    }
                }
                context.SaveChanges();
            }
        }
    }
}
