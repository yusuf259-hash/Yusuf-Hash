using System.Drawing;
using System.Windows.Forms;

namespace Car_Rent_Managment
{
    partial class RegisterForm
    {
        private System.ComponentModel.IContainer components = null;

        private Panel leftPanel;
        private Label lblBrand;
        private Label lblSub;
        private Label lblTitle;

        private Label lblFullName;
        private TextBox txtFullName;
        private Label lblEmail;
        private TextBox txtEmail;
        private Label lblUsername;
        private TextBox txtUsername;
        private Label lblRole;
        private ComboBox cmbRole;

        private Label lblPhone;
        private TextBox txtPhone;
        private Label lblAddress;
        private TextBox txtAddress;
        private Label lblPassword;
        private TextBox txtPassword;
        private Label lblConfirmPassword;
        private TextBox txtConfirmPassword;

        private CheckBox chkShowPassword;
        private Button btnRegister;
        private Button btnBack;
        private Label lblNote;

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
            leftPanel = new Panel();
            lblBrand = new Label();
            lblSub = new Label();
            lblTitle = new Label();

            lblFullName = new Label();
            txtFullName = new TextBox();
            lblEmail = new Label();
            txtEmail = new TextBox();
            lblUsername = new Label();
            txtUsername = new TextBox();
            lblRole = new Label();
            cmbRole = new ComboBox();

            lblPhone = new Label();
            txtPhone = new TextBox();
            lblAddress = new Label();
            txtAddress = new TextBox();
            lblPassword = new Label();
            txtPassword = new TextBox();
            lblConfirmPassword = new Label();
            txtConfirmPassword = new TextBox();

            chkShowPassword = new CheckBox();
            btnRegister = new Button();
            btnBack = new Button();
            lblNote = new Label();

            leftPanel.SuspendLayout();
            SuspendLayout();

            leftPanel.BackColor = Color.FromArgb(24, 90, 157);
            leftPanel.Controls.Add(lblBrand);
            leftPanel.Controls.Add(lblSub);
            leftPanel.Location = new Point(0, 0);
            leftPanel.Name = "leftPanel";
            leftPanel.Size = new Size(330, 650);
            leftPanel.TabIndex = 0;

            lblBrand.AutoSize = true;
            lblBrand.Font = new Font("Segoe UI", 28F, FontStyle.Bold);
            lblBrand.ForeColor = Color.White;
            lblBrand.Location = new Point(45, 180);
            lblBrand.Name = "lblBrand";
            lblBrand.Size = new Size(171, 100);
            lblBrand.TabIndex = 0;
            lblBrand.Text = "Create\r\nAccount";

            lblSub.AutoSize = true;
            lblSub.Font = new Font("Segoe UI", 11F);
            lblSub.ForeColor = Color.WhiteSmoke;
            lblSub.Location = new Point(50, 290);
            lblSub.Name = "lblSub";
            lblSub.Size = new Size(199, 20);
            lblSub.TabIndex = 1;
            lblSub.Text = "Join as Customer or Owner";

            lblTitle.AutoSize = true;
            lblTitle.Font = new Font("Segoe UI", 24F, FontStyle.Bold);
            lblTitle.ForeColor = Color.FromArgb(35, 35, 35);
            lblTitle.Location = new Point(390, 35);
            lblTitle.Name = "lblTitle";
            lblTitle.Size = new Size(199, 45);
            lblTitle.TabIndex = 1;
            lblTitle.Text = "Registration";

            lblFullName.AutoSize = true;
            lblFullName.Font = new Font("Segoe UI", 10F);
            lblFullName.Location = new Point(390, 100);
            lblFullName.Name = "lblFullName";
            lblFullName.Size = new Size(71, 19);
            lblFullName.Text = "Full Name";

            txtFullName.Font = new Font("Segoe UI", 10F);
            txtFullName.Location = new Point(390, 125);
            txtFullName.Name = "txtFullName";
            txtFullName.Size = new Size(220, 25);
            txtFullName.TabIndex = 0;

