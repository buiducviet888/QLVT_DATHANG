using DevExpress.XtraReports.UI;
using System;
using System.Data;

namespace QLVT_DATHANG
{
    public partial class rptBangKeChiTietNhapXuat : DevExpress.XtraReports.UI.XtraReport
    {
        public rptBangKeChiTietNhapXuat()
        {
            InitializeComponent();
        }

        // Constructor 4 tham số nhận dữ liệu từ frmBangKeNhapXuat
        public rptBangKeChiTietNhapXuat(DataTable dt, string loai, DateTime tuNgay, DateTime denNgay)
        {
            InitializeComponent();

            this.DataSource = dt;

            // Đổi tiêu đề báo cáo theo loại (NHAP/XUAT) và khoảng ngày lọc
            string tenLoai = (loai == "NHAP") ? "NHẬP" : "XUẤT";
            this.lblTieuDe.Text = $"BẢNG KÊ CHI TIẾT SỐ LƯỢNG – TRỊ GIÁ HÀNG {tenLoai}";
            this.lblThoiGian.Text = $"Từ ngày {tuNgay:dd/MM/yyyy} đến ngày {denNgay:dd/MM/yyyy}";

            // Gắn dữ liệu từng cột tương ứng từ Stored Procedure SP_BANGKECHITIETNHAPXUAT
            this.cellThangNam.DataBindings.Add("Text", dt, "THANGNAM");
            this.cellTenVT.DataBindings.Add("Text", dt, "TENVT");
            this.cellTongSL.DataBindings.Add("Text", dt, "TONGSOLUONG", "{0:#,##0}");
            this.cellTongTriGia.DataBindings.Add("Text", dt, "TONGTRIGIA", "{0:#,##0}");
        }
    }
}