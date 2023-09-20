using DevExpress.XtraReports.UI;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace QLVT
{
    public partial class frm_rp_THNX : Form
    {
        String macn;
        public frm_rp_THNX()
        {
            InitializeComponent();
        }

        private void btnXem_Click(object sender, EventArgs e)
        {
            if (dtpTu.Text.Length == 0 || dtpDen.Text.Length == 0)
            {
                MessageBox.Show("Không được để trống ngày chọn liệt kê\n Vui lòng nhập đủ thông tin!", "Cảnh báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }


            if (dtpTu.Text.Equals(dtpDen.Text))
            {
                MessageBox.Show("Ngày không hợp lệ!\n Vui lòng chọn lại ngày!", "Cảnh báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }
            DateTime dt1 = DateTime.Parse(dtpTu.Text);
            DateTime dt2 = DateTime.Parse(dtpDen.Text);
            Xrpt_THNX1 Xrpt = new Xrpt_THNX1(dtpTu.Text, dtpDen.Text, Program.mGroup);
            Xrpt.label1.Text = "BẢNG TỔNG HỢP NHẬP XUẤT, TỪ " + dt1.ToString("dd-MM-yyyy") + " ĐẾN " + dt2.ToString("dd-MM-yyyy") + " CỦA " + macn;
            ReportPrintTool print = new ReportPrintTool(Xrpt);
            print.ShowPreviewDialog();
        }

        private void frm_rp_THNX_Load(object sender, EventArgs e)
        {
            if (Program.mChinhanh.ToString() == "0")
            {
                macn = "CN1";
            }
            else
            {
                macn = "CN2";
            }

            if (Program.mGroup == "CONGTY")
            {
                macn = "TẤT CẢ CHI NHÁNH";
            }

            DateTime dt1 = DateTime.Parse(DateTime.Today.ToString());
            dtpDen.Text = dt1.ToString("M/dd/yyyy");
        }
    }
}
