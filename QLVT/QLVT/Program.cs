using DevExpress.Skins;
using DevExpress.UserSkins;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows.Forms;
using System.Data;
using System.Data.SqlClient;

namespace QLVT
{
    static class Program
    {
        /// <summary>
        /// The main entry point for the application.
        /// </summary>

        public static SqlConnection conn = new SqlConnection(); //SqlConnection class thuộc namespace System.Data.SqlClient và được sử dụng để kết nối mở đến CSDL SQL Server.
        public static String connstr;
        public static String connstr_publisher = "Data Source=DESKTOP-8V1JQRF;Initial Catalog=QLVT_DATHANG;Integrated Security=True";

        public static SqlDataReader myReader;
        public static String servername = "";
        public static String username = ""; // chứa mã nhân viên.
        public static String mlogin = "";
        public static String password = "";

        public static String database = "QLVT_DATHANG";
        public static String remotelogin = "HTKN";
        public static String remotepassword = "123";
        public static String mloginDN = ""; //chứa tài khoản đăng nhập thành công. Dùng cho những form sau này.
        public static String passwordDN = ""; // chứa mật khẩu đăng nhập thành công.
        public static String mGroup = "";
        public static String mHoten = "abc";
        public static int mChinhanh = 0;    // đăng nhập thành công thuộc chi nhánh nào.

        public static string maddh = "";
        public static string sopn = "";
        public static string sohd = "";

        public static BindingSource bds_dspm = new BindingSource(); //giữ bds phân mảnh khi đăng nhập -> chứa TENCN và TENSEVER của V_Get_Subscribes. Từ lúc đăng nhập thành công đến lúc kết thúc.
        public static BindingSource bds_dspm1 = new BindingSource();
        public static frmMain frmChinh;

        public static int KetNoi()
        {
            if (Program.conn != null && Program.conn.State == System.Data.ConnectionState.Open)
                Program.conn.Close();
            try
            {
                Program.connstr = "Data Source=" + Program.servername + ";Initial Catalog=" + Program.database + ";User ID=" +
                      Program.mlogin + ";password=" + Program.password;
                Program.conn.ConnectionString = Program.connstr;
                Program.conn.Open();
                return 1;
            }

            catch (Exception e)
            {
                MessageBox.Show("Lỗi kết nối cơ sở dữ liệu.\nBạn xem lại user name và password.\n " + e.Message, "", MessageBoxButtons.OK);
                return 0;
            }
        }
        // ExecuteNonQuery() thi hành truy vấn - không cần trả về dữ liệu gì, phù hợp thực hiện các
        //   truy vấn như Update, Delete ...
        // ExecuteReader() thi hành lệnh - trả về đối tượng giao diện IDataReader như SqlDataReader,
        //   từ đó đọc được dữ liệu trả về
        // ExecuteScalar() thi hành và trả về một giá trị duy nhất - ở hàng đầu tiên, cột đầu tiên

        public static SqlDataReader ExecSqlDataReader(String strLenh)
        { // thực thi câu lệnh và trả về dưới dạng DataReader.
            SqlDataReader myreader;
            SqlCommand sqlcmd = new SqlCommand(strLenh, Program.conn);
            sqlcmd.CommandType = CommandType.Text;
            if (Program.conn.State == ConnectionState.Closed) Program.conn.Open();
            try
            {
                myreader = sqlcmd.ExecuteReader();
                return myreader;
            }
            catch (SqlException ex)
            {
                Program.conn.Close();
                MessageBox.Show(ex.Message);
                return null;
            }
        }

        public static DataTable ExecsqlDataTable(String cmd)
        {
            DataTable dt = new DataTable();
            if (Program.conn.State == ConnectionState.Closed) Program.conn.Open();  //Khởi tạo một instance mới của class SqlDataAdapter bằng một lệnh và một chuỗi kết nối.đây là kho lưu trữ dữ liệu trong bộ nhớ. Có thể lưu trữ các bảng giống như một cơ sở dữ liệu.
            SqlDataAdapter da = new SqlDataAdapter(cmd, conn);  //SqlDataAdapter trong C# hoạt động như một cầu nối giữa DataSet và CSDL SQL Server để truy xuất dữ liệu. 
            da.Fill(dt);    // Giống với DataReader nhưng khác một chỗ là ta tải về bằng Fill.
            conn.Close();   // Nó cung cấp phương thức Fill(Dataset) được sử dụng để thêm các hàng trong DataSet sao cho khớp với các hàng trong CSDL.
            return dt;
        }

        public static int ExecSqlNonQuery(String strLenh)
        {

            SqlCommand Sqlcmd = new SqlCommand(strLenh, conn);
            Sqlcmd.CommandType = CommandType.Text;
            Sqlcmd.CommandTimeout = 600;// 10 phut  -- Những câu lệnh thực thi mà không truy vấn có khả năng làm tự động hàng loạt ở bên CSDL(backup,restore) có thể nó sẽ quá thời gian mặc định 60s.
            if (conn.State == ConnectionState.Closed) conn.Open();
            try
            {
                Sqlcmd.ExecuteNonQuery(); conn.Close();
                return 0;
            }
            catch (SqlException ex)
            {
                MessageBox.Show("" + ex.Message, "Error", MessageBoxButtons.OK);

                conn.Close();
                return ex.State;// trang thai lỗi gói từ RAISERROR trong SQL Server qua -> Message.
            }
        }

        [STAThread]
        static void Main()
        {
            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);
            frmChinh = new frmMain();
            Application.Run(frmChinh);
        }
    }
}
