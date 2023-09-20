using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using DevExpress.XtraReports.UI;


namespace QLVT
{
    public partial class frm_rpDSNV : Form
    {
        String macn;
        public frm_rpDSNV()
        {
            InitializeComponent();
        }

        private void btnIn_Click(object sender, EventArgs e)
        {
            Xrpt_DSNV1 Xrpt = new Xrpt_DSNV1(macn);
            Xrpt.label1.Text = "DANH SÁCH NHÂN VIÊN CỦA CHI NHÁNH " + macn;
            ReportPrintTool print = new ReportPrintTool(Xrpt);
            print.ShowPreviewDialog();
        }

        private void cboChiNhanh_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (cboChiNhanh.SelectedValue.ToString().Contains("SERVER1"))
            {
                macn = "CN1";
            }
            else if (cboChiNhanh.SelectedValue.ToString().Contains("SERVER2"))
            {
                macn = "CN2";
            }
            
        }

        private void frm_rpDSNV_Load(object sender, EventArgs e)
        {
            cboChiNhanh.DataSource = Program.bds_dspm1;
            cboChiNhanh.DisplayMember = "TENCN";
            cboChiNhanh.ValueMember = "TENSERVER";
            cboChiNhanh.SelectedIndex = Program.mChinhanh;
            cboChiNhanh.DropDownStyle = ComboBoxStyle.DropDownList;
            if (Program.mGroup.Equals("CHINHANH"))
            {
                cboChiNhanh.Enabled = false;
            }
            else
            {
                cboChiNhanh.Enabled = true;
            }
            macn = ((DataRowView)Program.bds_dspm[Program.mChinhanh])["TENCN"].ToString();
        }
    }
}
