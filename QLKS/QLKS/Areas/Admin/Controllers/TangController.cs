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
    public class TangController : Controller
    {
        private QLKSEntities db = new QLKSEntities();

        // GET: DS Tang
        public ActionResult Index()
        {
            return View(db.Tangs.ToList());
        }

        // GET: Tang/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Tang Tang = db.Tangs.Find(id);
            if (Tang == null)
            {
                return HttpNotFound();
            }
            return View(Tang);
        }

        // GET: Tang/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: Tang/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        //them tang


        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "ma_tang,ten_tang")] Tang Tang)
        {
            if (ModelState.IsValid)
            {
                db.Tangs.Add(Tang);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            return View(Tang);
        }

        // GET: Tang/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Tang Tang = db.Tangs.Find(id);
            if (Tang == null)
            {
                return HttpNotFound();
            }
            return View(Tang);
        }

        // POST: Tang/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "ma_tang,ten_tang")] Tang Tang)
        {
            if (ModelState.IsValid)
            {
                db.Entry(Tang).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(Tang);
        }

        // GET: Tang/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Tang Tang = db.Tangs.Find(id);
            if (Tang == null)
            {
                return HttpNotFound();
            }
            return View(Tang);
        }

        // POST: Tang/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            try
            {
                Tang Tang = db.Tangs.Find(id);
                db.Tangs.Remove(Tang);
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
