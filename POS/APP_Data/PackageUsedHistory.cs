//------------------------------------------------------------------------------
// <auto-generated>
//    This code was generated from a template.
//
//    Manual changes to this file may cause unexpected behavior in your application.
//    Manual changes to this file will be overwritten if the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace POS.APP_Data
{
    using System;
    using System.Collections.Generic;
    
    public partial class PackageUsedHistory
    {
        public string PackageUsedHistoryId { get; set; }
        public string PackagePurchasedInvoiceId { get; set; }
        public System.DateTime UsedDate { get; set; }
        public int UseQty { get; set; }
        public bool IsDelete { get; set; }
        public int UserId { get; set; }
        public Nullable<int> CustomerIDAsDoctor { get; set; }
        public Nullable<int> CustomerIDAsTherapist { get; set; }
        public Nullable<int> CustomerIDAsAssistantNurse { get; set; }
        public string Remark { get; set; }
        public string ProfilePath { get; set; }
    
        public virtual PackagePurchasedInvoice PackagePurchasedInvoice { get; set; }
        public virtual Customer Customer { get; set; }
    }
}
