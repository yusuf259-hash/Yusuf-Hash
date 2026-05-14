using Car_Rent_Managment.UI;
using System.Drawing;
using System.Windows.Forms;

namespace Car_Rent_Managment
{
    partial class ManageAllCarsForm
    {
        private System.ComponentModel.IContainer components = null;

        private Label lblTitle = null!;
        private Panel cardTotalCars = null!;
        private Panel cardAvailableCars = null!;
        private Panel cardRentedCars = null!;
        private Panel cardInactiveCars = null!;
        private Label lblTotalCarsValue = null!;
        private Label lblAvailableCarsValue = null!;
        private Label lblRentedCarsValue = null!;
        private Label lblInactiveCarsValue = null!;
        private DataGridView dgvCars = null!;
        private Panel actionPanel = null!;
        private Label lblPanelTitle = null!;
        private Label lblSelectedCar = null!;
        private Label lblOwner = null!;
        private Label lblCarNumber = null!;
        private Label lblStatus = null!;
        private ComboBox cmbStatus = null!;
        private Button btnUpdateStatus = null!;
        private Button btnDelete = null!;
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
            cardTotalCars = new Panel();
            cardAvailableCars = new Panel();
            cardRentedCars = new Panel();
            cardInactiveCars = new Panel();
            lblTotalCarsValue = new Label();
            lblAvailableCarsValue = new Label();
            lblRentedCarsValue = new Label();
            lblInactiveCarsValue = new Label();
            dgvCars = new DataGridView();
            actionPanel = new Panel();
            lblPanelTitle = new Label();
            lblSelectedCar = new Label();
            lblOwner = new Label();
            lblCarNumber = new Label();
            lblStatus = new Label();
            cmbStatus = new ComboBox();
            btnUpdateStatus = new Button();
            btnDelete = new Button();
            btnRefresh = new Button();

            ((System.ComponentModel.ISupportInitialize)dgvCars).BeginInit();
            actionPanel.SuspendLayout();
            SuspendLayout();

            BackColor = UiTheme.Background;
            ClientSize = new Size(1240, 720);
            StartPosition = FormStartPosition.CenterScreen;
            Text = "Manage All Cars";

            lblTitle.Text = "Manage All Cars";
            lblTitle.Font = UiTheme.TitleFont();
            lblTitle.ForeColor = UiTheme.TextDark;
            lblTitle.AutoSize = true;
            lblTitle.Location = new Point(30, 25);

            btnRefresh.Text = "Refresh";
            btnRefresh.Location = new Point(1090, 40);
            btnRefresh.Size = new Size(110, 38);
            UiTheme.StylePrimaryButton(btnRefresh);
            btnRefresh.Click += btnRefresh_Click;

            UiTheme.ConfigureStatCard(cardTotalCars, lblTotalCarsValue, "Total Cars", "0", 30, 105, UiTheme.Primary, 195, 90, 17F);
            UiTheme.ConfigureStatCard(cardAvailableCars, lblAvailableCarsValue, "Available Cars", "0", 245, 105, UiTheme.Success, 195, 90, 17F);
            UiTheme.ConfigureStatCard(cardRentedCars, lblRentedCarsValue, "Rented Cars", "0", 460, 105, UiTheme.Warning, 195, 90, 17F);
            UiTheme.ConfigureStatCard(cardInactiveCars, lblInactiveCarsValue, "Inactive Cars", "0", 675, 105, UiTheme.Danger, 195, 90, 17F);

            dgvCars.Location = new Point(30, 220);
            dgvCars.Size = new Size(800, 455);
            UiTheme.StyleGrid(dgvCars);
            dgvCars.CellClick += dgvCars_CellClick;

            actionPanel.Location = new Point(860, 220);
            actionPanel.Size = new Size(340, 455);
            UiTheme.StylePanel(actionPanel);

            lblPanelTitle.Text = "Car Actions";
            lblPanelTitle.Font = UiTheme.HeaderFont();
            lblPanelTitle.ForeColor = UiTheme.TextDark;
            lblPanelTitle.AutoSize = true;
            lblPanelTitle.Location = new Point(20, 25);

            lblSelectedCar.Text = "Selected Car: None";
            lblSelectedCar.Font = new Font("Segoe UI", 10F, FontStyle.Bold);
            lblSelectedCar.ForeColor = UiTheme.TextDark;
            lblSelectedCar.Location = new Point(20, 85);
            lblSelectedCar.Size = new Size(290, 45);

            lblOwner.Text = "Owner: -";
            lblOwner.Font = UiTheme.NormalFont();
            lblOwner.ForeColor = UiTheme.TextMuted;
            lblOwner.Location = new Point(20, 145);
            lblOwner.Size = new Size(290, 28);

            lblCarNumber.Text = "Car Number: -";
            lblCarNumber.Font = UiTheme.NormalFont();
            lblCarNumber.ForeColor = UiTheme.TextMuted;
            lblCarNumber.Location = new Point(20, 180);
            lblCarNumber.Size = new Size(290, 28);

            lblStatus.Text = "Status";
            lblStatus.Font = UiTheme.NormalFont();
            lblStatus.ForeColor = UiTheme.TextDark;
            lblStatus.AutoSize = true;
            lblStatus.Location = new Point(20, 230);

            cmbStatus.DropDownStyle = ComboBoxStyle.DropDownList;
            cmbStatus.Font = UiTheme.NormalFont();
            cmbStatus.Location = new Point(20, 260);
            cmbStatus.Size = new Size(290, 25);

            btnUpdateStatus.Text = "Update Status";
            btnUpdateStatus.Location = new Point(20, 320);
            btnUpdateStatus.Size = new Size(290, 40);
            UiTheme.StylePrimaryButton(btnUpdateStatus);
            btnUpdateStatus.Click += btnUpdateStatus_Click;

            btnDelete.Text = "Remove Car";
            btnDelete.Location = new Point(20, 380);
            btnDelete.Size = new Size(290, 40);
            UiTheme.StyleDangerButton(btnDelete);
            btnDelete.Click += btnDelete_Click;

            actionPanel.Controls.Add(lblPanelTitle);
            actionPanel.Controls.Add(lblSelectedCar);
            actionPanel.Controls.Add(lblOwner);
            actionPanel.Controls.Add(lblCarNumber);
            actionPanel.Controls.Add(lblStatus);
            actionPanel.Controls.Add(cmbStatus);
            actionPanel.Controls.Add(btnUpdateStatus);
            actionPanel.Controls.Add(btnDelete);

            Controls.Add(lblTitle);
            Controls.Add(btnRefresh);
            Controls.Add(cardTotalCars);
            Controls.Add(cardAvailableCars);
            Controls.Add(cardRentedCars);
            Controls.Add(cardInactiveCars);
            Controls.Add(dgvCars);
            Controls.Add(actionPanel);

            ((System.ComponentModel.ISupportInitialize)dgvCars).EndInit();
            actionPanel.ResumeLayout(false);
            actionPanel.PerformLayout();
            ResumeLayout(false);
            PerformLayout();
        }
    }
}
