using POS.APP_Data;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.Entity.Migrations;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace POS.mPOSUI.CustomerPurchaseInvoice {
    public partial class CustomerPurchaseInvoice : Form {
        private POSEntities entity = new POSEntities();
        public CustomerPurchaseInvoice() {
            InitializeComponent();
            }
        #region page Load
        private void CustomerPurchaseInvoice_Load(object sender, EventArgs e) {
     
            BindMember();
            BindCustomer();
            dtDate.Value = DateTime.Now;
            dtToDate.Value = DateTime.Now;
            //data binding for package Use grid view
            BindPackageUseGridView(0,0);
            //data binding for package Use History
            BindPackageUsedHistoryGridView(0, 0);
            BindPackageUsedHistoryDeleteLogGridView(0, 0);
            ChangegvpackageUsedHistoryCellColor();

        }
        #endregion

        #region grid view data bind
        private void BindPackageUsedHistoryGridView(int CustomerId, int MemberId)
        {
            if (CustomerId == 0 && MemberId == 0)
            {
                var data = (from puh in entity.PackageUsedHistories
                            where puh.IsDelete == false && puh.UsedDate >= dtDate.Value.Date && puh.UsedDate <= dtToDate.Value.Date
                            select new
                            {
                                PackageUsedHistoryId = puh.PackageUsedHistoryId,
                                Transaction_ID = puh.PackagePurchasedInvoice.TransactionDetail.TransactionId,
                                Customer_Name = puh.PackagePurchasedInvoice.Customer.Name,
                                Transaction_Date = puh.PackagePurchasedInvoice.InvoiceDate,
                                Used_Date = puh.UsedDate,
                                Product_Name = puh.PackagePurchasedInvoice.Product.Name,
                                Offset_Qty = puh.UseQty,
                                Doctor_Name = puh.CustomerIDAsDoctor,
                                Therapist_Name = puh.CustomerIDAsTherapist,
                                Nurse_Aid = puh.CustomerIDAsAssistantNurse,
                                Remark = puh.Remark,
                                Edit ="Edit",
                                Action = "Delete"
                            }).OrderByDescending(y => y.Transaction_Date).ToList();
                if (data.Count > 0)
                {
                    List<PackageUsedHistoryDataBind> packageUsedHistoryDataBindList = new List<PackageUsedHistoryDataBind>();
                    foreach (var item in data)
                    {
                        PackageUsedHistoryDataBind packageUsedHistoryDataBind = new PackageUsedHistoryDataBind();
                        packageUsedHistoryDataBind.Offset_Qty = item.Offset_Qty;
                        packageUsedHistoryDataBind.Transaction_ID = item.Transaction_ID;
                        packageUsedHistoryDataBind.UsedDate = item.Used_Date;
                        packageUsedHistoryDataBind.Customer_Name = item.Customer_Name;
                        packageUsedHistoryDataBind.Doctor_Name = (from c in entity.Customers where c.Id == item.Doctor_Name select c.Name).SingleOrDefault();
                        packageUsedHistoryDataBind.Thrapist_Name = (from c in entity.Customers where c.Id == item.Therapist_Name select c.Name).SingleOrDefault();
                        packageUsedHistoryDataBind.Nurse_Aid = (from c in entity.Customers where c.Id == item.Nurse_Aid select c.Name).SingleOrDefault();
                        packageUsedHistoryDataBind.ProductName = item.Product_Name;
                        packageUsedHistoryDataBind.PackageUsedHistoryId = item.PackageUsedHistoryId;
                        packageUsedHistoryDataBind.TransactionDate = item.Transaction_Date;
                        packageUsedHistoryDataBind.Remark = item.Remark;
                        packageUsedHistoryDataBind.Action = item.Action;
                        packageUsedHistoryDataBind.Edit = item.Edit;
                        packageUsedHistoryDataBindList.Add(packageUsedHistoryDataBind);                        
                    }                    
                    ChangegvpackageUsedHistoryCellColor();
                    gvpackageUsedHistory.DataSource = packageUsedHistoryDataBindList;
                    gvpackageUsedHistory.Columns["PackageUsedHistoryId"].Visible = false;
                }
              
                ChangegvpackageUsedHistoryCellColor();
            }
            else if (CustomerId > 0 && MemberId == 0)
            {
                var data = (from puh in entity.PackageUsedHistories
                            where puh.IsDelete == false && puh.PackagePurchasedInvoice.CustomerId == CustomerId
                            && puh.UsedDate >= dtDate.Value.Date && puh.UsedDate <= dtToDate.Value.Date
                            select new
                            {
                                PackageUsedHistoryId = puh.PackageUsedHistoryId,
                                Transaction_ID = puh.PackagePurchasedInvoice.TransactionDetail.TransactionId,
                                Customer_Name = puh.PackagePurchasedInvoice.Customer.Name,
                                Transaction_Date = puh.PackagePurchasedInvoice.InvoiceDate,
                                Used_Date = puh.UsedDate,
                                Product_Name = puh.PackagePurchasedInvoice.Product.Name,
                                Offset_Qty = puh.UseQty,
                                Doctor_Name = puh.CustomerIDAsDoctor,
                                Therapist_Name = puh.CustomerIDAsTherapist,
                                Nurse_Aid = puh.CustomerIDAsAssistantNurse,
                                Remark = puh.Remark,
                                Edit = "Edit",
                                Action = "Delete"
                            }).OrderByDescending(y => y.Transaction_Date).ToList();
                if (data.Count > 0)
                {
                    List<PackageUsedHistoryDataBind> packageUsedHistoryDataBindList = new List<PackageUsedHistoryDataBind>();
                    foreach (var item in data)
                    {
                        PackageUsedHistoryDataBind packageUsedHistoryDataBind = new PackageUsedHistoryDataBind();
                        packageUsedHistoryDataBind.Offset_Qty = item.Offset_Qty;
                        packageUsedHistoryDataBind.Transaction_ID = item.Transaction_ID;
                        packageUsedHistoryDataBind.UsedDate = item.Used_Date;
                        packageUsedHistoryDataBind.Customer_Name = item.Customer_Name;
                        packageUsedHistoryDataBind.Doctor_Name = (from c in entity.Customers where c.Id == item.Doctor_Name select c.Name).SingleOrDefault();
                        packageUsedHistoryDataBind.Thrapist_Name = (from c in entity.Customers where c.Id == item.Therapist_Name select c.Name).SingleOrDefault();
                        packageUsedHistoryDataBind.Nurse_Aid = (from c in entity.Customers where c.Id == item.Nurse_Aid select c.Name).SingleOrDefault();
                        packageUsedHistoryDataBind.ProductName = item.Product_Name;
                        packageUsedHistoryDataBind.PackageUsedHistoryId = item.PackageUsedHistoryId;
                        packageUsedHistoryDataBind.TransactionDate = item.Transaction_Date;
                        packageUsedHistoryDataBind.Remark = item.Remark;
                        packageUsedHistoryDataBind.Action = item.Action;
                        packageUsedHistoryDataBind.Edit = item.Edit;
                        packageUsedHistoryDataBindList.Add(packageUsedHistoryDataBind);
                        
                    }                   
                    gvpackageUsedHistory.DataSource = packageUsedHistoryDataBindList;
                    gvpackageUsedHistory.Columns["PackageUsedHistoryId"].Visible = false;
                    ChangegvpackageUsedHistoryCellColor();

                }
                
            }
            else if (MemberId > 0 && CustomerId == 0)
            {
                var data = (from puh in entity.PackageUsedHistories
                            where puh.IsDelete == false && puh.PackagePurchasedInvoice.Customer.MemberTypeID == MemberId
                            && puh.UsedDate >= dtDate.Value.Date && puh.UsedDate <= dtToDate.Value.Date
                            select new
                            {
                                PackageUsedHistoryId = puh.PackageUsedHistoryId,
                                Transaction_ID = puh.PackagePurchasedInvoice.TransactionDetail.TransactionId,
                                Customer_Name = puh.PackagePurchasedInvoice.Customer.Name,
                                Transaction_Date = puh.PackagePurchasedInvoice.InvoiceDate,
                                Used_Date = puh.UsedDate,
                                Product_Name = puh.PackagePurchasedInvoice.Product.Name,
                                Offset_Qty = puh.UseQty,
                                Doctor_Name = puh.CustomerIDAsDoctor,
                                Therapist_Name = puh.CustomerIDAsTherapist,
                                Nurse_Aid = puh.CustomerIDAsAssistantNurse,
                                Remark = puh.Remark,
                                Edit = "Edit",
                                Action = "Delete"
                            }).OrderByDescending(y => y.Transaction_Date).ToList();
                if (data.Count > 0)
                {
                    List<PackageUsedHistoryDataBind> packageUsedHistoryDataBindList = new List<PackageUsedHistoryDataBind>();
                    foreach (var item in data)
                    {
                        PackageUsedHistoryDataBind packageUsedHistoryDataBind = new PackageUsedHistoryDataBind();
                        packageUsedHistoryDataBind.Offset_Qty = item.Offset_Qty;
                        packageUsedHistoryDataBind.Transaction_ID = item.Transaction_ID;
                        packageUsedHistoryDataBind.UsedDate = item.Used_Date;
                        packageUsedHistoryDataBind.Customer_Name = item.Customer_Name;
                        packageUsedHistoryDataBind.Doctor_Name = (from c in entity.Customers where c.Id == item.Doctor_Name select c.Name).SingleOrDefault();
                        packageUsedHistoryDataBind.Thrapist_Name = (from c in entity.Customers where c.Id == item.Therapist_Name select c.Name).SingleOrDefault();
                        packageUsedHistoryDataBind.Nurse_Aid = (from c in entity.Customers where c.Id == item.Nurse_Aid select c.Name).SingleOrDefault();
                        packageUsedHistoryDataBind.ProductName = item.Product_Name;
                        packageUsedHistoryDataBind.PackageUsedHistoryId = item.PackageUsedHistoryId;
                        packageUsedHistoryDataBind.TransactionDate = item.Transaction_Date;
                        packageUsedHistoryDataBind.Remark = item.Remark;
                        packageUsedHistoryDataBind.Action = item.Action;
                        packageUsedHistoryDataBind.Edit = item.Edit;
                        packageUsedHistoryDataBindList.Add(packageUsedHistoryDataBind);                       
                    }
                   
                    gvpackageUsedHistory.DataSource = packageUsedHistoryDataBindList;
                    gvpackageUsedHistory.Columns["PackageUsedHistoryId"].Visible = false;
                    ChangegvpackageUsedHistoryCellColor();
                }
             

            }
            else if (MemberId > 0 && CustomerId > 0)
            {
                var data = (from puh in entity.PackageUsedHistories
                            where puh.IsDelete == false && puh.PackagePurchasedInvoice.Customer.MemberTypeID == MemberId && puh.PackagePurchasedInvoice.CustomerId == CustomerId
                            && puh.UsedDate >= dtDate.Value.Date && puh.UsedDate <= dtToDate.Value.Date
                            select new
                            {
                                PackageUsedHistoryId = puh.PackageUsedHistoryId,
                                Transaction_ID = puh.PackagePurchasedInvoice.TransactionDetail.TransactionId,
                                Customer_Name = puh.PackagePurchasedInvoice.Customer.Name,
                                Transaction_Date = puh.PackagePurchasedInvoice.InvoiceDate,
                                Used_Date = puh.UsedDate,
                                Product_Name = puh.PackagePurchasedInvoice.Product.Name,
                                Offset_Qty = puh.UseQty,
                                Doctor_Name = puh.CustomerIDAsDoctor,
                                Therapist_Name = puh.CustomerIDAsTherapist,
                                Nurse_Aid = puh.CustomerIDAsAssistantNurse,
                                Remark = puh.Remark,
                                Edit = "Edit",
                                Action = "Delete"
                            }).OrderByDescending(y => y.Transaction_Date).ToList();
                if (data.Count > 0)
                {
                    List<PackageUsedHistoryDataBind> packageUsedHistoryDataBindList = new List<PackageUsedHistoryDataBind>();
                    foreach (var item in data)
                    {
                        PackageUsedHistoryDataBind packageUsedHistoryDataBind = new PackageUsedHistoryDataBind();
                        packageUsedHistoryDataBind.Offset_Qty = item.Offset_Qty;
                        packageUsedHistoryDataBind.Transaction_ID = item.Transaction_ID;
                        packageUsedHistoryDataBind.UsedDate = item.Used_Date;
                        packageUsedHistoryDataBind.Customer_Name = item.Customer_Name;
                        packageUsedHistoryDataBind.Doctor_Name = (from c in entity.Customers where c.Id == item.Doctor_Name select c.Name).SingleOrDefault();
                        packageUsedHistoryDataBind.Thrapist_Name = (from c in entity.Customers where c.Id == item.Therapist_Name select c.Name).SingleOrDefault();
                        packageUsedHistoryDataBind.Nurse_Aid = (from c in entity.Customers where c.Id == item.Nurse_Aid select c.Name).SingleOrDefault();
                        packageUsedHistoryDataBind.ProductName = item.Product_Name;
                        packageUsedHistoryDataBind.PackageUsedHistoryId = item.PackageUsedHistoryId;
                        packageUsedHistoryDataBind.TransactionDate = item.Transaction_Date;
                        packageUsedHistoryDataBind.Remark = item.Remark;
                        packageUsedHistoryDataBind.Action = item.Action;
                        packageUsedHistoryDataBind.Edit = item.Edit;
                        packageUsedHistoryDataBindList.Add(packageUsedHistoryDataBind);
                    }                    
                    gvpackageUsedHistory.DataSource = packageUsedHistoryDataBindList;
                    gvpackageUsedHistory.Columns[0].Visible = false;
                    ChangegvpackageUsedHistoryCellColor();
                }
                ChangegvpackageUsedHistoryCellColor();
            }
        }
        //DeleteLog for customerPacakgeSale
        private void BindPackageUsedHistoryDeleteLogGridView(int CustomerId, int MemberId)
        {
            
            if (CustomerId == 0 && MemberId == 0)
            {
                var data = (from puh in entity.PackageUsedHistories
                            where puh.IsDelete == true && puh.UsedDate >=dtDate.Value.Date && puh.UsedDate<=dtToDate.Value.Date
                            select new
                            {
                                PackageUsedHistoryId = puh.PackageUsedHistoryId,
                                Transaction_ID = puh.PackagePurchasedInvoice.TransactionDetail.TransactionId,
                                Customer_Name = puh.PackagePurchasedInvoice.Customer.Name,
                                Transaction_Date = puh.PackagePurchasedInvoice.InvoiceDate,
                                Used_Date = puh.UsedDate,
                                Product_Name = puh.PackagePurchasedInvoice.Product.Name,
                                Offset_Qty = puh.UseQty,                        
                            }).OrderByDescending(y => y.Transaction_Date).ToList();
                dgvDeleteLogList.DataSource = data;
            }
            else if (CustomerId > 0 && MemberId == 0)
            {
                var data = (from puh in entity.PackageUsedHistories
                            where puh.IsDelete == true && puh.PackagePurchasedInvoice.CustomerId == CustomerId
                            && puh.UsedDate >= dtDate.Value.Date && puh.UsedDate <= dtToDate.Value.Date
                            select new
                            {
                                PackageUsedHistoryId = puh.PackageUsedHistoryId,
                                Transaction_ID = puh.PackagePurchasedInvoice.TransactionDetail.TransactionId,
                                Customer_Name = puh.PackagePurchasedInvoice.Customer.Name,
                                Transaction_Date = puh.PackagePurchasedInvoice.InvoiceDate,
                                Used_Date = puh.UsedDate,
                                Product_Name = puh.PackagePurchasedInvoice.Product.Name,
                                Offset_Qty = puh.UseQty,
                                  }).OrderByDescending(y => y.Transaction_Date).ToList();
                dgvDeleteLogList.DataSource = data;
            }
            else if (MemberId > 0 && CustomerId == 0)
            {
                var data = (from puh in entity.PackageUsedHistories
                            where puh.IsDelete == true && puh.PackagePurchasedInvoice.Customer.MemberTypeID == MemberId
                            && puh.UsedDate >= dtDate.Value.Date && puh.UsedDate <= dtToDate.Value.Date
                            select new
                            {
                                PackageUsedHistoryId = puh.PackageUsedHistoryId,
                                Transaction_ID = puh.PackagePurchasedInvoice.TransactionDetail.TransactionId,
                                Customer_Name = puh.PackagePurchasedInvoice.Customer.Name,
                                Transaction_Date = puh.PackagePurchasedInvoice.InvoiceDate,
                                Used_Date = puh.UsedDate,
                                Produc_tName = puh.PackagePurchasedInvoice.Product.Name,
                                Offset_Qty = puh.UseQty,
                              }).OrderByDescending(y => y.Transaction_Date).ToList();
                dgvDeleteLogList.DataSource = data;
            }
            else if (MemberId > 0 && CustomerId > 0)
            {
                var data = (from puh in entity.PackageUsedHistories
                            where puh.IsDelete == true && puh.PackagePurchasedInvoice.Customer.MemberTypeID == MemberId && puh.PackagePurchasedInvoice.CustomerId == CustomerId
                            && puh.UsedDate >= dtDate.Value.Date && puh.UsedDate <= dtToDate.Value.Date
                            select new
                            {
                                PackageUsedHistoryId = puh.PackageUsedHistoryId,
                                Transaction_ID = puh.PackagePurchasedInvoice.TransactionDetail.TransactionId,
                                Customer_Name = puh.PackagePurchasedInvoice.Customer.Name,
                                Transaction_Date = puh.PackagePurchasedInvoice.InvoiceDate,
                                Used_Date = puh.UsedDate,
                                Product_Name = puh.PackagePurchasedInvoice.Product.Name,
                                Offset_Qty = puh.UseQty,
                              }).OrderByDescending(y => y.Transaction_Date).ToList();
                dgvDeleteLogList.DataSource = data;
            }
            dgvDeleteLogList.Columns["PackageUsedHistoryId"].Visible = false;
               }

        private void ChangegvpackageUsedHistoryCellColor() {
            int rowscount = gvpackageUsedHistory.Rows.Count;
            for (int i = 0; i < rowscount; i++) {
                // gvpackageUsedHistory.Rows[i].Cells[6].Style.BackColor = Color.Green;
                //gvpackageUsedHistory.Rows[i].Cells[6].Style.ForeColor = Color.Red;
                this.gvpackageUsedHistory.Rows[i].Cells[11].Style.ForeColor = Color.Blue;             
                gvpackageUsedHistory.Rows[i].Cells[11].Style.Font = new Font(gvpackageUsedHistory.DefaultCellStyle.Font, FontStyle.Underline);
                this.gvpackageUsedHistory.Rows[i].Cells[12].Style.ForeColor = Color.Blue;
                gvpackageUsedHistory.Rows[i].Cells[12].Style.Font = new Font(gvpackageUsedHistory.DefaultCellStyle.Font, FontStyle.Underline);
            }
            }

        private void BindPackageUseGridView(int CustomerId,int MemberId) {
            if (CustomerId==0 && MemberId==0) {
                var data = (from ppi in entity.PackagePurchasedInvoices where ppi.Product.IsPackage==true 
                            && ppi.InvoiceDate>=dtDate.Value.Date && ppi.InvoiceDate<=dtToDate.Value.Date
                            && ppi.TransactionDetail.IsDeleted==false 
                                && (ppi.packageFrequency * ppi.TransactionDetail.Qty) - ppi.UseQty != 0
                            select new {
                                packagePurchasedInvoiceId = ppi.PackagePurchasedInvoiceId,
                                TransactionId = ppi.TransactionDetail.Transaction.Id,
                                Transaction_Date = ppi.InvoiceDate,
                                Customer_Name = ppi.Customer.Name,
                                Product_Name = ppi.Product.Name,
                                Procedure_Qty = ppi.packageFrequency * ppi.TransactionDetail.Qty,
                                Offset_Qty = ppi.UseQty,
                                Available_Qty = (ppi.packageFrequency * ppi.TransactionDetail.Qty) - ppi.UseQty,
                                Action= "Offset",
                                Offset_History= "View",
                                TransactionDetailId = ppi.TransactionDetailId
                            }).OrderByDescending(y => y.Transaction_Date).ToList();
                gvPurchaseInoivce.DataSource = data;
                }
            else if (CustomerId > 0 && MemberId == 0)
            {
                var data = (from ppi in entity.PackagePurchasedInvoices
                            where ppi.IsDelete == false && ppi.Product.IsPackage == true
                            && (ppi.CustomerId == CustomerId || ppi.InvoiceDate >= dtDate.Value.Date && ppi.InvoiceDate <= dtToDate.Value.Date)
                            && ppi.TransactionDetail.IsDeleted == false && (ppi.packageFrequency * ppi.TransactionDetail.Qty) - ppi.UseQty != 0
                            select new {
                                packagePurchasedInvoiceId = ppi.PackagePurchasedInvoiceId,
                                TransactionId = ppi.TransactionDetail.Transaction.Id,
                                Transaction_Date = ppi.InvoiceDate,
                                Customer_Name = ppi.Customer.Name,
                                Product_Name = ppi.Product.Name,
                                Procedure_Qty = ppi.packageFrequency * ppi.TransactionDetail.Qty,
                                Offset_Qty = ppi.UseQty,
                                Available_Qty = (ppi.packageFrequency * ppi.TransactionDetail.Qty) - ppi.UseQty,
                                Action = "Offset",
                                Offset_History = "View",
                                TransactionDetailId = ppi.TransactionDetailId
                            }).OrderByDescending(y => y.Transaction_Date).ToList();
                gvPurchaseInoivce.DataSource = data;
                }
            else if (MemberId > 0 && CustomerId == 0)
            {
                var data = (from ppi in entity.PackagePurchasedInvoices
                            where  ppi.IsDelete == false && ppi.Product.IsPackage == true
                           && ppi.Customer.MemberTypeID == MemberId && ppi.InvoiceDate >= dtDate.Value.Date && ppi.InvoiceDate <= dtToDate.Value.Date && ppi.TransactionDetail.IsDeleted == false && (ppi.packageFrequency * ppi.TransactionDetail.Qty) - ppi.UseQty != 0
                            select new {
                                packagePurchasedInvoiceId = ppi.PackagePurchasedInvoiceId,
                                TransactionId = ppi.TransactionDetail.Transaction.Id,
                                Transaction_Date = ppi.InvoiceDate,
                                Customer_Name = ppi.Customer.Name,
                                Product_Name = ppi.Product.Name,
                                Procedure_Qty = ppi.packageFrequency * ppi.TransactionDetail.Qty,
                                Offset_Qty = ppi.UseQty,
                                Available_Qty = (ppi.packageFrequency * ppi.TransactionDetail.Qty) - ppi.UseQty,
                                Action = "Offset",
                                Offset_History = "View",
                                TransactionDetailId = ppi.TransactionDetailId
                            }).OrderByDescending(y => y.Transaction_Date).ToList();
                gvPurchaseInoivce.DataSource = data;
                }
            else if (MemberId > 0 || CustomerId > 0)
            {
                var data = (from ppi in entity.PackagePurchasedInvoices
                            where ppi.IsDelete == false &&  ppi.Product.IsPackage == true
                            && (ppi.Customer.MemberTypeID == MemberId || ppi.CustomerId == CustomerId 
                            || ppi.InvoiceDate >= dtDate.Value.Date && ppi.InvoiceDate <= dtToDate.Value.Date)
                            && ppi.TransactionDetail.IsDeleted == false && (ppi.packageFrequency * ppi.TransactionDetail.Qty) - ppi.UseQty !=0
                            select new {
                                packagePurchasedInvoiceId = ppi.PackagePurchasedInvoiceId,
                                TransactionId = ppi.TransactionDetail.Transaction.Id,
                                Transaction_Date = ppi.InvoiceDate,
                                Customer_Name = ppi.Customer.Name,
                                Product_Name = ppi.Product.Name,
                                Procedure_Qty = ppi.packageFrequency * ppi.TransactionDetail.Qty,
                                Offset_Qty = ppi.UseQty,
                                Available_Qty = (ppi.packageFrequency * ppi.TransactionDetail.Qty) - ppi.UseQty,
                                Action = "Offset",
                                Offset_History = "View",
                                TransactionDetailId=ppi.TransactionDetailId                                
                            }).OrderByDescending(y => y.Transaction_Date).ToList();
                gvPurchaseInoivce.DataSource = data;
                }
            gvPurchaseInoivce.Columns["packagePurchasedInvoiceId"].Visible = false;
            gvPurchaseInoivce.Columns["TransactionDetailId"].Visible = false;             
            ChangegvPurchaseInoivceCellColor();
            }

        #endregion

        private void ChangegvPurchaseInoivceCellColor() {
            int rowscount = gvPurchaseInoivce.Rows.Count;
            for (int i = 0; i < rowscount; i++) {
                //gvPurchaseInoivce.Rows[i].Cells[7].Style.BackColor = Color.Green;
                //gvPurchaseInoivce.Rows[i].Cells[7].Style.ForeColor =Color.Red;
                this.gvPurchaseInoivce.Rows[i].Cells[8].Style.ForeColor = Color.Blue;
                this.gvPurchaseInoivce.Rows[i].Cells[9].Style.ForeColor = Color.Blue;
                gvPurchaseInoivce.Rows[i].Cells[8].Style.Font= new Font(gvPurchaseInoivce.DefaultCellStyle.Font, FontStyle.Underline);
                    gvPurchaseInoivce.Rows[i].Cells[9].Style.Font = new Font(gvPurchaseInoivce.DefaultCellStyle.Font, FontStyle.Underline);
                }
            }

        #region bind customer && Member id
        private void BindCustomer() {
            //Add Customer List with default option
            List<APP_Data.Customer> customerList = new List<APP_Data.Customer>();
            APP_Data.Customer customer = new APP_Data.Customer();
            customer.Id = 0;
            customer.Name = "All";
            customerList.Add(customer);
            customerList.AddRange(from c in entity.Customers where c.CustomerTypeId ==1 select c);
            cboCustomerId.DataSource = customerList;
            cboCustomerId.DisplayMember = "Name";
            cboCustomerId.ValueMember = "Id";
            }
        private void BindMember() {
            List<APP_Data.MemberType> mTypeList = new List<APP_Data.MemberType>();
            APP_Data.MemberType mType = new APP_Data.MemberType();
            mType.Id = 0;
            mType.Name = "All";
            mTypeList.Add(mType);
            mTypeList.AddRange(entity.MemberTypes.ToList());
            cbomemberId.DataSource = mTypeList;
            cbomemberId.DisplayMember = "Name";
            cbomemberId.ValueMember = "Id";
            }

       
#endregion


        private void gvPurchaseInoivce_CellClick(object sender, DataGridViewCellEventArgs e) {
            int index = e.RowIndex;// get the Row Index
            if (index!=-1) {
                DataGridViewRow selectedRow = this.gvPurchaseInoivce.Rows[index];
                if (e.ColumnIndex == 8) {//click the use package cell         
                    UsePackageByCustomer UsePackageByCustomer = new UsePackageByCustomer();
                    UsePackageByCustomer.packagePurchasedInvoiceId = selectedRow.Cells[0].Value.ToString();//get the  packagePurchasedInvoiceId
                    UsePackageByCustomer.productname = selectedRow.Cells[4].Value.ToString();
                    if (string.IsNullOrEmpty(selectedRow.Cells[5].Value.ToString())) UsePackageByCustomer.Package_Qty = 0;
                    else UsePackageByCustomer.Package_Qty = Convert.ToInt32(selectedRow.Cells[5].Value.ToString());
                    UsePackageByCustomer.UsedQty = Convert.ToInt32(selectedRow.Cells[6].Value.ToString());
                    UsePackageByCustomer.Show();
                    this.Close();
                    }
                if (e.ColumnIndex == 9) {
                    List<PackageUsedHistoryDataBind> PackageUsedHistoryList = new List<PackageUsedHistoryDataBind>();
                    long transactiondDetailId =Convert.ToInt64(selectedRow.Cells[10].Value);//get the  transactiondDetailId
                    var PackagePurchaseInvoiceData = (entity.PackagePurchasedInvoices.Where(x=>x.TransactionDetailId==transactiondDetailId && x.IsDelete==false)).ToList();
                        foreach(var i in PackagePurchaseInvoiceData) {
                                var data = (from puh in entity.PackageUsedHistories
                                            where puh.IsDelete == false && puh.PackagePurchasedInvoiceId ==i.PackagePurchasedInvoiceId
                                            select new PackageUsedHistoryDataBind {
                                                PackageUsedHistoryId = puh.PackageUsedHistoryId,
                                                Transaction_ID = puh.PackagePurchasedInvoice.TransactionDetail.TransactionId,
                                                Customer_Name = puh.PackagePurchasedInvoice.Customer.Name,
                                                TransactionDate = puh.PackagePurchasedInvoice.InvoiceDate,
                                                UsedDate = puh.UsedDate,
                                                ProductName = puh.PackagePurchasedInvoice.Product.Name,
                                                Offset_Qty = puh.UseQty,
                                                Action = "Delete"
                                            }).OrderByDescending(y => y.TransactionDate)
                                                .ToList();
                        foreach(var d in data) {
                            PackageUsedHistoryDataBind packageUsedHistoryDataBind = new PackageUsedHistoryDataBind();
                            packageUsedHistoryDataBind.PackageUsedHistoryId = d.PackageUsedHistoryId;
                            packageUsedHistoryDataBind.Transaction_ID = d.Transaction_ID;
                            packageUsedHistoryDataBind.Customer_Name = d.Customer_Name;
                            packageUsedHistoryDataBind.ProductName = d.ProductName;
                            packageUsedHistoryDataBind.Offset_Qty = d.Offset_Qty;
                            //packageUsedHistoryDataBind.Offset_Qty = d.Offset_Qty;
                            packageUsedHistoryDataBind.UsedDate = d.UsedDate;
                            packageUsedHistoryDataBind.TransactionDate = d.TransactionDate;
                            packageUsedHistoryDataBind.Action = d.Action;
                            PackageUsedHistoryList.Add(packageUsedHistoryDataBind);
                            }//adding the data to the list
                        }//end of package invoice detail.
                    gvpackageUsedHistory.DataSource = PackageUsedHistoryList;
                    gvpackageUsedHistory.Columns["PackageUsedHistoryId"].Visible = false;
                    ChangegvpackageUsedHistoryCellColor();
                    usepackagetab.SelectTab("tabhistory");//go to Use package History Tab automically
                    }          
                }
            }
        class PackageUsedHistoryDataBind {
            public string PackageUsedHistoryId { get; set; }
            public string Transaction_ID { get; set; }
            public string Customer_Name { get; set; }
            public DateTime TransactionDate { get; set; }
            public DateTime UsedDate { get; set; }
            public string ProductName { get; set; }
            public int Offset_Qty { get; set; }            
            public string Doctor_Name { get; set; }
            public string Thrapist_Name { get; set; }
            public string Nurse_Aid { get; set; }
            public string Remark { get; set; }            
            public string Edit { get; set; }
            public string Action { get; set; }

        }

        private void gvpackageUsedHistory_CellClick(object sender, DataGridViewCellEventArgs e) {
            if (e.ColumnIndex == 12) {//Delete link button
                DialogResult result = MessageBox.Show("Are you sure you to delete?", "Delete", MessageBoxButtons.OKCancel, MessageBoxIcon.Information);
                if (result.Equals(DialogResult.OK)) {
                    int index = e.RowIndex;// get the Row Index
                    DataGridViewRow selectedRow = this.gvpackageUsedHistory.Rows[index];
                    string PackageUsedHistoryId = selectedRow.Cells[0].Value.ToString();
                    //updating the package used hisotry 
                    PackageUsedHistory packageUsedHistory = entity.PackageUsedHistories.Where(x => x.PackageUsedHistoryId == PackageUsedHistoryId && x.IsDelete == false).SingleOrDefault();
                    if (packageUsedHistory != null) {
                        packageUsedHistory.IsDelete = true;
                        entity.PackageUsedHistories.AddOrUpdate(packageUsedHistory);
                        //updating package purchase invoice
                        PackagePurchasedInvoice packagePurchasedInvoice = entity.PackagePurchasedInvoices.Where(x => x.PackagePurchasedInvoiceId == packageUsedHistory.PackagePurchasedInvoiceId && x.IsDelete == false).SingleOrDefault();
                        if (packagePurchasedInvoice != null) {
                            packagePurchasedInvoice.UseQty -= packageUsedHistory.UseQty;
                            entity.PackagePurchasedInvoices.AddOrUpdate(packagePurchasedInvoice);
                            }                      
                        entity.SaveChanges(); 
                        }
                    this.BindPackageUsedHistoryGridView(0, 0);
                    this.BindPackageUseGridView(0, 0);
                    this.BindPackageUsedHistoryDeleteLogGridView(0, 0);
                    }//end of DialogResult.OK
               
                }else if(e.ColumnIndex == 11)
            {
                UsePackageByCustomer usePackageByCustomer = new UsePackageByCustomer();
                usePackageByCustomer.PackageUsedHistoryID = Convert.ToString(gvpackageUsedHistory.Rows[e.RowIndex].Cells[0].Value);
                usePackageByCustomer.productname = Convert.ToString(gvpackageUsedHistory.Rows[e.RowIndex].Cells[5].Value);
                usePackageByCustomer.UsedQty = Convert.ToInt32(gvpackageUsedHistory.Rows[e.RowIndex].Cells[6].Value);
                usePackageByCustomer.Doctor_Name = Convert.ToString(gvpackageUsedHistory.Rows[e.RowIndex].Cells[7].Value);
                usePackageByCustomer.Thrapist_Name = Convert.ToString(gvpackageUsedHistory.Rows[e.RowIndex].Cells[8].Value);
                usePackageByCustomer.Nurse_Aid = Convert.ToString(gvpackageUsedHistory.Rows[e.RowIndex].Cells[9].Value);
                usePackageByCustomer.isEdit = true;
                usePackageByCustomer.Show();
            }
            
            }

        private void btnSearch_Click_1(object sender, EventArgs e)
        {
            int memberId = 0;
            int customerId = 0;                
            if (cboCustomerId.SelectedIndex > 0) customerId = (int)cboCustomerId.SelectedValue;
            if (cbomemberId.SelectedIndex > 0) memberId = (int)cbomemberId.SelectedValue;
             BindPackageUseGridView(customerId, memberId);
            BindPackageUsedHistoryGridView(customerId, memberId);
            BindPackageUsedHistoryDeleteLogGridView(customerId, memberId);

        }

        private void btnClearSearch_Click(object sender, EventArgs e)
        {
            cboCustomerId.SelectedIndex = cbomemberId.SelectedIndex = 0;
            dtDate.Value= DateTime.Now;
            dtToDate.Value = DateTime.Now;
            BindPackageUseGridView(0, 0);
            BindPackageUsedHistoryGridView(0,0);
            BindPackageUsedHistoryDeleteLogGridView(0, 0);          
        }

        private void usepackagetab_SelectedIndexChanged(object sender, EventArgs e)
        {
            ChangegvPurchaseInoivceCellColor();
        }

        private void usepackagetab_TabIndexChanged(object sender, EventArgs e)
        {
            ChangegvPurchaseInoivceCellColor();
        }
    }
    }
