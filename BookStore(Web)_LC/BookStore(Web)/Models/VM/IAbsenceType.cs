using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BookStore_Web_.Models.VM
{
    interface IAbsenceTypeRepository
    {
        IEnumerable<Absence_Type> GetAll();
    }
}
