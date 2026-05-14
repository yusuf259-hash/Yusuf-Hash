using Car_Rent_Managment.Models;
using Car_Rent_Managment.Services;
using Car_Rent_Managment.UI;
using System;
using System.Data;
using System.Drawing;
using System.IO;
using System.Windows.Forms;

namespace Car_Rent_Managment
{
    public partial class PaymentReturnForm : Form
    {
        private readonly AuthenticatedUser currentUser;
        private readonly CustomerFeatureService customerFeatureService;
        private readonly BookingService bookingService;

        private int selectedPaymentBookingId = 0;
        private decimal selectedPaymentAmount = 0;
        private string selectedPaymentMethod = "bKash";

        private int selectedReturnBookingId = 0;
        private string selectedReturnCarName = "";
        private DateTime selectedRentDate = DateTime.Today;
        private DateTime selectedExpectedReturnDate = DateTime.Today;

        public PaymentReturnForm(AuthenticatedUser user)
        {
            InitializeComponent();

            currentUser = user;
            customerFeatureService = new CustomerFeatureService();
            bookingService = new BookingService();

            lblCustomer.Text = "Customer: " + currentUser.FullName;

            RenderPaymentMethodCards();
            GenerateTransactionId();

            LoadPaymentBookings();
            LoadReturnBookings();
        }

        private void LoadPaymentBookings()
        {
            DataTable table = customerFeatureService.GetUnpaidBookings(currentUser.UserId);
            RenderPaymentCards(table);
            ClearPaymentSelection();
        }

        private void LoadReturnBookings()
        {
            DataTable table = bookingService.GetActiveBookingsByCustomer(currentUser.UserId);
            DataTable paidTable = table.Clone();

            foreach (DataRow row in table.Rows)
            {
                string paymentStatus = row["PaymentStatus"].ToString() ?? "";

                if (paymentStatus == "Paid")
                {
                    paidTable.ImportRow(row);
                }
            }

            RenderReturnCards(paidTable);
            ClearReturnSelection();
        }

        private void RenderPaymentCards(DataTable table)
        {
            flpPayments.Controls.Clear();

            if (table.Rows.Count == 0)
            {
                flpPayments.Controls.Add(CreateEmptyLabel("No unpaid booking found."));
                return;
            }

            foreach (DataRow row in table.Rows)
            {
                flpPayments.Controls.Add(CreatePaymentCard(row));
            }
        }

