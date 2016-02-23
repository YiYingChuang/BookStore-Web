using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using BookStore_Web_.Models;

namespace BookStore_Web_.Controllers
{
    public class HomeController : Controller
    {
        Repository<Emp_Information> db = new Repository<Emp_Information>(new BookStoreObject_Entities());
        BookStoreObject_Entities detail = new BookStoreObject_Entities();
        // GET: Home
        internal static int EmpID;
        public ActionResult Index(Emp_Information emp)
        {
            if (db.GetByID(emp.EmployeeID) != null)
            {
                var q = db.GetByID(emp.EmployeeID);
                if (q.Emp_Password == emp.Emp_Password)
                {
                    EmpID = q.EmployeeID;
                    if (q.Emp_OccupationID == 1 || q.Emp_OccupationID == 2)
                    {
                        return RedirectToAction("Index","Admin");
                    }
                    else
                    {
                        return RedirectToAction("Index","Employee");
                    }
                }
                else
                {
                    ViewBag.message = "輸入錯誤，請從新輸入";
                    return RedirectToAction("login");
                }
            }
            else
            {
                ViewBag.message = "查無此員工";
                return RedirectToAction("login");
            }
        }
        public ActionResult Login()
        {
            return View();
        }

        [HttpGet]
        public ActionResult AddCount()
        {
            ViewBag.occu = new SelectList(detail.Dept_Information.ToList(), "Emp_DeptID", "Dept_Name");
            return View();
        }

        public ActionResult add()
        {
            ViewBag.occu = new SelectList(detail.Dept_Information.ToList(), "Emp_DeptID", "Dept_Name");
            return View();
        }
        public ActionResult Action()
        {
            return View();
        }
    }
}