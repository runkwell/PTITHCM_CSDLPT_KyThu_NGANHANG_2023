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
    public partial class frmCT_DDH : Form
    {
        int vitri = 0;
        int check = 0;
        public frmCT_DDH()
        {
            InitializeComponent();
        }

        private void cT_DONDHBindingNavigatorSaveItem_Click(object sender, EventArgs e)
        {
            this.Validate();
            this.bdsCT_DDH.EndEdit();
            this.tableAdapterManager.UpdateAll(this.DS);

        }

        private void frmCT_DDH_Load(object sender, EventArgs e)
        {
            
            DS.EnforceConstraints = false;
            this.hANGHOATableAdapter.Connection.ConnectionString = Program.connstr;
            this.hANGHOATableAdapter.Fill(this.DS.Vattu);

            this.cT_DONDHTableAdapter.Connection.ConnectionString = Program.connstr;
            this.cT_DONDHTableAdapter.Fill(this.DS.CTDDH);

            bdsCT_DDH.Filter = "MasoDDH ='" + Program.maddh + "'";
            gcCT_DDH.DataSource = bdsCT_DDH;
            txtTT.Enabled = false;
            txtMaDDH.Text = Program.maddh;
            
            panelControl2.Enabled = false;
            if (Program.mGroup == "CONGTY")
            {

                btnThem.Enabled = btnHieuChinh.Enabled = btnXoa.Enabled = btnLuu.Enabled = btnPhucHoi.Enabled = false;
            }
            else
            {

                btnThem.Enabled = btnHieuChinh.Enabled = btnXoa.Enabled = btnLuu.Enabled = btnPhucHoi.Enabled = true;
            }

        }


        private void btnTT_Click(object sender, EventArgs e)
        {
            int tt = 0;
            int rt = 0;
            for (int i =0; i< gridView1.DataRowCount; i++)
            {
                rt = int.Parse(gridView1.GetRowCellValue(i,"SOLUONG").ToString()) * int.Parse(gridView1.GetRowCellValue(i, "DONGIA").ToString());
                tt = tt + rt;
            }
            txtTT.Text = string.Format("{0:N0}", tt);
        }

        private void btnThem_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            
            vitri = bdsCT_DDH.Position;
            panelControl2.Enabled = true;
            bdsCT_DDH.AddNew();
            txtMaHH.Enabled = true;
            txtMaHH.Focus();
            btnThem.Enabled = btnHieuChinh.Enabled = btnXoa.Enabled = btnTT.Enabled  = false;
            btnLuu.Enabled = btnPhucHoi.Enabled = true;
            gcCT_DDH.Enabled = false;
            txtMaDDH.Text = Program.maddh;
            txtMaDDH.Enabled = false;
            check = 1;
            
        }

        private void btnHieuChinh_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            vitri = bdsCT_DDH.Position;
            txtMaHH.Enabled = true;
            txtMaHH.Focus();
            panelControl2.Enabled = true;
            btnThem.Enabled = btnXoa.Enabled = btnHieuChinh.Enabled = btnTT.Enabled = false;
            btnLuu.Enabled = btnPhucHoi.Enabled = true;
            gcCT_DDH.Enabled = false;
            txtMaDDH.Enabled = false;
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
            string dg1 = dONGIASpinEdit.Text.Trim().Replace(",", "");
            string dg = dg1.Trim().Replace(".", "");
            String sl1 = sOLUONGSpinEdit.Text.Trim().Replace(",", "");
            String sl = sl1.Trim().Replace(".", "");
            if (Int32.Parse(dg) < 0)
            {
                MessageBox.Show("Đơn giá phải lớn hơn 0", "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                dONGIASpinEdit.Focus();
                return;
            }

            if (Int32.Parse(sl) < 0)
            {
                MessageBox.Show("Số lượng phải lớn hơn 0", "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                sOLUONGSpinEdit.Focus();
                return;
            }

            try
            {
                if (check == 1)
                {
                    string strLenh = "DECLARE @RC INT " +
                               "EXEC @RC = SP_Kiem_Tra_Trang_Thai_Vat_Tu_DDH " +
                               "@MA=" + txtMaDDH.Text.Trim() + ", @MAHH=" + txtMaHH.Text.Trim() + " " +
                                "SELECT 'Result' = @RC";
                    SqlDataReader dataReader = null;
                    dataReader = Program.ExecSqlDataReader(strLenh);
                    // Đọc và lấy 
                    dataReader.Read();
                    int result = int.Parse(dataReader.GetValue(0).ToString());
                    dataReader.Close();
                    if (result == 1)
                    {
                        MessageBox.Show("MAHH này đã có trong đơn đặt hàng!", "Thông báo",
                        MessageBoxButtons.OK, MessageBoxIcon.Warning);
                        txtMaHH.Focus();
                        return;
                    }
                    else if (result == 2)
                    {
                        MessageBox.Show("MAHH này không tồn tại!", "Thông báo",
                        MessageBoxButtons.OK, MessageBoxIcon.Warning);
                        txtMaHH.Focus();
                        return;
                    }
                }
                bdsCT_DDH.EndEdit();    // kết thúc quá trình tạo. -> Ghi vào trong bds.
                bdsCT_DDH.ResetCurrentItem();   //Đưa những thông tin đó lên lưới.
                this.cT_DONDHTableAdapter.Connection.ConnectionString = Program.connstr;
                this.cT_DONDHTableAdapter.Update(this.DS.CTDDH); // Update trên adapter có 3 nghĩa: vừa là insert, update, delete. Nó tùy vào tình huống cụ thể để đưa lệnh tương ứng.
                this.hANGHOATableAdapter.Fill(this.DS.Vattu);
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
            gcCT_DDH.Enabled = true;
            btnThem.Enabled = btnXoa.Enabled = btnHieuChinh.Enabled = btnTT.Enabled = true;
            btnLuu.Enabled = btnPhucHoi.Enabled = false;
            panelControl2.Enabled = false;
        }

        private void btnXoa_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            if (MessageBox.Show("Bạn có thật sự muốn xóa DDH này ??", "Xác nhận",
               MessageBoxButtons.OKCancel) == DialogResult.OK)
            {
                try
                {
                    // Giữ lại cmnd hiện tại đang đứng để nếu xóa ở máy hiện tại thành công mà xóa ở CSDL thất bại thì ta sẽ fill data về lại máy và nhờ cmnd đó thì con trỏ nó sẽ nhảy đến cmnd vừa xóa.
                    bdsCT_DDH.RemoveCurrent();  // Xóa trên máy hiện tại trước, sau đó mới xóa trên CSDL sau.
                    this.cT_DONDHTableAdapter.Connection.ConnectionString = Program.connstr;
                    this.cT_DONDHTableAdapter.Update(this.DS.CTDDH); // xóa dữ liệu đó ở CSDL.
                    this.cT_DONDHTableAdapter.Fill(this.DS.CTDDH);
                }
                catch (Exception ex)
                {
                    MessageBox.Show("Lỗi xóa . Bạn hãy xóa lại\n" + ex.Message, "",
                        MessageBoxButtons.OK);
                    this.cT_DONDHTableAdapter.Fill(this.DS.CTDDH);   // Trường hợp xóa ở máy hiện tại thành công nhưng xóa trên CSDL bị lỗi thì ta phải tải về lại máy hiện tại.
                    return;
                }
            }
            if (bdsCT_DDH.Count == 0) btnXoa.Enabled = false;
        }

        private void btnPhucHoi_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            bdsCT_DDH.CancelEdit();
            this.cT_DONDHTableAdapter.Fill(this.DS.CTDDH);
            if (btnThem.Enabled == false) bdsCT_DDH.Position = vitri;
            gcCT_DDH.Enabled = true;
            panelControl2.Enabled = false;
            btnThem.Enabled = btnHieuChinh.Enabled = btnXoa.Enabled = btnTT.Enabled  = true;
            btnLuu.Enabled = btnPhucHoi.Enabled = false;
        }

        private void cTDDHBindingNavigatorSaveItem_Click(object sender, EventArgs e)
        {
            this.Validate();
            this.bdsCT_DDH.EndEdit();
            this.tableAdapterManager.UpdateAll(this.DS);

        }
    }
}
