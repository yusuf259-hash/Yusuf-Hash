using Car_Rent_Managment.Models;
using Car_Rent_Managment.Services;
using System;
using System.Data;
using System.Windows.Forms;

namespace Car_Rent_Managment
{
    public partial class SuperAdminDashboardForm : Form
    {
        private readonly AuthenticatedUser currentUser;
        private readonly AdminService adminService;

        public SuperAdminDashboardForm(AuthenticatedUser user)
        {
            InitializeComponent();

            currentUser = user;
            adminService = new AdminService();

            lblWelcome.Text = "Welcome, " + currentUser.FullName;
            LoadDashboardSummary();
        }

        private void LoadDashboardSummary()
        {
            DataTable summaryTable = adminService.GetPlatformSummary();

            if (summaryTable.Rows.Count == 0)
            {
                return;
            }

            DataRow row = summaryTable.Rows[0];

            lblTotalUsersValue.Text = ReadInt(row, "TotalUsers").ToString();
            lblAdminsValue.Text = ReadInt(row, "Admins").ToString();
            lblOwnersValue.Text = ReadInt(row, "Owners").ToString();
            lblCustomersValue.Text = ReadInt(row, "Customers").ToString();
            lblSuspendedUsersValue.Text = ReadInt(row, "SuspendedUsers").ToString();
            lblRevenueValue.Text = ReadDecimal(row, "Revenue").ToString("0.00") + " BDT";
            lblReviewsValue.Text = ReadInt(row, "Reviews").ToString();
            lblOffersValue.Text = ReadInt(row, "TotalOffers").ToString();
        }

        private void btnManageUsers_Click(object sender, EventArgs e)
        {
            ManageUsersForm manageUsersForm = new ManageUsersForm(currentUser);
            manageUsersForm.ShowDialog();
            LoadDashboardSummary();
        }

        private void btnManageCars_Click(object sender, EventArgs e)
        {
            ManageAllCarsForm manageAllCarsForm = new ManageAllCarsForm();
            manageAllCarsForm.ShowDialog();
            LoadDashboardSummary();
        }

        private void btnManageBookings_Click(object sender, EventArgs e)
        {
            ManageAllBookingsForm manageAllBookingsForm = new ManageAllBookingsForm();
            manageAllBookingsForm.ShowDialog();
            LoadDashboardSummary();
        }

        private void btnManageReviews_Click(object sender, EventArgs e)
        {
            ManageReviewsForm manageReviewsForm = new ManageReviewsForm(currentUser);
            manageReviewsForm.ShowDialog();
            LoadDashboardSummary();
        }

        private void btnManageOffers_Click(object sender, EventArgs e)
        {
            ManageOffersForm manageOffersForm = new ManageOffersForm(currentUser);
            manageOffersForm.ShowDialog();
            LoadDashboardSummary();
        }

        private void btnReports_Click(object sender, EventArgs e)
        {
            ReportsForm reportsForm = new ReportsForm();
            reportsForm.ShowDialog();
            LoadDashboardSummary();
        }

        private void btnLogout_Click(object sender, EventArgs e)
        {
            Close();
        }

        private int ReadInt(DataRow row, string columnName)
        {
            if (row[columnName] == DBNull.Value)
            {
                return 0;
            }

            return Convert.ToInt32(row[columnName]);
        }

        private decimal ReadDecimal(DataRow row, string columnName)
        {
            if (row[columnName] == DBNull.Value)
            {
                return 0;
            }

            return Convert.ToDecimal(row[columnName]);
        }
    }
}
