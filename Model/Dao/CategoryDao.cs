using Model.EF;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Model.Dao
{
    public class CategoryDao
    {
        TruyenOnlineDbContext db = null;
        public CategoryDao()
        {
            db = new TruyenOnlineDbContext();
        }
        public bool CheckName(string Name)
        {
            return db.TheLoais.Count(x => x.Ten_TheLoai == Name) > 0;
        }
        public long GetIdByName(string name)
        {
            return db.TheLoais.SingleOrDefault(x=>x.Ten_TheLoai == name).Id_TheLoai;
        }
        public TheLoai ViewDetail(int id)
        {
            return db.TheLoais.Find(id);
        }

        // insert, edit, delete
        public long Insert(TheLoai entity)
        {
            db.TheLoais.Add(entity);
            db.SaveChanges();
            return entity.Id_TheLoai;
        }
        public bool Update(TheLoai entity)
        {
            try
            {
                var theloai = db.TheLoais.Find(entity.Id_TheLoai);
                theloai.Ten_TheLoai = entity.Ten_TheLoai;
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
                var theloai = db.TheLoais.Find(id);
                db.TheLoais.Remove(theloai);
                db.SaveChanges();
                return true;
            }
            catch (Exception)
            {
                return false;
            }
        }

        // list category
        public List<TheLoai> getAll()
        {
            var list = db.Database.SqlQuery<TheLoai>("sp_Category_All").ToList();
            return list;
        }
    }
}