        private Panel CreatePaymentCard(DataRow row)
        {
            int bookingId = Convert.ToInt32(row["BookingId"]);
            string carName = row["CarName"].ToString() ?? "";
            string brand = row["Brand"].ToString() ?? "";
            string model = row["Model"].ToString() ?? "";
            DateTime rentDate = Convert.ToDateTime(row["RentDate"]);
            DateTime expectedReturnDate = Convert.ToDateTime(row["ExpectedReturnDate"]);

            decimal totalAmount = ToDecimalSafe(row["TotalAmount"]);
            decimal discountAmount = ToDecimalSafe(row["DiscountAmount"]);
            decimal payableAmount = ToDecimalSafe(row["PayableAmount"]);

            Panel card = new Panel();
            card.BackColor = Color.White;
            card.BorderStyle = BorderStyle.FixedSingle;
            card.Size = new Size(345, 185);
            card.Margin = new Padding(8);

            Panel topBar = new Panel();
            topBar.BackColor = UiTheme.Warning;
            topBar.Dock = DockStyle.Top;
            topBar.Height = 7;

            Label lblBooking = new Label();
            lblBooking.Text = "Booking #" + bookingId;
            lblBooking.Font = new Font("Segoe UI", 13F, FontStyle.Bold);
            lblBooking.ForeColor = UiTheme.TextDark;
            lblBooking.Location = new Point(15, 18);
            lblBooking.Size = new Size(300, 26);

            Label lblCarName = new Label();
            lblCarName.Text = carName;
            lblCarName.Font = new Font("Segoe UI", 11F, FontStyle.Bold);
            lblCarName.ForeColor = UiTheme.TextDark;
            lblCarName.Location = new Point(15, 48);
            lblCarName.Size = new Size(300, 24);

            Label lblModel = new Label();
            lblModel.Text = brand + " • " + model;
            lblModel.Font = UiTheme.SmallFont();
            lblModel.ForeColor = UiTheme.TextMuted;
            lblModel.Location = new Point(15, 72);
            lblModel.Size = new Size(300, 20);

            Label lblDates = new Label();
            lblDates.Text =
                "Rent: " + rentDate.ToShortDateString() + "\n" +
                "Return: " + expectedReturnDate.ToShortDateString();
            lblDates.Font = UiTheme.SmallFont();
            lblDates.ForeColor = UiTheme.TextDark;
            lblDates.Location = new Point(15, 98);
            lblDates.Size = new Size(210, 40);

            Label lblMoney = new Label();
            lblMoney.Text =
                "Original: " + totalAmount.ToString("0.00") + " BDT\n" +
                "Discount: " + discountAmount.ToString("0.00") + " BDT\n" +
                "Payable: " + payableAmount.ToString("0.00") + " BDT";
            lblMoney.Font = new Font("Segoe UI", 8F, FontStyle.Bold);
            lblMoney.ForeColor = UiTheme.Primary;
            lblMoney.Location = new Point(15, 138);
            lblMoney.Size = new Size(215, 45);

            Button btnSelect = new Button();
            btnSelect.Text = "Select";
            btnSelect.Size = new Size(82, 32);
            btnSelect.Location = new Point(245, 136);
            UiTheme.StylePrimaryButton(btnSelect);

            btnSelect.Click += (sender, e) =>
            {
                selectedPaymentBookingId = bookingId;
                selectedPaymentAmount = payableAmount;
                btnPay.Enabled = true;

                this.lblSelectedPayment.Text = "Selected Booking: #" + selectedPaymentBookingId;
                this.lblPaymentCar.Text = "Car: " + carName;
                this.lblPaymentAmount.Text = "Amount: " + selectedPaymentAmount.ToString("0.00") + " BDT";

                GenerateTransactionId();
                HighlightSelectedCard(flpPayments, card);
            };

            card.Controls.Add(topBar);
            card.Controls.Add(lblBooking);
            card.Controls.Add(lblCarName);
            card.Controls.Add(lblModel);
            card.Controls.Add(lblDates);
            card.Controls.Add(lblMoney);
            card.Controls.Add(btnSelect);

            return card;
        }

        private void RenderReturnCards(DataTable table)
        {
            flpReturns.Controls.Clear();

            if (table.Rows.Count == 0)
            {
                flpReturns.Controls.Add(CreateEmptyLabel("No paid active booking ready for return."));
                return;
            }

            foreach (DataRow row in table.Rows)
            {
                flpReturns.Controls.Add(CreateReturnCard(row));
            }
        }

