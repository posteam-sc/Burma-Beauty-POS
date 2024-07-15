using POS.APP_Data;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace POS.mPOSUI
{
    public partial class CustomerBeforeandAfter_Photo : Form
    {
        private POSEntities entity = new POSEntities();
        public string PackageHsitoryID { get; set; }
        public string customerName { get; set; }
        public string PackageName { get; set; }
        public string customerPhoto { get; set; }
        public string PackagePurchaseInvoiceID { get; set; }
        public CustomerBeforeandAfter_Photo()
        {
            InitializeComponent();
        }

        private void CustomerBeforeandAfter_Photo_Load(object sender, EventArgs e)
        {
            lblPatientName.Text = customerName;
            lblPackageName.Text = PackageName;
            if (customerPhoto != null && customerPhoto != "")
            {
                string FileNmae = customerPhoto.Substring(9).ToString();
                this.pbCustomerPhoto.ImageLocation = Application.StartupPath + "\\Images\\" + FileNmae;
                this.pbCustomerPhoto.SizeMode = PictureBoxSizeMode.Zoom;
            }
            else
            {
                pbCustomerPhoto.Image = null;
            }
            BindCustomerPhoto();
        }
        public void BindCustomerPhoto()
        {
            var PackageUseHistoryData = (from p in entity.PackageUsedHistories where p.PackagePurchasedInvoiceId == PackagePurchaseInvoiceID && p.IsDelete == false orderby p.UsedDate select p).ToList();
            if (PackageUseHistoryData != null)
            {
               
                foreach (var image in PackageUseHistoryData)
                {
                    Size size = new Size(100, 100);
                    PictureBox pb = new PictureBox();
                    Label dateLable = new Label();
                    Label remarkLabel = new Label();
                    if (image.ProfilePath != null && image.ProfilePath != "")
                    {
                        string FileNmae = image.ProfilePath.Substring(9).ToString();
                        pb.ImageLocation = Application.StartupPath + "\\Images\\" + FileNmae;
                        pb.SizeMode = PictureBoxSizeMode.Zoom;
                        pb.Size = size;
                        dateLable.Text = image.UsedDate.ToString();
                        remarkLabel.Text = image.Remark;
                       
                    }else 
                    {
                        pb.Image = null;
                        pb.SizeMode = PictureBoxSizeMode.Zoom;
                        pb.Size = size;
                        dateLable.Text = image.UsedDate.ToString();
                        remarkLabel.Text = image.Remark;
                    }


                    flowLayoutPanel1.Controls.Add(pb);
                    flowLayoutPanel1.Controls.Add(dateLable);
                    flowLayoutPanel1.Controls.Add(remarkLabel);

                }
              
            
        
            }

        }
    }
}
