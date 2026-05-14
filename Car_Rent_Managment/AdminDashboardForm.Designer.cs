using Car_Rent_Managment.UI;
using System.Drawing;
using System.Windows.Forms;

namespace Car_Rent_Managment
{
    partial class AdminDashboardForm
    {
        private System.ComponentModel.IContainer components = null;

        private Panel sidebar = null!;
        private Label lblTitle = null!;
        private Label lblWelcome = null!;
        private Button btnManageUsers = null!;
        private Button btnManageCars = null!;
        private Button btnManageBookings = null!;
        private Button btnManageReviews = null!;
        private Button btnManageOffers = null!;
        private Button btnReports = null!;
        private Button btnLogout = null!;

        private Panel cardTotalUsers = null!;
        private Panel cardCustomers = null!;
        private Panel cardOwners = null!;
        private Panel cardCars = null!;
        private Panel cardBookings = null!;
        private Panel cardRevenue = null!;
        private Panel cardReviews = null!;
        private Panel cardActiveOffers = null!;

        private Label lblTotalUsersValue = null!;
        private Label lblCustomersValue = null!;
        private Label lblOwnersValue = null!;
        private Label lblCarsValue = null!;
        private Label lblBookingsValue = null!;
        private Label lblRevenueValue = null!;
        private Label lblReviewsValue = null!;
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
            sidebar = UiTheme.CreateSidebar("Car Rental", "Admin Panel");
            lblTitle = new Label();
            lblWelcome = new Label();

            btnManageUsers = UiTheme.CreateSidebarButton("Manage Users", 185);
            btnManageCars = UiTheme.CreateSidebarButton("Manage Cars", 237);
            btnManageBookings = UiTheme.CreateSidebarButton("Manage Bookings", 289);
            btnManageReviews = UiTheme.CreateSidebarButton("Manage Reviews", 341);
            btnManageOffers = UiTheme.CreateSidebarButton("Manage Offers", 393);
            btnReports = UiTheme.CreateSidebarButton("Reports", 445);
            btnLogout = UiTheme.CreateSidebarButton("Logout", 560);

            cardTotalUsers = new Panel();
            cardCustomers = new Panel();
            cardOwners = new Panel();
            cardCars = new Panel();
            cardBookings = new Panel();
            cardRevenue = new Panel();
            cardReviews = new Panel();
            cardActiveOffers = new Panel();

            lblTotalUsersValue = new Label();
            lblCustomersValue = new Label();
            lblOwnersValue = new Label();
            lblCarsValue = new Label();
            lblBookingsValue = new Label();
            lblRevenueValue = new Label();
            lblReviewsValue = new Label();
            lblActiveOffersValue = new Label();

            Panel infoPanel = UiTheme.CreateInfoPanel(
                "Admin Control Center",
                "Use this dashboard to manage customers, owners, cars, bookings, reviews, offers, and reports. Summary cards update after each management page closes.",
                300, 410, 890, 155);

            SuspendLayout();

            BackColor = UiTheme.Background;
            ClientSize = new Size(1240, 700);
            StartPosition = FormStartPosition.CenterScreen;
            Text = "Admin Dashboard";

            lblTitle.Text = "Admin Dashboard";
            lblTitle.Font = UiTheme.TitleFont();
            lblTitle.ForeColor = UiTheme.TextDark;
            lblTitle.AutoSize = true;
            lblTitle.Location = new Point(300, 40);

            lblWelcome.Text = "Welcome";
            lblWelcome.Font = new Font("Segoe UI", 12F, FontStyle.Bold);
            lblWelcome.ForeColor = UiTheme.TextDark;
            lblWelcome.AutoSize = true;
            lblWelcome.Location = new Point(305, 92);

            UiTheme.ConfigureStatCard(cardTotalUsers, lblTotalUsersValue, "Total Users", "0", 300, 145, UiTheme.Primary);
            UiTheme.ConfigureStatCard(cardCustomers, lblCustomersValue, "Customers", "0", 525, 145, UiTheme.Success);
            UiTheme.ConfigureStatCard(cardOwners, lblOwnersValue, "Owners", "0", 750, 145, UiTheme.Warning);
            UiTheme.ConfigureStatCard(cardCars, lblCarsValue, "Cars", "0", 975, 145, UiTheme.Purple);
            UiTheme.ConfigureStatCard(cardBookings, lblBookingsValue, "Bookings", "0", 300, 275, UiTheme.Primary);
            UiTheme.ConfigureStatCard(cardRevenue, lblRevenueValue, "Revenue", "0.00 BDT", 525, 275, UiTheme.Success, 205, 100, 14F);
            UiTheme.ConfigureStatCard(cardReviews, lblReviewsValue, "Reviews", "0", 750, 275, UiTheme.Warning);
            UiTheme.ConfigureStatCard(cardActiveOffers, lblActiveOffersValue, "Active Offers", "0", 975, 275, UiTheme.Purple);

            btnManageUsers.Click += btnManageUsers_Click;
            btnManageCars.Click += btnManageCars_Click;
            btnManageBookings.Click += btnManageBookings_Click;
            btnManageReviews.Click += btnManageReviews_Click;
            btnManageOffers.Click += btnManageOffers_Click;
            btnReports.Click += btnReports_Click;
            btnLogout.Click += btnLogout_Click;

            UiTheme.StyleDangerButton(btnLogout);

            sidebar.Controls.Add(btnManageUsers);
            sidebar.Controls.Add(btnManageCars);
            sidebar.Controls.Add(btnManageBookings);
            sidebar.Controls.Add(btnManageReviews);
            sidebar.Controls.Add(btnManageOffers);
            sidebar.Controls.Add(btnReports);
            sidebar.Controls.Add(btnLogout);

            Controls.Add(sidebar);
            Controls.Add(lblTitle);
            Controls.Add(lblWelcome);
            Controls.Add(cardTotalUsers);
            Controls.Add(cardCustomers);
            Controls.Add(cardOwners);
            Controls.Add(cardCars);
            Controls.Add(cardBookings);
            Controls.Add(cardRevenue);
            Controls.Add(cardReviews);
            Controls.Add(cardActiveOffers);
            Controls.Add(infoPanel);

            ResumeLayout(false);
            PerformLayout();
        }
    }
}
