namespace QLVT_DATHANG
{
    partial class RptTongHopNhapXuat
    {
        private System.ComponentModel.IContainer components = null;

        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Designer generated code

        private void InitializeComponent()
        {
            DevExpress.XtraReports.UI.XRSummary xrSummary1 = new DevExpress.XtraReports.UI.XRSummary();
            DevExpress.XtraReports.UI.XRSummary xrSummary2 = new DevExpress.XtraReports.UI.XRSummary();
            this.TopMargin = new DevExpress.XtraReports.UI.TopMarginBand();
            this.BottomMargin = new DevExpress.XtraReports.UI.BottomMarginBand();
            this.Detail = new DevExpress.XtraReports.UI.DetailBand();
            this.xrTableDetail = new DevExpress.XtraReports.UI.XRTable();
            this.xrTableRowDetail = new DevExpress.XtraReports.UI.XRTableRow();
            this.cellNgay = new DevExpress.XtraReports.UI.XRTableCell();
            this.cellTienNhap = new DevExpress.XtraReports.UI.XRTableCell();
            this.cellTyLeNhap = new DevExpress.XtraReports.UI.XRTableCell();
            this.cellTienXuat = new DevExpress.XtraReports.UI.XRTableCell();
            this.cellTyLeXuat = new DevExpress.XtraReports.UI.XRTableCell();
            this.ReportHeader = new DevExpress.XtraReports.UI.ReportHeaderBand();
            this.lblTieuDe = new DevExpress.XtraReports.UI.XRLabel();
            this.xrLabel1 = new DevExpress.XtraReports.UI.XRLabel();
            this.PageHeader = new DevExpress.XtraReports.UI.PageHeaderBand();
            this.xrTableHeader = new DevExpress.XtraReports.UI.XRTable();
            this.xrTableRowHeader = new DevExpress.XtraReports.UI.XRTableRow();
            this.xrTableCell1 = new DevExpress.XtraReports.UI.XRTableCell();
            this.xrTableCell2 = new DevExpress.XtraReports.UI.XRTableCell();
            this.xrTableCell3 = new DevExpress.XtraReports.UI.XRTableCell();
            this.xrTableCell4 = new DevExpress.XtraReports.UI.XRTableCell();
            this.xrTableCell5 = new DevExpress.XtraReports.UI.XRTableCell();
            this.ReportFooter = new DevExpress.XtraReports.UI.ReportFooterBand();
            this.xrTableFooter = new DevExpress.XtraReports.UI.XRTable();
            this.xrTableRowFooter = new DevExpress.XtraReports.UI.XRTableRow();
            this.xrTableCell11 = new DevExpress.XtraReports.UI.XRTableCell();
            this.cellTongTienNhap = new DevExpress.XtraReports.UI.XRTableCell();
            this.xrTableCell13 = new DevExpress.XtraReports.UI.XRTableCell();
            this.cellTongTienXuat = new DevExpress.XtraReports.UI.XRTableCell();
            this.xrTableCell15 = new DevExpress.XtraReports.UI.XRTableCell();
            ((System.ComponentModel.ISupportInitialize)(this.xrTableDetail)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.xrTableHeader)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.xrTableFooter)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this)).BeginInit();
            // 
            // TopMargin
            // 
            this.TopMargin.Name = "TopMargin";
            // 
            // BottomMargin
            // 
            this.BottomMargin.Name = "BottomMargin";
            // 
            // Detail
            // 
            this.Detail.Controls.AddRange(new DevExpress.XtraReports.UI.XRControl[] {
            this.xrTableDetail});
            this.Detail.HeightF = 25F;
            this.Detail.Name = "Detail";
            // 
            // xrTableDetail
            // 
            this.xrTableDetail.Borders = ((DevExpress.XtraPrinting.BorderSide)((((DevExpress.XtraPrinting.BorderSide.Left | DevExpress.XtraPrinting.BorderSide.Top)
            | DevExpress.XtraPrinting.BorderSide.Right)
            | DevExpress.XtraPrinting.BorderSide.Bottom)));
            this.xrTableDetail.LocationFloat = new DevExpress.Utils.PointFloat(10.00001F, 0F);
            this.xrTableDetail.Name = "xrTableDetail";
            this.xrTableDetail.Padding = new DevExpress.XtraPrinting.PaddingInfo(2, 2, 0, 0, 96F);
            this.xrTableDetail.Rows.AddRange(new DevExpress.XtraReports.UI.XRTableRow[] {
            this.xrTableRowDetail});
            this.xrTableDetail.SizeF = new System.Drawing.SizeF(630.0001F, 25F);
            this.xrTableDetail.StylePriority.UseBorders = false;
            // 
            // xrTableRowDetail
            // 
            this.xrTableRowDetail.Cells.AddRange(new DevExpress.XtraReports.UI.XRTableCell[] {
            this.cellNgay,
            this.cellTienNhap,
            this.cellTyLeNhap,
            this.cellTienXuat,
            this.cellTyLeXuat});
            this.xrTableRowDetail.Name = "xrTableRowDetail";
            this.xrTableRowDetail.Weight = 1D;
            // 
            // cellNgay
            // 
            this.cellNgay.ExpressionBindings.AddRange(new DevExpress.XtraReports.UI.ExpressionBinding[] {
            new DevExpress.XtraReports.UI.ExpressionBinding("BeforePrint", "Text", "{0:dd/MM/yyyy}")});
            this.cellNgay.ExpressionBindings.AddRange(new DevExpress.XtraReports.UI.ExpressionBinding[] {
            new DevExpress.XtraReports.UI.ExpressionBinding("BeforePrint", "Text", "[NGAY]")});
            this.cellNgay.Multiline = true;
            this.cellNgay.Name = "cellNgay";
            this.cellNgay.StylePriority.UseTextAlignment = false;
            this.cellNgay.Text = "NGÀY";
            this.cellNgay.TextAlignment = DevExpress.XtraPrinting.TextAlignment.MiddleCenter;
            this.cellNgay.Weight = 1.1538461538461537D;
            // 
            // cellTienNhap
            // 
            this.cellTienNhap.ExpressionBindings.AddRange(new DevExpress.XtraReports.UI.ExpressionBinding[] {
            new DevExpress.XtraReports.UI.ExpressionBinding("BeforePrint", "Text", "[NHAP]")});
            this.cellTienNhap.Multiline = true;
            this.cellTienNhap.Name = "cellTienNhap";
            this.cellTienNhap.StylePriority.UseTextAlignment = false;
            this.cellTienNhap.Text = "NHAP";
            this.cellTienNhap.TextAlignment = DevExpress.XtraPrinting.TextAlignment.MiddleRight;
            this.cellTienNhap.Weight = 1.3076923076923077D;
            // 
            // cellTyLeNhap
            // 
            this.cellTyLeNhap.ExpressionBindings.AddRange(new DevExpress.XtraReports.UI.ExpressionBinding[] {
            new DevExpress.XtraReports.UI.ExpressionBinding("BeforePrint", "Text", "[TYLENHAP]")});
            this.cellTyLeNhap.Multiline = true;
            this.cellTyLeNhap.Name = "cellTyLeNhap";
            this.cellTyLeNhap.StylePriority.UseTextAlignment = false;
            this.cellTyLeNhap.Text = "TYLENHAP";
            this.cellTyLeNhap.TextAlignment = DevExpress.XtraPrinting.TextAlignment.MiddleRight;
            this.cellTyLeNhap.Weight = 0.92307692307692313D;
            // 
            // cellTienXuat
            // 
            this.cellTienXuat.ExpressionBindings.AddRange(new DevExpress.XtraReports.UI.ExpressionBinding[] {
            new DevExpress.XtraReports.UI.ExpressionBinding("BeforePrint", "Text", "[XUAT]")});
            this.cellTienXuat.Multiline = true;
            this.cellTienXuat.Name = "cellTienXuat";
            this.cellTienXuat.StylePriority.UseTextAlignment = false;
            this.cellTienXuat.Text = "XUAT";
            this.cellTienXuat.TextAlignment = DevExpress.XtraPrinting.TextAlignment.MiddleRight;
            this.cellTienXuat.Weight = 1.3076923076923077D;
            // 
            // cellTyLeXuat
            // 
            this.cellTyLeXuat.ExpressionBindings.AddRange(new DevExpress.XtraReports.UI.ExpressionBinding[] {
            new DevExpress.XtraReports.UI.ExpressionBinding("BeforePrint", "Text", "[TYLEXUAT]")});
            this.cellTyLeXuat.Multiline = true;
            this.cellTyLeXuat.Name = "cellTyLeXuat";
            this.cellTyLeXuat.StylePriority.UseTextAlignment = false;
            this.cellTyLeXuat.Text = "TYLEXUAT";
            this.cellTyLeXuat.TextAlignment = DevExpress.XtraPrinting.TextAlignment.MiddleRight;
            this.cellTyLeXuat.Weight = 1.3076923076923077D;
            // 
            // ReportHeader
            // 
            this.ReportHeader.Controls.AddRange(new DevExpress.XtraReports.UI.XRControl[] {
            this.lblTieuDe,
            this.xrLabel1});
            this.ReportHeader.HeightF = 77.08334F;
            this.ReportHeader.Name = "ReportHeader";
            // 
            // lblTieuDe
            // 
            this.lblTieuDe.Font = new DevExpress.Drawing.DXFont("Arial", 11F, DevExpress.Drawing.DXFontStyle.Italic);
            this.lblTieuDe.LocationFloat = new DevExpress.Utils.PointFloat(10.00001F, 39.58334F);
            this.lblTieuDe.Multiline = true;
            this.lblTieuDe.Name = "lblTieuDe";
            this.lblTieuDe.Padding = new DevExpress.XtraPrinting.PaddingInfo(2, 2, 0, 0, 96F);
            this.lblTieuDe.SizeF = new System.Drawing.SizeF(630.0001F, 23F);
            this.lblTieuDe.StylePriority.UseFont = false;
            this.lblTieuDe.StylePriority.UseTextAlignment = false;
            this.lblTieuDe.Text = "TỪ NGÀY... ĐẾN NGÀY...";
            this.lblTieuDe.TextAlignment = DevExpress.XtraPrinting.TextAlignment.MiddleCenter;
            // 
            // xrLabel1
            // 
            this.xrLabel1.Font = new DevExpress.Drawing.DXFont("Arial", 16F, DevExpress.Drawing.DXFontStyle.Bold);
            this.xrLabel1.LocationFloat = new DevExpress.Utils.PointFloat(10.00001F, 10F);
            this.xrLabel1.Multiline = true;
            this.xrLabel1.Name = "xrLabel1";
            this.xrLabel1.Padding = new DevExpress.XtraPrinting.PaddingInfo(2, 2, 0, 0, 96F);
            this.xrLabel1.SizeF = new System.Drawing.SizeF(630.0001F, 29.58334F);
            this.xrLabel1.StylePriority.UseFont = false;
            this.xrLabel1.StylePriority.UseTextAlignment = false;
            this.xrLabel1.Text = "BẢNG TỔNG HỢP NHẬP XUẤT";
            this.xrLabel1.TextAlignment = DevExpress.XtraPrinting.TextAlignment.MiddleCenter;
            // 
            // PageHeader
            // 
            this.PageHeader.Controls.AddRange(new DevExpress.XtraReports.UI.XRControl[] {
            this.xrTableHeader});
            this.PageHeader.HeightF = 25F;
            this.PageHeader.Name = "PageHeader";
            // 
            // xrTableHeader
            // 
            this.xrTableHeader.Borders = ((DevExpress.XtraPrinting.BorderSide)((((DevExpress.XtraPrinting.BorderSide.Left | DevExpress.XtraPrinting.BorderSide.Top)
            | DevExpress.XtraPrinting.BorderSide.Right)
            | DevExpress.XtraPrinting.BorderSide.Bottom)));
            this.xrTableHeader.Font = new DevExpress.Drawing.DXFont("Arial", 9.75F, DevExpress.Drawing.DXFontStyle.Bold);
            this.xrTableHeader.LocationFloat = new DevExpress.Utils.PointFloat(10.00001F, 0F);
            this.xrTableHeader.Name = "xrTableHeader";
            this.xrTableHeader.Padding = new DevExpress.XtraPrinting.PaddingInfo(2, 2, 0, 0, 96F);
            this.xrTableHeader.Rows.AddRange(new DevExpress.XtraReports.UI.XRTableRow[] {
            this.xrTableRowHeader});
            this.xrTableHeader.SizeF = new System.Drawing.SizeF(630.0001F, 25F);
            this.xrTableHeader.StylePriority.UseBorders = false;
            this.xrTableHeader.StylePriority.UseFont = false;
            this.xrTableHeader.StylePriority.UseTextAlignment = false;
            this.xrTableHeader.TextAlignment = DevExpress.XtraPrinting.TextAlignment.MiddleCenter;
            // 
            // xrTableRowHeader
            // 
            this.xrTableRowHeader.Cells.AddRange(new DevExpress.XtraReports.UI.XRTableCell[] {
            this.xrTableCell1,
            this.xrTableCell2,
            this.xrTableCell3,
            this.xrTableCell4,
            this.xrTableCell5});
            this.xrTableRowHeader.Name = "xrTableRowHeader";
            this.xrTableRowHeader.Weight = 1D;
            // 
            // xrTableCell1
            // 
            this.xrTableCell1.Multiline = true;
            this.xrTableCell1.Name = "xrTableCell1";
            this.xrTableCell1.Text = "NGÀY";
            this.xrTableCell1.Weight = 1.1538461538461537D;
            // 
            // xrTableCell2
            // 
            this.xrTableCell2.Multiline = true;
            this.xrTableCell2.Name = "xrTableCell2";
            this.xrTableCell2.Text = "NHẬP";
            this.xrTableCell2.Weight = 1.3076923076923077D;
            // 
            // xrTableCell3
            // 
            this.xrTableCell3.Multiline = true;
            this.xrTableCell3.Name = "xrTableCell3";
            this.xrTableCell3.Text = "TỶ LỆ";
            this.xrTableCell3.Weight = 0.92307692307692313D;
            // 
            // xrTableCell4
            // 
            this.xrTableCell4.Multiline = true;
            this.xrTableCell4.Name = "xrTableCell4";
            this.xrTableCell4.Text = "XUẤT";
            this.xrTableCell4.Weight = 1.3076923076923077D;
            // 
            // xrTableCell5
            // 
            this.xrTableCell5.Multiline = true;
            this.xrTableCell5.Name = "xrTableCell5";
            this.xrTableCell5.Text = "TỶ LỆ";
            this.xrTableCell5.Weight = 1.3076923076923077D;
            // 
            // ReportFooter
            // 
            this.ReportFooter.Controls.AddRange(new DevExpress.XtraReports.UI.XRControl[] {
            this.xrTableFooter});
            this.ReportFooter.HeightF = 35.41667F;
            this.ReportFooter.Name = "ReportFooter";
            // 
            // xrTableFooter
            // 
            this.xrTableFooter.Borders = ((DevExpress.XtraPrinting.BorderSide)((((DevExpress.XtraPrinting.BorderSide.Left | DevExpress.XtraPrinting.BorderSide.Top)
            | DevExpress.XtraPrinting.BorderSide.Right)
            | DevExpress.XtraPrinting.BorderSide.Bottom)));
            this.xrTableFooter.Font = new DevExpress.Drawing.DXFont("Arial", 9.75F, DevExpress.Drawing.DXFontStyle.Bold);
            this.xrTableFooter.LocationFloat = new DevExpress.Utils.PointFloat(10.00001F, 0F);
            this.xrTableFooter.Name = "xrTableFooter";
            this.xrTableFooter.Padding = new DevExpress.XtraPrinting.PaddingInfo(2, 2, 0, 0, 96F);
            this.xrTableFooter.Rows.AddRange(new DevExpress.XtraReports.UI.XRTableRow[] {
            this.xrTableRowFooter});
            this.xrTableFooter.SizeF = new System.Drawing.SizeF(630.0001F, 25F);
            this.xrTableFooter.StylePriority.UseBorders = false;
            this.xrTableFooter.StylePriority.UseFont = false;
            // 
            // xrTableRowFooter
            // 
            this.xrTableRowFooter.Cells.AddRange(new DevExpress.XtraReports.UI.XRTableCell[] {
            this.xrTableCell11,
            this.cellTongTienNhap,
            this.xrTableCell13,
            this.cellTongTienXuat,
            this.xrTableCell15});
            this.xrTableRowFooter.Name = "xrTableRowFooter";
            this.xrTableRowFooter.Weight = 1D;
            // 
            // xrTableCell11
            // 
            this.xrTableCell11.Multiline = true;
            this.xrTableCell11.Name = "xrTableCell11";
            this.xrTableCell11.StylePriority.UseTextAlignment = false;
            this.xrTableCell11.Text = "CỘNG";
            this.xrTableCell11.TextAlignment = DevExpress.XtraPrinting.TextAlignment.MiddleCenter;
            this.xrTableCell11.Weight = 1.1538461538461537D;
            // 
            // cellTongTienNhap
            // 
            this.cellTongTienNhap.ExpressionBindings.AddRange(new DevExpress.XtraReports.UI.ExpressionBinding[] {
            new DevExpress.XtraReports.UI.ExpressionBinding("BeforePrint", "Text", "sumSum([NHAP])")});
            this.cellTongTienNhap.Multiline = true;
            this.cellTongTienNhap.Name = "cellTongTienNhap";
            this.cellTongTienNhap.StylePriority.UseTextAlignment = false;
            xrSummary1.Running = DevExpress.XtraReports.UI.SummaryRunning.Report;
            this.cellTongTienNhap.Summary = xrSummary1;
            this.cellTongTienNhap.Text = "cellTongTienNhap";
            this.cellTongTienNhap.TextAlignment = DevExpress.XtraPrinting.TextAlignment.MiddleRight;
            this.cellTongTienNhap.Weight = 1.3076923076923077D;
            // 
            // xrTableCell13
            // 
            this.xrTableCell13.Multiline = true;
            this.xrTableCell13.Name = "xrTableCell13";
            this.xrTableCell13.Weight = 0.92307692307692313D;
            // 
            // cellTongTienXuat
            // 
            this.cellTongTienXuat.ExpressionBindings.AddRange(new DevExpress.XtraReports.UI.ExpressionBinding[] {
            new DevExpress.XtraReports.UI.ExpressionBinding("BeforePrint", "Text", "sumSum([XUAT])")});
            this.cellTongTienXuat.Multiline = true;
            this.cellTongTienXuat.Name = "cellTongTienXuat";
            this.cellTongTienXuat.StylePriority.UseTextAlignment = false;
            xrSummary2.Running = DevExpress.XtraReports.UI.SummaryRunning.Report;
            this.cellTongTienXuat.Summary = xrSummary2;
            this.cellTongTienXuat.Text = "cellTongTienXuat";
            this.cellTongTienXuat.TextAlignment = DevExpress.XtraPrinting.TextAlignment.MiddleRight;
            this.cellTongTienXuat.Weight = 1.3076923076923077D;
            // 
            // xrTableCell15
            // 
            this.xrTableCell15.Multiline = true;
            this.xrTableCell15.Name = "xrTableCell15";
            this.xrTableCell15.Weight = 1.3076923076923077D;
            // 
            // RptTongHopNhapXuat
            // 
            this.Bands.AddRange(new DevExpress.XtraReports.UI.Band[] {
            this.TopMargin,
            this.BottomMargin,
            this.Detail,
            this.ReportHeader,
            this.PageHeader,
            this.ReportFooter});
            this.Font = new DevExpress.Drawing.DXFont("Arial", 9.75F);
            this.Version = "25.2";
            ((System.ComponentModel.ISupportInitialize)(this.xrTableDetail)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.xrTableHeader)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.xrTableFooter)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this)).EndInit();

        }

        #endregion

        private DevExpress.XtraReports.UI.TopMarginBand TopMargin;
        private DevExpress.XtraReports.UI.BottomMarginBand BottomMargin;
        private DevExpress.XtraReports.UI.DetailBand Detail;
        private DevExpress.XtraReports.UI.ReportHeaderBand ReportHeader;
        private DevExpress.XtraReports.UI.XRLabel xrLabel1;
        private DevExpress.XtraReports.UI.XRLabel lblTieuDe;
        private DevExpress.XtraReports.UI.PageHeaderBand PageHeader;
        private DevExpress.XtraReports.UI.XRTable xrTableHeader;
        private DevExpress.XtraReports.UI.XRTableRow xrTableRowHeader;
        private DevExpress.XtraReports.UI.XRTableCell xrTableCell1;
        private DevExpress.XtraReports.UI.XRTableCell xrTableCell2;
        private DevExpress.XtraReports.UI.XRTableCell xrTableCell3;
        private DevExpress.XtraReports.UI.XRTableCell xrTableCell4;
        private DevExpress.XtraReports.UI.XRTableCell xrTableCell5;
        private DevExpress.XtraReports.UI.XRTable xrTableDetail;
        private DevExpress.XtraReports.UI.XRTableRow xrTableRowDetail;
        private DevExpress.XtraReports.UI.XRTableCell cellNgay;
        private DevExpress.XtraReports.UI.XRTableCell cellTienNhap;
        private DevExpress.XtraReports.UI.XRTableCell cellTyLeNhap;
        private DevExpress.XtraReports.UI.XRTableCell cellTienXuat;
        private DevExpress.XtraReports.UI.XRTableCell cellTyLeXuat;
        private DevExpress.XtraReports.UI.ReportFooterBand ReportFooter;
        private DevExpress.XtraReports.UI.XRTable xrTableFooter;
        private DevExpress.XtraReports.UI.XRTableRow xrTableRowFooter;
        private DevExpress.XtraReports.UI.XRTableCell xrTableCell11;
        private DevExpress.XtraReports.UI.XRTableCell cellTongTienNhap;
        private DevExpress.XtraReports.UI.XRTableCell xrTableCell13;
        private DevExpress.XtraReports.UI.XRTableCell cellTongTienXuat;
    
        private DevExpress.XtraReports.UI.XRTableCell xrTableCell15;
    }
}