        private Panel CreateReturnCard(DataRow row)
        {
            int bookingId = Convert.ToInt32(row["BookingId"]);
            string carName = row["CarName"].ToString() ?? "";
            string brand = row["Brand"].ToString() ?? "";
            string model = row["Model"].ToString() ?? "";
            string carNumber = row["CarNumber"].ToString() ?? "";
            DateTime rentDate = Convert.ToDateTime(row["RentDate"]);
            DateTime expectedReturnDate = Convert.ToDateTime(row["ExpectedReturnDate"]);
            decimal payableAmount = ToDecimalSafe(row["PayableAmount"]);

            Panel card = new Panel();
            card.BackColor = Color.White;
            card.BorderStyle = BorderStyle.FixedSingle;
            card.Size = new Size(370, 165);
            card.Margin = new Padding(8);

            Panel topBar = new Panel();
            topBar.BackColor = UiTheme.Primary;
            topBar.Dock = DockStyle.Top;
            topBar.Height = 7;

            Label lblBooking = new Label();
            lblBooking.Text = "Booking #" + bookingId;
            lblBooking.Font = new Font("Segoe UI", 13F, FontStyle.Bold);
            lblBooking.ForeColor = UiTheme.TextDark;
            lblBooking.Location = new Point(15, 18);
            lblBooking.Size = new Size(330, 26);

            Label lblCarName = new Label();
            lblCarName.Text = carName;
            lblCarName.Font = new Font("Segoe UI", 11F, FontStyle.Bold);
            lblCarName.ForeColor = UiTheme.TextDark;
            lblCarName.Location = new Point(15, 48);
            lblCarName.Size = new Size(330, 24);

            Label lblModel = new Label();
            lblModel.Text = brand + " • " + model + " • " + carNumber;
            lblModel.Font = UiTheme.SmallFont();
            lblModel.ForeColor = UiTheme.TextMuted;
            lblModel.Location = new Point(15, 72);
            lblModel.Size = new Size(330, 20);

            Label lblDates = new Label();
            lblDates.Text =
                "Rent: " + rentDate.ToShortDateString() + "\n" +
                "Expected Return: " + expectedReturnDate.ToShortDateString() + "\n" +
                "Paid: " + payableAmount.ToString("0.00") + " BDT";
            lblDates.Font = UiTheme.SmallFont();
            lblDates.ForeColor = UiTheme.TextDark;
            lblDates.Location = new Point(15, 96);
            lblDates.Size = new Size(230, 58);

            Button btnSelect = new Button();
            btnSelect.Text = "Select";
            btnSelect.Size = new Size(82, 32);
            btnSelect.Location = new Point(265, 118);
            UiTheme.StylePrimaryButton(btnSelect);

            btnSelect.Click += (sender, e) =>
            {
                selectedReturnBookingId = bookingId;
                selectedReturnCarName = carName;
                selectedRentDate = rentDate;
                selectedExpectedReturnDate = expectedReturnDate;
                btnReturn.Enabled = true;

                this.lblSelectedReturn.Text = "Selected Booking: #" + selectedReturnBookingId;
                this.lblReturnCar.Text = "Car: " + selectedReturnCarName;
                this.lblExpectedReturn.Text = "Expected Return: " + selectedExpectedReturnDate.ToShortDateString();

                dtpActualReturnDate.MinDate = selectedRentDate.Date;

                if (dtpActualReturnDate.Value.Date < selectedRentDate.Date)
                {
                    dtpActualReturnDate.Value = selectedRentDate.Date;
                }

                CalculateFinePreview();
                HighlightSelectedCard(flpReturns, card);
            };

            card.Controls.Add(topBar);
            card.Controls.Add(lblBooking);
            card.Controls.Add(lblCarName);
            card.Controls.Add(lblModel);
            card.Controls.Add(lblDates);
            card.Controls.Add(btnSelect);

            return card;
        }

        private void RenderPaymentMethodCards()
        {
            flpPaymentMethods.Controls.Clear();

            AddPaymentMethodCard("bKash", "bkash.png");
            AddPaymentMethodCard("Nagad", "nagad.png");
            AddPaymentMethodCard("Rocket", "rocket.png");
            AddPaymentMethodCard("Card", "card.png");
            AddPaymentMethodCard("Cash", "cash.png");
        }

        private void AddPaymentMethodCard(string methodValue, string fileName)
        {
            Panel card = new Panel();
            card.BackColor = methodValue == selectedPaymentMethod
                ? Color.FromArgb(219, 234, 254)
                : Color.White;

            card.BorderStyle = BorderStyle.FixedSingle;
            card.Size = new Size(108, 52);
            card.Margin = new Padding(5);
            card.Cursor = Cursors.Hand;

            PictureBox picLogo = new PictureBox();
            picLogo.Size = new Size(70, 24);
            picLogo.Location = new Point(19, 5);
            picLogo.SizeMode = PictureBoxSizeMode.Zoom;
            picLogo.Cursor = Cursors.Hand;

            string logoPath = ResolveLogoPath(fileName);

            if (!string.IsNullOrWhiteSpace(logoPath) && File.Exists(logoPath))
            {
                try
                {
                    using (FileStream fs = new FileStream(logoPath, FileMode.Open, FileAccess.Read))
                    {
                        picLogo.Image = Image.FromStream(fs);
                    }
                }
                catch
                {
                    picLogo.Image = null;
                }
            }

            Label lblName = new Label();
            lblName.Text = methodValue;
            lblName.Font = new Font("Segoe UI", 9F, FontStyle.Bold);
            lblName.ForeColor = UiTheme.TextDark;
            lblName.TextAlign = ContentAlignment.MiddleCenter;
            lblName.Location = new Point(5, 31);
            lblName.Size = new Size(98, 18);
            lblName.Cursor = Cursors.Hand;

            card.Click += (sender, e) => SelectPaymentMethod(methodValue);
            picLogo.Click += (sender, e) => SelectPaymentMethod(methodValue);
            lblName.Click += (sender, e) => SelectPaymentMethod(methodValue);

            card.Controls.Add(picLogo);
            card.Controls.Add(lblName);

            flpPaymentMethods.Controls.Add(card);
        }

