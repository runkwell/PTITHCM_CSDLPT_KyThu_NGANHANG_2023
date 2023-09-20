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
    public partial class frm_rpDSVT : Form
    {
        public frm_rpDSVT()
        {
            InitializeComponent();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            Xrpt_DSVT1 Xrpt = new Xrpt_DSVT1();
            Xrpt.label1.Text = "DANH SÁCH VẬT TƯ XẾP THEO TÊN";
            ReportPrintTool print = new ReportPrintTool(Xrpt);
            print.ShowPreviewDialog();
        }
    }
}
