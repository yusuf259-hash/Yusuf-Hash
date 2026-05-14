using Car_Rent_Managment.UI;
using System.Drawing;
using System.Windows.Forms;

namespace Car_Rent_Managment
{
    partial class ManageOffersForm
    {
        private System.ComponentModel.IContainer components = null;

        private Label lblTitle = null!;
        private Label lblSubtitle = null!;
        private Label lblAdmin = null!;
        private Panel cardTotalOffers = null!;
        private Panel cardActiveOffers = null!;
        private Panel cardInactiveOffers = null!;
        private Panel cardPlatformOffers = null!;
        private Panel cardOwnerCarOffers = null!;
        private Label lblTotalOffersValue = null!;
        private Label lblActiveOffersValue = null!;
        private Label lblInactiveOffersValue = null!;
        private Label lblPlatformOffersValue = null!;
        private Label lblOwnerCarOffersValue = null!;
        private Button btnRefresh = null!;
        private Button btnClear = null!;
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
        private Label lblScope = null!;
        private ComboBox cmbScope = null!;
        private Label lblStatus = null!;
        private ComboBox cmbStatus = null!;
        private Button btnAddPlatform = null!;
        private Button btnUpdate = null!;
        private Button btnActivate = null!;
        private Button btnDeactivate = null!;
        private Button btnDelete = null!;

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
            lblAdmin = new Label();
            cardTotalOffers = new Panel();
            cardActiveOffers = new Panel();
            cardInactiveOffers = new Panel();
            cardPlatformOffers = new Panel();
            cardOwnerCarOffers = new Panel();
            lblTotalOffersValue = new Label();
            lblActiveOffersValue = new Label();
            lblInactiveOffersValue = new Label();
            lblPlatformOffersValue = new Label();
            lblOwnerCarOffersValue = new Label();
            btnRefresh = new Button();
            btnClear = new Button();
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
            lblScope = new Label();
            cmbScope = new ComboBox();
            lblStatus = new Label();
            cmbStatus = new ComboBox();
            btnAddPlatform = new Button();
            btnUpdate = new Button();
            btnActivate = new Button();
            btnDeactivate = new Button();
            btnDelete = new Button();

            ((System.ComponentModel.ISupportInitialize)dgvOffers).BeginInit();
            ((System.ComponentModel.ISupportInitialize)nudDiscount).BeginInit();
            inputPanel.SuspendLayout();
            SuspendLayout();

            BackColor = UiTheme.Background;
            ClientSize = new Size(1220, 720);
            StartPosition = FormStartPosition.CenterScreen;
            Text = "Manage Offers";

            lblTitle.Text = "Manage Offers";
            lblTitle.Font = UiTheme.TitleFont();
            lblTitle.ForeColor = UiTheme.TextDark;
            lblTitle.AutoSize = true;
            lblTitle.Location = new Point(30, 25);

            lblSubtitle.Text = "Review all offers and manage platform-wide discounts.";
            lblSubtitle.Font = UiTheme.NormalFont();
            lblSubtitle.ForeColor = UiTheme.TextMuted;
            lblSubtitle.AutoSize = true;
            lblSubtitle.Location = new Point(35, 75);

            lblAdmin.Text = "User:";
            lblAdmin.Font = new Font("Segoe UI", 10F, FontStyle.Bold);
            lblAdmin.ForeColor = UiTheme.TextDark;
            lblAdmin.AutoSize = true;
            lblAdmin.Location = new Point(35, 100);

            btnClear.Text = "Clear";
            btnClear.Location = new Point(635, 40);
            btnClear.Size = new Size(100, 38);
            UiTheme.StyleSlateButton(btnClear);
            btnClear.Click += btnClear_Click;

            btnRefresh.Text = "Refresh";
            btnRefresh.Location = new Point(755, 40);
            btnRefresh.Size = new Size(110, 38);
            UiTheme.StylePrimaryButton(btnRefresh);
            btnRefresh.Click += btnRefresh_Click;

            UiTheme.ConfigureStatCard(cardTotalOffers, lblTotalOffersValue, "Total", "0", 35, 145, UiTheme.Primary, 150, 85, 16F);
            UiTheme.ConfigureStatCard(cardActiveOffers, lblActiveOffersValue, "Active", "0", 200, 145, UiTheme.Success, 150, 85, 16F);
            UiTheme.ConfigureStatCard(cardInactiveOffers, lblInactiveOffersValue, "Inactive", "0", 365, 145, UiTheme.Warning, 150, 85, 16F);
            UiTheme.ConfigureStatCard(cardPlatformOffers, lblPlatformOffersValue, "Platform", "0", 530, 145, UiTheme.Purple, 150, 85, 16F);
            UiTheme.ConfigureStatCard(cardOwnerCarOffers, lblOwnerCarOffersValue, "Owner/Car", "0", 695, 145, UiTheme.Primary, 170, 85, 16F);

            dgvOffers.Location = new Point(35, 255);
            dgvOffers.Size = new Size(830, 415);
            UiTheme.StyleGrid(dgvOffers);
            dgvOffers.CellClick += dgvOffers_CellClick;

