using System.Drawing;
using System.Windows.Forms;

namespace Car_Rent_Managment
{
    partial class ManageCarsForm
    {
        private System.ComponentModel.IContainer components = null;

        private Label lblTitle;
        private Label lblInfo;
        private Panel formPanel;
        private Label lblFormTitle;
        private Label lblCarName;
        private TextBox txtCarName;
        private Label lblBrand;
        private TextBox txtBrand;
        private Label lblModel;
        private TextBox txtModel;
        private Label lblCarNumber;
        private TextBox txtCarNumber;
        private Label lblSeats;
        private TextBox txtSeats;
        private Label lblPrice;
        private TextBox txtPricePerDay;
        private Label lblLocation;
        private TextBox txtLocation;
        private Label lblStatus;
        private ComboBox cmbStatus;
        private Label lblDescription;
        private TextBox txtDescription;
        private Button btnAdd;
        private Button btnUpdate;
        private Button btnDelete;
        private Button btnClear;
        private DataGridView dgvCars;
        private Button btnRefresh;

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
            lblInfo = new Label();
            formPanel = new Panel();
            lblFormTitle = new Label();
            lblCarName = new Label();
            txtCarName = new TextBox();
            lblBrand = new Label();
            txtBrand = new TextBox();
            lblModel = new Label();
            txtModel = new TextBox();
            lblCarNumber = new Label();
            txtCarNumber = new TextBox();
            lblSeats = new Label();
            txtSeats = new TextBox();
            lblPrice = new Label();
            txtPricePerDay = new TextBox();
            lblLocation = new Label();
            txtLocation = new TextBox();
            lblStatus = new Label();
            cmbStatus = new ComboBox();
            lblDescription = new Label();
            txtDescription = new TextBox();
            btnAdd = new Button();
            btnUpdate = new Button();
            btnDelete = new Button();
            btnClear = new Button();
            dgvCars = new DataGridView();
            btnRefresh = new Button();

