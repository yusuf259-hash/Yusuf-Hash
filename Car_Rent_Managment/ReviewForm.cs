using Car_Rent_Managment.Models;
using Car_Rent_Managment.Services;
using System;
using System.Data;
using System.Windows.Forms;
using System.Xml.Linq;

namespace Car_Rent_Managment
{
    public partial class ReviewForm : Form
    {
        private readonly AuthenticatedUser currentUser;
        private readonly CustomerFeatureService customerFeatureService;

        private int selectedBookingId = 0;
        private int selectedCarId = 0;
        private int selectedOwnerId = 0;

        public ReviewForm(AuthenticatedUser user)
        {
            InitializeComponent();

            currentUser = user;
            customerFeatureService = new CustomerFeatureService();

            lblCustomer.Text = "Customer: " + currentUser.FullName;

            cmbRating.Items.Add("1");
            cmbRating.Items.Add("2");
            cmbRating.Items.Add("3");
            cmbRating.Items.Add("4");
            cmbRating.Items.Add("5");
            cmbRating.SelectedIndex = 4;

            LoadCompletedBookings();
        }

        private void LoadCompletedBookings()
        {
            DataTable table = customerFeatureService.GetCompletedBookingsWithoutReview(currentUser.UserId);
            dgvCompletedBookings.DataSource = table;

            if (dgvCompletedBookings.Columns["BookingId"] != null)
            {
                dgvCompletedBookings.Columns["BookingId"].Visible = false;
            }

            if (dgvCompletedBookings.Columns["CarId"] != null)
            {
                dgvCompletedBookings.Columns["CarId"].Visible = false;
            }

            if (dgvCompletedBookings.Columns["OwnerId"] != null)
            {
                dgvCompletedBookings.Columns["OwnerId"].Visible = false;
            }

            ClearSelection();
        }

        private void dgvCompletedBookings_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex < 0)
            {
                return;
            }

            DataGridViewRow row = dgvCompletedBookings.Rows[e.RowIndex];

            selectedBookingId = Convert.ToInt32(row.Cells["BookingId"].Value);
            selectedCarId = Convert.ToInt32(row.Cells["CarId"].Value);
            selectedOwnerId = Convert.ToInt32(row.Cells["OwnerId"].Value);

            lblSelectedBooking.Text = "Selected Booking: #" + selectedBookingId;
            lblCar.Text = "Car: " + row.Cells["CarName"].Value;
        }

        private void btnSubmitReview_Click(object sender, EventArgs e)
        {
            if (selectedBookingId == 0)
            {
                MessageBox.Show("Please select a completed booking first.");
                return;
            }

            int rating = Convert.ToInt32(cmbRating.SelectedItem.ToString());
            string comment = txtComment.Text.Trim();

            if (string.IsNullOrWhiteSpace(comment))
            {
                MessageBox.Show("Please write a comment.");
                txtComment.Focus();
                return;
            }

            bool success = customerFeatureService.AddReview(
                selectedBookingId,
                currentUser.UserId,
                selectedCarId,
                selectedOwnerId,
                rating,
                comment,
                out string message
            );

            MessageBox.Show(message);

            if (success)
            {
                LoadCompletedBookings();
            }
        }

        private void btnRefresh_Click(object sender, EventArgs e)
        {
            LoadCompletedBookings();
        }

        private void ClearSelection()
        {
            selectedBookingId = 0;
            selectedCarId = 0;
            selectedOwnerId = 0;

            lblSelectedBooking.Text = "Selected Booking: None";
            lblCar.Text = "Car: -";
            txtComment.Clear();

            if (cmbRating.Items.Count > 0)
            {
                cmbRating.SelectedIndex = 4;
            }

            dgvCompletedBookings.ClearSelection();
        }
    }
}