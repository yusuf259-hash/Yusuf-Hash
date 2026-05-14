using Car_Rent_Managment.UI;
using System.Drawing;
using System.Windows.Forms;

namespace Car_Rent_Managment
{
    partial class ReportsForm
    {
        private System.ComponentModel.IContainer components = null;

        private Label lblTitle = null!;
        private Label lblSubtitle = null!;
        private DataGridView dgvReports = null!;
        private Button btnRefresh = null!;
        private Button btnClose = null!;

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
            lblSubtitle = new Label();
            dgvReports = new DataGridView();
            btnRefresh = new Button();
            btnClose = new Button();

            ((System.ComponentModel.ISupportInitialize)dgvReports).BeginInit();
            SuspendLayout();

            BackColor = UiTheme.Background;
            ClientSize = new Size(920, 620);
            StartPosition = FormStartPosition.CenterScreen;
            Text = "Reports";

            lblTitle.Text = "Platform Reports";
            lblTitle.Font = UiTheme.TitleFont();
            lblTitle.ForeColor = UiTheme.TextDark;
            lblTitle.AutoSize = true;
            lblTitle.Location = new Point(30, 25);

            lblSubtitle.Text = "Quick totals for users, cars, bookings, revenue, and fines.";
            lblSubtitle.Font = UiTheme.NormalFont();
            lblSubtitle.ForeColor = UiTheme.TextMuted;
            lblSubtitle.AutoSize = true;
            lblSubtitle.Location = new Point(35, 75);

            btnRefresh.Text = "Refresh";
            btnRefresh.Location = new Point(650, 40);
            btnRefresh.Size = new Size(110, 38);
            UiTheme.StylePrimaryButton(btnRefresh);
            btnRefresh.Click += btnRefresh_Click;

            btnClose.Text = "Close";
            btnClose.Location = new Point(780, 40);
            btnClose.Size = new Size(100, 38);
            UiTheme.StyleSlateButton(btnClose);
            btnClose.Click += btnClose_Click;

            dgvReports.Location = new Point(30, 120);
            dgvReports.Size = new Size(850, 445);
            UiTheme.StyleGrid(dgvReports);

            Controls.Add(lblTitle);
            Controls.Add(lblSubtitle);
            Controls.Add(btnRefresh);
            Controls.Add(btnClose);
            Controls.Add(dgvReports);

            ((System.ComponentModel.ISupportInitialize)dgvReports).EndInit();
            ResumeLayout(false);
            PerformLayout();
        }
    }
}
