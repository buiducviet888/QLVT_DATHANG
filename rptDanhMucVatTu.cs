using DevExpress.XtraReports.UI;
using System;
using System.Data;

namespace QLVT_DATHANG
{
    public partial class rptDanhMucVatTu : DevExpress.XtraReports.UI.XtraReport
    {
        public rptDanhMucVatTu()
        {
            InitializeComponent();
        }

        public rptDanhMucVatTu(DataTable dt)
        {
            InitializeComponent();

            // Gán dữ liệu nguồn
            this.DataSource = dt;

            // Binding từng cột trả về từ SP_INDANHMUCVATTU
            this.cellMaVT.DataBindings.Add("Text", dt, "MAVT");
            this.cellTenVT.DataBindings.Add("Text", dt, "TENVT");
            this.cellDVT.DataBindings.Add("Text", dt, "DVT");

            // Format số lượng tồn có phân cách hàng nghìn
            this.cellSLTon.DataBindings.Add("Text", dt, "SOLUONGTON", "{0:#,##0}");
        }
    }
}