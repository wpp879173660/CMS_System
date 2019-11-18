using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Model
{
   public partial class CMS_ArticleSSS
    {
        public int aid { get; set; }
        public Nullable<int> cid { get; set; }
        public string title { get; set; }
        public string author { get; set; }
        public Nullable<int> uid { get; set; }
        public Nullable<System.DateTime> ctime { get; set; }
        public Nullable<System.DateTime> ptime { get; set; }
        public Nullable<bool> istop { get; set; }
        public Nullable<int> state { get; set; }
        public Nullable<int> hits { get; set; }
        public Nullable<int> comments { get; set; }
        public string ahtml { get; set; }
        public string uname { get; set; }
        public string nname { get; set; }
        public string ctitle { get; set; }
    }
}
