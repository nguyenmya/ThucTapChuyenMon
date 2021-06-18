﻿using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using QLKS.Models;

namespace QLKS.Controllers.Home
{
    public class AccountController : Controller
    {
        private QLKSEntities db = new QLKSEntities();
        // GET: KhachHang
 

        // GET: KhachHang/Details/5
        public ActionResult Details(string id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            KhachHang KhachHang = db.KhachHangs.Find(id);
            if (KhachHang == null)
            {
                return HttpNotFound();
            }
            return View(KhachHang);
        }

        // GET: KhachHang/Create
        public ActionResult Register()
        {
            return View();
        }

        // POST: KhachHang/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Register([Bind(Include = "ma_kh,mat_khau,ho_ten,cmt,sdt,mail")] KhachHang KhachHang)
        {
            if (ModelState.IsValid)
            {
                if (db.KhachHangs.Find(KhachHang.ma_kh) == null)
                {
                    db.KhachHangs.Add(KhachHang);
                    db.SaveChanges();
                    Session["KH"] = KhachHang;
                    return RedirectToAction("FindRoom", "Home");
                }
                else
                {
                    ModelState.AddModelError("", "Tên tài khoản đã được sử dụng!");
                }
            }

            return View(KhachHang);
        }

        public ActionResult Add()
        {
            return View();
        }

        // POST: KhachHang/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Add([Bind(Include = "ma_kh,mat_khau,ho_ten,cmt,sdt,mail")] KhachHang KhachHang)
        {
            if (ModelState.IsValid)
            {
                if (db.KhachHangs.Find(KhachHang.ma_kh) == null)
                {
                    db.KhachHangs.Add(KhachHang);
                    db.SaveChanges();
                    return RedirectToAction("FindRoom", "Admin");
                }
                else
                {
                    ModelState.AddModelError("", "Login data is incorrect!");
                }
            }

            return View(KhachHang);
        }

        // GET: KhachHang/Edit/5
        public ActionResult Edit(string id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            KhachHang KhachHang = db.KhachHangs.Find(id);
            if (KhachHang == null)
            {
                return HttpNotFound();
            }
            return View(KhachHang);
        }

        // POST: KhachHang/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "ma_kh,mat_khau,ho_ten,cmt,sdt,mail,diem")] KhachHang KhachHang)
        {
            if (ModelState.IsValid)
            {
                db.Entry(KhachHang).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("CaNhan");
            }
            return View(KhachHang);
        }


        public ActionResult CaNhan()
        {
            KhachHang kh = new KhachHang();
            if (Session["KH"] == null)
            {
                return RedirectToAction("Login", "Account");
            }
            else
            {
                kh = (KhachHang)Session["KH"];
            }
            return View(kh);
        }

        // POST: KhachHang/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult CaNhan([Bind(Include = "ma_kh,mat_khau,ho_ten,cmt,sdt,mail,diem")] KhachHang KhachHang)
        {
            if (ModelState.IsValid)
            {
                db.Entry(KhachHang).State = EntityState.Modified;
                db.SaveChanges();
                Session["KH"] = KhachHang;
                return RedirectToAction("Index", "Home");
            }
            return View(KhachHang);
        }

        // GET: KhachHang/Delete/5
        public ActionResult Delete(string id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            KhachHang KhachHang = db.KhachHangs.Find(id);
            if (KhachHang == null)
            {
                return HttpNotFound();
            }
            return View(KhachHang);
        }

        // POST: KhachHang/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(string id)
        {
            try
            {
                KhachHang KhachHang = db.KhachHangs.Find(id);
                db.KhachHangs.Remove(KhachHang);
                db.SaveChanges();
            }
            catch
            {

            }
            return RedirectToAction("Index");
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Login(KhachHang objUser)
        {
            if (ModelState.IsValid)
            {
                var obj = db.KhachHangs.Where(a => a.ma_kh.Equals(objUser.ma_kh) && a.mat_khau.Equals(objUser.mat_khau)).FirstOrDefault();
                if (obj != null)
                {
                    Session["KH"] = obj;
                    return RedirectToAction("FindRoom", "Home");
                }
                else
                {
                    ModelState.AddModelError("", "Đăng nhập không thành công");
                }
            }
            return View(objUser);
        }
        [HttpGet]
        public ActionResult Login()
        {
            Session["KH"] = null;
            KhachHang kh = (KhachHang)Session["KH"];
            if (kh != null)
                return RedirectToAction("Index", "Home");
            return View();
        }



        // GET: PhieuDatPhong/Delete/5
        public ActionResult XoaPhieuDatPhong(int? id)
        {
            KhachHang kh = new KhachHang();
            if (Session["KH"] != null)
                kh = (KhachHang)Session["KH"];
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }

            PhieuDatPhong PhieuDatPhong = db.PhieuDatPhongs.Find(id);
            if (PhieuDatPhong == null)
            {
                return HttpNotFound();
            }
            if (PhieuDatPhong.ma_kh != kh.ma_kh)
                return RedirectToAction("Index", "Home");
            return View(PhieuDatPhong);
        }

        // POST: PhieuDatPhong/Delete/5
        [HttpPost, ActionName("XoaPhieuDatPhong")]
        [ValidateAntiForgeryToken]
        public ActionResult ConfirmXoaPhieuDatPhong(int id)
        {
            PhieuDatPhong PhieuDatPhong = db.PhieuDatPhongs.Find(id);
            PhieuDatPhong.ma_tinh_trang = 3;
            db.Entry(PhieuDatPhong).State = EntityState.Modified;
            db.SaveChanges();
            return RedirectToAction("FindRoom", "Home");
        }

        public ActionResult Logout()
        {
            Session["KH"] = null;
            return RedirectToAction("Login", "Account");
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }

        public ActionResult HoaDon()
        {
            KhachHang kh = new KhachHang();
            if (Session["KH"] != null)
                kh = (KhachHang)Session["KH"];
            else
                return RedirectToAction("Index", "Home");

            var dsHoaDon = db.HoaDons.Where(t => t.PhieuDatPhong.ma_kh == kh.ma_kh).ToList();
            return View(dsHoaDon);
        }
        public ActionResult PhieuDatPhong()
        {
            AutoHuyPhieuDatPhong();
            KhachHang kh = new KhachHang();
            if (Session["KH"] != null)
                kh = (KhachHang)Session["KH"];
            else
                return RedirectToAction("Index", "Home");

            var dsPDP = db.PhieuDatPhongs.Where(t => t.ma_kh == kh.ma_kh).ToList();
            return View(dsPDP);
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
        public ActionResult ChiTietHoaDon(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            HoaDon HoaDon = db.HoaDons.Find(id);
            if (HoaDon == null)
            {
                return HttpNotFound();
            }

            var tien_phong = (HoaDon.PhieuDatPhong.ngay_ra - HoaDon.PhieuDatPhong.ngay_vao).Value.TotalDays * HoaDon.PhieuDatPhong.Phong.LoaiPhong.gia;
            ViewBag.tien_phong = tien_phong;

            ViewBag.time_now = DateTime.Now.ToString();

            
            ViewBag.tong_tien = tien_phong;
            return View(HoaDon);
        }
    }
}
