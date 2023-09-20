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
    public partial class frmCT_PX : Form
    {
        int vitri = 0;
        int check = 0;
        public frmCT_PX()
        {
            InitializeComponent();
        }

        private void cT_HOADONBindingNavigatorSaveItem_Click(object sender, EventArgs e)
        {
            this.Validate();
            this.bdsCT_HD.EndEdit();
            this.tableAdapterManager.UpdateAll(this.DS);

        }

        private void frmCT_HD_Load(object sender, EventArgs e)
        {
            
            DS.EnforceConstraints = false;
            this.hANGHOATableAdapter.Connection.ConnectionString = Program.connstr;
            this.hANGHOATableAdapter.Fill(this.DS.Vattu);

            this.cT_HOADONTableAdapter.Connection.ConnectionString = Program.connstr;
            this.cT_HOADONTableAdapter.Fill(this.DS.CTPX);

            bdsCT_HD.Filter = "MAPX ='" + Program.sohd + "'";
            gcCT_HD.DataSource = bdsCT_HD;
            txtTT.Enabled = false;
            txtSoHD.Text = Program.sohd;
            if (Program.mGroup == "CONGTY")
            {

                btnThem.Enabled  = btnLuu.Enabled = btnPhucHoi.Enabled = false;
            }
            else
            {

                btnThem.Enabled  = btnLuu.Enabled = btnPhucHoi.Enabled = true;
            }
            panelControl2.Enabled = false;
        }

        private void btnThem_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            vitri = bdsCT_HD.Position;
            panelControl2.Enabled = true;
            bdsCT_HD.AddNew();
            txtMaHH.Enabled = true;
            txtMaHH.Focus();
            btnThem.Enabled  = btnTT.Enabled = false;
            btnLuu.Enabled = btnPhucHoi.Enabled = true;
            gcCT_HD.Enabled = false;
            txtSoHD.Text = Program.sohd;
            txtSoHD.Enabled = false;
            check = 1;
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
            string dg = dg1.Replace(".", "");
            
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
                               "EXEC @RC = SP_Kiem_Tra_Trang_Thai_Vat_Tu_px " +
                               "@MA=" + txtSoHD.Text.Trim() + ", @MAHH=" + txtMaHH.Text.Trim() + " " +
                                "SELECT 'Result' = @RC";

                    dataReader = Program.ExecSqlDataReader(strLenh);
                    // Đọc và lấy 
                    dataReader.Read();
                    int result = int.Parse(dataReader.GetValue(0).ToString());
                    dataReader.Close();
                    if (result == 1)
                    {
                        MessageBox.Show("MAHH này đã có trong phiếu xuất!", "Thông báo",
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
                               "EXEC @RC = sp_subCT_HD " + "@MAHH=" + txtMaHH.Text.Trim() + " " + ", @SOLUONG=" + txtSL.Text.Trim() +
                                " SELECT 'Result' = @RC";
                int checkpn = Program.ExecSqlNonQuery(Lenh);
                if (checkpn == 0)
                {
                    MessageBox.Show("Thêm thành công!", "", MessageBoxButtons.OK);
                    bdsCT_HD.EndEdit();    // kết thúc quá trình tạo. -> Ghi vào trong bds.
                    bdsCT_HD.ResetCurrentItem();   //Đưa những thông tin đó lên lưới.
                    this.cT_HOADONTableAdapter.Connection.ConnectionString = Program.connstr;
                    this.cT_HOADONTableAdapter.Update(this.DS.CTPX);
                    this.hANGHOATableAdapter.Fill(this.DS.Vattu);
                    check = 0;
                    gcCT_HD.Enabled = true;
                    btnThem.Enabled  = btnTT.Enabled = true;
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
            bdsCT_HD.CancelEdit();
            this.cT_HOADONTableAdapter.Fill(this.DS.CTPX);
            if (btnThem.Enabled == false) bdsCT_HD.Position = vitri;
            gcCT_HD.Enabled = true;
            panelControl2.Enabled = false;
            btnThem.Enabled = btnTT.Enabled = true;
            btnLuu.Enabled = btnPhucHoi.Enabled = false;
        }

        private void btnTT_Click(object sender, EventArgs e)
        {
            int tt = 0;
            int rt = 0;
            for (int i = 0; i < gridView1.DataRowCount; i++)
            {
                rt = int.Parse(gridView1.GetRowCellValue(i, "SOLUONG").ToString()) * int.Parse(gridView1.GetRowCellValue(i, "DONGIA").ToString());
                tt = tt + rt;
            }
            txtTT.Text = string.Format("{0:N0}", tt);
        }
    }
}
