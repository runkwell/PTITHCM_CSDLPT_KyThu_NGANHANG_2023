
namespace QLVT
{
    partial class frm_rp_THNX
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
            System.Windows.Forms.Label label2;
            System.Windows.Forms.Label lblTu;
            this.label1 = new System.Windows.Forms.Label();
            this.dtpDen = new DevExpress.XtraEditors.DateEdit();
            this.dtpTu = new DevExpress.XtraEditors.DateEdit();
            this.btnXem = new System.Windows.Forms.Button();
            label2 = new System.Windows.Forms.Label();
            lblTu = new System.Windows.Forms.Label();
            ((System.ComponentModel.ISupportInitialize)(this.dtpDen.Properties.CalendarTimeProperties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.dtpDen.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.dtpTu.Properties.CalendarTimeProperties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.dtpTu.Properties)).BeginInit();
            this.SuspendLayout();
            // 
            // label2
            // 
            label2.AutoSize = true;
            label2.Location = new System.Drawing.Point(409, 139);
            label2.Name = "label2";
            label2.Size = new System.Drawing.Size(27, 13);
            label2.TabIndex = 46;
            label2.Text = "Đến";
            // 
            // lblTu
            // 
            lblTu.AutoSize = true;
            lblTu.Location = new System.Drawing.Point(209, 139);
            lblTu.Name = "lblTu";
            lblTu.Size = new System.Drawing.Size(20, 13);
            lblTu.TabIndex = 44;
            lblTu.Text = "Từ";
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Font = new System.Drawing.Font("Microsoft Sans Serif", 14F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label1.Location = new System.Drawing.Point(210, 51);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(357, 24);
            this.label1.TabIndex = 48;
            this.label1.Text = "TỔNG HỢP NHẬP XUẤT TRONG NGÀY";
            // 
            // dtpDen
            // 
            this.dtpDen.EditValue = new System.DateTime(2024, 5, 23, 0, 0, 0, 0);
            this.dtpDen.Location = new System.Drawing.Point(442, 136);
            this.dtpDen.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.dtpDen.Name = "dtpDen";
            this.dtpDen.Properties.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)});
            this.dtpDen.Properties.CalendarTimeProperties.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)});
            this.dtpDen.Properties.Mask.EditMask = "dd-MM-yyyy";
            this.dtpDen.Size = new System.Drawing.Size(103, 20);
            this.dtpDen.TabIndex = 47;
            // 
            // dtpTu
            // 
            this.dtpTu.EditValue = new System.DateTime(2017, 1, 1, 0, 0, 0, 0);
            this.dtpTu.Location = new System.Drawing.Point(234, 136);
            this.dtpTu.Margin = new System.Windows.Forms.Padding(4);
            this.dtpTu.Name = "dtpTu";
            this.dtpTu.Properties.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)});
            this.dtpTu.Properties.CalendarTimeProperties.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)});
            this.dtpTu.Properties.Mask.EditMask = "dd-MM-yyyy";
            this.dtpTu.Size = new System.Drawing.Size(101, 20);
            this.dtpTu.TabIndex = 45;
            // 
            // btnXem
            // 
            this.btnXem.Location = new System.Drawing.Point(320, 251);
            this.btnXem.Name = "btnXem";
            this.btnXem.Size = new System.Drawing.Size(116, 80);
            this.btnXem.TabIndex = 49;
            this.btnXem.Text = "XEM";
            this.btnXem.UseVisualStyleBackColor = true;
            this.btnXem.Click += new System.EventHandler(this.btnXem_Click);
            // 
            // frm_rp_THNX
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(800, 450);
            this.Controls.Add(this.btnXem);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.dtpDen);
            this.Controls.Add(label2);
            this.Controls.Add(lblTu);
            this.Controls.Add(this.dtpTu);
            this.Name = "frm_rp_THNX";
            this.Text = "frm_rp_THNX";
            this.Load += new System.EventHandler(this.frm_rp_THNX_Load);
            ((System.ComponentModel.ISupportInitialize)(this.dtpDen.Properties.CalendarTimeProperties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.dtpDen.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.dtpTu.Properties.CalendarTimeProperties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.dtpTu.Properties)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label label1;
        private DevExpress.XtraEditors.DateEdit dtpDen;
        private DevExpress.XtraEditors.DateEdit dtpTu;
        private System.Windows.Forms.Button btnXem;
    }
}