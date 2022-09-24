using Microsoft.AspNet.Identity;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using WebApplication5.Models;

namespace WebApplication5.Controllers
{
    public class UserController : Controller
    {
       
        ApplicationDbContext db = new ApplicationDbContext();
        public ActionResult Register()
        {
            Viewer viewer = new Viewer();

            return View(viewer);
        }
        [HttpPost]
        public ActionResult Register(Viewer viewer, Admin admin, Editor editor, HttpPostedFileBase img)
        {
            if (db.Viewers.Any(X => X.username == viewer.username))
            {
                ViewBag.DuplicateMessage = "Username Already Exist";
                return View("Register", viewer);
            }
            if (viewer.Role == "Viewer")
            {
                string path = "";
                if (img != null && img.ContentLength > 0)
                {
                    path = "~/Images/" + Path.GetFileName(img.FileName);
                    img.SaveAs(Server.MapPath(path));
                    viewer.Photo = path;
                    db.SaveChanges();
                }
                db.Viewers.Add(viewer);

                db.SaveChanges();
            }
            else if (viewer.Role == "Admin")
            {
                string path = "";
                if (img != null && img.ContentLength > 0)
                {
                    path = "~/Images/" + Path.GetFileName(img.FileName);
                    img.SaveAs(Server.MapPath(path));
                    admin.Photo = path;
                    db.SaveChanges();
                }

                db.Admins.Add(admin);

                db.SaveChanges();
            }



            else if (viewer.Role == "Editor")
            {
                string path = "";
                if (img != null && img.ContentLength > 0)
                {
                    path = "~/Images/" + Path.GetFileName(img.FileName);
                    img.SaveAs(Server.MapPath(path));
                    editor.Photo = path;
                    db.SaveChanges();
                }
                db.Editors.Add(editor);

                db.SaveChanges();
            }
            ModelState.Clear();
            ViewBag.SuccessMessage = "Registeration is successful";
            return RedirectToAction("Main", "Posts");
        }
        [HttpGet]
        public ActionResult Login()
        {

            return View();
        }
        [HttpPost]
        public ActionResult Login(Viewer viewer, Admin admin, Editor editor)
        {
            if (viewer.Role == "Admin")
            {
                var user = db.Admins.Where(x => x.Email == admin.Email && x.Password == viewer.Password && x.Role == viewer.Role).Count();
                if (user > 0)
                {
                    ModelState.Clear();
                    ViewBag.SuccessMessage = "Login successful";
                    return RedirectToAction("Dashboard");
                }
            }
            else if (viewer.Role == "Viewer")
            {
                var user = db.Viewers.Where(x => x.Email == viewer.Email && x.Password == viewer.Password && x.Role == viewer.Role).Count();
                if (user > 0)
                {
                    ModelState.Clear();
                    ViewBag.SuccessMessage = "Login successful";
                    return RedirectToAction("viewerPage", "Posts");


                }
            }
            else if (viewer.Role == "Editor")
            {
                var user = db.Editors.Where(x => x.Email == editor.Email && x.Password == viewer.Password && x.Role == viewer.Role).Count();
                if (user > 0)
                {
                    ModelState.Clear();
                    ViewBag.SuccessMessage = "Login successful";
                    return View("Editorlayout");

                }
            }

            else
            {
                ModelState.Clear();
                ViewBag.FailMessage = "Login Failed Check Your Information";


            }
            return View("Main", "Posts");
        }
        public ActionResult Dashboard()
        {
            return View();


        }
        public ActionResult viewerlayout()
        {
            return View();
        }
        public ActionResult Editorlayout()
        {
            return View();



        }


        [HttpPost]
        public ActionResult Logout()
        {
            Session.Abandon();
            return RedirectToAction("Main", "Posts");

        }
        /*------------------------------------------*/
        public IEnumerable<Editor> getEditors()
        {
            var editors = db.Editors.ToList();

            return editors;
        }
        public ActionResult EditorProfile()
        {
            var editors = getEditors().ToList();

            return View(editors);
        }
            

        //Edit Editor
        [HttpGet]
        public ActionResult EditEditor(int id)
        {
            var editor = db.Editors.Single(c => c.id == id);

            return View(editor);
        }

        [HttpPost]
        public ActionResult EditEditor(Editor editor, HttpPostedFileBase img)
        {
            //validation
            try
            {
                var editorDB = db.Editors.Single(c => c.id == editor.id);
                if (TryUpdateModel(editorDB))
                {
                    editorDB.Email = editor.Email;
                    editorDB.FirstName = editor.FirstName;
                    editorDB.LastName = editor.LastName;
                    editorDB.Password = editor.Password;
                    editorDB.PhoneNO = editor.PhoneNO;
                    string path = "";

                    if (img != null && img.ContentLength > 0)
                    {
                        path = "~/Images/" + Path.GetFileName(img.FileName);
                        img.SaveAs(Server.MapPath(path));
                        editor.Photo = path;
                        editorDB.Photo = editor.Photo;
                    }

                

                    editorDB.id = editor.id;
                        editorDB.username = editor.username;

                        db.SaveChanges();
                        return RedirectToAction("EditorProfile");
                    }
                
                    return View(editorDB);
                
            }
            catch
            {
                return View();
            }
        }

    }
}