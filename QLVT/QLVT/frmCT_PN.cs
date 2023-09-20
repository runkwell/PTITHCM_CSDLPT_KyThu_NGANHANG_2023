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
    public partial class frmCT_PN : Form
    {
        int vitri = 0;
        int check = 0;
        public frmCT_PN()
        {
            InitializeComponent();
        }

        private void cT_PHIEUNHAPBindingNavigatorSaveItem_Click(object sender, EventArgs e)
        {
            this.Validate();
            this.bdsCT_PN.EndEdit();
            this.tableAdapterManager.UpdateAll(this.DS);

        }

        private void frmCT_PN_Load(object sender, EventArgs e)
        {
            
            DS.EnforceConstraints = false;
            this.cT_DONDHTableAdapter.Connection.ConnectionString = Program.connstr;
            this.cT_DONDHTableAdapter.Fill(this.DS.CTDDH);

            this.cT_PHIEUNHAPTableAdapter.Connection.ConnectionString = Program.connstr;
            this.cT_PHIEUNHAPTableAdapter.Fill(this.DS.CTPN);

            bdsCT_DDH.Filter = "MasoDDH ='" + Program.maddh + "'";
            bdsCT_PN.Filter = "MAPN ='" + Program.sopn + "'";
            gcCT_DDH.DataSource = bdsCT_DDH;
            gcCT_PN.DataSource = bdsCT_PN;
            txtTTPN.Enabled = false;
            txtTTDDH.Enabled = false;
            txtSoPN.Text = Program.sopn;
            if (Program.mGroup == "CONGTY")
            {

                btnThem.Enabled = btnHieuChinh.Enabled = btnXoa.Enabled = btnLuu.Enabled = btnPhucHoi.Enabled = false;
            }
            else
            {
                btnThem.Enabled = btnHieuChinh.Enabled = btnXoa.Enabled = btnLuu.Enabled = btnPhucHoi.Enabled = true;
            }
            panelControl2.Enabled = false;
        }

        private void label1_Click(object sender, EventArgs e)
        {

        }

        private void btnThem_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            vitri = bdsCT_PN.Position;
            panelControl2.Enabled = true;
            bdsCT_PN.AddNew();
            txtMaHH.Enabled =  true;
            txtMaHH.Focus();
            btnThem.Enabled  = btnTTPN.Enabled = btnTTDDH.Enabled = false;
            btnLuu.Enabled = btnPhucHoi.Enabled = true;
            gcCT_PN.Enabled = false;
            txtSoPN.Text = Program.sopn;
            txtSoPN.Enabled = false;
            check = 1;
        }

        private void txtTTPN_TextChanged(object sender, EventArgs e)
        {

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
            String sl1 = txtSL.Text.Trim().Replace(",", "");
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
                txtSL.Focus();
                return;
            }


            try
            {
                if (check == 1)
                {
                    SqlDataReader dataReader = null;
                    string strLenh = "DECLARE @RC INT " +
                               "EXEC @RC = SP_Kiem_Tra_Trang_Thai_Vat_Tu " +
                               "@MA=" + txtSoPN.Text.Trim() + ", @MAHH=" + txtMaHH.Text.Trim() + " " + 
                                "SELECT 'Result' = @RC";
                    
                    dataReader = Program.ExecSqlDataReader(strLenh);
                    // Đọc và lấy 
                    dataReader.Read();
                    int result = int.Parse(dataReader.GetValue(0).ToString());
                    dataReader.Close();
                    if (result == 1)
                    {
                        MessageBox.Show("MAHH này đã có trong phiếu nhập!", "Thông báo",
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

                string Lenh = "DECLARE @RC INT " + 
                               "EXEC @RC = sp_addCT_PN " +
                               "@MADDH=" + Program.maddh + ", @MAHH=" + txtMaHH.Text.Trim() + " " + ", @SOLUONG=" + txtSL.Text.Trim() +
                                "SELECT 'Result' = @RC";
                int checkpn = Program.ExecSqlNonQuery(Lenh);
                if (checkpn == 0)
                {
                    MessageBox.Show("Thêm thành công!", "", MessageBoxButtons.OK);
                    bdsCT_PN.EndEdit();    // kết thúc quá trình tạo. -> Ghi vào trong bds.
                    bdsCT_PN.ResetCurrentItem();   //Đưa những thông tin đó lên lưới.
                    this.cT_PHIEUNHAPTableAdapter.Connection.ConnectionString = Program.connstr;
                    this.cT_PHIEUNHAPTableAdapter.Update(this.DS.CTPN);
                    this.cT_DONDHTableAdapter.Fill(this.DS.CTDDH);
                    check = 0;
                    gcCT_PN.Enabled = true;
                    btnThem.Enabled  = btnTTPN.Enabled = btnTTDDH.Enabled = true;
                    btnLuu.Enabled = btnPhucHoi.Enabled = false;
                    panelControl2.Enabled = false;
                    return;
                }
                else
                {
                    return;
                }


            }
            catch (Exception ex)
            {
                MessageBox.Show("Lỗi: " + ex.Message, "", MessageBoxButtons.OK);
                return;
            }
            
        }



        private void btnPhucHoi_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {

            bdsCT_PN.CancelEdit();
            this.cT_PHIEUNHAPTableAdapter.Fill(this.DS.CTPN);
            if (btnThem.Enabled == false) bdsCT_DDH.Position = vitri;
            gcCT_PN.Enabled = true;
            panelControl2.Enabled = false;
            btnThem.Enabled = btnHieuChinh.Enabled = btnXoa.Enabled = btnTTPN.Enabled = btnTTDDH.Enabled = true;
            btnLuu.Enabled = btnPhucHoi.Enabled = false;
        }

        private void btnTTPN_Click(object sender, EventArgs e)
        {
            int tt = 0;
            int rt = 0;
            for (int i = 0; i < gridView1.DataRowCount; i++)
            {
                rt = int.Parse(gridView1.GetRowCellValue(i, "SOLUONG").ToString()) * int.Parse(gridView1.GetRowCellValue(i, "DONGIA").ToString());
                tt = tt + rt;
            }
            txtTTPN.Text = string.Format("{0:N0}", tt);
        }

        private void btnTTDDH_Click(object sender, EventArgs e)
        {
            int tt = 0;
            int rt = 0;
            for (int i = 0; i < gridView2.DataRowCount; i++)
            {
                rt = int.Parse(gridView2.GetRowCellValue(i, "SOLUONG").ToString()) * int.Parse(gridView2.GetRowCellValue(i, "DONGIA").ToString());
                tt = tt + rt;
            }
            txtTTDDH.Text = string.Format("{0:N0}", tt);
        }

        
    }
}
