using Car_Rent_Managment.UI;
using System.Drawing;
using System.Windows.Forms;

namespace Car_Rent_Managment
{
    partial class OwnerOffersForm
    {
        private System.ComponentModel.IContainer components = null;

        private Label lblTitle = null!;
        private Label lblSubtitle = null!;
        private Label lblOwner = null!;
        private Panel cardTotalOffers = null!;
        private Panel cardActiveOffers = null!;
        private Panel cardInactiveOffers = null!;
        private Label lblTotalOffersValue = null!;
        private Label lblActiveOffersValue = null!;
        private Label lblInactiveOffersValue = null!;
        private DataGridView dgvOffers = null!;
        private Panel inputPanel = null!;
        private Label lblFormTitle = null!;
        private Label lblOfferTitle = null!;
        private TextBox txtOfferTitle = null!;
        private Label lblDescription = null!;
        private TextBox txtDescription = null!;
        private Label lblDiscount = null!;
        private NumericUpDown nudDiscount = null!;
        private Label lblStartDate = null!;
        private DateTimePicker dtpStartDate = null!;
        private Label lblEndDate = null!;
        private DateTimePicker dtpEndDate = null!;
        private Label lblCar = null!;
        private ComboBox cmbCars = null!;
        private Label lblStatus = null!;
        private ComboBox cmbStatus = null!;
        private Button btnAdd = null!;
        private Button btnUpdate = null!;
        private Button btnDeactivate = null!;
        private Button btnClear = null!;
        private Button btnRefresh = null!;

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
            lblSubtitle = new Label();
            lblOwner = new Label();
            cardTotalOffers = new Panel();
            cardActiveOffers = new Panel();
            cardInactiveOffers = new Panel();
            lblTotalOffersValue = new Label();
            lblActiveOffersValue = new Label();
            lblInactiveOffersValue = new Label();
            dgvOffers = new DataGridView();
            inputPanel = new Panel();
            lblFormTitle = new Label();
            lblOfferTitle = new Label();
            txtOfferTitle = new TextBox();
            lblDescription = new Label();
            txtDescription = new TextBox();
            lblDiscount = new Label();
            nudDiscount = new NumericUpDown();
            lblStartDate = new Label();
            dtpStartDate = new DateTimePicker();
            lblEndDate = new Label();
            dtpEndDate = new DateTimePicker();
            lblCar = new Label();
            cmbCars = new ComboBox();
            lblStatus = new Label();
            cmbStatus = new ComboBox();
            btnAdd = new Button();
            btnUpdate = new Button();
            btnDeactivate = new Button();
            btnClear = new Button();
            btnRefresh = new Button();

            ((System.ComponentModel.ISupportInitialize)dgvOffers).BeginInit();
            ((System.ComponentModel.ISupportInitialize)nudDiscount).BeginInit();
            inputPanel.SuspendLayout();
            SuspendLayout();

            BackColor = UiTheme.Background;
            ClientSize = new Size(1220, 720);
            StartPosition = FormStartPosition.CenterScreen;
            Text = "My Offers";

            lblTitle.Text = "My Offers";
            lblTitle.Font = UiTheme.TitleFont();
            lblTitle.ForeColor = UiTheme.TextDark;
            lblTitle.AutoSize = true;
            lblTitle.Location = new Point(30, 25);

            lblSubtitle.Text = "Create discounts for all your cars or a selected car.";
            lblSubtitle.Font = UiTheme.NormalFont();
            lblSubtitle.ForeColor = UiTheme.TextMuted;
            lblSubtitle.AutoSize = true;
            lblSubtitle.Location = new Point(35, 75);

            lblOwner.Text = "Owner:";
            lblOwner.Font = new Font("Segoe UI", 10F, FontStyle.Bold);
            lblOwner.ForeColor = UiTheme.TextDark;
            lblOwner.AutoSize = true;
            lblOwner.Location = new Point(35, 100);

