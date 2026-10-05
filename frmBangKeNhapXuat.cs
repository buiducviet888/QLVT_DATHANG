using DevExpress.XtraReports.UI;
using System;
using System.Data;
using System.Windows.Forms;

namespace QLVT_DATHANG
{
    // BẮT BUỘC PHẢI CÓ : Form Ở ĐÂY
    public partial class frmBangKeNhapXuat : Form
    {
        public frmBangKeNhapXuat()
        {
            InitializeComponent();
        }

        private void frmBangKeNhapXuat_Load(object sender, EventArgs e)
        {
            // Thiết lập danh sách loại phiếu
            cmbLoai.Items.Clear();
            cmbLoai.Items.Add("NHAP");
            cmbLoai.Items.Add("XUAT");
            cmbLoai.SelectedIndex = 0;

            // Khởi tạo thời gian mặc định
            dtpTuNgay.Value = new DateTime(2020, 1, 1);
            dtpDenNgay.Value = DateTime.Now;
        }

        private void btnXemBaoCao_Click(object sender, EventArgs e)
        {
            DateTime tuNgay = dtpTuNgay.Value.Date;
            DateTime denNgay = dtpDenNgay.Value.Date.AddDays(1).AddSeconds(-1);

            if (tuNgay > denNgay)
            {
                MessageBox.Show("Từ ngày không được lớn hơn đến ngày!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                dtpTuNgay.Focus();
                return;
            }

            string loai = cmbLoai.SelectedItem.ToString();
            string role = Program.mGroup;

            string strFrom = tuNgay.ToString("yyyy-MM-dd HH:mm:ss");
            string strTo = denNgay.ToString("yyyy-MM-dd HH:mm:ss");

            string strLenh = $"EXEC dbo.SP_BANGKECHITIETNHAPXUAT '{loai}', '{strFrom}', '{strTo}', '{role}'";

            try
            {
                DataTable dt = Program.ExecSqlDataTable(strLenh);

                if (dt == null || dt.Rows.Count == 0)
                {
                    MessageBox.Show("Không có dữ liệu trong khoảng thời gian đã chọn!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    return;
                }

                rptBangKeChiTietNhapXuat report = new rptBangKeChiTietNhapXuat(dt, loai, dtpTuNgay.Value, dtpDenNgay.Value);
                ReportPrintTool printTool = new ReportPrintTool(report);
                printTool.ShowPreviewDialog();
            }
            catch (Exception ex)
            {
                MessageBox.Show("Lỗi khi mở báo cáo: " + ex.Message, "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void btnThoat_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void dtpTuNgay_ValueChanged(object sender, EventArgs e)
        {
        }
    }
}