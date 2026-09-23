// ------------------------------------------------------------------------------
//  GIAO DIỆN CỦA PaymentDialog (mở bằng Visual Studio Designer: nhấp đúp file hoặc Shift+F7).
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
    partial class PaymentDialog
    {
        private System.ComponentModel.IContainer components = null;

        private System.Windows.Forms.TableLayoutPanel layoutRoot;
        private System.Windows.Forms.Label lblInfo;
        private System.Windows.Forms.TableLayoutPanel layout;
        private System.Windows.Forms.Label lblSoTienKhachTraD;
        private System.Windows.Forms.TextBox txtSoTien;
        private System.Windows.Forms.Label lblPhuongThuc;
        private System.Windows.Forms.ComboBox cboPhuongThuc;
        private System.Windows.Forms.Label lblGhiChu;
        private System.Windows.Forms.TextBox txtGhiChu;
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
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.None;
            this.Font = new System.Drawing.Font("Segoe UI", 11F);
            this.ClientSize = new System.Drawing.Size(510, 306);
            this.Text = "Thanh toán hóa đơn";
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog;
            this.MaximizeBox = false;
            this.MinimizeBox = true;
            this.ShowInTaskbar = true;
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
            this.WindowState = System.Windows.Forms.FormWindowState.Normal;
            this.BackColor = System.Drawing.Color.FromArgb(255, 255, 255, 255);
            this.Name = "PaymentDialog";
            this.SuspendLayout();
            this.layoutRoot = new System.Windows.Forms.TableLayoutPanel();
            this.layoutRoot.Name = "layoutRoot";
            this.layoutRoot.Location = new System.Drawing.Point(0, 0);
            this.layoutRoot.Size = new System.Drawing.Size(510, 306);
            this.layoutRoot.Dock = System.Windows.Forms.DockStyle.Fill;
            this.layoutRoot.Margin = new System.Windows.Forms.Padding(3, 3, 3, 3);
            this.layoutRoot.ColumnCount = 1;
            this.layoutRoot.RowCount = 0;
            this.layoutRoot.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.layoutRoot.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.AutoSize, 0F));
            this.layoutRoot.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 50F));
            this.layoutRoot.ColumnCount = 1;
            this.lblInfo = new System.Windows.Forms.Label();
            this.lblInfo.Name = "lblInfo";
            this.lblInfo.Location = new System.Drawing.Point(2, 0);
            this.lblInfo.Size = new System.Drawing.Size(50, 12);
            this.lblInfo.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.lblInfo.AutoSize = false;
            this.lblInfo.Text = "Hóa đơn: HD-DESIGN\nPhòng:  — Kỳ: 2026-09\nTỔNG TIỀN: 1,500,000 đ";
            this.lblInfo.TextAlign = System.Drawing.ContentAlignment.TopLeft;
            this.layout = new System.Windows.Forms.TableLayoutPanel();
            this.layout.Name = "layout";
            this.layout.Location = new System.Drawing.Point(3, 15);
            this.layout.Size = new System.Drawing.Size(504, 288);
            this.layout.Dock = System.Windows.Forms.DockStyle.Fill;
            this.layout.Margin = new System.Windows.Forms.Padding(3, 3, 3, 3);
            this.layout.Padding = new System.Windows.Forms.Padding(7, 6, 7, 6);
            this.layout.ColumnCount = 2;
            this.layout.RowCount = 4;
            this.layout.AutoScroll = true;
            this.layout.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 40F));
            this.layout.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 60F));
            this.layout.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 24F));
            this.layout.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 24F));
            this.layout.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 24F));
            this.layout.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 50F));
            this.lblSoTienKhachTraD = new System.Windows.Forms.Label();
            this.lblSoTienKhachTraD.Name = "lblSoTienKhachTraD";
            this.lblSoTienKhachTraD.Location = new System.Drawing.Point(8, 6);
            this.lblSoTienKhachTraD.Size = new System.Drawing.Size(191, 24);
            this.lblSoTienKhachTraD.Dock = System.Windows.Forms.DockStyle.Fill;
            this.lblSoTienKhachTraD.Margin = new System.Windows.Forms.Padding(1, 0, 4, 0);
            this.lblSoTienKhachTraD.AutoSize = false;
            this.lblSoTienKhachTraD.Text = "Số tiền khách trả (đ):";
            this.lblSoTienKhachTraD.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.txtSoTien = new System.Windows.Forms.TextBox();
            this.txtSoTien.Name = "txtSoTien";
            this.txtSoTien.Location = new System.Drawing.Point(204, 8);
            this.txtSoTien.Size = new System.Drawing.Size(292, 20);
            this.txtSoTien.Anchor = ((System.Windows.Forms.AnchorStyles)(System.Windows.Forms.AnchorStyles.Left | System.Windows.Forms.AnchorStyles.Right));
            this.txtSoTien.Margin = new System.Windows.Forms.Padding(1, 2, 1, 2);
            this.txtSoTien.Text = "1500000";
            this.lblPhuongThuc = new System.Windows.Forms.Label();
            this.lblPhuongThuc.Name = "lblPhuongThuc";
            this.lblPhuongThuc.Location = new System.Drawing.Point(8, 30);
            this.lblPhuongThuc.Size = new System.Drawing.Size(191, 24);
            this.lblPhuongThuc.Dock = System.Windows.Forms.DockStyle.Fill;
            this.lblPhuongThuc.Margin = new System.Windows.Forms.Padding(1, 0, 4, 0);
            this.lblPhuongThuc.AutoSize = false;
            this.lblPhuongThuc.Text = "Phương thức:";
            this.lblPhuongThuc.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.cboPhuongThuc = new System.Windows.Forms.ComboBox();
            this.cboPhuongThuc.Name = "cboPhuongThuc";
            this.cboPhuongThuc.Location = new System.Drawing.Point(204, 32);
            this.cboPhuongThuc.Size = new System.Drawing.Size(292, 24);
            this.cboPhuongThuc.Anchor = ((System.Windows.Forms.AnchorStyles)(System.Windows.Forms.AnchorStyles.Left | System.Windows.Forms.AnchorStyles.Right));
            this.cboPhuongThuc.Margin = new System.Windows.Forms.Padding(1, 2, 1, 2);
            this.cboPhuongThuc.Text = "Tiền mặt";
            this.cboPhuongThuc.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cboPhuongThuc.Items.AddRange(new object[] { "Tiền mặt", "Chuyển khoản" });
            this.lblGhiChu = new System.Windows.Forms.Label();
            this.lblGhiChu.Name = "lblGhiChu";
            this.lblGhiChu.Location = new System.Drawing.Point(8, 54);
            this.lblGhiChu.Size = new System.Drawing.Size(191, 24);
            this.lblGhiChu.Dock = System.Windows.Forms.DockStyle.Fill;
            this.lblGhiChu.Margin = new System.Windows.Forms.Padding(1, 0, 4, 0);
            this.lblGhiChu.AutoSize = false;
            this.lblGhiChu.Text = "Ghi chú:";
            this.lblGhiChu.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.txtGhiChu = new System.Windows.Forms.TextBox();
            this.txtGhiChu.Name = "txtGhiChu";
            this.txtGhiChu.Location = new System.Drawing.Point(204, 56);
            this.txtGhiChu.Size = new System.Drawing.Size(292, 20);
            this.txtGhiChu.Anchor = ((System.Windows.Forms.AnchorStyles)(System.Windows.Forms.AnchorStyles.Left | System.Windows.Forms.AnchorStyles.Right));
            this.txtGhiChu.Margin = new System.Windows.Forms.Padding(1, 2, 1, 2);
            this.pnlButtons = new System.Windows.Forms.FlowLayoutPanel();
            this.pnlButtons.Name = "pnlButtons";
            this.pnlButtons.Location = new System.Drawing.Point(8, 80);
            this.pnlButtons.Size = new System.Drawing.Size(488, 200);
            this.pnlButtons.Dock = System.Windows.Forms.DockStyle.Fill;
            this.pnlButtons.Margin = new System.Windows.Forms.Padding(1, 2, 1, 2);
            this.pnlButtons.AutoSize = true;
            this.pnlButtons.FlowDirection = System.Windows.Forms.FlowDirection.RightToLeft;
            this.pnlButtons.WrapContents = true;
            this.btnOk = new System.Windows.Forms.Button();
            this.btnOk.Name = "btnOk";
            this.btnOk.Location = new System.Drawing.Point(327, 0);
            this.btnOk.Size = new System.Drawing.Size(161, 40);
            this.btnOk.Margin = new System.Windows.Forms.Padding(8, 0, 0, 0);
            this.btnOk.BackColor = System.Drawing.Color.FromArgb(255, 22, 163, 74);
            this.btnOk.Text = "Xác nhận thanh toán";
            this.btnOk.ForeColor = System.Drawing.Color.FromArgb(255, 255, 255, 255);
            this.btnOk.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnOk.FlatAppearance.BorderSize = 0;
            this.btnOk.Cursor = System.Windows.Forms.Cursors.Hand;
            this.btnOk.UseVisualStyleBackColor = false;
            this.btnCancel = new System.Windows.Forms.Button();
            this.btnCancel.Name = "btnCancel";
            this.btnCancel.Location = new System.Drawing.Point(189, 0);
            this.btnCancel.Size = new System.Drawing.Size(130, 40);
            this.btnCancel.Margin = new System.Windows.Forms.Padding(8, 0, 0, 0);
            this.btnCancel.Text = "Hủy";
            this.btnCancel.UseVisualStyleBackColor = false;
            this.Controls.Add(this.layoutRoot);
            this.layoutRoot.Controls.Add(this.lblInfo, 0, 0);
            this.layoutRoot.Controls.Add(this.layout, 0, 1);
            this.layout.Controls.Add(this.lblSoTienKhachTraD, 0, 0);
            this.layout.Controls.Add(this.txtSoTien, 1, 0);
            this.layout.Controls.Add(this.lblPhuongThuc, 0, 1);
            this.layout.Controls.Add(this.cboPhuongThuc, 1, 1);
            this.layout.Controls.Add(this.lblGhiChu, 0, 2);
            this.layout.Controls.Add(this.txtGhiChu, 1, 2);
            this.layout.Controls.Add(this.pnlButtons, 0, 3);
            this.layout.SetColumnSpan(this.pnlButtons, 2);
            this.pnlButtons.Controls.Add(this.btnOk);
            this.pnlButtons.Controls.Add(this.btnCancel);
            this.ResumeLayout(false);
            this.PerformLayout();
        }
    }
}
