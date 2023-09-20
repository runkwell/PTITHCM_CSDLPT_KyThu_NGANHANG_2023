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

namespace QLVT
{
    public partial class frmTaoTKNV : Form
    {
        public frmTaoTKNV()
        {
            InitializeComponent();
        }

        private void btLuu_Click(object sender, EventArgs e)
        {
            if (txtLoginName.Text.Trim() == "")
            {
                MessageBox.Show("Tên đăng nhập không được để trống", "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                txtLoginName.Focus();
                return;
            }
            if (txtPass.Text.Trim() == "")
            {
                MessageBox.Show("Password không được để trống", "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                txtPass.Focus();
                return;
            }
            try
            {
                String userName = cmbNhanVien.SelectedValue.ToString();
                string strLenh = "DECLARE @RC INT " +
                          "EXEC @RC = SP_TaoLogin " +
                          "@LGNAME= " + txtLoginName.Text + ", " +
                          "@PASS = " + txtPass.Text + ", " +
                          "@USERNAME = " + userName + ", " +
                          "@ROLE = " + txtRole.Text + " " +
                          "SELECT 'Result' = @RC";

                SqlDataReader dataReader = null;
                dataReader = Program.ExecSqlDataReader(strLenh);
                // Đọc và lấy 
                dataReader.Read();
                int result = int.Parse(dataReader.GetValue(0).ToString());
                dataReader.Close();

                if (result == 0)
                {
                    MessageBox.Show("Thêm thành công!", "Thông báo",
                    MessageBoxButtons.OK);
                    cmbNhanVien.Refresh();
                    return;
                }
                else if (result == 1)
                {
                    MessageBox.Show("Login name này đã có người sử dụng!", "Thông báo",
                    MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    txtLoginName.Focus();
                    return;
                }
                else if (result == 2)
                {
                    MessageBox.Show("Người này đã có tài khoản!", "Thông báo",
                    MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    cmbNhanVien.Focus();
                    return;
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Lỗi ghi tài khoản\n" + ex.Message, "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }
        }

        private void nHANVIENBindingNavigatorSaveItem_Click1(object sender, EventArgs e)
        {
            this.Validate();
            this.bdsNV.EndEdit();
            this.tableAdapterManager.UpdateAll(this.DS);

        }

        private void frmTaoTKNV_Load(object sender, EventArgs e)
        {
            DS.EnforceConstraints = false;
            this.NHANVIENTableAdapter.Connection.ConnectionString = Program.connstr;
            this.NHANVIENTableAdapter.Fill(this.DS.NhanVien);

            if (DS.NhanVien.Count == 0)
            {
                MessageBox.Show("Không tìm thấy bất kì nhân viên nào");
                Close();
                return;
            }
            txtRole.Text = Program.mGroup;
            GetNV();
        }
        private void GetNV()
        {
            if (Program.mloginDN == "") return;
            if (Program.KetNoi() == 0)
            {
                MessageBox.Show("Lỗi kết nối data", "Thông báo", MessageBoxButtons.OK);
                return;
            }
            DataTable db = new DataTable();
            String sql = "SELECT * FROM dbo.V_DS_NHANVIEN";
            SqlDataAdapter da = new SqlDataAdapter(sql, Program.conn);
            da.Fill(db);
            bdsNV.DataSource = db;
            cmbNhanVien.DataSource = bdsNV;
            cmbNhanVien.DisplayMember = "HOTEN";
            cmbNhanVien.ValueMember = "MANV";
        }

        private void btThoat_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void cmbNhanVien_SelectedIndexChanged(object sender, EventArgs e)
        {

        }
    }
}
