using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Model;
using DAL;

namespace UI.Controllers
{
    public class HomeController : Controller
    {
        DALBase db = new DALBase();
        // GET: Home
        public ActionResult Index()
        {
            if(Session["name"]==null)
            {
                return Content("<script>alert('请先登录！');window.location='/home/login'</script>");
            }
            ViewBag.cpzx = db.GetArticleByCidTopN(2, 6);
            ViewBag.dzfw = db.GetArticleByCidTopN(3, 6);
            ViewBag.cgal = db.GetArticleByCidTopN(4, 6);
            ViewBag.gywm = db.GetArticleByCidTopN(5, 6);
            var ls = db.GetArticleByCidTopN(1, 7);

            return View(ls);
        }

        protected override void Dispose(bool disposing)
        {
            db.Dispose();
            base.Dispose(disposing);
        }

        
        [HttpGet]
        public ActionResult login()
        {
            return View();
        }
        [HttpPost]
        public ActionResult login(string uname,string upwd)
        {
            if(db.CkUser(uname, upwd))
            {
                Session["name"] = uname;
                return Redirect("/home/index");
            }
            else
            {
                return Content("<script>alert('用户名或密码错误！');window.location='/home/login'</script>");
            }
            
        }
        [HttpGet]
        public ActionResult register()
        {
            return View();
        }
        [HttpPost]

        public int register(CMS_User user)
        {
                user.nname = user.uname;
                user.admin = false;
                if (db.AddUser(user) > 0)
                {
                return 1;
                }
                else if (db.AddUser(user) == 0)
                {
                return 0;
                }
                else
                {
                return -1;
                }
            
        }
        [HttpGet]
        public ActionResult list()
        {
            if (Session["name"] == null)
            {
                return Content("<script>alert('请先登录！');window.location='/home/login'</script>");
            }
            return View();
        }

        
        public ActionResult list(int cid,int index,int size)
        {
            var ls = db.getV_CMS_Article(cid, index, size);
            int total = db.getAllCMS_Article().Count();
            Dictionary<string, object> dic = new Dictionary<string, object>();
            dic.Add("total", total);
            dic.Add("rows",ls);
            return Json(dic);
        }

        public ActionResult page(int aid)
        {
            var ls = db.getV_CMS_Comment(aid);
            ViewBag.Count = db.getCMS_CommentCount(aid);
            return View(ls);
        }

        public ActionResult pages(int aid, int index, int size)
        {
            int total = db.getCMS_CommentCount();
            var ls = db.V_CMS_Comment(aid, index, size);
            Dictionary<string, object> dic = new Dictionary<string, object>();
            dic.Add("total", total);
            dic.Add("rows", ls);
            return Json(dic);
        }


        public ActionResult test()
        {
            return View();
        }

        [ValidateInput(false)]
        public ActionResult addComment(CMS_Comment com)
        {
            int uid=0;
            if (Session["name"] != null)
            {
                 CMS_User c= db.Userid(Session["name"].ToString());
                 uid = c.uid;
            }
            com.cmtime = DateTime.Now;
            com.uid = uid;
            V_CMS_Comment v = new V_CMS_Comment()
            {
                nname=Session["name"].ToString(),
                cmtime = com.cmtime,
                
            };
            if(db.addCMS_Comment(com)>0)
            {
                return Json(v);
            }
            else
            {
                return null;
            }
        }

        public bool CkAdmin()
        {
            bool b = false;
            if(Session["name"]!=null)
            {
                b=db.CkAdmin(Session["name"].ToString());
            }
            return b;
        }

        public ActionResult zhuxiao() {
            Session["name"] = null;
            return Redirect("/home/login");
        }



    }
}