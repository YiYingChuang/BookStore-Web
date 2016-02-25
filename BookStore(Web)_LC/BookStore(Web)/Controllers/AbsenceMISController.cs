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
    public class AbsenceMISController : Controller
    {
        //只有資訊部 總經理可以進來
        BookStoreObject_Entities db = new BookStoreObject_Entities();
        Repository<Absence_Table> absenceTableRepository = null;
        Repository<Absence_Type> absenceTypeRepository = null;
        IEnumerable<BookStore_Web_.Models.Absence_Table> absenceTable = null;
        int EmpID = HomeController.EmpID = 3;
        int EmpOccu;
        public AbsenceMISController()
        {
            absenceTableRepository = new Repository<Absence_Table>(new BookStoreObject_Entities());
            absenceTypeRepository = new Repository<Absence_Type>(new BookStoreObject_Entities());
            absenceTable = absenceTableRepository.GetAll();
            EmpOccu = db.Emp_Information.Where(p => p.EmployeeID == EmpID).Select(p => p.Emp_OccupationID).FirstOrDefault();
        }
        
        public ActionResult MIS(int? page)
        {
            ViewBag.SortAbsenceTableNo = "AbsenceTableNo";
            ViewBag.SortAbsenceTableNoDesc = "AbsenceTableNodesc";
            ViewBag.SortAbsence_Type = "Absence_Type";
            ViewBag.SortAbsence_TypeDesc = "Absence_Typedesc";
            ViewBag.SortStartDate = "StartDate";
            ViewBag.SortStartDateDesc = "StartDatedesc";
            ViewBag.SortStatus = "Status";
            ViewBag.SortStatusDesc = "Statusdesc";
            
            absenceTable = MIS_Dept_Occu();
            return View(absenceTable.ToPagedList(page ?? 1, 5));
        }

        public ActionResult MISPager(int? page, int Dept=0, int Occu=0)
        {
            absenceTable = MIS_Dept_Occu(Dept, Occu);

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

        IEnumerable<Absence_Table> MIS_Dept_Occu(int Dept = 0, int Occu = 0)
        {
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
        public ActionResult MISPartial(int? page, string sortBy, int Dept=0, int Occu=0)
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
            
            absenceTable = MIS_Dept_Occu(Dept, Occu);
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

        public ActionResult Delete(int id)
        {
            var delete = absenceTableRepository.GetByID(id);
            delete.Dispose = true;
            absenceTableRepository.Update(delete);
            return RedirectToAction("MIS");
        }

        [HttpGet]
        public ActionResult Edit(int id)
        {
            ViewBag.AbsenceType = new SelectList(absenceTypeRepository.GetAll().ToList(), "Absence_ID", "Absence_Type1");
            var editAbsence = absenceTableRepository.GetByID(id);
            
            return View(editAbsence);
        }

        [HttpPost]
        public ActionResult Edit(Absence_Table _absenceTable)
        {            
            Absence_Table ab = absenceTableRepository.GetByID(_absenceTable.Absence_No);
            ab.Absence_ID = _absenceTable.Absence_ID;
            ab.StartDate = _absenceTable.StartDate;
            ab.EndDate = _absenceTable.EndDate;
            ab.Reason = _absenceTable.Reason;
            ab.Check_ID = 1;
            HttpPostedFileBase file = Request.Files["CertificateDoc"];
            if(file.ContentLength>0)
            {
                var imgSize = file.ContentLength;
                byte[] imgByte = new Byte[imgSize];
                file.InputStream.Read(imgByte, 0, imgSize);
                ab.Certificate_Doc = imgByte;
            }
            absenceTableRepository.Update(ab);
            return RedirectToAction("MIS");
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

            return RedirectToAction("MIS");
        }
        public ActionResult Reject(int id)
        {
            var accept = absenceTableRepository.GetByID(id);
            accept.Check_ID = 3;
            absenceTableRepository.Update(accept);

            return RedirectToAction("MIS");
        }
    }
}