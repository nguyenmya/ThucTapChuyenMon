using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using Newtonsoft.Json;
using QLKS.Models;

namespace QLKS.Areas.Admin.Controllers.Admin
{
    public class PhieuDatPhongController : Controller
    {
        private QLKSEntities db = new QLKSEntities();

        // GET: PhieuDatPhong
        public ActionResult Index()
        {
            AutoHuyPhieuDatPhong();
            var PhieuDatPhongs = db.PhieuDatPhongs.Include(t => t.KhachHang).Include(t => t.Phong).Include(t => t.TinhTrangPhieuDatPhong);
            return View(PhieuDatPhongs.ToList());
        }

        private void AutoHuyPhieuDatPhong()
        {
            var datenow = DateTime.Now;
            var PhieuDatPhongs = db.PhieuDatPhongs.Where(u => u.ma_tinh_trang == 1).Include(t => t.KhachHang).Include(t => t.Phong).Include(t => t.TinhTrangPhieuDatPhong).ToList();
            foreach (var item in PhieuDatPhongs)
            {
                System.Diagnostics.Debug.WriteLine((item.ngay_vao - datenow).Value.Days);
                if ((item.ngay_vao - datenow).Value.Days < 0)
                {
                    item.ma_tinh_trang = 3;
                    db.Entry(item).State = EntityState.Modified;
                    db.SaveChanges();
                }
            }
        }


        public ActionResult List()
        {
            AutoHuyPhieuDatPhong();
            var PhieuDatPhongs = db.PhieuDatPhongs.Where(t => t.ma_tinh_trang == 1 && t.ngay_vao.Value.Day == DateTime.Now.Day && t.ngay_vao.Value.Month == DateTime.Now.Month && t.ngay_vao.Value.Year == DateTime.Now.Year).Include(t => t.KhachHang).Include(t => t.Phong).Include(t => t.TinhTrangPhieuDatPhong);
            return View(PhieuDatPhongs.ToList());
        }

        // GET: PhieuDatPhong/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            PhieuDatPhong PhieuDatPhong = db.PhieuDatPhongs.Find(id);
            if (PhieuDatPhong == null)
            {
                return HttpNotFound();
            }
            return View(PhieuDatPhong);
        }

        // GET: PhieuDatPhong/Create

        public ActionResult Create(int? id)
        {
            if (id != null)
            {
                ViewBag.select_ma_phong = id;
            }
            ViewBag.ma_kh = new SelectList(db.KhachHangs, "ma_kh", "ma_kh");
            ViewBag.ma_phong = new SelectList(db.Phongs.Where(u => u.ma_tinh_trang == 1), "ma_phong", "so_phong");
            ViewBag.ma_tinh_trang = new SelectList(db.TinhTrangPhieuDatPhongs, "ma_tinh_trang", "tinh_trang");
            return View();
        }


        public ActionResult SelectRoom(String dateE)
        {
            if (dateE == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            ViewBag.ma_kh = new SelectList(db.KhachHangs, "ma_kh", "ma_kh");
            DateTime ngay_ra = (DateTime.Parse(dateE)).AddHours(12);
            ViewBag.ngay_ra = ngay_ra;
            var s = db.Phongs.Where(t => !(db.PhieuDatPhongs.Where(m => (m.ma_tinh_trang == 1 || m.ma_tinh_trang == 2) && (m.ngay_ra > DateTime.Now && m.ngay_ra < ngay_ra))).Select(m => m.ma_phong).ToList().Contains(t.ma_phong) && t.ma_tinh_trang == 1);
            ViewBag.ma_phong = new SelectList(s, "ma_phong", "so_phong");
            ViewBag.ma_tinh_trang = new SelectList(db.TinhTrangPhieuDatPhongs, "ma_tinh_trang", "tinh_trang");
            return View();
        }


        // POST: PhieuDatPhong/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(String radSelect, [Bind(Include = "ma_pdp,ma_kh,ngay_dat,ngay_vao,ngay_ra,ma_phong,ma_tinh_trang")] PhieuDatPhong PhieuDatPhong)
        {
            System.Diagnostics.Debug.WriteLine("SS :" + radSelect);
            if (radSelect.Equals("rad2"))
            {
                PhieuDatPhong.ma_kh = null;
            }

            PhieuDatPhong.ma_tinh_trang = 1;
            PhieuDatPhong.ngay_vao = DateTime.Now;
            PhieuDatPhong.ngay_dat = DateTime.Now;
            db.PhieuDatPhongs.Add(PhieuDatPhong);
            db.SaveChanges();
            int ma = PhieuDatPhong.ma_pdp;
            return RedirectToAction("Add", "HoaDon", new { id = ma });

            ViewBag.ma_kh = new SelectList(db.KhachHangs, "ma_kh", "ma_kh", PhieuDatPhong.ma_kh);
            ViewBag.ma_phong = new SelectList(db.Phongs, "ma_phong", "so_phong", PhieuDatPhong.ma_phong);
            ViewBag.ma_tinh_trang = new SelectList(db.TinhTrangPhieuDatPhongs, "ma_tinh_trang", "tinh_trang", PhieuDatPhong.ma_tinh_trang);
            return View(PhieuDatPhong);
        }

        // GET: PhieuDatPhong/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            PhieuDatPhong PhieuDatPhong = db.PhieuDatPhongs.Find(id);
            if (PhieuDatPhong == null)
            {
                return HttpNotFound();
            }
            ViewBag.ma_kh = new SelectList(db.KhachHangs, "ma_kh", "mat_khau", PhieuDatPhong.ma_kh);
            ViewBag.ma_phong = new SelectList(db.Phongs, "ma_phong", "so_phong", PhieuDatPhong.ma_phong);
            ViewBag.ma_tinh_trang = new SelectList(db.TinhTrangPhieuDatPhongs, "ma_tinh_trang", "tinh_trang", PhieuDatPhong.ma_tinh_trang);
            return View(PhieuDatPhong);
        }

        // POST: PhieuDatPhong/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "ma_pdp,ma_kh,ngay_dat,ngay_vao,ngay_ra,ma_phong,ma_tinh_trang")] PhieuDatPhong PhieuDatPhong)
        {
            if (ModelState.IsValid)
            {
                db.Entry(PhieuDatPhong).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.ma_kh = new SelectList(db.KhachHangs, "ma_kh", "mat_khau", PhieuDatPhong.ma_kh);
            ViewBag.ma_phong = new SelectList(db.Phongs, "ma_phong", "so_phong", PhieuDatPhong.ma_phong);
            ViewBag.ma_tinh_trang = new SelectList(db.TinhTrangPhieuDatPhongs, "ma_tinh_trang", "tinh_trang", PhieuDatPhong.ma_tinh_trang);
            return View(PhieuDatPhong);
        }

        // GET: PhieuDatPhong/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            PhieuDatPhong PhieuDatPhong = db.PhieuDatPhongs.Find(id);
            if (PhieuDatPhong == null)
            {
                return HttpNotFound();
            }
            return View(PhieuDatPhong);
        }

        // POST: PhieuDatPhong/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            try
            {
                PhieuDatPhong PhieuDatPhong = db.PhieuDatPhongs.Find(id);
                db.PhieuDatPhongs.Remove(PhieuDatPhong);
                db.SaveChanges();
            }
            catch
            {

            }
            return RedirectToAction("Index");
        }


        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }
    }
}
