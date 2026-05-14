using Car_Rent_Managment.UI;
using System;
using System.Drawing;
using System.Windows.Forms;

namespace Car_Rent_Managment
{
    partial class RentCarForm
    {
        private System.ComponentModel.IContainer components = null;

        private Label lblTitle;
        private Label lblCustomer;

        private Panel filterPanel;
        private Label lblFilterTitle;
        private Label lblLocationFilter;
        private TextBox txtLocationFilter;
        private Label lblSeatsFilter;
        private ComboBox cmbSeats;
        private Label lblMinPrice;
        private TextBox txtMinPrice;
        private Label lblMaxPrice;
        private TextBox txtMaxPrice;
        private Button btnApplyFilter;
        private Button btnClearFilter;

        private Button btnRefresh;
        private FlowLayoutPanel flpCars;

        private Panel bookingPanel;
        private Label lblPanelTitle;
        private Label lblSelectedCar;
        private Label lblPricePerDay;
        private Label lblOfferInfo;
        private Button btnApplyOffer;
        private Button btnRemoveOffer;
        private Label lblRentDate;
        private DateTimePicker dtpRentDate;
        private Label lblReturnDate;
        private DateTimePicker dtpReturnDate;
        private Label lblTotalAmount;
        private Label lblDiscountAmount;
        private Label lblPayableAmount;
        private Button btnRent;

        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }

