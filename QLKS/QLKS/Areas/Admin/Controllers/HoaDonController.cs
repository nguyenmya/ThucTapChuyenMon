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
    public class HoaDonController : Controller
    {
        private QLKSEntities db = new QLKSEntities();

        // GET: HoaDon
        public ActionResult Index()
        {
            var HoaDons = db.HoaDons.Where(t => t.ma_tinh_trang == 2).Include(t => t.NhanVien).Include(t => t.PhieuDatPhong)
                .Include(t => t.TinhTrangHoaDon);
            double tong = 0;
            foreach (var item in HoaDons.ToList())
            {
                if (item.ma_tinh_trang == 2)
                {
                    tong += (double)item.tien_phong;
                }
            }
            ViewBag.tong_tien = String.Format("{0:0,0.00}", tong);
            return View(HoaDons.ToList());
        }

        [HttpPost]
        public ActionResult Index(String beginDate, String endDate)
        {
            System.Diagnostics.Debug.WriteLine("your message here " + beginDate);
            List<HoaDon> dshd = new List<HoaDon>();
            String query = "select * from HoaDon where ma_tinh_trang=2 ";
            if (!beginDate.Equals(""))
                query += " and ngay_tra_phong >= '" + beginDate + "'";
            if (!endDate.Equals(""))
                query += " and ngay_tra_phong <= '" + endDate + "'";

            dshd = db.HoaDons.SqlQuery(query).ToList();
            double tong = 0;
            foreach (var item in dshd)
            {
                if (item.ma_tinh_trang == 2)
                {
                    tong += (double)item.tien_phong;
                }
            }
            ViewBag.tong_tien = tong.ToString("C");
            return View(dshd);
        }

        // GET: HoaDon/Details/5
        public ActionResult Details(int? id)
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
            return View(HoaDon);
        }

        // GET: HoaDon/Create
        public ActionResult Create()
        {
            ViewBag.ma_nv = new SelectList(db.NhanViens, "ma_nv", "ho_ten");
            ViewBag.ma_pdp = new SelectList(db.PhieuDatPhongs, "ma_pdp", "ma_kh");
            ViewBag.ma_tinh_trang = new SelectList(db.TinhTrangHoaDons, "ma_tinh_trang", "mo_ta");
            return View();
        }

        // POST: HoaDon/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "ma_hd,ma_pdp,ngay_tra_phong,ma_tinh_trang")] HoaDon HoaDon)
        {
            if (ModelState.IsValid)
            {
                db.HoaDons.Add(HoaDon);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            ViewBag.ma_nv = new SelectList(db.NhanViens, "ma_nv", "ho_ten", HoaDon.ma_nv);
            ViewBag.ma_pdp = new SelectList(db.PhieuDatPhongs, "ma_pdp", "ma_kh", HoaDon.ma_pdp);
            ViewBag.ma_tinh_trang = new SelectList(db.TinhTrangHoaDons, "ma_tinh_trang", "mo_ta", HoaDon.ma_tinh_trang);
            return View(HoaDon);
        }
        public ActionResult Add(int? id)
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
        // GET: HoaDon/Edit/5
        public ActionResult Edit(int? id)
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
            ViewBag.ma_nv = new SelectList(db.NhanViens, "ma_nv", "ho_ten", HoaDon.ma_nv);
            ViewBag.ma_pdp = new SelectList(db.PhieuDatPhongs, "ma_pdp", "ma_kh", HoaDon.ma_pdp);
            ViewBag.ma_tinh_trang = new SelectList(db.TinhTrangHoaDons, "ma_tinh_trang", "mo_ta", HoaDon.ma_tinh_trang);
            return View(HoaDon);
        }

        // POST: HoaDon/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "ma_hd,ma_nv,ma_pdp,ngay_tra_phong,ma_tinh_trang,tien_phong,tien_dich_vu,phu_thu,tong_tien")] HoaDon HoaDon)
        {
            if (ModelState.IsValid)
            {
                db.Entry(HoaDon).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.ma_nv = new SelectList(db.NhanViens, "ma_nv", "ho_ten", HoaDon.ma_nv);
            ViewBag.ma_pdp = new SelectList(db.PhieuDatPhongs, "ma_pdp", "ma_kh", HoaDon.ma_pdp);
            ViewBag.ma_tinh_trang = new SelectList(db.TinhTrangHoaDons, "ma_tinh_trang", "mo_ta", HoaDon.ma_tinh_trang);
            return View(HoaDon);
        }

        // GET: HoaDon/Delete/5
        public ActionResult Delete(int? id)
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
            return View(HoaDon);
        }

        // POST: HoaDon/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            HoaDon HoaDon = db.HoaDons.Find(id);
            db.HoaDons.Remove(HoaDon);
            db.SaveChanges();
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
        public ActionResult Result(String ma_pdp)
        {
            if (ma_pdp == null)
            {
                return RedirectToAction("Index", "Index");
            }
            else
            {
                PhieuDatPhong pt = db.PhieuDatPhongs.Find(Int32.Parse(ma_pdp));
               
                HoaDon hd = new HoaDon();
                hd.ma_pdp = Int32.Parse(ma_pdp);
                hd.ma_tinh_trang = 1;
                try
                {
                    db.HoaDons.Add(hd);
                    PhieuDatPhong tgd = db.PhieuDatPhongs.Find(Int32.Parse(ma_pdp));
                    if (tgd == null)
                    {
                        return HttpNotFound();
                    }
                    Phong p = db.Phongs.Find(tgd.ma_phong);
                    if (p == null)
                    {
                        return HttpNotFound();
                    }
                    tgd.ma_tinh_trang = 2;
                    db.Entry(tgd).State = EntityState.Modified;
                    p.ma_tinh_trang = 2;
                    db.Entry(p).State = EntityState.Modified;
                    ViewBag.ngay_ra = tgd.ngay_ra;
                    db.SaveChanges();
                    ViewBag.Result = "success";
                }
                catch
                {
                    ViewBag.Result = "error";
                }
            }
            return View();
        }
        public ActionResult ThanhToan(int? id)
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
            DateTime ngay_ra = DateTime.Now;
            DateTime ngay_vao = (DateTime)HoaDon.PhieuDatPhong.ngay_vao;
            DateTime ngay_du_kien = (DateTime)HoaDon.PhieuDatPhong.ngay_ra;

            DateTime dateS = new DateTime(ngay_vao.Year, ngay_vao.Month, ngay_vao.Day, 12, 0, 0);
            DateTime dateE = new DateTime(ngay_ra.Year, ngay_ra.Month, ngay_ra.Day, 12, 0, 0);

            Double gia = (Double)HoaDon.PhieuDatPhong.Phong.LoaiPhong.gia;

            var songay = (dateE - dateS).TotalDays;
            if (dateS > ngay_vao)
                songay++;
            if (ngay_ra > dateE)
                songay++;

            var tien_phong = songay * gia;
            ViewBag.tien_phong = tien_phong;
            ViewBag.so_ngay = songay;

            NhanVien nv = (NhanVien)Session["NV"];
            if (nv != null)
            {
                ViewBag.ho_ten = nv.ho_ten;
            }
            ViewBag.tong_tien = tien_phong;
            return View(HoaDon);
        }



        /// <summary>
        /// ///////////////////

        /// <returns></returns>
        /// 

        public ActionResult XacNhanThanhToan(String ma_hd, String tien_phong)
        {
            if (ma_hd == null || tien_phong == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            try
            {
                HoaDon hd = db.HoaDons.Find(Int32.Parse(ma_hd));
                NhanVien nv = (NhanVien)Session["NV"];
                if (nv != null)
                    hd.ma_nv = nv.ma_nv;
                hd.tien_phong = Double.Parse(tien_phong);
                hd.ma_tinh_trang = 2;
                hd.ngay_tra_phong = DateTime.Now;
                db.Entry(hd).State = EntityState.Modified;

                Phong p = db.Phongs.Find(hd.PhieuDatPhong.ma_phong);
                p.ma_tinh_trang = 3;
                PhieuDatPhong pd = db.PhieuDatPhongs.Find(hd.PhieuDatPhong.ma_pdp);
                pd.ma_tinh_trang = 4;
                db.Entry(p).State = EntityState.Modified;
                db.Entry(pd).State = EntityState.Modified;
                db.SaveChanges();

                ViewBag.result = "success";
            }
            catch
            {
                ViewBag.result = "error";
            }
            ViewBag.ma_hd = ma_hd;
            return View();
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
        public ActionResult GiaHanPhong(int? id)
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
            PhieuDatPhong pdp = db.PhieuDatPhongs.Find(HoaDon.ma_pdp);
            String dt = null;
            try
            {
                DateTime d = (DateTime)db.PhieuDatPhongs.Where(t => t.ma_tinh_trang == 1 && t.ma_phong == pdp.Phong.ma_phong).Select(t => t.ngay_vao).OrderBy(t => t.Value).First();
                dt = d.ToString();
            }
            catch
            {

            }
            ViewBag.dateMax = dt;
            return View(pdp);
        }
        public ActionResult ResultGiaHan(String ma_pdp, String ngay_ra)
        {
            if (ma_pdp == null || ngay_ra == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            try
            {
                PhieuDatPhong pdp = db.PhieuDatPhongs.Find(Int32.Parse(ma_pdp));
                DateTime ngayra = DateTime.Parse(ngay_ra);
                pdp.ngay_ra = ngayra;
                ViewBag.result = "success";
                ViewBag.ngay_ra = ngay_ra;
            }
            catch (Exception e)
            {
                ViewBag.result = "error: " + e;
            }
            return View();
        }


        public ActionResult DoiPhong(int? id)
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
            PhieuDatPhong pdp = db.PhieuDatPhongs.Find(HoaDon.ma_pdp);

            var li = db.Phongs.Where(t => t.ma_tinh_trang == 1 && !(db.PhieuDatPhongs.Where(m => (m.ma_tinh_trang == 1 || m.ma_tinh_trang == 2) && m.ngay_ra > DateTime.Now && m.ngay_vao < pdp.ngay_ra)).Select(m => m.ma_phong).ToList().Contains(t.ma_phong));
            ViewBag.ma_phong_moi = new SelectList(li, "ma_phong", "so_phong");
            return View(pdp);
        }

        public ActionResult ResultDoiPhong(String ma_pdp, String ma_phong_cu, String ma_phong_moi)
        {
            if (ma_pdp == null || ma_phong_cu == null || ma_phong_moi == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            try
            {
                PhieuDatPhong pdp = db.PhieuDatPhongs.Find(Int32.Parse(ma_pdp));
                Phong p = db.Phongs.Find(pdp.Phong.ma_phong);      // lấy thông tin phòng cũ
                p.ma_tinh_trang = 3;                                        // set phòng cũ về đang dọn
                db.Entry(p).State = EntityState.Modified;
                pdp.ma_phong = Int32.Parse(ma_phong_moi);                   // đổi phòng cũ sang mới
                p = db.Phongs.Find(Int32.Parse(ma_phong_moi));           // lấy thông tin phòng mới
                p.ma_tinh_trang = 2;                                        // set phòng mới về đang sd
                db.Entry(p).State = EntityState.Modified;
                db.Entry(pdp).State = EntityState.Modified;
                db.SaveChanges();
                ViewBag.result = "success";
            }
            catch (Exception e)
            {
                ViewBag.result = "error: " + e;
            }
            return View();
        }
    }
}
