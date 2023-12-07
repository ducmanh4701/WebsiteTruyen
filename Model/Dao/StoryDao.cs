using Model.EF;
using Model.ViewModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
/*using System.Speech.Synthesis;*/

namespace Model.Dao
{
    public class StoryDao
    {
        TruyenOnlineDbContext db = null;
        public StoryDao()
        {
            db = new TruyenOnlineDbContext();
        }
        public bool CheckName(string name)
        {
            return db.Truyens.Count(x => x.Ten_Truyen == name) > 0;
        }
        public StoryViewModel ViewDetail(long? id)
        {
            var truyen = db.Truyens.Find(id);
            var Detail = new StoryViewModel()
            {
                ID = truyen.Id_Truyen,
                Name = truyen.Ten_Truyen,

                NameAuthor = truyen.TacGia.Ten_TacGia,
                NameCategory = truyen.TheLoai.Ten_TheLoai,
                Avt = truyen.Avt_Truyen,
                GioiThieu = truyen.GioiThieu_Truyen,
                Status = truyen.TrangThai.TrangThai1,
                View = truyen.TotalView,
                MetaTitle = truyen.MetaTitle
            };
            return Detail;
        }
        public ChuongTruyen ViewDetailContent(int id)
        {
            return db.ChuongTruyens.Find(id);
        }


        // insert story
        public long Insert(Truyen truyen, string tacgia, string theloai, string trangthai)
        {
            //check status author and category
            var authordao = new AuthorDao();
            var categorydao = new CategoryDao();
            var statusdao = new StatusDao();
            if (authordao.CheckName(tacgia) == false)
            {
                var author = new TacGia();
                author.Ten_TacGia = tacgia;
                author.CreateTime = DateTime.Now;
                db.TacGias.Add(author);
                db.SaveChanges();
            }

            if (categorydao.CheckName(theloai) == false)
            {
                var category = new TheLoai();
                category.Ten_TheLoai = theloai;
                category.CreateTime = DateTime.Now;
                db.TheLoais.Add(category);
                db.SaveChanges();
            }

            //add story
            truyen.Id_TacGia = authordao.GetIdByName(tacgia);
            truyen.Id_TheLoai = categorydao.GetIdByName(theloai);
            truyen.Id_TrangThai = statusdao.GetIdByName(trangthai);
            truyen.CreateDate = DateTime.Now;
            db.Truyens.Add(truyen);
            db.SaveChanges();
            return truyen.Id_Truyen;
        }
        // Update Story
        public bool Edit(Truyen entity, string tacgia, string theloai, string trangthai, long ID)
        {
            try
            {
                var truyen = db.Truyens.Find(ID);
                var statusdao = new StatusDao();

                // check author and add author
                var authordao = new AuthorDao();
                if (authordao.CheckName(tacgia) == false)
                {
                    var author = new TacGia();
                    author.Ten_TacGia = tacgia;
                    author.CreateTime = DateTime.Now;
                    db.TacGias.Add(author);
                    db.SaveChanges();
                }

                // check category and add category
                var categorydao = new CategoryDao();
                if (categorydao.CheckName(theloai) == false)
                {
                    var category = new TheLoai();
                    category.Ten_TheLoai = theloai;
                    category.CreateTime = DateTime.Now;
                    db.TheLoais.Add(category);
                    db.SaveChanges();
                }

                // update story
                truyen.Ten_Truyen = entity.Ten_Truyen;
                truyen.GioiThieu_Truyen = entity.GioiThieu_Truyen;
                truyen.Avt_Truyen = entity.Avt_Truyen;
                truyen.Id_TacGia = authordao.GetIdByName(tacgia);
                truyen.Id_TheLoai = categorydao.GetIdByName(theloai);
                truyen.Id_TrangThai = statusdao.GetIdByName(trangthai);
                truyen.CreateDate = DateTime.Now;
                db.SaveChanges();
                return true;
            }
            catch (Exception)
            {
                return false;
            }
        }
        // delete story
        public bool Delete(int id)
        {
            try
            {
                var storydel = db.Truyens.Find(id);
                db.Truyens.Remove(storydel);
                db.SaveChanges();
                return true;
            }
            catch (Exception)
            {
                return false;
            }
        }
        //insert content 
        public long InsertContent(ChuongTruyen chuongtruyen)
        {
            var them = new ChuongTruyen();
            them.Ten_Chuong = chuongtruyen.Ten_Chuong;
            them.NoiDung_Chuong = chuongtruyen.NoiDung_Chuong;
            them.MetaTitle = chuongtruyen.MetaTitle;
            them.Id_Truyen = chuongtruyen.Id_Truyen;
            var displayOrder = db.ChuongTruyens.Where(m => m.Id_Truyen == chuongtruyen.Id_Truyen).Max(m => m.DisplayOrder);
            them.DisplayOrder = displayOrder == null ? 1 : displayOrder + 1;
            db.ChuongTruyens.Add(them);
            return db.SaveChanges();
        }
        // Update Content
        public bool EditContent(ChuongTruyen entity)
        {
            try
            {
                var chuong = db.ChuongTruyens.Find(entity.Id_Chuong);
                chuong.MetaTitle = entity.MetaTitle;
                chuong.Ten_Chuong = entity.Ten_Chuong;
                chuong.NoiDung_Chuong = entity.NoiDung_Chuong;
                db.SaveChanges();
                return true;
            }
            catch (Exception e)
            {
                return false;
            }
        }
        // Delete Content
        public bool DeleteContent(int id_content)
        {
            try
            {
                var contentdel = db.ChuongTruyens.SingleOrDefault(x => x.Id_Chuong == id_content);
                db.ChuongTruyens.Remove(contentdel);
                db.SaveChanges();
                return true;
            }
            catch (Exception)
            {
                return false;
            }
        }

