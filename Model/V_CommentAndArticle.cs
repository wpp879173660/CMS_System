using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Model
{
    public class V_CommentAndArticle
    {
        public int cmid { get; set; }
        public Nullable<int> aid { get; set; }
        public Nullable<int> uid { get; set; }
        public Nullable<System.DateTime> cmtime { get; set; }
        public string cmhtml { get; set; }
        public string nname { get; set; }
        public string face { get; set; }
        public string title { get; set; }
        public string author { get; set; }
    }
}
