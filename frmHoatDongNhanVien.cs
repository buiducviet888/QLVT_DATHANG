using DevExpress.XtraReports.UI;
using System;
using System.Data;
using System.Windows.Forms;

namespace QLVT_DATHANG
{
    public partial class frmHoatDongNhanVien : Form
    {
        string serverHienTai = "";
        bool đangLoad = true;

        public frmHoatDongNhanVien()
        {
            InitializeComponent();
        }

        private void frmHoatDongNhanVien_Load(object sender, EventArgs e)
        {
            đangLoad = true;
            serverHienTai = Program.servername;

            // 1. Đổ dữ liệu danh sách phân mảnh vào ComboBox Chi nhánh
            cmbChiNhanh.DataSource = Program.bds_dspm;
            cmbChiNhanh.DisplayMember = "TENCN";
            cmbChiNhanh.ValueMember = "TENSERVER";

            // 2. Phân quyền hiển thị theo nhóm
            if (Program.mGroup == "CONGTY")
            {
                cmbChiNhanh.Enabled = true;
            }
            else
            {
                cmbChiNhanh.Enabled = false;
            }

            // Đặt mặc định chọn chi nhánh hiện tại
            cmbChiNhanh.SelectedValue = Program.servername;

            // 3. Load danh sách nhân viên ban đầu
            LoadDanhSachNhanVien();

            dtpTuNgay.Value = new DateTime(1995, 1, 1);
            dtpDenNgay.Value = DateTime.Now;

            đangLoad = false;
        }

        private void LoadDanhSachNhanVien()
        {
            try
            {
                string sql = "SELECT MANV, (HO + ' ' + TEN) AS HOTEN FROM NhanVien ORDER BY TEN ASC, HO ASC";
                DataTable dtNV = Program.ExecSqlDataTable(sql);

                cmbNhanVien.DataSource = dtNV;
                cmbNhanVien.DisplayMember = "HOTEN";
                cmbNhanVien.ValueMember = "MANV";
            }
            catch (Exception ex)
            {
                MessageBox.Show("Lỗi tải danh sách nhân viên: " + ex.Message, "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void cmbChiNhanh_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (đangLoad) return;

            if (cmbChiNhanh.SelectedValue == null || cmbChiNhanh.SelectedValue.ToString() == "System.Data.DataRowView") return;

            Program.servername = cmbChiNhanh.SelectedValue.ToString();

            if (cmbChiNhanh.SelectedIndex == Program.mChiNhanh)
            {
                Program.mlogin = Program.mloginDN;
                Program.password = Program.passwordDN;
            }
            else
            {
                Program.mlogin = Program.remotelogin;
                Program.password = Program.remotepassword;
            }

            if (Program.KetNoi() == 0)
            {
                MessageBox.Show("Lỗi kết nối về chi nhánh mới!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            LoadDanhSachNhanVien();
        }

        private void btnInBaoCao_Click(object sender, EventArgs e)
        {
            if (cmbNhanVien.SelectedValue == null)
            {
                MessageBox.Show("Vui lòng chọn nhân viên!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            int maNV = Convert.ToInt32(cmbNhanVien.SelectedValue);
            string hoTen = cmbNhanVien.Text;

            DateTime tuNgay = dtpTuNgay.Value.Date;
            DateTime denNgay = dtpDenNgay.Value.Date;

            if (tuNgay > denNgay)
            {
                MessageBox.Show("Từ ngày không thể lớn hơn Đến ngày!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            string role = Program.mGroup;
            string strFrom = tuNgay.ToString("yyyy-MM-dd");
            string strTo = denNgay.ToString("yyyy-MM-dd");

            // Gọi đúng tên Stored Procedure mới
            string sql = $"EXEC dbo.SP_BC_HOATDONGNHANVIEN '{strFrom}', '{strTo}', {maNV}, '{role}'";

            try
            {
                DataTable dt = Program.ExecSqlDataTable(sql);

                if (dt == null || dt.Rows.Count == 0)
                {
                    MessageBox.Show("Nhân viên này không có phát sinh giao dịch trong khoảng thời gian đã chọn!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    return;
                }

                // Gọi Report Hoạt Động Nhân Viên và truyền đầy đủ 5 tham số khớp với Report constructor
                rptHoatDongNhanVien report = new rptHoatDongNhanVien(dt, hoTen, maNV, tuNgay, denNgay);
                ReportPrintTool printTool = new ReportPrintTool(report);
                printTool.ShowPreviewDialog();
            }
            catch (Exception ex)
            {
                MessageBox.Show("Lỗi hiển thị báo cáo: " + ex.Message, "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void btnThoat_Click(object sender, EventArgs e)
        {
            this.Close();
        }
    }
}