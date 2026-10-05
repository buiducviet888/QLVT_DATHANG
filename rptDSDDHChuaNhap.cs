using DevExpress.XtraPrinting;
using DevExpress.XtraReports.UI;
using System;
using System.Data;
using System.Drawing;
using System.Windows.Forms;

namespace QLVT_DATHANG
{
    public partial class rptDSDDHChuaNhap : DevExpress.XtraReports.UI.XtraReport
    {
        public rptDSDDHChuaNhap()
        {
            InitializeComponent();
        }

        public rptDSDDHChuaNhap(DataTable dt)
        {
            InitializeComponent();

            if (dt == null || dt.Rows.Count == 0)
            {
                MessageBox.Show("Không có dữ liệu đơn đặt hàng chưa có phiếu nhập!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
                return;
            }

            // Gán nguồn dữ liệu cho Report chính
            this.DataSource = dt;

            // 1. Xóa toàn bộ binding cũ để tránh xung đột
            cellMasoDDH.DataBindings.Clear();
            cellNgay.DataBindings.Clear();
            cellNhaCC.DataBindings.Clear();
            cellHoTenNV.DataBindings.Clear();
            cellTenVT.DataBindings.Clear();
            cellSoLuong.DataBindings.Clear();
            cellDonGia.DataBindings.Clear();

            // 2. Ràng buộc dữ liệu Master cho GroupHeader1 (Thông tin chung đơn đặt hàng)
            cellMasoDDH.DataBindings.Add("Text", dt, "MasoDDH", "Mã ĐĐH: {0}");
            cellNgay.DataBindings.Add("Text", dt, "NGAY", "Ngày: {0:dd/MM/yyyy}");
            cellNhaCC.DataBindings.Add("Text", dt, "NhaCC", "NCC: {0}");
            cellHoTenNV.DataBindings.Add("Text", dt, "HOTENNV", "NV: {0}");

            // 3. Ràng buộc dữ liệu Detail cho Detail Band (Danh sách vật tư chi tiết)
            // Đảm bảo Detail Band nhận trực tiếp từ DataTable chung
            cellTenVT.DataBindings.Add("Text", dt, "TENVT");
            cellSoLuong.DataBindings.Add("Text", dt, "SOLUONG", "{0:#,##0}");
            cellDonGia.DataBindings.Add("Text", dt, "DONGIA", "{0:#,##0}");

            // Định dạng màu sắc và khung viền
            DinhDangKhungVien();
        }

        private void DinhDangKhungVien()
        {
            // Viền và nền cho dòng thông tin chung ĐĐH (GroupHeader)
            this.tableGroupHeader.Borders = BorderSide.All;
            this.tableGroupHeader.BorderColor = Color.Black;
            this.tableGroupHeader.BorderWidth = 1;
            this.tableGroupHeader.BackColor = Color.FromArgb(230, 244, 255); // Xanh nhạt

            // Viền và nền cho tiêu đề cột chi tiết vật tư
            this.tableHeaderCols.Borders = BorderSide.All;
            this.tableHeaderCols.BorderColor = Color.Black;
            this.tableHeaderCols.BorderWidth = 1;
            this.tableHeaderCols.BackColor = Color.FromArgb(235, 235, 235); // Xám nhạt

            // Viền cho từng dòng chi tiết vật tư (Detail)
            this.tableDetail.Borders = BorderSide.All;
            this.tableDetail.BorderColor = Color.Black;
            this.tableDetail.BorderWidth = 1;
        }
    }
}