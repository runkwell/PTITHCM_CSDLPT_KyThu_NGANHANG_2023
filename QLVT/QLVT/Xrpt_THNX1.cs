using DevExpress.XtraReports.UI;
using System;
using System.Collections;
using System.ComponentModel;
using System.Drawing;

namespace QLVT
{
    public partial class Xrpt_THNX1 : DevExpress.XtraReports.UI.XtraReport
    {
        public Xrpt_THNX1()
        {
            InitializeComponent();
        }
        public Xrpt_THNX1(String dateFrom, String dateTo, String manhom)
        {
            InitializeComponent();
            this.sqlDataSource1.Connection.ConnectionString = Program.connstr;

            this.sqlDataSource1.Queries[0].Parameters[0].Value = dateFrom;
            this.sqlDataSource1.Queries[0].Parameters[1].Value = dateTo;
            this.sqlDataSource1.Queries[0].Parameters[2].Value = manhom;
            //this.sqlDataSource1.Fill();
            
        }
    }
}
