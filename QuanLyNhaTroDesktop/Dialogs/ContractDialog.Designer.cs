// ------------------------------------------------------------------------------
//  GIAO DIỆN CỦA ContractDialog (mở bằng Visual Studio Designer: nhấp đúp file hoặc Shift+F7).
//
//  ĐƠN VỊ Ở ĐÂY LÀ ĐƠN VỊ THIẾT KẾ 96 DPI — lúc chạy, app tự nhân kích thước theo
//  tỉ lệ màn hình (100% / 125% / 150% / 200%) qua Dpi.ScaleForm. Vì vậy:
//    • Sửa VỊ TRÍ / KÍCH THƯỚC / CHỮ / MÀU thoải mái — lúc chạy vẫn đúng.
//    • KHÔNG đổi AutoScaleMode (phải là None), nếu không sẽ bị nhân kích thước 2 lần.
//    • Cỡ chữ khai bằng point (pt) nên tự đúng ở mọi DPI — không cần tự nhân.
//    • Thêm control mới: kéo từ Toolbox thả vào đây là được, app tự nhân DPI lúc chạy.
// ------------------------------------------------------------------------------
namespace QuanLyNhaTroDesktop.Dialogs
{
    partial class ContractDialog
    {
        private System.ComponentModel.IContainer components = null;

