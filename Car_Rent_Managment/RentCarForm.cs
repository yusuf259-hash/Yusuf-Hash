using Car_Rent_Managment.Models;
using Car_Rent_Managment.Services;
using Car_Rent_Managment.UI;
using System;
using System.Data;
using System.Drawing;
using System.Windows.Forms;

namespace Car_Rent_Managment
{
    public partial class RentCarForm : Form
    {
        private readonly AuthenticatedUser currentUser;
        private readonly BookingService bookingService;

        private int selectedCarId = 0;
        private string selectedCarName = "";
        private decimal selectedPricePerDay = 0;

        private decimal availableDiscountPercent = 0;
        private string availableOfferTitle = "No Offer";

        private decimal appliedDiscountPercent = 0;
        private string appliedOfferTitle = "No Offer";
        private bool isOfferApplied = false;

        public RentCarForm(AuthenticatedUser user)
        {
            InitializeComponent();

            currentUser = user;
            bookingService = new BookingService();

            lblCustomer.Text = "Customer: " + currentUser.FullName;

            cmbSeats.Items.Add("Any");
            cmbSeats.Items.Add("2");
            cmbSeats.Items.Add("4");
            cmbSeats.Items.Add("5");
            cmbSeats.Items.Add("7");
            cmbSeats.SelectedIndex = 0;

            dtpRentDate.Value = DateTime.Today;
            dtpRentDate.MinDate = DateTime.Today;

            dtpReturnDate.Value = DateTime.Today.AddDays(1);
            dtpReturnDate.MinDate = DateTime.Today.AddDays(1);

            LoadAvailableCars();
        }

        private void LoadAvailableCars()
        {
            string location = txtLocationFilter.Text.Trim();

            int minSeats = 0;
            if (cmbSeats.SelectedItem != null && cmbSeats.SelectedItem.ToString() != "Any")
            {
                int.TryParse(cmbSeats.SelectedItem.ToString(), out minSeats);
            }

            decimal minPrice = 0;
            decimal maxPrice = 0;

            if (!string.IsNullOrWhiteSpace(txtMinPrice.Text) && !decimal.TryParse(txtMinPrice.Text.Trim(), out minPrice))
            {
                MessageBox.Show("Minimum price must be a valid number.");
                txtMinPrice.Focus();
                return;
            }

            if (!string.IsNullOrWhiteSpace(txtMaxPrice.Text) && !decimal.TryParse(txtMaxPrice.Text.Trim(), out maxPrice))
            {
                MessageBox.Show("Maximum price must be a valid number.");
                txtMaxPrice.Focus();
                return;
            }

            DataTable carsTable = bookingService.GetAvailableCarsFiltered(location, minSeats, minPrice, maxPrice);
            RenderCarCards(carsTable);
            ClearSelection();
        }

        private void RenderCarCards(DataTable carsTable)
        {
            flpCars.Controls.Clear();

            if (carsTable.Rows.Count == 0)
            {
                Label emptyLabel = new Label();
                emptyLabel.Text = "No available cars found. Try changing the filters.";
                emptyLabel.Font = new Font("Segoe UI", 12F, FontStyle.Bold);
                emptyLabel.ForeColor = UiTheme.TextMuted;
                emptyLabel.AutoSize = true;
                emptyLabel.Margin = new Padding(20);
                flpCars.Controls.Add(emptyLabel);
                return;
            }

            foreach (DataRow row in carsTable.Rows)
            {
                Panel card = CreateCarCard(row);
                flpCars.Controls.Add(card);
            }
        }

