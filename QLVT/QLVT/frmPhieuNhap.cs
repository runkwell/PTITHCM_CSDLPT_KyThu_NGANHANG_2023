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
using System.Text.RegularExpressions;

namespace QLVT
{
    public partial class frmPhieuNhap : Form
    {
        int vitri = 0;
        int check = 0;
        public frmPhieuNhap()
        {
            InitializeComponent();
        }

        private void pHIEUNHAPBindingNavigatorSaveItem_Click(object sender, EventArgs e)
        {
            this.Validate();
            this.bdsPN.EndEdit();
            this.tableAdapterManager.UpdateAll(this.DS);

        }

        public Form CheckExists(Type ftype)
        {
            foreach (Form f in this.MdiChildren)
                if (f.GetType() == ftype)
                    return f;
            return null;
        }
        private void frmPhieuNhap_Load(object sender, EventArgs e)
        {

            DS.EnforceConstraints = false;
            this.CT_PHIEUNHAPTableAdapter.Connection.ConnectionString = Program.connstr;
            this.CT_PHIEUNHAPTableAdapter.Fill(this.DS.CTPN);

            this.PHIEUNHAPTableAdapter.Connection.ConnectionString = Program.connstr;
            this.PHIEUNHAPTableAdapter.Fill(this.DS.PhieuNhap);

            cboChiNhanh.DataSource = Program.bds_dspm;  //sao chép dspm đã load ở form đăng nhập.
            cboChiNhanh.DisplayMember = "TENCN";
            cboChiNhanh.ValueMember = "TENSERVER";
            cboChiNhanh.SelectedIndex = Program.mChinhanh;
            cboChiNhanh.DropDownStyle = ComboBoxStyle.DropDownList;
            nGAYLAPDateEdit.EditValue = DateTime.Today;
            panelControl2.Enabled = false;
            if (Program.mGroup == "CONGTY")
            {
                cboChiNhanh.Enabled = true;
                btnThem.Enabled = btnHieuChinh.Enabled = btnXoa.Enabled = btnLuu.Enabled = btnPhucHoi.Enabled = false;
            }
            else
            {
                cboChiNhanh.Enabled = false;
                btnThem.Enabled = btnHieuChinh.Enabled = btnXoa.Enabled = btnLuu.Enabled = btnPhucHoi.Enabled = true;
            }
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
            if (Program.KetNoi() == 0)
                MessageBox.Show("Lỗi kết nối tới cơ sở mới!", "Lỗi", MessageBoxButtons.OK);
            else
            {
                this.PHIEUNHAPTableAdapter.Connection.ConnectionString = Program.connstr;
                this.PHIEUNHAPTableAdapter.Fill(this.DS.PhieuNhap);
                this.CT_PHIEUNHAPTableAdapter.Connection.ConnectionString = Program.connstr;
                this.CT_PHIEUNHAPTableAdapter.Fill(this.DS.CTPN);
            }
        }

        private void btnThem_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            vitri = bdsPN.Position; // giữ vị trí kh hiện tại
            panelControl2.Enabled = true;
            bdsPN.AddNew();
            txtSoPN.Enabled = txtMaDDH.Enabled = true;
            txtSoPN.Focus();
            txtMaNV.Text = Program.username.Trim();
            txtMaNV.Enabled = false;
            btnThem.Enabled = btnHieuChinh.Enabled = btnXoa.Enabled = btnTaiLai.Enabled = btnThoat.Enabled = btnChiTiet.Enabled = false;
            btnLuu.Enabled = btnPhucHoi.Enabled = true;
            gcPN.Enabled =cTPNGridControl.Enabled = false;
            DateTime today = DateTime.Today;
            nGAYLAPDateEdit.Text = today.ToString("MM/dd/yyyy");
            check = 1;
        }

