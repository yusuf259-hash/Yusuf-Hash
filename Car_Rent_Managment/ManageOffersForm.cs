using Car_Rent_Managment.Models;
using Car_Rent_Managment.Services;
using System;
using System.Data;
using System.Windows.Forms;

namespace Car_Rent_Managment
{
    public partial class ManageOffersForm : Form
    {
        private readonly AuthenticatedUser currentUser;
        private readonly OfferService offerService;
        private int selectedOfferId = 0;

        public ManageOffersForm(AuthenticatedUser user)
        {
            InitializeComponent();

            currentUser = user;
            offerService = new OfferService();

            lblAdmin.Text = "User: " + currentUser.FullName + " (" + currentUser.Role + ")";

            LoadScopeOptions();
            LoadStatusOptions();
            LoadOffers();
        }

        private void LoadScopeOptions()
        {
            cmbScope.Items.Clear();
            cmbScope.Items.Add("Platform");
            cmbScope.Items.Add("Owner");
            cmbScope.Items.Add("Car");
            cmbScope.SelectedIndex = 0;
        }

        private void LoadStatusOptions()
        {
            cmbStatus.Items.Clear();
            cmbStatus.Items.Add("Active");
            cmbStatus.Items.Add("Inactive");
            cmbStatus.Items.Add("Expired");
            cmbStatus.SelectedIndex = 0;
        }

        private void LoadOffers()
        {
            if (!IsAdminOrSuperAdmin())
            {
                MessageBox.Show("Admin or SuperAdmin login is required.");
                Close();
                return;
            }

            DataTable offersTable = offerService.GetAllOffersForAdmin();
            dgvOffers.DataSource = offersTable;
            ConfigureOffersGrid();
            LoadOfferSummary();
            ClearForm();
        }

        private void LoadOfferSummary()
        {
            DataTable summaryTable = offerService.GetOfferSummaryForAdmin();

            if (summaryTable.Rows.Count == 0)
            {
                return;
            }

            DataRow row = summaryTable.Rows[0];
            lblTotalOffersValue.Text = ReadInt(row, "TotalOffers").ToString();
            lblActiveOffersValue.Text = ReadInt(row, "ActiveOffers").ToString();
            lblInactiveOffersValue.Text = ReadInt(row, "InactiveOffers").ToString();
            lblPlatformOffersValue.Text = ReadInt(row, "PlatformOffers").ToString();
            lblOwnerCarOffersValue.Text = ReadInt(row, "OwnerCarOffers").ToString();
        }

        private void ConfigureOffersGrid()
        {
            HideColumn("OfferId");
            HideColumn("CarId");
            HideColumn("CreatedByUserId");

            SetHeader("OfferTitle", "Offer Title");
            SetHeader("DiscountPercent", "Discount %");
            SetHeader("StartDate", "Start Date");
            SetHeader("EndDate", "End Date");
            SetHeader("OfferScope", "Scope");
            SetHeader("AppliesTo", "Applies To");
            SetHeader("CreatedBy", "Created By");
            SetHeader("CreatorRole", "Role");
            SetHeader("CreatedAt", "Created At");

            SetFillWeight("OfferTitle", 130);
            SetFillWeight("Description", 160);
            SetFillWeight("AppliesTo", 130);

            dgvOffers.ClearSelection();
        }

        private void btnAddPlatform_Click(object? sender, EventArgs e)
        {
            if (!ValidateInput(out string offerTitle, out string description, out decimal discountPercent, out DateTime startDate, out DateTime endDate, out string status))
            {
                return;
            }

            bool success = offerService.AddPlatformOffer(
                currentUser.UserId,
                offerTitle,
                description,
                discountPercent,
                startDate,
                endDate,
                status,
                out string message
            );

            MessageBox.Show(message);

            if (success)
            {
                LoadOffers();
            }
        }

        private void btnUpdate_Click(object? sender, EventArgs e)
        {
            if (selectedOfferId == 0)
            {
                MessageBox.Show("Please select an offer to update.");
                return;
            }

            if (!ValidateInput(out string offerTitle, out string description, out decimal discountPercent, out DateTime startDate, out DateTime endDate, out string status))
            {
                return;
            }

            bool success = offerService.UpdateAdminOffer(
                currentUser.UserId,
                selectedOfferId,
                offerTitle,
                description,
                discountPercent,
                startDate,
                endDate,
                status,
                out string message
            );

            MessageBox.Show(message);

            if (success)
            {
                LoadOffers();
            }
        }

        private void btnActivate_Click(object? sender, EventArgs e)
        {
            ChangeSelectedOfferStatus("Active");
        }

        private void btnDeactivate_Click(object? sender, EventArgs e)
        {
            ChangeSelectedOfferStatus("Inactive");
        }

        private void ChangeSelectedOfferStatus(string status)
        {
            if (selectedOfferId == 0)
            {
                MessageBox.Show("Please select an offer.");
                return;
            }

            bool success = offerService.SetAdminOfferStatus(currentUser.UserId, selectedOfferId, status, out string message);
            MessageBox.Show(message);

            if (success)
            {
                LoadOffers();
            }
        }

