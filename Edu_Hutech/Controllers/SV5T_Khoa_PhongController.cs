using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Edu_Hutech.Models;
namespace Edu_Hutech.Controllers
{
    public class SV5T_Khoa_PhongController : Controller
    {
        // GET: SV5T_Khoa_Phong
        HutechEduDataContext db = new HutechEduDataContext();
        string makhoa;
        public ActionResult DanhsachDK()
        {
            makhoa = System.Web.HttpContext.Current.Session["Khoa"].ToString();
            var lop = db.Lops.Where(s => s.MaKhoa == makhoa).Select(s => s).ToList();
            return View(lop);
        }
        [HttpGet]
        public ActionResult ds_sv_dk(string malop,string macap)
        {
            var ds = db.DsachDK_SV5T_Lop(malop, macap).ToList();
            return PartialView(ds);
        }
    }
}