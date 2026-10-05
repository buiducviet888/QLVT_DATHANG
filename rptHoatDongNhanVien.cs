using DevExpress.XtraReports.UI;
using System;
using System.Data;

namespace QLVT_DATHANG
{
    public partial class rptHoatDongNhanVien : DevExpress.XtraReports.UI.XtraReport
    {
        public rptHoatDongNhanVien()
        {
            InitializeComponent();
        }

        public rptHoatDongNhanVien(DataTable dt, string hoTenNV, int maNV, DateTime tuNgay, DateTime denNgay)
        {
            InitializeComponent();

            this.DataSource = dt;

            // Thông tin nhân viên & khoảng thời gian
            this.lblThongTinNV.Text = $"Nhân viên: {hoTenNV} - Mã NV: {maNV}";
            this.lblThoiGian.Text = $"Từ ngày {tuNgay:dd/MM/yyyy} đến ngày {denNgay:dd/MM/yyyy}";

            // GroupHeader theo THANGNAM (tháng/năm)
            this.lblThangNamHeader.DataBindings.Add("Text", dt, "THANGNAM", "Tháng: {0}");

            // Ràng buộc dữ liệu bảng chi tiết (Detail) - Khớp đúng cột SOPHIEU và TENKHO từ SP
            this.cellNgay.DataBindings.Add("Text", dt, "NGAY", "{0:dd/MM/yyyy}");
            this.cellSoPhieu.DataBindings.Add("Text", dt, "SOPHIEU");
            this.cellLoaiPhieu.DataBindings.Add("Text", dt, "LOAIPHIEU");
            this.cellDoiTac.DataBindings.Add("Text", dt, "HOTENKH");
            this.cellTenKho.DataBindings.Add("Text", dt, "TENKHO");
            this.cellTenVT.DataBindings.Add("Text", dt, "TENVT");
            this.cellSoLuong.DataBindings.Add("Text", dt, "SOLUONG", "{0:#,##0}");
            this.cellDonGia.DataBindings.Add("Text", dt, "DONGIA", "{0:#,##0}");
            this.cellTriGia.DataBindings.Add("Text", dt, "TRIGIA", "{0:#,##0}");

            // --- Ràng buộc tính tổng trị giá cho GroupFooterThang ---
            this.cellTongTienThang.DataBindings.Add("Text", dt, "TRIGIA", "{0:#,##0}");
            this.cellTongTienThang.Summary = new XRSummary(SummaryRunning.Group, SummaryFunc.Sum, "{0:#,##0}");

            // Ép viền đen sắc nét cho toàn bộ ô tiêu đề và chi tiết
            DevExpress.XtraPrinting.BorderSide allBorders = DevExpress.XtraPrinting.BorderSide.All;
            this.hNgay.Borders = allBorders;
            this.hSoPhieu.Borders = allBorders;
            this.hLoaiPhieu.Borders = allBorders;
            this.hDoiTac.Borders = allBorders;
            this.hTenKho.Borders = allBorders;
            this.hTenVT.Borders = allBorders;
            this.hSoLuong.Borders = allBorders;
            this.hDonGia.Borders = allBorders;
            this.hTriGia.Borders = allBorders;

            this.cellNgay.Borders = allBorders;
            this.cellSoPhieu.Borders = allBorders;
            this.cellLoaiPhieu.Borders = allBorders;
            this.cellDoiTac.Borders = allBorders;
            this.cellTenKho.Borders = allBorders;
            this.cellTenVT.Borders = allBorders;
            this.cellSoLuong.Borders = allBorders;
            this.cellDonGia.Borders = allBorders;
            this.cellTriGia.Borders = allBorders;
        }
    }
}