        private Panel CreateCarCard(DataRow row)
        {
            int carId = Convert.ToInt32(row["CarId"]);
            string carName = row["CarName"].ToString() ?? "";
            string brand = row["Brand"].ToString() ?? "";
            string model = row["Model"].ToString() ?? "";
            string carNumber = row["CarNumber"].ToString() ?? "";
            string seats = row["Seats"].ToString() ?? "";
            decimal price = Convert.ToDecimal(row["PricePerDay"]);
            string location = row["Location"].ToString() ?? "";
            string ownerName = row["OwnerName"].ToString() ?? "";
            string description = row["Description"].ToString() ?? "";

            string offerTitle = row.Table.Columns.Contains("OfferTitle")
                ? row["OfferTitle"].ToString() ?? "No Offer"
                : "No Offer";

            decimal discountPercent = 0;

            if (row.Table.Columns.Contains("DiscountPercent") && row["DiscountPercent"] != DBNull.Value)
            {
                discountPercent = Convert.ToDecimal(row["DiscountPercent"]);
            }

            Panel card = new Panel();
            card.BackColor = Color.White;
            card.BorderStyle = BorderStyle.FixedSingle;
            card.Size = new Size(260, 285);
            card.Margin = new Padding(12);

            Panel topBar = new Panel();
            topBar.BackColor = discountPercent > 0 ? UiTheme.Success : UiTheme.Primary;
            topBar.Dock = DockStyle.Top;
            topBar.Height = 8;

            Label lblCarName = new Label();
            lblCarName.Text = carName;
            lblCarName.Font = new Font("Segoe UI", 14F, FontStyle.Bold);
            lblCarName.ForeColor = UiTheme.TextDark;
            lblCarName.AutoSize = false;
            lblCarName.Location = new Point(15, 22);
            lblCarName.Size = new Size(220, 30);

            Label lblModel = new Label();
            lblModel.Text = brand + " • " + model;
            lblModel.Font = new Font("Segoe UI", 9F);
            lblModel.ForeColor = UiTheme.TextMuted;
            lblModel.AutoSize = false;
            lblModel.Location = new Point(15, 55);
            lblModel.Size = new Size(220, 22);

            Label lblMeta = new Label();
            lblMeta.Text =
                "Seats: " + seats + "\n" +
                "Location: " + location + "\n" +
                "Owner: " + ownerName + "\n" +
                "No: " + carNumber;
            lblMeta.Font = new Font("Segoe UI", 9F);
            lblMeta.ForeColor = UiTheme.TextDark;
            lblMeta.AutoSize = false;
            lblMeta.Location = new Point(15, 84);
            lblMeta.Size = new Size(225, 70);

            Label lblDescription = new Label();
            lblDescription.Text = description;
            lblDescription.Font = new Font("Segoe UI", 8F);
            lblDescription.ForeColor = UiTheme.TextMuted;
            lblDescription.AutoSize = false;
            lblDescription.Location = new Point(15, 152);
            lblDescription.Size = new Size(225, 32);

            Label lblOffer = new Label();
            lblOffer.Font = new Font("Segoe UI", 9F, FontStyle.Bold);
            lblOffer.AutoSize = false;
            lblOffer.Location = new Point(15, 190);
            lblOffer.Size = new Size(225, 24);

            if (discountPercent > 0)
            {
                lblOffer.Text = "Available: " + offerTitle + " - " + discountPercent.ToString("0") + "% OFF";
                lblOffer.ForeColor = UiTheme.Success;
            }
            else
            {
                lblOffer.Text = "No active discount";
                lblOffer.ForeColor = UiTheme.TextMuted;
            }

            Label lblPrice = new Label();
            lblPrice.Text = price.ToString("0.00") + " BDT / day";
            lblPrice.Font = new Font("Segoe UI", 11F, FontStyle.Bold);
            lblPrice.ForeColor = UiTheme.Primary;
            lblPrice.AutoSize = false;
            lblPrice.Location = new Point(15, 226);
            lblPrice.Size = new Size(135, 28);

            Button btnSelect = new Button();
            btnSelect.Text = "Select";
            btnSelect.Size = new Size(80, 32);
            btnSelect.Location = new Point(160, 222);
            UiTheme.StylePrimaryButton(btnSelect);

            btnSelect.Click += (sender, e) =>
            {
                selectedCarId = carId;
                selectedCarName = carName;
                selectedPricePerDay = price;

                availableDiscountPercent = discountPercent;
                availableOfferTitle = offerTitle;

                appliedDiscountPercent = 0;
                appliedOfferTitle = "No Offer";
                isOfferApplied = false;

                lblSelectedCar.Text = "Selected Car: " + selectedCarName;
                lblPricePerDay.Text = "Price Per Day: " + selectedPricePerDay.ToString("0.00") + " BDT";

                if (availableDiscountPercent > 0)
                {
                    lblOfferInfo.Text = "Available Offer: " + availableOfferTitle + " (" + availableDiscountPercent.ToString("0") + "% OFF)";
                }
                else
                {
                    lblOfferInfo.Text = "Available Offer: None";
                }

                HighlightSelectedCard(card);
                CalculateTotal();
            };

            card.Controls.Add(topBar);
            card.Controls.Add(lblCarName);
            card.Controls.Add(lblModel);
            card.Controls.Add(lblMeta);
            card.Controls.Add(lblDescription);
            card.Controls.Add(lblOffer);
            card.Controls.Add(lblPrice);
            card.Controls.Add(btnSelect);

            return card;
        }

        private void HighlightSelectedCard(Panel selectedCard)
        {
            foreach (Control control in flpCars.Controls)
            {
                if (control is Panel panel)
                {
                    panel.BackColor = Color.White;
                }
            }

            selectedCard.BackColor = Color.FromArgb(239, 246, 255);
        }

        private void DateChanged(object sender, EventArgs e)
        {
            if (dtpReturnDate.Value.Date <= dtpRentDate.Value.Date)
            {
                dtpReturnDate.Value = dtpRentDate.Value.Date.AddDays(1);
            }

            CalculateTotal();
        }

        private void btnApplyOffer_Click(object sender, EventArgs e)
        {
            if (selectedCarId == 0)
            {
                MessageBox.Show("Please select a car first.");
                return;
            }

            if (availableDiscountPercent <= 0)
            {
                MessageBox.Show("No active offer is available for this car.");
                return;
            }

            appliedDiscountPercent = availableDiscountPercent;
            appliedOfferTitle = availableOfferTitle;
            isOfferApplied = true;

            lblOfferInfo.Text = "Applied Offer: " + appliedOfferTitle + " (" + appliedDiscountPercent.ToString("0") + "% OFF)";
            CalculateTotal();
        }

