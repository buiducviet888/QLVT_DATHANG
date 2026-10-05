using System;
using System.Data;
using System.Data.SqlClient;
using System.Windows.Forms;

namespace QLVT_DATHANG
{
    public partial class frmPhieuNhap : Form
    {
        private int vitri = 0;
        private bool dangThemMoi = false;

        public frmPhieuNhap()
        {
            InitializeComponent();
        }

        private void frmPhieuNhap_Load(object sender, EventArgs e)
        {
            Program.mlogin = Program.mloginDN;
            Program.password = Program.passwordDN;

            if (Program.bds_dspm != null && Program.bds_dspm.Count > Program.mChiNhanh)
            {
                Program.servername = ((DataRowView)Program.bds_dspm[Program.mChiNhanh])["TENSERVER"].ToString();
            }

            Program.KetNoi();

            this.qLVT_DATHANGDataSet.EnforceConstraints = false;

            this.phieuNhapTableAdapter.Connection.ConnectionString = Program.connstr;
            this.phieuNhapTableAdapter.Fill(this.qLVT_DATHANGDataSet.PhieuNhap);

            this.cTPNTableAdapter.Connection.ConnectionString = Program.connstr;
            this.cTPNTableAdapter.Fill(this.qLVT_DATHANGDataSet.CTPN);

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
            masoDDHTextBox.ReadOnly = true;
            mAKHOTextBox.ReadOnly = true;
            mANVTextBox.ReadOnly = true;

            cTPNDataGridView.AllowUserToAddRows = false;

            if (Program.mGroup == "CONGTY")
            {
                cmbChiNhanh.Enabled = true;
                btnThem.Enabled = btnHieuChinh.Enabled = btnGhi.Enabled = btnXoa.Enabled = btnPhucHoi.Enabled = false;
                btnReload.Enabled = btnThoat.Enabled = true;
            }
            else
            {
                cmbChiNhanh.Enabled = false;
                btnThem.Enabled = btnHieuChinh.Enabled = btnXoa.Enabled = btnReload.Enabled = btnThoat.Enabled = true;
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
                this.phieuNhapTableAdapter.Connection.ConnectionString = Program.connstr;
                this.phieuNhapTableAdapter.Fill(this.qLVT_DATHANGDataSet.PhieuNhap);

                this.cTPNTableAdapter.Connection.ConnectionString = Program.connstr;
                this.cTPNTableAdapter.Fill(this.qLVT_DATHANGDataSet.CTPN);
                this.phieuNhapBindingSource.ResetBindings(false);
            }
            catch (Exception ex)
            {
                MessageBox.Show("Lỗi nạp dữ liệu chi nhánh mới: " + ex.Message, "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void frmPhieuNhap_FormClosing(object sender, FormClosingEventArgs e)
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
            vitri = phieuNhapBindingSource.Position;
            dangThemMoi = true;
            phieuNhapBindingSource.AddNew();

            nGAYDateTimePicker.Value = DateTime.Now;
            mANVTextBox.Text = Program.mMaNV;

            try
            {
                DataRowView currentPN = (DataRowView)phieuNhapBindingSource.Current;
                currentPN["rowguid"] = Guid.NewGuid();
            }
            catch { }

            groupBox1.Enabled = true;
            mAPNTextBox.Enabled = true;
            btnChonDDH.Enabled = true;
            cTPNDataGridView.Enabled = true;

            cTPNDataGridView.AllowUserToAddRows = true;

            btnThem.Enabled = btnHieuChinh.Enabled = btnXoa.Enabled = btnReload.Enabled = false;
            btnGhi.Enabled = btnPhucHoi.Enabled = true;
            phieuNhapDataGridView.Enabled = false;
            mAPNTextBox.Focus();
        }

        private void btnHieuChinh_Click(object sender, EventArgs e)
        {
            if (phieuNhapBindingSource.Count == 0)
            {
                MessageBox.Show("Không có phiếu nhập nào để hiệu chỉnh!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            vitri = phieuNhapBindingSource.Position;
            dangThemMoi = false;

            groupBox1.Enabled = true;
            mAPNTextBox.Enabled = false;
            btnChonDDH.Enabled = false;
            cTPNDataGridView.Enabled = true;

            cTPNDataGridView.AllowUserToAddRows = true;

            btnThem.Enabled = btnHieuChinh.Enabled = btnXoa.Enabled = btnReload.Enabled = false;
            btnGhi.Enabled = btnPhucHoi.Enabled = true;
            phieuNhapDataGridView.Enabled = false;
        }

        private void btnGhi_Click(object sender, EventArgs e)
        {
            cTPNBindingSource.EndEdit();
            cTPNDataGridView.EndEdit();
            phieuNhapBindingSource.EndEdit();

            if (mAPNTextBox.Text.Trim() == "")
            {
                MessageBox.Show("Mã phiếu nhập không được để trống!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                mAPNTextBox.Focus();
                return;
            }

            DataRowView pnRow = (DataRowView)phieuNhapBindingSource.Current;
            string maDDH = pnRow["MasoDDH"]?.ToString().Trim();

            if (string.IsNullOrEmpty(maDDH))
            {
                MessageBox.Show("Phiếu nhập phải gắn với một Đơn đặt hàng! Hãy bấm nút Chọn ĐĐH.", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                btnChonDDH.Focus();
                return;
            }

            if (dangThemMoi)
            {
                string queryCheck = "EXEC SP_KIEMTRAPHIEUNHAP '" + mAPNTextBox.Text.Trim() + "'";
                Program.myReader = Program.ExecSqlDataReader(queryCheck);
                if (Program.myReader == null) return;
                Program.myReader.Read();
                int tonTai = Convert.ToInt32(Program.myReader[0]);
                Program.myReader.Close();

                if (tonTai == 1)
                {
                    MessageBox.Show("Mã phiếu nhập đã tồn tại trong hệ thống!", "Cảnh báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    mAPNTextBox.Focus();
                    return;
                }
            }

            foreach (DataRow dr in this.qLVT_DATHANGDataSet.CTPN.Rows)
            {
                if (dr.RowState == DataRowState.Deleted) continue;

                if (dr.RowState == DataRowState.Added && (dr["MAVT"] == DBNull.Value || string.IsNullOrWhiteSpace(dr["MAVT"].ToString())))
                {
                    continue;
                }

                if (dr["MAVT"] == DBNull.Value || string.IsNullOrWhiteSpace(dr["MAVT"].ToString()))
                {
                    MessageBox.Show("Mã vật tư chi tiết không được để trống!", "Cảnh báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    return;
                }
                if (dr["SOLUONG"] == DBNull.Value || Convert.ToInt32(dr["SOLUONG"]) <= 0)
                {
                    MessageBox.Show("Số lượng nhập phải lớn hơn 0!", "Cảnh báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    return;
                }
                if (dr["DONGIA"] == DBNull.Value || Convert.ToDouble(dr["DONGIA"]) <= 0)
                {
                    MessageBox.Show("Đơn giá phải lớn hơn 0!", "Cảnh báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    return;
                }
            }

            try
            {
                phieuNhapBindingSource.ResetCurrentItem();

                string maPN = mAPNTextBox.Text.Trim();
                foreach (DataRow dr in this.qLVT_DATHANGDataSet.CTPN.Rows)
                {
                    if (dr.RowState == DataRowState.Deleted) continue;
                    if (dr["MAPN"] == DBNull.Value || string.IsNullOrWhiteSpace(dr["MAPN"].ToString()))
                    {
                        dr["MAPN"] = maPN;
                    }
                    try
                    {
                        if (dr["rowguid"] == DBNull.Value || dr["rowguid"] == null)
                        {
                            dr["rowguid"] = Guid.NewGuid();
                        }
                    }
                    catch { }
                }

                foreach (DataRow dr in this.qLVT_DATHANGDataSet.PhieuNhap.Rows)
                {
                    try
                    {
                        if (dr.RowState == DataRowState.Added && (dr["rowguid"] == DBNull.Value || dr["rowguid"] == null))
                        {
                            dr["rowguid"] = Guid.NewGuid();
                        }
                    }
                    catch { }
                }

                this.phieuNhapTableAdapter.Connection.ConnectionString = Program.connstr;
                this.cTPNTableAdapter.Connection.ConnectionString = Program.connstr;

                this.tableAdapterManager.PhieuNhapTableAdapter = this.phieuNhapTableAdapter;
                this.tableAdapterManager.CTPNTableAdapter = this.cTPNTableAdapter;

                if (!dangThemMoi)
                {
                    // Xử lý hiệu chỉnh: Hoàn lại tồn kho cũ, cập nhật lại tồn kho mới và xóa/thêm lại chi tiết
                    foreach (DataRow dr in this.qLVT_DATHANGDataSet.CTPN.Rows)
                    {
                        if (dr.RowState == DataRowState.Modified)
                        {
                            string maVT = dr["MAVT", DataRowVersion.Original].ToString().Trim();
                            int soLuongCu = Convert.ToInt32(dr["SOLUONG", DataRowVersion.Original]);
                            int soLuongMoi = Convert.ToInt32(dr["SOLUONG", DataRowVersion.Current]);
                            decimal donGiaMoi = Convert.ToDecimal(dr["DONGIA"]);

                            // 1. Hoàn lại kho số lượng cũ (dùng XUAT)
                            string sqlHoanLai = string.Format("EXEC SP_CAPNHAT_SOLUONG_TON '{0}', {1}, 'XUAT'", maVT, soLuongCu);
                            Program.ExecSqlNonQuery(sqlHoanLai);

                            // 2. Cập nhật vào kho số lượng mới (dùng NHAP)
                            string sqlThemMoi = string.Format("EXEC SP_CAPNHAT_SOLUONG_TON '{0}', {1}, 'NHAP'", maVT, soLuongMoi);
                            Program.ExecSqlNonQuery(sqlThemMoi);

                            // 3. Xóa và thêm lại dòng chi tiết bằng SQL trực tiếp tránh lỗi UPDATE rowguid của Replication
                            string sqlXoaThem = string.Format(
                                "DELETE FROM CTPN WHERE MAPN = '{0}' AND MAVT = '{1}';" +
                                "INSERT INTO CTPN (MAPN, MAVT, SOLUONG, DONGIA) VALUES ('{0}', '{1}', {2}, {3});",
                                maPN, maVT, soLuongMoi, donGiaMoi);
                            Program.ExecSqlNonQuery(sqlXoaThem);
                        }
                    }
                    this.qLVT_DATHANGDataSet.AcceptChanges();
                }
                else
                {
                    this.tableAdapterManager.UpdateAll(this.qLVT_DATHANGDataSet);
                }

                if (dangThemMoi)
                {
                    for (int i = 0; i < cTPNDataGridView.Rows.Count; i++)
                    {
                        var row = cTPNDataGridView.Rows[i];
                        if (row.IsNewRow) continue;

                        if (row.Cells["MAVT"].Value != null && row.Cells["SOLUONG"].Value != null)
                        {
                            string maVT = row.Cells["MAVT"].Value.ToString().Trim();
                            int soLuong = Convert.ToInt32(row.Cells["SOLUONG"].Value);

                            string sqlCapNhat = string.Format("EXEC SP_CAPNHAT_SOLUONG_TON '{0}', {1}, 'NHAP'", maVT, soLuong);
                            Program.ExecSqlNonQuery(sqlCapNhat);
                        }
                    }
                }

                this.phieuNhapTableAdapter.Fill(this.qLVT_DATHANGDataSet.PhieuNhap);
                this.cTPNTableAdapter.Fill(this.qLVT_DATHANGDataSet.CTPN);

                dangThemMoi = false;
                groupBox1.Enabled = false;
                cTPNDataGridView.AllowUserToAddRows = false;

                btnThem.Enabled = btnHieuChinh.Enabled = btnXoa.Enabled = btnReload.Enabled = true;
                btnGhi.Enabled = btnPhucHoi.Enabled = false;
                phieuNhapDataGridView.Enabled = true;
                cTPNDataGridView.Enabled = true;

                MessageBox.Show("Ghi phiếu nhập và cập nhật tồn kho thành công!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
            catch (Exception ex)
            {
                MessageBox.Show("Lỗi ghi phiếu nhập: " + ex.Message, "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void btnPhucHoi_Click(object sender, EventArgs e)
        {
            phieuNhapBindingSource.CancelEdit();
            cTPNBindingSource.CancelEdit();

            if (dangThemMoi) phieuNhapBindingSource.Position = vitri;

            this.cTPNTableAdapter.Connection.ConnectionString = Program.connstr;
            this.cTPNTableAdapter.Fill(this.qLVT_DATHANGDataSet.CTPN);

            dangThemMoi = false;
            groupBox1.Enabled = false;
            cTPNDataGridView.AllowUserToAddRows = false;

            btnThem.Enabled = btnHieuChinh.Enabled = btnXoa.Enabled = btnReload.Enabled = true;
            btnGhi.Enabled = btnPhucHoi.Enabled = false;
            phieuNhapDataGridView.Enabled = true;
            cTPNDataGridView.Enabled = true;
        }

        private void btnXoa_Click(object sender, EventArgs e)
        {
            if (cTPNBindingSource.Count > 0)
            {
                MessageBox.Show("Không thể xóa phiếu nhập đã có chi tiết vật tư!", "Cảnh báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            if (MessageBox.Show("Bạn có thực sự muốn xóa phiếu nhập này?", "Xác nhận", MessageBoxButtons.OKCancel, MessageBoxIcon.Question) == DialogResult.OK)
            {
                try
                {
                    phieuNhapBindingSource.RemoveCurrent();
                    this.phieuNhapTableAdapter.Connection.ConnectionString = Program.connstr;
                    this.phieuNhapTableAdapter.Update(this.qLVT_DATHANGDataSet.PhieuNhap);
                    MessageBox.Show("Xóa phiếu nhập thành công!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
                catch (Exception ex)
                {
                    MessageBox.Show("Lỗi xóa phiếu nhập: " + ex.Message, "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    this.phieuNhapTableAdapter.Fill(this.qLVT_DATHANGDataSet.PhieuNhap);
                }
            }
        }

        private void btnReload_Click(object sender, EventArgs e)
        {
            try
            {
                this.phieuNhapTableAdapter.Connection.ConnectionString = Program.connstr;
                this.phieuNhapTableAdapter.Fill(this.qLVT_DATHANGDataSet.PhieuNhap);

                this.cTPNTableAdapter.Connection.ConnectionString = Program.connstr;
                this.cTPNTableAdapter.Fill(this.qLVT_DATHANGDataSet.CTPN);
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

        private void btnChonDDH_Click(object sender, EventArgs e)
        {
            frmChonDonDatHang f = new frmChonDonDatHang();
            if (f.ShowDialog() == DialogResult.OK)
            {
                masoDDHTextBox.Text = f.MaSoDDH;
                mAKHOTextBox.Text = f.MaKho;

                DataRowView currentPN = (DataRowView)phieuNhapBindingSource.Current;
                currentPN["MasoDDH"] = f.MaSoDDH;
                currentPN["MAKHO"] = f.MaKho;

                DoDuLieuTuCTDDHSangCTPN(f.MaSoDDH);
            }
        }

        private void DoDuLieuTuCTDDHSangCTPN(string maSoDDH)
        {
            string sql = "SELECT MAVT, SOLUONG, DONGIA FROM CTDDH WHERE RTRIM(MasoDDH) = '" + maSoDDH.Trim() + "'";
            DataTable dtCTDDH = new DataTable();

            try
            {
                if (Program.conn.State == ConnectionState.Closed) Program.conn.Open();
                SqlDataAdapter da = new SqlDataAdapter(sql, Program.conn);
                da.Fill(dtCTDDH);

                if (dtCTDDH.Rows.Count == 0)
                {
                    MessageBox.Show("Đơn đặt hàng này không có chi tiết vật tư nào!", "Cảnh báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    return;
                }

                string maPN = mAPNTextBox.Text.Trim();

                for (int i = qLVT_DATHANGDataSet.CTPN.Rows.Count - 1; i >= 0; i--)
                {
                    DataRow dr = qLVT_DATHANGDataSet.CTPN.Rows[i];
                    if (dr.RowState != DataRowState.Deleted && dr["MAPN"]?.ToString().Trim() == maPN)
                    {
                        dr.Delete();
                    }
                }

                foreach (DataRow row in dtCTDDH.Rows)
                {
                    DataRow newRow = qLVT_DATHANGDataSet.CTPN.NewRow();
                    newRow["MAPN"] = maPN;
                    newRow["MAVT"] = row["MAVT"].ToString().Trim();
                    newRow["SOLUONG"] = Convert.ToInt32(row["SOLUONG"]);
                    newRow["DONGIA"] = Convert.ToDouble(row["DONGIA"]);

                    try { newRow["rowguid"] = Guid.NewGuid(); } catch { }

                    qLVT_DATHANGDataSet.CTPN.Rows.Add(newRow);
                }

                cTPNBindingSource.ResetBindings(false);
            }
            catch (Exception ex)
            {
                MessageBox.Show("Lỗi lấy chi tiết vật tư từ đơn đặt hàng: " + ex.Message, "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void cTPNDataGridView_CellValidating(object sender, DataGridViewCellValidatingEventArgs e)
        {
            if (cTPNDataGridView.Columns[e.ColumnIndex].Name == "SOLUONG")
            {
                int soLuongNhap = 0;
                if (!int.TryParse(e.FormattedValue.ToString(), out soLuongNhap) || soLuongNhap <= 0)
                {
                    MessageBox.Show("Số lượng nhập phải là số nguyên lớn hơn 0!", "Cảnh báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    e.Cancel = true;
                    return;
                }

                DataRowView pnRow = (DataRowView)phieuNhapBindingSource.Current;
                string maSoDDH = pnRow["MasoDDH"]?.ToString().Trim();
                string maVT = cTPNDataGridView.Rows[e.RowIndex].Cells["MAVT"].Value?.ToString().Trim();

                if (!string.IsNullOrEmpty(maSoDDH) && !string.IsNullOrEmpty(maVT))
                {
                    string query = string.Format("SELECT SOLUONG FROM CTDDH WHERE MasoDDH = '{0}' AND MAVT = '{1}'", maSoDDH, maVT);
                    Program.myReader = Program.ExecSqlDataReader(query);

                    if (Program.myReader != null)
                    {
                        try
                        {
                            if (Program.myReader.Read())
                            {
                                int soLuongDat = Program.myReader.GetInt32(0);

                                if (soLuongNhap > soLuongDat)
                                {
                                    MessageBox.Show(string.Format("Số lượng nhập ({0}) không được vượt quá số lượng đã đặt ({1})!", soLuongNhap, soLuongDat),
                                                    "Cảnh báo vi phạm số lượng", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                                    e.Cancel = true;
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
        }

        private void menuThemCT_Click(object sender, EventArgs e)
        {
            if (phieuNhapBindingSource.Count == 0)
            {
                MessageBox.Show("Vui lòng chọn phiếu nhập trước khi thêm chi tiết!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            try
            {
                cTPNBindingSource.AddNew();
                DataRowView drv = (DataRowView)phieuNhapBindingSource.Current;
                string maPN = drv["MAPN"].ToString().Trim();

                DataRowView currentCT = (DataRowView)cTPNBindingSource.Current;
                currentCT["MAPN"] = maPN;
                try { currentCT["rowguid"] = Guid.NewGuid(); } catch { }
                currentCT["SOLUONG"] = 1;
                currentCT["DONGIA"] = 0;

                btnGhi.Enabled = true;
                btnPhucHoi.Enabled = true;
            }
            catch (Exception ex)
            {
                MessageBox.Show("Lỗi thêm chi tiết phiếu nhập: " + ex.Message, "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void menuHieuChinhCT_Click(object sender, EventArgs e)
        {
            if (cTPNBindingSource.Count == 0)
            {
                MessageBox.Show("Không có dòng chi tiết nào để hiệu chỉnh!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            cTPNBindingSource.EndEdit();
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
            if (cTPNBindingSource.Count == 0)
            {
                MessageBox.Show("Không có dòng chi tiết nào để xóa!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            if (MessageBox.Show("Bạn có thực sự muốn xóa dòng chi tiết này?", "Xác nhận", MessageBoxButtons.OKCancel, MessageBoxIcon.Question) == DialogResult.OK)
            {
                try
                {
                    cTPNBindingSource.RemoveCurrent();
                    this.cTPNTableAdapter.Connection.ConnectionString = Program.connstr;
                    this.cTPNTableAdapter.Update(this.qLVT_DATHANGDataSet.CTPN);
                    MessageBox.Show("Xóa chi tiết thành công!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
                catch (Exception ex)
                {
                    MessageBox.Show("Lỗi xóa chi tiết: " + ex.Message, "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    this.cTPNTableAdapter.Fill(this.qLVT_DATHANGDataSet.CTPN);
                }
            }
        }

        private void splitContainer1_Panel1_Paint(object sender, PaintEventArgs e)
        {
            // Do nothing
        }

        private void cTPNDataGridView_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            // Do nothing
        }
    }
}