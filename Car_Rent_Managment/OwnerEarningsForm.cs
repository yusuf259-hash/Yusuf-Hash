using Car_Rent_Managment.Models;
using Car_Rent_Managment.Services;
using Car_Rent_Managment.UI;
using System;
using System.Data;
using System.Drawing;
using System.Windows.Forms;

namespace Car_Rent_Managment
{
    public partial class OwnerEarningsForm : Form
    {
        private readonly AuthenticatedUser currentUser;
        private readonly OwnerService ownerService;

        public OwnerEarningsForm(AuthenticatedUser user)
        {
            InitializeComponent();

            currentUser = user;
            ownerService = new OwnerService();

            lblOwner.Text = "Owner: " + currentUser.FullName;
            LoadEarnings();
        }

        private void LoadEarnings()
        {
            DataTable summaryTable = ownerService.GetOwnerSummary(currentUser.UserId);

            if (summaryTable.Rows.Count > 0)
            {
                DataRow row = summaryTable.Rows[0];

                lblTotalBookingsValue.Text = ReadInt(row, "TotalBookings").ToString();
                lblPaidEarningsValue.Text = ReadDecimal(row, "TotalPaidEarnings").ToString("0.00") + " BDT";
                lblPendingAmountValue.Text = ReadDecimal(row, "PendingUnpaidAmount").ToString("0.00") + " BDT";
            }

            dgvEarnings.DataSource = ownerService.GetOwnerEarningDetails(currentUser.UserId);

            if (dgvEarnings.Columns["BookingId"] != null)
            {
                dgvEarnings.Columns["BookingId"].Visible = false;
            }

            dgvEarnings.ClearSelection();
        }

        private void btnRefresh_Click(object? sender, EventArgs e)
        {
            LoadEarnings();
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
