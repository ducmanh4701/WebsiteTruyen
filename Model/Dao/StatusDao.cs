using Model.EF;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Model.Dao
{
    public class StatusDao
    {
        TruyenOnlineDbContext db = null;
        public StatusDao()
        {
            db = new TruyenOnlineDbContext();
        }

        public long GetIdByName(string name)
        {
            return db.TrangThais.SingleOrDefault(x => x.TrangThai1 == name).Id_TrangThai;
        }
        /// <summary>
        /// Get ViewDetail Status By id
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public TrangThai ViewDetail(int id)
        {
            return db.TrangThais.Find(id);
        }
        public List<TrangThai> ListAll()
        {
            return db.TrangThais.Where(x => x.Id_TrangThai >= 0).ToList();
        }
        
    }
}
