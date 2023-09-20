
namespace QLVT
{
    partial class frm_rpCTNX
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
            this.dtpDen = new DevExpress.XtraEditors.DateEdit();
            this.dtpTu = new DevExpress.XtraEditors.DateEdit();
            this.btnXemN = new System.Windows.Forms.Button();
            this.label1 = new System.Windows.Forms.Label();
            this.btnXemX = new System.Windows.Forms.Button();
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
            label2.Location = new System.Drawing.Point(405, 115);
            label2.Name = "label2";
            label2.Size = new System.Drawing.Size(27, 13);
            label2.TabIndex = 40;
            label2.Text = "Đến";
            // 
            // lblTu
            // 
            lblTu.AutoSize = true;
            lblTu.Location = new System.Drawing.Point(205, 115);
            lblTu.Name = "lblTu";
            lblTu.Size = new System.Drawing.Size(20, 13);
            lblTu.TabIndex = 38;
            lblTu.Text = "Từ";
            // 
            // dtpDen
            // 
            this.dtpDen.EditValue = new System.DateTime(2022, 3, 23, 0, 0, 0, 0);
            this.dtpDen.Location = new System.Drawing.Point(438, 112);
            this.dtpDen.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.dtpDen.Name = "dtpDen";
            this.dtpDen.Properties.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)});
            this.dtpDen.Properties.CalendarTimeProperties.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)});
            this.dtpDen.Size = new System.Drawing.Size(103, 20);
            this.dtpDen.TabIndex = 41;
            // 
            // dtpTu
            // 
            this.dtpTu.EditValue = new System.DateTime(2017, 1, 1, 0, 0, 0, 0);
            this.dtpTu.Location = new System.Drawing.Point(230, 112);
            this.dtpTu.Margin = new System.Windows.Forms.Padding(4);
            this.dtpTu.Name = "dtpTu";
            this.dtpTu.Properties.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)});
            this.dtpTu.Properties.CalendarTimeProperties.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)});
            this.dtpTu.Size = new System.Drawing.Size(101, 20);
            this.dtpTu.TabIndex = 39;
            // 
            // btnXemN
            // 
            this.btnXemN.Location = new System.Drawing.Point(222, 226);
            this.btnXemN.Name = "btnXemN";
            this.btnXemN.Size = new System.Drawing.Size(116, 80);
            this.btnXemN.TabIndex = 42;
            this.btnXemN.Text = "XEM NHẬP";
            this.btnXemN.UseVisualStyleBackColor = true;
            this.btnXemN.Click += new System.EventHandler(this.btnXemN_Click);
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Font = new System.Drawing.Font("Microsoft Sans Serif", 14F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label1.Location = new System.Drawing.Point(163, 23);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(440, 24);
            this.label1.TabIndex = 43;
            this.label1.Text = "DANH SÁCH NHẬP XUẤT VẬT TƯ THEO THÁNG";
            // 
            // btnXemX
            // 
            this.btnXemX.Location = new System.Drawing.Point(433, 226);
            this.btnXemX.Name = "btnXemX";
            this.btnXemX.Size = new System.Drawing.Size(116, 80);
            this.btnXemX.TabIndex = 44;
            this.btnXemX.Text = "XEM XUẤT";
            this.btnXemX.UseVisualStyleBackColor = true;
            this.btnXemX.Click += new System.EventHandler(this.btnXemX_Click);
            // 
            // frm_rpCTNX
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(800, 450);
            this.Controls.Add(this.btnXemX);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.btnXemN);
            this.Controls.Add(this.dtpDen);
            this.Controls.Add(label2);
            this.Controls.Add(lblTu);
            this.Controls.Add(this.dtpTu);
            this.Name = "frm_rpCTNX";
            this.Text = "frm_rpCTNX";
            this.Load += new System.EventHandler(this.frm_rpCTNX_Load);
            ((System.ComponentModel.ISupportInitialize)(this.dtpDen.Properties.CalendarTimeProperties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.dtpDen.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.dtpTu.Properties.CalendarTimeProperties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.dtpTu.Properties)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private DevExpress.XtraEditors.DateEdit dtpDen;
        private DevExpress.XtraEditors.DateEdit dtpTu;
        private System.Windows.Forms.Button btnXemN;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Button btnXemX;

    }
}