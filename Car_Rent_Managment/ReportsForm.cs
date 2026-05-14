using Car_Rent_Managment.Services;
using System;
using System.Data;
using System.Windows.Forms;

namespace Car_Rent_Managment
{
    public partial class ReportsForm : Form
    {
        private readonly AdminService adminService;

        public ReportsForm()
        {
            InitializeComponent();

            adminService = new AdminService();
            LoadReports();
        }

        private void LoadReports()
        {
            DataTable reportTable = adminService.GetReports();
            dgvReports.DataSource = reportTable;
            dgvReports.ClearSelection();
        }

        private void btnRefresh_Click(object sender, EventArgs e)
        {
            LoadReports();
        }

        private void btnClose_Click(object sender, EventArgs e)
        {
            Close();
        }
    }
}