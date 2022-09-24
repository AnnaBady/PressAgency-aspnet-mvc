using Microsoft.AspNet.Identity;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using WebApplication5.Models;
namespace WebApplication5.Controllers
{
    public class PostsController : Controller
    {

        ApplicationDbContext db = new ApplicationDbContext();
        // GET: Editor



        public IEnumerable<Post> posts { get; set; }

        public PostsController()
        {
            posts = db.Posts.ToList();
        }

        // GET: Customer
        public ActionResult Posts()
        {
            // lamda Exp : var_name => this Var_name
            var posts = getpost();

            return View(posts);
        }
        /*----------------------------------------------------------------------------------------------------------*/
        public IEnumerable<Post> getpost()
        {
            var posts = db.Posts.ToList();

            return posts;
        }
        /*----------------------------------------------------------------------------------------------------------*/
        public ActionResult Main()// viewer Read only posts  without any interaction
        {
            return View(db.Posts.Where(x => x.Approve == true).ToList());

        }
        /*----------------------------------------------------------------------------------------------------------*/
        public ActionResult viewerPage()// viewer layout to see all posts and interact  
        {


            return View(db.Posts.Where(x => x.Approve == true).ToList());
        }
        /*----------------------------------------------------------------------------------------------------------*/
        public ActionResult Details(int id) // view post 
        {
            var details = getpost().SingleOrDefault(c => c.post_id == id);
            if (details == null)
            {
                return HttpNotFound();
            }
            if (details.Viewers == null)
            {
                details.Viewers = 0;

            }

            details.Viewers++;
            // db.Posts.Add();
            db.SaveChanges();
            return View(details);
        }
        public ActionResult Editor_Details_posts(int id) // view post 
        {
            var details = getpost().SingleOrDefault(c => c.post_id == id);
            if (details == null)
            {
                return HttpNotFound();
            }
           

            // db.Posts.Add();
            db.SaveChanges();
            return View(details);
        }
        /*----------------------------------------------------------------------------------------------------------*/
        public ActionResult Likes(int id) // view post 
        {
            var details = getpost().SingleOrDefault(c => c.post_id == id);
            if (details.Likes == null)
            {
                details.Likes = 0;

            }

            details.Likes++;
            // db.Posts.Add();
            db.SaveChanges();
            return RedirectToAction("viewerPage");
        }
        /*----------------------------------------------------------------------------------------------------------*/
        public ActionResult DisLikes(int id) // view post 
        {
            var details = getpost().SingleOrDefault(c => c.post_id == id);
            if (details.Dislikes == null)
            {
                details.Dislikes = 0;

            }

            details.Dislikes++;
            // db.Posts.Add();
            db.SaveChanges();
            return RedirectToAction("viewerPage");
        }
        /*----------------------------------------------------------------------------------------------------------*/
        public ActionResult AddPosts()
        {
            return View();
        }
        [HttpPost]
        public ActionResult AddPosts(Post post, HttpPostedFileBase img, Editor editor)
        {
            try
            {
                string path = "";
                if (img != null && img.ContentLength > 0)
                {
                    path = "~/Images/" + Path.GetFileName(img.FileName);
                    img.SaveAs(Server.MapPath(path));
                    post.image = path;
                    db.Posts.Add(post);
                    //post.user= Session["username"].ToString();
                    //editor.post_id = post.post_id;
                    //db.Editors.Add(editor);


                    db.SaveChanges();
                    return RedirectToAction("Posts");

                }


                return View();
            }
            catch
            {
                return View();
            }
        }
        /*----------------------------------------------------------------------------------------------------------*/
        //public ActionResult DisplayInfo()
        //{
        //    var Editor = db.Editors.Include(P=>P.username);
        //    //return View(Editor.ToList());
        //    return View();


