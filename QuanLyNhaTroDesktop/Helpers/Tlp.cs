namespace QuanLyNhaTroDesktop.Helpers;

/// <summary>
/// Helper dựng form bằng TableLayoutPanel 2 cột (nhãn | ô nhập).
///
/// VÌ SAO KHÔNG DÙNG AutoScaleMode CỦA WINFORMS:
/// Trên máy đặt tỷ lệ hiển thị 125%/150%, AutoScaleDimensions mặc định (6,13) bị so với
/// kích thước thực tế (7,15) → WinForms nhân thêm ~1.17 lần, đẩy control ra ngoài form
/// (ô nhập biến mất, chữ bị cắt). Vì vậy mọi form đều đặt AutoScaleMode = None (xem
/// <see cref="Ui.InitForm"/>) và kích thước được tính theo FONT METRICS — font khai báo
/// bằng point nên tự đúng ở mọi DPI (12pt ở 125% cao hơn 12pt ở 100%).
/// </summary>
public static class Tlp
{
    // ================== Đo kích thước theo font ==================

    /// <summary>Chiều cao 1 dòng chữ (có tính dấu tiếng Việt như ậ, ọ, ị).</summary>
    public static int LineHeight(Font f) => TextRenderer.MeasureText("Mgjị", f).Height + 2;

    /// <summary>Chiều cao 1 hàng nhãn | ô nhập (đủ chỗ cho chữ và viền ô).</summary>
    public static int RowHeight(Font f) => LineHeight(f) + 14;

    /// <summary>Chiều cao nút bấm.</summary>
    public static int ButtonHeight(Font f) => LineHeight(f) + 18;

    /// <summary>Bề rộng cần cho 1 đoạn text (không bao giờ bị cắt chữ).</summary>
    public static int TextWidth(string text, Font f) => TextRenderer.MeasureText(text, f).Width + 6;

    // ================== Tạo bảng ==================

