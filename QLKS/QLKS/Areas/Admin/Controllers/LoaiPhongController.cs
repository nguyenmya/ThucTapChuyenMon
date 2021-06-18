using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using QLKS.Models;

namespace QLKS.Areas.Admin.Controllers
{
    public class LoaiPhongController : Controller
    {
        private QLKSEntities db = new QLKSEntities();

        // GET: LoaiPhong
        public ActionResult Index()
        {
            return View(db.LoaiPhongs.ToList());
        }

        // GET: LoaiPhong/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            LoaiPhong LoaiPhong = db.LoaiPhongs.Find(id);
            if (LoaiPhong == null)
            {
                return HttpNotFound();
            }
            return View(LoaiPhong);
        }

        // GET: LoaiPhong/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: LoaiPhong/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "mo_ta,gia,ti_le_phu_thu,anh")] LoaiPhong LoaiPhong)
        {
            if (ModelState.IsValid)
            {
                if (LoaiPhong.anh==null)
                    LoaiPhong.anh = "[\"/Content/Images/Phong/default.png\"]";
                db.LoaiPhongs.Add(LoaiPhong);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            return View(LoaiPhong);
        }

        // GET: LoaiPhong/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            LoaiPhong LoaiPhong = db.LoaiPhongs.Find(id);
            if (LoaiPhong == null)
            {
                return HttpNotFound();
            }
            return View(LoaiPhong);
        }

        // POST: LoaiPhong/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "loai_phong,mo_ta,gia,ti_le_phu_thu,anh")] LoaiPhong LoaiPhong)
        {
            if (ModelState.IsValid)
            {
                if (LoaiPhong.anh == null)
                    LoaiPhong.anh = "[\"/Content/Images/Phong/default.png\"]";
                db.Entry(LoaiPhong).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(LoaiPhong);
        }

        // GET: LoaiPhong/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            LoaiPhong LoaiPhong = db.LoaiPhongs.Find(id);
            if (LoaiPhong == null)
            {
                return HttpNotFound();
            }
            return View(LoaiPhong);
        }

        // POST: LoaiPhong/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            try
            {
                LoaiPhong LoaiPhong = db.LoaiPhongs.Find(id);
                db.LoaiPhongs.Remove(LoaiPhong);
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
