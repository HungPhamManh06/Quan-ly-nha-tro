using QuanLyNhaTro.ViewModels;

namespace QuanLyNhaTroDesktop.Controls;

/// <summary>
/// Biểu đồ cột vẽ trực tiếp bằng GDI+ (không cần thư viện ngoài).
/// Dùng cho tab Tổng quan: doanh thu theo tháng, trạng thái phòng...
/// </summary>
public sealed class BarChart : Control
{
    private List<ChartItemViewModel> _items = new();

    public string Title { get; set; } = "";
    public Color BarColor { get; set; } = Color.FromArgb(37, 99, 235);
    public Color BarColor2 { get; set; } = Color.FromArgb(251, 146, 60);
    public bool ShowSecondSeries { get; set; }

    public BarChart()
    {
        SetStyle(ControlStyles.AllPaintingInWmPaint | ControlStyles.UserPaint | ControlStyles.OptimizedDoubleBuffer, true);
        BackColor = Color.White;
        DoubleBuffered = true;
    }

    public void SetData(List<ChartItemViewModel> items)
    {
        _items = items ?? new();
        Invalidate();
    }

    protected override void OnPaint(PaintEventArgs e)
    {
        base.OnPaint(e);
        var g = e.Graphics;
        g.SmoothingMode = System.Drawing.Drawing2D.SmoothingMode.AntiAlias;

        using var titleFont = new Font("Segoe UI", 11F, FontStyle.Bold);
        using var labelFont = new Font("Segoe UI", 8.5F);
        using var valueFont = new Font("Segoe UI", 8F);
        using var gridPen = new Pen(Color.FromArgb(229, 231, 235));
        using var axisBrush = new SolidBrush(Color.FromArgb(107, 114, 128));
        using var titleBrush = new SolidBrush(Color.FromArgb(31, 41, 55));

        var titleHeight = 0;
        if (!string.IsNullOrEmpty(Title))
        {
            g.DrawString(Title, titleFont, titleBrush, 4, 4);
            titleHeight = (int)titleFont.GetHeight(g) + 6;
        }

        if (_items.Count == 0)
        {
            g.DrawString("Chưa có dữ liệu", labelFont, axisBrush, 6, titleHeight + 10);
            return;
        }

        var labelHeight = (int)labelFont.GetHeight(g) + 6;
        var left = 6;
        var right = Math.Max(left + 10, Width - 6);
        var top = titleHeight + 14;                       // chừa chỗ ghi giá trị trên đỉnh cột
        var bottom = Math.Max(top + 10, Height - labelHeight - 4);

        var max = _items.Max(i => Math.Max(i.GiaTri1, ShowSecondSeries ? i.GiaTri2 : 0));
        if (max <= 0) max = 1;

        // 4 đường lưới ngang
        for (var i = 0; i <= 4; i++)
        {
            var y = bottom - (bottom - top) * i / 4;
            g.DrawLine(gridPen, left, y, right, y);
            var value = max * i / 4;
            var text = value >= 1_000_000 ? $"{value / 1_000_000:0.#}tr" : value.ToString("N0");
            g.DrawString(text, valueFont, axisBrush, left, y - valueFont.Height - 1);
        }

        var slot = (right - left) / (float)_items.Count;
        var barWidth = Math.Max(4f, Math.Min(slot * (ShowSecondSeries ? 0.32f : 0.5f), 26f));

        for (var i = 0; i < _items.Count; i++)
        {
            var item = _items[i];
            var centerX = left + slot * i + slot / 2f;

            if (ShowSecondSeries)
            {
                DrawBar(g, centerX - barWidth - 1, bottom, barWidth, top, item.GiaTri2, max, BarColor2, valueFont, false);
                DrawBar(g, centerX + 1, bottom, barWidth, top, item.GiaTri1, max, BarColor, valueFont, true);
            }
            else
            {
                DrawBar(g, centerX - barWidth / 2f, bottom, barWidth, top, item.GiaTri1, max, BarColor, valueFont, true);
            }

            var label = item.Label.Length > 7 ? item.Label[^5..] : item.Label;
            var size = g.MeasureString(label, labelFont);
            g.DrawString(label, labelFont, axisBrush, centerX - size.Width / 2f, bottom + 2);
        }
    }

    private static void DrawBar(Graphics g, float x, int bottom, float width, int top, decimal value,
        decimal max, Color color, Font valueFont, bool drawValue)
    {
        var height = (float)((double)value / (double)max * (bottom - top));
        if (height < 1) height = 1;
        var rect = new RectangleF(x, bottom - height, width, height);

        using var brush = new SolidBrush(color);
        g.FillRectangle(brush, rect);

        if (!drawValue || height < 12) return;
        using var textBrush = new SolidBrush(Color.FromArgb(55, 65, 81));
        var text = value >= 1_000_000 ? $"{value / 1_000_000:0.#}tr" : value.ToString("N0");
        var size = g.MeasureString(text, valueFont);
        g.DrawString(text, valueFont, textBrush,
            rect.X + rect.Width / 2f - size.Width / 2f, Math.Max(0, rect.Y - size.Height + 2));
    }
}
