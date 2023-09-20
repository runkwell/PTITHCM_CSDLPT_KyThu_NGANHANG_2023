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
    public partial class frmNhanVien : Form
    {
        int vitri = 0;
        string macn = "";
        int check = 0;
        public frmNhanVien()
        {
            InitializeComponent();
        }

        private void nHANVIENBindingNavigatorSaveItem_Click(object sender, EventArgs e)
        {
            this.Validate();
            this.bdsNV.EndEdit();
            this.tableAdapterManager.UpdateAll(this.DS);

        }

        private void frmNhanVien_Load(object sender, EventArgs e)
        {


            DS.EnforceConstraints = false;


            this.NHANVIENTableAdapter.Connection.ConnectionString = Program.connstr;
            this.NHANVIENTableAdapter.Fill(this.DS.NhanVien);

            macn = ((DataRowView)bdsNV[0])["MACN"].ToString();
            cboChiNhanh.DataSource = Program.bds_dspm;  //sao chép dspm đã load ở form đăng nhập.
            cboChiNhanh.DisplayMember = "TENCN";
            cboChiNhanh.ValueMember = "TENSERVER";
            cboChiNhanh.SelectedIndex = Program.mChinhanh;
            cboChiNhanh.DropDownStyle = ComboBoxStyle.DropDownList;
            panelControl1.Enabled = false;

            nGAYSINHDateEdit.EditValue = DateTime.Today;
            if (Program.mGroup == "CONGTY")
            {
                cboChiNhanh.Enabled = true;
                btnThem.Enabled = btnHieuChinh.Enabled = btnXoa.Enabled = btnChuyenNV.Enabled = btnLuu.Enabled = btnPhucHoi.Enabled = false;
            }
            else
            {
                cboChiNhanh.Enabled = false;
                btnThem.Enabled = btnHieuChinh.Enabled = btnXoa.Enabled = btnLuu.Enabled = btnPhucHoi.Enabled = true;
            }
            btnLuu.Enabled = btnPhucHoi.Enabled = false;
        }

        private void btnThem_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            vitri = bdsNV.Position; //Giữ lại vị trí khách hàng mà chúng ta đang đứng
            panelControl1.Enabled = true;
            bdsNV.AddNew();
            txtMaNV.Enabled = true;
            txtMaNV.Focus();
            txtMaCN.Text = macn;
            txtMaCN.Enabled = false;
            chkXoa.Checked = false;

            nGAYSINHDateEdit.Text = "1/1/1990";
            btnThem.Enabled = btnHieuChinh.Enabled = btnXoa.Enabled = btnTaiLai.Enabled = btnThoat.Enabled = btnChuyenNV.Enabled = false;
            btnLuu.Enabled = btnPhucHoi.Enabled = true;
            gcNV.Enabled = false;
            check = 1;
        }

        private void btnHieuChinh_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            vitri = bdsNV.Position;
            txtMaNV.Enabled = false;
            txtMaCN.Enabled = false;


            panelControl1.Enabled = true;
            btnThem.Enabled = btnXoa.Enabled = btnHieuChinh.Enabled = btnTaiLai.Enabled = btnThoat.Enabled = btnChuyenNV.Enabled = false;
            btnLuu.Enabled = btnPhucHoi.Enabled = true;
            gcNV.Enabled = false;
            check = 2;
        }

        private void btnLuu_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            if (txtMaNV.Text.Trim() == "")
            {
                MessageBox.Show("Mã nhân viên không được thiếu!", "", MessageBoxButtons.OK);
                txtMaNV.Focus();
                return;
            }
            String manvc = "\\d+$";
            if (!Regex.IsMatch(txtMaNV.Text.Trim(), manvc) || txtMaNV.Text.Trim().Length > 6)
            {
                MessageBox.Show("Nhập sai định dạng mã nhân viên!", "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                txtMaNV.Focus();
                return;
            }
            String hoten = "[a-zA-Z]";
            if (txtHo.Text.Trim().Length == 0)
            {
                MessageBox.Show("Họ nhân viên không được trống!", "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                txtHo.Focus();
                return;
            }
            if (!Regex.IsMatch(txtHo.Text.Trim(), hoten))
            {
                MessageBox.Show("Nhập sai định dạng họ!", "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                txtHo.Focus();
                return;
            }
            if (txtTen.Text.Trim().Length == 0)
            {
                MessageBox.Show("Tên nhân viên không được trống!", "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                txtTen.Focus();
                return;
            }
            if (!Regex.IsMatch(txtTen.Text.Trim(), hoten))
            {
                MessageBox.Show("Nhập sai định dạng tên!", "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                txtTen.Focus();
                return;
            }


            String CMND = "\\d+$";
            if (txtSOCMND.Text.Trim().Length == 0)
            {
                MessageBox.Show("CMND không được bỏ trống!", "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                txtSOCMND.Focus();
                return;
            }
            if (!Regex.IsMatch(txtSOCMND.Text.Trim(), CMND) || txtSOCMND.Text.Trim().Length < 9 || txtSOCMND.Text.Trim().Length > 10)
            {
                MessageBox.Show("Nhập sai định dạng CMND!", "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                txtSOCMND.Focus();
                return;
            }

            try
            {
                if (check == 1)
                {
                    string strLenh = "DECLARE @RC INT " +
                               "EXEC @RC = SP_CHECKNVEXISTS " +
                               "@MANV=" + txtMaNV.Text.Trim() + " " +
                               "SELECT 'Result' = @RC";
                    SqlDataReader dataReader = null;
                    dataReader = Program.ExecSqlDataReader(strLenh);
                    // Đọc và lấy 
                    dataReader.Read();
                    int result = int.Parse(dataReader.GetValue(0).ToString());
                    dataReader.Close();
                    if (result == 1)
                    {
                        MessageBox.Show("Mã nhân viên này đã tồn tại!", "Thông báo",
                        MessageBoxButtons.OK, MessageBoxIcon.Warning);
                        txtMaNV.Focus();
                        return;
                    }
                }
                bdsNV.EndEdit();    // kết thúc quá trình tạo. -> Ghi vào trong bds.
                bdsNV.ResetCurrentItem();   //Đưa những thông tin đó lên lưới.
                this.NHANVIENTableAdapter.Connection.ConnectionString = Program.connstr;
                this.NHANVIENTableAdapter.Update(this.DS.NhanVien); // Update trên adapter có 3 nghĩa: vừa là insert, update, delete. Nó tùy vào tình huống cụ thể để đưa lệnh tương ứng.
                //this.KHACHHANGTableAdapter.Fill(this.DS.KhachHang);
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
            gcNV.Enabled = true;
            btnThem.Enabled = btnXoa.Enabled = btnHieuChinh.Enabled = btnTaiLai.Enabled = btnThoat.Enabled = btnChuyenNV.Enabled = true;
            btnLuu.Enabled = btnPhucHoi.Enabled = false;
            panelControl1.Enabled = false;
        }

        private void btnPhucHoi_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            bdsNV.CancelEdit();
            this.NHANVIENTableAdapter.Fill(this.DS.NhanVien);    // tải lại vì khi chọn thêm, sau đó phục hồi thì trên grild vẫn xuất hiện 1 ô trắng do chưa load lên lại vào grild.
            if (btnThem.Enabled == false) bdsNV.Position = vitri;   //nếu trường hợp đã bấm nút Thêm thì sẽ nhảy về lại vị trí trước đó.
            gcNV.Enabled = true;
            panelControl1.Enabled = false;
            btnThem.Enabled = btnHieuChinh.Enabled = btnXoa.Enabled = btnTaiLai.Enabled = btnThoat.Enabled = btnChuyenNV.Enabled = true;
            btnLuu.Enabled = btnPhucHoi.Enabled = false;
        }

        private void btnXoa_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            String manv = ((DataRowView)bdsNV[bdsNV.Position])["MANV"].ToString();

            if (MessageBox.Show("Bạn có thật sự muốn xóa nhân viên này ??", "Xác nhận",
                MessageBoxButtons.OKCancel) == DialogResult.OK)
            {
                try
                {
                    bdsNV.RemoveCurrent();  // Xóa trên máy hiện tại trước, sau đó mới xóa trên CSDL sau.
                    this.NHANVIENTableAdapter.Connection.ConnectionString = Program.connstr;
                    this.NHANVIENTableAdapter.Update(this.DS.NhanVien); // xóa dữ liệu đó ở CSDL.
                    this.NHANVIENTableAdapter.Fill(this.DS.NhanVien);
                }
                catch (Exception ex)
                {
                    MessageBox.Show("Lỗi xóa nhân viên. Bạn hãy xóa lại\n" + ex.Message, "",
                        MessageBoxButtons.OK);
                    this.NHANVIENTableAdapter.Fill(this.DS.NhanVien);   // Trường hợp xóa ở máy hiện tạo thành công nhưng xóa trên CSDL bị lỗi thì ta phải tải về lại máy hiện tại.
                    bdsNV.Position = bdsNV.Find("MANV", manv);  // đưa con trỏ nhảy đến vị trí manv đã xóa thất bại trước đó.
                    return;
                }
            }
            if (bdsNV.Count == 0) btnXoa.Enabled = false;
        }

        private void btnTaiLai_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            try
            {

                this.NHANVIENTableAdapter.Fill(this.DS.NhanVien);
                this.NHANVIENTableAdapter.Update(this.DS.NhanVien);
            }
            catch (Exception ex)
            {
                MessageBox.Show("Lỗi Tải lại danh sách nhân viên: " + ex.Message, "", MessageBoxButtons.OK);
                return;
            }
        }

        private void btnThoat_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            this.Close();
        }

        private void cboChiNhanh_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (cboChiNhanh.SelectedValue.ToString() == "System.Data.DataRowView") return;
            Program.servername = cboChiNhanh.SelectedValue.ToString();

            if (cboChiNhanh.SelectedIndex != Program.mChinhanh) //nếu ta chọn chi nhánh khác với chi nhánh ở thời điểm đăng nhập thì ta sẽ dùng tk HTKN;
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
                MessageBox.Show("Lỗi kết nối về chi nhánh mới!", "", MessageBoxButtons.OK);
            else
            {
                this.NHANVIENTableAdapter.Connection.ConnectionString = Program.connstr;    // gán thông tin đăng nhập vào các Adapter tương ứng để fill lấy thông tin đúng với thông tin đăng nhập.
                this.NHANVIENTableAdapter.Fill(this.DS.NhanVien);
            }
        }

        private void nHANVIENBindingNavigatorSaveItem_Click_1(object sender, EventArgs e)
        {
            this.Validate();
            this.bdsNV.EndEdit();
            this.tableAdapterManager.UpdateAll(this.DS);

        }

        private void btnChuyenNV_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            if (chkXoa.Checked == true)
            {
                MessageBox.Show(" Nhân viên này đã được chuyển qua chi nhánh khác!", "ERROR", MessageBoxButtons.OK);
                return;
            }
            String manv = ((DataRowView)bdsNV[bdsNV.Position])["MANV"].ToString().Trim();
            if (manv == Program.username)
            {

                MessageBox.Show("Không thể chuyển nhân viên này vì đang login !", "ERROR", MessageBoxButtons.OK);
                return;
            }


            if (MessageBox.Show("Bạn có chắc chắn muốn chuyển nhân viên này không?", "Xác Nhận", MessageBoxButtons.OKCancel) == DialogResult.OK)
            {
                macn = ((DataRowView)Program.bds_dspm[Program.mChinhanh])["TENCN"].ToString();

                if (macn.Trim().Equals("CN1"))
                {
                    String lenh = "EXEC SP_CHUYENCHINHANH_NV " + "@HO = '" + txtHo.Text.Trim() + "', @TEN = '" + txtTen.Text.Trim() +"', @NGAYSINH = '"+nGAYSINHDateEdit.Text+ "', @SOCMND = '" + txtSOCMND.Text.Trim()+
                            "', @MACN = 'CN2'";
                    int check = Program.ExecSqlNonQuery(lenh);
                    if (check == 0)
                    {
                        MessageBox.Show("Chuyển nhân viên thành công!!!", "", MessageBoxButtons.OK);
                        bdsNV.EndEdit();
                        this.NHANVIENTableAdapter.Fill(this.DS.NhanVien);
                    }
                    else
                    {
                        bdsNV.RemoveCurrent();
                        this.NHANVIENTableAdapter.Connection.ConnectionString = Program.connstr;
                        return;
                    }
                }
                else
                {
                    String lenh = "EXEC SP_CHUYENCHINHANH_NV " + "@HO = '" + txtHo.Text.Trim() + "', @TEN = '" + txtTen.Text.Trim() + "', @NGAYSINH = '"+nGAYSINHDateEdit.Text+ "', @SOCMND = '" + txtSOCMND.Text.Trim() +
                            "', @MACN = 'CN1'";
                    int check = Program.ExecSqlNonQuery(lenh);
                    if (check == 0)
                    {
                        MessageBox.Show("Chuyển nhân viên thành công!!!", "", MessageBoxButtons.OK);
                        bdsNV.EndEdit();
                        this.NHANVIENTableAdapter.Fill(this.DS.NhanVien);
                    }
                    else
                    {
                        bdsNV.RemoveCurrent();
                        this.NHANVIENTableAdapter.Connection.ConnectionString = Program.connstr;
                        return;
                    }
                }

            }
        }
    }
}
