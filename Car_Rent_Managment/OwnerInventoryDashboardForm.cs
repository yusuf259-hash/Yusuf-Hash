using Car_Rent_Managment.Models;
using Car_Rent_Managment.Services;
using Car_Rent_Managment.UI;
using System;
using System.Data;
using System.Drawing;
using System.Windows.Forms;

namespace Car_Rent_Managment
{
    public partial class OwnerInventoryDashboardForm : Form
    {
        private readonly AuthenticatedUser currentUser;
        private readonly OwnerService ownerService;

        public OwnerInventoryDashboardForm(AuthenticatedUser user)
        {
            InitializeComponent();

            currentUser = user;
            ownerService = new OwnerService();

            lblOwner.Text = "Owner: " + currentUser.FullName;
            LoadInventory();
        }

        private void LoadInventory()
        {
            DataTable summaryTable = ownerService.GetOwnerSummary(currentUser.UserId);

            if (summaryTable.Rows.Count > 0)
            {
                DataRow row = summaryTable.Rows[0];

                lblTotalCarsValue.Text = ReadInt(row, "TotalCars").ToString();
                lblAvailableCarsValue.Text = ReadInt(row, "AvailableCars").ToString();
                lblRentedCarsValue.Text = ReadInt(row, "RentedCars").ToString();
                lblInactiveCarsValue.Text = ReadInt(row, "InactiveCars").ToString();
                lblTotalBookingsValue.Text = ReadInt(row, "TotalBookings").ToString();
            }

            dgvCars.DataSource = ownerService.GetOwnerCarInventoryDetails(currentUser.UserId);
            dgvRecentBookings.DataSource = ownerService.GetOwnerRecentBookings(currentUser.UserId);

            if (dgvCars.Columns["CarId"] != null)
            {
                dgvCars.Columns["CarId"].Visible = false;
            }

            if (dgvRecentBookings.Columns["BookingId"] != null)
            {
                dgvRecentBookings.Columns["BookingId"].Visible = false;
            }

            dgvCars.ClearSelection();
            dgvRecentBookings.ClearSelection();
        }

        private void btnRefresh_Click(object? sender, EventArgs e)
        {
            LoadInventory();
        }

        private void btnClose_Click(object? sender, EventArgs e)
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

    }
}
