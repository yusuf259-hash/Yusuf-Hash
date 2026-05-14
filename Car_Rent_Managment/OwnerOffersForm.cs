using Car_Rent_Managment.Models;
using Car_Rent_Managment.Services;
using System;
using System.Data;
using System.Windows.Forms;

namespace Car_Rent_Managment
{
    public partial class OwnerOffersForm : Form
    {
        private readonly AuthenticatedUser currentUser;
        private readonly OfferService offerService;
        private int selectedOfferId = 0;

        public OwnerOffersForm(AuthenticatedUser user)
        {
            InitializeComponent();

            currentUser = user;
            offerService = new OfferService();

            lblOwner.Text = "Owner: " + currentUser.FullName;

            LoadStatusOptions();
            LoadOwnerCars();
            LoadOffers();
        }

        private void LoadStatusOptions()
        {
            cmbStatus.Items.Clear();
            cmbStatus.Items.Add("Active");
            cmbStatus.Items.Add("Inactive");
            cmbStatus.Items.Add("Expired");
            cmbStatus.SelectedIndex = 0;
        }

        private void LoadOwnerCars()
        {
            DataTable ownerCars = offerService.GetOwnerCarsForOffers(currentUser.UserId);

            DataTable carOptions = new DataTable();
            carOptions.Columns.Add("CarId", typeof(int));
            carOptions.Columns.Add("DisplayName", typeof(string));

            DataRow allCarsRow = carOptions.NewRow();
            allCarsRow["CarId"] = DBNull.Value;
            allCarsRow["DisplayName"] = "All My Cars";
            carOptions.Rows.Add(allCarsRow);

            foreach (DataRow ownerCar in ownerCars.Rows)
            {
                DataRow row = carOptions.NewRow();
                row["CarId"] = ownerCar["CarId"];
                row["DisplayName"] = ownerCar["DisplayName"];
                carOptions.Rows.Add(row);
            }

            cmbCars.DataSource = carOptions;
            cmbCars.DisplayMember = "DisplayName";
            cmbCars.ValueMember = "CarId";
            cmbCars.SelectedIndex = 0;
        }

        private void LoadOffers()
        {
            if (!IsOwnerLoggedIn())
            {
                MessageBox.Show("Owner login is required.");
                return;
            }

            DataTable offersTable = offerService.GetOwnerOffers(currentUser.UserId);
            dgvOffers.DataSource = offersTable;
            ConfigureOffersGrid();
            LoadOfferSummary();
            ClearForm();
        }

        private void LoadOfferSummary()
        {
            DataTable summaryTable = offerService.GetOwnerOfferSummary(currentUser.UserId);

            if (summaryTable.Rows.Count == 0)
            {
                return;
            }

            DataRow row = summaryTable.Rows[0];
            lblTotalOffersValue.Text = ReadInt(row, "TotalOffers").ToString();
            lblActiveOffersValue.Text = ReadInt(row, "ActiveOffers").ToString();
            lblInactiveOffersValue.Text = ReadInt(row, "InactiveOffers").ToString();
        }

        private void ConfigureOffersGrid()
        {
            HideColumn("OfferId");
            HideColumn("CarId");

            SetHeader("OfferTitle", "Offer Title");
            SetHeader("DiscountPercent", "Discount %");
            SetHeader("StartDate", "Start Date");
            SetHeader("EndDate", "End Date");
            SetHeader("OfferScope", "Scope");
            SetHeader("AppliesTo", "Applies To");
            SetHeader("CreatedAt", "Created At");

            DataGridViewColumn? descriptionColumn = dgvOffers.Columns["Description"];
            if (descriptionColumn != null)
            {
                descriptionColumn.FillWeight = 160;
            }

            DataGridViewColumn? offerTitleColumn = dgvOffers.Columns["OfferTitle"];
            if (offerTitleColumn != null)
            {
                offerTitleColumn.FillWeight = 130;
            }

            dgvOffers.ClearSelection();
        }

