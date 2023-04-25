using BAITAPLON.Models;
using BAITAPLON.Models.Map;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace BAITAPLON.Areas.Admin.Controllers
{
    public class NhanVienController : Controller
    {
        // GET: Admin/NhanVien

        public bool KiemTraChucNang( int idChucNang)
        {
            QuanLySanPhamEntities db = new QuanLySanPhamEntities();
            NhanVien nvSession = (NhanVien)Session["user"];
            var count = db.PhanQuyens.Count(m => m.idNhanVien == nvSession.ID & m.idChucNang == idChucNang);
            if (count == 0)
            {
                // báo ko có quyền 
                return false;
            }
            else
            {
                return true;
            }
        }
        public ActionResult DanhSach()
        {
            if(KiemTraChucNang(1) == false)
            {
                return Redirect("/Admin/BaoLoi/KhongCoQuyen");
            }

            var map = new mapNhanVien();
            return View(map.DanhSach());
        }

      

        public ActionResult ThemMoi()
        {
            if (KiemTraChucNang(1) == false)
            {
                return Redirect("/Admin/BaoLoi/KhongCoQuyen");
            }
            return View();
        }
       
        [HttpPost]
        public ActionResult ThemMoi(NhanVien model, HttpPostedFileBase file)
        {




            var map = new mapNhanVien();
            if (map.ThemMoi(model) > 0)
            {
                return RedirectToAction("DanhSach");
            }
            else
            {
                ModelState.AddModelError("", map.message);
                return View(model);
            }

        }

        // update

        public ActionResult CapNhat(int ID)
        {
            if (KiemTraChucNang(1) == false)
            {
                return Redirect("/Admin/BaoLoi/KhongCoQuyen");
            }
            var map = new mapNhanVien();
            return View(map.ChiTiet(ID));
        }

      

        [HttpPost]
        public ActionResult CapNhat(NhanVien model, HttpPostedFileBase file)
        {
           
            var map = new mapNhanVien();
            if (map.CapNhat(model) > 0)
            {
                return RedirectToAction("DanhSach");
            }
            else
            {
                ModelState.AddModelError("", map.message);
                return View(model);
            }
        }



        // delete

        public ActionResult Xoa(int id)
        {
            if (KiemTraChucNang(1) == false)
            {
                return Redirect("/Admin/BaoLoi/KhongCoQuyen");
            }
            var map = new mapNhanVien();
            map.Xoa(id);
            return RedirectToAction("DanhSach");
        }
      

    }
}