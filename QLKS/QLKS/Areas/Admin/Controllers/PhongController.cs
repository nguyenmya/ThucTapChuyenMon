using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using QLKS.Models;

namespace QLKS.Areas.Admin.Controllers.Admin
{
    public class PhongController : Controller
    {
        private QLKSEntities db = new QLKSEntities();

        // GET: Phong
        public ActionResult Index()
        {
            var Phongs = db.Phongs.Where(t=>t.ma_tinh_trang<5).Include(t => t.LoaiPhong).Include(t => t.Tang).Include(t => t.TinhTrangPhong);
            return View(Phongs.ToList());
        }

        // GET: Phong/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Phong Phong = db.Phongs.Find(id);
            if (Phong == null)
            {
                return HttpNotFound();
            }
            return View(Phong);
        }

        // GET: Phong/Create
        public ActionResult Create()
        {
            ViewBag.loai_phong = new SelectList(db.LoaiPhongs, "loai_phong", "mo_ta");
            ViewBag.ma_tang = new SelectList(db.Tangs, "ma_tang", "ten_tang");
            ViewBag.ma_tinh_trang = new SelectList(db.TinhTrangPhongs, "ma_tinh_trang", "mo_ta");
            return View();
        }

        // POST: Phong/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "ma_phong,so_phong,loai_phong,ma_tang,ma_tinh_trang")] Phong Phong)
        {
            if (ModelState.IsValid)
            {
                db.Phongs.Add(Phong);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            ViewBag.loai_phong = new SelectList(db.LoaiPhongs, "loai_phong", "mo_ta", Phong.loai_phong);
            ViewBag.ma_tang = new SelectList(db.Tangs, "ma_tang", "ten_tang", Phong.ma_tang);
            ViewBag.ma_tinh_trang = new SelectList(db.TinhTrangPhongs, "ma_tinh_trang", "mo_ta", Phong.ma_tinh_trang);
            return View(Phong);
        }

        // GET: Phong/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Phong Phong = db.Phongs.Find(id);
            if (Phong == null)
            {
                return HttpNotFound();
            }
            ViewBag.loai_phong = new SelectList(db.LoaiPhongs, "loai_phong", "mo_ta", Phong.loai_phong);
            ViewBag.ma_tang = new SelectList(db.Tangs, "ma_tang", "ten_tang", Phong.ma_tang);
            ViewBag.ma_tinh_trang = new SelectList(db.TinhTrangPhongs, "ma_tinh_trang", "mo_ta", Phong.ma_tinh_trang);
            return View(Phong);
        }

        // POST: Phong/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "ma_phong,so_phong,loai_phong,ma_tang,ma_tinh_trang")] Phong Phong)
        {
            if (ModelState.IsValid)
            {
                db.Entry(Phong).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.loai_phong = new SelectList(db.LoaiPhongs, "loai_phong", "mo_ta", Phong.loai_phong);
            ViewBag.ma_tang = new SelectList(db.Tangs, "ma_tang", "ten_tang", Phong.ma_tang);
            ViewBag.ma_tinh_trang = new SelectList(db.TinhTrangPhongs, "ma_tinh_trang", "mo_ta", Phong.ma_tinh_trang);
            return View(Phong);
        }

        // GET: Phong/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Phong Phong = db.Phongs.Find(id);
            if (Phong == null)
            {
                return HttpNotFound();
            }
            return View(Phong);
        }

        // POST: Phong/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            try
            {
                Phong Phong = db.Phongs.Find(id);
                Phong.ma_tinh_trang = 5;
                db.Entry(Phong).State = EntityState.Modified;
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
