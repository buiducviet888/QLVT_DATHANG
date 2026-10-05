using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace QLVT_DATHANG
{
    internal static class Program
    {
        // kết nối SQL
        public static SqlConnection conn = new SqlConnection();
        public static String connstr;
        public static SqlDataReader myReader;

        // Cấu hình kết nối 
        public static String servername = "";
        public static String username = "";
        public static String mlogin = "";
        public static String password = "";

        public static String database = "QLVT_DATHANG";
        public static String remotelogin = "HTKN";
        public static String remotepassword = "26041994";

        // Lưu vết tài khoản và thông tin người dùng sau đăng nhập
        public static String mloginDN = "";
        public static String passwordDN = "";
        public static String mMaNV = "";
        public static String mHoten = "";
        public static String mGroup = "";
        public static int mChiNhanh = 0;
        public static String tenChiNhanh = "";

        //  lưu danh sách phân mảnh từ View
        public static BindingSource bds_dspm = new BindingSource();
        public static frmMain formChinh;

        // mở kết nối CSDL
        public static int KetNoi()
        {
            if (Program.conn != null && Program.conn.State == ConnectionState.Open)
                Program.conn.Close();
            try
            {
                // Bổ sung Encrypt=False, TrustServerCertificate=True và Connection Timeout=60
                Program.connstr = "Data Source=" + Program.servername +
                                  ";Initial Catalog=" + Program.database +
                                  ";User ID=" + Program.mlogin +
                                  ";Password=" + Program.password +
                                  ";Encrypt=False;TrustServerCertificate=True;Connection Timeout=60";

                Program.conn.ConnectionString = Program.connstr;
                Program.conn.Open();
                return 1;
            }
            catch (Exception e)
            {
                MessageBox.Show("Lỗi kết nối cơ sở dữ liệu.\nBạn xem lại user name và password.\n" + e.Message, "Lỗi kết nối", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return 0;
            }
        }

        // thực thi truy vấn lấy dữ liệu về DataTable 
        public static DataTable ExecSqlDataTable(String strLenh)
        {
            DataTable dt = new DataTable();
            if (Program.conn.State == ConnectionState.Closed) Program.conn.Open();
            using (SqlDataAdapter da = new SqlDataAdapter(strLenh, Program.conn))
            {
                // Nâng timeout lên 120 giây tránh nghẽn khi truy vấn phân tán
                da.SelectCommand.CommandTimeout = 120;
                try
                {
                    da.Fill(dt);
                    return dt;
                }
                catch (SqlException ex)
                {
                    Program.conn.Close();
                    MessageBox.Show("Lỗi truy vấn SQL: " + ex.Message, "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    return null;
                }
            }
        }

        // thực thi truy vấn lấy dữ liệu về SqlDataReader
        public static SqlDataReader ExecSqlDataReader(String strLenh)
        {
            SqlDataReader myreader;
            SqlCommand sqlcmd = new SqlCommand(strLenh, Program.conn);
            sqlcmd.CommandType = CommandType.Text;
            sqlcmd.CommandTimeout = 120;

            if (Program.conn.State == ConnectionState.Closed) Program.conn.Open();
            try
            {
                myreader = sqlcmd.ExecuteReader();
                return myreader;
            }
            catch (SqlException ex)
            {
                Program.conn.Close();
                MessageBox.Show(ex.Message, "Lỗi truy vấn dữ liệu", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return null;
            }
        }

        // Hàm thực thi lệnh không trả bảng 
        public static int ExecSqlNonQuery(String strlenh)
        {
            SqlCommand Sqlcmd = new SqlCommand(strlenh, conn);
            Sqlcmd.CommandType = CommandType.Text;
            Sqlcmd.CommandTimeout = 600;
            if (conn.State == ConnectionState.Closed) conn.Open();
            try
            {
                Sqlcmd.ExecuteNonQuery();
                conn.Close();
                return 0;
            }
            catch (SqlException ex)
            {
                MessageBox.Show(ex.Message, "Lỗi thực thi", MessageBoxButtons.OK, MessageBoxIcon.Error);
                conn.Close();
                return ex.Number;
            }
        }

        // Hàm gọi Stored Procedure kiểm tra trùng khóa 
        public static int KiemTraTrung(string spName, string paramName, string giaTri)
        {
            SqlCommand sqlcmd = new SqlCommand(spName, Program.conn);
            sqlcmd.CommandType = CommandType.StoredProcedure;
            sqlcmd.CommandTimeout = 120;
            sqlcmd.Parameters.AddWithValue(paramName, giaTri);

            SqlParameter returnParam = new SqlParameter();
            returnParam.Direction = ParameterDirection.ReturnValue;
            sqlcmd.Parameters.Add(returnParam);

            try
            {
                if (Program.conn.State == ConnectionState.Closed) Program.conn.Open();
                sqlcmd.ExecuteNonQuery();
                return (int)returnParam.Value;
            }
            catch (Exception)
            {
                try
                {
                    string sql = string.Format("EXEC {0} '{1}'", spName, giaTri);
                    SqlDataReader reader = Program.ExecSqlDataReader(sql);
                    if (reader != null && reader.Read())
                    {
                        int ketqua = Convert.ToInt32(reader[0]);
                        reader.Close();
                        return ketqua;
                    }
                    if (reader != null) reader.Close();
                }
                catch { }
                return 0;
            }
        }

        // Hàm gọi Stored Procedure có tham số truyền vào và nhận mã lỗi trả về
        public static int ExecSqlStoredProcedure(string spName, SqlParameter[] parameters)
        {
            SqlCommand sqlcmd = new SqlCommand(spName, Program.conn);
            sqlcmd.CommandType = CommandType.StoredProcedure;
            sqlcmd.CommandTimeout = 120;

            if (parameters != null)
            {
                sqlcmd.Parameters.AddRange(parameters);
            }

            SqlParameter returnParam = new SqlParameter();
            returnParam.Direction = ParameterDirection.ReturnValue;
            sqlcmd.Parameters.Add(returnParam);

            try
            {
                if (Program.conn.State == ConnectionState.Closed) Program.conn.Open();
                sqlcmd.ExecuteNonQuery();
                return (int)returnParam.Value;
            }
            catch (SqlException ex)
            {
                MessageBox.Show(ex.Message, "Lỗi thực thi Stored Procedure", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return ex.Number;
            }
        }

        [STAThread]
        static void Main()
        {
            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);

            // Khởi tạo frmMain làm cửa sổ chủ MDI
            formChinh = new frmMain();
            Application.Run(formChinh);
        }
    }
}