            lblEmail.AutoSize = true;
            lblEmail.Font = new Font("Segoe UI", 10F);
            lblEmail.Location = new Point(390, 170);
            lblEmail.Name = "lblEmail";
            lblEmail.Size = new Size(41, 19);
            lblEmail.Text = "Email";

            txtEmail.Font = new Font("Segoe UI", 10F);
            txtEmail.Location = new Point(390, 195);
            txtEmail.Name = "txtEmail";
            txtEmail.Size = new Size(220, 25);
            txtEmail.TabIndex = 1;

            lblUsername.AutoSize = true;
            lblUsername.Font = new Font("Segoe UI", 10F);
            lblUsername.Location = new Point(390, 240);
            lblUsername.Name = "lblUsername";
            lblUsername.Size = new Size(71, 19);
            lblUsername.Text = "Username";

            txtUsername.Font = new Font("Segoe UI", 10F);
            txtUsername.Location = new Point(390, 265);
            txtUsername.Name = "txtUsername";
            txtUsername.Size = new Size(220, 25);
            txtUsername.TabIndex = 2;

            lblRole.AutoSize = true;
            lblRole.Font = new Font("Segoe UI", 10F);
            lblRole.Location = new Point(390, 310);
            lblRole.Name = "lblRole";
            lblRole.Size = new Size(75, 19);
            lblRole.Text = "Register As";

            cmbRole.DropDownStyle = ComboBoxStyle.DropDownList;
            cmbRole.Font = new Font("Segoe UI", 10F);
            cmbRole.Items.AddRange(new object[] { "Customer", "Owner" });
            cmbRole.Location = new Point(390, 335);
            cmbRole.Name = "cmbRole";
            cmbRole.Size = new Size(220, 25);
            cmbRole.TabIndex = 3;
            cmbRole.SelectedIndex = 0;

            lblPhone.AutoSize = true;
            lblPhone.Font = new Font("Segoe UI", 10F);
            lblPhone.Location = new Point(640, 100);
            lblPhone.Name = "lblPhone";
            lblPhone.Size = new Size(48, 19);
            lblPhone.Text = "Phone";

            txtPhone.Font = new Font("Segoe UI", 10F);
            txtPhone.Location = new Point(640, 125);
            txtPhone.Name = "txtPhone";
            txtPhone.Size = new Size(220, 25);
            txtPhone.TabIndex = 4;

            lblAddress.AutoSize = true;
            lblAddress.Font = new Font("Segoe UI", 10F);
            lblAddress.Location = new Point(640, 170);
            lblAddress.Name = "lblAddress";
            lblAddress.Size = new Size(58, 19);
            lblAddress.Text = "Address";

            txtAddress.Font = new Font("Segoe UI", 10F);
            txtAddress.Location = new Point(640, 195);
            txtAddress.Name = "txtAddress";
            txtAddress.Size = new Size(220, 25);
            txtAddress.TabIndex = 5;

            lblPassword.AutoSize = true;
            lblPassword.Font = new Font("Segoe UI", 10F);
            lblPassword.Location = new Point(640, 240);
            lblPassword.Name = "lblPassword";
            lblPassword.Size = new Size(67, 19);
            lblPassword.Text = "Password";

            txtPassword.Font = new Font("Segoe UI", 10F);
            txtPassword.Location = new Point(640, 265);
            txtPassword.Name = "txtPassword";
            txtPassword.Size = new Size(220, 25);
            txtPassword.TabIndex = 6;
            txtPassword.UseSystemPasswordChar = true;

            lblConfirmPassword.AutoSize = true;
            lblConfirmPassword.Font = new Font("Segoe UI", 10F);
            lblConfirmPassword.Location = new Point(640, 310);
            lblConfirmPassword.Name = "lblConfirmPassword";
            lblConfirmPassword.Size = new Size(119, 19);
            lblConfirmPassword.Text = "Confirm Password";

