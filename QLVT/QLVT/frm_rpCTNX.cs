using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using DevExpress.XtraReports.UI;
namespace QLVT
{
    public partial class frm_rpCTNX : Form
    {
        String macn;
        String maloai;

        public frm_rpCTNX()
        {
            InitializeComponent();
        }

        private void btnXemN_Click(object sender, EventArgs e)
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

            Xrpt_CTNX1 Xrpt = new Xrpt_CTNX1(dt1.ToString("MM/dd/yyyy"), dt2.ToString("MM/dd/yyyy"), Program.mGroup, "N");
             Xrpt.label1.Text = "DANH SÁCH NHẬP VẬT TƯ, TỪ " + dt1.ToString("dd-MM-yyyy") + " ĐẾN " + dt2.ToString("dd-MM-yyyy") + " " + macn;
            ReportPrintTool print = new ReportPrintTool(Xrpt);
            print.ShowPreviewDialog();
        }

        private void frm_rpCTNX_Load(object sender, EventArgs e)
        {


            //dtpDen.EditValue = DateTime.Today;
            //dtpTu.EditValue = DateTime.Today;
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

        private void btnXemX_Click(object sender, EventArgs e)
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

            Xrpt_CTNX1 Xrpt = new Xrpt_CTNX1(dt1.ToString("MM/dd/yyyy"), dt2.ToString("MM/dd/yyyy"), Program.mGroup, "X");
            Xrpt.label1.Text = "DANH SÁCH XUẤT VẬT TƯ, TỪ " + dt1.ToString("dd-MM-yyyy") + " ĐẾN " + dt2.ToString("dd-MM-yyyy") + " " + macn;
            ReportPrintTool print = new ReportPrintTool(Xrpt);
            print.ShowPreviewDialog();
        }


    }
}