            base.Dispose(disposing);
        }

        private void InitializeComponent()
        {
            lblTitle = new Label();
            lblCustomer = new Label();

            filterPanel = new Panel();
            lblFilterTitle = new Label();
            lblLocationFilter = new Label();
            txtLocationFilter = new TextBox();
            lblSeatsFilter = new Label();
            cmbSeats = new ComboBox();
            lblMinPrice = new Label();
            txtMinPrice = new TextBox();
            lblMaxPrice = new Label();
            txtMaxPrice = new TextBox();
            btnApplyFilter = new Button();
            btnClearFilter = new Button();

            btnRefresh = new Button();
            flpCars = new FlowLayoutPanel();

            bookingPanel = new Panel();
            lblPanelTitle = new Label();
            lblSelectedCar = new Label();
            lblPricePerDay = new Label();
            lblOfferInfo = new Label();
            btnApplyOffer = new Button();
            btnRemoveOffer = new Button();
            lblRentDate = new Label();
            dtpRentDate = new DateTimePicker();
            lblReturnDate = new Label();
            dtpReturnDate = new DateTimePicker();
            lblTotalAmount = new Label();
            lblDiscountAmount = new Label();
            lblPayableAmount = new Label();
            btnRent = new Button();

            filterPanel.SuspendLayout();
            bookingPanel.SuspendLayout();
            SuspendLayout();

            BackColor = UiTheme.Background;
            ClientSize = new Size(1220, 720);
            StartPosition = FormStartPosition.CenterScreen;
            Text = "Rent a Car";

            lblTitle.Text = "Available Cars";
            lblTitle.Font = UiTheme.TitleFont();
            lblTitle.ForeColor = UiTheme.TextDark;
            lblTitle.AutoSize = true;
            lblTitle.Location = new Point(30, 25);

            lblCustomer.Text = "Customer:";
            lblCustomer.Font = UiTheme.NormalFont();
            lblCustomer.ForeColor = UiTheme.TextMuted;
            lblCustomer.AutoSize = true;
            lblCustomer.Location = new Point(35, 75);

            filterPanel.Location = new Point(30, 110);
            filterPanel.Size = new Size(830, 100);
            UiTheme.StylePanel(filterPanel);

            lblFilterTitle.Text = "Find your car";
            lblFilterTitle.Font = new Font("Segoe UI", 12F, FontStyle.Bold);
            lblFilterTitle.ForeColor = UiTheme.TextDark;
            lblFilterTitle.AutoSize = true;
            lblFilterTitle.Location = new Point(15, 12);

            lblLocationFilter.Text = "Location";
            lblLocationFilter.Font = UiTheme.NormalFont();
            lblLocationFilter.AutoSize = true;
            lblLocationFilter.Location = new Point(15, 45);

            txtLocationFilter.Font = UiTheme.NormalFont();
            txtLocationFilter.Location = new Point(80, 43);
            txtLocationFilter.Size = new Size(120, 25);

            lblSeatsFilter.Text = "Seats";
            lblSeatsFilter.Font = UiTheme.NormalFont();
            lblSeatsFilter.AutoSize = true;
            lblSeatsFilter.Location = new Point(220, 45);

            cmbSeats.DropDownStyle = ComboBoxStyle.DropDownList;
            cmbSeats.Font = UiTheme.NormalFont();
            cmbSeats.Location = new Point(265, 43);
            cmbSeats.Size = new Size(90, 25);

            lblMinPrice.Text = "Min";
            lblMinPrice.Font = UiTheme.NormalFont();
            lblMinPrice.AutoSize = true;
            lblMinPrice.Location = new Point(375, 45);

            txtMinPrice.Font = UiTheme.NormalFont();
            txtMinPrice.Location = new Point(410, 43);
            txtMinPrice.Size = new Size(90, 25);

            lblMaxPrice.Text = "Max";
            lblMaxPrice.Font = UiTheme.NormalFont();
            lblMaxPrice.AutoSize = true;
            lblMaxPrice.Location = new Point(520, 45);

            txtMaxPrice.Font = UiTheme.NormalFont();
            txtMaxPrice.Location = new Point(560, 43);
            txtMaxPrice.Size = new Size(90, 25);

            btnApplyFilter.Text = "Apply";
            btnApplyFilter.Location = new Point(675, 38);
            btnApplyFilter.Size = new Size(65, 32);
            UiTheme.StylePrimaryButton(btnApplyFilter);
            btnApplyFilter.Click += btnApplyFilter_Click;

            btnClearFilter.Text = "Clear";
            btnClearFilter.Location = new Point(750, 38);
            btnClearFilter.Size = new Size(65, 32);
            UiTheme.StyleSlateButton(btnClearFilter);
            btnClearFilter.Click += btnClearFilter_Click;

            filterPanel.Controls.Add(lblFilterTitle);
            filterPanel.Controls.Add(lblLocationFilter);
            filterPanel.Controls.Add(txtLocationFilter);
            filterPanel.Controls.Add(lblSeatsFilter);
            filterPanel.Controls.Add(cmbSeats);
            filterPanel.Controls.Add(lblMinPrice);
            filterPanel.Controls.Add(txtMinPrice);
            filterPanel.Controls.Add(lblMaxPrice);
            filterPanel.Controls.Add(txtMaxPrice);
            filterPanel.Controls.Add(btnApplyFilter);
            filterPanel.Controls.Add(btnClearFilter);

            btnRefresh.Text = "Refresh";
            btnRefresh.Location = new Point(740, 60);
            btnRefresh.Size = new Size(120, 38);
            UiTheme.StylePrimaryButton(btnRefresh);
            btnRefresh.Click += btnRefresh_Click;

            flpCars.Location = new Point(30, 230);
            flpCars.Size = new Size(830, 440);
            flpCars.BackColor = UiTheme.Background;
            flpCars.AutoScroll = true;
            flpCars.WrapContents = true;
            flpCars.FlowDirection = FlowDirection.LeftToRight;
            flpCars.BorderStyle = BorderStyle.None;

            bookingPanel.Location = new Point(890, 110);
            bookingPanel.Size = new Size(300, 560);
            UiTheme.StylePanel(bookingPanel);

            lblPanelTitle.Text = "Booking Details";
            lblPanelTitle.Font = UiTheme.HeaderFont();
            lblPanelTitle.ForeColor = UiTheme.TextDark;
            lblPanelTitle.AutoSize = true;
            lblPanelTitle.Location = new Point(20, 25);

            lblSelectedCar.Text = "Selected Car: None";
            lblSelectedCar.Font = new Font("Segoe UI", 10F, FontStyle.Bold);
            lblSelectedCar.Location = new Point(20, 75);
            lblSelectedCar.Size = new Size(250, 38);

            lblPricePerDay.Text = "Price Per Day: 0";
            lblPricePerDay.Font = UiTheme.NormalFont();
            lblPricePerDay.AutoSize = true;
            lblPricePerDay.Location = new Point(20, 120);

            lblOfferInfo.Text = "Available Offer: -";
            lblOfferInfo.Font = new Font("Segoe UI", 9F, FontStyle.Bold);
            lblOfferInfo.ForeColor = UiTheme.Success;
            lblOfferInfo.Location = new Point(20, 150);
            lblOfferInfo.Size = new Size(250, 38);

            btnApplyOffer.Text = "Apply Offer";
            btnApplyOffer.Location = new Point(20, 195);
            btnApplyOffer.Size = new Size(120, 32);
            UiTheme.StyleSuccessButton(btnApplyOffer);
            btnApplyOffer.Click += btnApplyOffer_Click;

            btnRemoveOffer.Text = "Remove";
            btnRemoveOffer.Location = new Point(150, 195);
            btnRemoveOffer.Size = new Size(120, 32);
            UiTheme.StyleSlateButton(btnRemoveOffer);
            btnRemoveOffer.Click += btnRemoveOffer_Click;

            lblRentDate.Text = "Rent Date";
            lblRentDate.Font = UiTheme.NormalFont();
            lblRentDate.AutoSize = true;
            lblRentDate.Location = new Point(20, 245);

            dtpRentDate.Font = UiTheme.NormalFont();
            dtpRentDate.Format = DateTimePickerFormat.Short;
            dtpRentDate.Location = new Point(20, 275);
            dtpRentDate.Size = new Size(250, 25);
            dtpRentDate.MinDate = DateTime.Today;
            dtpRentDate.ValueChanged += DateChanged;

            lblReturnDate.Text = "Expected Return Date";
            lblReturnDate.Font = UiTheme.NormalFont();
            lblReturnDate.AutoSize = true;
            lblReturnDate.Location = new Point(20, 320);

            dtpReturnDate.Font = UiTheme.NormalFont();
            dtpReturnDate.Format = DateTimePickerFormat.Short;
            dtpReturnDate.Location = new Point(20, 350);
            dtpReturnDate.Size = new Size(250, 25);
            dtpReturnDate.MinDate = DateTime.Today.AddDays(1);
            dtpReturnDate.Value = DateTime.Today.AddDays(1);
            dtpReturnDate.ValueChanged += DateChanged;

            lblTotalAmount.Text = "Original Total: 0 BDT";
            lblTotalAmount.Font = new Font("Segoe UI", 10F, FontStyle.Bold);
            lblTotalAmount.ForeColor = UiTheme.TextDark;
            lblTotalAmount.AutoSize = true;
            lblTotalAmount.Location = new Point(20, 400);

            lblDiscountAmount.Text = "Discount: 0 BDT";
            lblDiscountAmount.Font = new Font("Segoe UI", 10F, FontStyle.Bold);
            lblDiscountAmount.ForeColor = UiTheme.Success;
            lblDiscountAmount.AutoSize = true;
            lblDiscountAmount.Location = new Point(20, 430);

            lblPayableAmount.Text = "Payable: 0 BDT";
            lblPayableAmount.Font = new Font("Segoe UI", 13F, FontStyle.Bold);
            lblPayableAmount.ForeColor = UiTheme.Primary;
            lblPayableAmount.AutoSize = true;
            lblPayableAmount.Location = new Point(20, 465);

            btnRent.Text = "Confirm Rent";
            btnRent.Location = new Point(20, 510);
            btnRent.Size = new Size(250, 42);
            UiTheme.StyleSuccessButton(btnRent);
            btnRent.Click += btnRent_Click;

            bookingPanel.Controls.Add(lblPanelTitle);
            bookingPanel.Controls.Add(lblSelectedCar);
            bookingPanel.Controls.Add(lblPricePerDay);
            bookingPanel.Controls.Add(lblOfferInfo);
            bookingPanel.Controls.Add(btnApplyOffer);
            bookingPanel.Controls.Add(btnRemoveOffer);
            bookingPanel.Controls.Add(lblRentDate);
            bookingPanel.Controls.Add(dtpRentDate);
            bookingPanel.Controls.Add(lblReturnDate);
            bookingPanel.Controls.Add(dtpReturnDate);
            bookingPanel.Controls.Add(lblTotalAmount);
            bookingPanel.Controls.Add(lblDiscountAmount);
            bookingPanel.Controls.Add(lblPayableAmount);
            bookingPanel.Controls.Add(btnRent);

            Controls.Add(lblTitle);
            Controls.Add(lblCustomer);
            Controls.Add(btnRefresh);
            Controls.Add(filterPanel);
            Controls.Add(flpCars);
            Controls.Add(bookingPanel);

            filterPanel.ResumeLayout(false);
            filterPanel.PerformLayout();
            bookingPanel.ResumeLayout(false);
            bookingPanel.PerformLayout();
            ResumeLayout(false);
            PerformLayout();
        }
    }
}