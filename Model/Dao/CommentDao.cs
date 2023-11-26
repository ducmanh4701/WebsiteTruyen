using Model.EF;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Model.Dao
{
    public class CommentDao
    {
        TruyenOnlineDbContext db = null;
        public CommentDao()
        {
            db = new TruyenOnlineDbContext();
        }
        public bool Delete(int id)
        {
            try 
            {
                var cm = db.BinhLuans.Find(id);
                db.BinhLuans.Remove(cm);
                db.SaveChanges();
                return true;
            }
            catch (Exception)
            {
                return false;
            }
        }


        public List<BinhLuan> getAll()
        {
            var list = db.Database.SqlQuery<BinhLuan>("sp_Comment_getAll").ToList();
            return list;
        }
    }
}
