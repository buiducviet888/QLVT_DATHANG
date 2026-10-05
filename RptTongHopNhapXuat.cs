using DevExpress.XtraReports.UI;
using System;
using System.Collections;
using System.ComponentModel;
using System.Drawing;

namespace QLVT_DATHANG
{
    public partial class RptTongHopNhapXuat : DevExpress.XtraReports.UI.XtraReport
    {
        public RptTongHopNhapXuat()
        {
            InitializeComponent();
        }

        // Constructor nhận thêm 2 tham số ngày tháng để hiển thị lên tiêu đề báo cáo
        public RptTongHopNhapXuat(DateTime fromDate, DateTime toDate)
        {
            InitializeComponent();

            // Gán giá trị khoảng thời gian vào nhãn tiêu đề (lblTieuDe) trên Report
            lblTieuDe.Text = "TỪ NGÀY " + fromDate.ToString("dd/MM/yyyy") + " ĐẾN NGÀY " + toDate.ToString("dd/MM/yyyy");
        }
    }
}