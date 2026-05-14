using Car_Rent_Managment.Models;
using Car_Rent_Managment.Services;
using System;
using System.Data;
using System.Windows.Forms;

namespace Car_Rent_Managment
{
    public partial class ManageUsersForm : Form
    {
        private readonly AuthenticatedUser currentUser;
        private readonly AdminService adminService;
        private int selectedUserId = 0;

        public ManageUsersForm(AuthenticatedUser user)
        {
            InitializeComponent();

            currentUser = user;
            adminService = new AdminService();

            SetupRoleAccess();
            lblInfo.Text = "Logged in as: " + currentUser.FullName + " (" + currentUser.Role + ")";
            LoadUsers();
        }

        private void SetupRoleAccess()
        {
            cmbRole.Items.Clear();

            cmbRole.Items.Add("Customer");
            cmbRole.Items.Add("Owner");

            if (currentUser.Role == "SuperAdmin")
            {
                cmbRole.Items.Add("Admin");
            }

            cmbRole.SelectedIndex = 0;

            cmbStatus.Items.Clear();
            cmbStatus.Items.Add("Active");
            cmbStatus.Items.Add("Suspended");
            cmbStatus.SelectedIndex = 0;
        }

        private void LoadUsers()
        {
            DataTable usersTable = adminService.GetUsersForManager(currentUser.Role);
            dgvUsers.DataSource = usersTable;
            LoadUserSummary();

            if (dgvUsers.Columns["UserId"] != null)
            {
                dgvUsers.Columns["UserId"].Visible = false;
            }

            ClearForm();
        }

        private void LoadUserSummary()
        {
            DataTable summaryTable = adminService.GetUserSummaryForManager(currentUser.Role);

            if (summaryTable.Rows.Count == 0)
            {
                return;
            }

            DataRow row = summaryTable.Rows[0];
            lblCustomersValue.Text = ReadInt(row, "Customers").ToString();
            lblOwnersValue.Text = ReadInt(row, "Owners").ToString();
            lblAdminsValue.Text = ReadInt(row, "Admins").ToString();
            lblSuspendedUsersValue.Text = ReadInt(row, "SuspendedUsers").ToString();
        }

        private void dgvUsers_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex < 0)
            {
                return;
            }

            DataGridViewRow row = dgvUsers.Rows[e.RowIndex];

            selectedUserId = Convert.ToInt32(row.Cells["UserId"].Value);
            txtFullName.Text = row.Cells["FullName"].Value.ToString();
            txtEmail.Text = row.Cells["Email"].Value.ToString();
            txtUsername.Text = row.Cells["Username"].Value.ToString();
            txtPassword.Clear();
            txtPhone.Text = row.Cells["Phone"].Value.ToString();
            txtAddress.Text = row.Cells["Address"].Value.ToString();

            string role = row.Cells["Role"].Value.ToString() ?? "";
            string status = row.Cells["Status"].Value.ToString() ?? "";

            if (cmbRole.Items.Contains(role))
            {
                cmbRole.SelectedItem = role;
            }

