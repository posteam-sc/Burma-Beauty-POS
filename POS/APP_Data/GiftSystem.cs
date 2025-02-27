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
    
    public partial class GiftSystem
    {
        public GiftSystem()
        {
            this.GiftSystemInTransactions = new HashSet<GiftSystemInTransaction>();
        }
    
        public int Id { get; set; }
        public string Type { get; set; }
        public string Name { get; set; }
        public Nullable<long> MustBuyCostFrom { get; set; }
        public Nullable<long> MustBuyCostTo { get; set; }
        public Nullable<long> MustIncludeProductId { get; set; }
        public Nullable<int> FilterBrandId { get; set; }
        public Nullable<int> FilterCategoryId { get; set; }
        public Nullable<int> FilterSubCategoryId { get; set; }
        public System.DateTime ValidFrom { get; set; }
        public System.DateTime ValidTo { get; set; }
        public bool UsePromotionQty { get; set; }
        public Nullable<int> PromotionQty { get; set; }
        public Nullable<long> GiftProductId { get; set; }
        public long PriceForGiftProduct { get; set; }
        public Nullable<long> GiftCashAmount { get; set; }
        public Nullable<int> DiscountPercentForTransaction { get; set; }
        public Nullable<bool> UseBrandFilter { get; set; }
        public Nullable<bool> UseCategoryFilter { get; set; }
        public Nullable<bool> UseSubCategoryFilter { get; set; }
        public Nullable<bool> UseProductFilter { get; set; }
        public Nullable<bool> IsActive { get; set; }
        public Nullable<bool> UseSizeFilter { get; set; }
        public Nullable<bool> UseQtyFilter { get; set; }
        public Nullable<int> FilterSize { get; set; }
        public Nullable<int> FilterQty { get; set; }
    
        public virtual Brand Brand { get; set; }
        public virtual Product Product { get; set; }
        public virtual Product Product1 { get; set; }
        public virtual ProductCategory ProductCategory { get; set; }
        public virtual ProductSubCategory ProductSubCategory { get; set; }
        public virtual ICollection<GiftSystemInTransaction> GiftSystemInTransactions { get; set; }
    }
}