            btnRefresh.Text = "Refresh";
            btnRefresh.Location = new Point(760, 40);
            btnRefresh.Size = new Size(110, 38);
            UiTheme.StylePrimaryButton(btnRefresh);
            btnRefresh.Click += btnRefresh_Click;

            UiTheme.ConfigureStatCard(cardTotalOffers, lblTotalOffersValue, "Total Offers", "0", 35, 145, UiTheme.Primary, 260, 95);
            UiTheme.ConfigureStatCard(cardActiveOffers, lblActiveOffersValue, "Active Offers", "0", 315, 145, UiTheme.Success, 260, 95);
            UiTheme.ConfigureStatCard(cardInactiveOffers, lblInactiveOffersValue, "Inactive/Expired", "0", 595, 145, UiTheme.Warning, 275, 95);

            dgvOffers.Location = new Point(35, 265);
            dgvOffers.Size = new Size(835, 405);
            UiTheme.StyleGrid(dgvOffers);
            dgvOffers.CellClick += dgvOffers_CellClick;

            inputPanel.Location = new Point(900, 40);
            inputPanel.Size = new Size(285, 630);
            UiTheme.StylePanel(inputPanel);

            lblFormTitle.Text = "Offer Details";
            lblFormTitle.Font = UiTheme.HeaderFont();
            lblFormTitle.ForeColor = UiTheme.TextDark;
            lblFormTitle.AutoSize = true;
            lblFormTitle.Location = new Point(20, 20);

            lblOfferTitle.Text = "Offer title";
            lblOfferTitle.Font = UiTheme.NormalFont();
            lblOfferTitle.ForeColor = UiTheme.TextDark;
            lblOfferTitle.AutoSize = true;
            lblOfferTitle.Location = new Point(20, 70);

            txtOfferTitle.Font = UiTheme.NormalFont();
            txtOfferTitle.Location = new Point(20, 98);
            txtOfferTitle.Size = new Size(245, 25);

            lblDescription.Text = "Description";
            lblDescription.Font = UiTheme.NormalFont();
            lblDescription.ForeColor = UiTheme.TextDark;
            lblDescription.AutoSize = true;
            lblDescription.Location = new Point(20, 138);

            txtDescription.Font = UiTheme.NormalFont();
            txtDescription.Location = new Point(20, 154);
            txtDescription.Multiline = true;
            txtDescription.ScrollBars = ScrollBars.Vertical;
            txtDescription.Size = new Size(245, 55);

            lblDiscount.Text = "Discount percent";
            lblDiscount.Font = UiTheme.NormalFont();
            lblDiscount.ForeColor = UiTheme.TextDark;
            lblDiscount.AutoSize = true;
            lblDiscount.Location = new Point(20, 222);

            nudDiscount.DecimalPlaces = 2;
            nudDiscount.Font = UiTheme.NormalFont();
            nudDiscount.Location = new Point(20, 248);
            nudDiscount.Maximum = 100;
            nudDiscount.Minimum = 0.01M;
            nudDiscount.Size = new Size(245, 25);
            nudDiscount.Value = 1;

            lblStartDate.Text = "Start date";
            lblStartDate.Font = UiTheme.NormalFont();
            lblStartDate.ForeColor = UiTheme.TextDark;
            lblStartDate.AutoSize = true;
            lblStartDate.Location = new Point(20, 286);

            dtpStartDate.Font = UiTheme.NormalFont();
            dtpStartDate.Format = DateTimePickerFormat.Short;
            dtpStartDate.Location = new Point(20, 312);
            dtpStartDate.Size = new Size(245, 25);

            lblEndDate.Text = "End date";
            lblEndDate.Font = UiTheme.NormalFont();
            lblEndDate.ForeColor = UiTheme.TextDark;
            lblEndDate.AutoSize = true;
            lblEndDate.Location = new Point(20, 350);