        private void btnHieuChinh_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            vitri = bdsPN.Position;
            txtMaDDH.Enabled = false;
            txtMaNV.Enabled = false;
            txtSoPN.Enabled = false;
            panelControl2.Enabled = true;
            btnThem.Enabled = btnXoa.Enabled = btnHieuChinh.Enabled = btnTaiLai.Enabled = btnThoat.Enabled = btnChiTiet.Enabled = false;
            btnLuu.Enabled = btnPhucHoi.Enabled = true;
            gcPN.Enabled = cTPNGridControl.Enabled = false;
            check = 2;
        }

        private int checkMa(String ma)
        {
          string strLenh = "DECLARE @RC INT " +
                              "EXEC @RC = SP_CHECKMAPNEXISTS " +
                              "@MAPN=" + ma + " " +
                               "SELECT 'Result' = @RC";
            SqlDataReader dataReader = null;
            dataReader = Program.ExecSqlDataReader(strLenh);
            // Đọc và lấy 
            dataReader.Read();
            int result = int.Parse(dataReader.GetValue(0).ToString());
            dataReader.Close();
            return result;
        }

        private int checkNgay(String ma, String ngay)
        {
            string strLenh1 = "DECLARE	@rc int " +
                                "EXEC @rc = SP_checkNgayPN " +
                                "@MADDH=" + ma +
                                ", @NGAYPN='" + ngay + "' " +
                                 "SELECT 'result' = @rc";
            SqlDataReader dataReader = null;
            dataReader = Program.ExecSqlDataReader(strLenh1);
            // Đọc và lấy 
            dataReader.Read();
            int result = int.Parse(dataReader.GetValue(0).ToString());
            dataReader.Close();
            return result;
        }

        private void btnLuu_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            if (txtMaDDH.Text.Trim() == "")
            {
                MessageBox.Show("Mã DDH không được thiếu!", "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                txtMaDDH.Focus();
                return;
            }

            if (txtSoPN.Text.Trim().Length == 0)
            {
                MessageBox.Show("Số PN không được thiếu!", "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                txtSoPN.Focus();
                return;
            }
            if (txtMaKho.Text.Trim().Length == 0)
            {
                MessageBox.Show("Mã kho không được thiếu!", "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                txtMaKho.Focus();
                return;
            }
            string maDDH = txtMaDDH.Text.Trim();
            string maSoPN = txtSoPN.Text.Trim();
            String dt = nGAYLAPDateEdit.Text;
            try
            {
                if (check == 1)
                {

                    int result1 = checkMa(maSoPN);
                    if (result1 == 1)
                    {
                        MessageBox.Show("PN này đã tồn tại!", "Thông báo",
                        MessageBoxButtons.OK, MessageBoxIcon.Warning);
                        txtMaDDH.Focus();
                        return;
                    }


                    int result2 = checkNgay(maDDH, dt);
                    if (result2 == 0)
                    {
                        MessageBox.Show("Ngày nhập hàng phải sau ngày lập đơn đặt hàng!", "Thông báo",
                        MessageBoxButtons.OK, MessageBoxIcon.Warning);
                        txtMaDDH.Focus();
                        return;
                    }
                }
                


                bdsPN.EndEdit();    // kết thúc quá trình tạo. -> Ghi vào trong bds.
                bdsPN.ResetCurrentItem();   //Đưa những thông tin đó lên lưới.
                this.PHIEUNHAPTableAdapter.Connection.ConnectionString = Program.connstr;
                this.PHIEUNHAPTableAdapter.Update(this.DS.PhieuNhap); // Update trên adapter có 3 nghĩa: vừa là insert, update, delete. Nó tùy vào tình huống cụ thể để đưa lệnh tương ứng.
                this.CT_PHIEUNHAPTableAdapter.Connection.ConnectionString = Program.connstr;
                this.CT_PHIEUNHAPTableAdapter.Fill(this.DS.CTPN);
                if (check == 1)
                {
                    MessageBox.Show("Thêm thành công!", "", MessageBoxButtons.OK);
                }
                else if (check == 2)
                {
                    MessageBox.Show("Cập nhật thành công!", "", MessageBoxButtons.OK);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Lỗi: " + ex.Message, "", MessageBoxButtons.OK);
                return;
            }
            check = 0;
            gcPN.Enabled = cTPNGridControl.Enabled = true;
            btnThem.Enabled = btnXoa.Enabled = btnHieuChinh.Enabled = btnTaiLai.Enabled = btnThoat.Enabled = btnChiTiet.Enabled = true;
            btnLuu.Enabled = btnPhucHoi.Enabled = false;
            panelControl2.Enabled = false;
        }

        private void btnPhucHoi_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            bdsPN.CancelEdit();
            this.PHIEUNHAPTableAdapter.Fill(this.DS.PhieuNhap);
            if (btnThem.Enabled == false) bdsPN.Position = vitri;
            gcPN.Enabled = cTPNGridControl.Enabled = true;
            panelControl2.Enabled = false;
            btnThem.Enabled = btnHieuChinh.Enabled = btnXoa.Enabled = btnTaiLai.Enabled = btnThoat.Enabled = btnChiTiet.Enabled = true;
            btnLuu.Enabled = btnPhucHoi.Enabled = false;
        }

        private void btnXoa_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            String sopn = ((DataRowView)bdsPN[bdsPN.Position])["MAPN"].ToString();
            if (bdsCT_PN.Count > 0)
            {
                MessageBox.Show("Không thể xóa PN này!!!", "",
                    MessageBoxButtons.OK);
                return;
            }
            if (MessageBox.Show("Bạn có thật sự muốn xóa PN này ??", "Xác nhận",
               MessageBoxButtons.OKCancel) == DialogResult.OK)
            {
                try
                {
                    // Giữ lại cmnd hiện tại đang đứng để nếu xóa ở máy hiện tại thành công mà xóa ở CSDL thất bại thì ta sẽ fill data về lại máy và nhờ cmnd đó thì con trỏ nó sẽ nhảy đến cmnd vừa xóa.
                    bdsPN.RemoveCurrent();  // Xóa trên máy hiện tại trước, sau đó mới xóa trên CSDL sau.
                    this.PHIEUNHAPTableAdapter.Connection.ConnectionString = Program.connstr;
                    this.PHIEUNHAPTableAdapter.Update(this.DS.PhieuNhap); // xóa dữ liệu đó ở CSDL.
                    this.PHIEUNHAPTableAdapter.Fill(this.DS.PhieuNhap);
                    this.CT_PHIEUNHAPTableAdapter.Connection.ConnectionString = Program.connstr;
                    this.CT_PHIEUNHAPTableAdapter.Fill(this.DS.CTPN);
                }
                catch (Exception ex)
                {
                    MessageBox.Show("Lỗi xóa nhân viên. Bạn hãy xóa lại\n" + ex.Message, "",
                        MessageBoxButtons.OK);
                    this.PHIEUNHAPTableAdapter.Fill(this.DS.PhieuNhap);   // Trường hợp xóa ở máy hiện tại thành công nhưng xóa trên CSDL bị lỗi thì ta phải tải về lại máy hiện tại.
                    bdsPN.Position = bdsPN.Find("MAPN", sopn);  // đưa con trỏ nhảy đến vị trí manv đã xóa thất bại trước đó.
                    return;
                }
            }
            if (bdsPN.Count == 0) btnXoa.Enabled = false;
        }

        private void btnTaiLai_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            try
            {
                this.PHIEUNHAPTableAdapter.Fill(this.DS.PhieuNhap);
                this.CT_PHIEUNHAPTableAdapter.Fill(this.DS.CTPN);
            }
            catch (Exception ex)
            {
                MessageBox.Show("Lỗi Tải lại đơn đặt hàng: " + ex.Message, "", MessageBoxButtons.OK);
                return;
            }
        }

        private void btnThoat_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            this.Close();
        }

        private void btnChiTiet_Click(object sender, EventArgs e)
        {
            Program.maddh = txtMaDDH.Text.ToString().Trim();
            Program.sopn = txtSoPN.Text.ToString().Trim();
            Form frm = this.CheckExists(typeof(frmCT_PN));
            if (frm != null) frm.Activate();
            else
            {
                frmCT_PN f = new frmCT_PN();
                f.Show();
            }
        }

        private void phieuNhapBindingNavigatorSaveItem_Click_1(object sender, EventArgs e)
        {
            this.Validate();
            this.bdsPN.EndEdit();
            this.tableAdapterManager.UpdateAll(this.DS);

        }
    }
}
