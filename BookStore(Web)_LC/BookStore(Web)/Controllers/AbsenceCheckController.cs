using BookStore_Web_.Models;
using BookStore_Web_.Models.VM;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using PagedList;
using PagedList.Mvc;

namespace BookStore_Web_.Controllers
{
    public class AbsenceCheckController : Controller
    {
        //只有最高權限以及總經理可以進來!!

        BookStoreObject_Entities db = new BookStoreObject_Entities();
        Repository<Absence_Table> absenceTableRepository = null;
        Repository<Absence_Type> absenceTypeRepository = null;
        IEnumerable<BookStore_Web_.Models.Absence_Table> absenceTable = null;
        int EmpID = HomeController.EmpID = 11;
        int EmpOccu;
        public AbsenceCheckController()
        {
            absenceTableRepository = new Repository<Absence_Table>(new BookStoreObject_Entities());
            absenceTypeRepository = new Repository<Absence_Type>(new BookStoreObject_Entities());
            absenceTable = absenceTableRepository.GetAll();
            EmpOccu = db.Emp_Information.Where(p => p.EmployeeID == EmpID).Select(p => p.Emp_OccupationID).FirstOrDefault();
        }
        
        public ActionResult Check(int? page)
        {
            ViewBag.SortAbsenceTableNo = "AbsenceTableNo";
            ViewBag.SortAbsenceTableNoDesc = "AbsenceTableNodesc";
            ViewBag.SortAbsence_Type = "Absence_Type";
            ViewBag.SortAbsence_TypeDesc = "Absence_Typedesc";
            ViewBag.SortStartDate = "StartDate";
            ViewBag.SortStartDateDesc = "StartDatedesc";
            ViewBag.SortStatus = "Status";
            ViewBag.SortStatusDesc = "Statusdesc";
            //判斷權限
            absenceTable = EmployeeOccu(EmpOccu);

            return View(absenceTable.ToPagedList(page ?? 1, 5));            
        }

        public ActionResult CheckPager(int? page, int? Dept=0, int Occu=0)
        {            
            //判斷權限
            absenceTable = EmployeeOccu(EmpOccu, Dept, Occu);

            page = SetPage(page, absenceTable.Count());
            return PartialView(absenceTable.ToPagedList(page ?? 1, 5));
        }

        int? SetPage(int? page = 1, int data = 0)
        {
            if (data == 0)
                return page = 1;
            else if (data / 5 < page && data % 5 == 0)
                return page = data / 5;
            else if (data / 5 < page && data % 5 > 0)
                return page = data / 5 + 1;
            else
                return page;
        }

        IEnumerable<Absence_Table> EmployeeOccu(int empOccu, int? Dept = 0, int? Occu = 0)
        {
            absenceTable = absenceTable.Where(p => p.Check_ID == 1);
            if (empOccu == 2)//高權限
            {
                absenceTable = absenceTable.Where(p => p.Emp_Information.Emp_Report == EmpID && p.EmployeeID != EmpID);//只會看到 "report"是自己EmpID的請假單 看不到自己的假單
            }
            else if (empOccu == 1)//總經理
            {
                absenceTable = absenceTable.Where(p => p.Emp_Information.Emp_Report == EmpID);//只會看到 "report"是自己EmpID的請假單 自己不用請假
            }

            if (Dept != 0 && Occu != 0)
                absenceTable = absenceTable.Where(p => p.Emp_Information.Emp_DeptID == Dept && p.Emp_Information.Emp_OccupationID == Occu).ToList();
            else if (Dept != 0 && Occu == 0)
                absenceTable = absenceTable.Where(p => p.Emp_Information.Emp_DeptID == Dept).ToList();
            else if (Occu != 0 && Dept == 0)
                absenceTable = absenceTable.Where(p => p.Emp_Information.Emp_OccupationID == Occu).ToList();
            else
                absenceTable = absenceTable.ToList();

            return absenceTable;
        }

        //[ChildActionOnly]
        public ActionResult CheckPartial(int? page, string sortBy, int Dept=0, int Occu=0)
        {
            #region for sortBy
            switch (sortBy)
            {
                case "AbsenceTableNo":
                    absenceTable = absenceTable.OrderBy(p => p.Absence_No);
                    break;
                case "AbsenceTableNodesc":
                    absenceTable = absenceTable.OrderByDescending(p => p.Absence_No);
                    break;
                case "Absence_Type":
                    absenceTable = absenceTable.OrderBy(p => p.Absence_Type.Absence_Type1);
                    break;
                case "Absence_Typedesc":
                    absenceTable = absenceTable.OrderByDescending(p => p.Absence_Type.Absence_Type1);
                    break;
                case "StartDate":
                    absenceTable = absenceTable.OrderBy(p => p.StartDate.Date);
                    break;
                case "StartDatedesc":
                    absenceTable = absenceTable.OrderByDescending(p => p.StartDate.Date);
                    break;
                case "Status":
                    absenceTable = absenceTable.OrderBy(p => p.ChechStatus.Status);
                    break;
                case "Statusdesc":
                    absenceTable = absenceTable.OrderByDescending(p => p.ChechStatus.Status);
                    break;
                default:
                    absenceTable = absenceTable.OrderBy(p => p.Absence_No);
                    break;
            }
            #endregion

            //判斷權限
            absenceTable = EmployeeOccu(EmpOccu, Dept, Occu);

            page = SetPage(page, absenceTable.Count());
            return PartialView(absenceTable.ToPagedList(page ?? 1, 5));
        }

        public ActionResult Dept()
        {
            var dept = db.Dept_Information.Select(p => new
            {
                DeptID = p.Emp_DeptID,
                DeptName = p.Dept_Name
            });
            return Json(dept.ToList(), JsonRequestBehavior.AllowGet);

        }

        public ActionResult Occu()
        {
            var occu = db.Emp_Occupation.Select(p => new
            {
                OccuID = p.Emp_OccupationID,
                OccuName = p.Emp_OccupationName
            });
            return Json(occu.ToList(), JsonRequestBehavior.AllowGet);

        }

        public ActionResult GetImage(int id)
        {
            Absence_Table certificate_Doc = absenceTableRepository.GetByID(id);
            byte[] img = certificate_Doc.Certificate_Doc;
            if (img == null)
                return File(@"~\Content\AbsenceImage\oppos.jpg", "image/jpeg");
            else
                return new FileContentResult(img, "image/jpeg");
           
        }

        public ActionResult Accept(int id)
        {
            var accept = absenceTableRepository.GetByID(id);
            accept.Check_ID = 2;
            absenceTableRepository.Update(accept);
            //TODO 修改成功 頁面

            return RedirectToAction("Check");
        }
        public ActionResult Reject(int id)
        {
            var accept = absenceTableRepository.GetByID(id);
            accept.Check_ID = 3;
            absenceTableRepository.Update(accept);

            return RedirectToAction("Check");
        }
    }
}