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
    public partial class frmDangNhap : Form
    {
        private static SqlConnection conn_publisher = new SqlConnection();
        public string nhom = "";
        public frmDangNhap()
        {
            InitializeComponent();
        }

        public static int KetNoi_CSDLGOC()
        {
            if (conn_publisher != null && conn_publisher.State == System.Data.ConnectionState.Open)
                conn_publisher.Close();   // Khi ta mở sever và tải dữ liệu về thì trong vòng từ 5-10s nó sẽ tự đóng -> trong trường hợp ta kiểm tra mà sever vẫn mở nhưng khi tải dữ liệu về thì nó sẽ tự động đóng gây ra lỗi. 
            try
            {
                conn_publisher.ConnectionString = Program.connstr_publisher;   // gán Tên sever + tên DB từ connstr_publisher vào ConnectionString.
                conn_publisher.Open();
                return 1;
            }

            catch (Exception e)
            {
                MessageBox.Show("Lỗi kết nối về cơ sở dữ liệu gốc.\nBạn xem lại Tên sever của publisher và tên CSDL trong chuỗi kết nối.\n " + e.Message, "", MessageBoxButtons.OK);
                return 0;
            }
        }

        private void btnThoat_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void btnDangNhap_Click(object sender, EventArgs e)
        {

                if (txtLogin.Text.Trim() == "" || txtPass.Text.Trim() == "")
                {
                    MessageBox.Show("Login name và mật mā không được trống", "", MessageBoxButtons.OK);
                    return;
                }

                Program.mlogin = txtLogin.Text;
                Program.password = txtPass.Text;
                if (Program.KetNoi() == 0) return;

                string strLenh = "EXEC SP_Lay_Thong_Tin_NV_Tu_Login '" + Program.mlogin + "'";

                Program.myReader = Program.ExecSqlDataReader(strLenh);

                if (Program.myReader == null || !Program.myReader.HasRows)
                {
                    MessageBox.Show("Bạn không có quyền truy cập!", "", MessageBoxButtons.OK);
                    return;   //null-> không lấy đc ds nv
                }
                Program.myReader.Read();    // Khi thực thi xong SP_Lay_Thong... thì nó chỉ trả ra 1 hàng nên ta chỉ cần Read() 1 lần. Nếu nhiều hàng thì ta phải tạo ra một vòng lặp và lặp cho đến khi Read()==null để lấy ra.
                Program.username = Program.myReader.GetInt32(0) + "";
                if (Convert.IsDBNull(Program.username))
                {
                    MessageBox.Show("Login bạn nhập không có quyền truy cập dữ liệu\n Bạn xem lại username, password", "", MessageBoxButtons.OK);
                    return;
                }
                Program.mChinhanh = cmbChiNhanh.SelectedIndex;
                Program.mloginDN = Program.mlogin;
                Program.passwordDN = Program.password;
                //MessageBox.Show(Program.myReader.GetString(1), "", MessageBoxButtons.OK);
                Program.mHoten = Program.myReader.GetString(1);
                Program.mGroup = Program.myReader.GetString(2);
                Program.myReader.Close();
                Program.conn.Close();
                Program.frmChinh.MANV.Text = "Mã NV = " + Program.username;
                Program.frmChinh.HOTEN.Text = "Họ tên = " + Program.mHoten;
                Program.frmChinh.NHOM.Text = "Nhóm = " + Program.mGroup;
                nhom = Program.mGroup;
                showMenu();
                Close();
           
        }

        private void showMenu()
        {
            if(nhom == "CONGTY")
            {
                Program.frmChinh.btnDangNhap.Enabled = false;
                Program.frmChinh.btnDonDH.Enabled = false;
                Program.frmChinh.btnHangHoa.Enabled = false;
                Program.frmChinh.btnPN.Enabled = false;
                Program.frmChinh.btnPX.Enabled = false;
                Program.frmChinh.barButtonItem6.Enabled = true;
                Program.frmChinh.barButtonItem5.Enabled = true;
                Program.frmChinh.ribbonPage2.Visible = true;
                Program.frmChinh.ribbonPage3.Visible = true;
                Program.frmChinh.ribbonPage4.Visible = true;
                Program.frmChinh.Visible = true;
            }
            else
            {
                Program.frmChinh.btnDangNhap.Enabled = false;
                Program.frmChinh.btnDonDH.Enabled = true;
                Program.frmChinh.btnHangHoa.Enabled = true;
                Program.frmChinh.btnPN.Enabled = true;
                Program.frmChinh.btnPX.Enabled = true;
                Program.frmChinh.barButtonItem6.Enabled = true;
                Program.frmChinh.barButtonItem5.Enabled = true;
                Program.frmChinh.ribbonPage2.Visible = true;
                Program.frmChinh.ribbonPage3.Visible = true;
                Program.frmChinh.ribbonPage4.Visible = true;
                Program.frmChinh.Visible = true;
            }
            
            this.Close();
        }

        private void LayDSPM(String cmd)
        {
            DataTable dt = new DataTable();
            if (conn_publisher.State == ConnectionState.Closed) conn_publisher.Open();
            SqlDataAdapter da = new SqlDataAdapter(cmd, conn_publisher); // Tạo ra một đối tượng thuộc lớp SqlDataAdapter có 2 tham số là chuỗi lệnh và đối tượng SqlConnection.
            da.Fill(dt);    // Muốn tải số liệu từ View,Table từ DataAdapter vào DataTable thì ta dùng Fill -> dt sẽ chứa các danh sách phân mảnh.
            conn_publisher.Close();

            Program.bds_dspm = new BindingSource();
            Program.bds_dspm.DataSource = dt;   // ta gán dspm đó cho bds_dspm ở Program.    
            // Liên kết số liệu bds_dspm với cmd

            cmbChiNhanh.DataSource = Program.bds_dspm;  // gán bds_dspm ở Program cho DataSource ở cmbChiNhanh. //
            cmbChiNhanh.DisplayMember = "TENCN"; cmbChiNhanh.ValueMember = "TENSERVER";
            cmbChiNhanh.DropDownStyle = ComboBoxStyle.DropDownList;
        }
        private void LayDSPM1(String cmd)
        {
            DataTable dt1 = new DataTable();
            if (conn_publisher.State == ConnectionState.Closed) conn_publisher.Open();
            SqlDataAdapter da1 = new SqlDataAdapter(cmd, conn_publisher); // Tạo ra một đối tượng thuộc lớp SqlDataAdapter có 2 tham số là chuỗi lệnh và đối tượng SqlConnection.
            da1.Fill(dt1);    // Muốn tải số liệu từ View,Table từ DataAdapter vào DataTable thì ta dùng Fill -> dt sẽ chứa các danh sách phân mảnh.
            conn_publisher.Close();

            Program.bds_dspm1 = new BindingSource();
            Program.bds_dspm1.DataSource = dt1;   // ta gán dspm đó cho bds_dspm ở Program.    
            // Liên kết số liệu bds_dspm với cmd

        }

        private void cmbChiNhanh_SelectedIndexChanged(object sender, EventArgs e)
        {
            try
            {
                Program.servername = cmbChiNhanh.SelectedValue.ToString();
                // Lấy Value Member gán vào severname của Program.
                // Trong Value Member thuộc tính chứa giá trị trên đó -> SelectedValue
                // Trong Display Member thuộc tính chứa giá trị trên đó -> Text
            }
            catch (Exception) { }
        }

        private void frmDangNhap_Load(object sender, EventArgs e)
        {
            if (KetNoi_CSDLGOC() == 0) return; // nếu hàm KetNoi_CSDLGOC() == 0 -> đăng nhập thất bại
            LayDSPM("SELECT * FROM V_DS_PHANMANH");
            LayDSPM1("SELECT * FROM V_DS_PHANMANH1");// Lấy ra danh sách các phân mảnh từ V_Get_Subscribles.
            cmbChiNhanh.SelectedIndex = 1;
            cmbChiNhanh.SelectedIndex = 0;
            txtLogin.Focus();
        }


    }
}
