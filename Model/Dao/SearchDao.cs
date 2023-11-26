using Model.EF;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Model.Dao
{
    public class SearchDao
    {
        TruyenOnlineDbContext db = null;
        public SearchDao()
        {
            db = new TruyenOnlineDbContext();
        }
       
        // list category
        public List<Truyen> getSearchBook(string searchString)
        {
            System.Diagnostics.Debug.WriteLine(searchString);

            var truyenIds = db.ChuongTruyens.Where(o => o.NoiDung_Chuong.Contains(searchString)).Select(o => o.Id_Truyen.ToString()).ToList();
            var tacgiaIds = db.TacGias.Where(o => o.Ten_TacGia.Contains(searchString)).Select(o => o.Id_TacGia.ToString()).ToList();
            var list = db.Truyens.Where(t => t.Ten_Truyen.Contains(searchString) || t.MetaTitle.Contains(searchString)
                || truyenIds.Contains(t.Id_Truyen.ToString()) || tacgiaIds.Contains(t.Id_TacGia.ToString())
            ).ToList();


            return list;
        }
    }
}
