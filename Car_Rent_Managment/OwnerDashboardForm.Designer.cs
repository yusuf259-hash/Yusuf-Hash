using Car_Rent_Managment.UI;
using System.Drawing;
using System.Windows.Forms;

namespace Car_Rent_Managment
{
    partial class OwnerDashboardForm
    {
        private System.ComponentModel.IContainer components = null;

        private Panel sidebar = null!;
        private Label lblTitle = null!;
        private Label lblSubtitle = null!;
        private Label lblWelcome = null!;
        private Button btnManageCars = null!;
        private Button btnViewBookings = null!;
        private Button btnEarnings = null!;
        private Button btnInventory = null!;
        private Button btnMyOffers = null!;
        private Button btnLogout = null!;

        private Panel cardTotalCars = null!;
        private Panel cardAvailableCars = null!;
        private Panel cardRentedCars = null!;
        private Panel cardInactiveCars = null!;
        private Panel cardTotalBookings = null!;
        private Panel cardPaidEarnings = null!;
        private Panel cardPendingAmount = null!;
        private Panel cardActiveOffers = null!;

        private Label lblTotalCarsValue = null!;
        private Label lblAvailableCarsValue = null!;
        private Label lblRentedCarsValue = null!;
        private Label lblInactiveCarsValue = null!;
        private Label lblTotalBookingsValue = null!;
        private Label lblPaidEarningsValue = null!;
        private Label lblPendingAmountValue = null!;
        private Label lblActiveOffersValue = null!;

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
            sidebar = UiTheme.CreateSidebar("Car Rental", "Owner Panel");
            lblTitle = new Label();
            lblSubtitle = new Label();
            lblWelcome = new Label();

            btnManageCars = UiTheme.CreateSidebarButton("Manage My Cars", 175);
            btnViewBookings = UiTheme.CreateSidebarButton("Bookings for My Cars", 230);
            btnEarnings = UiTheme.CreateSidebarButton("Earnings", 285);
            btnInventory = UiTheme.CreateSidebarButton("Inventory Dashboard", 340);
            btnMyOffers = UiTheme.CreateSidebarButton("My Offers", 395);
            btnLogout = UiTheme.CreateSidebarButton("Logout", 540);

            cardTotalCars = new Panel();
            cardAvailableCars = new Panel();
            cardRentedCars = new Panel();
            cardInactiveCars = new Panel();
            cardTotalBookings = new Panel();
            cardPaidEarnings = new Panel();
            cardPendingAmount = new Panel();
            cardActiveOffers = new Panel();

            lblTotalCarsValue = new Label();
            lblAvailableCarsValue = new Label();
            lblRentedCarsValue = new Label();
            lblInactiveCarsValue = new Label();
            lblTotalBookingsValue = new Label();
            lblPaidEarningsValue = new Label();
            lblPendingAmountValue = new Label();
            lblActiveOffersValue = new Label();

            SuspendLayout();

            BackColor = UiTheme.Background;
            ClientSize = new Size(1240, 700);
            StartPosition = FormStartPosition.CenterScreen;
            Text = "Owner Dashboard";

            lblTitle.Text = "Owner Dashboard";
            lblTitle.Font = UiTheme.TitleFont();
            lblTitle.ForeColor = UiTheme.TextDark;
            lblTitle.AutoSize = true;
            lblTitle.Location = new Point(310, 45);

            lblSubtitle.Text = "Manage cars, monitor bookings, and track earnings.";
            lblSubtitle.Font = UiTheme.NormalFont();
            lblSubtitle.ForeColor = UiTheme.TextMuted;
            lblSubtitle.AutoSize = true;
            lblSubtitle.Location = new Point(315, 95);

            lblWelcome.Text = "Welcome";
            lblWelcome.Font = new Font("Segoe UI", 12F, FontStyle.Bold);
            lblWelcome.ForeColor = UiTheme.TextDark;
            lblWelcome.AutoSize = true;
            lblWelcome.Location = new Point(315, 125);

            UiTheme.ConfigureStatCard(cardTotalCars, lblTotalCarsValue, "Total Cars", "0", 310, 175, UiTheme.Primary);
            UiTheme.ConfigureStatCard(cardAvailableCars, lblAvailableCarsValue, "Available Cars", "0", 535, 175, UiTheme.Success);
            UiTheme.ConfigureStatCard(cardRentedCars, lblRentedCarsValue, "Rented Cars", "0", 760, 175, UiTheme.Warning);
            UiTheme.ConfigureStatCard(cardInactiveCars, lblInactiveCarsValue, "Inactive Cars", "0", 985, 175, UiTheme.Danger);
            UiTheme.ConfigureStatCard(cardTotalBookings, lblTotalBookingsValue, "Total Bookings", "0", 310, 305, UiTheme.Primary);
            UiTheme.ConfigureStatCard(cardPaidEarnings, lblPaidEarningsValue, "Paid Earnings", "0.00 BDT", 535, 305, UiTheme.Purple, 205, 100, 14F);
            UiTheme.ConfigureStatCard(cardPendingAmount, lblPendingAmountValue, "Pending Amount", "0.00 BDT", 760, 305, UiTheme.Warning, 205, 100, 14F);
            UiTheme.ConfigureStatCard(cardActiveOffers, lblActiveOffersValue, "Active Offers", "0", 985, 305, UiTheme.Success);

            btnManageCars.Click += btnManageCars_Click;
            btnViewBookings.Click += btnViewBookings_Click;
            btnEarnings.Click += btnEarnings_Click;
            btnInventory.Click += btnInventory_Click;
            btnMyOffers.Click += btnMyOffers_Click;
            btnLogout.Click += btnLogout_Click;

            UiTheme.StyleDangerButton(btnLogout);

            sidebar.Controls.Add(btnManageCars);
            sidebar.Controls.Add(btnViewBookings);
            sidebar.Controls.Add(btnEarnings);
            sidebar.Controls.Add(btnInventory);
            sidebar.Controls.Add(btnMyOffers);
            sidebar.Controls.Add(btnLogout);

            Controls.Add(sidebar);
            Controls.Add(lblTitle);
            Controls.Add(lblSubtitle);
            Controls.Add(lblWelcome);
            Controls.Add(cardTotalCars);
            Controls.Add(cardAvailableCars);
            Controls.Add(cardRentedCars);
            Controls.Add(cardInactiveCars);
            Controls.Add(cardTotalBookings);
            Controls.Add(cardPaidEarnings);
            Controls.Add(cardPendingAmount);
            Controls.Add(cardActiveOffers);

            ResumeLayout(false);
            PerformLayout();
        }
    }
}
