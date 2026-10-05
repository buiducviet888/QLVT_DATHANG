namespace QLVT_DATHANG
{
    partial class rptBangKeChiTietNhapXuat
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary> 
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            DevExpress.XtraReports.UI.XRWatermark xrWatermark1 = new DevExpress.XtraReports.UI.XRWatermark();
            this.TopMargin = new DevExpress.XtraReports.UI.TopMarginBand();
            this.BottomMargin = new DevExpress.XtraReports.UI.BottomMarginBand();
            this.ReportHeader = new DevExpress.XtraReports.UI.ReportHeaderBand();
            this.lblTieuDe = new DevExpress.XtraReports.UI.XRLabel();
            this.lblThoiGian = new DevExpress.XtraReports.UI.XRLabel();
            this.PageHeader = new DevExpress.XtraReports.UI.PageHeaderBand();
            this.xrTable1 = new DevExpress.XtraReports.UI.XRTable();
            this.xrTableRow1 = new DevExpress.XtraReports.UI.XRTableRow();
            this.xrTableCell1 = new DevExpress.XtraReports.UI.XRTableCell();
            this.xrTableCell2 = new DevExpress.XtraReports.UI.XRTableCell();
            this.xrTableCell3 = new DevExpress.XtraReports.UI.XRTableCell();
            this.xrTableCell4 = new DevExpress.XtraReports.UI.XRTableCell();
            this.Detail = new DevExpress.XtraReports.UI.DetailBand();
            this.xrTable2 = new DevExpress.XtraReports.UI.XRTable();
            this.xrTableRow2 = new DevExpress.XtraReports.UI.XRTableRow();
            this.cellThangNam = new DevExpress.XtraReports.UI.XRTableCell();
            this.cellTenVT = new DevExpress.XtraReports.UI.XRTableCell();
            this.cellTongSL = new DevExpress.XtraReports.UI.XRTableCell();
            this.cellTongTriGia = new DevExpress.XtraReports.UI.XRTableCell();
            ((System.ComponentModel.ISupportInitialize)(this.xrTable1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.xrTable2)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this)).BeginInit();
            // 
            // TopMargin
            // 
            this.TopMargin.HeightF = 50F;
            this.TopMargin.Name = "TopMargin";
            // 
            // BottomMargin
            // 
            this.BottomMargin.HeightF = 50F;
            this.BottomMargin.Name = "BottomMargin";
            // 
            // ReportHeader
            // 
            this.ReportHeader.Controls.AddRange(new DevExpress.XtraReports.UI.XRControl[] {
            this.lblTieuDe,
            this.lblThoiGian});
            this.ReportHeader.HeightF = 75F;
            this.ReportHeader.Name = "ReportHeader";
            // 
            // lblTieuDe
            // 
            this.lblTieuDe.Font = new DevExpress.Drawing.DXFont("Arial", 15F, ((DevExpress.Drawing.DXFontStyle)((DevExpress.Drawing.DXFontStyle.Bold | DevExpress.Drawing.DXFontStyle.Italic))));
            this.lblTieuDe.LocationFloat = new DevExpress.Utils.PointFloat(0F, 10F);
            this.lblTieuDe.Multiline = true;
            this.lblTieuDe.Name = "lblTieuDe";
            this.lblTieuDe.Padding = new DevExpress.XtraPrinting.PaddingInfo(2, 2, 0, 0, 100F);
            this.lblTieuDe.SizeF = new System.Drawing.SizeF(650F, 30F);
            this.lblTieuDe.StylePriority.UseFont = false;
            this.lblTieuDe.StylePriority.UseTextAlignment = false;
            this.lblTieuDe.Text = "BẢNG KÊ CHI TIẾT SỐ LƯỢNG – TRỊ GIÁ HÀNG NHẬP";
            this.lblTieuDe.TextAlignment = DevExpress.XtraPrinting.TextAlignment.MiddleCenter;
            // 
            // lblThoiGian
            // 
            this.lblThoiGian.Font = new DevExpress.Drawing.DXFont("Arial", 10F, DevExpress.Drawing.DXFontStyle.Italic);
            this.lblThoiGian.LocationFloat = new DevExpress.Utils.PointFloat(0F, 42F);
            this.lblThoiGian.Multiline = true;
            this.lblThoiGian.Name = "lblThoiGian";
            this.lblThoiGian.Padding = new DevExpress.XtraPrinting.PaddingInfo(2, 2, 0, 0, 100F);
            this.lblThoiGian.SizeF = new System.Drawing.SizeF(650F, 23F);
            this.lblThoiGian.StylePriority.UseFont = false;
            this.lblThoiGian.StylePriority.UseTextAlignment = false;
            this.lblThoiGian.Text = "Từ ngày ... đến ngày ...";
            this.lblThoiGian.TextAlignment = DevExpress.XtraPrinting.TextAlignment.MiddleCenter;
            // 
            // PageHeader
            // 
            this.PageHeader.Controls.AddRange(new DevExpress.XtraReports.UI.XRControl[] {
            this.xrTable1});
            this.PageHeader.HeightF = 28F;
            this.PageHeader.Name = "PageHeader";
            // 
            // xrTable1
            // 
            this.xrTable1.Borders = ((DevExpress.XtraPrinting.BorderSide)((((DevExpress.XtraPrinting.BorderSide.Left | DevExpress.XtraPrinting.BorderSide.Top)
            | DevExpress.XtraPrinting.BorderSide.Right)
            | DevExpress.XtraPrinting.BorderSide.Bottom)));
            this.xrTable1.Font = new DevExpress.Drawing.DXFont("Arial", 9.75F, DevExpress.Drawing.DXFontStyle.Bold);
            this.xrTable1.LocationFloat = new DevExpress.Utils.PointFloat(0F, 0F);
            this.xrTable1.Name = "xrTable1";
            this.xrTable1.Padding = new DevExpress.XtraPrinting.PaddingInfo(2, 2, 0, 0, 100F);
            this.xrTable1.Rows.AddRange(new DevExpress.XtraReports.UI.XRTableRow[] {
            this.xrTableRow1});
            this.xrTable1.SizeF = new System.Drawing.SizeF(650F, 28F);
            this.xrTable1.StylePriority.UseBorders = false;
            this.xrTable1.StylePriority.UseFont = false;
            this.xrTable1.StylePriority.UseTextAlignment = false;
            this.xrTable1.TextAlignment = DevExpress.XtraPrinting.TextAlignment.MiddleCenter;
            // 
            // xrTableRow1
            // 
            this.xrTableRow1.Cells.AddRange(new DevExpress.XtraReports.UI.XRTableCell[] {
            this.xrTableCell1,
            this.xrTableCell2,
            this.xrTableCell3,
            this.xrTableCell4});
            this.xrTableRow1.Name = "xrTableRow1";
            this.xrTableRow1.Weight = 1D;
            // 
            // xrTableCell1
            // 
            this.xrTableCell1.Multiline = true;
            this.xrTableCell1.Name = "xrTableCell1";
            this.xrTableCell1.Text = "THÁNG/NĂM";
            this.xrTableCell1.Weight = 1D;
            // 
            // xrTableCell2
            // 
            this.xrTableCell2.Multiline = true;
            this.xrTableCell2.Name = "xrTableCell2";
            this.xrTableCell2.Text = "TÊN VẬT TƯ";
            this.xrTableCell2.Weight = 2.4D;
            // 
            // xrTableCell3
            // 
            this.xrTableCell3.Multiline = true;
            this.xrTableCell3.Name = "xrTableCell3";
            this.xrTableCell3.Text = "TỔNG SỐ LƯỢNG";
            this.xrTableCell3.Weight = 1.3D;
            // 
            // xrTableCell4
            // 
            this.xrTableCell4.Multiline = true;
            this.xrTableCell4.Name = "xrTableCell4";
            this.xrTableCell4.Text = "TỔNG TRỊ GIÁ";
            this.xrTableCell4.Weight = 1.8D;
            // 
            // Detail
            // 
            this.Detail.Controls.AddRange(new DevExpress.XtraReports.UI.XRControl[] {
            this.xrTable2});
            this.Detail.HeightF = 25F;
            this.Detail.Name = "Detail";
            // 
            // xrTable2
            // 
            this.xrTable2.Borders = ((DevExpress.XtraPrinting.BorderSide)((((DevExpress.XtraPrinting.BorderSide.Left | DevExpress.XtraPrinting.BorderSide.Top)
            | DevExpress.XtraPrinting.BorderSide.Right)
            | DevExpress.XtraPrinting.BorderSide.Bottom)));
            this.xrTable2.LocationFloat = new DevExpress.Utils.PointFloat(0F, 0F);
            this.xrTable2.Name = "xrTable2";
            this.xrTable2.Padding = new DevExpress.XtraPrinting.PaddingInfo(2, 2, 0, 0, 100F);
            this.xrTable2.Rows.AddRange(new DevExpress.XtraReports.UI.XRTableRow[] {
            this.xrTableRow2});
            this.xrTable2.SizeF = new System.Drawing.SizeF(650F, 25F);
            this.xrTable2.StylePriority.UseBorders = false;
            this.xrTable2.StylePriority.UseTextAlignment = false;
            this.xrTable2.TextAlignment = DevExpress.XtraPrinting.TextAlignment.MiddleCenter;
            // 
            // xrTableRow2
            // 
            this.xrTableRow2.Cells.AddRange(new DevExpress.XtraReports.UI.XRTableCell[] {
            this.cellThangNam,
            this.cellTenVT,
            this.cellTongSL,
            this.cellTongTriGia});
            this.xrTableRow2.Name = "xrTableRow2";
            this.xrTableRow2.Weight = 1D;
            // 
            // cellThangNam
            // 
            this.cellThangNam.Multiline = true;
            this.cellThangNam.Name = "cellThangNam";
            this.cellThangNam.StylePriority.UseTextAlignment = false;
            this.cellThangNam.TextAlignment = DevExpress.XtraPrinting.TextAlignment.MiddleCenter;
            this.cellThangNam.Weight = 1D;
            // 
            // cellTenVT
            // 
            this.cellTenVT.Multiline = true;
            this.cellTenVT.Name = "cellTenVT";
            this.cellTenVT.Padding = new DevExpress.XtraPrinting.PaddingInfo(8, 2, 0, 0, 100F);
            this.cellTenVT.StylePriority.UsePadding = false;
            this.cellTenVT.StylePriority.UseTextAlignment = false;
            this.cellTenVT.TextAlignment = DevExpress.XtraPrinting.TextAlignment.MiddleLeft;
            this.cellTenVT.Weight = 2.4D;
            // 
            // cellTongSL
            // 
            this.cellTongSL.Multiline = true;
            this.cellTongSL.Name = "cellTongSL";
            this.cellTongSL.Padding = new DevExpress.XtraPrinting.PaddingInfo(2, 8, 0, 0, 100F);
            this.cellTongSL.StylePriority.UsePadding = false;
            this.cellTongSL.StylePriority.UseTextAlignment = false;
            this.cellTongSL.TextAlignment = DevExpress.XtraPrinting.TextAlignment.MiddleRight;
            this.cellTongSL.Weight = 1.3D;
            // 
            // cellTongTriGia
            // 
            this.cellTongTriGia.Multiline = true;
            this.cellTongTriGia.Name = "cellTongTriGia";
            this.cellTongTriGia.Padding = new DevExpress.XtraPrinting.PaddingInfo(2, 8, 0, 0, 100F);
            this.cellTongTriGia.StylePriority.UsePadding = false;
            this.cellTongTriGia.StylePriority.UseTextAlignment = false;
            this.cellTongTriGia.TextAlignment = DevExpress.XtraPrinting.TextAlignment.MiddleRight;
            this.cellTongTriGia.Weight = 1.8D;
            // 
            // rptBangKeChiTietNhapXuat
            // 
            this.Bands.AddRange(new DevExpress.XtraReports.UI.Band[] {
            this.TopMargin,
            this.BottomMargin,
            this.ReportHeader,
            this.PageHeader,
            this.Detail});
            this.Font = new DevExpress.Drawing.DXFont("Arial", 9.75F);
            this.Margins = new DevExpress.Drawing.DXMargins(100F, 100F, 50F, 50F);
            this.Version = "25.2";
            xrWatermark1.Id = "Watermark1";
            this.Watermarks.AddRange(new DevExpress.XtraPrinting.Drawing.Watermark[] {
            xrWatermark1});
            ((System.ComponentModel.ISupportInitialize)(this.xrTable1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.xrTable2)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this)).EndInit();
        }

        #endregion

        private DevExpress.XtraReports.UI.TopMarginBand TopMargin;
        private DevExpress.XtraReports.UI.BottomMarginBand BottomMargin;
        private DevExpress.XtraReports.UI.ReportHeaderBand ReportHeader;
        private DevExpress.XtraReports.UI.XRLabel lblThoiGian;
        private DevExpress.XtraReports.UI.XRLabel lblTieuDe;
        private DevExpress.XtraReports.UI.PageHeaderBand PageHeader;
        private DevExpress.XtraReports.UI.XRTable xrTable1;
        private DevExpress.XtraReports.UI.XRTableRow xrTableRow1;
        private DevExpress.XtraReports.UI.XRTableCell xrTableCell1;
        private DevExpress.XtraReports.UI.XRTableCell xrTableCell2;
        private DevExpress.XtraReports.UI.XRTableCell xrTableCell3;
        private DevExpress.XtraReports.UI.XRTableCell xrTableCell4;
        private DevExpress.XtraReports.UI.DetailBand Detail;
        private DevExpress.XtraReports.UI.XRTable xrTable2;
        private DevExpress.XtraReports.UI.XRTableRow xrTableRow2;
        private DevExpress.XtraReports.UI.XRTableCell cellThangNam;
        private DevExpress.XtraReports.UI.XRTableCell cellTenVT;
        private DevExpress.XtraReports.UI.XRTableCell cellTongSL;
        private DevExpress.XtraReports.UI.XRTableCell cellTongTriGia;
    }
}