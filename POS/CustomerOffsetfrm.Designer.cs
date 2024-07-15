namespace POS
{
    partial class CustomerOffsetfrm
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.dgvPatientPacakgeDetail = new System.Windows.Forms.DataGridView();
            ((System.ComponentModel.ISupportInitialize)(this.dgvPatientPacakgeDetail)).BeginInit();
            this.SuspendLayout();
            // 
            // dgvPatientPacakgeDetail
            // 
            this.dgvPatientPacakgeDetail.AllowUserToAddRows = false;
            this.dgvPatientPacakgeDetail.BackgroundColor = System.Drawing.SystemColors.Window;
            this.dgvPatientPacakgeDetail.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dgvPatientPacakgeDetail.Location = new System.Drawing.Point(27, 47);
            this.dgvPatientPacakgeDetail.Name = "dgvPatientPacakgeDetail";
            this.dgvPatientPacakgeDetail.Size = new System.Drawing.Size(914, 271);
            this.dgvPatientPacakgeDetail.TabIndex = 0;
            this.dgvPatientPacakgeDetail.CellClick += new System.Windows.Forms.DataGridViewCellEventHandler(this.dgvPatientPacakgeDetail_CellClick);
            // 
            // CustomerOffsetfrm
            // 
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.None;
            this.ClientSize = new System.Drawing.Size(968, 384);
            this.Controls.Add(this.dgvPatientPacakgeDetail);
            this.Font = new System.Drawing.Font("Zawgyi-One", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.Name = "CustomerOffsetfrm";
            this.Text = "Patient Package Detail";
            this.Load += new System.EventHandler(this.CustomerOffsetfrm_Load);
            ((System.ComponentModel.ISupportInitialize)(this.dgvPatientPacakgeDetail)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.DataGridView dgvPatientPacakgeDetail;
    }
}