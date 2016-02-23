using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BookStore_Web_.Models.VM
{
    interface IAbsenceTableRepository
    {
        Absence_Table GetById(int id);
        IEnumerable<Absence_Table> GetAll();
        void Create(Absence_Table AbsenceTable);
        void Delete(Absence_Table AbsenceTable);
        void Update(Absence_Table AbsenceTable);
    }
}
