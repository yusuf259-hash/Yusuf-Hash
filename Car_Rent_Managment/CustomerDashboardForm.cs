using Car_Rent_Managment.Models;
using System;
using System.Windows.Forms;

namespace Car_Rent_Managment
{
    public partial class CustomerDashboardForm : Form
    {
        private readonly AuthenticatedUser currentUser;

        public CustomerDashboardForm(AuthenticatedUser user)
        {
            InitializeComponent();

            currentUser = user;
            lblWelcome.Text = "Welcome, " + currentUser.FullName;
        }

        private void btnRentCar_Click(object sender, EventArgs e)
        {
            RentCarForm rentCarForm = new RentCarForm(currentUser);
            rentCarForm.ShowDialog();
        }

        private void btnPaymentReturn_Click(object sender, EventArgs e)
        {
            PaymentReturnForm paymentReturnForm = new PaymentReturnForm(currentUser);
            paymentReturnForm.ShowDialog();
        }

        private void btnReviews_Click(object sender, EventArgs e)
        {
            ReviewForm reviewForm = new ReviewForm(currentUser);
            reviewForm.ShowDialog();
        }

        private void btnBookingHistory_Click(object sender, EventArgs e)
        {
            BookingHistoryForm bookingHistoryForm = new BookingHistoryForm(currentUser, "Customer");
            bookingHistoryForm.ShowDialog();
        }

        private void btnOffers_Click(object sender, EventArgs e)
        {
            OffersForm offersForm = new OffersForm();
            offersForm.ShowDialog();
        }

        private void btnLogout_Click(object sender, EventArgs e)
        {
            Close();
        }
    }
}