using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Edu_Hutech.Models;

namespace Edu_Hutech.Controllers
{
    public class UserLoginController : Controller
    {
        HutechEduDataContext db = new HutechEduDataContext();

        // GET: UserLogin
        [HttpGet]
        public ActionResult UserLogin()
        {
            return View();
        }

        [HttpPost]
        public ActionResult UserLogin(FormCollection collection)
        {
            string tendn = collection["TaiKhoan"].ToString();
            var matkhau = collection["MatKhau"];

            var infoSV = db.Login_Acc(tendn, matkhau).SingleOrDefault();
            if (infoSV != null)
            {
                System.Web.HttpContext.Current.Session["SinhVien"] = tendn;
                var path_data = db.MinhChungs.Where(s => s.TenMInhChung == tendn).Select(s => s.URL).SingleOrDefault();
                string path = "~/MinhChung/" + tendn;
                if (!Directory.Exists(Server.MapPath(path)) && path_data == null)
                {
                    Directory.CreateDirectory(Server.MapPath(path));
                    db.Add_MinhChung(tendn, path);
                    db.SubmitChanges();
                }
                return RedirectToAction("Index", "SinhVien5Tot");
            }
            var infoKhoa = db.TaiKhoan_ADMINs.Where(s => s.TaiKhoan == tendn && s.MatKhau == matkhau).Select(s => s).SingleOrDefault();
            if (infoKhoa != null)
            {
                string[] makhoa = tendn.Split('0');
                System.Web.HttpContext.Current.Session["Khoa"] = makhoa[0];
                return RedirectToAction("DanhsachDK", "SV5T_Khoa_Phong");
            }
            else
            {
                ViewBag.ThongBao = "Tên đăng nhập hoặc mật khẩu không đúng.";
            }
            return View();
        }
    }
}