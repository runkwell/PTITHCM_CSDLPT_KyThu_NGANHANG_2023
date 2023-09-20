using DevExpress.XtraReports.UI;
using System;
using System.Collections;
using System.ComponentModel;
using System.Drawing;

namespace QLVT
{
    public partial class Xrpt_DDHchuacoPN1 : DevExpress.XtraReports.UI.XtraReport
    {
        public Xrpt_DDHchuacoPN1()
        {
            InitializeComponent();
        }
        public Xrpt_DDHchuacoPN1(String macn)
        {
            InitializeComponent();
            this.sqlDataSource1.Queries[0].Parameters[0].Value = macn;
        }
    }
}
