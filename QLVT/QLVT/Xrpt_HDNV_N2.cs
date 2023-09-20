using DevExpress.XtraReports.UI;
using System;
using System.Collections;
using System.ComponentModel;
using System.Drawing;

namespace QLVT
{
    public partial class Xrpt_HDNV_N2 : DevExpress.XtraReports.UI.XtraReport
    {
        public Xrpt_HDNV_N2()
        {
            InitializeComponent();
        }
        public Xrpt_HDNV_N2(String manv, String dateFrom, String dateTo, String macn)
        {
            InitializeComponent();
            //this.sqlDataSource1.Connection.ConnectionString = Program.connstr;
            this.sqlDataSource1.Queries[0].Parameters[0].Value = manv;
            this.sqlDataSource1.Queries[0].Parameters[1].Value = dateFrom;
            this.sqlDataSource1.Queries[0].Parameters[2].Value = dateTo;
            this.sqlDataSource1.Queries[0].Parameters[3].Value = macn;
            this.sqlDataSource1.Fill();
            //  lblTime.Text = DateTime.Now.ToString("yyyy-MM-dd hh:mm:ss");
        }
    }
}
