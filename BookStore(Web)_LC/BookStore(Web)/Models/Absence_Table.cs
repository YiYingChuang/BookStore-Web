//------------------------------------------------------------------------------
// <auto-generated>
//     這個程式碼是由範本產生。
//
//     對這個檔案進行手動變更可能導致您的應用程式產生未預期的行為。
//     如果重新產生程式碼，將會覆寫對這個檔案的手動變更。
// </auto-generated>
//------------------------------------------------------------------------------

namespace BookStore_Web_.Models
{
    using System;
    using System.Collections.Generic;
    
    public partial class Absence_Table
    {
        public int Absence_No { get; set; }
        public int EmployeeID { get; set; }
        public int Absence_ID { get; set; }
        public System.DateTime StartDate { get; set; }
        public System.DateTime EndDate { get; set; }
        public string Reason { get; set; }
        public byte[] Certificate_Doc { get; set; }
        public Nullable<int> Check_ID { get; set; }
        public bool Dispose { get; set; }
    
        public virtual Absence_Type Absence_Type { get; set; }
        public virtual ChechStatus ChechStatus { get; set; }
        public virtual Emp_Information Emp_Information { get; set; }
    }
}
