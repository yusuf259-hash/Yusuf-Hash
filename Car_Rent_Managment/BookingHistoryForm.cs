using Car_Rent_Managment.Models;
using Car_Rent_Managment.Services;
using System.Data;
using System.Windows.Forms;

namespace Car_Rent_Managment
{
    public partial class BookingHistoryForm : Form
    {
        private readonly AuthenticatedUser currentUser;
        private readonly string historyMode;
        private readonly BookingService bookingService;

        public BookingHistoryForm(AuthenticatedUser user, string mode)
        {
            InitializeComponent();

            currentUser = user;
            historyMode = mode;
            bookingService = new BookingService();

            SetupTitle();
            LoadBookingHistory();
        }

        private void SetupTitle()
        {
            if (historyMode == "Customer")
            {
                Text = "My Booking History";
                lblTitle.Text = "My Booking History";
                lblInfo.Text = "Customer: " + currentUser.FullName;
            }
            else if (historyMode == "Owner")
            {
                Text = "Bookings For My Cars";
                lblTitle.Text = "Bookings For My Cars";
                lblInfo.Text = "Owner: " + currentUser.FullName;
            }
            else
            {
                Text = "Booking History";
                lblTitle.Text = "Booking History";
                lblInfo.Text = currentUser.FullName;
            }
        }

        private void LoadBookingHistory()
        {
            DataTable table;

            if (historyMode == "Customer")
            {
                table = bookingService.GetCustomerBookingHistory(currentUser.UserId);
            }
            else
            {
                table = bookingService.GetOwnerCarBookings(currentUser.UserId);
            }

            dgvBookingHistory.DataSource = table;

            if (dgvBookingHistory.Columns["BookingId"] != null)
            {
                dgvBookingHistory.Columns["BookingId"].Visible = false;
            }

            dgvBookingHistory.ClearSelection();
        }

        private void btnRefresh_Click(object sender, System.EventArgs e)
        {
            LoadBookingHistory();
        }

        private void btnClose_Click(object sender, System.EventArgs e)
        {
            Close();
        }
    }
}