            txtConfirmPassword.Font = new Font("Segoe UI", 10F);
            txtConfirmPassword.Location = new Point(640, 335);
            txtConfirmPassword.Name = "txtConfirmPassword";
            txtConfirmPassword.Size = new Size(220, 25);
            txtConfirmPassword.TabIndex = 7;
            txtConfirmPassword.UseSystemPasswordChar = true;

            chkShowPassword.AutoSize = true;
            chkShowPassword.Font = new Font("Segoe UI", 9F);
            chkShowPassword.Location = new Point(640, 375);
            chkShowPassword.Name = "chkShowPassword";
            chkShowPassword.Size = new Size(108, 19);
            chkShowPassword.Text = "Show password";
            chkShowPassword.UseVisualStyleBackColor = true;
            chkShowPassword.CheckedChanged += chkShowPassword_CheckedChanged;

            btnRegister.BackColor = Color.FromArgb(24, 90, 157);
            btnRegister.FlatStyle = FlatStyle.Flat;
            btnRegister.Font = new Font("Segoe UI", 11F, FontStyle.Bold);
            btnRegister.ForeColor = Color.White;
            btnRegister.Location = new Point(390, 430);
            btnRegister.Name = "btnRegister";
            btnRegister.Size = new Size(220, 42);
            btnRegister.TabIndex = 8;
            btnRegister.Text = "Register";
            btnRegister.UseVisualStyleBackColor = false;
            btnRegister.Click += btnRegister_Click;

            btnBack.FlatStyle = FlatStyle.Flat;
            btnBack.Font = new Font("Segoe UI", 10F);
            btnBack.Location = new Point(640, 430);
            btnBack.Name = "btnBack";
            btnBack.Size = new Size(220, 42);
            btnBack.TabIndex = 9;
            btnBack.Text = "Back to Login";
            btnBack.UseVisualStyleBackColor = true;
            btnBack.Click += btnBack_Click;

            lblNote.AutoSize = true;
            lblNote.Font = new Font("Segoe UI", 9F);
            lblNote.ForeColor = Color.Gray;
            lblNote.Location = new Point(390, 510);
            lblNote.Name = "lblNote";
            lblNote.Size = new Size(467, 15);
            lblNote.Text = "Note: Admin and Super Admin accounts cannot be created from public registration.";

            AcceptButton = btnRegister;
            AutoScaleDimensions = new SizeF(7F, 15F);
            AutoScaleMode = AutoScaleMode.Font;
            BackColor = Color.FromArgb(245, 247, 250);
            ClientSize = new Size(884, 611);
            Controls.Add(leftPanel);
            Controls.Add(lblTitle);
            Controls.Add(lblFullName);
            Controls.Add(txtFullName);
            Controls.Add(lblEmail);
            Controls.Add(txtEmail);
            Controls.Add(lblUsername);
            Controls.Add(txtUsername);
            Controls.Add(lblRole);
            Controls.Add(cmbRole);
            Controls.Add(lblPhone);
            Controls.Add(txtPhone);
            Controls.Add(lblAddress);
            Controls.Add(txtAddress);
            Controls.Add(lblPassword);
            Controls.Add(txtPassword);
            Controls.Add(lblConfirmPassword);
            Controls.Add(txtConfirmPassword);
            Controls.Add(chkShowPassword);
            Controls.Add(btnRegister);
            Controls.Add(btnBack);
            Controls.Add(lblNote);
            FormBorderStyle = FormBorderStyle.FixedSingle;
            MaximizeBox = false;
            Name = "RegisterForm";
            StartPosition = FormStartPosition.CenterScreen;
            Text = "Car Rental Service - Registration";

            leftPanel.ResumeLayout(false);
            leftPanel.PerformLayout();
            ResumeLayout(false);
            PerformLayout();
        }
    }
}