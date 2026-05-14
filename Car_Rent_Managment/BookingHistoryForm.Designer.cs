using System.Drawing;
using System.Windows.Forms;

namespace Car_Rent_Managment
{
    partial class BookingHistoryForm
    {
        private System.ComponentModel.IContainer components = null;

        private Label lblTitle;
        private Label lblInfo;
        private DataGridView dgvBookingHistory;
        private Button btnRefresh;
        private Button btnClose;

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
            lblTitle = new Label();
            lblInfo = new Label();
            dgvBookingHistory = new DataGridView();
            btnRefresh = new Button();
            btnClose = new Button();

            ((System.ComponentModel.ISupportInitialize)dgvBookingHistory).BeginInit();
            SuspendLayout();

            lblTitle.AutoSize = true;
            lblTitle.Font = new Font("Segoe UI", 24F, FontStyle.Bold);
            lblTitle.Location = new Point(30, 25);
            lblTitle.Name = "lblTitle";
            lblTitle.Size = new Size(250, 45);
            lblTitle.TabIndex = 0;
            lblTitle.Text = "Booking History";

            lblInfo.AutoSize = true;
            lblInfo.Font = new Font("Segoe UI", 10F);
            lblInfo.ForeColor = Color.Gray;
            lblInfo.Location = new Point(35, 75);
            lblInfo.Name = "lblInfo";
            lblInfo.Size = new Size(35, 19);
            lblInfo.TabIndex = 1;
            lblInfo.Text = "User";

            btnRefresh.BackColor = Color.FromArgb(24, 90, 157);
            btnRefresh.FlatStyle = FlatStyle.Flat;
            btnRefresh.Font = new Font("Segoe UI", 10F, FontStyle.Bold);
            btnRefresh.ForeColor = Color.White;
            btnRefresh.Location = new Point(760, 60);
            btnRefresh.Name = "btnRefresh";
            btnRefresh.Size = new Size(120, 38);
            btnRefresh.TabIndex = 0;
            btnRefresh.Text = "Refresh";
            btnRefresh.UseVisualStyleBackColor = false;
            btnRefresh.Click += btnRefresh_Click;

            btnClose.BackColor = Color.Gray;
            btnClose.FlatStyle = FlatStyle.Flat;
            btnClose.Font = new Font("Segoe UI", 10F, FontStyle.Bold);
            btnClose.ForeColor = Color.White;
            btnClose.Location = new Point(900, 60);
            btnClose.Name = "btnClose";
            btnClose.Size = new Size(120, 38);
            btnClose.TabIndex = 1;
            btnClose.Text = "Close";
            btnClose.UseVisualStyleBackColor = false;
            btnClose.Click += btnClose_Click;

            dgvBookingHistory.AllowUserToAddRows = false;
            dgvBookingHistory.AllowUserToDeleteRows = false;
            dgvBookingHistory.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill;
            dgvBookingHistory.BackgroundColor = Color.White;
            dgvBookingHistory.BorderStyle = BorderStyle.FixedSingle;
            dgvBookingHistory.Location = new Point(30, 120);
            dgvBookingHistory.MultiSelect = false;
            dgvBookingHistory.Name = "dgvBookingHistory";
            dgvBookingHistory.ReadOnly = true;
            dgvBookingHistory.SelectionMode = DataGridViewSelectionMode.FullRowSelect;
            dgvBookingHistory.Size = new Size(1040, 470);
            dgvBookingHistory.TabIndex = 2;

            AutoScaleDimensions = new SizeF(7F, 15F);
            AutoScaleMode = AutoScaleMode.Font;
            BackColor = Color.FromArgb(245, 247, 250);
            ClientSize = new Size(1100, 620);
            Controls.Add(lblTitle);
            Controls.Add(lblInfo);
            Controls.Add(btnRefresh);
            Controls.Add(btnClose);
            Controls.Add(dgvBookingHistory);
            Name = "BookingHistoryForm";
            StartPosition = FormStartPosition.CenterScreen;
            Text = "Booking History";

            ((System.ComponentModel.ISupportInitialize)dgvBookingHistory).EndInit();
            ResumeLayout(false);
            PerformLayout();
        }
    }
}