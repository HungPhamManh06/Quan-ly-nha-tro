namespace ViDu32CheckBox;

partial class Form1
{
    private System.ComponentModel.IContainer components = null;

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
        components = new System.ComponentModel.Container();
        AutoScaleDimensions = new SizeF(7F, 15F);
        AutoScaleMode = AutoScaleMode.Font;
        ClientSize = new Size(320, 200);
        Controls.Add(groupBox1);
        Name = "Form1";
        Text = "Đăng ký";

        groupBox1.SuspendLayout();
        groupBox1.Controls.Add(chkLapTrinhCSharp);
        groupBox1.Controls.Add(chkLapTrinhWeb);
        groupBox1.Controls.Add(chkCCNA);
        groupBox1.Controls.Add(chkPhanTichThietKe);
        groupBox1.Controls.Add(chkHeQuanTriCSDL);
        groupBox1.Controls.Add(btnDangKy);
        groupBox1.ResumeLayout(false);
    }

    private GroupBox groupBox1 = new()
    {
        Text = "Các học phần",
        Location = new Point(12, 12),
        Size = new Size(296, 176),
    };

    private CheckBox chkLapTrinhCSharp = new()
    {
        Text = "Lập trình trực quan C#",
        Location = new Point(10, 25),
        Size = new Size(270, 24),
    };

    private CheckBox chkLapTrinhWeb = new()
    {
        Text = "Lập trình Web",
        Location = new Point(10, 52),
        Size = new Size(270, 24),
    };

    private CheckBox chkCCNA = new()
    {
        Text = "CCNA",
        Location = new Point(10, 79),
        Size = new Size(270, 24),
    };

    private CheckBox chkPhanTichThietKe = new()
    {
        Text = "Phân tích thiết kế hệ thống",
        Location = new Point(10, 106),
        Size = new Size(270, 24),
    };

    private CheckBox chkHeQuanTriCSDL = new()
    {
        Text = "Hệ quản trị cơ sở dữ liệu",
        Location = new Point(10, 133),
        Size = new Size(270, 24),
    };

    private Button btnDangKy = new()
    {
        Text = "Đăng ký",
        Location = new Point(195, 68),
        Size = new Size(90, 30),
    };
}