        //danh sach truyen
        public List<StoryViewModel> getAll()
        {
            /*            var list = db.Database.SqlQuery<Truyen>("sp_Truyen_getAll").ToList();
                        return list;*/

            var model = from a in db.Truyens
                        join b in db.TacGias
                        on a.Id_TacGia equals b.Id_TacGia
                        join c in db.TheLoais
                        on a.Id_TheLoai equals c.Id_TheLoai
                        join d in db.TrangThais
                        on a.Id_TrangThai equals d.Id_TrangThai
                        where a.Id_Truyen >= 0
                        select new StoryViewModel()
                        {
                            ID = a.Id_Truyen,
                            Name = a.Ten_Truyen,
                            NameAuthor = b.Ten_TacGia,
                            NameCategory = c.Ten_TheLoai,
                            GioiThieu = a.GioiThieu_Truyen,
                            Status = d.TrangThai1,
                            Avt = a.Avt_Truyen,
                            MetaTitle = a.MetaTitle
                        };
            return model.ToList();
        }
        //Noi dung truyen
        public List<ChuongTruyen> getAllContentByID(long? id_story)
        {
            return db.ChuongTruyens.Where(x => x.Id_Truyen == id_story).OrderBy(x => x.DisplayOrder).ToList();
        }
        public List<Truyen> ListNewStory(int top)
        {
            return db.Truyens.OrderByDescending(x => x.CreateDate).Take(top).ToList();
        }
        public List<Truyen> ListHotStory(int top)
        {
            return db.Truyens.Where(x => x.TopHot != null && x.TopHot > DateTime.Now).OrderByDescending(x => x.CreateDate).Take(top).ToList();
        }
        public List<Truyen> ListViewStory(int top)
        {
            return db.Truyens.OrderByDescending(x => x.TotalView).Take(top).ToList();
        }
        /// <summary>
        /// Get story by id category
        /// </summary>
        /// <param name="id_theloai"></param>
        /// <returns></returns>
        public List<Truyen> ListStoryByIDCategory(int id_theloai)
        {
            return db.Truyens.Where(x => x.Id_TheLoai == id_theloai).ToList();
        }
        /// <summary>
        /// Get story by id status
        /// </summary>
        /// <param name="id_trangthai"></param>
        /// <returns></returns>
        public List<Truyen> ListStoryByIDStatus(int id_trangthai)
        {
            return db.Truyens.Where(x=>x.Id_TrangThai == id_trangthai).ToList();
        }
        /// <summary>
        /// Get comment by id story
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public List<BinhLuan> ListAllComment(long? id)
        {
            return db.BinhLuans.Where(x => x.Id_Truyen == id).ToList();
        }

        public List<History> GetListHistoryByTruyenId(long? truyenId, Guid anonymousId) 
        {
            return db.Histories.Where(m => m.TruyenId == truyenId && m.AnonymousId == anonymousId).ToList();
        }
        public void CreateHistory(long truyenId, long chuongTruyenId, Guid anonymousId)
        {
            var hasExists = db.Histories.Any(m => m.TruyenId == truyenId && m.ChuongTruyenId == chuongTruyenId && m.AnonymousId == anonymousId);
            if(hasExists == false)
            {
                db.Histories.Add(new History
                {
                    AnonymousId = anonymousId,
                    ChuongTruyenId = chuongTruyenId,
                    TruyenId = truyenId,
                    CreatedAt = DateTime.UtcNow,
                    UpdatedAt = DateTime.UtcNow
                });
                db.SaveChanges();
            }
        }

        public void FollowStory(long truyenId, long userId)
        {
            db.Follows.Add(new Follow
            {
                TruyenId = truyenId,
                UserId = userId,
                CreatedAt = DateTime.Now,
                UpdatedAt = DateTime.Now
            });
            db.SaveChanges();
        }
        public void UnFollowStory(long truyenId, long userId)
        {
            var followEntity = db.Follows.FirstOrDefault(m => m.TruyenId == truyenId && m.UserId == userId);
            if(followEntity != null)
            {
                db.Follows.Remove(followEntity);
                db.SaveChanges();
            }
        }
        public Follow GetFollowByTruyenUser(long truyenId, long userId)
        {
            return db.Follows.FirstOrDefault(m => m.TruyenId == truyenId && m.UserId == userId);
        }
        public List<TruyenViewModel> GetFollowByUsers(long userId)
        {
            var iQueryable = db.Truyens.Join(db.Follows, t => t.Id_Truyen, f => f.TruyenId, (t, f) => new { t, f })
                .Where(m => m.f.UserId == userId)
                .AsQueryable();
            var results = iQueryable.Select(m => new TruyenViewModel
            {
                Avt_Truyen = m.t.Avt_Truyen,
                CreateDate = m.t.CreateDate,
                GioiThieu_Truyen = m.t.GioiThieu_Truyen,
                Id_TacGia = m.t.Id_TacGia,
                Id_TheLoai = m.t.Id_TheLoai,
                Id_TrangThai = m.t.Id_TrangThai,
                Id_Truyen = m.t.Id_Truyen,
                MetaTitle = m.t.MetaTitle,
                TopHot = m.t.TopHot,
                TotalView = m.t.TotalView
            }).ToList();
            return results;
        }
       /* static void Speech(string[] args)
        {
            SpeechSynthesizer _SS = new SpeechSynthesizer();
            _SS.Volume = 50;
            _SS.Speak("Hello World");

        }*/
    }
}
