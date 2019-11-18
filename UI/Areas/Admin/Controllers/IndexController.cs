using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Model;
using DAL;
using System.Web.Script.Serialization;

namespace UI.Areas.Admin.Controllers
{
    public class IndexController : Controller
    {
        DAL.DALBase db = new DAL.DALBase();

        CMSEntities d = new CMSEntities();
        // GET: Admin/Index
        //首页
        public ActionResult Index()
        {
            return View();
        }

        public ActionResult main()
        {
            return View();
        }
        //main 最新评论
        public ActionResult seleCMS_Comment()
        {
            var a = d.CMS_Comment
                .OrderByDescending(c => c.cmid)
                .Take(3)
                .ToList();
            return Json(a, JsonRequestBehavior.AllowGet);
        }

        //main 最新注册用户
        public ActionResult seleCMS_Userss()
        {
            var a = d.CMS_User
                .OrderByDescending(c => c.uid)
                .Take(3)
                .ToList();
            return Json(a, JsonRequestBehavior.AllowGet);
        }

        //添加文章
        [HttpGet]
        public ActionResult articleadd()
        {
            return View();
        }

        [HttpPost]
        public int articleadd(CMS_ArticleSSS ss)
        {
            int i = 0;
            List<CMS_User> u = d.CMS_User.Where(a => a.uname == ss.uname).ToList();
            foreach (var item in u)
            {
                i = item.uid;
            }
            CMS_Article s = new CMS_Article()
            {
                
                istop = ss.istop,
                ahtml = ss.ahtml,
                author = ss.author,
                cid = ss.cid,
                title = ss.title,
                uid = i,
                hits = 0,
                ctime = DateTime.Now,
                ptime = DateTime.Now,
                comments = 0
            };
            return db.articleadd(s);
        }

        [HttpPost]
        public int articleadds(CMS_Article art)
        {
            art.state = 2;
            if (art.istop == null)
            {
                art.istop = false;
            }
            return db.articleadd(art);
        }

        [HttpGet]
        public ActionResult articleedit(int aid)
        {
            return View(db.GetCMS_ArticleByid(aid));
        }
        [HttpPost]
        public int articleedit(CMS_Article a)
        {
            var b = d.CMS_User.Where(c => c.uid == a.uid).ToList();
            
            foreach (var item in b)
            {
                
            }
           return db.editArticle(a);
        }
        //文章管理
        public ActionResult articlelist()
        {
            return View();
        }


        public ActionResult GetCMS_Article(int page, int rows, DateTime ? stime, DateTime ? etime)
        {
            int count = 0;

            if (stime.ToString() != "" || etime.ToString() != "")
            {
                count = d.CMS_Article.Where(a => a.ctime >= stime || a.ctime <= etime).Count();
            }
            else {
                count = db.V_CMS_ArticleCount();
            }
            Dictionary<string, object> dic = new Dictionary<string, object>();
            dic.Add("total", count);
            dic.Add("rows", db.GetCMS_Articles(page, rows, stime, etime));

            return Json(dic, JsonRequestBehavior.AllowGet);
            
        }

        public ActionResult layout1()
        {
            return View();
        }

        public ActionResult GetCMS_Comment()
        {
            var ls = db.GetV_CMS_Comment();
            //var total = db.getV_CMS_CommentCount();
            //Dictionary<string, object> dic = new Dictionary<string, object>();
            //dic.Add("total", total);
            //dic.Add("rows", ls);
            return Json(ls, JsonRequestBehavior.AllowGet);
        }


        public ActionResult layout2()
        {

            return View();
        }

        public ActionResult GetCMS_User()
        {
            return Json(db.GetCMS_User(), JsonRequestBehavior.AllowGet);
        }



        public int deluser(int uid) {
            return db.deluser(uid);
        }

        public int Adduser(CMS_User u)
        {
            return db.AddUser(u);
        }
        public int editUser(CMS_User u)
        {
            return db.editUser(u);
        }

        //layout1修改评论内容
        public int editcom(CMS_Comment u)
        {
            return db.editcom(u);
        }
        ////layout1修改评论内容
        //public ActionResult seleuid(int uid)
        //{
        //    var ls = d.CMS_User.Where(a => a.uid == uid).ToList();
        //    return Json(ls, JsonRequestBehavior.AllowGet);
        //}
        


        public CMS_User getUserByid(int uid)
        {
            return db.getUserByid(uid);
        }

        public int delArticle(int? aid)
        {
            return db.delArticle(aid);
        }


        public int updateistop(int aid,bool istop) {
            return db.updateistop(aid,istop);
        }

        public int delcomment(int cid){
            return db.delcomment(cid);
        }
        
        public int yidong(int aid,int cid)
        {
            return db.yidong(aid, cid);
        }

        public int yidongss(int aid, int cid)
        {
            return db.yidongss(aid, cid);
        }

        //public ActionResult seleCMS_Article(int page, int rows, DateTime ctime, DateTime ptime)
        //{
        //    //模糊查询Contains
        //    var ls = d.V_CMS_Article.Where(a => a.ctime >=ctime && a.ctime <=ptime).ToList();
        //    return Json(ls, JsonRequestBehavior.AllowGet);

        //    //int count = db.V_CMS_ArticleCount();
        //    //Dictionary<string, object> dic = new Dictionary<string, object>();
        //    //dic.Add("total", count);
        //    //dic.Add("rows", db.GetCMS_Articles(page, rows));

        //    //return Json(dic, JsonRequestBehavior.AllowGet);

        //}


    }
}