using databasefirst1.DAL;
using databasefirst1.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace databasefirst1.Controllers
{
    public class HomeController : Controller
    {
        studentaccess DB = new studentaccess();                                      
        private int procid;
        // GET: home
        public ActionResult Index()
        {

            return View();
        }
        public ActionResult adddetail()
        {

            student Model = new student();
            if (Request.QueryString["sid"] != null)
            {
                Model.StudentId = Convert.ToInt32(Request.QueryString["sid"].ToString());
                Model = DB.Students(3, Model).ToList().FirstOrDefault();
                ViewBag.buttonName = "Update";
            }
            else
            {
                ViewBag.buttonName = "Submit";
            }

            var list1 = DB.Students(4, Model).ToList();
            if (list1.Count > 0)
                ViewBag.list = list1;
            else
                ViewBag.list = null;

            return View(Model);
            
        }
        [HttpPost]
        public ActionResult Adddetail(student Model, string command, HttpPostedFileBase imgInp)
        
        {
            
            string prepath = "~/Content/Uploads";
            string path = "";
            
            var uploadUrl = Server.MapPath(prepath);
            string extension = System.IO.Path.GetExtension(Request.Files["imgInp"].FileName);
            if (extension.ToLower() == ".jpg" || extension.ToLower() == ".jpeg")
            {
                if (imgInp.ContentLength <= 0)
                {
                    TempData["Message"] = "Please Upload Photo !";
                }
                else
                {
                    imgInp = Request.Files["imgInp"];
                    string Name = DateTime.Now.Ticks + "_P" + extension.ToLower().ToString();
                    string pathtosave = prepath + "/" + Name;
                    path = uploadUrl + "/" + Name;
                    Model.imagee = pathtosave;
                    imgInp.SaveAs(path);
                }
            }
            else
            {
                TempData["Message"] = "Please Upload Valid File !";
            }
            if (command == "Submit")
                procid = 1;
            else if (command == "Update")
                procid = 2;
            var list = DB.Students(procid, Model).ToList();
            if (list.Count > 0)
            {
                if (list[0].msg == "success")
                {
                    TempData["msg"] = "1";
                }
                else if (list[0].msg == "exists")
                {
                    TempData["msg"] = "2";
                }
                else if (list[0].msg == "update")
                {
                    TempData["msg"] = "3";
                }
                else
                {
                    TempData["msg"] = "4";
                }
                ViewBag.list = list;
            }
            else
            {
                ViewBag.list = null;
            }
            return RedirectToAction("Adddetail", "Home");
        }
        public ActionResult EditStudent(int id)
        {
            student Model = new student();
            return RedirectToAction("Adddetail", new { sid = id });
        }
        public JsonResult StudentDelete(int sid)
        {
            student Model = new student();
            Model.StudentId = sid;
            var data = DB.Students(5, Model).ToList().FirstOrDefault();
            return Json(true, JsonRequestBehavior.AllowGet);



        }
    }
}