            inputPanel.Location = new Point(895, 25);
            inputPanel.Size = new Size(295, 680);
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
            lblOfferTitle.Location = new Point(20, 66);

            txtOfferTitle.Font = UiTheme.NormalFont();
            txtOfferTitle.Location = new Point(20, 92);
            txtOfferTitle.Size = new Size(255, 25);

            lblDescription.Text = "Description";
            lblDescription.Font = UiTheme.NormalFont();
            lblDescription.ForeColor = UiTheme.TextDark;
            lblDescription.AutoSize = true;
            lblDescription.Location = new Point(20, 128);

            txtDescription.Font = UiTheme.NormalFont();
            txtDescription.Location = new Point(20, 154);
            txtDescription.Multiline = true;
            txtDescription.ScrollBars = ScrollBars.Vertical;
            txtDescription.Size = new Size(255, 55);

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
            nudDiscount.Size = new Size(255, 25);
            nudDiscount.Value = 1;

            lblStartDate.Text = "Start date";
            lblStartDate.Font = UiTheme.NormalFont();
            lblStartDate.ForeColor = UiTheme.TextDark;
            lblStartDate.AutoSize = true;
            lblStartDate.Location = new Point(20, 286);

            dtpStartDate.Font = UiTheme.NormalFont();
            dtpStartDate.Format = DateTimePickerFormat.Short;
            dtpStartDate.Location = new Point(20, 312);
            dtpStartDate.Size = new Size(255, 25);

            lblEndDate.Text = "End date";
            lblEndDate.Font = UiTheme.NormalFont();
            lblEndDate.ForeColor = UiTheme.TextDark;
            lblEndDate.AutoSize = true;
            lblEndDate.Location = new Point(20, 350);

            dtpEndDate.Font = UiTheme.NormalFont();
            dtpEndDate.Format = DateTimePickerFormat.Short;
            dtpEndDate.Location = new Point(20, 376);
            dtpEndDate.Size = new Size(255, 25);

            lblScope.Text = "Scope";
            lblScope.Font = UiTheme.NormalFont();
            lblScope.ForeColor = UiTheme.TextDark;
            lblScope.AutoSize = true;
            lblScope.Location = new Point(20, 414);

            cmbScope.DropDownStyle = ComboBoxStyle.DropDownList;
            cmbScope.Enabled = false;
            cmbScope.Font = UiTheme.NormalFont();
            cmbScope.Location = new Point(20, 440);
            cmbScope.Size = new Size(255, 25);

            lblStatus.Text = "Status";
            lblStatus.Font = UiTheme.NormalFont();
            lblStatus.ForeColor = UiTheme.TextDark;
            lblStatus.AutoSize = true;
            lblStatus.Location = new Point(20, 478);

            cmbStatus.DropDownStyle = ComboBoxStyle.DropDownList;
            cmbStatus.Font = UiTheme.NormalFont();
            cmbStatus.Location = new Point(20, 504);
            cmbStatus.Size = new Size(255, 25);

            btnAddPlatform.Text = "Add Platform Offer";
            btnAddPlatform.Location = new Point(20, 544);
            btnAddPlatform.Size = new Size(255, 36);
            UiTheme.StyleSuccessButton(btnAddPlatform);
            btnAddPlatform.Click += btnAddPlatform_Click;

            btnUpdate.Text = "Update Offer";
            btnUpdate.Location = new Point(20, 590);
            btnUpdate.Size = new Size(120, 36);
            UiTheme.StylePrimaryButton(btnUpdate);
            btnUpdate.Click += btnUpdate_Click;

            btnActivate.Text = "Activate";
            btnActivate.Location = new Point(155, 590);
            btnActivate.Size = new Size(120, 36);
            UiTheme.StyleSuccessButton(btnActivate);
            btnActivate.Click += btnActivate_Click;

            btnDeactivate.Text = "Deactivate";
            btnDeactivate.Location = new Point(20, 636);
            btnDeactivate.Size = new Size(120, 36);
            UiTheme.StyleWarningButton(btnDeactivate);
            btnDeactivate.Click += btnDeactivate_Click;

            btnDelete.Text = "Delete";
            btnDelete.Location = new Point(155, 636);
            btnDelete.Size = new Size(120, 36);
            UiTheme.StyleDangerButton(btnDelete);
            btnDelete.Click += btnDelete_Click;

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
            inputPanel.Controls.Add(lblScope);
            inputPanel.Controls.Add(cmbScope);
            inputPanel.Controls.Add(lblStatus);
            inputPanel.Controls.Add(cmbStatus);
            inputPanel.Controls.Add(btnAddPlatform);
            inputPanel.Controls.Add(btnUpdate);
            inputPanel.Controls.Add(btnActivate);
            inputPanel.Controls.Add(btnDeactivate);
            inputPanel.Controls.Add(btnDelete);

            Controls.Add(lblTitle);
            Controls.Add(lblSubtitle);
            Controls.Add(lblAdmin);
            Controls.Add(cardTotalOffers);
            Controls.Add(cardActiveOffers);
            Controls.Add(cardInactiveOffers);
            Controls.Add(cardPlatformOffers);
            Controls.Add(cardOwnerCarOffers);
            Controls.Add(btnClear);
            Controls.Add(btnRefresh);
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
