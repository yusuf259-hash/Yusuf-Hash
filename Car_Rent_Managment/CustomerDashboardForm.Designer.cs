using Car_Rent_Managment.UI;
using System.Drawing;
using System.Windows.Forms;

namespace Car_Rent_Managment
{
    partial class CustomerDashboardForm
    {
        private System.ComponentModel.IContainer components = null;

        private Panel sidebar;
        private Label lblWelcome;
        private Label lblTitle;
        private Label lblSubtitle;

        private Button btnRentCar;
        private Button btnPaymentReturn;
        private Button btnReviews;
        private Button btnBookingHistory;
        private Button btnOffers;
        private Button btnLogout;

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
            sidebar = UiTheme.CreateSidebar("Car Rental", "Customer Panel");

            lblTitle = new Label();
            lblSubtitle = new Label();
            lblWelcome = new Label();

            btnRentCar = UiTheme.CreateSidebarButton("Rent a Car", 180);
            btnPaymentReturn = UiTheme.CreateSidebarButton("Payment & Return", 235);
            btnReviews = UiTheme.CreateSidebarButton("Reviews", 290);
            btnBookingHistory = UiTheme.CreateSidebarButton("Booking History", 345);
            btnOffers = UiTheme.CreateSidebarButton("Special Offers", 400);
            btnLogout = UiTheme.CreateSidebarButton("Logout", 540);

            Panel card1 = UiTheme.CreateStatCard("Step 1", "Rent", 310, 155, UiTheme.Primary);
            Panel card2 = UiTheme.CreateStatCard("Step 2", "Pay", 535, 155, UiTheme.Success);
            Panel card3 = UiTheme.CreateStatCard("Step 3", "Return", 760, 155, UiTheme.Warning);
            Panel card4 = UiTheme.CreateStatCard("Step 4", "Review", 985, 155, UiTheme.Purple);

            Panel infoPanel = UiTheme.CreateInfoPanel(
                "Customer Features",
                "This dashboard keeps features separated for academic demonstration while grouping payment and return together because they are part of the same rental flow.\r\n\r\n" +
                "Available operations:\r\n" +
                "• Rent a car with filters\r\n" +
                "• Pay unpaid booking\r\n" +
                "• Return paid active car\r\n" +
                "• Submit reviews and ratings\r\n" +
                "• View booking history\r\n" +
                "• Check special offers",
                310, 295, 900, 260);

            SuspendLayout();

            BackColor = UiTheme.Background;
            ClientSize = new Size(1240, 680);
            StartPosition = FormStartPosition.CenterScreen;
            Text = "Customer Dashboard";

            lblTitle.Text = "Customer Dashboard";
            lblTitle.Font = UiTheme.TitleFont();
            lblTitle.ForeColor = UiTheme.TextDark;
            lblTitle.AutoSize = true;
            lblTitle.Location = new Point(310, 45);

            lblSubtitle.Text = "Rent cars, complete payment, return rentals, and review completed bookings.";
            lblSubtitle.Font = UiTheme.NormalFont();
            lblSubtitle.ForeColor = UiTheme.TextMuted;
            lblSubtitle.AutoSize = true;
            lblSubtitle.Location = new Point(315, 95);

            lblWelcome.Text = "Welcome";
            lblWelcome.Font = new Font("Segoe UI", 12F, FontStyle.Bold);
            lblWelcome.ForeColor = UiTheme.TextDark;
            lblWelcome.AutoSize = true;
            lblWelcome.Location = new Point(315, 125);

            btnRentCar.Click += btnRentCar_Click;
            btnPaymentReturn.Click += btnPaymentReturn_Click;
            btnReviews.Click += btnReviews_Click;
            btnBookingHistory.Click += btnBookingHistory_Click;
            btnOffers.Click += btnOffers_Click;
            btnLogout.Click += btnLogout_Click;

            UiTheme.StyleDangerButton(btnLogout);

            sidebar.Controls.Add(btnRentCar);
            sidebar.Controls.Add(btnPaymentReturn);
            sidebar.Controls.Add(btnReviews);
            sidebar.Controls.Add(btnBookingHistory);
            sidebar.Controls.Add(btnOffers);
            sidebar.Controls.Add(btnLogout);

            Controls.Add(sidebar);
            Controls.Add(lblTitle);
            Controls.Add(lblSubtitle);
            Controls.Add(lblWelcome);
            Controls.Add(card1);
            Controls.Add(card2);
            Controls.Add(card3);
            Controls.Add(card4);
            Controls.Add(infoPanel);

            ResumeLayout(false);
            PerformLayout();
        }
    }
}