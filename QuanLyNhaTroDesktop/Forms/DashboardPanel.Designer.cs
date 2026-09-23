// ------------------------------------------------------------------------------
//  GIAO DIỆN CỦA DashboardPanel (mở bằng Visual Studio Designer: nhấp đúp file hoặc Shift+F7).
//
//  ĐƠN VỊ Ở ĐÂY LÀ ĐƠN VỊ THIẾT KẾ 96 DPI — lúc chạy, app tự nhân kích thước theo
//  tỉ lệ màn hình (100% / 125% / 150% / 200%) qua Dpi.ScaleForm. Vì vậy:
//    • Sửa VỊ TRÍ / KÍCH THƯỚC / CHỮ / MÀU thoải mái — lúc chạy vẫn đúng.
//    • KHÔNG đổi AutoScaleMode (phải là None), nếu không sẽ bị nhân kích thước 2 lần.
//    • Cỡ chữ khai bằng point (pt) nên tự đúng ở mọi DPI — không cần tự nhân.
//    • Thêm control mới: kéo từ Toolbox thả vào đây là được, app tự nhân DPI lúc chạy.
// ------------------------------------------------------------------------------
namespace QuanLyNhaTroDesktop.Forms
{
    partial class DashboardPanel
    {
        private System.ComponentModel.IContainer components = null;

