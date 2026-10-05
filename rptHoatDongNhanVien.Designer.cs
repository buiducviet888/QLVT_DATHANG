namespace QLVT_DATHANG
{
    partial class rptHoatDongNhanVien
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
            this.TopMargin = new DevExpress.XtraReports.UI.TopMarginBand();
            this.BottomMargin = new DevExpress.XtraReports.UI.BottomMarginBand();
            this.ReportHeader = new DevExpress.XtraReports.UI.ReportHeaderBand();
            this.GroupHeaderThang = new DevExpress.XtraReports.UI.GroupHeaderBand();
            this.Detail = new DevExpress.XtraReports.UI.DetailBand();
            this.GroupFooterThang = new DevExpress.XtraReports.UI.GroupFooterBand();

            this.lblTieuDe = new DevExpress.XtraReports.UI.XRLabel();
            this.lblThongTinNV = new DevExpress.XtraReports.UI.XRLabel();
            this.lblThoiGian = new DevExpress.XtraReports.UI.XRLabel();

            this.lblThangNamHeader = new DevExpress.XtraReports.UI.XRLabel();
            this.tableHeaderCols = new DevExpress.XtraReports.UI.XRTable();
            this.rowHeaderCols = new DevExpress.XtraReports.UI.XRTableRow();
            this.hNgay = new DevExpress.XtraReports.UI.XRTableCell();
            this.hSoPhieu = new DevExpress.XtraReports.UI.XRTableCell();
            this.hLoaiPhieu = new DevExpress.XtraReports.UI.XRTableCell();
            this.hDoiTac = new DevExpress.XtraReports.UI.XRTableCell();
            this.hTenKho = new DevExpress.XtraReports.UI.XRTableCell();
            this.hTenVT = new DevExpress.XtraReports.UI.XRTableCell();
            this.hSoLuong = new DevExpress.XtraReports.UI.XRTableCell();
            this.hDonGia = new DevExpress.XtraReports.UI.XRTableCell();
            this.hTriGia = new DevExpress.XtraReports.UI.XRTableCell();

            this.tableDetail = new DevExpress.XtraReports.UI.XRTable();
            this.rowDetail = new DevExpress.XtraReports.UI.XRTableRow();
            this.cellNgay = new DevExpress.XtraReports.UI.XRTableCell();
            this.cellSoPhieu = new DevExpress.XtraReports.UI.XRTableCell();
            this.cellLoaiPhieu = new DevExpress.XtraReports.UI.XRTableCell();
            this.cellDoiTac = new DevExpress.XtraReports.UI.XRTableCell();
            this.cellTenKho = new DevExpress.XtraReports.UI.XRTableCell();
            this.cellTenVT = new DevExpress.XtraReports.UI.XRTableCell();
            this.cellSoLuong = new DevExpress.XtraReports.UI.XRTableCell();
            this.cellDonGia = new DevExpress.XtraReports.UI.XRTableCell();
            this.cellTriGia = new DevExpress.XtraReports.UI.XRTableCell();

            this.lblNhanTongCong = new DevExpress.XtraReports.UI.XRLabel();
            this.cellTongTienThang = new DevExpress.XtraReports.UI.XRLabel();

            ((System.ComponentModel.ISupportInitialize)(this.tableHeaderCols)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.tableDetail)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this)).BeginInit();

            this.TopMargin.HeightF = 40F;
            this.BottomMargin.HeightF = 40F;

            // ReportHeader
            this.ReportHeader.HeightF = 85F;
            this.lblTieuDe.Text = "HOẠT ĐỘNG CỦA NHÂN VIÊN";
            this.lblTieuDe.Font = new DevExpress.Drawing.DXFont("Times New Roman", 15F, DevExpress.Drawing.DXFontStyle.Bold);
            this.lblTieuDe.TextAlignment = DevExpress.XtraPrinting.TextAlignment.MiddleCenter;
            this.lblTieuDe.SizeF = new System.Drawing.SizeF(750F, 30F);
            this.lblTieuDe.LocationFloat = new DevExpress.Utils.PointFloat(0F, 5F);

            this.lblThongTinNV.Font = new DevExpress.Drawing.DXFont("Times New Roman", 11F, DevExpress.Drawing.DXFontStyle.Bold);
            this.lblThongTinNV.SizeF = new System.Drawing.SizeF(750F, 22F);
            this.lblThongTinNV.LocationFloat = new DevExpress.Utils.PointFloat(0F, 35F);
            this.lblThongTinNV.TextAlignment = DevExpress.XtraPrinting.TextAlignment.MiddleCenter;

            this.lblThoiGian.Font = new DevExpress.Drawing.DXFont("Times New Roman", 10F, DevExpress.Drawing.DXFontStyle.Italic);
            this.lblThoiGian.SizeF = new System.Drawing.SizeF(750F, 20F);
            this.lblThoiGian.LocationFloat = new DevExpress.Utils.PointFloat(0F, 57F);
            this.lblThoiGian.TextAlignment = DevExpress.XtraPrinting.TextAlignment.MiddleCenter;

            this.ReportHeader.Controls.AddRange(new DevExpress.XtraReports.UI.XRControl[] {
                this.lblTieuDe, this.lblThongTinNV, this.lblThoiGian
            });

            // GroupHeader: gom theo THANGNAM
            this.GroupHeaderThang.HeightF = 55F;
            this.GroupHeaderThang.GroupFields.AddRange(new DevExpress.XtraReports.UI.GroupField[] {
                new DevExpress.XtraReports.UI.GroupField("THANGNAM", DevExpress.XtraReports.UI.XRColumnSortOrder.Ascending)
            });

            this.lblThangNamHeader.Font = new DevExpress.Drawing.DXFont("Times New Roman", 11F, DevExpress.Drawing.DXFontStyle.Bold);
            this.lblThangNamHeader.SizeF = new System.Drawing.SizeF(750F, 25F);
            this.lblThangNamHeader.LocationFloat = new DevExpress.Utils.PointFloat(0F, 5F);
            this.lblThangNamHeader.BackColor = System.Drawing.Color.LightSkyBlue;
            this.lblThangNamHeader.Borders = DevExpress.XtraPrinting.BorderSide.All;
            this.lblThangNamHeader.Padding = new DevExpress.XtraPrinting.PaddingInfo(5, 5, 0, 0, 100F);
            this.lblThangNamHeader.TextAlignment = DevExpress.XtraPrinting.TextAlignment.MiddleLeft;

            this.tableHeaderCols.SizeF = new System.Drawing.SizeF(750F, 25F);
            this.tableHeaderCols.LocationFloat = new DevExpress.Utils.PointFloat(0F, 30F);
            this.tableHeaderCols.Font = new DevExpress.Drawing.DXFont("Times New Roman", 9F, DevExpress.Drawing.DXFontStyle.Bold);
            this.tableHeaderCols.BackColor = System.Drawing.Color.Gainsboro;

            this.rowHeaderCols.Cells.AddRange(new DevExpress.XtraReports.UI.XRTableCell[] {
                this.hNgay, this.hSoPhieu, this.hLoaiPhieu, this.hDoiTac, this.hTenKho, this.hTenVT, this.hSoLuong, this.hDonGia, this.hTriGia
            });
            this.hNgay.WidthF = 75F; this.hNgay.Text = "Ngày"; this.hNgay.TextAlignment = DevExpress.XtraPrinting.TextAlignment.MiddleCenter;
            this.hSoPhieu.WidthF = 75F; this.hSoPhieu.Text = "Số phiếu"; this.hSoPhieu.TextAlignment = DevExpress.XtraPrinting.TextAlignment.MiddleCenter;
            this.hLoaiPhieu.WidthF = 80F; this.hLoaiPhieu.Text = "Loại"; this.hLoaiPhieu.TextAlignment = DevExpress.XtraPrinting.TextAlignment.MiddleCenter;
            this.hDoiTac.WidthF = 110F; this.hDoiTac.Text = "Khách hàng"; this.hDoiTac.TextAlignment = DevExpress.XtraPrinting.TextAlignment.MiddleCenter;
            this.hTenKho.WidthF = 95F; this.hTenKho.Text = "Tên kho"; this.hTenKho.TextAlignment = DevExpress.XtraPrinting.TextAlignment.MiddleCenter;
            this.hTenVT.WidthF = 115F; this.hTenVT.Text = "Tên vật tư"; this.hTenVT.TextAlignment = DevExpress.XtraPrinting.TextAlignment.MiddleCenter;
            this.hSoLuong.WidthF = 55F; this.hSoLuong.Text = "SL"; this.hSoLuong.TextAlignment = DevExpress.XtraPrinting.TextAlignment.MiddleCenter;
            this.hDonGia.WidthF = 70F; this.hDonGia.Text = "Đơn giá"; this.hDonGia.TextAlignment = DevExpress.XtraPrinting.TextAlignment.MiddleCenter;
            this.hTriGia.WidthF = 75F; this.hTriGia.Text = "Thành tiền"; this.hTriGia.TextAlignment = DevExpress.XtraPrinting.TextAlignment.MiddleCenter;

            this.tableHeaderCols.Rows.Add(this.rowHeaderCols);
            this.GroupHeaderThang.Controls.AddRange(new DevExpress.XtraReports.UI.XRControl[] {
                this.lblThangNamHeader, this.tableHeaderCols
            });

            // Detail
            this.Detail.HeightF = 22F;
            this.tableDetail.SizeF = new System.Drawing.SizeF(750F, 22F);
            this.tableDetail.LocationFloat = new DevExpress.Utils.PointFloat(0F, 0F);
            this.tableDetail.Font = new DevExpress.Drawing.DXFont("Times New Roman", 9F);

            this.rowDetail.Cells.AddRange(new DevExpress.XtraReports.UI.XRTableCell[] {
                this.cellNgay, this.cellSoPhieu, this.cellLoaiPhieu, this.cellDoiTac, this.cellTenKho, this.cellTenVT, this.cellSoLuong, this.cellDonGia, this.cellTriGia
            });
            this.cellNgay.WidthF = 75F; this.cellNgay.TextAlignment = DevExpress.XtraPrinting.TextAlignment.MiddleCenter;
            this.cellSoPhieu.WidthF = 75F; this.cellSoPhieu.TextAlignment = DevExpress.XtraPrinting.TextAlignment.MiddleCenter;
            this.cellLoaiPhieu.WidthF = 80F; this.cellLoaiPhieu.TextAlignment = DevExpress.XtraPrinting.TextAlignment.MiddleLeft; this.cellLoaiPhieu.Padding = new DevExpress.XtraPrinting.PaddingInfo(3, 3, 0, 0, 100F);
            this.cellDoiTac.WidthF = 110F; this.cellDoiTac.TextAlignment = DevExpress.XtraPrinting.TextAlignment.MiddleLeft; this.cellDoiTac.Padding = new DevExpress.XtraPrinting.PaddingInfo(3, 3, 0, 0, 100F);
            this.cellTenKho.WidthF = 95F; this.cellTenKho.TextAlignment = DevExpress.XtraPrinting.TextAlignment.MiddleLeft; this.cellTenKho.Padding = new DevExpress.XtraPrinting.PaddingInfo(3, 3, 0, 0, 100F);
            this.cellTenVT.WidthF = 115F; this.cellTenVT.TextAlignment = DevExpress.XtraPrinting.TextAlignment.MiddleLeft; this.cellTenVT.Padding = new DevExpress.XtraPrinting.PaddingInfo(3, 3, 0, 0, 100F);
            this.cellSoLuong.WidthF = 55F; this.cellSoLuong.TextAlignment = DevExpress.XtraPrinting.TextAlignment.MiddleRight; this.cellSoLuong.Padding = new DevExpress.XtraPrinting.PaddingInfo(3, 3, 0, 0, 100F);
            this.cellDonGia.WidthF = 70F; this.cellDonGia.TextAlignment = DevExpress.XtraPrinting.TextAlignment.MiddleRight; this.cellDonGia.Padding = new DevExpress.XtraPrinting.PaddingInfo(3, 3, 0, 0, 100F);
            this.cellTriGia.WidthF = 75F; this.cellTriGia.TextAlignment = DevExpress.XtraPrinting.TextAlignment.MiddleRight; this.cellTriGia.Padding = new DevExpress.XtraPrinting.PaddingInfo(3, 3, 0, 0, 100F);

            this.tableDetail.Rows.Add(this.rowDetail);
            this.Detail.Controls.Add(this.tableDetail);

            // GroupFooterThang (Dòng Tổng cộng theo tháng)
            this.GroupFooterThang.HeightF = 35F;

            this.lblNhanTongCong.Text = "Tổng cộng";
            this.lblNhanTongCong.Font = new DevExpress.Drawing.DXFont("Times New Roman", 10F, DevExpress.Drawing.DXFontStyle.Bold);
            this.lblNhanTongCong.SizeF = new System.Drawing.SizeF(150F, 23F);
            this.lblNhanTongCong.LocationFloat = new DevExpress.Utils.PointFloat(445F, 5F);
            this.lblNhanTongCong.TextAlignment = DevExpress.XtraPrinting.TextAlignment.MiddleRight;

            this.cellTongTienThang.Font = new DevExpress.Drawing.DXFont("Times New Roman", 10F, DevExpress.Drawing.DXFontStyle.Bold);
            this.cellTongTienThang.SizeF = new System.Drawing.SizeF(150F, 23F);
            this.cellTongTienThang.LocationFloat = new DevExpress.Utils.PointFloat(600F, 5F); // Canh thẳng cột Thành tiền
            this.cellTongTienThang.TextAlignment = DevExpress.XtraPrinting.TextAlignment.MiddleRight;
            this.cellTongTienThang.Padding = new DevExpress.XtraPrinting.PaddingInfo(3, 3, 0, 0, 100F);

            this.GroupFooterThang.Controls.AddRange(new DevExpress.XtraReports.UI.XRControl[] {
                this.lblNhanTongCong, this.cellTongTienThang
            });

            this.Bands.AddRange(new DevExpress.XtraReports.UI.Band[] {
                this.TopMargin,
                this.ReportHeader,
                this.GroupHeaderThang,
                this.Detail,
                this.GroupFooterThang,
                this.BottomMargin
            });
            this.PageWidth = 850;
            this.PageHeight = 1100;
            this.Margins = new DevExpress.Drawing.DXMargins(50, 50, 40, 40);

            ((System.ComponentModel.ISupportInitialize)(this.tableHeaderCols)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.tableDetail)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this)).EndInit();
        }

        #endregion

        private DevExpress.XtraReports.UI.TopMarginBand TopMargin;
        private DevExpress.XtraReports.UI.BottomMarginBand BottomMargin;
        private DevExpress.XtraReports.UI.ReportHeaderBand ReportHeader;
        private DevExpress.XtraReports.UI.GroupHeaderBand GroupHeaderThang;
        private DevExpress.XtraReports.UI.DetailBand Detail;
        private DevExpress.XtraReports.UI.GroupFooterBand GroupFooterThang;

        public DevExpress.XtraReports.UI.XRLabel lblTieuDe;
        public DevExpress.XtraReports.UI.XRLabel lblThongTinNV;
        public DevExpress.XtraReports.UI.XRLabel lblThoiGian;
        public DevExpress.XtraReports.UI.XRLabel lblThangNamHeader;
        public DevExpress.XtraReports.UI.XRLabel lblNhanTongCong;
        public DevExpress.XtraReports.UI.XRLabel cellTongTienThang;

        private DevExpress.XtraReports.UI.XRTable tableHeaderCols;
        private DevExpress.XtraReports.UI.XRTableRow rowHeaderCols;
        public DevExpress.XtraReports.UI.XRTableCell hNgay;
        public DevExpress.XtraReports.UI.XRTableCell hSoPhieu;
        public DevExpress.XtraReports.UI.XRTableCell hLoaiPhieu;
        public DevExpress.XtraReports.UI.XRTableCell hDoiTac;
        public DevExpress.XtraReports.UI.XRTableCell hTenKho;
        public DevExpress.XtraReports.UI.XRTableCell hTenVT;
        public DevExpress.XtraReports.UI.XRTableCell hSoLuong;
        public DevExpress.XtraReports.UI.XRTableCell hDonGia;
        public DevExpress.XtraReports.UI.XRTableCell hTriGia;

        private DevExpress.XtraReports.UI.XRTable tableDetail;
        private DevExpress.XtraReports.UI.XRTableRow rowDetail;
        public DevExpress.XtraReports.UI.XRTableCell cellNgay;
        public DevExpress.XtraReports.UI.XRTableCell cellSoPhieu;
        public DevExpress.XtraReports.UI.XRTableCell cellLoaiPhieu;
        public DevExpress.XtraReports.UI.XRTableCell cellDoiTac;
        public DevExpress.XtraReports.UI.XRTableCell cellTenKho;
        public DevExpress.XtraReports.UI.XRTableCell cellTenVT;
        public DevExpress.XtraReports.UI.XRTableCell cellSoLuong;
        public DevExpress.XtraReports.UI.XRTableCell cellDonGia;
        public DevExpress.XtraReports.UI.XRTableCell cellTriGia;
    }
}