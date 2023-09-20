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
    public partial class frmHangHoa : Form
    {
        int vitri = 0;
        int check = 0;

        public frmHangHoa()
        {
            InitializeComponent();
        }

        private void hANGHOABindingNavigatorSaveItem_Click(object sender, EventArgs e)
        {
            this.Validate();
            this.bdsHH.EndEdit();
            this.tableAdapterManager.UpdateAll(this.DS);

        }

        //private void GetLoaiHH()
        //{
        //    if (Program.mloginDN == "") return;
        //    if (Program.KetNoi() == 0)
        //    {
        //        MessageBox.Show("Lỗi kết nối data", "Thông báo", MessageBoxButtons.OK);
        //        return;
        //    }
        //    DataTable db = new DataTable();
        //    String sql = "SELECT * FROM dbo.V_DS_LOAIHH";
        //    SqlDataAdapter da = new SqlDataAdapter(sql, Program.conn);
        //    da.Fill(db);
        //    bdsLHH.DataSource = db;
        //    cmbMALHH.DataSource = bdsLHH;
        //    cmbMALHH.DisplayMember = "TENLHH";
        //    cmbMALHH.ValueMember = "MALHH";
        //    cmbMALHH.DropDownStyle = ComboBoxStyle.DropDownList;
        //}

      

        private void frmHangHoa_Load(object sender, EventArgs e)
        {
            
            DS.EnforceConstraints = false;
            this.HANGHOATableAdapter.Connection.ConnectionString = Program.connstr;
            this.HANGHOATableAdapter.Fill(this.DS.Vattu);

            this.CT_DONDHTableAdapter.Connection.ConnectionString = Program.connstr;
            this.CT_DONDHTableAdapter.Fill(this.DS.CTDDH);

            this.CT_HOADONTableAdapter.Connection.ConnectionString = Program.connstr;
            this.CT_HOADONTableAdapter.Fill(this.DS.CTPX);

            this.CT_PHIEUNHAPTableAdapter.Connection.ConnectionString = Program.connstr;
            this.CT_PHIEUNHAPTableAdapter.Fill(this.DS.CTPN);

            cboChiNhanh.DataSource = Program.bds_dspm;  //sao chép dspm đã load ở form đăng nhập.
            cboChiNhanh.DisplayMember = "TENCN";
            cboChiNhanh.ValueMember = "TENSERVER";
            cboChiNhanh.SelectedIndex = Program.mChinhanh;
            cboChiNhanh.DropDownStyle = ComboBoxStyle.DropDownList;
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
           // GetLoaiHH();

        }

        private void btnThem_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            panelControl2.Enabled = true;
            vitri = bdsHH.Position;

            bdsHH.AddNew();
            txtMaHH.Enabled = true;
            
            txtMaHH.Focus();
            btnThem.Enabled = btnHieuChinh.Enabled = btnXoa.Enabled = btnTaiLai.Enabled = btnThoat.Enabled = false;
            btnLuu.Enabled = btnPhucHoi.Enabled = true;
            gcHH.Enabled = false;
            check = 1;
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
                this.HANGHOATableAdapter.Connection.ConnectionString = Program.connstr;
                this.HANGHOATableAdapter.Fill(this.DS.Vattu);
            }
        }

        private void btnHieuChinh_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            vitri = bdsHH.Position;
            txtMaHH.Enabled = false;
            panelControl2.Enabled = true;
            btnThem.Enabled = btnXoa.Enabled = btnHieuChinh.Enabled = btnTaiLai.Enabled = btnThoat.Enabled = false;
            btnLuu.Enabled = btnPhucHoi.Enabled = true;
            gcHH.Enabled = false;
            check = 2;
        }

        private void btnLuu_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            if (txtMaHH.Text.Trim() == "")
            {
                MessageBox.Show("Mã hàng hóa không được thiếu!", "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                txtMaHH.Focus();
                return;
            }

            if (tENHHTextEdit.Text.Trim().Length == 0)
            {
                MessageBox.Show("Tên hàng hóa không được trống!", "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                tENHHTextEdit.Focus();
                return;
            }
            if (dVTTextEdit.Text.Trim().Length == 0)
            {
                MessageBox.Show("Đơn vị tính không được trống!", "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                dVTTextEdit.Focus();
                return;
            }
            String sl = sOLUONGTONSpinEdit.Text.Trim().Replace(".", "");
            if (Int32.Parse(sl) < 0)
            {
                MessageBox.Show("Số lượng phải lớn hơn 0", "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                sOLUONGTONSpinEdit.Focus();
                return;
            }
            try
            {
                if (check == 1)
                {
                    string strLenh = "DECLARE @RC INT " +
                               "EXEC @RC = SP_CHECKMAHHEXISTS " +
                               "@MAHH=" + txtMaHH.Text.Trim() + " " +
                                "SELECT 'Result' = @RC";
                    SqlDataReader dataReader = null;
                    dataReader = Program.ExecSqlDataReader(strLenh);
                    // Đọc và lấy 
                    dataReader.Read();
                    int result = int.Parse(dataReader.GetValue(0).ToString());
                    dataReader.Close();
                    if (result == 1)
                    {
                        MessageBox.Show("Mã hàng này đã tồn tại!", "Thông báo",
                        MessageBoxButtons.OK, MessageBoxIcon.Warning);
                        txtMaHH.Focus();
                        return;
                    }
                }
                bdsHH.EndEdit();    // kết thúc quá trình tạo. -> Ghi vào trong bds.
                bdsHH.ResetCurrentItem();   //Đưa những thông tin đó lên lưới.
                this.HANGHOATableAdapter.Connection.ConnectionString = Program.connstr;
                this.HANGHOATableAdapter.Update(this.DS.Vattu); // Update trên adapter có 3 nghĩa: vừa là insert, update, delete. Nó tùy vào tình huống cụ thể để đưa lệnh tương ứng.
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
            gcHH.Enabled = true;
            btnThem.Enabled = btnXoa.Enabled = btnHieuChinh.Enabled = btnTaiLai.Enabled = btnThoat.Enabled = true;
            btnLuu.Enabled = btnPhucHoi.Enabled = false;
            txtMaHH.Enabled = true;
            panelControl2.Enabled = false;
        }

        private void btnPhucHoi_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            bdsHH.CancelEdit();
            this.HANGHOATableAdapter.Fill(this.DS.Vattu);
            if (btnThem.Enabled == false) bdsHH.Position = vitri;
            gcHH.Enabled = true;
            panelControl2.Enabled = false;
            btnThem.Enabled = btnHieuChinh.Enabled = btnXoa.Enabled = btnTaiLai.Enabled = btnThoat.Enabled = true;
            btnLuu.Enabled = btnPhucHoi.Enabled = false;
            txtMaHH.Enabled = true;
        }

        private void btnXoa_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            String mahh = ((DataRowView)bdsHH[bdsHH.Position])["MAVT"].ToString();
            if (bdsCT_DONDH.Count > 0)
            {
                MessageBox.Show("Không thể xóa mặt hàng này!!!", "",
                    MessageBoxButtons.OK);
                return;
            }
            if (MessageBox.Show("Bạn có thật sự muốn xóa mặt hàng này ??", "Xác nhận",
               MessageBoxButtons.OKCancel) == DialogResult.OK)
            {
                try
                {
                    // Giữ lại cmnd hiện tại đang đứng để nếu xóa ở máy hiện tại thành công mà xóa ở CSDL thất bại thì ta sẽ fill data về lại máy và nhờ cmnd đó thì con trỏ nó sẽ nhảy đến cmnd vừa xóa.
                    bdsHH.RemoveCurrent();  // Xóa trên máy hiện tại trước, sau đó mới xóa trên CSDL sau.
                    this.HANGHOATableAdapter.Connection.ConnectionString = Program.connstr;
                    this.HANGHOATableAdapter.Update(this.DS.Vattu); // xóa dữ liệu đó ở CSDL.
                    this.HANGHOATableAdapter.Fill(this.DS.Vattu);
                }
                catch (Exception ex)
                {
                    MessageBox.Show("Lỗi xóa hàng hóa. Bạn hãy xóa lại\n" + ex.Message, "",
                        MessageBoxButtons.OK);
                    this.HANGHOATableAdapter.Fill(this.DS.Vattu);   // Trường hợp xóa ở máy hiện tại thành công nhưng xóa trên CSDL bị lỗi thì ta phải tải về lại máy hiện tại.
                    bdsHH.Position = bdsHH.Find("MAVT", mahh);  // đưa con trỏ nhảy đến vị trí manv đã xóa thất bại trước đó.
                    return;
                }
            }
            if (bdsHH.Count == 0) btnXoa.Enabled = false;
        }

        private void btnTaiLai_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            try
            {
                this.HANGHOATableAdapter.Fill(this.DS.Vattu);   // tải lại Khách hàng.
            }
            catch (Exception ex)
            {
                MessageBox.Show("Lỗi Tải lại danh sách hàng hóa: " + ex.Message, "", MessageBoxButtons.OK);
                return;
            }
        }

        private void btnThoat_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            this.Close();
        }
    }
}
