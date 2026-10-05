using DevExpress.XtraBars;
using DevExpress.XtraReports.UI;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace QLVT_DATHANG
{
    public partial class frmMain : DevExpress.XtraBars.Ribbon.RibbonForm
    {
        public frmMain()
        {
            InitializeComponent();
        }

        // Kiểm tra xem form con đã mở hay chưa để active, tránh mở trùng
        public Form CheckExists(Type ftype)
        {
            foreach (Form f in this.MdiChildren)
            {
                if (f.GetType() == ftype)
                    return f;
            }
            return null;
        }

        // Cập nhật thông tin phiên làm việc và phân quyền các nút chức năng sau khi Đăng nhập thành công
        public void HienThiMenu()
        {
            MANV.Caption = "MÃ NV: " + Program.mMaNV;
            HOTEN.Caption = "HỌ TÊN: " + Program.mHoten;
            NHOM.Caption = "NHÓM: " + Program.mGroup;

            btnDangNhap.Enabled = false;
            btnDangXuat.Enabled = true;

            // Hiển thị các trang chức năng
            ribbonPage2.Visible = true; // Tab Danh mục
            ribbonPage3.Visible = true; // Tab Nghiệp vụ
            ribbonPage1.Visible = true; // Tab Báo cáo luôn hiển thị

            // Phân quyền theo nhóm người dùng
            if (Program.mGroup == "USER")
            {
                btnTaoTaiKhoan.Enabled = false;

                // Vô hiệu hóa (làm xám) toàn bộ các nút báo cáo đối với User
                btnDSDDH.Enabled = false;
                btnHDNV.Enabled = false;
                btnTHNX.Enabled = false;
                btnNX.Enabled = false; // <-- THÊM DÒNG NÀY ĐỂ LÀM XÁM NÚT BẢNG KÊ LUÔN
            }
            else // CONGTY hoặc CHINHANH
            {
                btnTaoTaiKhoan.Enabled = true;

                // Mở sáng các nút báo cáo cho cấp quản lý
                btnDSDDH.Enabled = true;
                btnHDNV.Enabled = true;
                btnTHNX.Enabled = true;
                btnNX.Enabled = true; // Sáng lại cho cấp quản lý
            }
        }

        private void frmMain_Load(object sender, EventArgs e)
        {
            Program.formChinh = this;

            // Thiết lập trạng thái ban đầu khi chưa đăng nhập
            MANV.Caption = "MÃ NV: Chưa đăng nhập";
            HOTEN.Caption = "HỌ TÊN: ...";
            NHOM.Caption = "NHÓM: ...";

            btnDangNhap.Enabled = true;
            btnDangXuat.Enabled = false;
            btnTaoTaiKhoan.Enabled = false;

            // Ẩn tất cả các trang chức năng khi chưa đăng nhập
            ribbonPage2.Visible = false; // Tab Danh mục
            ribbonPage3.Visible = false; // Tab Nghiệp vụ
            ribbonPage1.Visible = false; // Tab Báo cáo

            // Gắn sự kiện click trực tiếp bằng code cho các nút báo cáo
            this.btnDSDDH.ItemClick += new DevExpress.XtraBars.ItemClickEventHandler(this.btnDSDDH_ItemClick);
            this.btnHDNV.ItemClick += new DevExpress.XtraBars.ItemClickEventHandler(this.btnHDNV_ItemClick);
            this.btnTHNX.ItemClick += new DevExpress.XtraBars.ItemClickEventHandler(this.btnTHNX_ItemClick);
        }

        // Bấm nút Đăng nhập trên Ribbon mới mở form đăng nhập
        private void btnDangNhap_ItemClick(object sender, ItemClickEventArgs e)
        {
            frmDangNhap loginForm = new frmDangNhap();
            loginForm.ShowDialog();
        }

        private void btnDangXuat_ItemClick(object sender, ItemClickEventArgs e)
        {
            DialogResult dr = MessageBox.Show("Bạn có chắc chắn muốn đăng xuất không?", "Xác nhận", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
            if (dr == DialogResult.Yes)
            {
                // Đóng tất cả các form con đang mở
                foreach (Form f in this.MdiChildren)
                {
                    f.Close();
                }

                if (Program.conn != null && Program.conn.State == ConnectionState.Open)
                    Program.conn.Close();

                // Đặt lại thông tin phiên làm việc
                Program.mMaNV = "";
                Program.mHoten = "";
                Program.mGroup = "";

                MANV.Caption = "MÃ NV: Chưa đăng nhập";
                HOTEN.Caption = "HỌ TÊN: ...";
                NHOM.Caption = "NHÓM: ...";

                btnDangNhap.Enabled = true;
                btnDangXuat.Enabled = false;
                btnTaoTaiKhoan.Enabled = false;

                // Ẩn lại các trang chức năng
                ribbonPage2.Visible = false;
                ribbonPage3.Visible = false;
                ribbonPage1.Visible = false;
            }
        }

        // Mở Form Tạo tài khoản đăng nhập (chỉ sáng với nhóm CONGTY và CHINHANH)
        private void btnTaoTaiKhoan_ItemClick(object sender, ItemClickEventArgs e)
        {
            Form frm = CheckExists(typeof(frmTaoLogin));
            if (frm != null)
            {
                frm.Activate();
            }
            else
            {
                frmTaoLogin f = new frmTaoLogin();
                f.ShowDialog();
            }
        }

        // --- NHÓM DANH MỤC ---

        private void btnNhanVien_ItemClick(object sender, ItemClickEventArgs e)
        {
            Form frm = CheckExists(typeof(frmNhanVien));
            if (frm != null)
            {
                frm.Activate();
            }
            else
            {
                frmNhanVien f = new frmNhanVien();
                f.MdiParent = this;
                f.Show();
            }
        }

        private void btnKho_ItemClick(object sender, ItemClickEventArgs e)
        {
            Form frm = CheckExists(typeof(frmKho));
            if (frm != null)
            {
                frm.Activate();
            }
            else
            {
                frmKho f = new frmKho();
                f.MdiParent = this;
                f.Show();
            }
        }

        private void btnVatTu_ItemClick(object sender, ItemClickEventArgs e)
        {
            MoFormVatTu();
        }

        private void btnVattu_ItemClick_1(object sender, ItemClickEventArgs e)
        {
            MoFormVatTu();
        }

        private void MoFormVatTu()
        {
            Form frm = CheckExists(typeof(frmVatTu));
            if (frm != null)
            {
                frm.Activate();
            }
            else
            {
                frmVatTu f = new frmVatTu();
                f.MdiParent = this;
                f.Show();
            }
        }

        // --- NHÓM NGHIỆP VỤ ---

        private void btnDonDatHang_ItemClick(object sender, ItemClickEventArgs e)
        {
            Form frm = CheckExists(typeof(frmDonDatHang));
            if (frm != null)
            {
                frm.Activate();
            }
            else
            {
                frmDonDatHang f = new frmDonDatHang();
                f.MdiParent = this;
                f.Show();
            }
        }

        private void btnPhieuNhap_ItemClick(object sender, ItemClickEventArgs e)
        {
            Form frm = CheckExists(typeof(frmPhieuNhap));
            if (frm != null)
            {
                frm.Activate();
            }
            else
            {
                frmPhieuNhap f = new frmPhieuNhap();
                f.MdiParent = this;
                f.Show();
            }
        }

        private void btnPhieuXuat_ItemClick(object sender, ItemClickEventArgs e)
        {
            Form frm = CheckExists(typeof(frmPhieuXuat));
            if (frm != null)
            {
                frm.Activate();
            }
            else
            {
                frmPhieuXuat f = new frmPhieuXuat();
                f.MdiParent = this;
                f.Show();
            }
        }

        private void ribbon_Click(object sender, EventArgs e)
        {
        }

        // --- NHÓM BÁO CÁO / THỐNG KÊ ---

        private void btnNX_ItemClick(object sender, ItemClickEventArgs e)
        {
            Form frm = CheckExists(typeof(frmBangKeNhapXuat));
            if (frm != null)
            {
                frm.Activate();
            }
            else
            {
                frmBangKeNhapXuat f = new frmBangKeNhapXuat();
                f.MdiParent = this;
                f.Show();
            }
        }

        // Mở Báo cáo Danh sách đơn đặt hàng chưa có phiếu nhập
        private void btnDSDDH_ItemClick(object sender, ItemClickEventArgs e)
        {
            string role = Program.mGroup;
            string strLenh = $"EXEC dbo.SP_IN_DS_DDH_CHUA_NHAP '{role}'";

            try
            {
                DataTable dt = Program.ExecSqlDataTable(strLenh);

                if (dt == null || dt.Rows.Count == 0)
                {
                    MessageBox.Show("Hiện tại không có đơn đặt hàng nào chưa lập phiếu nhập!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    return;
                }

                rptDSDDHChuaNhap report = new rptDSDDHChuaNhap(dt);
                ReportPrintTool printTool = new ReportPrintTool(report);
                printTool.ShowPreviewDialog();
            }
            catch (Exception ex)
            {
                MessageBox.Show("Lỗi khi mở báo cáo đơn đặt hàng: " + ex.Message, "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void btnHDNV_ItemClick(object sender, ItemClickEventArgs e)
        {
            Form frm = CheckExists(typeof(frmHoatDongNhanVien));
            if (frm != null)
            {
                frm.Activate();
            }
            else
            {
                frmHoatDongNhanVien f = new frmHoatDongNhanVien();
                f.MdiParent = this;
                f.Show();
            }
        }

        private void btnTHNX_ItemClick(object sender, ItemClickEventArgs e)
        {
            Form frm = CheckExists(typeof(frmTongHopNhapXuat));
            if (frm != null)
            {
                frm.Activate();
            }
            else
            {
                frmTongHopNhapXuat f = new frmTongHopNhapXuat();
                f.MdiParent = this;
                f.Show();
            }
        }
    }
}