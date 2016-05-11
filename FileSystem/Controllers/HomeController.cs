using FileSystem.Classes;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace FileSystem.Controllers
{
    public class HomeController : Controller
    {
        public ActionResult Index()
        {
            return View();
        }

        public JsonResult GetDirectoryData(string path) {
            if (ModelState.IsValid) {
                if (path == "") {
                    path = Server.MapPath("~");
                }
            
                var model = FileSystemAnalyser.GetData(path);

                return Json(model);
            }
            return Json(null);
        }
    }
}