        private System.Windows.Forms.TableLayoutPanel root;
        private System.Windows.Forms.FlowLayoutPanel header;
        private System.Windows.Forms.Button btnRefresh;
        private System.Windows.Forms.Label lblStatus;
        private System.Windows.Forms.FlowLayoutPanel cards;
        private System.Windows.Forms.TableLayoutPanel body;
        private System.Windows.Forms.GroupBox chartBox;
        private System.Windows.Forms.TableLayoutPanel charts;
        private QuanLyNhaTroDesktop.Controls.BarChart chart;
        private QuanLyNhaTroDesktop.Controls.BarChart roomChart;
        private System.Windows.Forms.GroupBox attentionBox;
        private System.Windows.Forms.DataGridView gridAttention;
        private System.Windows.Forms.DataGridViewTextBoxColumn colLoai;
        private System.Windows.Forms.DataGridViewTextBoxColumn colTieuDe;
        private System.Windows.Forms.DataGridViewTextBoxColumn colMoTa;
        private System.Windows.Forms.DataGridViewTextBoxColumn colSoTien;

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
            this.root = new System.Windows.Forms.TableLayoutPanel();
            this.header = new System.Windows.Forms.FlowLayoutPanel();
            this.btnRefresh = new System.Windows.Forms.Button();
            this.lblStatus = new System.Windows.Forms.Label();
            this.cards = new System.Windows.Forms.FlowLayoutPanel();
            this.body = new System.Windows.Forms.TableLayoutPanel();
            this.chartBox = new System.Windows.Forms.GroupBox();
            this.charts = new System.Windows.Forms.TableLayoutPanel();
            this.chart = new QuanLyNhaTroDesktop.Controls.BarChart();
            this.roomChart = new QuanLyNhaTroDesktop.Controls.BarChart();
            this.attentionBox = new System.Windows.Forms.GroupBox();
            this.gridAttention = new System.Windows.Forms.DataGridView();
            this.colLoai = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.colTieuDe = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.colMoTa = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.colSoTien = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.root.SuspendLayout();
            this.header.SuspendLayout();
            this.body.SuspendLayout();
            this.chartBox.SuspendLayout();
            this.charts.SuspendLayout();
            this.attentionBox.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.gridAttention)).BeginInit();
            this.SuspendLayout();
            //
            // btnRefresh
            //
            this.btnRefresh.BackColor = System.Drawing.Color.White;
            this.btnRefresh.Cursor = System.Windows.Forms.Cursors.Hand;
            this.btnRefresh.FlatAppearance.BorderColor = System.Drawing.Color.FromArgb(209, 213, 219);
            this.btnRefresh.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnRefresh.Font = new System.Drawing.Font("Segoe UI", 10F);
            this.btnRefresh.ForeColor = System.Drawing.Color.FromArgb(31, 41, 55);
            this.btnRefresh.Location = new System.Drawing.Point(0, 0);
            this.btnRefresh.Margin = new System.Windows.Forms.Padding(0, 0, 5, 0);
            this.btnRefresh.Name = "btnRefresh";
            this.btnRefresh.Size = new System.Drawing.Size(110, 36);
            this.btnRefresh.TabIndex = 0;
            this.btnRefresh.Text = "🔄 Làm mới";
            this.btnRefresh.UseVisualStyleBackColor = false;
            //
            // lblStatus
            //
            this.lblStatus.AutoSize = true;
            this.lblStatus.Font = new System.Drawing.Font("Segoe UI", 10F);
            this.lblStatus.ForeColor = System.Drawing.Color.FromArgb(107, 114, 128);
            this.lblStatus.Location = new System.Drawing.Point(120, 1);
            this.lblStatus.Margin = new System.Windows.Forms.Padding(5, 1, 0, 0);
            this.lblStatus.Name = "lblStatus";
            this.lblStatus.Size = new System.Drawing.Size(240, 17);
            this.lblStatus.TabIndex = 1;
            this.lblStatus.Text = "Cập nhật lúc --:--:--";
            //
            // header
            //
            this.header.BackColor = System.Drawing.Color.White;
            this.header.Controls.Add(this.btnRefresh);
            this.header.Controls.Add(this.lblStatus);
            this.header.Dock = System.Windows.Forms.DockStyle.Fill;
            this.header.Location = new System.Drawing.Point(0, 0);
            this.header.Margin = new System.Windows.Forms.Padding(0);
            this.header.Name = "header";
            this.header.Padding = new System.Windows.Forms.Padding(12, 6, 0, 0);
            this.header.Size = new System.Drawing.Size(1200, 46);
            this.header.TabIndex = 0;
            this.header.WrapContents = false;
            //
            // cards — dải thẻ số liệu (thẻ được thêm lúc chạy)
            //
            this.cards.AutoScroll = true;
            this.cards.BackColor = System.Drawing.Color.FromArgb(249, 250, 251);
            this.cards.Dock = System.Windows.Forms.DockStyle.Fill;
            this.cards.Location = new System.Drawing.Point(0, 46);
            this.cards.Margin = new System.Windows.Forms.Padding(0);
            this.cards.Name = "cards";
            this.cards.Padding = new System.Windows.Forms.Padding(10, 8, 10, 0);
            this.cards.Size = new System.Drawing.Size(1200, 196);
            this.cards.TabIndex = 1;
            this.cards.WrapContents = true;
            //
            // chart
            //
            this.chart.Dock = System.Windows.Forms.DockStyle.Fill;
            this.chart.Location = new System.Drawing.Point(3, 3);
            this.chart.Name = "chart";
            this.chart.ShowSecondSeries = true;
            this.chart.Size = new System.Drawing.Size(806, 230);
            this.chart.TabIndex = 0;
            //
            // roomChart
            //
            this.roomChart.Dock = System.Windows.Forms.DockStyle.Fill;
            this.roomChart.Location = new System.Drawing.Point(815, 3);
            this.roomChart.Name = "roomChart";
            this.roomChart.Size = new System.Drawing.Size(379, 230);
            this.roomChart.TabIndex = 1;
            this.roomChart.Title = "Trạng thái phòng";
            //
            // charts
            //
            this.charts.BackColor = System.Drawing.Color.White;
            this.charts.ColumnCount = 2;
            this.charts.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 68F));
            this.charts.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 32F));
            this.charts.Controls.Add(this.chart, 0, 0);
            this.charts.Controls.Add(this.roomChart, 1, 0);
            this.charts.Dock = System.Windows.Forms.DockStyle.Fill;
            this.charts.Location = new System.Drawing.Point(4, 20);
            this.charts.Name = "charts";
            this.charts.RowCount = 1;
            this.charts.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.charts.Size = new System.Drawing.Size(1192, 236);
            this.charts.TabIndex = 0;
            //
            // chartBox
            //
            this.chartBox.BackColor = System.Drawing.Color.White;
            this.chartBox.Controls.Add(this.charts);
            this.chartBox.Dock = System.Windows.Forms.DockStyle.Fill;
            this.chartBox.Font = new System.Drawing.Font("Segoe UI", 10.5F, System.Drawing.FontStyle.Bold);
            this.chartBox.ForeColor = System.Drawing.Color.FromArgb(31, 41, 55);
            this.chartBox.Location = new System.Drawing.Point(3, 3);
            this.chartBox.Name = "chartBox";
            this.chartBox.Padding = new System.Windows.Forms.Padding(8);
            this.chartBox.Size = new System.Drawing.Size(1200, 260);
            this.chartBox.TabIndex = 0;
            this.chartBox.TabStop = false;
            this.chartBox.Text = "Doanh thu (xanh) và công nợ (cam) theo tháng";
            //
            // colLoai
            //
            this.colLoai.FillWeight = 12F;
            this.colLoai.HeaderText = "Loại";
            this.colLoai.Name = "colLoai";
            //
            // colTieuDe
            //
            this.colTieuDe.FillWeight = 30F;
            this.colTieuDe.HeaderText = "Khoản mục";
            this.colTieuDe.Name = "colTieuDe";
            //
            // colMoTa
            //
            this.colMoTa.FillWeight = 40F;
            this.colMoTa.HeaderText = "Chi tiết";
            this.colMoTa.Name = "colMoTa";
            //
            // colSoTien
            //
            this.colSoTien.FillWeight = 18F;
            this.colSoTien.HeaderText = "Số tiền";
            this.colSoTien.Name = "colSoTien";
            //
            // gridAttention
            //
            this.gridAttention.AllowUserToAddRows = false;
            this.gridAttention.AllowUserToDeleteRows = false;
            this.gridAttention.AllowUserToResizeRows = false;
            this.gridAttention.AutoGenerateColumns = false;
            this.gridAttention.AutoSizeColumnsMode = System.Windows.Forms.DataGridViewAutoSizeColumnsMode.Fill;
            this.gridAttention.BackgroundColor = System.Drawing.Color.White;
            this.gridAttention.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.gridAttention.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.gridAttention.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.colLoai,
            this.colTieuDe,
            this.colMoTa,
            this.colSoTien});
            this.gridAttention.Dock = System.Windows.Forms.DockStyle.Fill;
            this.gridAttention.Font = new System.Drawing.Font("Segoe UI", 10F);
            this.gridAttention.Location = new System.Drawing.Point(4, 20);
            this.gridAttention.MultiSelect = false;
            this.gridAttention.Name = "gridAttention";
            this.gridAttention.ReadOnly = true;
            this.gridAttention.RowHeadersVisible = false;
            this.gridAttention.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
            this.gridAttention.Size = new System.Drawing.Size(1192, 164);
            this.gridAttention.TabIndex = 0;
            //
            // attentionBox
            //
            this.attentionBox.BackColor = System.Drawing.Color.White;
            this.attentionBox.Controls.Add(this.gridAttention);
            this.attentionBox.Dock = System.Windows.Forms.DockStyle.Fill;
            this.attentionBox.Font = new System.Drawing.Font("Segoe UI", 10.5F, System.Drawing.FontStyle.Bold);
            this.attentionBox.ForeColor = System.Drawing.Color.FromArgb(31, 41, 55);
            this.attentionBox.Location = new System.Drawing.Point(3, 269);
            this.attentionBox.Name = "attentionBox";
            this.attentionBox.Padding = new System.Windows.Forms.Padding(8);
            this.attentionBox.Size = new System.Drawing.Size(1200, 188);
            this.attentionBox.TabIndex = 1;
            this.attentionBox.TabStop = false;
            this.attentionBox.Text = "Cần xử lý";
            //
            // body
            //
            this.body.BackColor = System.Drawing.Color.FromArgb(249, 250, 251);
            this.body.ColumnCount = 1;
            this.body.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.body.Controls.Add(this.chartBox, 0, 0);
            this.body.Controls.Add(this.attentionBox, 0, 1);
            this.body.Dock = System.Windows.Forms.DockStyle.Fill;
            this.body.Location = new System.Drawing.Point(0, 242);
            this.body.Name = "body";
            this.body.RowCount = 2;
            this.body.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 58F));
            this.body.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 42F));
            this.body.Size = new System.Drawing.Size(1206, 460);
            this.body.TabIndex = 2;
            //
            // root
            //
            this.root.BackColor = System.Drawing.Color.FromArgb(249, 250, 251);
            this.root.ColumnCount = 1;
            this.root.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.root.Controls.Add(this.header, 0, 0);
            this.root.Controls.Add(this.cards, 0, 1);
            this.root.Controls.Add(this.body, 0, 2);
            this.root.Dock = System.Windows.Forms.DockStyle.Fill;
            this.root.Location = new System.Drawing.Point(0, 0);
            this.root.Name = "root";
            this.root.RowCount = 3;
            this.root.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 46F));
            this.root.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 196F));
            this.root.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.root.Size = new System.Drawing.Size(1206, 581);
            this.root.TabIndex = 0;
            //
            // DashboardPanel
            //
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.None;
            this.BackColor = System.Drawing.Color.FromArgb(249, 250, 251);
            this.Controls.Add(this.root);
            this.Font = new System.Drawing.Font("Segoe UI", 10F);
            this.Name = "DashboardPanel";
            this.Size = new System.Drawing.Size(1206, 581);
            this.root.ResumeLayout(false);
            this.header.ResumeLayout(false);
            this.header.PerformLayout();
            this.body.ResumeLayout(false);
            this.chartBox.ResumeLayout(false);
            this.charts.ResumeLayout(false);
            this.attentionBox.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.gridAttention)).EndInit();
            this.ResumeLayout(false);
        }
    }
}
