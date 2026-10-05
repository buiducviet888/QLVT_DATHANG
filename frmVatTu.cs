using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using DevExpress.XtraReports.UI;

namespace QLVT_DATHANG
{
    public partial class frmVatTu : Form
    {
        // Biến lưu vị trí dòng đang chọn trên lưới khi thao tác
        int viTri = 0;
        // Biến cờ: true = đang thêm mới, false = đang hiệu chỉnh
        bool dangThemMoi = false;

        public frmVatTu()
        {
            InitializeComponent();
        }

        // Sự kiện nạp dữ liệu khi form khởi động
        private void frmVatTu_Load(object sender, EventArgs e)
        {
            // 1. Tắt tạm thời kiểm tra ràng buộc khóa để tránh crash dữ liệu
            this.qLVT_DATHANGDataSet.EnforceConstraints = false;

            // 2. Gán chuỗi kết nối động theo phân mảnh hiện tại (đăng nhập từ Program)
            this.vattuTableAdapter.Connection.ConnectionString = Program.connstr;

            // 3. Tải dữ liệu vật tư vào DataSet
            this.vattuTableAdapter.Fill(this.qLVT_DATHANGDataSet.Vattu);

            // 4. Trạng thái ban đầu: Khóa khung nhập liệu, chỉ mở khi bấm Thêm/Sửa
            groupBox1.Enabled = false;

            // 5. Phân quyền theo nhóm đăng nhập
            if (Program.mGroup == "CONGTY")
            {
                btnThem.Enabled = btnHieuChinh.Enabled = btnGhi.Enabled = btnXoa.Enabled = btnPhucHoi.Enabled = false;
                btnReload.Enabled = btnIn.Enabled = btnThoat.Enabled = true;
            }
            else
            {
                btnThem.Enabled = btnHieuChinh.Enabled = btnXoa.Enabled = btnReload.Enabled = btnIn.Enabled = btnThoat.Enabled = true;
                btnGhi.Enabled = btnPhucHoi.Enabled = false;
            }
        }

        // ==========================================================
        // CÁC HÀM SỰ KIỆN DO DESIGNER SINH RA (GIỮ LẠI ĐỂ TRÁNH LỖI)
        // ==========================================================
        private void toolStrip1_ItemClicked(object sender, ToolStripItemClickedEventArgs e)
        {

        }

        private void groupBox1_Enter(object sender, EventArgs e)
        {

        }

        private void vattuBindingNavigatorSaveItem_Click(object sender, EventArgs e)
        {
            this.Validate();
            this.vattuBindingSource.EndEdit();
            this.tableAdapterManager.UpdateAll(this.qLVT_DATHANGDataSet);
        }

        // ==========================================================
        // CÁC NÚT THAO TÁC TRÊN THANH CÔNG CỤ (THÊM, XÓA, GHI,...)
        // ==========================================================

        // Nút THÊM
        private void btnThem_Click(object sender, EventArgs e)
        {
            viTri = vattuBindingSource.Position;
            groupBox1.Enabled = true;
            vattuBindingSource.AddNew();

            vattuDataGridView.Enabled = false;
            dangThemMoi = true;

            // Mở ô Mã VT để nhập liệu
            mAVTTextBox.Enabled = true;
            mAVTTextBox.Focus();

            btnThem.Enabled = btnHieuChinh.Enabled = btnXoa.Enabled = btnReload.Enabled = btnIn.Enabled = btnThoat.Enabled = false;
            btnGhi.Enabled = btnPhucHoi.Enabled = true;
        }

