// ------------------------------------------------------------------------------
//  GIAO DIỆN CỦA FORM ĐĂNG NHẬP (mở bằng Visual Studio Designer: Shift+F7).
//
//  ĐƠN VỊ Ở ĐÂY LÀ ĐƠN VỊ THIẾT KẾ 96 DPI (baseline) — giống như bạn vẽ trên màn hình
//  100%. Lúc chạy, LoginForm gọi Dpi.ScaleForm(this) để nhân đúng theo tỉ lệ màn hình
//  (100% / 125% / 150% / 200%). Vì vậy:
//    • Sửa VỊ TRÍ / KÍCH THƯỚC / CHỮ / MÀU ở file này thoải mái — lúc chạy vẫn đúng.
//    • KHÔNG đổi AutoScaleMode (phải là None) và không gọi AutoScaleDimensions,
//      nếu không sẽ bị nhân kích thước 2 lần trên màn hình tỉ lệ cao.
//    • Cỡ chữ khai báo bằng point (pt) nên tự đúng ở mọi DPI — không cần nhân.
// ------------------------------------------------------------------------------
namespace QuanLyNhaTroDesktop.Forms
{
    partial class LoginForm
    {
        private System.ComponentModel.IContainer components = null;

        private System.Windows.Forms.TableLayoutPanel layout;
        private System.Windows.Forms.Label lblTitle;
        private System.Windows.Forms.Label lblSubtitle;
        private System.Windows.Forms.Label lblSpacer;
        private System.Windows.Forms.Label lblUserName;
        private System.Windows.Forms.TextBox txtUserName;
        private System.Windows.Forms.Label lblPassword;
        private System.Windows.Forms.TextBox txtPassword;
        private System.Windows.Forms.Label lblError;
        private System.Windows.Forms.Label lblHint;
        private System.Windows.Forms.FlowLayoutPanel pnlButtons;
        private System.Windows.Forms.Button btnLogin;
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
            lblTitle = new Label();
            lblSubtitle = new Label();
            lblSpacer = new Label();
            lblUserName = new Label();
            txtUserName = new TextBox();
            lblPassword = new Label();
            txtPassword = new TextBox();
            lblError = new Label();
            lblHint = new Label();
            pnlButtons = new FlowLayoutPanel();
            btnLogin = new Button();
            btnCancel = new Button();
            layout.SuspendLayout();
            pnlButtons.SuspendLayout();
            SuspendLayout();
            // 
            // layout
            // 
            layout.AutoScroll = true;
            layout.ColumnCount = 2;
            layout.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 40F));
            layout.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 60F));
            layout.Controls.Add(lblTitle, 0, 0);
            layout.Controls.Add(lblSubtitle, 0, 1);
            layout.Controls.Add(lblSpacer, 0, 2);
            layout.Controls.Add(lblUserName, 0, 3);
            layout.Controls.Add(txtUserName, 1, 3);
            layout.Controls.Add(lblPassword, 0, 4);
            layout.Controls.Add(txtPassword, 1, 4);
            layout.Controls.Add(lblError, 0, 5);
            layout.Controls.Add(lblHint, 0, 6);
            layout.Controls.Add(pnlButtons, 0, 7);
            layout.Dock = DockStyle.Fill;
            layout.Location = new Point(0, 0);
            layout.Name = "layout";
            layout.Padding = new Padding(7, 6, 7, 6);
            layout.RowCount = 8;
            layout.RowStyles.Add(new RowStyle(SizeType.Absolute, 39F));
            layout.RowStyles.Add(new RowStyle(SizeType.Absolute, 26F));
            layout.RowStyles.Add(new RowStyle(SizeType.Absolute, 10F));
            layout.RowStyles.Add(new RowStyle(SizeType.Absolute, 24F));
            layout.RowStyles.Add(new RowStyle(SizeType.Absolute, 24F));
            layout.RowStyles.Add(new RowStyle(SizeType.Absolute, 44F));
            layout.RowStyles.Add(new RowStyle(SizeType.Absolute, 22F));
            layout.RowStyles.Add(new RowStyle(SizeType.Absolute, 52F));
            layout.Size = new Size(570, 284);
            layout.TabIndex = 0;
            // 
            // lblTitle
            // 
            layout.SetColumnSpan(lblTitle, 2);
            lblTitle.Dock = DockStyle.Fill;
            lblTitle.Font = new Font("Segoe UI", 17F, FontStyle.Bold);
            lblTitle.ForeColor = Color.FromArgb(37, 99, 235);
            lblTitle.Location = new Point(7, 6);
            lblTitle.Margin = new Padding(0);
            lblTitle.Name = "lblTitle";
            lblTitle.Size = new Size(556, 39);
            lblTitle.TabIndex = 0;
            lblTitle.Text = "🏠 QUẢN LÝ NHÀ TRỌ";
            lblTitle.TextAlign = ContentAlignment.MiddleCenter;
            // 
            // lblSubtitle
            // 
            layout.SetColumnSpan(lblSubtitle, 2);
            lblSubtitle.Dock = DockStyle.Fill;
            lblSubtitle.ForeColor = Color.FromArgb(107, 114, 128);
            lblSubtitle.Location = new Point(7, 45);
            lblSubtitle.Margin = new Padding(0);
            lblSubtitle.Name = "lblSubtitle";
            lblSubtitle.Size = new Size(556, 26);
            lblSubtitle.TabIndex = 1;
            lblSubtitle.Text = "Đăng nhập với tài khoản Chủ trọ";
            lblSubtitle.TextAlign = ContentAlignment.MiddleCenter;
            // 
            // lblSpacer
            // 
            lblSpacer.Dock = DockStyle.Fill;
            lblSpacer.Location = new Point(7, 71);
            lblSpacer.Margin = new Padding(0);
            lblSpacer.Name = "lblSpacer";
            lblSpacer.Size = new Size(222, 10);
            lblSpacer.TabIndex = 2;
            lblSpacer.TextAlign = ContentAlignment.MiddleLeft;
            // 
            // lblUserName
            // 
            lblUserName.Dock = DockStyle.Fill;
            lblUserName.Location = new Point(8, 81);
            lblUserName.Margin = new Padding(1, 0, 4, 0);
            lblUserName.Name = "lblUserName";
            lblUserName.Size = new Size(217, 24);
            lblUserName.TabIndex = 3;
            lblUserName.Text = "Tên đăng nhập:";
            lblUserName.TextAlign = ContentAlignment.MiddleLeft;
            // 
            // txtUserName
            // 
            txtUserName.Anchor = AnchorStyles.Left | AnchorStyles.Right;
            txtUserName.Location = new Point(230, 83);
            txtUserName.Margin = new Padding(1, 2, 1, 2);
            txtUserName.Name = "txtUserName";
            txtUserName.Size = new Size(332, 27);
            txtUserName.TabIndex = 4;
            // 
            // lblPassword
            // 
            lblPassword.Dock = DockStyle.Fill;
            lblPassword.Location = new Point(8, 105);
            lblPassword.Margin = new Padding(1, 0, 4, 0);
            lblPassword.Name = "lblPassword";
            lblPassword.Size = new Size(217, 24);
            lblPassword.TabIndex = 5;
            lblPassword.Text = "Mật khẩu:";
            lblPassword.TextAlign = ContentAlignment.MiddleLeft;
            // 
            // txtPassword
            // 
            txtPassword.Anchor = AnchorStyles.Left | AnchorStyles.Right;
            txtPassword.Location = new Point(230, 107);
            txtPassword.Margin = new Padding(1, 2, 1, 2);
            txtPassword.Name = "txtPassword";
            txtPassword.Size = new Size(332, 27);
            txtPassword.TabIndex = 6;
            txtPassword.UseSystemPasswordChar = true;
            // 
            // lblError
            // 
            layout.SetColumnSpan(lblError, 2);
            lblError.Dock = DockStyle.Fill;
            lblError.Font = new Font("Segoe UI", 10F, FontStyle.Bold);
            lblError.ForeColor = Color.FromArgb(220, 38, 38);
            lblError.Location = new Point(8, 131);
            lblError.Margin = new Padding(1, 2, 1, 2);
            lblError.Name = "lblError";
            lblError.Size = new Size(554, 40);
            lblError.TabIndex = 7;
            // 
            // lblHint
            // 
            lblHint.AutoSize = true;
            layout.SetColumnSpan(lblHint, 2);
            lblHint.Dock = DockStyle.Fill;
            lblHint.Font = new Font("Segoe UI", 9.5F);
            lblHint.ForeColor = Color.FromArgb(107, 114, 128);
            lblHint.Location = new Point(8, 175);
            lblHint.Margin = new Padding(1, 2, 1, 2);
            lblHint.Name = "lblHint";
            lblHint.Size = new Size(554, 18);
            lblHint.TabIndex = 8;
            lblHint.Text = "Tài khoản mẫu: chutro / Chutro@123";
            // 
            // pnlButtons
            // 
            pnlButtons.AutoSize = true;
            layout.SetColumnSpan(pnlButtons, 2);
            pnlButtons.Controls.Add(btnLogin);
            pnlButtons.Controls.Add(btnCancel);
            pnlButtons.Dock = DockStyle.Fill;
            pnlButtons.FlowDirection = FlowDirection.RightToLeft;
            pnlButtons.Location = new Point(8, 197);
            pnlButtons.Margin = new Padding(1, 2, 1, 2);
            pnlButtons.Name = "pnlButtons";
            pnlButtons.Size = new Size(554, 79);
            pnlButtons.TabIndex = 9;
            // 
            // btnLogin
            // 
            btnLogin.BackColor = Color.FromArgb(37, 99, 235);
            btnLogin.Cursor = Cursors.Hand;
            btnLogin.FlatAppearance.BorderSize = 0;
            btnLogin.FlatStyle = FlatStyle.Flat;
            btnLogin.ForeColor = Color.FromArgb(255, 255, 255);
            btnLogin.Location = new Point(424, 0);
            btnLogin.Margin = new Padding(8, 0, 0, 0);
            btnLogin.Name = "btnLogin";
            btnLogin.Size = new Size(130, 40);
            btnLogin.TabIndex = 0;
            btnLogin.Text = "Đăng nhập";
            btnLogin.UseVisualStyleBackColor = false;
            // 
            // btnCancel
            // 
            btnCancel.Cursor = Cursors.Hand;
            btnCancel.FlatStyle = FlatStyle.Flat;
            btnCancel.ForeColor = Color.FromArgb(31, 41, 55);
            btnCancel.Location = new Point(286, 0);
            btnCancel.Margin = new Padding(8, 0, 0, 0);
            btnCancel.Name = "btnCancel";
            btnCancel.Size = new Size(130, 40);
            btnCancel.TabIndex = 1;
            btnCancel.Text = "Thoát";
            btnCancel.UseVisualStyleBackColor = false;
            // 
            // LoginForm
            // 
            AutoScaleMode = AutoScaleMode.None;
            BackColor = Color.FromArgb(255, 255, 255);
            ClientSize = new Size(570, 284);
            Controls.Add(layout);
            Font = new Font("Segoe UI", 11F);
            FormBorderStyle = FormBorderStyle.FixedDialog;
            MaximizeBox = false;
            MinimizeBox = false;
            Name = "LoginForm";
            StartPosition = FormStartPosition.CenterScreen;
            Text = "Đăng nhập - Quản lý nhà trọ";
            layout.ResumeLayout(false);
            layout.PerformLayout();
            pnlButtons.ResumeLayout(false);
            ResumeLayout(false);
        }
    }
}
