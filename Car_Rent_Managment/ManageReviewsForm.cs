using Car_Rent_Managment.Models;
using Car_Rent_Managment.Services;
using System;
using System.Data;
using System.Windows.Forms;

namespace Car_Rent_Managment
{
    public partial class ManageReviewsForm : Form
    {
        private readonly AuthenticatedUser currentUser;
        private readonly ReviewService reviewService;
        private int selectedReviewId = 0;
        private int selectedOwnerId = 0;

        public ManageReviewsForm(AuthenticatedUser user)
        {
            InitializeComponent();

            currentUser = user;
            reviewService = new ReviewService();

            lblUser.Text = "User: " + currentUser.FullName + " (" + currentUser.Role + ")";

            LoadRatingOptions();
            ConfigureRoleActions();
            LoadReviews();
        }

        private void LoadRatingOptions()
        {
            cmbRating.Items.Clear();
            cmbRating.Items.Add("Any");
            cmbRating.Items.Add("1");
            cmbRating.Items.Add("2");
            cmbRating.Items.Add("3");
            cmbRating.Items.Add("4");
            cmbRating.Items.Add("5");
            cmbRating.SelectedIndex = 0;
        }

        private void ConfigureRoleActions()
        {
            bool isSuperAdmin = string.Equals(currentUser.Role, "SuperAdmin", StringComparison.OrdinalIgnoreCase);
            btnSuspendOwner.Visible = isSuperAdmin;
            btnSuspendOwner.Enabled = isSuperAdmin;
        }

        private void LoadReviews()
        {
            if (!IsAdminOrSuperAdmin())
            {
                MessageBox.Show("Admin or SuperAdmin login is required.");
                Close();
                return;
            }

            int rating = GetSelectedRating();
            DataTable table = reviewService.GetReviews(
                rating,
                txtCustomerName.Text.Trim(),
                txtOwnerName.Text.Trim(),
                txtCarName.Text.Trim()
            );

            dgvReviews.DataSource = table;
            ConfigureReviewsGrid();
            LoadReviewSummary();
            ClearSelection();
        }

        private void LoadReviewSummary()
        {
            DataTable summaryTable = reviewService.GetReviewSummary();

            if (summaryTable.Rows.Count == 0)
            {
                return;
            }

            DataRow row = summaryTable.Rows[0];
            lblTotalReviewsValue.Text = ReadInt(row, "TotalReviews").ToString();
            lblAverageRatingValue.Text = ReadDecimal(row, "AverageRating").ToString("0.00");
            lblLowRatingsValue.Text = ReadInt(row, "LowRatings").ToString();
            lblFiveStarReviewsValue.Text = ReadInt(row, "FiveStarReviews").ToString();
        }

        private void ConfigureReviewsGrid()
        {
            HideColumn("OwnerId");

            SetHeader("ReviewId", "Review ID");
            SetHeader("BookingId", "Booking ID");
            SetHeader("CustomerName", "Customer");
            SetHeader("OwnerName", "Owner");
            SetHeader("CarName", "Car");
            SetHeader("CreatedAt", "Created At");

            SetFillWeight("Comment", 190);
            SetFillWeight("CustomerName", 120);
            SetFillWeight("OwnerName", 120);
            SetFillWeight("CarName", 120);

            dgvReviews.ClearSelection();
        }

        private void btnApplyFilter_Click(object? sender, EventArgs e)
        {
            LoadReviews();
        }

        private void btnClearFilter_Click(object? sender, EventArgs e)
        {
            cmbRating.SelectedIndex = 0;
            txtCustomerName.Clear();
            txtOwnerName.Clear();
            txtCarName.Clear();
            LoadReviews();
        }

        private void btnRefresh_Click(object? sender, EventArgs e)
        {
            LoadReviews();
        }

        private void btnDeleteReview_Click(object? sender, EventArgs e)
        {
            if (selectedReviewId == 0)
            {
                MessageBox.Show("Please select a review to delete.");
                return;
            }

            DialogResult result = MessageBox.Show(
                "Delete the selected review?",
                "Confirm Delete",
                MessageBoxButtons.YesNo,
                MessageBoxIcon.Warning
            );

            if (result != DialogResult.Yes)
            {
                return;
            }

            bool success = reviewService.DeleteReview(selectedReviewId, out string message);
            MessageBox.Show(message);

            if (success)
            {
                LoadReviews();
            }
        }

        private void btnSuspendOwner_Click(object? sender, EventArgs e)
        {
            if (!string.Equals(currentUser.Role, "SuperAdmin", StringComparison.OrdinalIgnoreCase))
            {
                MessageBox.Show("Admin cannot suspend owners from this page.");
                return;
            }

            if (selectedReviewId == 0 || selectedOwnerId == 0)
            {
                MessageBox.Show("Please select a review first.");
                return;
            }

            DialogResult result = MessageBox.Show(
                "Suspend the owner connected to this review?",
                "Confirm Suspension",
                MessageBoxButtons.YesNo,
                MessageBoxIcon.Warning
            );

            if (result != DialogResult.Yes)
            {
                return;
            }

            bool success = reviewService.SuspendOwnerFromReview(selectedReviewId, currentUser.Role, out string message);
            MessageBox.Show(message);

            if (success)
            {
                LoadReviews();
            }
        }

        private void dgvReviews_CellClick(object? sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex < 0)
            {
                return;
            }

            DataGridViewRow row = dgvReviews.Rows[e.RowIndex];

            selectedReviewId = Convert.ToInt32(row.Cells["ReviewId"].Value);
            selectedOwnerId = Convert.ToInt32(row.Cells["OwnerId"].Value);

            lblSelectedReview.Text = "Selected Review: #" + selectedReviewId;
            lblSelectedOwner.Text = "Owner: " + ReadString(row, "OwnerName");
        }

        private int GetSelectedRating()
        {
            if (cmbRating.SelectedItem == null || cmbRating.SelectedItem.ToString() == "Any")
            {
                return 0;
            }

            int.TryParse(cmbRating.SelectedItem.ToString(), out int rating);
            return rating;
        }

        private void ClearSelection()
        {
            selectedReviewId = 0;
            selectedOwnerId = 0;
            lblSelectedReview.Text = "Selected Review: None";
            lblSelectedOwner.Text = "Owner: -";
            dgvReviews.ClearSelection();
        }

        private bool IsAdminOrSuperAdmin()
        {
            return currentUser.UserId > 0
                && (
                    string.Equals(currentUser.Role, "Admin", StringComparison.OrdinalIgnoreCase)
                    || string.Equals(currentUser.Role, "SuperAdmin", StringComparison.OrdinalIgnoreCase)
                );
        }

        private void HideColumn(string columnName)
        {
            DataGridViewColumn? column = dgvReviews.Columns[columnName];
            if (column != null)
            {
                column.Visible = false;
            }
        }

        private void SetHeader(string columnName, string headerText)
        {
            DataGridViewColumn? column = dgvReviews.Columns[columnName];
            if (column != null)
            {
                column.HeaderText = headerText;
            }
        }

        private void SetFillWeight(string columnName, float fillWeight)
        {
            DataGridViewColumn? column = dgvReviews.Columns[columnName];
            if (column != null)
            {
                column.FillWeight = fillWeight;
            }
        }

        private string ReadString(DataGridViewRow row, string columnName)
        {
            object? value = row.Cells[columnName].Value;

            if (value == null || value == DBNull.Value)
            {
                return "";
            }

            return value.ToString() ?? "";
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
