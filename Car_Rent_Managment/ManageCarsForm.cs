using Car_Rent_Managment.Models;
using Car_Rent_Managment.Services;
using System;
using System.Data;
using System.Windows.Forms;

namespace Car_Rent_Managment
{
    public partial class ManageCarsForm : Form
    {
        private AuthenticatedUser currentUser;
        private CarService carService;
        private int selectedCarId = 0;

        public ManageCarsForm(AuthenticatedUser user)
        {
            InitializeComponent();
            currentUser = user;
            carService = new CarService();

            lblInfo.Text = "Owner: " + currentUser.FullName;
            LoadCars();
        }

        private void LoadCars()
        {
            DataTable carsTable = carService.GetCarsByOwner(currentUser.UserId);
            dgvCars.DataSource = carsTable;

            if (dgvCars.Columns["CarId"] != null)
            {
                dgvCars.Columns["CarId"].Visible = false;
            }

            ClearForm();
        }

        private void btnAdd_Click(object sender, EventArgs e)
        {
            if (!ValidateInput(out int seats, out decimal price))
            {
                return;
            }

            Car car = new Car
            {
                OwnerId = currentUser.UserId,
                CarName = txtCarName.Text.Trim(),
                Brand = txtBrand.Text.Trim(),
                Model = txtModel.Text.Trim(),
                CarNumber = txtCarNumber.Text.Trim(),
                Seats = seats,
                PricePerDay = price,
                Location = txtLocation.Text.Trim(),
                Status = cmbStatus.SelectedItem.ToString(),
                Description = txtDescription.Text.Trim()
            };

            bool success = carService.AddCar(car, out string message);
            MessageBox.Show(message);

            if (success)
            {
                LoadCars();
            }
        }

        private void btnUpdate_Click(object sender, EventArgs e)
        {
            if (selectedCarId == 0)
            {
                MessageBox.Show("Please select a car to update.");
                return;
            }

            if (!ValidateInput(out int seats, out decimal price))
            {
                return;
            }

            Car car = new Car
            {
                CarId = selectedCarId,
                OwnerId = currentUser.UserId,
                CarName = txtCarName.Text.Trim(),
                Brand = txtBrand.Text.Trim(),
                Model = txtModel.Text.Trim(),
                CarNumber = txtCarNumber.Text.Trim(),
                Seats = seats,
                PricePerDay = price,
                Location = txtLocation.Text.Trim(),
                Status = cmbStatus.SelectedItem.ToString(),
                Description = txtDescription.Text.Trim()
            };

            bool success = carService.UpdateCar(car, out string message);
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
                MessageBox.Show("Please select a car to delete.");
                return;
            }

            DialogResult result = MessageBox.Show(
                "Are you sure you want to delete this car?",
                "Confirm Delete",
                MessageBoxButtons.YesNo,
                MessageBoxIcon.Warning
            );

            if (result != DialogResult.Yes)
            {
                return;
            }

            bool success = carService.DeleteCar(selectedCarId, currentUser.UserId, out string message);
            MessageBox.Show(message);

            if (success)
            {
                LoadCars();
            }
        }

        private void btnClear_Click(object sender, EventArgs e)
        {
            ClearForm();
        }

        private void btnRefresh_Click(object sender, EventArgs e)
        {
            LoadCars();
        }

        private void dgvCars_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex < 0)
            {
                return;
            }

            DataGridViewRow row = dgvCars.Rows[e.RowIndex];

            selectedCarId = Convert.ToInt32(row.Cells["CarId"].Value);
            txtCarName.Text = row.Cells["CarName"].Value.ToString();
            txtBrand.Text = row.Cells["Brand"].Value.ToString();
            txtModel.Text = row.Cells["Model"].Value.ToString();
            txtCarNumber.Text = row.Cells["CarNumber"].Value.ToString();
            txtSeats.Text = row.Cells["Seats"].Value.ToString();
            txtPricePerDay.Text = row.Cells["PricePerDay"].Value.ToString();
            txtLocation.Text = row.Cells["Location"].Value.ToString();
            cmbStatus.SelectedItem = row.Cells["Status"].Value.ToString();
            txtDescription.Text = row.Cells["Description"].Value.ToString();
        }

        private bool ValidateInput(out int seats, out decimal price)
        {
            seats = 0;
            price = 0;

            if (string.IsNullOrWhiteSpace(txtCarName.Text))
            {
                MessageBox.Show("Please enter car name.");
                txtCarName.Focus();
                return false;
            }

            if (string.IsNullOrWhiteSpace(txtBrand.Text))
            {
                MessageBox.Show("Please enter brand.");
                txtBrand.Focus();
                return false;
            }

            if (string.IsNullOrWhiteSpace(txtModel.Text))
            {
                MessageBox.Show("Please enter model.");
                txtModel.Focus();
                return false;
            }

            if (string.IsNullOrWhiteSpace(txtCarNumber.Text))
            {
                MessageBox.Show("Please enter car number.");
                txtCarNumber.Focus();
                return false;
            }

            if (!int.TryParse(txtSeats.Text.Trim(), out seats) || seats <= 0)
            {
                MessageBox.Show("Seats must be a valid positive number.");
                txtSeats.Focus();
                return false;
            }

            if (!decimal.TryParse(txtPricePerDay.Text.Trim(), out price) || price <= 0)
            {
                MessageBox.Show("Price per day must be a valid positive amount.");
                txtPricePerDay.Focus();
                return false;
            }

            if (string.IsNullOrWhiteSpace(txtLocation.Text))
            {
                MessageBox.Show("Please enter location.");
                txtLocation.Focus();
                return false;
            }

            if (cmbStatus.SelectedItem == null)
            {
                MessageBox.Show("Please select status.");
                cmbStatus.Focus();
                return false;
            }

            return true;
        }

        private void ClearForm()
        {
            selectedCarId = 0;

            txtCarName.Clear();
            txtBrand.Clear();
            txtModel.Clear();
            txtCarNumber.Clear();
            txtSeats.Clear();
            txtPricePerDay.Clear();
            txtLocation.Clear();
            txtDescription.Clear();

            if (cmbStatus.Items.Count > 0)
            {
                cmbStatus.SelectedIndex = 0;
            }

            dgvCars.ClearSelection();
        }
    }
}