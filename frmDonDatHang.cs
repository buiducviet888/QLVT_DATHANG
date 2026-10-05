using System;
using System.Data;
using System.Data.SqlClient;
using System.Windows.Forms;

namespace QLVT_DATHANG
{
    public partial class frmDonDatHang : Form
    {
        private int vitri = 0;
        private bool dangThemMoi = false;

        public frmDonDatHang()
        {
            InitializeComponent();
        }

        private void frmDonDatHang_Load(object sender, EventArgs e)
        {
            Program.mlogin = Program.mloginDN;
            Program.password = Program.passwordDN;

            if (Program.bds_dspm != null && Program.bds_dspm.Count > Program.mChiNhanh)
            {
                Program.servername = ((DataRowView)Program.bds_dspm[Program.mChiNhanh])["TENSERVER"].ToString();
            }

            Program.KetNoi();

            this.qLVT_DATHANGDataSet.EnforceConstraints = false;

            this.datHangTableAdapter.Connection.ConnectionString = Program.connstr;
            this.datHangTableAdapter.Fill(this.qLVT_DATHANGDataSet.DatHang);

            this.cTDDHTableAdapter.Connection.ConnectionString = Program.connstr;
            this.cTDDHTableAdapter.Fill(this.qLVT_DATHANGDataSet.CTDDH);

            cmbChiNhanh.SelectedIndexChanged -= cmbChiNhanh_SelectedIndexChanged;

            if (Program.bds_dspm != null)
            {
                cmbChiNhanh.DataSource = Program.bds_dspm;
                cmbChiNhanh.DisplayMember = "TENCN";
                cmbChiNhanh.ValueMember = "TENSERVER";
                cmbChiNhanh.SelectedIndex = Program.mChiNhanh;
            }

            cmbChiNhanh.SelectedIndexChanged += cmbChiNhanh_SelectedIndexChanged;

            groupBox1.Enabled = false;
            mANVTextBox.ReadOnly = true;

            // Phân quyền theo nhóm đăng nhập
            if (Program.mGroup == "CONGTY")
            {
                cmbChiNhanh.Enabled = true;
                btnThem.Enabled = btnHieuChinh.Enabled = btnGhi.Enabled = btnXoa.Enabled = btnPhucHoi.Enabled = false;
                btnReload.Enabled = btnThoat.Enabled = true;
            }
            else
            {
                cmbChiNhanh.Enabled = false;
                btnThem.Enabled = btnReload.Enabled = btnThoat.Enabled = true;
                btnGhi.Enabled = btnPhucHoi.Enabled = false;
                btnHieuChinh.Enabled = btnXoa.Enabled = (datHangBindingSource.Count > 0);
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
                this.datHangTableAdapter.Connection.ConnectionString = Program.connstr;
                this.datHangTableAdapter.Fill(this.qLVT_DATHANGDataSet.DatHang);

                this.cTDDHTableAdapter.Connection.ConnectionString = Program.connstr;
                this.cTDDHTableAdapter.Fill(this.qLVT_DATHANGDataSet.CTDDH);
                this.datHangBindingSource.ResetBindings(false);
            }
            catch (Exception ex)
            {
                MessageBox.Show("Lỗi nạp dữ liệu chi nhánh mới: " + ex.Message, "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void frmDonDatHang_FormClosing(object sender, FormClosingEventArgs e)
        {
            Program.mlogin = Program.mloginDN;
            Program.password = Program.passwordDN;

            if (Program.bds_dspm != null && Program.bds_dspm.Count > Program.mChiNhanh)
            {
                Program.servername = ((DataRowView)Program.bds_dspm[Program.mChiNhanh])["TENSERVER"].ToString();
            }

            Program.KetNoi();
        }

        private void btnThem_Click(object sender, EventArgs e)
        {
            vitri = datHangBindingSource.Position;
            dangThemMoi = true;
            datHangBindingSource.AddNew();

            // Tự động gán rowguid cho dòng mới thêm ở Master
            ((DataRowView)datHangBindingSource[datHangBindingSource.Position])["rowguid"] = Guid.NewGuid();

            nGAYDateTimePicker.Value = DateTime.Now;
            mANVTextBox.Text = Program.mMaNV;

            // Mở khóa khung nhập liệu và lưới chi tiết
            groupBox1.Enabled = true;
            masoDDHTextBox.Enabled = true;
            cTDDHDataGridView.Enabled = true;

            // Khóa các nút không cần thiết, BẬT SÁNG NÚT GHI VÀ PHỤC HỒI
            btnThem.Enabled = btnHieuChinh.Enabled = btnXoa.Enabled = btnReload.Enabled = false;
            btnGhi.Enabled = btnPhucHoi.Enabled = true;

            datHangDataGridView.Enabled = false;
            masoDDHTextBox.Focus();
        }

        private void btnHieuChinh_Click(object sender, EventArgs e)
        {
            if (datHangBindingSource.Count == 0)
            {
                MessageBox.Show("Không có đơn đặt hàng nào để hiệu chỉnh!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            string maSoDDH = masoDDHTextBox.Text.Trim();

            string queryCheckPN = "SELECT COUNT(*) FROM PhieuNhap WHERE MasoDDH = '" + maSoDDH + "'";
            Program.myReader = Program.ExecSqlDataReader(queryCheckPN);
            if (Program.myReader != null)
            {
                try
                {
                    if (Program.myReader.Read())
                    {
                        int daCoPN = Program.myReader.GetInt32(0);
                        if (daCoPN > 0)
                        {
                            MessageBox.Show("Đơn đặt hàng này đã được lập Phiếu Nhập, không thể chỉnh sửa!", "Cảnh báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                            return;
                        }
                    }
                }
                finally
                {
                    Program.myReader.Close();
                }
            }

            vitri = datHangBindingSource.Position;
            dangThemMoi = false;

            // Mở khóa khung nhập liệu và lưới chi tiết
            groupBox1.Enabled = true;
            masoDDHTextBox.Enabled = false;
            cTDDHDataGridView.Enabled = true;

            // Khóa các nút khác, BẬT SÁNG NÚT GHI VÀ PHỤC HỒI
            btnThem.Enabled = btnHieuChinh.Enabled = btnXoa.Enabled = btnReload.Enabled = false;
            btnGhi.Enabled = btnPhucHoi.Enabled = true;

            datHangDataGridView.Enabled = false;
            nhaCCTextBox.Focus();
        }

        private void btnGhi_Click(object sender, EventArgs e)
        {
            // === Ép buộc kết thúc chỉnh sửa để cập nhật dữ liệu từ các ô đang gõ dở vào BindingSource ===
            cTDDHBindingSource.EndEdit();
            cTDDHDataGridView.EndEdit();
            datHangBindingSource.EndEdit();

            if (string.IsNullOrWhiteSpace(masoDDHTextBox.Text))
            {
                MessageBox.Show("Mã số đơn đặt hàng không được để trống!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                masoDDHTextBox.Focus();
                return;
            }

            if (string.IsNullOrWhiteSpace(nhaCCTextBox.Text))
            {
                MessageBox.Show("Nhà cung cấp không được để trống!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                nhaCCTextBox.Focus();
                return;
            }

            if (string.IsNullOrWhiteSpace(mAKHOTextBox.Text))
            {
                MessageBox.Show("Mã kho không được để trống!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                mAKHOTextBox.Focus();
                return;
            }

            if (dangThemMoi)
            {
                string queryCheck = "EXEC SP_KIEMTRADATHANG '" + masoDDHTextBox.Text.Trim() + "'";
                Program.myReader = Program.ExecSqlDataReader(queryCheck);
                if (Program.myReader == null) return;
                Program.myReader.Read();
                int tonTai = Convert.ToInt32(Program.myReader[0]);
                Program.myReader.Close();

                if (tonTai == 1)
                {
                    MessageBox.Show("Mã số đơn đặt hàng đã tồn tại trên hệ thống!", "Cảnh báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    masoDDHTextBox.Focus();
                    return;
                }
            }

            // Kiểm tra ràng buộc dữ liệu chi tiết không được để trống MAVT, SOLUONG, DONGIA
            foreach (DataRow dr in this.qLVT_DATHANGDataSet.CTDDH.Rows)
            {
                if (dr.RowState == DataRowState.Deleted) continue;

                if (dr["MAVT"] == DBNull.Value || string.IsNullOrWhiteSpace(dr["MAVT"].ToString()))
                {
                    MessageBox.Show("Mã vật tư chi tiết không được để trống!", "Cảnh báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    return;
                }
                if (dr["SOLUONG"] == DBNull.Value || Convert.ToInt32(dr["SOLUONG"]) <= 0)
                {
                    MessageBox.Show("Số lượng chi tiết phải lớn hơn 0!", "Cảnh báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    return;
                }
                if (dr["DONGIA"] == DBNull.Value || Convert.ToDouble(dr["DONGIA"]) <= 0)
                {
                    MessageBox.Show("Đơn giá chi tiết phải lớn hơn 0!", "Cảnh báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    return;
                }
            }

            try
            {
                datHangBindingSource.ResetCurrentItem();

                // Tự động gán rowguid cho các dòng mới ở bảng DatHang nếu bị thiếu
                foreach (DataRow dr in this.qLVT_DATHANGDataSet.DatHang.Rows)
                {
                    if (dr.RowState == DataRowState.Added && (dr["rowguid"] == DBNull.Value || dr["rowguid"] == null))
                    {
                        dr["rowguid"] = Guid.NewGuid();
                    }
                }

                // Tự động gán rowguid cho các dòng mới ở bảng CTDDH nếu bị thiếu
                foreach (DataRow dr in this.qLVT_DATHANGDataSet.CTDDH.Rows)
                {
                    if (dr.RowState == DataRowState.Added && (dr["rowguid"] == DBNull.Value || dr["rowguid"] == null))
                    {
                        dr["rowguid"] = Guid.NewGuid();
                    }
                }

                this.datHangTableAdapter.Connection.ConnectionString = Program.connstr;
                this.datHangTableAdapter.Update(this.qLVT_DATHANGDataSet.DatHang);

                this.cTDDHTableAdapter.Connection.ConnectionString = Program.connstr;
                this.cTDDHTableAdapter.Update(this.qLVT_DATHANGDataSet.CTDDH);

                dangThemMoi = false;
                groupBox1.Enabled = false;

                // Trả lại trạng thái các nút sau khi lưu thành công
                btnThem.Enabled = btnHieuChinh.Enabled = btnXoa.Enabled = btnReload.Enabled = true;
                btnGhi.Enabled = btnPhucHoi.Enabled = false;
                datHangDataGridView.Enabled = true;
                cTDDHDataGridView.Enabled = true;

                MessageBox.Show("Ghi đơn đặt hàng thành công!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
            catch (Exception ex)
            {
                MessageBox.Show("Lỗi ghi đơn đặt hàng: " + ex.Message, "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void btnPhucHoi_Click(object sender, EventArgs e)
        {
            datHangBindingSource.CancelEdit();
            cTDDHBindingSource.CancelEdit();

            if (dangThemMoi) datHangBindingSource.Position = vitri;

            this.cTDDHTableAdapter.Connection.ConnectionString = Program.connstr;
            this.cTDDHTableAdapter.Fill(this.qLVT_DATHANGDataSet.CTDDH);

            dangThemMoi = false;
            groupBox1.Enabled = false;

            btnThem.Enabled = btnHieuChinh.Enabled = btnXoa.Enabled = btnReload.Enabled = true;
            btnGhi.Enabled = btnPhucHoi.Enabled = false;
            datHangDataGridView.Enabled = true;
            cTDDHDataGridView.Enabled = true;
        }

        private void btnXoa_Click(object sender, EventArgs e)
        {
            if (datHangBindingSource.Count == 0)
            {
                MessageBox.Show("Không có đơn đặt hàng nào để xóa!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            string maSoDDH = masoDDHTextBox.Text.Trim();

            if (cTDDHBindingSource.Count > 0)
            {
                MessageBox.Show("Không thể xóa đơn đặt hàng này vì đã có chi tiết mặt hàng!", "Cảnh báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            string queryCheckPN = "SELECT COUNT(*) FROM PhieuNhap WHERE MasoDDH = '" + maSoDDH + "'";
            Program.myReader = Program.ExecSqlDataReader(queryCheckPN);
            if (Program.myReader != null)
            {
                try
                {
                    if (Program.myReader.Read())
                    {
                        int daCoPN = Program.myReader.GetInt32(0);
                        if (daCoPN > 0)
                        {
                            MessageBox.Show("Không thể xóa đơn đặt hàng này vì đã được lập phiếu nhập!", "Cảnh báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                            return;
                        }
                    }
                }
                finally
                {
                    Program.myReader.Close();
                }
            }

            if (MessageBox.Show("Bạn có thực sự muốn xóa đơn đặt hàng này?", "Xác nhận", MessageBoxButtons.OKCancel, MessageBoxIcon.Question) == DialogResult.OK)
            {
                try
                {
                    datHangBindingSource.RemoveCurrent();
                    this.datHangTableAdapter.Connection.ConnectionString = Program.connstr;
                    this.datHangTableAdapter.Update(this.qLVT_DATHANGDataSet.DatHang);
                    MessageBox.Show("Xóa đơn đặt hàng thành công!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
                catch (Exception ex)
                {
                    MessageBox.Show("Lỗi xóa đơn đặt hàng: " + ex.Message, "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    this.datHangTableAdapter.Fill(this.qLVT_DATHANGDataSet.DatHang);
                }
            }
        }

        private void btnReload_Click(object sender, EventArgs e)
        {
            try
            {
                this.datHangTableAdapter.Connection.ConnectionString = Program.connstr;
                this.datHangTableAdapter.Fill(this.qLVT_DATHANGDataSet.DatHang);

                this.cTDDHTableAdapter.Connection.ConnectionString = Program.connstr;
                this.cTDDHTableAdapter.Fill(this.qLVT_DATHANGDataSet.CTDDH);
            }
            catch (Exception ex)
            {
                MessageBox.Show("Lỗi reload: " + ex.Message, "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void btnThoat_Click(object sender, EventArgs e)
        {
            Close();
        }

        // ==========================================================
        // CÁC HÀM XỬ LÝ CHO MENU CHUỘT PHẢI (SHORTCUT MENU) Ở CHI TIẾT
        // ==========================================================

        private void menuThemCT_Click(object sender, EventArgs e)
        {
            if (datHangBindingSource.Count == 0)
            {
                MessageBox.Show("Vui lòng chọn đơn đặt hàng trước khi thêm chi tiết!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            try
            {
                cTDDHBindingSource.AddNew();
                DataRowView drv = (DataRowView)datHangBindingSource.Current;
                string maSoDDH = drv["MasoDDH"].ToString().Trim();

                // Gán sẵn các giá trị mặc định để tránh lỗi NULL bắt buộc của SQL Server
                DataRowView currentCT = (DataRowView)cTDDHBindingSource.Current;
                currentCT["MasoDDH"] = maSoDDH;
                currentCT["rowguid"] = Guid.NewGuid();
                currentCT["SOLUONG"] = 1; // Mặc định số lượng là 1
                currentCT["DONGIA"] = 0;  // Mặc định đơn giá là 0

                btnGhi.Enabled = true;
                btnPhucHoi.Enabled = true;
            }
            catch (Exception ex)
            {
                MessageBox.Show("Lỗi thêm chi tiết: " + ex.Message, "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void menuHieuChinhCT_Click(object sender, EventArgs e)
        {
            if (cTDDHBindingSource.Count == 0)
            {
                MessageBox.Show("Không có dòng chi tiết nào để hiệu chỉnh!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            cTDDHBindingSource.EndEdit();
            btnGhi.Enabled = true;
            btnPhucHoi.Enabled = true;
            MessageBox.Show("Bạn có thể chỉnh sửa trực tiếp trên lưới, sau đó bấm Ghi để lưu.", "Hướng dẫn", MessageBoxButtons.OK, MessageBoxIcon.Information);
        }

        private void menuGhiCT_Click(object sender, EventArgs e)
        {
            btnGhi_Click(sender, e);
        }

        private void menuXoaCT_Click(object sender, EventArgs e)
        {
            if (cTDDHBindingSource.Count == 0)
            {
                MessageBox.Show("Không có dòng chi tiết nào để xóa!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            if (MessageBox.Show("Bạn có thực sự muốn xóa dòng chi tiết này?", "Xác nhận", MessageBoxButtons.OKCancel, MessageBoxIcon.Question) == DialogResult.OK)
            {
                try
                {
                    cTDDHBindingSource.RemoveCurrent();
                    this.cTDDHTableAdapter.Connection.ConnectionString = Program.connstr;
                    this.cTDDHTableAdapter.Update(this.qLVT_DATHANGDataSet.CTDDH);
                    MessageBox.Show("Xóa chi tiết thành công!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
                catch (Exception ex)
                {
                    MessageBox.Show("Lỗi xóa chi tiết: " + ex.Message, "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    this.cTDDHTableAdapter.Fill(this.qLVT_DATHANGDataSet.CTDDH);
                }
            }
        }
    }
}