using Model.EF;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Model.Dao
{
    public class AdminDao
    {
        TruyenOnlineDbContext db = null;
        public AdminDao()
        {
            db = new TruyenOnlineDbContext();
        }

        public long Insert(Admin admin)
        {
            db.Admins.Add(admin);
            db.SaveChanges();
            return admin.Id_Admin;
        }

        public Admin GetById(string userName)
        {
            return db.Admins.SingleOrDefault(x => x.UserName == userName);
        }
        public int Login(string UserName, string PassWord)
        {
            var result = db.Admins.SingleOrDefault(x => x.UserName == UserName);
            if(result == null)
            {
                return 0;
            }
            else if(result.status == false)
            {
                return -1;
            }
            else 
            {
                if(result.Password == PassWord)
                {
                    return 1;
                }
                else
                {
                    return -2;
                }
            }
        }
    }
}
