using Car_Rent_Managment.UI;
using System.Drawing;
using System.Windows.Forms;

namespace Car_Rent_Managment
{
    partial class ManageUsersForm
    {
        private System.ComponentModel.IContainer components = null;

        private Label lblTitle = null!;
        private Label lblInfo = null!;
        private Panel cardCustomers = null!;
        private Panel cardOwners = null!;
        private Panel cardAdmins = null!;
        private Panel cardSuspendedUsers = null!;
        private Label lblCustomersValue = null!;
        private Label lblOwnersValue = null!;
        private Label lblAdminsValue = null!;
        private Label lblSuspendedUsersValue = null!;
        private Panel formPanel = null!;
        private Label lblFormTitle = null!;
        private Label lblFullName = null!;
        private TextBox txtFullName = null!;
        private Label lblEmail = null!;
        private TextBox txtEmail = null!;
        private Label lblUsername = null!;
        private TextBox txtUsername = null!;
        private Label lblPassword = null!;
        private TextBox txtPassword = null!;
        private Label lblRole = null!;
        private ComboBox cmbRole = null!;
        private Label lblStatus = null!;
        private ComboBox cmbStatus = null!;
        private Label lblPhone = null!;
        private TextBox txtPhone = null!;
        private Label lblAddress = null!;
        private TextBox txtAddress = null!;
        private Button btnAdd = null!;
        private Button btnUpdate = null!;
        private Button btnSuspend = null!;
        private Button btnActivate = null!;
        private Button btnDelete = null!;
        private Button btnClear = null!;
        private Button btnRefresh = null!;
        private DataGridView dgvUsers = null!;

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
            cardCustomers = new Panel();
            cardOwners = new Panel();
            cardAdmins = new Panel();
            cardSuspendedUsers = new Panel();
            lblCustomersValue = new Label();
            lblOwnersValue = new Label();
            lblAdminsValue = new Label();
            lblSuspendedUsersValue = new Label();
            formPanel = new Panel();
            lblFormTitle = new Label();
            lblFullName = new Label();
            txtFullName = new TextBox();
            lblEmail = new Label();
            txtEmail = new TextBox();
            lblUsername = new Label();
            txtUsername = new TextBox();
            lblPassword = new Label();
            txtPassword = new TextBox();
            lblRole = new Label();
            cmbRole = new ComboBox();
            lblStatus = new Label();
            cmbStatus = new ComboBox();
            lblPhone = new Label();
            txtPhone = new TextBox();
            lblAddress = new Label();
            txtAddress = new TextBox();
            btnAdd = new Button();
            btnUpdate = new Button();
            btnSuspend = new Button();
            btnActivate = new Button();
            btnDelete = new Button();
            btnClear = new Button();
            btnRefresh = new Button();
            dgvUsers = new DataGridView();

