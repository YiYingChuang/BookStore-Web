using BookStore_Web_.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace BookStore_Web_.Controllers
{
    public class EmployeeController : Controller
    {

        // GET: Employee
        public ActionResult Index()
        {
            return View();
        }
        public ActionResult Authority()
        {           
            return View();
        }
    }
}