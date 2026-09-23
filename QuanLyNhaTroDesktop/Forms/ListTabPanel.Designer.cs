// ------------------------------------------------------------------------------
//  GIAO DIỆN CỦA ListTabPanel (mở bằng Visual Studio Designer: nhấp đúp file hoặc Shift+F7).
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
    partial class ListTabPanel
    {
        private System.ComponentModel.IContainer components = null;

        private System.Windows.Forms.ListView list;
        private System.Windows.Forms.FlowLayoutPanel bar;
        private System.Windows.Forms.Button btnAdd;
        private System.Windows.Forms.Button btnEdit;
        private System.Windows.Forms.Button btnDelete;
        private System.Windows.Forms.Button btnDetail;
        private System.Windows.Forms.Button btnRefresh;

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
            this.list = new System.Windows.Forms.ListView();
            this.bar = new System.Windows.Forms.FlowLayoutPanel();
            this.btnAdd = new System.Windows.Forms.Button();
            this.btnEdit = new System.Windows.Forms.Button();
            this.btnDelete = new System.Windows.Forms.Button();
            this.btnDetail = new System.Windows.Forms.Button();
            this.btnRefresh = new System.Windows.Forms.Button();
            this.bar.SuspendLayout();
            this.SuspendLayout();
            //
            // list — bảng dữ liệu của tab
            //
            this.list.Dock = System.Windows.Forms.DockStyle.Fill;
            this.list.Font = new System.Drawing.Font("Segoe UI", 10F);
            this.list.FullRowSelect = true;
            this.list.GridLines = true;
            this.list.HeaderStyle = System.Windows.Forms.ColumnHeaderStyle.Nonclickable;
            this.list.HideSelection = false;
            this.list.Location = new System.Drawing.Point(0, 0);
            this.list.MultiSelect = false;
            this.list.Name = "list";
            this.list.Size = new System.Drawing.Size(900, 464);
            this.list.TabIndex = 0;
            this.list.UseCompatibleStateImageBehavior = false;
            this.list.View = System.Windows.Forms.View.Details;
            //
            // bar — thanh nút bên dưới bảng
            //
            this.bar.BackColor = System.Drawing.Color.White;
            this.bar.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.bar.FlowDirection = System.Windows.Forms.FlowDirection.LeftToRight;
            this.bar.Location = new System.Drawing.Point(0, 464);
            this.bar.Name = "bar";
            this.bar.Padding = new System.Windows.Forms.Padding(0, 6, 0, 0);
            this.bar.Size = new System.Drawing.Size(900, 52);
            this.bar.TabIndex = 1;
            this.bar.WrapContents = false;
            //
            // btnAdd
            //
            this.btnAdd.BackColor = System.Drawing.Color.FromArgb(37, 99, 235);
            this.btnAdd.Cursor = System.Windows.Forms.Cursors.Hand;
            this.btnAdd.FlatAppearance.BorderSize = 0;
            this.btnAdd.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnAdd.Font = new System.Drawing.Font("Segoe UI", 10F);
            this.btnAdd.ForeColor = System.Drawing.Color.White;
            this.btnAdd.Location = new System.Drawing.Point(0, 6);
            this.btnAdd.Margin = new System.Windows.Forms.Padding(0, 0, 8, 0);
            this.btnAdd.Name = "btnAdd";
            this.btnAdd.Size = new System.Drawing.Size(110, 36);
            this.btnAdd.TabIndex = 0;
            this.btnAdd.Text = "➕ Thêm";
            this.btnAdd.UseVisualStyleBackColor = false;
            //
            // btnEdit
            //
            this.btnEdit.BackColor = System.Drawing.Color.White;
            this.btnEdit.Cursor = System.Windows.Forms.Cursors.Hand;
            this.btnEdit.FlatAppearance.BorderColor = System.Drawing.Color.FromArgb(209, 213, 219);
            this.btnEdit.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnEdit.Font = new System.Drawing.Font("Segoe UI", 10F);
            this.btnEdit.ForeColor = System.Drawing.Color.FromArgb(31, 41, 55);
            this.btnEdit.Location = new System.Drawing.Point(118, 6);
            this.btnEdit.Margin = new System.Windows.Forms.Padding(0, 0, 8, 0);
            this.btnEdit.Name = "btnEdit";
            this.btnEdit.Size = new System.Drawing.Size(110, 36);
            this.btnEdit.TabIndex = 1;
            this.btnEdit.Text = "✏ Sửa";
            this.btnEdit.UseVisualStyleBackColor = false;
            //
            // btnDelete
            //
            this.btnDelete.BackColor = System.Drawing.Color.White;
            this.btnDelete.Cursor = System.Windows.Forms.Cursors.Hand;
            this.btnDelete.FlatAppearance.BorderColor = System.Drawing.Color.FromArgb(209, 213, 219);
            this.btnDelete.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnDelete.Font = new System.Drawing.Font("Segoe UI", 10F);
            this.btnDelete.ForeColor = System.Drawing.Color.FromArgb(220, 38, 38);
            this.btnDelete.Location = new System.Drawing.Point(236, 6);
            this.btnDelete.Margin = new System.Windows.Forms.Padding(0, 0, 8, 0);
            this.btnDelete.Name = "btnDelete";
            this.btnDelete.Size = new System.Drawing.Size(110, 36);
            this.btnDelete.TabIndex = 2;
            this.btnDelete.Text = "🗑 Xóa";
            this.btnDelete.UseVisualStyleBackColor = false;
            //
            // btnDetail
            //
            this.btnDetail.BackColor = System.Drawing.Color.White;
            this.btnDetail.Cursor = System.Windows.Forms.Cursors.Hand;
            this.btnDetail.FlatAppearance.BorderColor = System.Drawing.Color.FromArgb(209, 213, 219);
            this.btnDetail.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnDetail.Font = new System.Drawing.Font("Segoe UI", 10F);
            this.btnDetail.ForeColor = System.Drawing.Color.FromArgb(31, 41, 55);
            this.btnDetail.Location = new System.Drawing.Point(354, 6);
            this.btnDetail.Margin = new System.Windows.Forms.Padding(0, 0, 8, 0);
            this.btnDetail.Name = "btnDetail";
            this.btnDetail.Size = new System.Drawing.Size(120, 36);
            this.btnDetail.TabIndex = 3;
            this.btnDetail.Text = "🔍 Chi tiết";
            this.btnDetail.UseVisualStyleBackColor = false;
            //
            // btnRefresh
            //
            this.btnRefresh.BackColor = System.Drawing.Color.White;
            this.btnRefresh.Cursor = System.Windows.Forms.Cursors.Hand;
            this.btnRefresh.FlatAppearance.BorderColor = System.Drawing.Color.FromArgb(209, 213, 219);
            this.btnRefresh.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnRefresh.Font = new System.Drawing.Font("Segoe UI", 10F);
            this.btnRefresh.ForeColor = System.Drawing.Color.FromArgb(31, 41, 55);
            this.btnRefresh.Location = new System.Drawing.Point(482, 6);
            this.btnRefresh.Margin = new System.Windows.Forms.Padding(0, 0, 8, 0);
            this.btnRefresh.Name = "btnRefresh";
            this.btnRefresh.Size = new System.Drawing.Size(130, 36);
            this.btnRefresh.TabIndex = 4;
            this.btnRefresh.Text = "🔄 Làm mới";
            this.btnRefresh.UseVisualStyleBackColor = false;
            //
            // ListTabPanel
            //
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.None;
            this.BackColor = System.Drawing.Color.White;
            this.Controls.Add(this.list);
            this.Controls.Add(this.bar);
            this.bar.Controls.Add(this.btnAdd);
            this.bar.Controls.Add(this.btnEdit);
            this.bar.Controls.Add(this.btnDelete);
            this.bar.Controls.Add(this.btnDetail);
            this.bar.Controls.Add(this.btnRefresh);
            this.Font = new System.Drawing.Font("Segoe UI", 10F);
            this.Name = "ListTabPanel";
            this.Size = new System.Drawing.Size(900, 516);
            this.bar.ResumeLayout(false);
            this.ResumeLayout(false);
        }
    }
}
