using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Model;

namespace DAL
{
    public class DALBase : IDisposable
    {
        CMSEntities db = new CMSEntities();
        
        public List<CMS_Category> getAllCategory() 
        {
            return db.CMS_Category.ToList();
        }

        public List<CMS_Article> GetArticleByCidTopN(int cid,int top)
        {
            
            return db.CMS_Article.OrderByDescending(c => c.title)
                .Where(c => c.state == 2 && c.cid == cid).Take(top).ToList();
        }
        //登录
        public bool CkUser(string uname,string upwd)
        {
            var ls= db.CMS_User.Any(c => c.uname == uname && c.upwd == upwd);
            
            if(ls)
            {
                return true;
            }
            else
            {
                return false;
            }
        }
        //注册
        public int AddUser(CMS_User user)
        {
           var ls= db.CMS_User.Any(c => c.uname == user.uname);
            if(ls)
            {
                return 0;
            }
            db.CMS_User.Add(user);
            if(db.SaveChanges()>0)
            {
                return 1;
            }
            else
            {
                return -1;
            }
        }


        public List<CMS_Article> getAllCMS_Article() { 
            return db.CMS_Article.ToList();
        }

        public int getCMS_CommentCount()
        {
            return db.CMS_Comment.Count();
        }
        public int getV_CMS_CommentCount()
        {
            return db.CMS_Comment.Count();
        }
        public int getCMS_CommentCount(int aid)
        {
            return db.CMS_Comment.Where(c=>c.aid==aid).Count();
        }
        public List<V_CMS_Article> getV_CMS_Article(int cid,int index,int size)
        {
           var ls= db.V_CMS_Article
                .Where(c => c.cid == cid).OrderBy(c=>c.cid).Skip((index - 1) * size).Take(size).ToList();
            return ls;
        }

        public V_CMS_Article getV_CMS_Comment(int aid)
        {
             V_CMS_Article v= db.V_CMS_Article.Where(a => a.aid == aid).Single();
             return v;
        }


        public List<V_CMS_Comment> V_CMS_Comment(int aid, int index, int size)
        {
            var ls = db.V_CMS_Comment.Where(c=>c.aid==aid)
                 .OrderBy(c => c.aid).Skip((index - 1) * size).Take(size).ToList();
            return ls;
        }

        public List<V_CMS_Comment> GetV_CMS_Comment()
        {
            var ls = db.V_CMS_Comment
                 .ToList();
            return ls;
        }

        public CMS_User Userid(string name) {
           return db.CMS_User.Where(c => c.uname == name).Single();
        }

        public int addCMS_Comment(CMS_Comment com) {
            db.CMS_Comment.Add(com);
            return db.SaveChanges();
        }

        public int articleadd(CMS_Article art) {
            db.CMS_Article.Add(art);
            return db.SaveChanges();
        }

        public List<CMS_User> GetCMS_User() {
           return db.CMS_User.ToList();
        }

        public int deluser(int uid) {
            CMS_User user= db.CMS_User.Find(uid);
            db.CMS_User.Remove(user);
            return db.SaveChanges();
        }

        public int Adduser(CMS_User u) {
            db.CMS_User.Add(u);
            return db.SaveChanges();
        }
        public int editUser(CMS_User u)
        {
            db.Entry<CMS_User>(u).State = System.Data.Entity.EntityState.Modified;
            return db.SaveChanges();
        }

        //layout1 评论内容修改
        public int editcom(CMS_Comment u)
        {
            db.Entry<CMS_Comment>(u).State = System.Data.Entity.EntityState.Modified;
            return db.SaveChanges();
        }
        

        public CMS_User getUserByid(int uid)
        {
            CMS_User u= db.CMS_User.Find(uid);
            return u;
        }

        public bool CkAdmin(string uname) {
           return Convert.ToBoolean(db.CMS_User.Where(c => c.uname == uname).Single().admin);
        }

        public List<V_CMS_Article> GetCMS_Articles(int page,int rows, DateTime? ctime, DateTime? ptime)
        {
            if (ctime.ToString()!="" && ptime.ToString()!="") {
                return db.V_CMS_Article.OrderByDescending(c => c.aid).Skip((page - 1) * rows).Take(rows).Where(a => a.ctime >= ctime && a.ctime <= ptime).ToList();
            }
            else
            {
                return db.V_CMS_Article.OrderByDescending(c => c.aid).Skip((page - 1) * rows).Take(rows).ToList();
            }
            
        }

        public int V_CMS_ArticleCount()
        {
          return  db.CMS_Article.Count();
        }

        public int delArticle(int ?aid)
        {
            db.CMS_Article.Remove(db.CMS_Article.Find(aid));
            return db.SaveChanges();
        }

        public int editArticle(CMS_Article a)
        {
            db.Entry<CMS_Article>(a).State = System.Data.Entity.EntityState.Modified;
            return db.SaveChanges();
        }

        public int updateistop(int aid,bool istop)
        {
            var ls = db.CMS_Article.Find(aid);
            ls.istop = istop;
          
            return db.SaveChanges();
        }

        public V_CMS_Article GetCMS_ArticleByid(int aid) {
           return db.V_CMS_Article.Where(a=>a.aid==aid).Single();
        }

        public int delcomment(int cid)
        {
            var com = db.CMS_Comment.Find(cid);
            db.CMS_Comment.Remove(com);
            return db.SaveChanges();
        }

        public List<CMS_Category> getAllCMS_Category()
        {
           return db.CMS_Category.ToList();
        }

        public int yidong(int aid,int cid) {
           var ls= db.CMS_Category.Find(aid);
            ls.cid = cid;
            return db.SaveChanges();
        }

        public int yidongss(int aid, int cid)
        {
            var ls = db.CMS_Article.Find(aid);
            ls.cid = cid;
             db.Entry<CMS_Article>(ls).State = System.Data.Entity.EntityState.Modified;
            return db.SaveChanges();
        }
            public void Dispose()
        {
            db.Dispose();
        }
    }
}
