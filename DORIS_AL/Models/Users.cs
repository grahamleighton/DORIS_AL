//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated from a template.
//
//     Manual changes to this file may cause unexpected behavior in your application.
//     Manual changes to this file will be overwritten if the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace DORIS_AL.Models
{
    using System;
    
    public partial class Users
    {
        public long UserID { get; set; }
        public string UserName { get; set; }
        public string Password { get; set; }
        public string UID { get; set; }
        public Nullable<System.DateTime> CreatedOn { get; set; }
        public string FullName { get; set; }
        public Nullable<System.DateTime> LastPasswordChange { get; set; }
        public Nullable<int> AdminLevel { get; set; }
        public Nullable<int> UnsuccessfulLogonCount { get; set; }
        public Nullable<long> SecurityQuestionID { get; set; }
        public string SecurityAnswer { get; set; }
        public Nullable<System.DateTime> LastLoginTime { get; set; }
        public Nullable<bool> Deactivated { get; set; }
        public string Telephone { get; set; }
        public string AdminLevelName { get; set; }
        public string SupplierCode { get; set; }
        public string SupplierName { get; set; }
    }
}
