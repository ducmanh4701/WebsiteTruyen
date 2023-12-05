using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Model.ViewModel
{
    public class ChuongTruyenViewModel
    {
        public long Id_Chuong { get; set; }
        public string Ten_Chuong { get; set; }
        public string NoiDung_Chuong { get; set; }
        public string MetaTitle { get; set; }
        public int? DisplayOrder { get; set; }
        public long? Id_Truyen { get; set; }
        public bool IsRead { get; set; }
    }
}
