using Car_Rent_Managment.Models;
using Car_Rent_Managment.Services;
using Car_Rent_Managment.UI;
using System;
using System.Data;
using System.Drawing;
using System.Windows.Forms;

namespace Car_Rent_Managment
{
    public partial class OwnerDashboardForm : Form
    {
        private readonly AuthenticatedUser currentUser;
        private readonly OwnerService ownerService;

        public OwnerDashboardForm(AuthenticatedUser user)
        {
            InitializeComponent();

            currentUser = user;
            ownerService = new OwnerService();

            lblWelcome.Text = "Welcome, " + currentUser.FullName;
            LoadOwnerSummary();
        }

        private void LoadOwnerSummary()
        {
            DataTable summaryTable = ownerService.GetOwnerSummary(currentUser.UserId);

            if (summaryTable.Rows.Count == 0)
            {
                return;
            }

            DataRow row = summaryTable.Rows[0];

            lblTotalCarsValue.Text = ReadInt(row, "TotalCars").ToString();
            lblAvailableCarsValue.Text = ReadInt(row, "AvailableCars").ToString();
            lblRentedCarsValue.Text = ReadInt(row, "RentedCars").ToString();
            lblInactiveCarsValue.Text = ReadInt(row, "InactiveCars").ToString();
            lblTotalBookingsValue.Text = ReadInt(row, "TotalBookings").ToString();
            lblPaidEarningsValue.Text = ReadDecimal(row, "TotalPaidEarnings").ToString("0.00") + " BDT";
            lblPendingAmountValue.Text = ReadDecimal(row, "PendingUnpaidAmount").ToString("0.00") + " BDT";
            lblActiveOffersValue.Text = ReadInt(row, "ActiveOffers").ToString();
        }

        private void btnManageCars_Click(object? sender, EventArgs e)
        {
            ManageCarsForm manageCarsForm = new ManageCarsForm(currentUser);
            manageCarsForm.ShowDialog();
            LoadOwnerSummary();
        }

        private void btnViewBookings_Click(object? sender, EventArgs e)
        {
            BookingHistoryForm bookingHistoryForm = new BookingHistoryForm(currentUser, "Owner");
            bookingHistoryForm.ShowDialog();
            LoadOwnerSummary();
        }

        private void btnEarnings_Click(object? sender, EventArgs e)
        {
            OwnerEarningsForm earningsForm = new OwnerEarningsForm(currentUser);
            earningsForm.ShowDialog();
            LoadOwnerSummary();
        }

        private void btnInventory_Click(object? sender, EventArgs e)
        {
            OwnerInventoryDashboardForm inventoryForm = new OwnerInventoryDashboardForm(currentUser);
            inventoryForm.ShowDialog();
            LoadOwnerSummary();
        }

        private void btnMyOffers_Click(object? sender, EventArgs e)
        {
            OwnerOffersForm ownerOffersForm = new OwnerOffersForm(currentUser);
            ownerOffersForm.ShowDialog();
            LoadOwnerSummary();
        }

        private void btnLogout_Click(object? sender, EventArgs e)
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
