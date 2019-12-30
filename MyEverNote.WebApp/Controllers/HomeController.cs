using MyEvernote.BusinessLayer;
using MyEvernote.Entities;
using MyEvernote.Entities.Messages;
using MyEvernote.Entities.ValueObject;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;

namespace MyEverNote.WebApp.Controllers
{
    public class HomeController : Controller
    {
        NoteManager nm = new NoteManager();

        Test test = new Test();
        // GET: Home
        public ActionResult Index()
        {

            //MyEvernote.BusinessLayer.Test test = new MyEvernote.BusinessLayer.Test();
            ////test.InsertTest();
            ////test.UpdateTest();
            //test.DeleteTest();
            //return View();
            return View(nm.GetAllNote().OrderByDescending(x => x.ModifiedOn).ToList());
        }
        public ActionResult ByCategory(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            CategoryManager cm = new CategoryManager();
            Category cat = cm.GetCategoryById(id.Value); //tip uyuşmazlığını önlemek için .value kullandık
            if (cat==null)
            {
                return HttpNotFound();
            }
            return View("Index", cat.Notes.OrderByDescending(x => x.ModifiedOn).ToList());


        }
        public ActionResult MostLiked()
        {
            return View("Index", nm.GetAllNote().OrderByDescending(x => x.LikeCount).ToList());
        }
        public ActionResult About()
        {
            return View();
        }
        [HttpGet]
        public ActionResult Login()
        {
            return View();
        }
        [HttpPost]
        public ActionResult Login(LoginViewModel model)
        {
            if (ModelState.IsValid)
            {
                EverNoteUserManager eum = new EverNoteUserManager();
                BusinessLayerResult<EverNoteUser> res = eum.LoginUser(model);
                if (res.Errors.Count>0)
                {
                    if (res.Errors.Find(x=>x.Code==ErrorMessageCode.UserIsNotActive)!=null)
                    {
                        ViewBag.SetLink = "http://Home/Active/1234-4567-789000";
                    }
                    res.Errors.ForEach(x => ModelState.AddModelError("", x.Message));
                    return View(model);
                }
                Session["login"] = res.Result;
                return RedirectToAction("Index");
            }
           
            return View();
        }
        public ActionResult Register()
        {
           
            return View();
        }
        [HttpPost]
        public ActionResult Register(RegisterViewModel model)
        {
            if (ModelState.IsValid)
            {
                EverNoteUserManager eum = new EverNoteUserManager();
                BusinessLayerResult<EverNoteUser> res = eum.RegisterUser(model);
                if (res.Errors.Count>0)
                {
                    res.Errors.ForEach(x => ModelState.AddModelError("", x.Message));
                    return View(model);
                }
                return RedirectToAction("RegisterOk");
                /* Yöntem-1 */
                //bool hasError = false;
                //if (model.UserName=="aaa")
                //{
                //    ModelState.AddModelError("", "Kullanıcı adı kullanılıyor.");
                //    hasError = true;
                //}
                //if (model.Email=="aaa@aaa.com")
                //{
                //    ModelState.AddModelError("", "Email adresi kullanılıyor.");
                //    hasError = true;
                //}
                //if (hasError) return View(model);
                //return RedirectToAction("RegisterOk");

                /* Yöntem-1-Son */


                /*Yöntem-2*/

                //EverNoteUserManager eum = new EverNoteUserManager();
                //EverNoteUser user = null;
                //try
                //{
                //    user = eum.RegisterUser(model);
                //}
                //catch (Exception ex)
                //{

                //    ModelState.AddModelError("", ex.Message);
                //}
                //if (user==null)
                //{
                //    return View(model);
                //}
                //return RedirectToAction("RegisterOk");



                /*Yöntem-2-Son*/

            }

            return View(model);
        }
        public ActionResult Logout()
        {
            Session.Clear();
            return RedirectToAction("Index");
        }
        public ActionResult RegisterOk()
        {
            return View();
        }
        public ActionResult UserActivate(Guid id)
        {
            EverNoteUserManager eum = new EverNoteUserManager();
            BusinessLayerResult<EverNoteUser> res = eum.ActivateUser(id);
            if (res.Errors.Count>0)
            {
                TempData["errors"] = res.Errors;
                return RedirectToAction("UserActivateCancel");
            }
            return RedirectToAction("UserActivateOk");
        }
        public ActionResult UserActivateOk()
        {
            return View();
        }
        public ActionResult UserActivateCancel()
        {
            List<ErrorMessageObj> errors = null;
            if (TempData["errors"]!=null) 
            {
                errors = TempData["errors"] as List<ErrorMessageObj>;
            }
            return View(errors);
        }
        public ActionResult ShowProfile()
        {
            EverNoteUser currentUser = Session["login"] as EverNoteUser;
            EverNoteUserManager eum = new EverNoteUserManager();
            BusinessLayerResult<EverNoteUser> res = eum.GetUserById(currentUser.Id);
            if (res.Errors.Count>0)
            {
                //Kullanıcıyı hata ekranına gönder
            }
            return View(res.Result);
        }
        
        public ActionResult EditProfile()
        {
            return View();
        }
        [HttpPost]
        public ActionResult EditProfile(EverNoteUser user)
        {
            return View();
        }
        public ActionResult RemoveProfile()
        {
            return View();
        }
    }
}