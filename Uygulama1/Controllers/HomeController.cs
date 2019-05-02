using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Uygulama1.Models;

namespace Uygulama1.Controllers
{
    public class HomeController : Controller
    {
        MyContext db = new MyContext();
        // GET: Home
        public ActionResult Index()
        {
            var data = db.Urun.ToList();
            CerezOkuma();
            return View(data);
        }

        public ActionResult SepeteEkle(int id)
        {
            CerezOlustur(id);
            return RedirectToAction("Index");
        }
        public void CerezOlustur(int id)
        {
            var urun = db.Urun.Find(id);
            if (urun != null)
            {
                HttpCookie UrunCerez = new HttpCookie("Urun");
                UrunCerez.Values.Add("UrunId", urun.Id.ToString());
                UrunCerez.Values.Add("UrunAd", urun.Ad.ToString());
                UrunCerez.Values.Add("Urunadet", urun.Adet.ToString());
                UrunCerez.Values.Add("Urunfiyat", urun.Fiyat.ToString());
                UrunCerez.Expires = DateTime.Now.AddDays(1);
                Response.Cookies.Add(UrunCerez);
            }
        }
        public ActionResult SatinAl(string id)
        {
            CerezOlustur(Convert.ToInt32(id));
            var urun = db.Urun.Find(Convert.ToInt32(id));
            if (urun == null || id != HttpContext.Request.Cookies["Urun"].Values["UrunId"])
            {
                return RedirectToAction("Index");
            }
            else
            {
                return View(urun);
            }

        }
        public ActionResult SatinAlOnay(int id)
        {
            var urun = db.Urun.Find(id);
            if (urun != null)
            {
                urun.Adet -= 1;
                db.SaveChanges();
                CerezSil();
                return RedirectToAction("Index");
            }
            else
            {
                ViewBag.Hata = "İptal Edildi.";
                return RedirectToAction("Index");
            }
        }
        private string CerezOkuma()
        {

            if (Request.Cookies["Urun"] != null)
            {
                ViewBag.UrunId = HttpContext.Request.Cookies["Urun"].Values["UrunId"];
                ViewBag.UrunAd = HttpContext.Request.Cookies["Urun"].Values["UrunAd"];
                ViewBag.Urunadet = HttpContext.Request.Cookies["Urun"].Values["Urunadet"];
                ViewBag.Urunfiyat = HttpContext.Request.Cookies["Urun"].Values["Urunfiyat"];
                return ViewBag.UrunAd;
            }
            else { return null; }
        }

        public ActionResult CerezSil()
        {
            if (Request.Cookies["Urun"] != null)
            {
                Response.Cookies.Remove("Urun");
                Response.Cookies["Urun"].Expires = DateTime.Now.AddDays(-1);
                return RedirectToAction("Index");
            }
            else
            {
                return RedirectToAction("Index");
            }
        }
    }
}