        private void btnRemoveOffer_Click(object sender, EventArgs e)
        {
            if (selectedCarId == 0)
            {
                MessageBox.Show("Please select a car first.");
                return;
            }

            appliedDiscountPercent = 0;
            appliedOfferTitle = "No Offer";
            isOfferApplied = false;

            if (availableDiscountPercent > 0)
            {
                lblOfferInfo.Text = "Available Offer: " + availableOfferTitle + " (" + availableDiscountPercent.ToString("0") + "% OFF)";
            }
            else
            {
                lblOfferInfo.Text = "Available Offer: None";
            }

            CalculateTotal();
        }

        private void CalculateTotal()
        {
            if (selectedCarId == 0 || selectedPricePerDay <= 0)
            {
                lblTotalAmount.Text = "Original Total: 0 BDT";
                lblDiscountAmount.Text = "Discount: 0 BDT";
                lblPayableAmount.Text = "Payable: 0 BDT";
                return;
            }

            int days = (dtpReturnDate.Value.Date - dtpRentDate.Value.Date).Days;

            if (days <= 0)
            {
                days = 1;
            }

            decimal total = days * selectedPricePerDay;
            decimal discountAmount = isOfferApplied ? total * (appliedDiscountPercent / 100m) : 0;
            decimal payableAmount = total - discountAmount;

            lblTotalAmount.Text = "Original Total: " + total.ToString("0.00") + " BDT";
            lblDiscountAmount.Text = "Discount: " + discountAmount.ToString("0.00") + " BDT";
            lblPayableAmount.Text = "Payable: " + payableAmount.ToString("0.00") + " BDT";
        }

        private void btnRent_Click(object sender, EventArgs e)
        {
            if (selectedCarId == 0)
            {
                MessageBox.Show("Please select a car first.");
                return;
            }

            DateTime rentDate = dtpRentDate.Value.Date;
            DateTime expectedReturnDate = dtpReturnDate.Value.Date;

            if (expectedReturnDate <= rentDate)
            {
                MessageBox.Show("Expected return date must be after rent date.");
                return;
            }

            int days = (expectedReturnDate - rentDate).Days;
            decimal totalAmount = days * selectedPricePerDay;
            decimal discountAmount = isOfferApplied ? totalAmount * (appliedDiscountPercent / 100m) : 0;
            decimal payableAmount = totalAmount - discountAmount;

            DialogResult result = MessageBox.Show(
                "Confirm booking for " + selectedCarName + "?\n\n" +
                "Rent Date: " + rentDate.ToShortDateString() + "\n" +
                "Return Date: " + expectedReturnDate.ToShortDateString() + "\n" +
                "Original Total: " + totalAmount.ToString("0.00") + " BDT\n" +
                "Applied Offer: " + appliedOfferTitle + "\n" +
                "Discount: " + discountAmount.ToString("0.00") + " BDT\n" +
                "Payable Amount: " + payableAmount.ToString("0.00") + " BDT",
                "Confirm Booking",
                MessageBoxButtons.YesNo,
                MessageBoxIcon.Question
            );

            if (result != DialogResult.Yes)
            {
                return;
            }

            bool success = bookingService.RentCar(
                currentUser.UserId,
                selectedCarId,
                rentDate,
                expectedReturnDate,
                totalAmount,
                discountAmount,
                payableAmount,
                out string message
            );

            MessageBox.Show(message);

            if (success)
            {
                LoadAvailableCars();
            }
        }

        private void btnApplyFilter_Click(object sender, EventArgs e)
        {
            LoadAvailableCars();
        }

        private void btnClearFilter_Click(object sender, EventArgs e)
        {
            txtLocationFilter.Clear();
            txtMinPrice.Clear();
            txtMaxPrice.Clear();

            if (cmbSeats.Items.Count > 0)
            {
                cmbSeats.SelectedIndex = 0;
            }

            LoadAvailableCars();
        }

        private void btnRefresh_Click(object sender, EventArgs e)
        {
            LoadAvailableCars();
        }

        private void ClearSelection()
        {
            selectedCarId = 0;
            selectedCarName = "";
            selectedPricePerDay = 0;

            availableDiscountPercent = 0;
            availableOfferTitle = "No Offer";

            appliedDiscountPercent = 0;
            appliedOfferTitle = "No Offer";
            isOfferApplied = false;

            lblSelectedCar.Text = "Selected Car: None";
            lblPricePerDay.Text = "Price Per Day: 0";
            lblOfferInfo.Text = "Available Offer: -";
            lblTotalAmount.Text = "Original Total: 0 BDT";
            lblDiscountAmount.Text = "Discount: 0 BDT";
            lblPayableAmount.Text = "Payable: 0 BDT";
        }
    }
}