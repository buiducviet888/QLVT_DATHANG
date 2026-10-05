using System;
using System.Data;
using System.Data.SqlClient;
using System.Windows.Forms;

namespace QLVT_DATHANG
{
    public partial class frmTaoLogin : Form
    {
        public frmTaoLogin()
        {
            InitializeComponent();
        }

        private void frmTaoLogin_Load(object sender, EventArgs e)
        {
            if (Program.mGroup == "CONGTY")
            {
                rdoCongTy.Checked = true;
                rdoCongTy.Enabled = true;
                rdoChiNhanh.Enabled = false;
                rdoUser.Enabled = false;
            }
            else if (Program.mGroup == "CHINHANH")
            {
                rdoChiNhanh.Checked = true;
                rdoCongTy.Enabled = false;
                rdoChiNhanh.Enabled = true;
                rdoUser.Enabled = true;
            }
        }

        private void btnXacNhan_Click(object sender, EventArgs e)
        {
            string login = txtLogin.Text.Trim();
            string pass = txtPass.Text.Trim();
            string maNV = txtMaNV.Text.Trim();

            if (string.IsNullOrEmpty(login))
            {
                MessageBox.Show("Tên đăng nhập không được để trống!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                txtLogin.Focus();
                return;
            }

            if (string.IsNullOrEmpty(pass))
            {
                MessageBox.Show("Mật mã không được để trống!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                txtPass.Focus();
                return;
            }

            if (string.IsNullOrEmpty(maNV))
            {
                MessageBox.Show("Mã nhân viên không được để trống!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                txtMaNV.Focus();
                return;
            }

            string role = "";
            if (rdoCongTy.Checked) role = "CONGTY";
            else if (rdoChiNhanh.Checked) role = "CHINHANH";
            else if (rdoUser.Checked) role = "USER";

            if (Program.conn.State == ConnectionState.Closed)
            {
                Program.conn.Open();
            }

            using (SqlCommand cmd = new SqlCommand("SP_TAOLOGIN", Program.conn))
            {
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("@LGNAME", login);
                cmd.Parameters.AddWithValue("@PASS", pass);
                cmd.Parameters.AddWithValue("@USERNAME", maNV);
                cmd.Parameters.AddWithValue("@ROLE", role);

                SqlParameter retParam = new SqlParameter();
                retParam.Direction = ParameterDirection.ReturnValue;
                cmd.Parameters.Add(retParam);

                try
                {
                    cmd.ExecuteNonQuery();
                    int ketQua = Convert.ToInt32(retParam.Value);

                    if (ketQua == 0)
                    {
                        MessageBox.Show("Tạo tài khoản đăng nhập thành công!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
                        this.Close();
                    }
                    else if (ketQua == 1)
                    {
                        MessageBox.Show("Tên Login đã tồn tại trên Server!", "Cảnh báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                        txtLogin.Focus();
                    }
                    else if (ketQua == 2)
                    {
                        MessageBox.Show("Mã nhân viên không tồn tại trong hệ thống!", "Cảnh báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                        txtMaNV.Focus();
                    }
                    else if (ketQua == 3)
                    {
                        MessageBox.Show("Nhân viên này đã nghỉ việc hoặc chuyển chi nhánh, không được phép cấp tài khoản!", "Cảnh báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                        txtMaNV.Focus();
                    }
                    else if (ketQua == 4)
                    {
                        MessageBox.Show("Nhân viên này đã được tạo tài khoản trước đó!", "Cảnh báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                        txtMaNV.Focus();
                    }
                }
                catch (SqlException ex)
                {
                    MessageBox.Show(ex.Message, "Lỗi từ CSDL", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
        }

        private void btnThoat_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void textBox3_TextChanged(object sender, EventArgs e)
        {
        }
    }
}