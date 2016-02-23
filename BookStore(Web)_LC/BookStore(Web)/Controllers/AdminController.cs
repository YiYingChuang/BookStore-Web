using BookStore_Web_.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace BookStore_Web_.Controllers
{
    public class AdminController : Controller
    {
        BookStoreObject_Entities detail = new BookStoreObject_Entities();
        // GET: Admin
        public ActionResult Index()
        {
            return View();
        }
        public ActionResult Authority()
        {
            ViewBag.Dept = new SelectList(detail.Dept_Information.ToList(), "Emp_DeptID", "Dept_Name");
            ViewBag.Occu = new SelectList(detail.Emp_Occupation.ToList(), "Emp_OccupationID", "Emp_OccupationName");
            
            return View();
        }
    }
}