            if (cmbStatus.Items.Contains(status))
            {
                cmbStatus.SelectedItem = status;
            }
        }

        private void btnAdd_Click(object sender, EventArgs e)
        {
            if (!ValidateInput(true))
            {
                return;
            }

            bool success = adminService.AddUser(
                currentUser.Role,
                txtFullName.Text.Trim(),
                txtEmail.Text.Trim(),
                txtUsername.Text.Trim(),
                txtPassword.Text.Trim(),
                cmbRole.SelectedItem.ToString() ?? "Customer",
                txtPhone.Text.Trim(),
                txtAddress.Text.Trim(),
                cmbStatus.SelectedItem.ToString() ?? "Active",
                out string message
            );

            MessageBox.Show(message);

            if (success)
            {
                LoadUsers();
            }
        }

        private void btnUpdate_Click(object sender, EventArgs e)
        {
            if (selectedUserId == 0)
            {
                MessageBox.Show("Please select a user to update.");
                return;
            }

            if (!ValidateInput(false))
            {
                return;
            }

            bool success = adminService.UpdateUser(
                currentUser.Role,
                selectedUserId,
                currentUser.UserId,
                txtFullName.Text.Trim(),
                txtEmail.Text.Trim(),
                txtUsername.Text.Trim(),
                txtPassword.Text.Trim(),
                cmbRole.SelectedItem.ToString() ?? "Customer",
                txtPhone.Text.Trim(),
                txtAddress.Text.Trim(),
                cmbStatus.SelectedItem.ToString() ?? "Active",
                out string message
            );

            MessageBox.Show(message);

            if (success)
            {
                LoadUsers();
            }
        }

        private void btnSuspend_Click(object sender, EventArgs e)
        {
            if (selectedUserId == 0)
            {
                MessageBox.Show("Please select a user first.");
                return;
            }

            bool success = adminService.SuspendUser(
                currentUser.Role,
                selectedUserId,
                currentUser.UserId,
                out string message
            );

            MessageBox.Show(message);

            if (success)
            {
                LoadUsers();
            }
        }

        private void btnActivate_Click(object sender, EventArgs e)
        {
            if (selectedUserId == 0)
            {
                MessageBox.Show("Please select a user first.");
                return;
            }

            bool success = adminService.ActivateUser(
                currentUser.Role,
                selectedUserId,
                currentUser.UserId,
                out string message
            );

            MessageBox.Show(message);

            if (success)
            {
                LoadUsers();
            }
        }

        private void btnDelete_Click(object sender, EventArgs e)
        {
            if (selectedUserId == 0)
            {
                MessageBox.Show("Please select a user first.");
                return;
            }

            DialogResult result = MessageBox.Show(
                "Are you sure you want to delete this user?\n\nIf the user has related records, the account will be suspended instead.",
                "Confirm Delete",
                MessageBoxButtons.YesNo,
                MessageBoxIcon.Warning
            );

            if (result != DialogResult.Yes)
            {
                return;
            }

            bool success = adminService.DeleteUserSafe(
                currentUser.Role,
                selectedUserId,
                currentUser.UserId,
                out string message
            );

            MessageBox.Show(message);

            if (success)
            {
                LoadUsers();
            }
        }

        private void btnClear_Click(object sender, EventArgs e)
        {
            ClearForm();
        }

        private void btnRefresh_Click(object sender, EventArgs e)
        {
            LoadUsers();
        }

        private bool ValidateInput(bool passwordRequired)
        {
            if (string.IsNullOrWhiteSpace(txtFullName.Text))
            {
                MessageBox.Show("Please enter full name.");
                txtFullName.Focus();
                return false;
            }

            if (string.IsNullOrWhiteSpace(txtEmail.Text) || !txtEmail.Text.Contains("@") || !txtEmail.Text.Contains("."))
            {
                MessageBox.Show("Please enter a valid email.");
                txtEmail.Focus();
                return false;
            }

            if (string.IsNullOrWhiteSpace(txtUsername.Text) || txtUsername.Text.Trim().Length < 4)
            {
                MessageBox.Show("Username must be at least 4 characters.");
                txtUsername.Focus();
                return false;
            }

            if (passwordRequired && string.IsNullOrWhiteSpace(txtPassword.Text))
            {
                MessageBox.Show("Please enter password.");
                txtPassword.Focus();
                return false;
            }

            if (!string.IsNullOrWhiteSpace(txtPassword.Text) && txtPassword.Text.Trim().Length < 4)
            {
                MessageBox.Show("Password must be at least 4 characters.");
                txtPassword.Focus();
                return false;
            }

            if (cmbRole.SelectedItem == null)
            {
                MessageBox.Show("Please select role.");
                return false;
            }

            if (cmbStatus.SelectedItem == null)
            {
                MessageBox.Show("Please select status.");
                return false;
            }

            return true;
        }

        private void ClearForm()
        {
            selectedUserId = 0;

            txtFullName.Clear();
            txtEmail.Clear();
            txtUsername.Clear();
            txtPassword.Clear();
            txtPhone.Clear();
            txtAddress.Clear();

            if (cmbRole.Items.Count > 0)
            {
                cmbRole.SelectedIndex = 0;
            }

            if (cmbStatus.Items.Count > 0)
            {
                cmbStatus.SelectedIndex = 0;
            }

            dgvUsers.ClearSelection();
        }

        private int ReadInt(DataRow row, string columnName)
        {
            if (row[columnName] == DBNull.Value)
            {
                return 0;
            }

            return Convert.ToInt32(row[columnName]);
        }
    }
}
