using System;
using System.Data;
using System.Data.SqlClient;
using System.Windows.Forms;

namespace QLVT_DATHANG
{
    public partial class frmPhieuXuat : Form
    {
        private int vitri = 0;
        private bool dangThemMoi = false;

        public frmPhieuXuat()
        {
            InitializeComponent();
        }

        private void frmPhieuXuat_Load(object sender, EventArgs e)
        {
            Program.mlogin = Program.mloginDN;
            Program.password = Program.passwordDN;

            if (Program.bds_dspm != null && Program.bds_dspm.Count > Program.mChiNhanh)
            {
                Program.servername = ((DataRowView)Program.bds_dspm[Program.mChiNhanh])["TENSERVER"].ToString();
            }

            Program.KetNoi();

            this.qLVT_DATHANGDataSet.EnforceConstraints = false;

            this.phieuXuatTableAdapter.Connection.ConnectionString = Program.connstr;
            this.phieuXuatTableAdapter.Fill(this.qLVT_DATHANGDataSet.PhieuXuat);

            this.cTPXTableAdapter.Connection.ConnectionString = Program.connstr;
            this.cTPXTableAdapter.Fill(this.qLVT_DATHANGDataSet.CTPX);

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
                btnHieuChinh.Enabled = btnXoa.Enabled = (phieuXuatBindingSource.Count > 0);
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
                this.phieuXuatTableAdapter.Connection.ConnectionString = Program.connstr;
                this.phieuXuatTableAdapter.Fill(this.qLVT_DATHANGDataSet.PhieuXuat);

                this.cTPXTableAdapter.Connection.ConnectionString = Program.connstr;
                this.cTPXTableAdapter.Fill(this.qLVT_DATHANGDataSet.CTPX);
                this.phieuXuatBindingSource.ResetBindings(false);
            }
            catch (Exception ex)
            {
                MessageBox.Show("Lỗi nạp dữ liệu chi nhánh mới: " + ex.Message, "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void frmPhieuXuat_FormClosing(object sender, FormClosingEventArgs e)
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
            vitri = phieuXuatBindingSource.Position;
            dangThemMoi = true;
            phieuXuatBindingSource.AddNew();

            nGAYDateTimePicker.Value = DateTime.Now;
            mANVTextBox.Text = Program.mMaNV;

            // Xoá sạch các dòng chi tiết cũ trên lưới để đảm bảo lưới trống hoàn toàn khi tạo phiếu mới
            for (int i = this.qLVT_DATHANGDataSet.CTPX.Rows.Count - 1; i >= 0; i--)
            {
                DataRow dr = this.qLVT_DATHANGDataSet.CTPX.Rows[i];
                if (dr.RowState != DataRowState.Deleted)
                {
                    dr.Delete();
                }
            }

            groupBox1.Enabled = true;
            mAPXTextBox.Enabled = true;
            cTPXDataGridView.Enabled = true;
            cTPXDataGridView.AllowUserToAddRows = true;

            btnThem.Enabled = btnHieuChinh.Enabled = btnXoa.Enabled = btnReload.Enabled = false;
            btnGhi.Enabled = btnPhucHoi.Enabled = true;
            phieuXuatDataGridView.Enabled = false;
            mAPXTextBox.Focus();
        }

        private void btnHieuChinh_Click(object sender, EventArgs e)
        {
            if (phieuXuatBindingSource.Count == 0)
            {
                MessageBox.Show("Không có phiếu xuất nào để hiệu chỉnh!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            vitri = phieuXuatBindingSource.Position;
            dangThemMoi = false;

            groupBox1.Enabled = true;
            mAPXTextBox.Enabled = false;
            cTPXDataGridView.Enabled = true;
            cTPXDataGridView.AllowUserToAddRows = true;

            btnThem.Enabled = btnHieuChinh.Enabled = btnXoa.Enabled = btnReload.Enabled = false;
            btnGhi.Enabled = btnPhucHoi.Enabled = true;
            phieuXuatDataGridView.Enabled = false;
            hOTENKHTextBox.Focus();
        }

        private void btnGhi_Click(object sender, EventArgs e)
        {
            cTPXDataGridView.EndEdit();
            cTPXBindingSource.EndEdit();
            phieuXuatBindingSource.EndEdit();

            if (string.IsNullOrWhiteSpace(mAPXTextBox.Text))
            {
                MessageBox.Show("Mã phiếu xuất không được để trống!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                mAPXTextBox.Focus();
                return;
            }

            if (string.IsNullOrWhiteSpace(hOTENKHTextBox.Text))
            {
                MessageBox.Show("Họ tên khách hàng không được để trống!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                hOTENKHTextBox.Focus();
                return;
            }

            if (string.IsNullOrWhiteSpace(mAKHOTextBox.Text))
            {
                MessageBox.Show("Mã kho không được để trống!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                mAKHOTextBox.Focus();
                return;
            }

            string maPX = mAPXTextBox.Text.Trim();
            DateTime ngay = nGAYDateTimePicker.Value;
            string hotenKH = hOTENKHTextBox.Text.Trim();
            int maNV = Convert.ToInt32(mANVTextBox.Text.Trim());
            string maKho = mAKHOTextBox.Text.Trim();

            if (dangThemMoi)
            {
                string queryCheck = "EXEC SP_KIEMTRAPHIEUXUAT '" + maPX + "'";
                Program.myReader = Program.ExecSqlDataReader(queryCheck);
                if (Program.myReader == null) return;
                Program.myReader.Read();
                int tonTai = Convert.ToInt32(Program.myReader[0]);
                Program.myReader.Close();

                if (tonTai == 1)
                {
                    MessageBox.Show("Mã phiếu xuất đã tồn tại trong hệ thống!", "Cảnh báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    mAPXTextBox.Focus();
                    return;
                }
            }

            // Kiểm tra chi tiết và tồn kho (Chỉ lấy các dòng thực sự có MAVT được nhập)
            int soDongHopLe = 0;
            foreach (DataRow dr in this.qLVT_DATHANGDataSet.CTPX.Rows)
            {
                if (dr.RowState == DataRowState.Deleted) continue;
                if (dr["MAVT"] == DBNull.Value || string.IsNullOrWhiteSpace(dr["MAVT"].ToString())) continue;

                soDongHopLe++;
                string maVT = dr["MAVT"].ToString().Trim();
                int soLuongXuat = Convert.ToInt32(dr["SOLUONG"]);
                decimal donGia = Convert.ToDecimal(dr["DONGIA"]);

                if (soLuongXuat <= 0)
                {
                    MessageBox.Show("Số lượng xuất phải lớn hơn 0!", "Cảnh báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    return;
                }
                if (donGia <= 0)
                {
                    MessageBox.Show("Đơn giá phải lớn hơn 0!", "Cảnh báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    return;
                }

                // Kiểm tra tồn kho khi thêm mới
                if (dangThemMoi)
                {
                    string sqlCheckTon = string.Format("DECLARE @ret INT; EXEC @ret = SP_KIEMTRA_SOLUONG_TON '{0}', {1}; SELECT @ret;", maVT, soLuongXuat);
                    Program.myReader = Program.ExecSqlDataReader(sqlCheckTon);
                    if (Program.myReader != null)
                    {
                        try
                        {
                            if (Program.myReader.Read())
                            {
                                int duHang = Convert.ToInt32(Program.myReader[0]);
                                if (duHang == 0)
                                {
                                    MessageBox.Show(string.Format("Vật tư {0} không đủ số lượng tồn để xuất ({1})!", maVT, soLuongXuat),
                                                    "Cảnh báo thiếu hàng tồn", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                                    return;
                                }
                            }
                        }
                        finally
                        {
                            Program.myReader.Close();
                        }
                    }
                }
            }

            if (soDongHopLe == 0)
            {
                MessageBox.Show("Phiếu xuất phải có ít nhất một chi tiết vật tư!", "Cảnh báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            try
            {
                if (dangThemMoi)
                {
                    // 1. Insert Header PhieuXuat trực tiếp bằng SQL
                    string sqlInsertPX = string.Format(
                        "INSERT INTO PhieuXuat (MAPX, NGAY, HOTENKH, MANV, MAKHO, rowguid) VALUES ('{0}', '{1}', N'{2}', {3}, '{4}', NEWID())",
                        maPX, ngay.ToString("yyyy-MM-dd"), hotenKH, maNV, maKho);
                    Program.ExecSqlNonQuery(sqlInsertPX);

                    // 2. Insert Detail CTPX và gọi SP trừ tồn kho (Bỏ qua dòng rác chưa có MAVT)
                    foreach (DataRow dr in this.qLVT_DATHANGDataSet.CTPX.Rows)
                    {
                        if (dr.RowState == DataRowState.Deleted) continue;
                        if (dr["MAVT"] == DBNull.Value || string.IsNullOrWhiteSpace(dr["MAVT"].ToString())) continue;

                        string maVT = dr["MAVT"].ToString().Trim();
                        int soLuong = Convert.ToInt32(dr["SOLUONG"]);
                        decimal donGia = Convert.ToDecimal(dr["DONGIA"]);

                        string sqlInsertCTPX = string.Format(
                            "INSERT INTO CTPX (MAPX, MAVT, SOLUONG, DONGIA, rowguid) VALUES ('{0}', '{1}', {2}, {3}, NEWID())",
                            maPX, maVT, soLuong, donGia);
                        Program.ExecSqlNonQuery(sqlInsertCTPX);

                        // Trừ tồn kho
                        string sqlTruKho = string.Format("EXEC SP_CAPNHAT_SOLUONG_TON '{0}', {1}, 'XUAT'", maVT, soLuong);
                        Program.ExecSqlNonQuery(sqlTruKho);
                    }
                }
                else
                {
                    // Cập nhật Header
                    string sqlUpdatePX = string.Format(
                        "UPDATE PhieuXuat SET HOTENKH = N'{0}', MAKHO = '{1}' WHERE MAPX = '{2}'",
                        hotenKH, maKho, maPX);
                    Program.ExecSqlNonQuery(sqlUpdatePX);

                    // Cập nhật Detail (Xóa cũ thêm mới)
                    foreach (DataRow dr in this.qLVT_DATHANGDataSet.CTPX.Rows)
                    {
                        if (dr.RowState == DataRowState.Deleted) continue;
                        if (dr["MAVT"] == DBNull.Value || string.IsNullOrWhiteSpace(dr["MAVT"].ToString())) continue;

                        string maVT = dr["MAVT"].ToString().Trim();
                        int soLuongMoi = Convert.ToInt32(dr["SOLUONG"]);
                        decimal donGiaMoi = Convert.ToDecimal(dr["DONGIA"]);

                        string sqlXoaThemCT = string.Format(
                            "DELETE FROM CTPX WHERE MAPX = '{0}' AND MAVT = '{1}';" +
                            "INSERT INTO CTPX (MAPX, MAVT, SOLUONG, DONGIA, rowguid) VALUES ('{0}', '{1}', {2}, {3}, NEWID());",
                            maPX, maVT, soLuongMoi, donGiaMoi);
                        Program.ExecSqlNonQuery(sqlXoaThemCT);
                    }
                }

                // Nạp lại dữ liệu cho bảng
                this.phieuXuatTableAdapter.Fill(this.qLVT_DATHANGDataSet.PhieuXuat);
                this.cTPXTableAdapter.Fill(this.qLVT_DATHANGDataSet.CTPX);

                dangThemMoi = false;
                groupBox1.Enabled = false;
                cTPXDataGridView.AllowUserToAddRows = false;

                btnThem.Enabled = btnHieuChinh.Enabled = btnXoa.Enabled = btnReload.Enabled = true;
                btnGhi.Enabled = btnPhucHoi.Enabled = false;
                phieuXuatDataGridView.Enabled = true;
                cTPXDataGridView.Enabled = true;

                MessageBox.Show("Ghi phiếu xuất và cập nhật tồn kho thành công!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
            catch (Exception ex)
            {
                MessageBox.Show("Lỗi ghi phiếu xuất: " + ex.Message, "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void btnPhucHoi_Click(object sender, EventArgs e)
        {
            phieuXuatBindingSource.CancelEdit();
            cTPXBindingSource.CancelEdit();

            if (dangThemMoi) phieuXuatBindingSource.Position = vitri;

            this.phieuXuatTableAdapter.Fill(this.qLVT_DATHANGDataSet.PhieuXuat);
            this.cTPXTableAdapter.Fill(this.qLVT_DATHANGDataSet.CTPX);

            dangThemMoi = false;
            groupBox1.Enabled = false;
            cTPXDataGridView.AllowUserToAddRows = false;

            btnThem.Enabled = btnHieuChinh.Enabled = btnXoa.Enabled = btnReload.Enabled = true;
            btnGhi.Enabled = btnPhucHoi.Enabled = false;
            phieuXuatDataGridView.Enabled = true;
            cTPXDataGridView.Enabled = true;
        }

        private void btnXoa_Click(object sender, EventArgs e)
        {
            if (cTPXBindingSource.Count > 0)
            {
                MessageBox.Show("Không thể xóa phiếu xuất đã có chi tiết xuất!", "Cảnh báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            if (MessageBox.Show("Bạn có thực sự muốn xóa phiếu xuất này?", "Xác nhận", MessageBoxButtons.OKCancel, MessageBoxIcon.Question) == DialogResult.OK)
            {
                try
                {
                    string maPX = mAPXTextBox.Text.Trim();
                    string sqlXoa = "DELETE FROM PhieuXuat WHERE MAPX = '" + maPX + "'";
                    Program.ExecSqlNonQuery(sqlXoa);

                    this.phieuXuatTableAdapter.Fill(this.qLVT_DATHANGDataSet.PhieuXuat);
                    MessageBox.Show("Xóa phiếu xuất thành công!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
                catch (Exception ex)
                {
                    MessageBox.Show("Lỗi xóa phiếu xuất: " + ex.Message, "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    this.phieuXuatTableAdapter.Fill(this.qLVT_DATHANGDataSet.PhieuXuat);
                }
            }
        }

        private void btnReload_Click(object sender, EventArgs e)
        {
            try
            {
                this.phieuXuatTableAdapter.Fill(this.qLVT_DATHANGDataSet.PhieuXuat);
                this.cTPXTableAdapter.Fill(this.qLVT_DATHANGDataSet.CTPX);
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

        private void menuThemCT_Click(object sender, EventArgs e)
        {
            if (phieuXuatBindingSource.Count == 0)
            {
                MessageBox.Show("Vui lòng chọn phiếu xuất trước khi thêm chi tiết!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            try
            {
                cTPXBindingSource.AddNew();
                DataRowView drv = (DataRowView)phieuXuatBindingSource.Current;
                string maPX = drv["MAPX"].ToString().Trim();

                DataRowView currentCT = (DataRowView)cTPXBindingSource.Current;
                currentCT["MAPX"] = maPX;
                currentCT["SOLUONG"] = 1;
                currentCT["DONGIA"] = 0;

                btnGhi.Enabled = true;
                btnPhucHoi.Enabled = true;
            }
            catch (Exception ex)
            {
                MessageBox.Show("Lỗi thêm chi tiết phiếu xuất: " + ex.Message, "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void menuHieuChinhCT_Click(object sender, EventArgs e)
        {
            if (cTPXBindingSource.Count == 0)
            {
                MessageBox.Show("Không có dòng chi tiết nào để hiệu chỉnh!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            cTPXBindingSource.EndEdit();
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
            if (cTPXBindingSource.Count == 0)
            {
                MessageBox.Show("Không có dòng chi tiết nào để xóa!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            if (MessageBox.Show("Bạn có thực sự muốn xóa dòng chi tiết này?", "Xác nhận", MessageBoxButtons.OKCancel, MessageBoxIcon.Question) == DialogResult.OK)
            {
                try
                {
                    cTPXBindingSource.RemoveCurrent();
                    this.cTPXTableAdapter.Update(this.qLVT_DATHANGDataSet.CTPX);
                    MessageBox.Show("Xóa chi tiết thành công!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
                catch (Exception ex)
                {
                    MessageBox.Show("Lỗi xóa chi tiết: " + ex.Message, "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    this.cTPXTableAdapter.Fill(this.qLVT_DATHANGDataSet.CTPX);
                }
            }
        }
    }
}