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
    public class NhanVienController : Controller
    {
        private QLKSEntities db = new QLKSEntities();

        // GET: NhanVien
        public ActionResult Index()
        {
            NhanVien nv = (NhanVien)Session["NV"];
            if (nv.ma_chuc_vu> 2){
                return View("Error");
            }
            var NhanViens = db.NhanViens.Include(t => t.ChucVu);
            return View(NhanViens.ToList());
        }

        // GET: NhanVien/Details/5
        public ActionResult Details(int? id)
        {
            NhanVien nv = (NhanVien)Session["NV"];
            if (nv.ma_chuc_vu > 2)
            {
                return View("Error");
            }
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            NhanVien NhanVien = db.NhanViens.Find(id);
            if (NhanVien == null)
            {
                return HttpNotFound();
            }
            return View(NhanVien);
        }

        // GET: NhanVien/Create
        public ActionResult Create()
        {
            NhanVien nv = (NhanVien)Session["NV"];
            if (nv.ma_chuc_vu > 2)
            {
                return View("Error");
            }
            ViewBag.ma_chuc_vu = new SelectList(db.ChucVus, "ma_chuc_vu", "chuc_vu");
            return View();
        }

        // POST: NhanVien/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "ma_nv,ho_ten,ngay_sinh,dia_chi,sdt,tai_khoan,mat_khau,ma_chuc_vu")] NhanVien NhanVien)
        {
            NhanVien nv = (NhanVien)Session["NV"];
            if (nv.ma_chuc_vu > 2)
            {
                return View("Error");
            }
            if (ModelState.IsValid)
            {
                db.NhanViens.Add(NhanVien);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            ViewBag.ma_chuc_vu = new SelectList(db.ChucVus, "ma_chuc_vu", "chuc_vu", NhanVien.ma_chuc_vu);
            return View(NhanVien);
        }

        // GET: NhanVien/Edit/5
        public ActionResult Edit(int? id)
        {
            NhanVien nv = (NhanVien)Session["NV"];
            if (nv.ma_chuc_vu > 2)
            {
                return View("Error");
            }
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            NhanVien NhanVien = db.NhanViens.Find(id);
            if (NhanVien == null)
            {
                return HttpNotFound();
            }
            ViewBag.ma_chuc_vu = new SelectList(db.ChucVus, "ma_chuc_vu", "chuc_vu", NhanVien.ma_chuc_vu);
            return View(NhanVien);
        }

        // POST: NhanVien/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "ma_nv,ho_ten,ngay_sinh,dia_chi,sdt,tai_khoan,mat_khau,ma_chuc_vu")] NhanVien NhanVien)
        {
            NhanVien nv = (NhanVien)Session["NV"];
            if (nv.ma_chuc_vu > 2)
            {
                return View("Error");
            }
            if (ModelState.IsValid)
            {
                db.Entry(NhanVien).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.ma_chuc_vu = new SelectList(db.ChucVus, "ma_chuc_vu", "chuc_vu", NhanVien.ma_chuc_vu);
            return View(NhanVien);
        }

        // GET: NhanVien/Delete/5
        public ActionResult Delete(int? id)
        {
            NhanVien nv = (NhanVien)Session["NV"];
            if (nv.ma_chuc_vu > 2)
            {
                return View("Error");
            }
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            NhanVien NhanVien = db.NhanViens.Find(id);
            if (NhanVien == null)
            {
                return HttpNotFound();
            }
            try
            {
                db.NhanViens.Remove(NhanVien);
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
