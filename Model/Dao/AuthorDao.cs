using Model.EF;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Model.Dao
{
    public class AuthorDao
    {
        TruyenOnlineDbContext db = null;
        public AuthorDao()
        {
            db = new TruyenOnlineDbContext();
        }
        public bool CheckName(string name)
        {
            return db.TacGias.Count(x => x.Ten_TacGia == name) > 0;
        }
        public long GetIdByName(string name)
        {
            return db.TacGias.SingleOrDefault(x => x.Ten_TacGia == name).Id_TacGia;
        }
        public TacGia ViewDetail(int id)
        {
            return db.TacGias.Find(id);
        }

        //insert, update, delete
        public long Insert(TacGia entity)
        {
            db.TacGias.Add(entity);
            db.SaveChanges();
            return entity.Id_TacGia;
        }
        public bool Update(TacGia entity)
        {
            try
            {
                var tacgia = db.TacGias.Find(entity.Id_TacGia);
                tacgia.Ten_TacGia = entity.Ten_TacGia;
                tacgia.CreateTime = DateTime.Now;
                db.SaveChanges();
                return true;
            }
            catch (Exception)
            {
                return false;
            }
        }
        public bool Delete(int id)
        {
            try
            {
                var user = db.TacGias.Find(id);
                db.TacGias.Remove(user);
                db.SaveChanges();
                return true;
            }
            catch (Exception)
            {
                return false;
            }

        }


        //list author
        public List<TacGia> getAll()
        {
            var list = db.Database.SqlQuery<TacGia>("sp_Tacgia_getALl").ToList();
            return list;
        }
    }
}
