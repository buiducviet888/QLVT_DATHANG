using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Windows.Forms;
using DevExpress.XtraReports.UI;

namespace QLVT_DATHANG
{
    public partial class frmNhanVien : DevExpress.XtraEditors.XtraForm
    {
        private int vitri = 0;
        private bool dangThemMoi = false;
        private string macn = "";

        public frmNhanVien()
        {
            InitializeComponent();
        }

        private void frmNhanVien_Load(object sender, EventArgs e)
        {
            Program.mlogin = Program.mloginDN;
            Program.password = Program.passwordDN;

            if (Program.bds_dspm != null && Program.bds_dspm.Count > Program.mChiNhanh)
            {
                Program.servername = ((DataRowView)Program.bds_dspm[Program.mChiNhanh])["TENSERVER"].ToString();
            }

            Program.KetNoi();

            this.qLVT_DATHANGDataSet.EnforceConstraints = false;

            this.nhanVienTableAdapter.Connection.ConnectionString = Program.connstr;
            this.nhanVienTableAdapter.Fill(this.qLVT_DATHANGDataSet.NhanVien);

            cmbChiNhanh.SelectedIndexChanged -= cmbChiNhanh_SelectedIndexChanged;

            if (Program.bds_dspm != null)
            {
                cmbChiNhanh.DataSource = Program.bds_dspm;
                cmbChiNhanh.DisplayMember = "TENCN";
                cmbChiNhanh.ValueMember = "TENSERVER";
                cmbChiNhanh.SelectedIndex = Program.mChiNhanh;
            }

            cmbChiNhanh.SelectedIndexChanged += cmbChiNhanh_SelectedIndexChanged;

            if (nhanVienBindingSource.Count > 0)
            {
                macn = ((DataRowView)nhanVienBindingSource[0])["MACN"].ToString().Trim();
            }
            else
            {
                macn = Program.mChiNhanh == 0 ? "CN1" : "CN2";
            }

            groupBox1.Enabled = false;

            if (Program.mGroup == "CONGTY")
            {
                cmbChiNhanh.Enabled = true;
                btnThem.Enabled = btnHieuChinh.Enabled = btnGhi.Enabled = btnXoa.Enabled = btnPhucHoi.Enabled = btnChuyenCN.Enabled = false;
                btnReload.Enabled = btnInDSNV.Enabled = btnThoat.Enabled = true;
            }
            else
            {
                cmbChiNhanh.Enabled = false;
                btnThem.Enabled = btnHieuChinh.Enabled = btnXoa.Enabled = btnReload.Enabled = btnChuyenCN.Enabled = btnInDSNV.Enabled = btnThoat.Enabled = true;
                btnGhi.Enabled = btnPhucHoi.Enabled = false;
            }
        }

        private void cmbChiNhanh_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (cmbChiNhanh.SelectedValue == null || cmbChiNhanh.SelectedValue.ToString() == "System.Data.DataRowView")
                return;

            string serverDuocChon = cmbChiNhanh.SelectedValue.ToString();
            string serverGoc = ((DataRowView)Program.bds_dspm[Program.mChiNhanh])["TENSERVER"].ToString();

            if (serverDuocChon.ToUpper() != serverGoc.ToUpper())
            {
                Program.mlogin = Program.remotelogin;
                Program.password = Program.remotepassword;
            }
            else
            {
                Program.mlogin = Program.mloginDN;
                Program.password = Program.passwordDN;
            }

            Program.servername = serverDuocChon;

            Program.connstr = "Data Source=" + Program.servername +
                              ";Initial Catalog=" + Program.database +
                              ";User ID=" + Program.mlogin +
                              ";Password=" + Program.password +
                              ";Encrypt=False;TrustServerCertificate=True;Connection Timeout=30";

