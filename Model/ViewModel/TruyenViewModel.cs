using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Model.ViewModel
{
    public class TruyenViewModel
    {
        public long Id_Truyen { get; set; }
        public string MetaTitle { get; set; }
        public string Avt_Truyen { get; set; }
        public string GioiThieu_Truyen { get; set; }
        public long? TotalView { get; set; }
        public DateTime? CreateDate { get; set; }
        public DateTime? TopHot { get; set; }
        public long? Id_TacGia { get; set; }
        public long? Id_TheLoai { get; set; }
        public long? Id_TrangThai { get; set; }
        public string Ten_Truyen { get; set; }
    }
}