        private string ResolveLogoPath(string fileName)
        {
            string baseDirectory = AppDomain.CurrentDomain.BaseDirectory;

            string directOutputPath = Path.Combine(baseDirectory, "Assets", fileName);
            if (File.Exists(directOutputPath))
            {
                return directOutputPath;
            }

            string paymentLogoOutputPath = Path.Combine(baseDirectory, "Assets", "PaymentLogos", fileName);
            if (File.Exists(paymentLogoOutputPath))
            {
                return paymentLogoOutputPath;
            }

            DirectoryInfo? directory = new DirectoryInfo(baseDirectory);

            for (int i = 0; i < 8 && directory != null; i++)
            {
                string projectAssetsPath = Path.Combine(directory.FullName, "Assets", fileName);
                if (File.Exists(projectAssetsPath))
                {
                    return projectAssetsPath;
                }

                string projectPaymentLogosPath = Path.Combine(directory.FullName, "Assets", "PaymentLogos", fileName);
                if (File.Exists(projectPaymentLogosPath))
                {
                    return projectPaymentLogosPath;
                }

                directory = directory.Parent;
            }

            return "";
        }

        private void SelectPaymentMethod(string method)
        {
            selectedPaymentMethod = method;
            lblSelectedMethod.Text = "Method: " + selectedPaymentMethod;
            GenerateTransactionId();
            RenderPaymentMethodCards();
        }

        private void GenerateTransactionId()
        {
            string prefix;

            if (selectedPaymentMethod == "bKash")
            {
                prefix = "BK";
            }
            else if (selectedPaymentMethod == "Nagad")
            {
                prefix = "NG";
            }
            else if (selectedPaymentMethod == "Rocket")
            {
                prefix = "RK";
            }
            else if (selectedPaymentMethod == "Card")
            {
                prefix = "CD";
            }
            else
            {
                prefix = "CS";
            }

            string bookingPart = selectedPaymentBookingId > 0
                ? selectedPaymentBookingId.ToString("0000")
                : "0000";

            string timePart = DateTime.Now.ToString("yyMMddHHmmss");

            txtTransactionNumber.Text = prefix + "-" + bookingPart + "-" + timePart;
        }

        private void btnPay_Click(object sender, EventArgs e)
        {
            if (selectedPaymentBookingId == 0)
            {
                MessageBox.Show("Please select an unpaid booking first.");
                return;
            }

            GenerateTransactionId();

            DialogResult result = MessageBox.Show(
                "Confirm payment?\n\n" +
                "Booking: #" + selectedPaymentBookingId + "\n" +
                "Method: " + selectedPaymentMethod + "\n" +
                "Transaction ID: " + txtTransactionNumber.Text + "\n" +
                "Amount: " + selectedPaymentAmount.ToString("0.00") + " BDT",
                "Confirm Payment",
                MessageBoxButtons.YesNo,
                MessageBoxIcon.Question
            );

            if (result != DialogResult.Yes)
            {
                return;
            }

            bool success = customerFeatureService.MakePayment(
                selectedPaymentBookingId,
                currentUser.UserId,
                selectedPaymentAmount,
                selectedPaymentMethod,
                txtTransactionNumber.Text.Trim(),
                out string message
            );

            MessageBox.Show(message);

            if (success)
            {
                LoadPaymentBookings();
                LoadReturnBookings();
            }
        }