            try
            {
                if (Program.conn.State == ConnectionState.Open) Program.conn.Close();
                Program.conn.ConnectionString = Program.connstr;
                Program.conn.Open();

                this.qLVT_DATHANGDataSet.EnforceConstraints = false;
                this.nhanVienTableAdapter.Connection.ConnectionString = Program.connstr;
                this.nhanVienTableAdapter.Fill(this.qLVT_DATHANGDataSet.NhanVien);
                this.nhanVienBindingSource.ResetBindings(false);

                if (nhanVienBindingSource.Count > 0)
                {
                    macn = ((DataRowView)nhanVienBindingSource[0])["MACN"].ToString().Trim();
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Lỗi nạp dữ liệu chi nhánh mới: " + ex.Message, "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        public void frmNhanVien_FormClosing(object sender, FormClosingEventArgs e)
        {
            Program.mlogin = Program.mloginDN;
            Program.password = Program.passwordDN;

            if (Program.bds_dspm != null && Program.bds_dspm.Count > Program.mChiNhanh)
            {
                Program.servername = ((DataRowView)Program.bds_dspm[Program.mChiNhanh])["TENSERVER"].ToString();
            }

            Program.KetNoi();
        }

        public void btnThem_Click(object sender, EventArgs e)
        {
            vitri = nhanVienBindingSource.Position;
            dangThemMoi = true;
            nhanVienBindingSource.AddNew();

            ((DataRowView)nhanVienBindingSource[nhanVienBindingSource.Position])["rowguid"] = Guid.NewGuid();

            mANVTextBox.Enabled = true;
            mACNTextBox.Text = macn;
            mACNTextBox.Enabled = false;
            trangThaiXoaTextBox.Text = "0";
            trangThaiXoaTextBox.Enabled = false;
            nGAYSINHDateTimePicker.Value = DateTime.Now.AddYears(-20);

            groupBox1.Enabled = true;
            nhanVienDataGridView.Enabled = false;

            btnThem.Enabled = btnHieuChinh.Enabled = btnXoa.Enabled = btnReload.Enabled = btnChuyenCN.Enabled = btnInDSNV.Enabled = btnThoat.Enabled = false;
            btnGhi.Enabled = btnPhucHoi.Enabled = true;

            mANVTextBox.Focus();
        }

        public void btnHieuChinh_Click(object sender, EventArgs e)
        {
            if (nhanVienBindingSource.Count == 0)
            {
                MessageBox.Show("Không có nhân viên nào để hiệu chỉnh!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
                return;
            }

            vitri = nhanVienBindingSource.Position;
            dangThemMoi = false;

            groupBox1.Enabled = true;
            mANVTextBox.Enabled = false;
            mACNTextBox.Enabled = false;
            trangThaiXoaTextBox.Enabled = false;

            nhanVienDataGridView.Enabled = false;
            btnThem.Enabled = btnHieuChinh.Enabled = btnXoa.Enabled = btnReload.Enabled = btnChuyenCN.Enabled = btnInDSNV.Enabled = btnThoat.Enabled = false;
            btnGhi.Enabled = btnPhucHoi.Enabled = true;

            hOTextBox.Focus();
        }

        public void btnGhi_Click(object sender, EventArgs e)
        {
            if (string.IsNullOrWhiteSpace(mANVTextBox.Text))
            {
                MessageBox.Show("Mã nhân viên không được để trống!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                mANVTextBox.Focus();
                return;
            }

            if (string.IsNullOrWhiteSpace(hOTextBox.Text))
            {
                MessageBox.Show("Họ nhân viên không được để trống!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                hOTextBox.Focus();
                return;
            }

            if (string.IsNullOrWhiteSpace(tENTextBox.Text))
            {
                MessageBox.Show("Tên nhân viên không được để trống!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                tENTextBox.Focus();
                return;
            }

            if (string.IsNullOrWhiteSpace(dIACHITextBox.Text))
            {
                MessageBox.Show("Địa chỉ không được để trống!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                dIACHITextBox.Focus();
                return;
            }

            double luong = 0;
            if (!double.TryParse(lUONGTextBox.Text.Trim(), out luong) || luong < 4000000)
            {
                MessageBox.Show("Lương phải là số và tối thiểu 4.000.000 VNĐ!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                lUONGTextBox.Focus();
                return;
            }

            if (DateTime.Now.Year - nGAYSINHDateTimePicker.Value.Year < 18)
            {
                MessageBox.Show("Nhân viên phải từ đủ 18 tuổi trở lên!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                nGAYSINHDateTimePicker.Focus();
                return;
            }

            // Kiểm tra trùng mã nhân viên phân tán sử dụng SP_KIEMTRAMA_NV qua LINK1
            if (dangThemMoi)
            {
                string queryCheck = "EXEC SP_KIEMTRAMA_NV " + mANVTextBox.Text.Trim();
                try
                {
                    Program.myReader = Program.ExecSqlDataReader(queryCheck);
                    if (Program.myReader != null)
                    {
                        if (Program.myReader.Read())
                        {
                            int tonTai = Convert.ToInt32(Program.myReader["KetQua"]);
                            if (tonTai == 1)
                            {
                                MessageBox.Show("Mã nhân viên đã tồn tại ở chi nhánh này hoặc chi nhánh khác!", "Cảnh báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                                mANVTextBox.Focus();
                                return;
                            }
                        }
                    }
                    else
                    {
                        MessageBox.Show("Không thể kết nối kiểm tra trùng mã phân tán!", "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
                        return;
                    }
                }
                catch (Exception ex)
                {
                    MessageBox.Show("Lỗi thực thi kiểm tra trùng: " + ex.Message, "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    return;
                }
                finally
                {
                    if (Program.myReader != null && !Program.myReader.IsClosed)
                    {
                        Program.myReader.Close();
                    }
                }
            }

            try
            {
                nhanVienBindingSource.EndEdit();
                nhanVienBindingSource.ResetCurrentItem();
                this.nhanVienTableAdapter.Connection.ConnectionString = Program.connstr;
                this.nhanVienTableAdapter.Update(this.qLVT_DATHANGDataSet.NhanVien);

                dangThemMoi = false;
                groupBox1.Enabled = false;
                nhanVienDataGridView.Enabled = true;

                btnThem.Enabled = btnHieuChinh.Enabled = btnXoa.Enabled = btnReload.Enabled = btnChuyenCN.Enabled = btnInDSNV.Enabled = btnThoat.Enabled = true;
                btnGhi.Enabled = btnPhucHoi.Enabled = false;

                MessageBox.Show("Ghi thông tin nhân viên thành công!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
            catch (Exception ex)
            {
                MessageBox.Show("Lỗi ghi nhân viên: " + ex.Message, "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        public void btnPhucHoi_Click(object sender, EventArgs e)
        {
            nhanVienBindingSource.CancelEdit();

            if (dangThemMoi)
            {
                nhanVienBindingSource.Position = vitri;
            }

            this.nhanVienTableAdapter.Connection.ConnectionString = Program.connstr;
            this.nhanVienTableAdapter.Fill(this.qLVT_DATHANGDataSet.NhanVien);

            dangThemMoi = false;
            groupBox1.Enabled = false;
            nhanVienDataGridView.Enabled = true;

            btnThem.Enabled = btnHieuChinh.Enabled = btnXoa.Enabled = btnReload.Enabled = btnChuyenCN.Enabled = btnInDSNV.Enabled = btnThoat.Enabled = true;
            btnGhi.Enabled = btnPhucHoi.Enabled = false;
        }

        public void btnXoa_Click(object sender, EventArgs e)
        {
            if (nhanVienBindingSource.Count == 0)
            {
                MessageBox.Show("Không có nhân viên nào để xóa!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
                return;
            }

            string maNV = ((DataRowView)nhanVienBindingSource[nhanVienBindingSource.Position])["MANV"].ToString().Trim();

            if (maNV == Program.mMaNV)
            {
                MessageBox.Show("Bạn không thể tự xóa chính tài khoản của mình đang đăng nhập!", "Cảnh báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            string queryCheckPN = "SELECT COUNT(*) FROM PhieuNhap WHERE MANV = " + maNV;
            string queryCheckPX = "SELECT COUNT(*) FROM PhieuXuat WHERE MANV = " + maNV;
            string queryCheckDH = "SELECT COUNT(*) FROM DatHang WHERE MANV = " + maNV;

            int countPN = 0, countPX = 0, countDH = 0;

            Program.myReader = Program.ExecSqlDataReader(queryCheckPN);
            if (Program.myReader != null)
            {
                try { if (Program.myReader.Read()) countPN = Program.myReader.GetInt32(0); }
                finally { Program.myReader.Close(); }
            }

            Program.myReader = Program.ExecSqlDataReader(queryCheckPX);
            if (Program.myReader != null)
            {
                try { if (Program.myReader.Read()) countPX = Program.myReader.GetInt32(0); }
                finally { Program.myReader.Close(); }
            }

            Program.myReader = Program.ExecSqlDataReader(queryCheckDH);
            if (Program.myReader != null)
            {
                try { if (Program.myReader.Read()) countDH = Program.myReader.GetInt32(0); }
                finally { Program.myReader.Close(); }
            }

            if (countPN > 0 || countPX > 0 || countDH > 0)
            {
                MessageBox.Show("Nhân viên này đã từng lập Phiếu Nhập, Phiếu Xuất hoặc Đơn Đặt Hàng. Chỉ được chuyển trạng thái xóa thay vì xóa hẳn!", "Cảnh báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            if (MessageBox.Show("Bạn có thực sự muốn xóa vĩnh viễn nhân viên này?", "Xác nhận", MessageBoxButtons.OKCancel, MessageBoxIcon.Question) == DialogResult.OK)
            {
                try
                {
                    nhanVienBindingSource.RemoveCurrent();
                    this.nhanVienTableAdapter.Connection.ConnectionString = Program.connstr;
                    this.nhanVienTableAdapter.Update(this.qLVT_DATHANGDataSet.NhanVien);
                    MessageBox.Show("Xóa nhân viên thành công!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
                catch (Exception ex)
                {
                    MessageBox.Show("Lỗi xóa nhân viên: " + ex.Message, "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    this.nhanVienTableAdapter.Fill(this.qLVT_DATHANGDataSet.NhanVien);
                }
            }
        }

        public void btnChuyenCN_Click(object sender, EventArgs e)
        {
            if (nhanVienBindingSource.Count == 0)
            {
                MessageBox.Show("Không có nhân viên nào để chuyển chi nhánh!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
                return;
            }

            string maNV = ((DataRowView)nhanVienBindingSource[nhanVienBindingSource.Position])["MANV"].ToString().Trim();

            if (maNV == Program.mMaNV)
            {
                MessageBox.Show("Bạn không thể tự chuyển chi nhánh cho chính tài khoản đang đăng nhập!", "Cảnh báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            string trangThaiXoa = ((DataRowView)nhanVienBindingSource[nhanVienBindingSource.Position])["TrangThaiXoa"].ToString().Trim();
            if (trangThaiXoa == "1")
            {
                MessageBox.Show("Nhân viên này đã nghỉ việc (Trạng thái xóa = 1), không thể chuyển chi nhánh!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            string chiNhanhChuyenDen = Program.mChiNhanh == 0 ? "CN2" : "CN1";

            if (MessageBox.Show(string.Format("Bạn có chắc chắn muốn chuyển nhân viên mã {0} sang {1}?", maNV, chiNhanhChuyenDen),
                                "Xác nhận chuyển chi nhánh", MessageBoxButtons.OKCancel, MessageBoxIcon.Question) == DialogResult.OK)
            {
                string sqlChuyen = string.Format("EXEC SP_CHUYENCHINHANH {0}, '{1}'", maNV, chiNhanhChuyenDen);
                int ret = Program.ExecSqlNonQuery(sqlChuyen);
                if (ret == 0)
                {
                    MessageBox.Show("Chuyển nhân viên sang chi nhánh mới thành công!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    this.nhanVienTableAdapter.Connection.ConnectionString = Program.connstr;
                    this.nhanVienTableAdapter.Fill(this.qLVT_DATHANGDataSet.NhanVien);
                }
            }
        }

        public void btnReload_Click(object sender, EventArgs e)
        {
            try
            {
                this.nhanVienTableAdapter.Connection.ConnectionString = Program.connstr;
                this.nhanVienTableAdapter.Fill(this.qLVT_DATHANGDataSet.NhanVien);
            }
            catch (Exception ex)
            {
                MessageBox.Show("Lỗi reload danh sách nhân viên: " + ex.Message, "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        public void btnThoat_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        public void btnInDSNV_Click(object sender, EventArgs e)
        {
            try
            {
                string strLenh = "EXEC dbo.SP_INDANHSACHNHANVIEN";
                DataTable dt = Program.ExecSqlDataTable(strLenh);

                if (dt == null || dt.Rows.Count == 0)
                {
                    MessageBox.Show("Chi nhánh này hiện chưa có nhân viên nào để in!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    return;
                }

                string tenChiNhanh = cmbChiNhanh.Text.Trim();

                rptDanhSachNhanVien report = new rptDanhSachNhanVien(dt, tenChiNhanh);
                ReportPrintTool printTool = new ReportPrintTool(report);
                printTool.ShowPreviewDialog();
            }
            catch (Exception ex)
            {
                MessageBox.Show("Lỗi khi mở báo cáo: " + ex.Message, "Thông báo lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void nhanVienBindingNavigatorSaveItem_Click(object sender, EventArgs e)
        {
            this.Validate();
            this.nhanVienBindingSource.EndEdit();
            this.tableAdapterManager.UpdateAll(this.qLVT_DATHANGDataSet);
        }

        private void label1_Click(object sender, EventArgs e)
        {
        }

        private void nhanVienDataGridView_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
        }
    }
}