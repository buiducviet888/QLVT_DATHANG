using System;
using System.Data;
using System.Data.SqlClient;
using System.Windows.Forms;

namespace QLVT_DATHANG
{
    public partial class frmDangNhap : Form
    {
        private SqlConnection conn_publisher = new SqlConnection();

        public frmDangNhap()
        {
            InitializeComponent();
        }

        // Kết nối về Site Chủ để đọc danh sách phân mảnh
        private int KetNoi_CSDLGOC()
        {
            if (conn_publisher != null && conn_publisher.State == ConnectionState.Open)
                conn_publisher.Close();
            try
            {
                conn_publisher.ConnectionString = "Data Source=LAPTOP-IIK5FBUA;Initial Catalog=QLVT_DATHANG;Integrated Security=True;Connection Timeout=10";
                conn_publisher.Open();
                return 1;
            }
            catch (Exception ex)
            {
                MessageBox.Show("Lỗi kết nối về cơ sở dữ liệu gốc.\n" + ex.Message, "Lỗi kết nối", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return 0;
            }
        }

        // Đổ danh sách chi nhánh vào ComboBox
        private void LayDSPM(string cmd)
        {
            DataTable dt = new DataTable();
            try
            {
                if (conn_publisher.State == ConnectionState.Closed) conn_publisher.Open();
                using (SqlDataAdapter da = new SqlDataAdapter(cmd, conn_publisher))
                {
                    da.SelectCommand.CommandTimeout = 15;
                    da.Fill(dt);
                }
                conn_publisher.Close();

                Program.bds_dspm.DataSource = dt;
                cmbChiNhanh.DataSource = Program.bds_dspm;
                cmbChiNhanh.DisplayMember = "TENCN";
                cmbChiNhanh.ValueMember = "TENSERVER";
            }
            catch (Exception ex)
            {
                MessageBox.Show("Lỗi nạp danh sách phân mảnh: " + ex.Message, "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void frmDangNhap_Load(object sender, EventArgs e)
        {
            if (KetNoi_CSDLGOC() == 0) return;
            LayDSPM("SELECT * FROM dbo.Get_Subscribes WITH (NOLOCK)");

            if (cmbChiNhanh.Items.Count > 0)
            {
                cmbChiNhanh.SelectedIndex = 1;
                cmbChiNhanh.SelectedIndex = 0;
            }
        }

        private void cmbChiNhanh_SelectedIndexChanged(object sender, EventArgs e)
        {
            try
            {
                if (cmbChiNhanh.SelectedValue != null)
                {
                    Program.servername = cmbChiNhanh.SelectedValue.ToString();
                }
            }
            catch (Exception) { }
        }

        private void btnDangNhap_Click(object sender, EventArgs e)
        {
            if (txtLogin.Text.Trim() == "" || txtPass.Text.Trim() == "")
            {
                MessageBox.Show("Tài khoản và mật mã không được để trống!", "Cảnh báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                txtLogin.Focus();
                return;
            }

            Program.mlogin = txtLogin.Text.Trim();
            Program.password = txtPass.Text.Trim();

            // Kết nối thử tới chi nhánh đã chọn trên combobox
            if (Program.KetNoi() == 0) return;

            Program.mChiNhanh = cmbChiNhanh.SelectedIndex;
            Program.tenChiNhanh = cmbChiNhanh.Text;
            Program.mloginDN = Program.mlogin;
            Program.passwordDN = Program.password;

            // Đọc thông tin phân quyền từ Stored Procedure SP_Lay_Thong_Tin_NV_Tu_Login
            string strLenh = "EXEC SP_Lay_Thong_Tin_NV_Tu_Login '" + Program.mlogin + "'";
            Program.myReader = Program.ExecSqlDataReader(strLenh);
            if (Program.myReader == null) return;

            try
            {
                Program.myReader.Read();

                Program.mMaNV = Program.myReader.GetString(0);
                if (Convert.IsDBNull(Program.mMaNV))
                {
                    MessageBox.Show("Login của bạn không có quyền truy cập dữ liệu.", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    Program.myReader.Close();
                    return;
                }

                Program.mHoten = Program.myReader.GetString(1);
                Program.mGroup = Program.myReader.GetString(2);
                Program.myReader.Close();

                MessageBox.Show("Đăng nhập thành công!\nXin chào: " + Program.mHoten + " - Nhóm: " + Program.mGroup,
                                "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);

                // Cập nhật lại thông tin quyền và trạng thái lên Form Chính
                if (Program.formChinh != null)
                {
                    Program.formChinh.HienThiMenu();
                }

                // Đóng form đăng nhập để quay về Form Chính làm việc
                this.Close();
            }
            catch (Exception ex)
            {
                MessageBox.Show("Lỗi đọc thông tin nhân viên: " + ex.Message, "Thông báo lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
                if (Program.myReader != null && !Program.myReader.IsClosed)
                    Program.myReader.Close();
            }
        }

        private void btnThoat_Click(object sender, EventArgs e)
        {
            this.Close();
        }
    }
}