            formPanel.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)dgvCars).BeginInit();
            SuspendLayout();

            lblTitle.AutoSize = true;
            lblTitle.Font = new Font("Segoe UI", 24F, FontStyle.Bold);
            lblTitle.Location = new Point(30, 25);
            lblTitle.Name = "lblTitle";
            lblTitle.Size = new Size(266, 45);
            lblTitle.TabIndex = 0;
            lblTitle.Text = "Manage My Cars";

            lblInfo.AutoSize = true;
            lblInfo.Font = new Font("Segoe UI", 10F);
            lblInfo.ForeColor = Color.Gray;
            lblInfo.Location = new Point(35, 70);
            lblInfo.Name = "lblInfo";
            lblInfo.Size = new Size(54, 19);
            lblInfo.TabIndex = 1;
            lblInfo.Text = "Owner:";

            formPanel.BackColor = Color.White;
            formPanel.BorderStyle = BorderStyle.FixedSingle;
            formPanel.Controls.Add(lblFormTitle);
            formPanel.Controls.Add(lblCarName);
            formPanel.Controls.Add(txtCarName);
            formPanel.Controls.Add(lblBrand);
            formPanel.Controls.Add(txtBrand);
            formPanel.Controls.Add(lblModel);
            formPanel.Controls.Add(txtModel);
            formPanel.Controls.Add(lblCarNumber);
            formPanel.Controls.Add(txtCarNumber);
            formPanel.Controls.Add(lblSeats);
            formPanel.Controls.Add(txtSeats);
            formPanel.Controls.Add(lblPrice);
            formPanel.Controls.Add(txtPricePerDay);
            formPanel.Controls.Add(lblLocation);
            formPanel.Controls.Add(txtLocation);
            formPanel.Controls.Add(lblStatus);
            formPanel.Controls.Add(cmbStatus);
            formPanel.Controls.Add(lblDescription);
            formPanel.Controls.Add(txtDescription);
            formPanel.Controls.Add(btnAdd);
            formPanel.Controls.Add(btnUpdate);
            formPanel.Controls.Add(btnDelete);
            formPanel.Controls.Add(btnClear);
            formPanel.Location = new Point(30, 110);
            formPanel.Name = "formPanel";
            formPanel.Size = new Size(360, 530);
            formPanel.TabIndex = 2;

            lblFormTitle.AutoSize = true;
            lblFormTitle.Font = new Font("Segoe UI", 15F, FontStyle.Bold);
            lblFormTitle.Location = new Point(20, 20);
            lblFormTitle.Name = "lblFormTitle";
            lblFormTitle.Size = new Size(162, 28);
            lblFormTitle.Text = "Car Information";

            lblCarName.AutoSize = true;
            lblCarName.Font = new Font("Segoe UI", 9F);
            lblCarName.Location = new Point(20, 70);
            lblCarName.Text = "Car Name";

            txtCarName.Font = new Font("Segoe UI", 10F);
            txtCarName.Location = new Point(20, 95);
            txtCarName.Name = "txtCarName";
            txtCarName.Size = new Size(145, 25);
            txtCarName.TabIndex = 0;

            lblBrand.AutoSize = true;
            lblBrand.Font = new Font("Segoe UI", 9F);
            lblBrand.Location = new Point(190, 70);
            lblBrand.Text = "Brand";

            txtBrand.Font = new Font("Segoe UI", 10F);
            txtBrand.Location = new Point(190, 95);
            txtBrand.Name = "txtBrand";
            txtBrand.Size = new Size(145, 25);
            txtBrand.TabIndex = 1;

            lblModel.AutoSize = true;
            lblModel.Font = new Font("Segoe UI", 9F);
            lblModel.Location = new Point(20, 140);
            lblModel.Text = "Model";

            txtModel.Font = new Font("Segoe UI", 10F);
            txtModel.Location = new Point(20, 165);
            txtModel.Name = "txtModel";
            txtModel.Size = new Size(145, 25);
            txtModel.TabIndex = 2;

            lblCarNumber.AutoSize = true;
            lblCarNumber.Font = new Font("Segoe UI", 9F);
            lblCarNumber.Location = new Point(190, 140);
            lblCarNumber.Text = "Car Number";

            txtCarNumber.Font = new Font("Segoe UI", 10F);
            txtCarNumber.Location = new Point(190, 165);
            txtCarNumber.Name = "txtCarNumber";
            txtCarNumber.Size = new Size(145, 25);
            txtCarNumber.TabIndex = 3;

            lblSeats.AutoSize = true;
            lblSeats.Font = new Font("Segoe UI", 9F);
            lblSeats.Location = new Point(20, 210);
            lblSeats.Text = "Seats";

            txtSeats.Font = new Font("Segoe UI", 10F);
            txtSeats.Location = new Point(20, 235);
            txtSeats.Name = "txtSeats";
            txtSeats.Size = new Size(145, 25);
            txtSeats.TabIndex = 4;

            lblPrice.AutoSize = true;
            lblPrice.Font = new Font("Segoe UI", 9F);
            lblPrice.Location = new Point(190, 210);
            lblPrice.Text = "Price Per Day";

            txtPricePerDay.Font = new Font("Segoe UI", 10F);
            txtPricePerDay.Location = new Point(190, 235);
            txtPricePerDay.Name = "txtPricePerDay";
            txtPricePerDay.Size = new Size(145, 25);
            txtPricePerDay.TabIndex = 5;

            lblLocation.AutoSize = true;
            lblLocation.Font = new Font("Segoe UI", 9F);
            lblLocation.Location = new Point(20, 280);
            lblLocation.Text = "Location";

            txtLocation.Font = new Font("Segoe UI", 10F);
            txtLocation.Location = new Point(20, 305);
            txtLocation.Name = "txtLocation";
            txtLocation.Size = new Size(145, 25);
            txtLocation.TabIndex = 6;

            lblStatus.AutoSize = true;
            lblStatus.Font = new Font("Segoe UI", 9F);
            lblStatus.Location = new Point(190, 280);
            lblStatus.Text = "Status";

            cmbStatus.DropDownStyle = ComboBoxStyle.DropDownList;
            cmbStatus.Font = new Font("Segoe UI", 10F);
            cmbStatus.Items.AddRange(new object[] { "Available", "Maintenance", "Unavailable", "Rented" });
            cmbStatus.Location = new Point(190, 305);
            cmbStatus.Name = "cmbStatus";
            cmbStatus.Size = new Size(145, 25);
            cmbStatus.TabIndex = 7;
            cmbStatus.SelectedIndex = 0;

            lblDescription.AutoSize = true;
            lblDescription.Font = new Font("Segoe UI", 9F);
            lblDescription.Location = new Point(20, 350);
            lblDescription.Text = "Description";

            txtDescription.Font = new Font("Segoe UI", 10F);
            txtDescription.Location = new Point(20, 375);
            txtDescription.Multiline = true;
            txtDescription.Name = "txtDescription";
            txtDescription.Size = new Size(315, 55);
            txtDescription.TabIndex = 8;

            btnAdd.BackColor = Color.FromArgb(24, 90, 157);
            btnAdd.FlatStyle = FlatStyle.Flat;
            btnAdd.Font = new Font("Segoe UI", 9F, FontStyle.Bold);
            btnAdd.ForeColor = Color.White;
            btnAdd.Location = new Point(20, 455);
            btnAdd.Name = "btnAdd";
            btnAdd.Size = new Size(70, 35);
            btnAdd.TabIndex = 9;
            btnAdd.Text = "Add";
            btnAdd.UseVisualStyleBackColor = false;
            btnAdd.Click += btnAdd_Click;

            btnUpdate.BackColor = Color.FromArgb(38, 166, 91);
            btnUpdate.FlatStyle = FlatStyle.Flat;
            btnUpdate.Font = new Font("Segoe UI", 9F, FontStyle.Bold);
            btnUpdate.ForeColor = Color.White;
            btnUpdate.Location = new Point(100, 455);
            btnUpdate.Name = "btnUpdate";
            btnUpdate.Size = new Size(80, 35);
            btnUpdate.TabIndex = 10;
            btnUpdate.Text = "Update";
            btnUpdate.UseVisualStyleBackColor = false;
            btnUpdate.Click += btnUpdate_Click;

            btnDelete.BackColor = Color.FromArgb(200, 60, 60);
            btnDelete.FlatStyle = FlatStyle.Flat;
            btnDelete.Font = new Font("Segoe UI", 9F, FontStyle.Bold);
            btnDelete.ForeColor = Color.White;
            btnDelete.Location = new Point(190, 455);
            btnDelete.Name = "btnDelete";
            btnDelete.Size = new Size(70, 35);
            btnDelete.TabIndex = 11;
            btnDelete.Text = "Delete";
            btnDelete.UseVisualStyleBackColor = false;
            btnDelete.Click += btnDelete_Click;

            btnClear.BackColor = Color.Gray;
            btnClear.FlatStyle = FlatStyle.Flat;
            btnClear.Font = new Font("Segoe UI", 9F, FontStyle.Bold);
            btnClear.ForeColor = Color.White;
            btnClear.Location = new Point(270, 455);
            btnClear.Name = "btnClear";
            btnClear.Size = new Size(70, 35);
            btnClear.TabIndex = 12;
            btnClear.Text = "Clear";
            btnClear.UseVisualStyleBackColor = false;
            btnClear.Click += btnClear_Click;

            dgvCars.AllowUserToAddRows = false;
            dgvCars.AllowUserToDeleteRows = false;
            dgvCars.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill;
            dgvCars.BackgroundColor = Color.White;
            dgvCars.BorderStyle = BorderStyle.FixedSingle;
            dgvCars.Location = new Point(420, 110);
            dgvCars.MultiSelect = false;
            dgvCars.Name = "dgvCars";
            dgvCars.ReadOnly = true;
            dgvCars.SelectionMode = DataGridViewSelectionMode.FullRowSelect;
            dgvCars.Size = new Size(700, 470);
            dgvCars.TabIndex = 3;
            dgvCars.CellClick += dgvCars_CellClick;

            btnRefresh.BackColor = Color.FromArgb(24, 90, 157);
            btnRefresh.FlatStyle = FlatStyle.Flat;
            btnRefresh.Font = new Font("Segoe UI", 10F, FontStyle.Bold);
            btnRefresh.ForeColor = Color.White;
            btnRefresh.Location = new Point(1000, 600);
            btnRefresh.Name = "btnRefresh";
            btnRefresh.Size = new Size(120, 38);
            btnRefresh.TabIndex = 4;
            btnRefresh.Text = "Refresh";
            btnRefresh.UseVisualStyleBackColor = false;
            btnRefresh.Click += btnRefresh_Click;

            AutoScaleDimensions = new SizeF(7F, 15F);
            AutoScaleMode = AutoScaleMode.Font;
            BackColor = Color.FromArgb(245, 247, 250);
            ClientSize = new Size(1134, 661);
            Controls.Add(lblTitle);
            Controls.Add(lblInfo);
            Controls.Add(formPanel);
            Controls.Add(dgvCars);
            Controls.Add(btnRefresh);
            Name = "ManageCarsForm";
            StartPosition = FormStartPosition.CenterScreen;
            Text = "Manage Cars";

            formPanel.ResumeLayout(false);
            formPanel.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)dgvCars).EndInit();
            ResumeLayout(false);
            PerformLayout();
        }
    }
}