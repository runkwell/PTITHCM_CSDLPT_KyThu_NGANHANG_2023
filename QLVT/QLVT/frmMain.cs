using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace QLVT
{
    public partial class frmMain : DevExpress.XtraBars.Ribbon.RibbonForm
    {
        public frmMain()
        {
            InitializeComponent();
            dangNhap();
        }
        public Form CheckExists(Type ftype)
        {
            foreach (Form f in this.MdiChildren)
                if (f.GetType() == ftype)
                    return f;
            return null;
        }
        public void dangNhap()
        {
            if (Program.mloginDN != "")
            {
                MessageBox.Show("Bạn cần đăng xuất trước khi thực hiện hành động này");
                return;
            }

            Form frm = this.CheckExists(typeof(frmDangNhap));
            if (frm != null)
            {
                frm.Activate();
                frm.Visible = true;
            }
            else
            {
                frmDangNhap f = new frmDangNhap();
                f.MdiParent = this;
                f.Show();
            }
            btnDangNhap.Enabled = true;
            ribbonPage2.Visible = ribbonPage3.Visible = ribbonPage4.Visible = false;
            barButtonItem6.Enabled = false;
            barButtonItem5.Enabled = false;

        }

        private void btnDangNhap_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            if (Program.mloginDN != "")
            {
                MessageBox.Show("Bạn cần đăng xuất trước khi thực hiện hành động này");
                return;
            }

            Form frm = this.CheckExists(typeof(frmDangNhap));
            if (frm != null) frm.Activate();
            else
            {
                frmDangNhap f = new frmDangNhap();
                f.MdiParent = this;
                f.Show();
            }
        }

        private void btnHangHoa_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            Form frm = this.CheckExists(typeof(frmHangHoa));
            if (frm != null) frm.Activate();
            else
            {
                frmHangHoa f = new frmHangHoa();
                f.MdiParent = this;
                f.Show();
            }
        }


        private void btnDonDH_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            Form frm = this.CheckExists(typeof(frmDonDH));
            if (frm != null) frm.Activate();
            else
            {
                frmDonDH f = new frmDonDH();
                f.MdiParent = this;
                f.Show();
            }
        }

        private void btnNV_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            Form frm = this.CheckExists(typeof(frmNhanVien));
            if (frm != null) frm.Activate();
            else
            {
                frmNhanVien f = new frmNhanVien();
                f.MdiParent = this;
                f.Show();
            }
        }

        private void btnPN_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            Form frm = this.CheckExists(typeof(frmPhieuNhap));
            if (frm != null) frm.Activate();
            else
            {
                frmPhieuNhap f = new frmPhieuNhap();
                f.MdiParent = this;
                f.Show();
            }
        }

        private void btnHD_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            Form frm = this.CheckExists(typeof(frmPhieuXuat));
            if (frm != null) frm.Activate();
            else
            {
                frmPhieuXuat f = new frmPhieuXuat();
                f.MdiParent = this;
                f.Show();
            }
        }

        private void btnKho_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            Form frm = this.CheckExists(typeof(frmKho));
            if (frm != null) frm.Activate();
            else
            {
                frmKho f = new frmKho();
                f.MdiParent = this;
                f.Show();
            }
        }


        private void barButtonItem1_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            Form frm = this.CheckExists(typeof(frm_rpDSNV));
            if (frm != null) frm.Activate();
            else
            {
                frm_rpDSNV f = new frm_rpDSNV();
                f.MdiParent = this;
                f.Show();
            }
        }

        private void btnrpDSVT_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            Form frm = this.CheckExists(typeof(frm_rpDSVT));
            if (frm != null) frm.Activate();
            else
            {
                frm_rpDSVT f = new frm_rpDSVT();
                f.MdiParent = this;
                f.Show();
            }
        }

        private void btnCTNX_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            Form frm = this.CheckExists(typeof(frm_rpCTNX));
            if (frm != null) frm.Activate();
            else
            {
                frm_rpCTNX f = new frm_rpCTNX();
                f.MdiParent = this;
                f.Show();
            }
        }

        private void barButtonItem5_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            Form frm = this.CheckExists(typeof(frmTaoTKNV));
            if (frm != null) frm.Activate();
            else
            {
                frmTaoTKNV f = new frmTaoTKNV();
                f.MdiParent = this;
                f.Show();
            }
        }

        private void barButtonItem6_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            if (Program.mloginDN == "")
            {
                MessageBox.Show("Bạn phải đăng nhập trước khi đăng xuất!", "", MessageBoxButtons.OK);
                return;
            }
            else
            {
                try
                {
                    Program.servername = "";
                    Program.username = "";
                    Program.mlogin = "";
                    Program.password = "";
                    Program.mloginDN = "";
                    Program.passwordDN = "";
                    Program.mGroup = "";
                    if (Program.conn.State == ConnectionState.Open) Program.conn.Close();

                    Form[] childArray = this.MdiChildren; // close form
                    foreach (Form childForm in childArray)
                    {
                        childForm.Close();
                    }


                    MANV.Text = "MANV "; HOTEN.Text = "HOTEN "; NHOM.Text = "NHOM";
                    MessageBox.Show("Đăng xuất thành công.", "", MessageBoxButtons.OK);
                    dangNhap();
                }
                catch (Exception ex)
                {
                    MessageBox.Show("Lỗi: " + ex.Message, "", MessageBoxButtons.OK);
                    return;
                }
            }
            return;
        }

        private void btnDDHcPN_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            Form frm = this.CheckExists(typeof(frm_DDHchuacoPN));
            if (frm != null) frm.Activate();
            else
            {
                frm_DDHchuacoPN f = new frm_DDHchuacoPN();
                f.MdiParent = this;
                f.Show();
            }
        }

        private void barButtonItem2_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            Form frm = this.CheckExists(typeof(frm_rpCT_HDNV));
            if (frm != null) frm.Activate();
            else
            {
                frm_rpCT_HDNV f = new frm_rpCT_HDNV();
                f.MdiParent = this;
                f.Show();
            }
        }

        private void btnTHNX_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            Form frm = this.CheckExists(typeof(frm_rp_THNX));
            if (frm != null) frm.Activate();
            else
            {
                frm_rp_THNX f = new frm_rp_THNX();
                f.MdiParent = this;
                f.Show();
            }
        }
    }
}
