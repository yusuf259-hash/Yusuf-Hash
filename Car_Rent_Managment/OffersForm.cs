using Car_Rent_Managment.Services;
using System;
using System.Data;
using System.Windows.Forms;

namespace Car_Rent_Managment
{
    public partial class OffersForm : Form
    {
        private readonly CustomerFeatureService customerFeatureService;

        public OffersForm()
        {
            InitializeComponent();

            customerFeatureService = new CustomerFeatureService();
            LoadOffers();
        }

        private void LoadOffers()
        {
            DataTable table = customerFeatureService.GetActiveOffersForCustomer();
            dgvOffers.DataSource = table;

            if (dgvOffers.Columns["OfferId"] != null)
            {
                dgvOffers.Columns["OfferId"].Visible = false;
            }

            dgvOffers.ClearSelection();
        }

        private void btnRefresh_Click(object sender, EventArgs e)
        {
            LoadOffers();
        }

        private void btnClose_Click(object sender, EventArgs e)
        {
            Close();
        }
    }
}