using Car_Rent_Managment.UI;
using System.Drawing;
using System.Windows.Forms;

namespace Car_Rent_Managment
{
    partial class OwnerInventoryDashboardForm
    {
        private System.ComponentModel.IContainer components = null;

        private Label lblTitle = null!;
        private Label lblSubtitle = null!;
        private Label lblOwner = null!;
        private Button btnRefresh = null!;
        private Button btnClose = null!;
        private Panel cardTotalCars = null!;
        private Panel cardAvailableCars = null!;
        private Panel cardRentedCars = null!;
        private Panel cardInactiveCars = null!;
        private Panel cardTotalBookings = null!;
        private Label lblTotalCarsValue = null!;
        private Label lblAvailableCarsValue = null!;
        private Label lblRentedCarsValue = null!;
        private Label lblInactiveCarsValue = null!;
        private Label lblTotalBookingsValue = null!;
        private Label lblCarsTitle = null!;
        private Label lblRecentBookingsTitle = null!;
        private DataGridView dgvCars = null!;
        private DataGridView dgvRecentBookings = null!;

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
            cardTotalCars = new Panel();
            cardAvailableCars = new Panel();
            cardRentedCars = new Panel();
            cardInactiveCars = new Panel();
            cardTotalBookings = new Panel();
            lblTotalCarsValue = new Label();
            lblAvailableCarsValue = new Label();
            lblRentedCarsValue = new Label();
            lblInactiveCarsValue = new Label();
            lblTotalBookingsValue = new Label();
            lblCarsTitle = new Label();
            lblRecentBookingsTitle = new Label();
            dgvCars = new DataGridView();
            dgvRecentBookings = new DataGridView();

            ((System.ComponentModel.ISupportInitialize)dgvCars).BeginInit();
            ((System.ComponentModel.ISupportInitialize)dgvRecentBookings).BeginInit();
            SuspendLayout();

            BackColor = UiTheme.Background;
            ClientSize = new Size(1220, 720);
            StartPosition = FormStartPosition.CenterScreen;
            Text = "Owner Inventory Dashboard";

            lblTitle.Text = "Inventory Dashboard";
            lblTitle.Font = UiTheme.TitleFont();
            lblTitle.ForeColor = UiTheme.TextDark;
            lblTitle.AutoSize = true;
            lblTitle.Location = new Point(30, 25);

            lblSubtitle.Text = "Car stock summary, status counts, and recent bookings for your cars.";
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
            btnRefresh.Location = new Point(950, 40);
            btnRefresh.Size = new Size(110, 38);
            UiTheme.StylePrimaryButton(btnRefresh);
            btnRefresh.Click += btnRefresh_Click;

            btnClose.Text = "Close";
            btnClose.Location = new Point(1080, 40);
            btnClose.Size = new Size(100, 38);
            UiTheme.StyleSlateButton(btnClose);
            btnClose.Click += btnClose_Click;

            UiTheme.ConfigureStatCard(cardTotalCars, lblTotalCarsValue, "Total Cars", "0", 35, 145, UiTheme.Primary, 205, 105);
            UiTheme.ConfigureStatCard(cardAvailableCars, lblAvailableCarsValue, "Available", "0", 265, 145, UiTheme.Success, 205, 105);
            UiTheme.ConfigureStatCard(cardRentedCars, lblRentedCarsValue, "Rented", "0", 495, 145, UiTheme.Warning, 205, 105);
            UiTheme.ConfigureStatCard(cardInactiveCars, lblInactiveCarsValue, "Inactive", "0", 725, 145, UiTheme.Danger, 205, 105);
            UiTheme.ConfigureStatCard(cardTotalBookings, lblTotalBookingsValue, "Total Bookings", "0", 955, 145, UiTheme.Purple, 205, 105);

            lblCarsTitle.Text = "Car Inventory";
            lblCarsTitle.Font = UiTheme.HeaderFont();
            lblCarsTitle.ForeColor = UiTheme.TextDark;
            lblCarsTitle.AutoSize = true;
            lblCarsTitle.Location = new Point(35, 285);

            dgvCars.Location = new Point(35, 325);
            dgvCars.Size = new Size(545, 330);
            UiTheme.StyleGrid(dgvCars);

            lblRecentBookingsTitle.Text = "Recent Bookings";
            lblRecentBookingsTitle.Font = UiTheme.HeaderFont();
            lblRecentBookingsTitle.ForeColor = UiTheme.TextDark;
            lblRecentBookingsTitle.AutoSize = true;
            lblRecentBookingsTitle.Location = new Point(610, 285);

            dgvRecentBookings.Location = new Point(610, 325);
            dgvRecentBookings.Size = new Size(575, 330);
            UiTheme.StyleGrid(dgvRecentBookings);

            Controls.Add(lblTitle);
            Controls.Add(lblSubtitle);
            Controls.Add(lblOwner);
            Controls.Add(btnRefresh);
            Controls.Add(btnClose);
            Controls.Add(cardTotalCars);
            Controls.Add(cardAvailableCars);
            Controls.Add(cardRentedCars);
            Controls.Add(cardInactiveCars);
            Controls.Add(cardTotalBookings);
            Controls.Add(lblCarsTitle);
            Controls.Add(dgvCars);
            Controls.Add(lblRecentBookingsTitle);
            Controls.Add(dgvRecentBookings);

            ((System.ComponentModel.ISupportInitialize)dgvCars).EndInit();
            ((System.ComponentModel.ISupportInitialize)dgvRecentBookings).EndInit();
            ResumeLayout(false);
            PerformLayout();
        }
    }
}