        //}
        /*----------------------------------------------------------------------------------------------------------*/
        public ActionResult comment(Question question, int id)
        {
            var details = getpost().SingleOrDefault(c => c.post_id == id);
            question.post_id = id;
            //question.username_Viewer = (string)Session["Email"];
            //var userId = System.Web.HttpContext.Current.Session["username"];
            //question.username_Viewer = userId.ToString();

            //HttpCookie im = Request.Cookies.Get(Constants.  ]]CONSTANT_COOKIES_NAME.ID);
            //if (im == null)
            //{
            //    return false;
            //}

            db.Questions.Add(question);
            db.SaveChanges();
            return View();
           
        }
        /*----------------------------------------------------------------------------------------------------------*/
        //public ActionResult Reply(Question question, int id)
        //{
        //    var details = getpost().SingleOrDefault(c => c.post_id == id);
        //    question.post_id = id;
        //    db.Questions.Add(question);
        //    db.SaveChanges();
        //    return View();

        //}
        [HttpGet]
        public ActionResult Reply(int id)
        {
            var question = db.Questions.SingleOrDefault(c => c.QuestionNumber == id);

            return View(question);
        }

        [HttpPost]
        public ActionResult Reply(Question question)
        {
            //validation
            try
            {
                var questionDB = db.Questions.Single(c => c.QuestionNumber == question.QuestionNumber);
                if (TryUpdateModel(questionDB))
                {
                    questionDB.QuestionNumber = question.QuestionNumber;
                    questionDB.Reply = question.Reply;
                    questionDB.username_Editor = question.username_Editor;


                    db.SaveChanges();
                    return RedirectToAction("RecivedQuestions");
                }
                return View(questionDB);
            }
            catch
            {
                return View();
            }
        }


        /*-----------------------------------------------------------------------------------------------------*/
        public ActionResult save(int id)
        {
            var saved = new Favorite();
            var post = db.Favorites.SingleOrDefault(c => c.post_id == id);
            //if (post != null)
            //{
            //    return RedirectToAction("Main");
            //}
            //else
            //{
            //var userId = System.Web.HttpContext.Current.Session["username"];
            //saved.username_Viewer = userId.ToString();
            saved.post_id = id;
            db.Favorites.Add(saved);
            db.SaveChanges();
            return RedirectToAction("viewerPage");

        }
        /*----------------------------------------------------------------------------------------------------------*/
        public ActionResult DeletePost(int id)
            {

                var post = db.Posts.OrderBy(c => c.post_id == id).Include(c => c.Favorites).First();

                db.Posts.Remove(post);
                db.SaveChanges();

                return RedirectToAction("Posts");
            }



            /*----------------------------------------------------------------------------------------------------------*/
            [HttpGet]
            public ActionResult Edit(int id)
            {
                var post = db.Posts.Single(c => c.post_id == id);

                return View(post);
            }

            [HttpPost]
            public ActionResult Edit(Post post, HttpPostedFileBase img)
            {
                //validation
                try
                {
                    var PostDB = db.Posts.Single(c => c.post_id == post.post_id);
                    if (TryUpdateModel(PostDB))
                    {

                        PostDB.ArticleBody = post.ArticleBody;
                        PostDB.ArticleTitle = post.ArticleTitle;
                        PostDB.Date = post.Date;

                        string path = "";
                        if (img != null && img.ContentLength > 0)
                        {
                            path = "~/Images/" + Path.GetFileName(img.FileName);
                            img.SaveAs(Server.MapPath(path));
                            post.image = path;
                            PostDB.image = post.image;
                        }

                        PostDB.post_id = post.post_id;

                        PostDB.Type = post.Type;

                        db.SaveChanges();
                        return RedirectToAction("Posts");
                    }
                    return View(PostDB);
                }
                catch
                {
                    return View();
                } 
            }
            /*------------------------------------------------------------------------------*/
            public ActionResult RecivedQuestions()
            {
                return View(db.Questions.ToList());
            }

        public ActionResult Recived()
        {
            return View(db.Questions.ToList());
        }
        /*-----------------------------------------------------------------------------*/
        public ActionResult savedposts()
        {
            return View(db.Favorites.ToList());
        }
        [HttpGet]
        public ActionResult Deletesave(int id)
        {
            var post = db.Favorites.Single(c => c.fav_id == id);
            db.Favorites.Remove(post);
            db.SaveChanges();

            return RedirectToAction("savedposts");
        }
    } 
}


