using Model.EF;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Model.Dao
{
    public class UserDao
    {
        TruyenOnlineDbContext db = null;
        public UserDao()
        {
            db = new TruyenOnlineDbContext();
        }
        public bool CheckUserName(string userName)
        {
            return db.Users.Count(x => x.UserName == userName) > 0;
        }
        public bool CheckEmail(string email)
        {
            return db.Users.Count(x => x.Email == email) > 0;
        }
        public User GetById(string userName)
        {
            return db.Users.SingleOrDefault(x => x.UserName == userName);
        }


        public User ViewDetail(int id)
        {
            return db.Users.Find(id);
        }



        //login
        public int Login(string userName, string passWord)
        {
            var result =  db.Users.SingleOrDefault(x => x.UserName == userName);
            if(result == null)
            {
                return 0;
            }
            else
            {
                if(result.status == false)
                {
                    return -1;
                }
                else
                {
                    if (result.Password == passWord)
                        return 1;
                    else
                        return -2;
                }
            }
        }


        // Insert, update, delete
        public long Insert(User user)
        {
            db.Users.Add(user);
            db.SaveChanges();
            return user.Id_User;
        }
        public bool Update(User entity)
        {
            try
            {
                var user = db.Users.Find(entity.Id_User);
                user.HoTen = entity.HoTen;
                user.GioiTinh = entity.GioiTinh;
                user.NgaySinh = entity.NgaySinh;
                user.SDT = entity.SDT;
                user.Email = entity.Email;
                user.UserName = entity.UserName;
                user.Password = entity.Password;
                user.CreateTime = DateTime.Now;

                db.SaveChanges();
                return true;
            }
            catch (Exception ex)
            {
                //logging
                return false;
            }
        }
        public bool Delete(int id)
        {
            try
            {
                var user = db.Users.Find(id);
                db.Users.Remove(user);
                db.SaveChanges();
                return true;
            }
            catch (Exception e)
            {
                return false;
            }
        }



        //view
        public List<User> getAll()
        {
            var list = db.Users.ToList();
            return list;
        }
    }
}
