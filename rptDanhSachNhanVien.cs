using DevExpress.XtraReports.UI;
using System;
using System.Data;

namespace QLVT_DATHANG
{
    public partial class rptDanhSachNhanVien : DevExpress.XtraReports.UI.XtraReport
    {
        public rptDanhSachNhanVien()
        {
            InitializeComponent();
        }

        // Constructor nhận DataTable kết quả từ Stored Procedure và Tên Chi Nhánh
        public rptDanhSachNhanVien(DataTable dt, string tenChiNhanh)
        {
            InitializeComponent();

            // 1. Hiển thị thông tin tên chi nhánh lên tiêu đề
            this.lblChiNhanh.Text = "CHI NHÁNH: " + tenChiNhanh.ToUpper();

            // 2. Gán DataTable vào nguồn dữ liệu của Report
            this.DataSource = dt;

            // 3. Binding từng cột của Stored Procedure vào các ô ở dải Detail
            this.cellMaNV.DataBindings.Add("Text", dt, "MANV");
            this.cellHo.DataBindings.Add("Text", dt, "HO");
            this.cellTen.DataBindings.Add("Text", dt, "TEN");
            this.cellDiachi.DataBindings.Add("Text", dt, "DIACHI");

            // Format ngày sinh dd/MM/yyyy
            this.cellNgaySinh.DataBindings.Add("Text", dt, "NGAYSINH", "{0:dd/MM/yyyy}");

            // Format lương có dấu phân cách hàng nghìn (ví dụ: 7,000,000)
            this.cellLuong.DataBindings.Add("Text", dt, "LUONG", "{0:#,##0}");
        }
    }
}