        private System.Windows.Forms.TableLayoutPanel layout;
        private System.Windows.Forms.Label lblPhong;
        private System.Windows.Forms.ComboBox cboRoom;
        private System.Windows.Forms.Label lblNguoiThue;
        private System.Windows.Forms.ComboBox cboTenant;
        private System.Windows.Forms.Label lblNgayBatDau;
        private System.Windows.Forms.DateTimePicker dtpStart;
        private System.Windows.Forms.Label lblNgayKetThuc;
        private System.Windows.Forms.DateTimePicker dtpEnd;
        private System.Windows.Forms.Label lblGiaThueDThang;
        private System.Windows.Forms.TextBox txtGia;
        private System.Windows.Forms.Label lblTienCocD;
        private System.Windows.Forms.TextBox txtCoc;
        private System.Windows.Forms.FlowLayoutPanel pnlButtons;
        private System.Windows.Forms.Button btnOk;
        private System.Windows.Forms.Button btnCancel;

        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        private void InitializeComponent()
        {
            layout = new TableLayoutPanel();
            lblPhong = new Label();
            cboRoom = new ComboBox();
            lblNguoiThue = new Label();
            cboTenant = new ComboBox();
            lblNgayBatDau = new Label();
            dtpStart = new DateTimePicker();
            lblNgayKetThuc = new Label();
            dtpEnd = new DateTimePicker();
            lblGiaThueDThang = new Label();
            txtGia = new TextBox();
            lblTienCocD = new Label();
            txtCoc = new TextBox();
            pnlButtons = new FlowLayoutPanel();
            btnOk = new Button();
            btnCancel = new Button();
            layout.SuspendLayout();
            pnlButtons.SuspendLayout();
            SuspendLayout();
            // 
            // layout
            // 
            layout.AutoScroll = true;
            layout.ColumnCount = 2;
            layout.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 38F));
            layout.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 62F));
            layout.Controls.Add(lblPhong, 0, 0);
            layout.Controls.Add(cboRoom, 1, 0);
            layout.Controls.Add(lblNguoiThue, 0, 1);
            layout.Controls.Add(cboTenant, 1, 1);
            layout.Controls.Add(lblNgayBatDau, 0, 2);
            layout.Controls.Add(dtpStart, 1, 2);
            layout.Controls.Add(lblNgayKetThuc, 0, 3);
            layout.Controls.Add(dtpEnd, 1, 3);
            layout.Controls.Add(lblGiaThueDThang, 0, 4);
            layout.Controls.Add(txtGia, 1, 4);
            layout.Controls.Add(lblTienCocD, 0, 5);
            layout.Controls.Add(txtCoc, 1, 5);
            layout.Controls.Add(pnlButtons, 0, 6);
            layout.Dock = DockStyle.Fill;
            layout.Location = new Point(0, 0);
            layout.Name = "layout";
            layout.Padding = new Padding(7, 6, 7, 6);
            layout.RowCount = 7;
            layout.RowStyles.Add(new RowStyle(SizeType.Absolute, 24F));
            layout.RowStyles.Add(new RowStyle(SizeType.Absolute, 24F));
            layout.RowStyles.Add(new RowStyle(SizeType.Absolute, 24F));
            layout.RowStyles.Add(new RowStyle(SizeType.Absolute, 24F));
            layout.RowStyles.Add(new RowStyle(SizeType.Absolute, 24F));
            layout.RowStyles.Add(new RowStyle(SizeType.Absolute, 24F));
            layout.RowStyles.Add(new RowStyle(SizeType.Absolute, 50F));
            layout.Size = new Size(465, 222);
            layout.TabIndex = 0;
            // 
            // lblPhong
            // 
            lblPhong.Dock = DockStyle.Fill;
            lblPhong.Location = new Point(8, 6);
            lblPhong.Margin = new Padding(1, 0, 4, 0);
            lblPhong.Name = "lblPhong";
            lblPhong.Size = new Size(166, 24);
            lblPhong.TabIndex = 0;
            lblPhong.Text = "Phòng:";
            lblPhong.TextAlign = ContentAlignment.MiddleLeft;
            // 
            // cboRoom
            // 
            cboRoom.Anchor = AnchorStyles.Left | AnchorStyles.Right;
            cboRoom.Location = new Point(179, 8);
            cboRoom.Margin = new Padding(1, 2, 1, 2);
            cboRoom.Name = "cboRoom";
            cboRoom.Size = new Size(278, 28);
            cboRoom.TabIndex = 1;
            // 
            // lblNguoiThue
            // 
            lblNguoiThue.Dock = DockStyle.Fill;
            lblNguoiThue.Location = new Point(8, 30);
            lblNguoiThue.Margin = new Padding(1, 0, 4, 0);
            lblNguoiThue.Name = "lblNguoiThue";
            lblNguoiThue.Size = new Size(166, 24);
            lblNguoiThue.TabIndex = 2;
            lblNguoiThue.Text = "Người thuê:";
            lblNguoiThue.TextAlign = ContentAlignment.MiddleLeft;
            // 
            // cboTenant
            // 
            cboTenant.Anchor = AnchorStyles.Left | AnchorStyles.Right;
            cboTenant.Location = new Point(179, 32);
            cboTenant.Margin = new Padding(1, 2, 1, 2);
            cboTenant.Name = "cboTenant";
            cboTenant.Size = new Size(278, 28);
            cboTenant.TabIndex = 3;
            // 
            // lblNgayBatDau
            // 
            lblNgayBatDau.Dock = DockStyle.Fill;
            lblNgayBatDau.Location = new Point(8, 54);
            lblNgayBatDau.Margin = new Padding(1, 0, 4, 0);
            lblNgayBatDau.Name = "lblNgayBatDau";
            lblNgayBatDau.Size = new Size(166, 24);
            lblNgayBatDau.TabIndex = 4;
            lblNgayBatDau.Text = "Ngày bắt đầu:";
            lblNgayBatDau.TextAlign = ContentAlignment.MiddleLeft;
            // 
            // dtpStart
            // 
            dtpStart.Anchor = AnchorStyles.Left | AnchorStyles.Right;
            dtpStart.Format = DateTimePickerFormat.Short;
            dtpStart.Location = new Point(179, 56);
            dtpStart.Margin = new Padding(1, 2, 1, 2);
            dtpStart.Name = "dtpStart";
            dtpStart.Size = new Size(278, 27);
            dtpStart.TabIndex = 5;
            dtpStart.Value = new DateTime(2026, 9, 23, 0, 0, 0, 0);
            // 
            // lblNgayKetThuc
            // 
            lblNgayKetThuc.Dock = DockStyle.Fill;
            lblNgayKetThuc.Location = new Point(8, 78);
            lblNgayKetThuc.Margin = new Padding(1, 0, 4, 0);
            lblNgayKetThuc.Name = "lblNgayKetThuc";
            lblNgayKetThuc.Size = new Size(166, 24);
            lblNgayKetThuc.TabIndex = 6;
            lblNgayKetThuc.Text = "Ngày kết thúc:";
            lblNgayKetThuc.TextAlign = ContentAlignment.MiddleLeft;
            // 
            // dtpEnd
            // 
            dtpEnd.Anchor = AnchorStyles.Left | AnchorStyles.Right;
            dtpEnd.Format = DateTimePickerFormat.Short;
            dtpEnd.Location = new Point(179, 80);
            dtpEnd.Margin = new Padding(1, 2, 1, 2);
            dtpEnd.Name = "dtpEnd";
            dtpEnd.Size = new Size(278, 27);
            dtpEnd.TabIndex = 7;
            dtpEnd.Value = new DateTime(2027, 9, 22, 0, 0, 0, 0);
            // 
            // lblGiaThueDThang
            // 
            lblGiaThueDThang.Dock = DockStyle.Fill;
            lblGiaThueDThang.Location = new Point(8, 102);
            lblGiaThueDThang.Margin = new Padding(1, 0, 4, 0);
            lblGiaThueDThang.Name = "lblGiaThueDThang";
            lblGiaThueDThang.Size = new Size(166, 24);
            lblGiaThueDThang.TabIndex = 8;
            lblGiaThueDThang.Text = "Giá thuê (đ/tháng):";
            lblGiaThueDThang.TextAlign = ContentAlignment.MiddleLeft;
            // 
            // txtGia
            // 
            txtGia.Anchor = AnchorStyles.Left | AnchorStyles.Right;
            txtGia.Location = new Point(179, 104);
            txtGia.Margin = new Padding(1, 2, 1, 2);
            txtGia.Name = "txtGia";
            txtGia.Size = new Size(278, 27);
            txtGia.TabIndex = 9;
            txtGia.Text = "1500000";
            // 
            // lblTienCocD
            // 
            lblTienCocD.Dock = DockStyle.Fill;
            lblTienCocD.Location = new Point(8, 126);
            lblTienCocD.Margin = new Padding(1, 0, 4, 0);
            lblTienCocD.Name = "lblTienCocD";
            lblTienCocD.Size = new Size(166, 24);
            lblTienCocD.TabIndex = 10;
            lblTienCocD.Text = "Tiền cọc (đ):";
            lblTienCocD.TextAlign = ContentAlignment.MiddleLeft;
            // 
            // txtCoc
            // 
            txtCoc.Anchor = AnchorStyles.Left | AnchorStyles.Right;
            txtCoc.Location = new Point(179, 128);
            txtCoc.Margin = new Padding(1, 2, 1, 2);
            txtCoc.Name = "txtCoc";
            txtCoc.Size = new Size(278, 27);
            txtCoc.TabIndex = 11;
            // 
            // pnlButtons
            // 
            pnlButtons.AutoSize = true;
            layout.SetColumnSpan(pnlButtons, 2);
            pnlButtons.Controls.Add(btnOk);
            pnlButtons.Controls.Add(btnCancel);
            pnlButtons.Dock = DockStyle.Fill;
            pnlButtons.FlowDirection = FlowDirection.RightToLeft;
            pnlButtons.Location = new Point(8, 152);
            pnlButtons.Margin = new Padding(1, 2, 1, 2);
            pnlButtons.Name = "pnlButtons";
            pnlButtons.Size = new Size(449, 62);
            pnlButtons.TabIndex = 12;
            // 
            // btnOk
            // 
            btnOk.BackColor = Color.FromArgb(37, 99, 235);
            btnOk.Cursor = Cursors.Hand;
            btnOk.FlatAppearance.BorderSize = 0;
            btnOk.FlatStyle = FlatStyle.Flat;
            btnOk.ForeColor = Color.FromArgb(255, 255, 255);
            btnOk.Location = new Point(319, 0);
            btnOk.Margin = new Padding(8, 0, 0, 0);
            btnOk.Name = "btnOk";
            btnOk.Size = new Size(130, 40);
            btnOk.TabIndex = 0;
            btnOk.Text = "Lập hợp đồng";
            btnOk.UseVisualStyleBackColor = false;
            // 
            // btnCancel
            // 
            btnCancel.Location = new Point(181, 0);
            btnCancel.Margin = new Padding(8, 0, 0, 0);
            btnCancel.Name = "btnCancel";
            btnCancel.Size = new Size(130, 40);
            btnCancel.TabIndex = 1;
            btnCancel.Text = "Hủy";
            btnCancel.UseVisualStyleBackColor = false;
            // 
            // ContractDialog
            // 
            AutoScaleMode = AutoScaleMode.None;
            BackColor = Color.FromArgb(255, 255, 255);
            ClientSize = new Size(465, 222);
            Controls.Add(layout);
            Font = new Font("Segoe UI", 11F);
            FormBorderStyle = FormBorderStyle.FixedDialog;
            MaximizeBox = false;
            Name = "ContractDialog";
            StartPosition = FormStartPosition.CenterParent;
            Text = "Lập hợp đồng thuê phòng";
            layout.ResumeLayout(false);
            layout.PerformLayout();
            pnlButtons.ResumeLayout(false);
            ResumeLayout(false);
        }
    }
}