            formPanel.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)dgvUsers).BeginInit();
            SuspendLayout();

            AutoScaleDimensions = new SizeF(7F, 15F);
            AutoScaleMode = AutoScaleMode.Font;
            BackColor = UiTheme.Background;
            ClientSize = new Size(1240, 760);
            StartPosition = FormStartPosition.CenterScreen;
            Text = "Manage Users";

            lblTitle.Text = "Manage Users";
            lblTitle.Font = UiTheme.TitleFont();
            lblTitle.ForeColor = UiTheme.TextDark;
            lblTitle.AutoSize = true;
            lblTitle.Location = new Point(30, 25);

            lblInfo.Text = "Logged in as:";
            lblInfo.Font = UiTheme.NormalFont();
            lblInfo.ForeColor = UiTheme.TextMuted;
            lblInfo.AutoSize = true;
            lblInfo.Location = new Point(35, 75);

            UiTheme.ConfigureStatCard(cardCustomers, lblCustomersValue, "Customers", "0", 30, 115, UiTheme.Primary, 200, 85, 17F);
            UiTheme.ConfigureStatCard(cardOwners, lblOwnersValue, "Owners", "0", 250, 115, UiTheme.Success, 200, 85, 17F);
            UiTheme.ConfigureStatCard(cardAdmins, lblAdminsValue, "Admins", "0", 470, 115, UiTheme.Purple, 200, 85, 17F);
            UiTheme.ConfigureStatCard(cardSuspendedUsers, lblSuspendedUsersValue, "Suspended Users", "0", 690, 115, UiTheme.Danger, 220, 85, 17F);

            btnRefresh.Text = "Refresh";
            btnRefresh.Location = new Point(1060, 145);
            btnRefresh.Size = new Size(120, 38);
            UiTheme.StylePrimaryButton(btnRefresh);
            btnRefresh.Click += btnRefresh_Click;

            formPanel.Location = new Point(30, 225);
            formPanel.Size = new Size(360, 500);
            UiTheme.StylePanel(formPanel);

            lblFormTitle.Text = "User Information";
            lblFormTitle.Font = UiTheme.HeaderFont();
            lblFormTitle.ForeColor = UiTheme.TextDark;
            lblFormTitle.AutoSize = true;
            lblFormTitle.Location = new Point(20, 20);

            lblFullName.Text = "Full Name";
            lblFullName.Font = UiTheme.SmallFont();
            lblFullName.AutoSize = true;
            lblFullName.Location = new Point(20, 70);

            txtFullName.Font = UiTheme.NormalFont();
            txtFullName.Location = new Point(20, 95);
            txtFullName.Size = new Size(145, 25);

            lblEmail.Text = "Email";
            lblEmail.Font = UiTheme.SmallFont();
            lblEmail.AutoSize = true;
            lblEmail.Location = new Point(190, 70);

            txtEmail.Font = UiTheme.NormalFont();
            txtEmail.Location = new Point(190, 95);
            txtEmail.Size = new Size(145, 25);

            lblUsername.Text = "Username";
            lblUsername.Font = UiTheme.SmallFont();
            lblUsername.AutoSize = true;
            lblUsername.Location = new Point(20, 135);

            txtUsername.Font = UiTheme.NormalFont();
            txtUsername.Location = new Point(20, 160);
            txtUsername.Size = new Size(145, 25);

            lblPassword.Text = "Password";
            lblPassword.Font = UiTheme.SmallFont();
            lblPassword.AutoSize = true;
            lblPassword.Location = new Point(190, 135);

            txtPassword.Font = UiTheme.NormalFont();
            txtPassword.Location = new Point(190, 160);
            txtPassword.Size = new Size(145, 25);
            txtPassword.UseSystemPasswordChar = true;

            lblRole.Text = "Role";
            lblRole.Font = UiTheme.SmallFont();
            lblRole.AutoSize = true;
            lblRole.Location = new Point(20, 200);

            cmbRole.DropDownStyle = ComboBoxStyle.DropDownList;
            cmbRole.Font = UiTheme.NormalFont();
            cmbRole.Location = new Point(20, 225);
            cmbRole.Size = new Size(145, 25);

            lblStatus.Text = "Status";
            lblStatus.Font = UiTheme.SmallFont();
            lblStatus.AutoSize = true;
            lblStatus.Location = new Point(190, 200);

            cmbStatus.DropDownStyle = ComboBoxStyle.DropDownList;
            cmbStatus.Font = UiTheme.NormalFont();
            cmbStatus.Location = new Point(190, 225);
            cmbStatus.Size = new Size(145, 25);

            lblPhone.Text = "Phone";
            lblPhone.Font = UiTheme.SmallFont();
            lblPhone.AutoSize = true;
            lblPhone.Location = new Point(20, 265);

            txtPhone.Font = UiTheme.NormalFont();
            txtPhone.Location = new Point(20, 290);
            txtPhone.Size = new Size(145, 25);

            lblAddress.Text = "Address";
            lblAddress.Font = UiTheme.SmallFont();
            lblAddress.AutoSize = true;
            lblAddress.Location = new Point(190, 265);

            txtAddress.Font = UiTheme.NormalFont();
            txtAddress.Location = new Point(190, 290);
            txtAddress.Size = new Size(145, 25);

            btnAdd.Text = "Add User";
            btnAdd.Location = new Point(20, 350);
            btnAdd.Size = new Size(145, 35);
            UiTheme.StyleSuccessButton(btnAdd);
            btnAdd.Click += btnAdd_Click;

            btnUpdate.Text = "Update User";
            btnUpdate.Location = new Point(190, 350);
            btnUpdate.Size = new Size(145, 35);
            UiTheme.StylePrimaryButton(btnUpdate);
            btnUpdate.Click += btnUpdate_Click;

            btnSuspend.Text = "Suspend";
            btnSuspend.Location = new Point(20, 400);
            btnSuspend.Size = new Size(145, 35);
            UiTheme.StyleWarningButton(btnSuspend);
            btnSuspend.Click += btnSuspend_Click;

            btnActivate.Text = "Activate";
            btnActivate.Location = new Point(190, 400);
            btnActivate.Size = new Size(145, 35);
            UiTheme.StyleSuccessButton(btnActivate);
            btnActivate.Click += btnActivate_Click;

            btnDelete.Text = "Delete";
            btnDelete.Location = new Point(20, 450);
            btnDelete.Size = new Size(145, 35);
            UiTheme.StyleDangerButton(btnDelete);
            btnDelete.Click += btnDelete_Click;

            btnClear.Text = "Clear";
            btnClear.Location = new Point(190, 450);
            btnClear.Size = new Size(145, 35);
            UiTheme.StyleSlateButton(btnClear);
            btnClear.Click += btnClear_Click;

            formPanel.Controls.Add(lblFormTitle);
            formPanel.Controls.Add(lblFullName);
            formPanel.Controls.Add(txtFullName);
            formPanel.Controls.Add(lblEmail);
            formPanel.Controls.Add(txtEmail);
            formPanel.Controls.Add(lblUsername);
            formPanel.Controls.Add(txtUsername);
            formPanel.Controls.Add(lblPassword);
            formPanel.Controls.Add(txtPassword);
            formPanel.Controls.Add(lblRole);
            formPanel.Controls.Add(cmbRole);
            formPanel.Controls.Add(lblStatus);
            formPanel.Controls.Add(cmbStatus);
            formPanel.Controls.Add(lblPhone);
            formPanel.Controls.Add(txtPhone);
            formPanel.Controls.Add(lblAddress);
            formPanel.Controls.Add(txtAddress);
            formPanel.Controls.Add(btnAdd);
            formPanel.Controls.Add(btnUpdate);
            formPanel.Controls.Add(btnSuspend);
            formPanel.Controls.Add(btnActivate);
            formPanel.Controls.Add(btnDelete);
            formPanel.Controls.Add(btnClear);

            dgvUsers.Location = new Point(420, 225);
            dgvUsers.Size = new Size(790, 500);
            UiTheme.StyleGrid(dgvUsers);
            dgvUsers.CellClick += dgvUsers_CellClick;

            Controls.Add(lblTitle);
            Controls.Add(lblInfo);
            Controls.Add(cardCustomers);
            Controls.Add(cardOwners);
            Controls.Add(cardAdmins);
            Controls.Add(cardSuspendedUsers);
            Controls.Add(btnRefresh);
            Controls.Add(formPanel);
            Controls.Add(dgvUsers);

            formPanel.ResumeLayout(false);
            formPanel.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)dgvUsers).EndInit();
            ResumeLayout(false);
            PerformLayout();
        }
    }
}
