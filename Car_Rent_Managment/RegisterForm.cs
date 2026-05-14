using Car_Rent_Managment.Services;
using System;
using System.Windows.Forms;

namespace Car_Rent_Managment
{
    public partial class RegisterForm : Form
    {
        public RegisterForm()
        {
            InitializeComponent();
        }

        private void chkShowPassword_CheckedChanged(object sender, EventArgs e)
        {
            bool show = chkShowPassword.Checked;
            txtPassword.UseSystemPasswordChar = !show;
            txtConfirmPassword.UseSystemPasswordChar = !show;
        }

        private void btnRegister_Click(object sender, EventArgs e)
        {
            string fullName = txtFullName.Text.Trim();
            string email = txtEmail.Text.Trim();
            string username = txtUsername.Text.Trim();
            string password = txtPassword.Text.Trim();
            string confirmPassword = txtConfirmPassword.Text.Trim();
            string phone = txtPhone.Text.Trim();
            string address = txtAddress.Text.Trim();
            string role = cmbRole.SelectedItem.ToString();

            if (string.IsNullOrWhiteSpace(fullName))
            {
                MessageBox.Show("Please enter full name.");
                txtFullName.Focus();
                return;
            }

            if (string.IsNullOrWhiteSpace(email))
            {
                MessageBox.Show("Please enter email.");
                txtEmail.Focus();
                return;
            }

            if (!email.Contains("@") || !email.Contains("."))
            {
                MessageBox.Show("Please enter a valid email.");
                txtEmail.Focus();
                return;
            }

            if (string.IsNullOrWhiteSpace(username))
            {
                MessageBox.Show("Please enter username.");
                txtUsername.Focus();
                return;
            }

            if (username.Length < 4)
            {
                MessageBox.Show("Username must be at least 4 characters.");
                txtUsername.Focus();
                return;
            }

            if (string.IsNullOrWhiteSpace(password))
            {
                MessageBox.Show("Please enter password.");
                txtPassword.Focus();
                return;
            }

            if (password.Length < 4)
            {
                MessageBox.Show("Password must be at least 4 characters.");
                txtPassword.Focus();
                return;
            }

            if (password != confirmPassword)
            {
                MessageBox.Show("Password and confirm password do not match.");
                txtConfirmPassword.Focus();
                return;
            }

            if (string.IsNullOrWhiteSpace(phone))
            {
                MessageBox.Show("Please enter phone number.");
                txtPhone.Focus();
                return;
            }

            UserService userService = new UserService();

            bool success = userService.RegisterUser(
                fullName,
                email,
                username,
                password,
                role,
                phone,
                address,
                out string message
            );

            MessageBox.Show(message);

            if (success)
            {
                Close();
            }
        }

        private void btnBack_Click(object sender, EventArgs e)
        {
            Close();
        }
    }
}