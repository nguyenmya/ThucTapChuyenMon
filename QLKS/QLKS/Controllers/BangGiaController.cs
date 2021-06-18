using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace QLKS.Controllers
{
    public class BangGiaController : Controller
    {
        // GET: BangGia
        QLKS.Models.QLKSEntities db = new Models.QLKSEntities();
        public ActionResult Index()
        {
            return View();
        }
        public ActionResult GiaPhong()
        {
            return View(db.LoaiPhongs.ToList());
        }
    }
}