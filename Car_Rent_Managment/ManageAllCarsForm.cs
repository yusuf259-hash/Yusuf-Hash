using Car_Rent_Managment.Services;
using System;
using System.Data;
using System.Windows.Forms;

namespace Car_Rent_Managment
{
    public partial class ManageAllCarsForm : Form
    {
        private readonly AdminService adminService;
        private int selectedCarId = 0;

        public ManageAllCarsForm()
        {
            InitializeComponent();

            adminService = new AdminService();

            cmbStatus.Items.Add("Available");
            cmbStatus.Items.Add("Rented");
            cmbStatus.Items.Add("Maintenance");
            cmbStatus.Items.Add("Unavailable");
            cmbStatus.SelectedIndex = 0;

            LoadCars();
        }

        private void LoadCars()
        {
            DataTable carsTable = adminService.GetAllCars();
            dgvCars.DataSource = carsTable;
            LoadCarSummary();

            if (dgvCars.Columns["CarId"] != null)
            {
                dgvCars.Columns["CarId"].Visible = false;
            }

            ClearSelection();
        }

        private void LoadCarSummary()
        {
            DataTable summaryTable = adminService.GetCarSummary();

            if (summaryTable.Rows.Count == 0)
            {
                return;
            }

            DataRow row = summaryTable.Rows[0];
            lblTotalCarsValue.Text = ReadInt(row, "TotalCars").ToString();
            lblAvailableCarsValue.Text = ReadInt(row, "AvailableCars").ToString();
            lblRentedCarsValue.Text = ReadInt(row, "RentedCars").ToString();
            lblInactiveCarsValue.Text = ReadInt(row, "InactiveCars").ToString();
        }

        private void dgvCars_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex < 0)
            {
                return;
            }

            DataGridViewRow row = dgvCars.Rows[e.RowIndex];

            selectedCarId = Convert.ToInt32(row.Cells["CarId"].Value);

            lblSelectedCar.Text = "Selected Car: " + row.Cells["CarName"].Value;
            lblOwner.Text = "Owner: " + row.Cells["OwnerName"].Value;
            lblCarNumber.Text = "Car Number: " + row.Cells["CarNumber"].Value;

            string status = row.Cells["Status"].Value.ToString() ?? "Available";

            if (cmbStatus.Items.Contains(status))
            {
                cmbStatus.SelectedItem = status;
            }
        }

        private void btnUpdateStatus_Click(object sender, EventArgs e)
        {
            if (selectedCarId == 0)
            {
                MessageBox.Show("Please select a car first.");
                return;
            }

            string status = cmbStatus.SelectedItem.ToString() ?? "Available";

            bool success = adminService.UpdateCarStatus(selectedCarId, status, out string message);

            MessageBox.Show(message);

            if (success)
            {
                LoadCars();
            }
        }

        private void btnDelete_Click(object sender, EventArgs e)
        {
            if (selectedCarId == 0)
            {
                MessageBox.Show("Please select a car first.");
                return;
            }

            DialogResult result = MessageBox.Show(
                "Are you sure you want to remove this car?\n\nIf it has booking history, it will be marked as Unavailable instead.",
                "Confirm Remove",
                MessageBoxButtons.YesNo,
                MessageBoxIcon.Warning
            );

            if (result != DialogResult.Yes)
            {
                return;
            }

            bool success = adminService.DeleteCarSafe(selectedCarId, out string message);

            MessageBox.Show(message);

            if (success)
            {
                LoadCars();
            }
        }

        private void btnRefresh_Click(object sender, EventArgs e)
        {
            LoadCars();
        }

        private void ClearSelection()
        {
            selectedCarId = 0;
            lblSelectedCar.Text = "Selected Car: None";
            lblOwner.Text = "Owner: -";
            lblCarNumber.Text = "Car Number: -";

            if (cmbStatus.Items.Count > 0)
            {
                cmbStatus.SelectedIndex = 0;
            }

            dgvCars.ClearSelection();
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
