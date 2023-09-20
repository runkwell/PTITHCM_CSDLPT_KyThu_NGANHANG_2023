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
using System.Data.SqlClient;
namespace QLVT
{
    public partial class frm_DDHchuacoPN : Form
    {
        String macn;
        public frm_DDHchuacoPN()
        {
            InitializeComponent();
        }

        private void btnXem_Click(object sender, EventArgs e)
        {
            macn = cboChiNhanh.Text.ToString();
            Xrpt_DDHchuacoPN1 Xrpt = new Xrpt_DDHchuacoPN1(macn);
            Xrpt.label1.Text = "DANH SÁCH CÁC ĐƠN ĐẶT HÀNG CHƯA CÓ PHIẾU NHẬP CỦA " + macn;
            ReportPrintTool print = new ReportPrintTool(Xrpt);
            print.ShowPreviewDialog();
        }

        private void cboChiNhanh_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (cboChiNhanh.SelectedValue.ToString() == "System.Data.DataRowView")
                return;
            Program.servername = cboChiNhanh.SelectedValue.ToString();
            if (cboChiNhanh.SelectedIndex != Program.mChinhanh)
            {
                Program.mlogin = Program.remotelogin;
                Program.password = Program.remotepassword;
            }
            else
            {
                Program.mlogin = Program.mloginDN;
                Program.password = Program.passwordDN;
            }
            if (Program.KetNoi() == 0) {
                MessageBox.Show("Lỗi kết nối tới cơ sở mới!", "Lỗi", MessageBoxButtons.OK);
            }
                
        }

        private void frm_DDHchuacoPN_Load(object sender, EventArgs e)
        {
            cboChiNhanh.DataSource = Program.bds_dspm1;  //sao chép dspm đã load ở form đăng nhập.
            cboChiNhanh.DisplayMember = "TENCN";
            cboChiNhanh.ValueMember = "TENSERVER";
            cboChiNhanh.SelectedIndex = Program.mChinhanh;
            cboChiNhanh.DropDownStyle = ComboBoxStyle.DropDownList;
            macn = cboChiNhanh.Text.ToString();
            if (Program.mGroup == "CONGTY")
            {
                cboChiNhanh.Enabled = true;
            }
            else
            {
                cboChiNhanh.Enabled = false;
            }
        }
    }
}
