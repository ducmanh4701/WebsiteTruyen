using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Model.ViewModel
{
    public class StoryViewModel
    {
        public long ID { set; get; }
        public string Name { set; get; }
        public string Avt { set; get; }
        public string GioiThieu { set; get; }
        public string NameAuthor { set; get; }
        public string NameCategory { set; get; }
        public string Status { set; get; }
        public long? View { set; get; }
        public string MetaTitle { set;get; }
        public long? CurrentChuongId { get; set; }
        public string CurrentMetaTitle { get; set; }
        public bool HasFollow { get; set; }
    }
}
