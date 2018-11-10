using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Edu_Hutech.Models;
namespace Edu_Hutech.Controllers
{
    public class SinhVien5TotController : Controller
    {
        // GET: SinhVien5Tot
        HutechEduDataContext db = new HutechEduDataContext();
        string mssv = System.Web.HttpContext.Current.Session["SinhVien"].ToString();
        public ActionResult Index()
        {
            return View();
        }
        public ActionResult SV5T()
        {
            var PDK = db.PhieuDangKy_SV5Ts.Where(s => s.MSSV == mssv).Select(s => s).FirstOrDefault();
            if (PDK == null)
            {
                return View();
            }
            string macap = PDK.MaCap;
            return RedirectToAction("theodoi_SV5T", new { macap = macap});
        }

        [HttpPost]
        public ActionResult Dky()
        {
            var maMC = db.MinhChungs.Where(a => a.TenMInhChung == mssv).Select(a => a.MaMinhChung).FirstOrDefault();
            string years = DateTime.Now.ToString().Trim();
            int year = Int32.Parse(years.Substring(6, 4));
            string macap = System.Web.HttpContext.Current.Request.Params["dky_sv5t"];            
            var tam = db.GetTaiLieu_SV5T_TheoCap(macap).ToList();
            int? maPDK = db.DKy_SV5T(maMC, macap, year, mssv).First().MaPDK;
            foreach (var item in tam)
                {
                    db.DKy_SV5T_2(maPDK,item.Ma_NDTieuChi);
                }
                db.SubmitChanges();
                return Json(new { success = true }, JsonRequestBehavior.AllowGet);
        }

        public ActionResult theodoi_SV5T(string macap)
        {            
            System.Web.HttpContext.Current.Session["MaCap"] = macap;
            var theo_doi = db.GetTieuChiSV5T_TheoMSSV_TheoCap(mssv,macap).ToList();
            return View(theo_doi);
        }
        public ActionResult LHTT()
        {
            return View();
        }
    }
}