        private void btnReturn_Click(object sender, EventArgs e)
        {
            if (selectedReturnBookingId == 0)
            {
                MessageBox.Show("Please select a paid active booking first.");
                return;
            }

            DateTime actualReturnDate = dtpActualReturnDate.Value.Date;

            DialogResult result = MessageBox.Show(
                "Confirm return?\n\n" +
                "Booking: #" + selectedReturnBookingId + "\n" +
                "Car: " + selectedReturnCarName + "\n" +
                "Actual Return Date: " + actualReturnDate.ToShortDateString() + "\n" +
                lblLateDays.Text + "\n" +
                lblFinePreview.Text,
                "Confirm Return",
                MessageBoxButtons.YesNo,
                MessageBoxIcon.Question
            );

            if (result != DialogResult.Yes)
            {
                return;
            }

            bool success = bookingService.ReturnCar(
                currentUser.UserId,
                selectedReturnBookingId,
                actualReturnDate,
                out int lateDays,
                out decimal fineAmount,
                out string message
            );

            MessageBox.Show(message);

            if (success)
            {
                LoadPaymentBookings();
                LoadReturnBookings();
            }
        }

        private void dtpActualReturnDate_ValueChanged(object sender, EventArgs e)
        {
            CalculateFinePreview();
        }

        private void CalculateFinePreview()
        {
            if (selectedReturnBookingId == 0)
            {
                lblLateDays.Text = "Late Days: 0";
                lblFinePreview.Text = "Fine Preview: 0 BDT";
                return;
            }

            int lateDays = (dtpActualReturnDate.Value.Date - selectedExpectedReturnDate.Date).Days;

            if (lateDays < 0)
            {
                lateDays = 0;
            }

            decimal fineAmount = lateDays * 500m;

            lblLateDays.Text = "Late Days: " + lateDays;
            lblFinePreview.Text = "Fine Preview: " + fineAmount.ToString("0.00") + " BDT";
        }

        private void btnRefresh_Click(object sender, EventArgs e)
        {
            LoadPaymentBookings();
            LoadReturnBookings();
        }

        private void HighlightSelectedCard(FlowLayoutPanel flowPanel, Panel selectedCard)
        {
            foreach (Control control in flowPanel.Controls)
            {
                if (control is Panel panel)
                {
                    panel.BackColor = Color.White;
                }
            }

            selectedCard.BackColor = Color.FromArgb(239, 246, 255);
        }

        private Label CreateEmptyLabel(string text)
        {
            Label emptyLabel = new Label();
            emptyLabel.Text = text;
            emptyLabel.Font = new Font("Segoe UI", 11F, FontStyle.Bold);
            emptyLabel.ForeColor = UiTheme.TextMuted;
            emptyLabel.AutoSize = false;
            emptyLabel.TextAlign = ContentAlignment.MiddleCenter;
            emptyLabel.BackColor = Color.White;
            emptyLabel.Size = new Size(700, 130);
            emptyLabel.Margin = new Padding(18);
            return emptyLabel;
        }

        private decimal ToDecimalSafe(object value)
        {
            if (value == null || value == DBNull.Value)
            {
                return 0;
            }

            return Convert.ToDecimal(value);
        }

        private void ClearPaymentSelection()
        {
            selectedPaymentBookingId = 0;
            selectedPaymentAmount = 0;

            lblSelectedPayment.Text = "Selected Booking: None";
            lblPaymentCar.Text = "Car: -";
            lblPaymentAmount.Text = "Amount: 0 BDT";
            lblSelectedMethod.Text = "Method: " + selectedPaymentMethod;
            btnPay.Enabled = false;

            GenerateTransactionId();
        }

        private void ClearReturnSelection()
        {
            selectedReturnBookingId = 0;
            selectedReturnCarName = "";
            selectedRentDate = DateTime.Today;
            selectedExpectedReturnDate = DateTime.Today;

            lblSelectedReturn.Text = "Selected Booking: None";
            lblReturnCar.Text = "Car: -";
            lblExpectedReturn.Text = "Expected Return: -";
            lblLateDays.Text = "Late Days: 0";
            lblFinePreview.Text = "Fine Preview: 0 BDT";
            btnReturn.Enabled = false;

            dtpActualReturnDate.MinDate = new DateTime(2020, 1, 1);
            dtpActualReturnDate.Value = DateTime.Today;
        }

        private void lblSelectedMethod_Click(object sender, EventArgs e)
        {

        }

        private void paymentDetailsPanel_Paint(object sender, PaintEventArgs e)
        {

        }
    }
}
