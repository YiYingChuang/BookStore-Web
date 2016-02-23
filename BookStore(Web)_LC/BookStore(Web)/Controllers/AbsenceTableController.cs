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
    public class AbsenceTableController : Controller
    {
        BookStoreObject_Entities db = new BookStoreObject_Entities();
        Repository<Absence_Table> absenceTableRepository = null;
        Repository<Absence_Type> absenceTypeRepository = null;
        int EmpID = HomeController.EmpID = 3;
        public AbsenceTableController()
        {
            absenceTableRepository = new Repository<Absence_Table>(new BookStoreObject_Entities());
            absenceTypeRepository = new Repository<Absence_Type>(new BookStoreObject_Entities());
        }
        
        public ActionResult Index(int? page, string sortBy)
        {
            ViewBag.SortAbsenceTableNo = "AbsenceTableNo";
            ViewBag.AbsenceTableNoDesc = "AbsenceTableNo desc";
            ViewBag.SortAbsence_Type = "Absence_Type";
            ViewBag.SortAbsence_TypeDesc = "Absence_Type desc";
            ViewBag.StartDate = "StartDate";
            ViewBag.StartDateDesc = "StartDate desc";
            ViewBag.EndDate = sortBy = "EndDate";
            ViewBag.EndDateDesc = "EndDate desc";
            ViewBag.Reason = "Reason";
            ViewBag.ReasonDesc = "Reason desc";
            ViewBag.Status = "Status";
            ViewBag.StatusDesc = "Status desc";
            var absenceTable = absenceTableRepository.GetAll();
            //switch(sortBy)
            //{
            //    case "AbsenceTableNo":
            //        absenceTable = absenceTable.OrderBy(p => p.Absence_No);
            //        break;
            //    case "AbsenceTableNo desc":
            //        absenceTable = absenceTable.OrderByDescending(p => p.Absence_No);
            //        break;
            //    case "Absence_Type":
            //        absenceTable = absenceTable.OrderBy(p => p.Absence_Type.Absence_Type1);
            //        break;
            //    case "Absence_Type desc":
            //        absenceTable = absenceTable.OrderByDescending(p => p.Absence_Type.Absence_Type1);
            //        break;
            //    case "StartDate":
            //        absenceTable = absenceTable.OrderBy(p => p.StartDate.Date);
            //        break;
            //    case "StartDate desc":
            //        absenceTable = absenceTable.OrderByDescending(p => p.StartDate.Date);
            //        break;
            //    case "EndDate":
            //        absenceTable = absenceTable.OrderBy(p => p.EndDate.Date);
            //        break;
            //    case "EndDate desc":
            //        absenceTable = absenceTable.OrderByDescending(p => p.EndDate.Date);
            //        break;
            //    case "Reason":
            //        absenceTable = absenceTable.OrderBy(p => p.Reason);
            //        break;
            //    case "Reason desc":
            //        absenceTable = absenceTable.OrderByDescending(p => p.Reason);
            //        break;
            //    case "Status":
            //        absenceTable = absenceTable.OrderBy(p => p.ChechStatus.Status);
            //        break;
            //    case "Status desc":
            //        absenceTable = absenceTable.OrderByDescending(p => p.ChechStatus.Status);
            //        break;
            //    default:
            //        absenceTable = absenceTable.OrderBy(p => p.Absence_No);
            //        break;
            //}
            var q = db.Emp_Information.Where(p => p.EmployeeID == EmpID).Select(p => p.Emp_OccupationID).FirstOrDefault();
            if (q == 3)
                return View(absenceTable.Where(p => p.EmployeeID == EmpID).ToList().ToPagedList(page ?? 1, 5));
            else
                return View(absenceTable.ToList().ToPagedList(page ?? 1, 5));
            //return View(absenceTable.ToList());
        }

        //[ChildActionOnly]
        public ActionResult IndexPartial(int? page, string sortBy, int? Dept, int? Occu)
        {

            ViewBag.SortAbsenceTableNo = "AbsenceTableNo";
            ViewBag.AbsenceTableNoDesc = "AbsenceTableNo desc";
            ViewBag.SortAbsence_Type = "Absence_Type"; 
            ViewBag.SortAbsence_TypeDesc = "Absence_Type desc";
            ViewBag.StartDate = "StartDate"; 
            ViewBag.StartDateDesc = "StartDate desc";
            ViewBag.EndDate = sortBy = "EndDate";
            ViewBag.EndDateDesc = "EndDate desc";
            ViewBag.Reason = "Reason";
            ViewBag.ReasonDesc = "Reason desc";
            ViewBag.Status = "Status";
            ViewBag.StatusDesc = "Status desc";
            var absenceTable = absenceTableRepository.GetAll();
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
                case "Absence_Type desc":
                    absenceTable = absenceTable.OrderByDescending(p => p.Absence_Type.Absence_Type1);
                    break;
                case "StartDate":
                    absenceTable = absenceTable.OrderBy(p => p.StartDate.Date);
                    break;
                case "StartDate desc":
                    absenceTable = absenceTable.OrderByDescending(p => p.StartDate.Date);
                    break;
                case "EndDate":
                    absenceTable = absenceTable.OrderBy(p => p.EndDate.Date);
                    break;
                case "EndDate desc":
                    absenceTable = absenceTable.OrderByDescending(p => p.EndDate.Date);
                    break;
                case "Reason":
                    absenceTable = absenceTable.OrderBy(p => p.Reason);
                    break;
                case "Reason desc":
                    absenceTable = absenceTable.OrderByDescending(p => p.Reason);
                    break;
                case "Status":
                    absenceTable = absenceTable.OrderBy(p => p.ChechStatus.Status);
                    break;
                case "Status desc":
                    absenceTable = absenceTable.OrderByDescending(p => p.ChechStatus.Status);
                    break;
                default:
                    absenceTable = absenceTable.OrderBy(p => p.Absence_No);
                    break;
            }
            if (Request.UrlReferrer.Query != "" && Request.UrlReferrer.Query.Split('=')[1] != "0")
            {
                page = Convert.ToInt32(Request.UrlReferrer.Query.Split('=')[1]);
            }
            var q = db.Emp_Information.Where(p => p.EmployeeID == EmpID).Select(p => p.Emp_OccupationID).FirstOrDefault();
            if (q == 3)
                return PartialView(absenceTable.Where(p => p.EmployeeID == EmpID).ToList().ToPagedList(page ?? 1, 5));
            else
            {
                if (Dept != null && Occu != null)
                {
                    if (Dept != 0 && Occu != 0)
                        return PartialView(absenceTable.Where(p => p.Emp_Information.Emp_DeptID == Dept && p.Emp_Information.Emp_OccupationID == Occu).ToList().ToPagedList(page ?? 1, 5));
                    else if (Dept != 0 && Occu == 0)
                    {
                        return PartialView(absenceTable.Where(p => p.Emp_Information.Emp_DeptID == Dept).ToList().ToPagedList(page ?? 1, 5));
                    }
                    else if (Occu != 0 && Dept == 0)
                    {
                        return PartialView(absenceTable.Where(p => p.Emp_Information.Emp_OccupationID == Occu).ToList().ToPagedList(page ?? 1, 5));
                    }
                    else
                        return PartialView(absenceTable.ToList().ToPagedList(page ?? 1, 5));
                }
                else
                    return PartialView(absenceTable.ToList().ToPagedList(page ?? 1, 5));
            }
            //TODO...如果是網管人員 不需要限制employeeID
        }

        public ActionResult Dept()
        {
            var q = db.Dept_Information.Select(p => new
            {
                DeptID = p.Emp_DeptID,
                DeptName = p.Dept_Name
            });
            return Json(q.ToList(),JsonRequestBehavior.AllowGet);

        }

        public ActionResult Occu()
        {
            var q = db.Emp_Occupation.Select(p => new
            {
                OccuID = p.Emp_OccupationID,
                OccuName = p.Emp_OccupationName
            });
            return Json(q.ToList(), JsonRequestBehavior.AllowGet);

        }

        public ActionResult Add(Absence_Table absenceTable)
        {
            ViewBag.AbsenceType = new SelectList(absenceTypeRepository.GetAll().ToList(), "Absence_ID", "Absence_Type1");
            //ViewBag.StartDate = DateTime.Now.ToString("yyyy/MM/dd tt mm:ss");
            if (ModelState.IsValid)
            {
                absenceTable.EmployeeID = EmpID;
                absenceTable.Check_ID = 1;
                absenceTable.Dispose = false;
                absenceTable.StartDate = Convert.ToDateTime(string.Format("{0} {1}", absenceTable.StartDate.ToShortDateString(), Request.Form["StartDatetime"]));
                absenceTable.EndDate = Convert.ToDateTime(string.Format("{0} {1}", absenceTable.EndDate.ToShortDateString(), Request.Form["EndDatetime"]));
                HttpPostedFileBase file = Request.Files["Certificate_Doc"];
                if (file != null && file.ContentLength > 0)
                {
                    var imgSize = file.ContentLength;
                    byte[] imgByte = new Byte[imgSize];
                    file.InputStream.Read(imgByte, 0, imgSize);
                    absenceTable.Certificate_Doc = imgByte;
                }
                absenceTableRepository.Create(absenceTable);
                return RedirectToAction("Index");
            }
            
            return View();
        }

        public ActionResult Delete(int id)            
        {
            var delete = absenceTableRepository.GetByID(id);
            absenceTableRepository.Delete(delete);
            return RedirectToAction("Index");
        }

        [HttpGet]
        public ActionResult Edit(int id)
        {
            ViewBag.AbsenceType = new SelectList(absenceTypeRepository.GetAll().ToList(), "Absence_ID", "Absence_Type1");
            var editAbsence = absenceTableRepository.GetByID(id);
            
            return View(editAbsence);
        }

        [HttpPost]
        public ActionResult Edit(Absence_Table Absent)
        {            
            Absence_Table ab = absenceTableRepository.GetByID(Absent.Absence_No);
            ab.Absence_ID = Absent.Absence_ID;
            ab.StartDate = Absent.StartDate;
            ab.EndDate = Absent.EndDate;
            ab.Reason = Absent.Reason;
            HttpPostedFileBase file = Request.Files["CertificateDoc"];
            if(file.ContentLength>0)
            {
                var imgSize = file.ContentLength;
                byte[] imgByte = new Byte[imgSize];
                file.InputStream.Read(imgByte, 0, imgSize);
                ab.Certificate_Doc = imgByte;
            }
            absenceTableRepository.Update(ab);
            return RedirectToAction("Index");
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


    }
}