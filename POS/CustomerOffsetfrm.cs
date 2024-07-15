using POS.APP_Data;
using POS.mPOSUI;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace POS
{
    public partial class CustomerOffsetfrm : Form
    {
        public int customerID;
        POSEntities entities = new POSEntities();
private void CustomerOffsetfrm_Load(object sender, EventArgs e)
        {
            bindCustomerPackageDetail();
        }

        public CustomerOffsetfrm()
        {
            InitializeComponent();
        }
        private void bindCustomerPackageDetail()
        {
            var data = (from c in entities.PackageUsedHistories
                        where c.PackagePurchasedInvoice.CustomerId == customerID && c.IsDelete == false
                        select new
                        {
                            customerID = c.PackagePurchasedInvoice.CustomerId,
                            PackageHistoryID=c.PackageUsedHistoryId,
                            customerPhoto=c.PackagePurchasedInvoice.Customer.ProfilePath,
                            PackagePurchaseInvoiceID=c.PackagePurchasedInvoice.PackagePurchasedInvoiceId,
                            Customer_Name=c.PackagePurchasedInvoice.Customer.Name,
                            Transaction_ID = c.PackagePurchasedInvoice.TransactionDetail.TransactionId,
                            Transation_Date = c.PackagePurchasedInvoice.InvoiceDate,
                            Package_Name = c.PackagePurchasedInvoice.Product.Name,
                            Procedure_Qty = c.PackagePurchasedInvoice.packageFrequency * c.PackagePurchasedInvoice.TransactionDetail.Qty,
                            Available_Qty = (c.PackagePurchasedInvoice.packageFrequency * c.PackagePurchasedInvoice.TransactionDetail.Qty) - c.UseQty,
                            Offset_Qty = c.UseQty,
                            Action ="View Treatement Photo"
                        }).OrderByDescending(y => y.Transation_Date).ToList();
            dgvPatientPacakgeDetail.DataSource = data;
            dgvPatientPacakgeDetail.Columns["customerID"].Visible = false;
            dgvPatientPacakgeDetail.Columns["PackageHistoryID"].Visible = false;
            dgvPatientPacakgeDetail.Columns["customerPhoto"].Visible = false;
            dgvPatientPacakgeDetail.Columns["PackagePurchaseInvoiceID"].Visible = false;
        }

        private void dgvPatientPacakgeDetail_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.ColumnIndex == 11)
            {
                CustomerBeforeandAfter_Photo customerPhotoFrm = new CustomerBeforeandAfter_Photo();
                customerPhotoFrm.PackageHsitoryID =Convert.ToString( dgvPatientPacakgeDetail.Rows[e.RowIndex].Cells[1].Value);
                customerPhotoFrm.customerName= Convert.ToString(dgvPatientPacakgeDetail.Rows[e.RowIndex].Cells[4].Value);
                customerPhotoFrm.PackageName= Convert.ToString(dgvPatientPacakgeDetail.Rows[e.RowIndex].Cells[7].Value);
                customerPhotoFrm.customerPhoto= Convert.ToString(dgvPatientPacakgeDetail.Rows[e.RowIndex].Cells[2].Value);
                customerPhotoFrm.PackagePurchaseInvoiceID = Convert.ToString(dgvPatientPacakgeDetail.Rows[e.RowIndex].Cells[3].Value);
                customerPhotoFrm.Show();
            }
        }
    }
}
