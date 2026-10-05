using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace QLVT_DATHANG
{
    public partial class frmKho : Form
    {
        int viTri = 0;
        bool dangThemMoi = false;
        string macn = "";

        public frmKho()
        {
            InitializeComponent();
        }

        private void frmKho_Load(object sender, EventArgs e)
        {
            this.qLVT_DATHANGDataSet.EnforceConstraints = false;

            this.khoTableAdapter.Connection.ConnectionString = Program.connstr;
            this.khoTableAdapter.Fill(this.qLVT_DATHANGDataSet.Kho);

            if (khoBindingSource.Count > 0)
            {
                macn = ((DataRowView)khoBindingSource[0])["MACN"].ToString().Trim();
            }
            else
            {
                macn = Program.mChiNhanh == 0 ? "CN1" : "CN2";
            }

            cmbChiNhanh.SelectedIndexChanged -= cmbChiNhanh_SelectedIndexChanged;

            if (Program.bds_dspm != null)
            {
                cmbChiNhanh.DataSource = Program.bds_dspm;
                cmbChiNhanh.DisplayMember = "TENCN";
                cmbChiNhanh.ValueMember = "TENSERVER";
                cmbChiNhanh.SelectedIndex = Program.mChiNhanh;
            }

            cmbChiNhanh.SelectedIndexChanged += cmbChiNhanh_SelectedIndexChanged;

            if (Program.mGroup == "CONGTY")
            {
                cmbChiNhanh.Enabled = true;
                btnThem.Enabled = btnHieuChinh.Enabled = btnXoa.Enabled = btnGhi.Enabled = btnPhucHoi.Enabled = false;
                btnReload.Enabled = btnThoat.Enabled = true;
            }
            else
            {
                cmbChiNhanh.Enabled = false;
                btnThem.Enabled = btnHieuChinh.Enabled = btnXoa.Enabled = btnReload.Enabled = btnThoat.Enabled = true;
                btnGhi.Enabled = btnPhucHoi.Enabled = false;
            }

            groupBox1.Enabled = false;
            mACNTextBox.Enabled = false;
        }

        private void toolStrip1_ItemClicked(object sender, ToolStripItemClickedEventArgs e)
        {

        }

        private void groupBox1_Enter(object sender, EventArgs e)
        {

        }

        private void khoBindingNavigatorSaveItem_Click(object sender, EventArgs e)
        {
            this.Validate();
            this.khoBindingSource.EndEdit();
            this.tableAdapterManager.UpdateAll(this.qLVT_DATHANGDataSet);
        }

        private void cmbChiNhanh_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (cmbChiNhanh.SelectedValue == null || cmbChiNhanh.SelectedValue.ToString() == "System.Data.DataRowView")
                return;

            string serverDuocChon = cmbChiNhanh.SelectedValue.ToString();

            if (cmbChiNhanh.SelectedIndex != Program.mChiNhanh)
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

            if (Program.KetNoi() == 0)
            {
                MessageBox.Show("Lỗi kết nối về chi nhánh mới!", "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            try
            {
                this.qLVT_DATHANGDataSet.EnforceConstraints = false;
                this.khoTableAdapter.Connection.ConnectionString = Program.connstr;
                this.khoTableAdapter.Fill(this.qLVT_DATHANGDataSet.Kho);
                this.khoBindingSource.ResetBindings(false);

                if (khoBindingSource.Count > 0)
                {
                    macn = ((DataRowView)khoBindingSource[0])["MACN"].ToString().Trim();
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Lỗi nạp dữ liệu kho: " + ex.Message, "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void btnThem_Click(object sender, EventArgs e)
        {
            viTri = khoBindingSource.Position;
            groupBox1.Enabled = true;
            khoBindingSource.AddNew();

            mACNTextBox.Text = macn;

            khoDataGridView.Enabled = false;
            btnThem.Enabled = btnHieuChinh.Enabled = btnXoa.Enabled = btnReload.Enabled = btnThoat.Enabled = false;
            btnGhi.Enabled = btnPhucHoi.Enabled = true;
            dangThemMoi = true;

            mAKHOTextBox.Enabled = true;
            mAKHOTextBox.Focus();
        }

        private void btnHieuChinh_Click(object sender, EventArgs e)
        {
            if (khoBindingSource.Count == 0)
            {
                MessageBox.Show("Không có kho nào để hiệu chỉnh!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
                return;
            }

            viTri = khoBindingSource.Position;
            groupBox1.Enabled = true;

            mAKHOTextBox.Enabled = false;
            mACNTextBox.Enabled = false;

            khoDataGridView.Enabled = false;
            btnThem.Enabled = btnHieuChinh.Enabled = btnXoa.Enabled = btnReload.Enabled = btnThoat.Enabled = false;
            btnGhi.Enabled = btnPhucHoi.Enabled = true;
            dangThemMoi = false;
        }

        private void btnGhi_Click(object sender, EventArgs e)
        {
            if (string.IsNullOrWhiteSpace(mAKHOTextBox.Text))
            {
                MessageBox.Show("Mã kho không được để trống!", "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                mAKHOTextBox.Focus();
                return;
            }

            if (string.IsNullOrWhiteSpace(tENKHOTextBox.Text))
            {
                MessageBox.Show("Tên kho không được để trống!", "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                tENKHOTextBox.Focus();
                return;
            }

            if (string.IsNullOrWhiteSpace(dIACHITextBox.Text))
            {
                MessageBox.Show("Địa chỉ kho không được để trống!", "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                dIACHITextBox.Focus();
                return;
            }

            try
            {
                khoBindingSource.EndEdit();
                khoBindingSource.ResetCurrentItem();
                this.khoTableAdapter.Connection.ConnectionString = Program.connstr;
                this.khoTableAdapter.Update(this.qLVT_DATHANGDataSet.Kho);

                MessageBox.Show("Ghi dữ liệu kho thành công!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
            catch (Exception ex)
            {
                MessageBox.Show("Lỗi ghi kho (có thể trùng Mã kho hoặc Tên kho): \n" + ex.Message, "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            groupBox1.Enabled = false;
            khoDataGridView.Enabled = true;
            btnThem.Enabled = btnHieuChinh.Enabled = btnXoa.Enabled = btnReload.Enabled = btnThoat.Enabled = true;
            btnGhi.Enabled = btnPhucHoi.Enabled = false;
        }

        private void btnXoa_Click(object sender, EventArgs e)
        {
            if (khoBindingSource.Count == 0)
            {
                MessageBox.Show("Không có kho nào để xóa!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
                return;
            }

            string maKho = ((DataRowView)khoBindingSource[khoBindingSource.Position])["MAKHO"].ToString().Trim();

            if (MessageBox.Show("Bạn có chắc chắn muốn xóa kho này?", "Xác nhận", MessageBoxButtons.OKCancel, MessageBoxIcon.Question) == DialogResult.OK)
            {
                try
                {
                    khoBindingSource.RemoveCurrent();
                    this.khoTableAdapter.Connection.ConnectionString = Program.connstr;
                    this.khoTableAdapter.Update(this.qLVT_DATHANGDataSet.Kho);

                    MessageBox.Show("Xóa kho thành công!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
                catch (Exception ex)
                {
                    MessageBox.Show("Lỗi xóa kho (Kho này đã phát sinh phiếu Đặt hàng/Nhập/Xuất): \n" + ex.Message, "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    this.khoTableAdapter.Fill(this.qLVT_DATHANGDataSet.Kho);
                    khoBindingSource.Position = khoBindingSource.Find("MAKHO", maKho);
                }
            }
        }

        private void btnPhucHoi_Click(object sender, EventArgs e)
        {
            khoBindingSource.CancelEdit();
            if (dangThemMoi)
            {
                khoBindingSource.Position = viTri;
            }

            groupBox1.Enabled = false;
            khoDataGridView.Enabled = true;
            btnThem.Enabled = btnHieuChinh.Enabled = btnXoa.Enabled = btnReload.Enabled = btnThoat.Enabled = true;
            btnGhi.Enabled = btnPhucHoi.Enabled = false;
        }

        private void btnReload_Click(object sender, EventArgs e)
        {
            try
            {
                this.khoTableAdapter.Connection.ConnectionString = Program.connstr;
                this.khoTableAdapter.Fill(this.qLVT_DATHANGDataSet.Kho);
            }
            catch (Exception ex)
            {
                MessageBox.Show("Lỗi tải lại danh sách kho: " + ex.Message, "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void btnThoat_Click(object sender, EventArgs e)
        {
            this.Close();
        }
    }
}