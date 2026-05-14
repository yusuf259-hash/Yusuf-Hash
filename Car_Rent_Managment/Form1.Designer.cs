using System.Drawing;
using System.Windows.Forms;

namespace Car_Rent_Managment
{
    partial class Form1
    {
        private System.ComponentModel.IContainer components = null;

        private Panel leftPanel;
        private Label lblBrand;
        private Label lblBrandSub;
        private Label lblTitle;
        private Label lblUsername;
        private TextBox txtUsername;
        private Label lblPassword;
        private TextBox txtPassword;
        private CheckBox chkShowPassword;
        private Button btnLogin;
        private Button btnRegister;
        private PictureBox picLoginCar;

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
            lblBrandSub = new Label();
            lblTitle = new Label();
            lblUsername = new Label();
            txtUsername = new TextBox();
            lblPassword = new Label();
            txtPassword = new TextBox();
            chkShowPassword = new CheckBox();
            btnLogin = new Button();
            btnRegister = new Button();
            picLoginCar = new PictureBox();
            leftPanel.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)picLoginCar).BeginInit();
            SuspendLayout();
            // 
            // leftPanel
            // 
            leftPanel.BackColor = Color.FromArgb(24, 90, 157);
            leftPanel.Controls.Add(lblBrand);
            leftPanel.Controls.Add(lblBrandSub);
            leftPanel.Controls.Add(picLoginCar);
            leftPanel.Location = new Point(0, 0);
            leftPanel.Name = "leftPanel";
            leftPanel.Size = new Size(380, 550);
            leftPanel.TabIndex = 0;
            // 
            // lblBrand
            // 
            lblBrand.AutoSize = true;
            lblBrand.Font = new Font("Segoe UI", 28F, FontStyle.Bold);
            lblBrand.ForeColor = Color.White;
            lblBrand.Location = new Point(45, 150);
            lblBrand.Name = "lblBrand";
            lblBrand.Size = new Size(205, 102);
            lblBrand.TabIndex = 0;
            lblBrand.Text = "Car Rental\r\nService";
            // 
            // lblBrandSub
            // 
            lblBrandSub.AutoSize = true;
            lblBrandSub.Font = new Font("Segoe UI", 12F);
            lblBrandSub.ForeColor = Color.WhiteSmoke;
            lblBrandSub.Location = new Point(50, 260);
            lblBrandSub.Name = "lblBrandSub";
            lblBrandSub.Size = new Size(162, 21);
            lblBrandSub.TabIndex = 1;
            lblBrandSub.Text = "Rent. Return. Manage.";
            // 
            // lblTitle
            // 
            lblTitle.AutoSize = true;
            lblTitle.Font = new Font("Segoe UI", 26F, FontStyle.Bold);
            lblTitle.ForeColor = Color.FromArgb(35, 35, 35);
            lblTitle.Location = new Point(470, 80);
            lblTitle.Name = "lblTitle";
            lblTitle.Size = new Size(112, 47);
            lblTitle.TabIndex = 1;
            lblTitle.Text = "Login";
            // 
            // lblUsername
            // 
            lblUsername.AutoSize = true;
            lblUsername.Font = new Font("Segoe UI", 10F);
            lblUsername.Location = new Point(475, 155);
            lblUsername.Name = "lblUsername";
            lblUsername.Size = new Size(124, 19);
            lblUsername.TabIndex = 2;
            lblUsername.Text = "Username or Email";
            // 
            // txtUsername
            // 
            txtUsername.Font = new Font("Segoe UI", 11F);
            txtUsername.Location = new Point(475, 185);
            txtUsername.Name = "txtUsername";
            txtUsername.Size = new Size(300, 27);
            txtUsername.TabIndex = 0;
            // 
            // lblPassword
            // 
            lblPassword.AutoSize = true;
            lblPassword.Font = new Font("Segoe UI", 10F);
            lblPassword.Location = new Point(475, 235);
            lblPassword.Name = "lblPassword";
            lblPassword.Size = new Size(67, 19);
            lblPassword.TabIndex = 4;
            lblPassword.Text = "Password";
            // 
            // txtPassword
            // 
            txtPassword.Font = new Font("Segoe UI", 11F);
            txtPassword.Location = new Point(475, 265);
            txtPassword.Name = "txtPassword";
            txtPassword.Size = new Size(300, 27);
            txtPassword.TabIndex = 1;
            txtPassword.UseSystemPasswordChar = true;
            // 
            // chkShowPassword
            // 
            chkShowPassword.AutoSize = true;
            chkShowPassword.Font = new Font("Segoe UI", 9F);
            chkShowPassword.Location = new Point(475, 305);
            chkShowPassword.Name = "chkShowPassword";
            chkShowPassword.Size = new Size(108, 19);
            chkShowPassword.TabIndex = 2;
            chkShowPassword.Text = "Show password";
            chkShowPassword.UseVisualStyleBackColor = true;
            chkShowPassword.CheckedChanged += chkShowPassword_CheckedChanged;
            // 
            // btnLogin
            // 
            btnLogin.BackColor = Color.FromArgb(24, 90, 157);
            btnLogin.FlatStyle = FlatStyle.Flat;
            btnLogin.Font = new Font("Segoe UI", 11F, FontStyle.Bold);
            btnLogin.ForeColor = Color.White;
            btnLogin.Location = new Point(475, 350);
            btnLogin.Name = "btnLogin";
            btnLogin.Size = new Size(300, 42);
            btnLogin.TabIndex = 3;
            btnLogin.Text = "Login";
            btnLogin.UseVisualStyleBackColor = false;
            btnLogin.Click += btnLogin_Click;
            // 
            // btnRegister
            // 
            btnRegister.FlatStyle = FlatStyle.Flat;
            btnRegister.Font = new Font("Segoe UI", 10F);
            btnRegister.Location = new Point(475, 405);
            btnRegister.Name = "btnRegister";
            btnRegister.Size = new Size(300, 38);
            btnRegister.TabIndex = 4;
            btnRegister.Text = "Create Customer / Owner Account";
            btnRegister.UseVisualStyleBackColor = true;
            btnRegister.Click += btnRegister_Click;
            // 
            // picLoginCar
            // 
            picLoginCar.Location = new Point(50, 340);
            picLoginCar.Name = "picLoginCar";
            picLoginCar.Size = new Size(220, 173);
            picLoginCar.SizeMode = PictureBoxSizeMode.Zoom;
            picLoginCar.TabIndex = 10;
            picLoginCar.TabStop = false;
            // 
            // Form1
            // 
            AcceptButton = btnLogin;
            AutoScaleDimensions = new SizeF(7F, 15F);
            AutoScaleMode = AutoScaleMode.Font;
            BackColor = Color.FromArgb(245, 247, 250);
            ClientSize = new Size(1040, 550);
            Controls.Add(leftPanel);
            Controls.Add(lblTitle);
            Controls.Add(lblUsername);
            Controls.Add(txtUsername);
            Controls.Add(lblPassword);
            Controls.Add(txtPassword);
            Controls.Add(chkShowPassword);
            Controls.Add(btnLogin);
            Controls.Add(btnRegister);
            FormBorderStyle = FormBorderStyle.FixedSingle;
            MaximizeBox = false;
            Name = "Form1";
            StartPosition = FormStartPosition.CenterScreen;
            Text = "Car Rental Service - Login";
            leftPanel.ResumeLayout(false);
            leftPanel.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)picLoginCar).EndInit();
            ResumeLayout(false);
            PerformLayout();
        }
    }
}
