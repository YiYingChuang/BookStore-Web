using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Web;

namespace BookStore_Web_.Models.VM
{
    public class AbsenceTableRepository:IAbsenceTableRepository
    {
        BookStoreObject_Entities db = null;
        public AbsenceTableRepository() 
        {
            db = new BookStoreObject_Entities();
        }
        public Absence_Table GetById(int id)
        {
            return db.Absence_Table.Find(id);
        }

        public IEnumerable<Absence_Table> GetAll()
        {
            var absenceTable =db.Absence_Table.AsQueryable();
            return absenceTable;
        }

        public void Create(Absence_Table AbsenceTable)
        {
            db.Absence_Table.Add(AbsenceTable);
            db.SaveChanges();
        }

        public void Delete(Absence_Table AbsenceTable)
        {
            db.Absence_Table.Remove(AbsenceTable);
            db.SaveChanges();
        }

        public void Update(Absence_Table AbsenceTable)
        {
            db.Entry(AbsenceTable).State = EntityState.Modified;
            db.SaveChanges();
        }
    }
}