        // Nút HIỆU CHỈNH
        private void btnHieuChinh_Click(object sender, EventArgs e)
        {
            if (vattuBindingSource.Count == 0)
            {
                MessageBox.Show("Không có vật tư nào để hiệu chỉnh!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
                return;
            }

            viTri = vattuBindingSource.Position;
            groupBox1.Enabled = true;

            // Khóa mã vật tư (không cho sửa khóa chính)
            mAVTTextBox.Enabled = false;

            vattuDataGridView.Enabled = false;
            dangThemMoi = false;

            btnThem.Enabled = btnHieuChinh.Enabled = btnXoa.Enabled = btnReload.Enabled = btnIn.Enabled = btnThoat.Enabled = false;
            btnGhi.Enabled = btnPhucHoi.Enabled = true;
        }

        // Nút GHI (LƯU)
        private void btnGhi_Click(object sender, EventArgs e)
        {
            // Kiểm tra rỗng
            if (string.IsNullOrWhiteSpace(mAVTTextBox.Text))
            {
                MessageBox.Show("Mã vật tư không được để trống!", "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                mAVTTextBox.Focus();
                return;
            }

            if (string.IsNullOrWhiteSpace(tENVTTextBox.Text))
            {
                MessageBox.Show("Tên vật tư không được để trống!", "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                tENVTTextBox.Focus();
                return;
            }

            if (string.IsNullOrWhiteSpace(dVTTextBox.Text))
            {
                MessageBox.Show("Đơn vị tính không được để trống!", "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                dVTTextBox.Focus();
                return;
            }

            try
            {
                vattuBindingSource.EndEdit();
                vattuBindingSource.ResetCurrentItem();
                this.vattuTableAdapter.Connection.ConnectionString = Program.connstr;
                this.vattuTableAdapter.Update(this.qLVT_DATHANGDataSet.Vattu);

                MessageBox.Show("Ghi dữ liệu thành công!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
            catch (Exception ex)
            {
                MessageBox.Show("Lỗi ghi vật tư: " + ex.Message, "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            groupBox1.Enabled = false;
            vattuDataGridView.Enabled = true;

            btnThem.Enabled = btnHieuChinh.Enabled = btnXoa.Enabled = btnReload.Enabled = btnIn.Enabled = btnThoat.Enabled = true;
            btnGhi.Enabled = btnPhucHoi.Enabled = false;
        }

        // Nút XÓA
        private void btnXoa_Click(object sender, EventArgs e)
        {
            if (vattuBindingSource.Count == 0)
            {
                MessageBox.Show("Không có vật tư nào để xóa!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
                return;
            }

            if (MessageBox.Show("Bạn có thực sự muốn xóa vật tư này?", "Xác nhận", MessageBoxButtons.OKCancel, MessageBoxIcon.Question) == DialogResult.OK)
            {
                string maVT = "";
                try
                {
                    maVT = ((DataRowView)vattuBindingSource[vattuBindingSource.Position])["MAVT"].ToString();
                    vattuBindingSource.RemoveCurrent();
                    this.vattuTableAdapter.Connection.ConnectionString = Program.connstr;
                    this.vattuTableAdapter.Update(this.qLVT_DATHANGDataSet.Vattu);

                    MessageBox.Show("Xóa vật tư thành công!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
                catch (Exception ex)
                {
                    MessageBox.Show("Lỗi xóa vật tư (có thể vật tư đã tồn tại trong chi tiết đơn hàng, phiếu nhập/xuất): \n" + ex.Message, "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    this.vattuTableAdapter.Fill(this.qLVT_DATHANGDataSet.Vattu);
                    vattuBindingSource.Position = vattuBindingSource.Find("MAVT", maVT);
                }
            }
        }

        // Nút PHỤC HỒI (HỦY THAO TÁC)
        private void btnPhucHoi_Click(object sender, EventArgs e)
        {
            vattuBindingSource.CancelEdit();
            if (dangThemMoi)
            {
                vattuBindingSource.Position = viTri;
            }

            groupBox1.Enabled = false;
            vattuDataGridView.Enabled = true;

            btnThem.Enabled = btnHieuChinh.Enabled = btnXoa.Enabled = btnReload.Enabled = btnIn.Enabled = btnThoat.Enabled = true;
            btnGhi.Enabled = btnPhucHoi.Enabled = false;
        }

        // Nút RELOAD
        private void btnReload_Click(object sender, EventArgs e)
        {
            try
            {
                this.vattuTableAdapter.Connection.ConnectionString = Program.connstr;
                this.vattuTableAdapter.Fill(this.qLVT_DATHANGDataSet.Vattu);
            }
            catch (Exception ex)
            {
                MessageBox.Show("Lỗi tải lại dữ liệu: " + ex.Message, "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        // Nút IN DANH MỤC VẬT TƯ
        private void btnIn_Click(object sender, EventArgs e)
        {
            try
            {
                string strLenh = "EXEC dbo.SP_INDANHMUCVATTU";
                DataTable dt = Program.ExecSqlDataTable(strLenh);

                if (dt == null || dt.Rows.Count == 0)
                {
                    MessageBox.Show("Không có dữ liệu vật tư để in!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    return;
                }

                rptDanhMucVatTu report = new rptDanhMucVatTu(dt);
                ReportPrintTool printTool = new ReportPrintTool(report);
                printTool.ShowPreviewDialog();
            }
            catch (Exception ex)
            {
                MessageBox.Show("Lỗi khi mở báo cáo vật tư: " + ex.Message, "Thông báo lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        // Nút THOÁT
        private void btnThoat_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void vattuDataGridView_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {

        }
    }
}