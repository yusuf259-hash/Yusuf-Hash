using Car_Rent_Managment.Services;
using System;
using System.Data;
using System.Windows.Forms;

namespace Car_Rent_Managment
{
    public partial class ManageAllBookingsForm : Form
    {
        private readonly AdminService adminService;
        private int selectedBookingId = 0;
        private string selectedBookingStatus = "";

        public ManageAllBookingsForm()
        {
            InitializeComponent();

            adminService = new AdminService();
            LoadBookings();
        }

        private void LoadBookings()
        {
            DataTable bookingsTable = adminService.GetAllBookings();
            dgvBookings.DataSource = bookingsTable;
            LoadBookingSummary();

            if (dgvBookings.Columns["BookingId"] != null)
            {
                dgvBookings.Columns["BookingId"].Visible = false;
            }

            ClearSelection();
        }

        private void LoadBookingSummary()
        {
            DataTable summaryTable = adminService.GetBookingSummary();

            if (summaryTable.Rows.Count == 0)
            {
                return;
            }

            DataRow row = summaryTable.Rows[0];
            lblTotalBookingsValue.Text = ReadInt(row, "TotalBookings").ToString();
            lblActiveBookingsValue.Text = ReadInt(row, "ActiveBookings").ToString();
            lblCompletedBookingsValue.Text = ReadInt(row, "CompletedBookings").ToString();
            lblCancelledBookingsValue.Text = ReadInt(row, "CancelledBookings").ToString();
            lblPaidBookingsValue.Text = ReadInt(row, "PaidBookings").ToString();
            lblUnpaidBookingsValue.Text = ReadInt(row, "UnpaidBookings").ToString();
        }

        private void dgvBookings_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex < 0)
            {
                return;
            }

            DataGridViewRow row = dgvBookings.Rows[e.RowIndex];

            selectedBookingId = Convert.ToInt32(row.Cells["BookingId"].Value);
            selectedBookingStatus = row.Cells["BookingStatus"].Value.ToString() ?? "";

            lblSelectedBooking.Text = "Selected Booking: #" + selectedBookingId;
            lblCustomer.Text = "Customer: " + row.Cells["CustomerName"].Value;
            lblCar.Text = "Car: " + row.Cells["CarName"].Value;
            lblStatus.Text = "Status: " + selectedBookingStatus;
            lblFine.Text = "Fine: " + row.Cells["FineAmount"].Value + " BDT, " + row.Cells["FineStatus"].Value;
        }

        private void btnCancelBooking_Click(object sender, EventArgs e)
        {
            if (selectedBookingId == 0)
            {
                MessageBox.Show("Please select a booking first.");
                return;
            }

            DialogResult result = MessageBox.Show(
                "Are you sure you want to cancel this booking?",
                "Confirm Cancel",
                MessageBoxButtons.YesNo,
                MessageBoxIcon.Warning
            );

            if (result != DialogResult.Yes)
            {
                return;
            }

            bool success = adminService.CancelBooking(selectedBookingId, out string message);

            MessageBox.Show(message);

            if (success)
            {
                LoadBookings();
            }
        }

        private void btnMarkFinePaid_Click(object sender, EventArgs e)
        {
            if (selectedBookingId == 0)
            {
                MessageBox.Show("Please select a booking first.");
                return;
            }

            bool success = adminService.MarkFinePaid(selectedBookingId, out string message);

            MessageBox.Show(message);

            if (success)
            {
                LoadBookings();
            }
        }

        private void btnRefresh_Click(object sender, EventArgs e)
        {
            LoadBookings();
        }

        private void ClearSelection()
        {
            selectedBookingId = 0;
            selectedBookingStatus = "";

            lblSelectedBooking.Text = "Selected Booking: None";
            lblCustomer.Text = "Customer: -";
            lblCar.Text = "Car: -";
            lblStatus.Text = "Status: -";
            lblFine.Text = "Fine: -";

            dgvBookings.ClearSelection();
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
