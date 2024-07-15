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
    
    public partial class Shop
    {
        public Shop()
        {
            this.StockInHeaders = new HashSet<StockInHeader>();
            this.StockInHeaders1 = new HashSet<StockInHeader>();
            this.Transactions = new HashSet<Transaction>();
        }
    
        public int Id { get; set; }
        public string ShopName { get; set; }
        public string Address { get; set; }
        public string PhoneNumber { get; set; }
        public string OpeningHours { get; set; }
        public int CityId { get; set; }
        public string ShortCode { get; set; }
        public Nullable<bool> IsDefaultShop { get; set; }
    
        public virtual City City { get; set; }
        public virtual ICollection<StockInHeader> StockInHeaders { get; set; }
        public virtual ICollection<StockInHeader> StockInHeaders1 { get; set; }
        public virtual ICollection<Transaction> Transactions { get; set; }
    }
}