        private void btnDelete_Click(object? sender, EventArgs e)
        {
            if (selectedOfferId == 0)
            {
                MessageBox.Show("Please select an offer to delete.");
                return;
            }

            DialogResult result = MessageBox.Show(
                "Delete the selected offer?\n\nActive offers must be deactivated before deletion.",
                "Confirm Delete",
                MessageBoxButtons.YesNo,
                MessageBoxIcon.Warning
            );

            if (result != DialogResult.Yes)
            {
                return;
            }

            bool success = offerService.DeleteAdminOffer(currentUser.UserId, selectedOfferId, out string message);
            MessageBox.Show(message);

            if (success)
            {
                LoadOffers();
            }
        }

        private void btnClear_Click(object? sender, EventArgs e)
        {
            ClearForm();
        }

        private void btnRefresh_Click(object? sender, EventArgs e)
        {
            LoadOffers();
        }

        private void dgvOffers_CellClick(object? sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex < 0)
            {
                return;
            }

            DataGridViewRow row = dgvOffers.Rows[e.RowIndex];

            selectedOfferId = Convert.ToInt32(row.Cells["OfferId"].Value);
            txtOfferTitle.Text = ReadString(row, "OfferTitle");
            txtDescription.Text = ReadString(row, "Description");
            nudDiscount.Value = ReadDiscount(row);
            dtpStartDate.Value = ReadDate(row, "StartDate");
            dtpEndDate.Value = ReadDate(row, "EndDate");

            string scope = ReadString(row, "OfferScope");
            if (cmbScope.Items.Contains(scope))
            {
                cmbScope.SelectedItem = scope;
            }

            string status = ReadString(row, "Status");
            if (cmbStatus.Items.Contains(status))
            {
                cmbStatus.SelectedItem = status;
            }
        }

        private bool ValidateInput(
            out string offerTitle,
            out string description,
            out decimal discountPercent,
            out DateTime startDate,
            out DateTime endDate,
            out string status)
        {
            offerTitle = txtOfferTitle.Text.Trim();
            description = txtDescription.Text.Trim();
            discountPercent = nudDiscount.Value;
            startDate = dtpStartDate.Value.Date;
            endDate = dtpEndDate.Value.Date;
            status = cmbStatus.SelectedItem?.ToString() ?? "";

            if (!IsAdminOrSuperAdmin())
            {
                MessageBox.Show("Admin or SuperAdmin login is required.");
                return false;
            }

            if (string.IsNullOrWhiteSpace(offerTitle))
            {
                MessageBox.Show("Offer title cannot be empty.");
                txtOfferTitle.Focus();
                return false;
            }

            if (discountPercent <= 0 || discountPercent > 100)
            {
                MessageBox.Show("Discount percent must be greater than 0 and less than or equal to 100.");
                nudDiscount.Focus();
                return false;
            }

            if (endDate < startDate)
            {
                MessageBox.Show("End date must be greater than or equal to start date.");
                dtpEndDate.Focus();
                return false;
            }

            if (string.IsNullOrWhiteSpace(status))
            {
                MessageBox.Show("Please select a status.");
                cmbStatus.Focus();
                return false;
            }

            return true;
        }

        private void ClearForm()
        {
            selectedOfferId = 0;
            txtOfferTitle.Clear();
            txtDescription.Clear();
            nudDiscount.Value = 1;
            dtpStartDate.Value = DateTime.Today;
            dtpEndDate.Value = DateTime.Today.AddDays(30);
            cmbScope.SelectedItem = "Platform";
            cmbStatus.SelectedItem = "Active";
            dgvOffers.ClearSelection();
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
            DataGridViewColumn? column = dgvOffers.Columns[columnName];
            if (column != null)
            {
                column.Visible = false;
            }
        }

        private void SetHeader(string columnName, string headerText)
        {
            DataGridViewColumn? column = dgvOffers.Columns[columnName];
            if (column != null)
            {
                column.HeaderText = headerText;
            }
        }

        private void SetFillWeight(string columnName, float fillWeight)
        {
            DataGridViewColumn? column = dgvOffers.Columns[columnName];
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

        private decimal ReadDiscount(DataGridViewRow row)
        {
            object? value = row.Cells["DiscountPercent"].Value;

            if (value == null || value == DBNull.Value)
            {
                return 1;
            }

            decimal discount = Convert.ToDecimal(value);

            if (discount < nudDiscount.Minimum)
            {
                return nudDiscount.Minimum;
            }

            if (discount > nudDiscount.Maximum)
            {
                return nudDiscount.Maximum;
            }

            return discount;
        }

        private DateTime ReadDate(DataGridViewRow row, string columnName)
        {
            object? value = row.Cells[columnName].Value;

            if (value == null || value == DBNull.Value)
            {
                return DateTime.Today;
            }

            return Convert.ToDateTime(value);
        }
    }
}
