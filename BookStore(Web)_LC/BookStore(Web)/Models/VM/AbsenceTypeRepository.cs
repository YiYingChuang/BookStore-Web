using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace BookStore_Web_.Models.VM
{
    public class AbsenceTypeRepository:IAbsenceTypeRepository
    {
        BookStoreObject_Entities db = null;
        public AbsenceTypeRepository()
        {
            db = new BookStoreObject_Entities();
        }

        public IEnumerable<Absence_Type> GetAll()
        {
            return db.Absence_Type.ToList();
        }
    }
}