    /// <summary>Tạo bảng 2 cột (nhãn | ô nhập) lấp đầy form, tự cuộn khi nội dung dài.</summary>
    public static TableLayoutPanel Create(int labelColPercent = 38)
    {
        var t = new TableLayoutPanel
        {
            Dock = DockStyle.Fill,
            AutoScroll = true,              // nội dung cao hơn form → cuộn, KHÔNG mất control
            BackColor = Color.White,
            Padding = new Padding(14, 12, 14, 12),
            RowCount = 0,
            ColumnCount = 2,
        };
        t.ColumnStyles.Clear();
        t.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, labelColPercent));
        t.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 100 - labelColPercent));
        return t;
    }

    /// <summary>
    /// Thêm 1 hàng mới và gán RowStyle ĐÚNG chỉ số hàng (đây là lỗi làm các hàng bị cao 0
    /// và control biến mất ở phiên bản trước — RowCount và RowStyles bị lệch nhau).
    /// </summary>
    private static int NextRow(TableLayoutPanel t, RowStyle style)
    {
        t.RowCount += 1;
        while (t.RowStyles.Count > t.RowCount) t.RowStyles.RemoveAt(t.RowStyles.Count - 1);
        while (t.RowStyles.Count < t.RowCount) t.RowStyles.Add(new RowStyle(SizeType.AutoSize));
        t.RowStyles[t.RowCount - 1] = style;
        return t.RowCount - 1;
    }

    /// <summary>Thêm 1 dòng "nhãn | ô nhập".</summary>
    public static void AddInputRow(TableLayoutPanel t, string labelText, Control input)
    {
        t.SuspendLayout();

        var rowHeight = Math.Max(RowHeight(t.Font), RowHeight(input.Font));
        var row = NextRow(t, new RowStyle(SizeType.Absolute, rowHeight));

        var lbl = new Label
        {
            Text = labelText,
            AutoSize = false,
            Dock = DockStyle.Fill,
            TextAlign = ContentAlignment.MiddleLeft,
            Margin = new Padding(2, 0, 8, 0),
        };

        // Anchor Left|Right (không kéo giãn chiều cao) → ô nhập tự căn giữa theo hàng,
        // giữ đúng chiều cao gốc của TextBox/ComboBox/DateTimePicker.
        input.Anchor = AnchorStyles.Left | AnchorStyles.Right;
        input.Margin = new Padding(2, 4, 2, 4);

        t.Controls.Add(lbl, 0, row);
        t.Controls.Add(input, 1, row);
        t.ResumeLayout();
    }

    /// <summary>
    /// Thêm 1 dòng trải rộng cả 2 cột (tiêu đề, thông báo, bảng nút...).
    /// autoHeight = true → hàng cao theo nội dung; false → hàng cao cố định đủ chứa nút bấm.
    /// </summary>
    public static void AddSpanRow(TableLayoutPanel t, Control c, bool autoHeight = true, int heightIfFixed = 56)
    {
        t.SuspendLayout();

        int height;
        if (autoHeight)
        {
            height = Math.Max(c.PreferredSize.Height + 6, LineHeight(c.Font) + 6);
        }
        else
        {
            // baseline của người thiết kế (56 = chiều cao dòng nút) nhưng không nhỏ hơn nút thật
            height = Math.Max(Dpi.S(heightIfFixed), ButtonHeight(t.Font) + Dpi.S(10));
        }

        var row = NextRow(t, new RowStyle(SizeType.Absolute, height));
        c.Dock = DockStyle.Fill;
        c.Margin = new Padding(2, 3, 2, 3);
        t.SetColumnSpan(c, 2);
        t.Controls.Add(c, 0, row);
        t.ResumeLayout();
    }

    /// <summary>Thêm 1 dòng tiêu đề / ghi chú canh giữa (trải cả 2 cột).</summary>
    public static Label AddCentered(TableLayoutPanel t, string text, Font font, Color color, int extraPad = 10)
    {
        var lbl = new Label
        {
            Text = text,
            Font = font,
            ForeColor = color,
            AutoSize = false,
            TextAlign = ContentAlignment.MiddleCenter,
            Dock = DockStyle.Fill,
            Margin = new Padding(0),
        };
        var lines = text.Count(ch => ch == '\n') + 1;
        var row = NextRow(t, new RowStyle(SizeType.Absolute, LineHeight(font) * lines + extraPad));
        t.SetColumnSpan(lbl, 2);
        t.Controls.Add(lbl, 0, row);
        return lbl;
    }

    /// <summary>
    /// Thêm dòng nhãn nhiều dòng với CHIỀU CAO CỐ ĐỊNH theo số dòng.
    /// Dùng cho các nhãn được điền nội dung LÚC CHẠY — nếu để AutoSize thì hàng được tính
    /// khi nhãn còn rỗng nên nội dung thật sẽ bị cắt mất.
    /// </summary>
    public static Label AddLines(TableLayoutPanel t, Font font, int lines, Color? color = null)
    {
        var lbl = new Label
        {
            Text = "",
            Font = font,
            ForeColor = color ?? Color.FromArgb(31, 41, 55),
            AutoSize = false,
            Dock = DockStyle.Fill,
            TextAlign = ContentAlignment.TopLeft,
            Margin = new Padding(2, 3, 2, 3),
        };
        var row = NextRow(t, new RowStyle(SizeType.Absolute, LineHeight(font) * lines + 10));
        t.SetColumnSpan(lbl, 2);
        t.Controls.Add(lbl, 0, row);
        return lbl;
    }

    /// <summary>Thêm 1 dòng trống (khoảng cách).</summary>
    public static void AddSpacer(TableLayoutPanel t, int pixels = 8)
    {
        var row = NextRow(t, new RowStyle(SizeType.Absolute, Dpi.S(pixels)));
        t.Controls.Add(new Label { Text = "", AutoSize = false, Dock = DockStyle.Fill, Margin = new Padding(0) }, 0, row);
    }

    /// <summary>Thêm 1 dòng chứa nhãn ghi chú (nhiều dòng, tự xuống hàng).</summary>
    public static Label AddNote(TableLayoutPanel t, Font font, Color color, int wrapWidth = 460)
    {
        var lbl = new Label
        {
            AutoSize = true,
            Font = font,
            ForeColor = color,
            MaximumSize = new Size(Dpi.S(wrapWidth), 0),
        };
        AddSpanRow(t, lbl);
        return lbl;
    }

    // ================== Co form vừa đúng nội dung ==================

    /// <summary>
    /// Đặt lại CHIỀU CAO form vừa đúng tổng chiều cao các hàng của bảng — tránh form bị
    /// thừa một khoảng trắng lớn ở đáy (WinForms kéo giãn hàng cuối để lấp chỗ trống).
    /// Nếu nội dung cao hơn màn hình thì giới hạn lại và cuộn (AutoScroll của bảng).
    /// </summary>
    public static void FitHeight(Form f, TableLayoutPanel t, int extra = 0, double maxScreenRatio = 0.94)
    {
        var h = t.Padding.Vertical + Dpi.S(extra);
        for (var i = 0; i < t.RowCount; i++)
            if (i < t.RowStyles.Count) h += (int)Math.Ceiling(t.RowStyles[i].Height);

        var max = (int)(Screen.FromControl(f).WorkingArea.Height * maxScreenRatio);
        var capped = h > max;
        if (capped) h = max;
        if (h < Dpi.S(140)) return;

        f.ClientSize = new Size(f.ClientSize.Width, h);
    }

    // ================== Nút bấm ==================

    /// <summary>
    /// Tạo dòng nút: nút truyền TRƯỚC nằm bên PHẢI (nút chính). Bề rộng nút được đo theo
    /// độ dài chữ nên không bao giờ bị cắt ("Đăng nhập" chứ không phải "Đăng").
    /// </summary>
    public static FlowLayoutPanel ButtonRow(params Button[] buttons)
    {
        var panel = new FlowLayoutPanel
        {
            FlowDirection = FlowDirection.RightToLeft,
            AutoSize = true,
            AutoSizeMode = AutoSizeMode.GrowAndShrink,
            WrapContents = false,
            Margin = new Padding(0),
        };

        foreach (var b in buttons)
        {
            b.AutoSize = false;
            var w = Math.Max(TextWidth(b.Text, b.Font) + Dpi.S(40), Dpi.S(130));
            b.Size = new Size(w, Math.Max(ButtonHeight(b.Font), Dpi.S(40)));
            b.Margin = new Padding(Dpi.S(8), 0, 0, 0);
            panel.Controls.Add(b);
        }
        return panel;
    }

    /// <summary>Dòng nút nằm bên TRÁI (nút phụ trợ như "Xem trước", "Xuất Excel"...) + các nút phải.</summary>
    public static TableLayoutPanel TwoSidedButtonRow(Button[] leftButtons, Button[] rightButtons)
    {
        var left = new FlowLayoutPanel
        {
            FlowDirection = FlowDirection.LeftToRight,
            AutoSize = true,
            AutoSizeMode = AutoSizeMode.GrowAndShrink,
            WrapContents = false,
            Margin = new Padding(0),
        };
        foreach (var b in leftButtons)
        {
            b.AutoSize = false;
            b.Size = new Size(Math.Max(TextWidth(b.Text, b.Font) + Dpi.S(40), Dpi.S(130)), Math.Max(ButtonHeight(b.Font), Dpi.S(40)));
            b.Margin = new Padding(0, 0, Dpi.S(8), 0);
            left.Controls.Add(b);
        }

        var right = ButtonRow(rightButtons);
        right.Dock = DockStyle.Right;

        var wrapper = new TableLayoutPanel { Dock = DockStyle.Fill, ColumnCount = 2, RowCount = 1, Margin = new Padding(0) };
        wrapper.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 50));
        wrapper.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 50));
        wrapper.Controls.Add(left, 0, 0);
        wrapper.Controls.Add(right, 1, 0);
        wrapper.RowStyles.Add(new RowStyle(SizeType.Absolute, Math.Max(left.PreferredSize.Height, right.PreferredSize.Height)));
        return wrapper;
    }
}