        private void btnAdd_Click(object? sender, EventArgs e)
        {
            if (!ValidateInput(out string offerTitle, out string description, out decimal discountPercent, out DateTime startDate, out DateTime endDate, out int? carId, out string status))
            {
                return;
            }

            bool success = offerService.AddOwnerOffer(
                currentUser.UserId,
                offerTitle,
                description,
                discountPercent,
                startDate,
                endDate,
                carId,
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

            if (!ValidateInput(out string offerTitle, out string description, out decimal discountPercent, out DateTime startDate, out DateTime endDate, out int? carId, out string status))
            {
                return;
            }

            bool success = offerService.UpdateOwnerOffer(
                currentUser.UserId,
                selectedOfferId,
                offerTitle,
                description,
                discountPercent,
                startDate,
                endDate,
                carId,
                status,
                out string message
            );

            MessageBox.Show(message);

            if (success)
            {
                LoadOffers();
            }
        }

        private void btnDeactivate_Click(object? sender, EventArgs e)
        {
            if (selectedOfferId == 0)
            {
                MessageBox.Show("Please select an offer to deactivate.");
                return;
            }

            DialogResult result = MessageBox.Show(
                "Deactivate the selected offer?",
                "Confirm Deactivate",
                MessageBoxButtons.YesNo,
                MessageBoxIcon.Question
            );

            if (result != DialogResult.Yes)
            {
                return;
            }

            bool success = offerService.DeactivateOwnerOffer(currentUser.UserId, selectedOfferId, out string message);
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
            LoadOwnerCars();
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
            cmbStatus.SelectedItem = ReadString(row, "Status");

            object? carValue = row.Cells["CarId"].Value;
            if (carValue == null || carValue == DBNull.Value)
            {
                cmbCars.SelectedIndex = 0;
            }
            else
            {
                cmbCars.SelectedValue = Convert.ToInt32(carValue);
            }
        }

        private bool ValidateInput(
            out string offerTitle,
            out string description,
            out decimal discountPercent,
            out DateTime startDate,
            out DateTime endDate,
            out int? carId,
            out string status)
        {
            offerTitle = txtOfferTitle.Text.Trim();
            description = txtDescription.Text.Trim();
            discountPercent = nudDiscount.Value;
            startDate = dtpStartDate.Value.Date;
            endDate = dtpEndDate.Value.Date;
            carId = GetSelectedCarId();
            status = cmbStatus.SelectedItem?.ToString() ?? "";

            if (!IsOwnerLoggedIn())
            {
                MessageBox.Show("Owner login is required.");
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
                MessageBox.Show("Please select an offer status.");
                cmbStatus.Focus();
                return false;
            }

            return true;
        }

        private int? GetSelectedCarId()
        {
            if (cmbCars.SelectedValue == null || cmbCars.SelectedValue == DBNull.Value)
            {
                return null;
            }

            return Convert.ToInt32(cmbCars.SelectedValue);
        }

        private void ClearForm()
        {
            selectedOfferId = 0;
            txtOfferTitle.Clear();
            txtDescription.Clear();
            nudDiscount.Value = 1;
            dtpStartDate.Value = DateTime.Today;
            dtpEndDate.Value = DateTime.Today.AddDays(7);

            if (cmbCars.Items.Count > 0)
            {
                cmbCars.SelectedIndex = 0;
            }

            if (cmbStatus.Items.Count > 0)
            {
                cmbStatus.SelectedIndex = 0;
            }

            dgvOffers.ClearSelection();
        }

        private bool IsOwnerLoggedIn()
        {
            return currentUser.UserId > 0 && string.Equals(currentUser.Role, "Owner", StringComparison.OrdinalIgnoreCase);
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

        private int ReadInt(DataRow row, string columnName)
        {
            if (row[columnName] == DBNull.Value)
            {
                return 0;
            }

            return Convert.ToInt32(row[columnName]);
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