            dtpEndDate.Font = UiTheme.NormalFont();
            dtpEndDate.Format = DateTimePickerFormat.Short;
            dtpEndDate.Location = new Point(20, 376);
            dtpEndDate.Size = new Size(245, 25);

            lblCar.Text = "Car selection";
            lblCar.Font = UiTheme.NormalFont();
            lblCar.ForeColor = UiTheme.TextDark;
            lblCar.AutoSize = true;
            lblCar.Location = new Point(20, 414);

            cmbCars.DropDownStyle = ComboBoxStyle.DropDownList;
            cmbCars.Font = UiTheme.NormalFont();
            cmbCars.Location = new Point(20, 440);
            cmbCars.Size = new Size(245, 25);

            lblStatus.Text = "Status";
            lblStatus.Font = UiTheme.NormalFont();
            lblStatus.ForeColor = UiTheme.TextDark;
            lblStatus.AutoSize = true;
            lblStatus.Location = new Point(20, 478);

            cmbStatus.DropDownStyle = ComboBoxStyle.DropDownList;
            cmbStatus.Font = UiTheme.NormalFont();
            cmbStatus.Location = new Point(20, 504);
            cmbStatus.Size = new Size(245, 25);

            btnAdd.Text = "Add Offer";
            btnAdd.Location = new Point(20, 546);
            btnAdd.Size = new Size(115, 36);
            UiTheme.StyleSuccessButton(btnAdd);
            btnAdd.Click += btnAdd_Click;

            btnUpdate.Text = "Update";
            btnUpdate.Location = new Point(150, 546);
            btnUpdate.Size = new Size(115, 36);
            UiTheme.StylePrimaryButton(btnUpdate);
            btnUpdate.Click += btnUpdate_Click;

            btnDeactivate.Text = "Deactivate";
            btnDeactivate.Location = new Point(20, 590);
            btnDeactivate.Size = new Size(115, 36);
            UiTheme.StyleWarningButton(btnDeactivate);
            btnDeactivate.Click += btnDeactivate_Click;

            btnClear.Text = "Clear";
            btnClear.Location = new Point(150, 590);
            btnClear.Size = new Size(115, 36);
            UiTheme.StyleSlateButton(btnClear);
            btnClear.Click += btnClear_Click;

            inputPanel.Controls.Add(lblFormTitle);
            inputPanel.Controls.Add(lblOfferTitle);
            inputPanel.Controls.Add(txtOfferTitle);
            inputPanel.Controls.Add(lblDescription);
            inputPanel.Controls.Add(txtDescription);
            inputPanel.Controls.Add(lblDiscount);
            inputPanel.Controls.Add(nudDiscount);
            inputPanel.Controls.Add(lblStartDate);
            inputPanel.Controls.Add(dtpStartDate);
            inputPanel.Controls.Add(lblEndDate);
            inputPanel.Controls.Add(dtpEndDate);
            inputPanel.Controls.Add(lblCar);
            inputPanel.Controls.Add(cmbCars);
            inputPanel.Controls.Add(lblStatus);
            inputPanel.Controls.Add(cmbStatus);
            inputPanel.Controls.Add(btnAdd);
            inputPanel.Controls.Add(btnUpdate);
            inputPanel.Controls.Add(btnDeactivate);
            inputPanel.Controls.Add(btnClear);

            Controls.Add(lblTitle);
            Controls.Add(lblSubtitle);
            Controls.Add(lblOwner);
            Controls.Add(btnRefresh);
            Controls.Add(cardTotalOffers);
            Controls.Add(cardActiveOffers);
            Controls.Add(cardInactiveOffers);
            Controls.Add(dgvOffers);
            Controls.Add(inputPanel);

            ((System.ComponentModel.ISupportInitialize)dgvOffers).EndInit();
            ((System.ComponentModel.ISupportInitialize)nudDiscount).EndInit();
            inputPanel.ResumeLayout(false);
            inputPanel.PerformLayout();
            ResumeLayout(false);
            PerformLayout();
        }
    }
}
