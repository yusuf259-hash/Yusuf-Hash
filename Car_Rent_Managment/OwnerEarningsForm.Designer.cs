using Car_Rent_Managment.UI;
using System.Drawing;
using System.Windows.Forms;

namespace Car_Rent_Managment
{
    partial class OwnerEarningsForm
    {
        private System.ComponentModel.IContainer components = null;

        private Label lblTitle = null!;
        private Label lblSubtitle = null!;
        private Label lblOwner = null!;
        private Button btnRefresh = null!;
        private Button btnClose = null!;
        private Panel cardTotalBookings = null!;
        private Panel cardPaidEarnings = null!;
        private Panel cardPendingAmount = null!;
        private Label lblTotalBookingsValue = null!;
        private Label lblPaidEarningsValue = null!;
        private Label lblPendingAmountValue = null!;
        private Label lblGridTitle = null!;
        private DataGridView dgvEarnings = null!;

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
            lblOwner = new Label();
            btnRefresh = new Button();
            btnClose = new Button();
            cardTotalBookings = new Panel();
            cardPaidEarnings = new Panel();
            cardPendingAmount = new Panel();
            lblTotalBookingsValue = new Label();
            lblPaidEarningsValue = new Label();
            lblPendingAmountValue = new Label();
            lblGridTitle = new Label();
            dgvEarnings = new DataGridView();

            ((System.ComponentModel.ISupportInitialize)dgvEarnings).BeginInit();
            SuspendLayout();

            BackColor = UiTheme.Background;
            ClientSize = new Size(1120, 660);
            StartPosition = FormStartPosition.CenterScreen;
            Text = "Owner Earnings";

            lblTitle.Text = "Earnings";
            lblTitle.Font = UiTheme.TitleFont();
            lblTitle.ForeColor = UiTheme.TextDark;
            lblTitle.AutoSize = true;
            lblTitle.Location = new Point(30, 25);

            lblSubtitle.Text = "Paid earnings and pending unpaid booking amounts for your cars.";
            lblSubtitle.Font = UiTheme.NormalFont();
            lblSubtitle.ForeColor = UiTheme.TextMuted;
            lblSubtitle.AutoSize = true;
            lblSubtitle.Location = new Point(35, 75);

            lblOwner.Text = "Owner:";
            lblOwner.Font = new Font("Segoe UI", 10F, FontStyle.Bold);
            lblOwner.ForeColor = UiTheme.TextDark;
            lblOwner.AutoSize = true;
            lblOwner.Location = new Point(35, 100);

            btnRefresh.Text = "Refresh";
            btnRefresh.Location = new Point(850, 40);
            btnRefresh.Size = new Size(110, 38);
            UiTheme.StylePrimaryButton(btnRefresh);
            btnRefresh.Click += btnRefresh_Click;

            btnClose.Text = "Close";
            btnClose.Location = new Point(980, 40);
            btnClose.Size = new Size(100, 38);
            UiTheme.StyleSlateButton(btnClose);
            btnClose.Click += btnClose_Click;

            UiTheme.ConfigureStatCard(cardTotalBookings, lblTotalBookingsValue, "Total Bookings", "0", 35, 145, UiTheme.Primary, 220, 105);
            UiTheme.ConfigureStatCard(cardPaidEarnings, lblPaidEarningsValue, "Total Paid Earnings", "0.00 BDT", 275, 145, UiTheme.Success, 250, 105, 14F);
            UiTheme.ConfigureStatCard(cardPendingAmount, lblPendingAmountValue, "Pending/Unpaid", "0.00 BDT", 555, 145, UiTheme.Warning, 250, 105, 14F);

            lblGridTitle.Text = "Earning Details";
            lblGridTitle.Font = UiTheme.HeaderFont();
            lblGridTitle.ForeColor = UiTheme.TextDark;
            lblGridTitle.AutoSize = true;
            lblGridTitle.Location = new Point(35, 285);

            dgvEarnings.Location = new Point(35, 325);
            dgvEarnings.Size = new Size(1045, 290);
            UiTheme.StyleGrid(dgvEarnings);

            Controls.Add(lblTitle);
            Controls.Add(lblSubtitle);
            Controls.Add(lblOwner);
            Controls.Add(btnRefresh);
            Controls.Add(btnClose);
            Controls.Add(cardTotalBookings);
            Controls.Add(cardPaidEarnings);
            Controls.Add(cardPendingAmount);
            Controls.Add(lblGridTitle);
            Controls.Add(dgvEarnings);

            ((System.ComponentModel.ISupportInitialize)dgvEarnings).EndInit();
            ResumeLayout(false);
            PerformLayout();
        }
    }
}
