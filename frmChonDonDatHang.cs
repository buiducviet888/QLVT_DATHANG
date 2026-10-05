using System;
using System.Data;
using System.Data.SqlClient;
using System.Windows.Forms;

namespace QLVT_DATHANG
{
    public partial class frmChonDonDatHang : Form
    {
        // Thuộc tính truyền dữ liệu về frmPhieuNhap
        public string MaSoDDH { get; private set; } = "";
        public string MaKho { get; private set; } = "";

        public frmChonDonDatHang()
        {
            InitializeComponent();
        }

        private void frmChonDonDatHang_Load(object sender, EventArgs e)
        {
            LoadDanhSachDDH();
        }

        private void LoadDanhSachDDH()
        {
            string cmd = "EXEC SP_DONDATHANGCHUANHAP";
            DataTable dt = new DataTable();

            try
            {
                if (Program.conn.State == ConnectionState.Closed) Program.conn.Open();
                SqlDataAdapter da = new SqlDataAdapter(cmd, Program.conn);
                da.Fill(dt);
                dgvDonDatHang.DataSource = dt;
                dgvDonDatHang.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill;
            }
            catch (Exception ex)
            {
                MessageBox.Show("Lỗi lấy danh sách Đơn đặt hàng chưa nhập: " + ex.Message, "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void btnChon_Click(object sender, EventArgs e)
        {
            if (dgvDonDatHang.CurrentRow == null || dgvDonDatHang.CurrentRow.Index < 0)
            {
                MessageBox.Show("Vui lòng chọn một đơn đặt hàng!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            try
            {
                // Lấy mã số đơn đặt hàng và mã kho từ dòng đang chọn
                MaSoDDH = dgvDonDatHang.CurrentRow.Cells["MasoDDH"].Value.ToString().Trim();
                MaKho = dgvDonDatHang.CurrentRow.Cells["MAKHO"].Value.ToString().Trim();

                this.DialogResult = DialogResult.OK;
                this.Close();
            }
            catch (Exception ex)
            {
                MessageBox.Show("Lỗi chọn đơn đặt hàng: " + ex.Message, "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void btnThoat_Click(object sender, EventArgs e)
        {
            this.DialogResult = DialogResult.Cancel;
            this.Close();
        }
    }
}