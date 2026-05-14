using Car_Rent_Managment.Models;
using Car_Rent_Managment.Services;
using System;
using System.Drawing;
using System.IO;
using System.Windows.Forms;

namespace Car_Rent_Managment
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
            LoadLoginCarImage();
        }

        private void LoadLoginCarImage()
        {
            string imagePath = ResolveAssetPath("Cars.png");

            if (string.IsNullOrWhiteSpace(imagePath) || !File.Exists(imagePath))
            {
                picLoginCar.Visible = false;
                return;
            }

            try
            {
                using (FileStream stream = new FileStream(imagePath, FileMode.Open, FileAccess.Read))
                {
                    picLoginCar.Image = Image.FromStream(stream);
                }

                picLoginCar.Visible = true;
            }
            catch
            {
                picLoginCar.Image = null;
                picLoginCar.Visible = false;
            }
        }

        private string ResolveAssetPath(string fileName)
        {
            string baseDirectory = AppDomain.CurrentDomain.BaseDirectory;

            string outputPath = Path.Combine(baseDirectory, "Assets", fileName);
            if (File.Exists(outputPath))
            {
                return outputPath;
            }

            DirectoryInfo? directory = new DirectoryInfo(baseDirectory);

            for (int i = 0; i < 8 && directory != null; i++)
            {
                string projectPath = Path.Combine(directory.FullName, "Assets", fileName);

                if (File.Exists(projectPath))
                {
                    return projectPath;
                }

                directory = directory.Parent;
            }

            return "";
        }

        private void chkShowPassword_CheckedChanged(object sender, EventArgs e)
        {
            txtPassword.UseSystemPasswordChar = !chkShowPassword.Checked;
        }

        private void btnLogin_Click(object sender, EventArgs e)
        {
            string usernameOrEmail = txtUsername.Text.Trim();
            string password = txtPassword.Text.Trim();

            if (string.IsNullOrWhiteSpace(usernameOrEmail))
            {
                MessageBox.Show("Please enter your username or email.");
                txtUsername.Focus();
                return;
            }

            if (string.IsNullOrWhiteSpace(password))
            {
                MessageBox.Show("Please enter your password.");
                txtPassword.Focus();
                return;
            }

            AuthService authService = new AuthService();
            AuthenticatedUser user = authService.Login(usernameOrEmail, password);

            if (user == null)
            {
                MessageBox.Show(
                    "Invalid username/email or password.",
                    "Login Failed",
                    MessageBoxButtons.OK,
                    MessageBoxIcon.Error
                );
                return;
            }

            OpenDashboard(user);
        }

        private void OpenDashboard(AuthenticatedUser user)
        {
            Form dashboard = null;

            if (user.Role == "Customer")
            {
                dashboard = new CustomerDashboardForm(user);
            }
            else if (user.Role == "Owner")
            {
                dashboard = new OwnerDashboardForm(user);
            }
            else if (user.Role == "Admin")
            {
                dashboard = new AdminDashboardForm(user);
            }
            else if (user.Role == "SuperAdmin")
            {
                dashboard = new SuperAdminDashboardForm(user);
            }
            else
            {
                MessageBox.Show("Unknown user role.");
                return;
            }

            this.Hide();
            dashboard.ShowDialog();
            this.Show();

            txtPassword.Clear();
            txtUsername.Focus();
        }

        private void btnRegister_Click(object sender, EventArgs e)
        {
            RegisterForm registerForm = new RegisterForm();
            registerForm.ShowDialog();
        }
    }
}
