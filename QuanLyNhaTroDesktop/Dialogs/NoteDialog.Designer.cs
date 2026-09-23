// ------------------------------------------------------------------------------
//  GIAO DIỆN CỦA NoteDialog (mở bằng Visual Studio Designer: nhấp đúp file hoặc Shift+F7).
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
    partial class NoteDialog
    {
        private System.ComponentModel.IContainer components = null;

        private System.Windows.Forms.TableLayoutPanel layout;
        private System.Windows.Forms.Label lblPrompt;
        private System.Windows.Forms.TextBox _text;
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
            this.ClientSize = new System.Drawing.Size(560, 224);
            this.Text = "Ghi chú";
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog;
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.ShowInTaskbar = false;
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
            this.WindowState = System.Windows.Forms.FormWindowState.Normal;
            this.BackColor = System.Drawing.Color.FromArgb(255, 255, 255, 255);
            this.Name = "NoteDialog";
            this.SuspendLayout();
            this.layout = new System.Windows.Forms.TableLayoutPanel();
            this.layout.Name = "layout";
            this.layout.Location = new System.Drawing.Point(0, 0);
            this.layout.Size = new System.Drawing.Size(560, 224);
            this.layout.Dock = System.Windows.Forms.DockStyle.Fill;
            this.layout.Margin = new System.Windows.Forms.Padding(3, 3, 3, 3);
            this.layout.Padding = new System.Windows.Forms.Padding(7, 6, 7, 6);
            this.layout.ColumnCount = 2;
            this.layout.RowCount = 3;
            this.layout.AutoScroll = true;
            this.layout.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 30F));
            this.layout.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 70F));
            this.layout.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 70F));
            this.layout.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 90F));
            this.layout.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 52F));
            this.lblPrompt = new System.Windows.Forms.Label();
            this.lblPrompt.Name = "lblPrompt";
            this.lblPrompt.Location = new System.Drawing.Point(8, 8);
            this.lblPrompt.Size = new System.Drawing.Size(544, 66);
            this.lblPrompt.Dock = System.Windows.Forms.DockStyle.Fill;
            this.lblPrompt.Margin = new System.Windows.Forms.Padding(1, 2, 1, 2);
            this.lblPrompt.AutoSize = false;
            this.lblPrompt.Text = "Nhập nội dung ghi chú:";
            this.lblPrompt.ForeColor = System.Drawing.Color.FromArgb(255, 31, 41, 55);
            this.lblPrompt.TextAlign = System.Drawing.ContentAlignment.TopLeft;
            this._text = new System.Windows.Forms.TextBox();
            this._text.Name = "_text";
            this._text.Location = new System.Drawing.Point(8, 78);
            this._text.Size = new System.Drawing.Size(544, 86);
            this._text.Dock = System.Windows.Forms.DockStyle.Fill;
            this._text.Margin = new System.Windows.Forms.Padding(1, 2, 1, 2);
            this._text.Multiline = true;
            this._text.ScrollBars = System.Windows.Forms.ScrollBars.Vertical;
            this.pnlButtons = new System.Windows.Forms.FlowLayoutPanel();
            this.pnlButtons.Name = "pnlButtons";
            this.pnlButtons.Location = new System.Drawing.Point(8, 168);
            this.pnlButtons.Size = new System.Drawing.Size(544, 48);
            this.pnlButtons.Dock = System.Windows.Forms.DockStyle.Fill;
            this.pnlButtons.Margin = new System.Windows.Forms.Padding(1, 2, 1, 2);
            this.pnlButtons.AutoSize = true;
            this.pnlButtons.FlowDirection = System.Windows.Forms.FlowDirection.RightToLeft;
            this.pnlButtons.WrapContents = true;
            this.btnOk = new System.Windows.Forms.Button();
            this.btnOk.Name = "btnOk";
            this.btnOk.Location = new System.Drawing.Point(414, 0);
            this.btnOk.Size = new System.Drawing.Size(130, 40);
            this.btnOk.Margin = new System.Windows.Forms.Padding(8, 0, 0, 0);
            this.btnOk.BackColor = System.Drawing.Color.FromArgb(255, 37, 99, 235);
            this.btnOk.Text = "Xác nhận";
            this.btnOk.ForeColor = System.Drawing.Color.FromArgb(255, 255, 255, 255);
            this.btnOk.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnOk.FlatAppearance.BorderSize = 0;
            this.btnOk.Cursor = System.Windows.Forms.Cursors.Hand;
            this.btnOk.UseVisualStyleBackColor = false;
            this.btnCancel = new System.Windows.Forms.Button();
            this.btnCancel.Name = "btnCancel";
            this.btnCancel.Location = new System.Drawing.Point(276, 0);
            this.btnCancel.Size = new System.Drawing.Size(130, 40);
            this.btnCancel.Margin = new System.Windows.Forms.Padding(8, 0, 0, 0);
            this.btnCancel.Text = "Hủy";
            this.btnCancel.ForeColor = System.Drawing.Color.FromArgb(255, 31, 41, 55);
            this.btnCancel.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnCancel.Cursor = System.Windows.Forms.Cursors.Hand;
            this.btnCancel.UseVisualStyleBackColor = false;
            this.Controls.Add(this.layout);
            this.layout.Controls.Add(this.lblPrompt, 0, 0);
            this.layout.SetColumnSpan(this.lblPrompt, 2);
            this.layout.Controls.Add(this._text, 0, 1);
            this.layout.SetColumnSpan(this._text, 2);
            this.layout.Controls.Add(this.pnlButtons, 0, 2);
            this.layout.SetColumnSpan(this.pnlButtons, 2);
            this.pnlButtons.Controls.Add(this.btnOk);
            this.pnlButtons.Controls.Add(this.btnCancel);
            this.ResumeLayout(false);
            this.PerformLayout();
        }
    }
}
