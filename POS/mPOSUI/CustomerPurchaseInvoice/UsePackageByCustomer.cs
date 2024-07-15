using POS.APP_Data;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace POS.mPOSUI.CustomerPurchaseInvoice {
    public partial class UsePackageByCustomer : Form {
        public string productname { get; set; }
        public int Package_Qty { get; set; }
        public int UsedQty { get; set; }
        public bool isEdit { get; set; }
        public string Doctor_Name { get; set; }
        public string Thrapist_Name { get; set; }
        public string Nurse_Aid { get; set; }
        public string PackageUsedHistoryID { get; set; }
        POSEntities entity = new POSEntities();
        public string packagePurchasedInvoiceId { get; set; }
        public string editpackagePurchasedInvoiceId { get; set; }
        private String FilePath;

        public UsePackageByCustomer() {
            InitializeComponent();
            }

        private void UsePackageByCustomer_Load(object sender, EventArgs e) {
            textDatabind();
            bindDoctor();
            bindAssistantNurse();
            bindTherapist();
            if (isEdit)
            {
                PackageUsedHistory editPackageUsedHistory = (from c in entity.PackageUsedHistories where c.PackageUsedHistoryId == PackageUsedHistoryID select c).FirstOrDefault<PackageUsedHistory>();
                txtQty.Enabled = false;
                editpackagePurchasedInvoiceId = editPackageUsedHistory.PackagePurchasedInvoiceId;
                txtproductname.Enabled = false;
                txtAvailableQty.Enabled = false;                
                txtproductname.Text = productname;
                txtAvailableQty.Text = ((editPackageUsedHistory.PackagePurchasedInvoice.packageFrequency * editPackageUsedHistory.PackagePurchasedInvoice.TransactionDetail.Qty)-editPackageUsedHistory.UseQty).ToString() ;
                txtQty.Text = editPackageUsedHistory.UseQty.ToString();
                txtRemark.Text = editPackageUsedHistory.Remark;
                cboDoctorName.Text = Doctor_Name;
                cboNurse.Text = Nurse_Aid;
                cboTherepist.Text =Thrapist_Name;
                if (editPackageUsedHistory.ProfilePath != null && editPackageUsedHistory.ProfilePath != "")
                {
                    this.txtPhotopath.Text = editPackageUsedHistory.ProfilePath.ToString();
                    string FileNmae = txtPhotopath.Text.Substring(9);
                    this.pbImage.ImageLocation = Application.StartupPath + "\\Images\\" + FileNmae;
                    this.pbImage.SizeMode = PictureBoxSizeMode.Zoom;
                }
                else
                {
                    pbImage.Image = null;
                }
            }
            }
        public void bindDoctor() {
                entity = new POSEntities();
                List<APP_Data.Customer> doctorList = new List<APP_Data.Customer>();
                APP_Data.Customer doctor = new APP_Data.Customer();
                doctor.Id = 0;
                doctor.Name = "Select";
                doctorList.Add(doctor);
                doctorList.AddRange(entity.Customers.Where(x => x.CustomerTypeId == 2).ToList());
                cboDoctorName.DataSource = doctorList;
                cboDoctorName.DisplayMember = "Name";
                cboDoctorName.ValueMember = "Id";
            }
        public void bindTherapist()
        {
            entity = new POSEntities();
            List<APP_Data.Customer> thereapistList = new List<APP_Data.Customer>();
            APP_Data.Customer therapsit = new APP_Data.Customer();
            therapsit.Id = 0;
            therapsit.Name = "Select";
            thereapistList.Add(therapsit);
            thereapistList.AddRange(entity.Customers.Where(x => x.CustomerTypeId == 3).ToList());
            cboTherepist.DataSource = thereapistList;
            cboTherepist.DisplayMember = "Name";
            cboTherepist.ValueMember = "Id";
        }
        public void bindAssistantNurse() {
                entity = new POSEntities();
                List<APP_Data.Customer> nurseList = new List<APP_Data.Customer>();
                APP_Data.Customer nurse = new Customer();
                nurse.Id = 0;
                nurse.Name = "Select";
                nurseList.Add(nurse);
                nurseList.AddRange(entity.Customers.Where(x => x.CustomerTypeId == 4).ToList());
                cboNurse.DataSource = nurseList;
                cboNurse.DisplayMember = "Name";
                cboNurse.ValueMember = "Id";           
            }
        private void textDatabind() {
            if (UsedQty == 0)
                txtAvailableQty.Text = Package_Qty.ToString();
            else txtAvailableQty.Text = (Package_Qty - UsedQty).ToString();
            
            txtproductname.Text = productname;
            }

        private void btnSubmit_Click_1(object sender, EventArgs e)
        {
            if (cboDoctorName.SelectedIndex < 0 || cboNurse.SelectedIndex < 0 || cboTherepist.SelectedIndex < 0)
            {
                MessageBox.Show("Select  Doctor or Nurse ", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }
            if (string.IsNullOrEmpty(txtQty.Text) || Convert.ToInt32(txtQty.Text) <= 0)
            {
                MessageBox.Show("Invalid data", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }
            if (Convert.ToInt32(txtQty.Text) > Convert.ToInt32(txtAvailableQty.Text))
            {
                MessageBox.Show("Not allow for input Quantity is greater than Abailable Qty", "Information", MessageBoxButtons.OK, MessageBoxIcon.Information);
                return;
            }

            DialogResult result = MessageBox.Show("Are you sure you to use it?", "Use Package", MessageBoxButtons.OKCancel, MessageBoxIcon.Information);
            if (result.Equals(DialogResult.OK))
            {
                if (isEdit)
                {
                    PackageUsedHistory editpackageUsedHistory = (from c in entity.PackageUsedHistories where c.PackageUsedHistoryId == PackageUsedHistoryID select c).FirstOrDefault<PackageUsedHistory>();
                   
                    editpackageUsedHistory.PackagePurchasedInvoiceId = editpackagePurchasedInvoiceId;
                    editpackageUsedHistory.UsedDate =editpackageUsedHistory.UsedDate;
                    editpackageUsedHistory.UseQty = Convert.ToInt32(txtQty.Text);
                    editpackageUsedHistory.IsDelete = false;
                    editpackageUsedHistory.UserId = MemberShip.UserId;
                    editpackageUsedHistory.CustomerIDAsDoctor = Convert.ToInt32(cboDoctorName.SelectedValue);
                    editpackageUsedHistory.CustomerIDAsAssistantNurse = Convert.ToInt32(cboNurse.SelectedValue);
                    editpackageUsedHistory.CustomerIDAsTherapist = Convert.ToInt32(cboTherepist.SelectedValue);
                    if (!(string.IsNullOrEmpty(this.txtPhotopath.Text.Trim())))
                    {
                        try
                        {
                            File.Copy(txtPhotopath.Text, Application.StartupPath + "\\Images\\" + FilePath);
                            editpackageUsedHistory.ProfilePath = "~\\Images\\" + FilePath;
                        }
                        catch
                        {
                            editpackageUsedHistory.ProfilePath = "~\\Images\\" + FilePath;
                        }
                    }
                    else
                    {

                        editpackageUsedHistory.ProfilePath = string.Empty;
                    }
                    editpackageUsedHistory.Remark = txtRemark.Text;                   
                    entity.Entry(editpackageUsedHistory).State = EntityState.Modified;
                    entity.SaveChanges();
                    MessageBox.Show("Update Successfully");
                    this.Close();       

                }
                else
                {
                    PackageUsedHistory packageUsedHistory = new PackageUsedHistory();
                    packageUsedHistory.PackageUsedHistoryId = System.Guid.NewGuid().ToString();
                    packageUsedHistory.PackagePurchasedInvoiceId = packagePurchasedInvoiceId;
                    packageUsedHistory.UsedDate = DateTime.Now;
                    packageUsedHistory.UseQty = Convert.ToInt32(txtQty.Text);
                    packageUsedHistory.IsDelete = false;
                    packageUsedHistory.UserId = MemberShip.UserId;
                    packageUsedHistory.CustomerIDAsDoctor = Convert.ToInt32(cboDoctorName.SelectedValue);
                    packageUsedHistory.CustomerIDAsAssistantNurse = Convert.ToInt32(cboNurse.SelectedValue);
                    packageUsedHistory.CustomerIDAsTherapist = Convert.ToInt32(cboTherepist.SelectedValue);
                    if (!(string.IsNullOrEmpty(this.txtPhotopath.Text.Trim())))
                    {
                        try
                        {
                            File.Copy(txtPhotopath.Text, Application.StartupPath + "\\Images\\" + FilePath);

                            packageUsedHistory.ProfilePath = "~\\Images\\" + FilePath;
                        }
                        catch
                        {
                            packageUsedHistory.ProfilePath = "~\\Images\\" + FilePath;
                        }
                    }
                    else
                    {
                        packageUsedHistory.ProfilePath = string.Empty;
                    }
                    packageUsedHistory.Remark = txtRemark.Text;
                    entity.PackageUsedHistories.Add(packageUsedHistory);

                    PackagePurchasedInvoice packagePurchaseinvoice = new PackagePurchasedInvoice();
                    packagePurchaseinvoice = entity.PackagePurchasedInvoices.Where(x => x.PackagePurchasedInvoiceId == packagePurchasedInvoiceId).SingleOrDefault();
                    if (packagePurchaseinvoice != null)
                    {
                        packagePurchaseinvoice.UseQty += (Convert.ToInt32(txtQty.Text));
                        int i = entity.SaveChanges();
                        if (i > 0) MessageBox.Show("Success !", "Information", MessageBoxButtons.OK, MessageBoxIcon.Information);
                        this.Close();
                        CustomerPurchaseInvoice CustomerPurchaseInvoiceForm = new CustomerPurchaseInvoice();
                        CustomerPurchaseInvoiceForm.Show();
                    }
                }
           
            }
        }

        private void btnCancel_Click_1(object sender, EventArgs e)
        {
            this.Close();
            CustomerPurchaseInvoice CustomerPurchaseInvoiceForm = new CustomerPurchaseInvoice();
            CustomerPurchaseInvoiceForm.Show();
        }

        private void btnBrowse_Click(object sender, EventArgs e)
        {
            OpenFileDialog dlg = new OpenFileDialog();
            dlg.Title = "Open Picture";
            dlg.Filter = "JPEGs (*.jpg;*.jpeg;*.jpe) |*.jpg;*.jpeg;*.jpe |GIFs (*.gif)|*.gif|PNGs (*.png)|*.png";

            try
            {
                if (dlg.ShowDialog() == DialogResult.OK)
                {
                    txtPhotopath.Text = dlg.FileName;
                    pbImage.SizeMode = PictureBoxSizeMode.StretchImage;
                    pbImage.ImageLocation = txtPhotopath.Text;
                    FilePath = System.IO.Path.GetFileName(dlg.FileName);

                }

            }
            catch (Exception ex)
            {
                //MessageBox.ShowMessage(Globalizer.MessageType.Warning, "You have to select Picture.\n Try again!!!");
                MessageBox.Show("You have to select Picture.\n Try again!!!");
                throw ex;
            }
        }
    }
    }
