using System;
using System.Data;
using System.Windows.Forms;
using DevExpress.XtraReports.UI;

namespace QLVT_DATHANG
{
    public partial class frmTongHopNhapXuat : Form
    {
        public frmTongHopNhapXuat()
        {
            InitializeComponent();
        }

        private void frmTongHopNhapXuat_Load(object sender, EventArgs e)
        {
            // Đưa danh sách phân mảnh từ bds_dspm vào ComboBox
            if (Program.bds_dspm != null)
            {
                cmbChiNhanh.DataSource = Program.bds_dspm;
                cmbChiNhanh.DisplayMember = "TENCN";
                cmbChiNhanh.ValueMember = "TENSERVER";
            }

            // Phân quyền theo nhóm đăng nhập: CONGTY được chọn chi nhánh, ngược lại bị khóa
            if (Program.mGroup == "CONGTY")
            {
                cmbChiNhanh.Enabled = true;
            }
            else
            {
                cmbChiNhanh.Enabled = false;
            }

            // Gán giá trị mặc định cho khoảng thời gian (Từ đầu tháng đến hiện tại)
            dateEditFrom.EditValue = new DateTime(DateTime.Now.Year, DateTime.Now.Month, 1);
            dateEditTo.EditValue = DateTime.Now;
        }

        private void cmbChiNhanh_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (cmbChiNhanh.SelectedValue == null || cmbChiNhanh.SelectedValue.ToString() == "System.Data.DataRowView") return;

            Program.servername = cmbChiNhanh.SelectedValue.ToString();

            // Nếu người dùng chọn sang chi nhánh khác với chi nhánh đăng nhập ban đầu
            if (cmbChiNhanh.SelectedIndex != Program.mChiNhanh)
            {
                Program.mlogin = Program.remotelogin;
                Program.password = Program.remotepassword;
            }
            else
            {
                Program.mlogin = Program.mloginDN;
                Program.password = Program.passwordDN;
            }

            // Thực hiện kết nối tới server chi nhánh mới
            if (Program.KetNoi() == 0)
            {
                MessageBox.Show("Lỗi kết nối về chi nhánh mới!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void btnIn_Click(object sender, EventArgs e)
        {
            // Kiểm tra chọn ngày tháng
            if (dateEditFrom.EditValue == null || dateEditTo.EditValue == null)
            {
                MessageBox.Show("Vui lòng chọn đầy đủ khoảng thời gian!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            DateTime fromDate = dateEditFrom.DateTime;
            DateTime toDate = dateEditTo.DateTime;

            if (fromDate > toDate)
            {
                MessageBox.Show("Ngày bắt đầu không được lớn hơn ngày kết thúc!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            // Gọi Stored Procedure SP_TONGHOPNHOPXUAT trong SQL Server
            string strLenh = "EXEC SP_TONGHOPNHAPXUAT '" + fromDate.ToString("yyyy-MM-dd") + "', '" + toDate.ToString("yyyy-MM-dd") + "'";

            DataTable dt = Program.ExecSqlDataTable(strLenh);

            if (dt == null || dt.Rows.Count == 0)
            {
                MessageBox.Show("Không có dữ liệu trong khoảng thời gian này để in báo cáo!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
                return;
            }

            // Khởi tạo Report và truyền dữ liệu vào
            RptTongHopNhapXuat rpt = new RptTongHopNhapXuat(fromDate, toDate);
            rpt.DataSource = dt;

            // Hiển thị cửa sổ xem trước (Preview)
            ReportPrintTool printTool = new ReportPrintTool(rpt);
            printTool.ShowPreviewDialog();
        }

        private void btnThoat_Click(object sender, EventArgs e)
        {
            this.Close();
        }
    }
}