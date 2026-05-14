using Car_Rent_Managment.UI;
using System.Drawing;
using System.Windows.Forms;

namespace Car_Rent_Managment
{
    partial class ManageAllBookingsForm
    {
        private System.ComponentModel.IContainer components = null;

        private Label lblTitle = null!;
        private Panel cardTotalBookings = null!;
        private Panel cardActiveBookings = null!;
        private Panel cardCompletedBookings = null!;
        private Panel cardCancelledBookings = null!;
        private Panel cardPaidBookings = null!;
        private Panel cardUnpaidBookings = null!;
        private Label lblTotalBookingsValue = null!;
        private Label lblActiveBookingsValue = null!;
        private Label lblCompletedBookingsValue = null!;
        private Label lblCancelledBookingsValue = null!;
        private Label lblPaidBookingsValue = null!;
        private Label lblUnpaidBookingsValue = null!;
        private DataGridView dgvBookings = null!;
        private Panel actionPanel = null!;
        private Label lblPanelTitle = null!;
        private Label lblSelectedBooking = null!;
        private Label lblCustomer = null!;
        private Label lblCar = null!;
        private Label lblStatus = null!;
        private Label lblFine = null!;
        private Button btnCancelBooking = null!;
        private Button btnMarkFinePaid = null!;
        private Button btnRefresh = null!;

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
            cardTotalBookings = new Panel();
            cardActiveBookings = new Panel();
            cardCompletedBookings = new Panel();
            cardCancelledBookings = new Panel();
            cardPaidBookings = new Panel();
            cardUnpaidBookings = new Panel();
            lblTotalBookingsValue = new Label();
            lblActiveBookingsValue = new Label();
            lblCompletedBookingsValue = new Label();
            lblCancelledBookingsValue = new Label();
            lblPaidBookingsValue = new Label();
            lblUnpaidBookingsValue = new Label();
            dgvBookings = new DataGridView();
            actionPanel = new Panel();
            lblPanelTitle = new Label();
            lblSelectedBooking = new Label();
            lblCustomer = new Label();
            lblCar = new Label();
            lblStatus = new Label();
            lblFine = new Label();
            btnCancelBooking = new Button();
            btnMarkFinePaid = new Button();
            btnRefresh = new Button();

            ((System.ComponentModel.ISupportInitialize)dgvBookings).BeginInit();
            actionPanel.SuspendLayout();
            SuspendLayout();

            BackColor = UiTheme.Background;
            ClientSize = new Size(1280, 760);
            StartPosition = FormStartPosition.CenterScreen;
            Text = "Manage All Bookings";

            lblTitle.Text = "Manage All Bookings";
            lblTitle.Font = UiTheme.TitleFont();
            lblTitle.ForeColor = UiTheme.TextDark;
            lblTitle.AutoSize = true;
            lblTitle.Location = new Point(30, 25);

            btnRefresh.Text = "Refresh";
            btnRefresh.Location = new Point(1110, 40);
            btnRefresh.Size = new Size(120, 38);
            UiTheme.StylePrimaryButton(btnRefresh);
            btnRefresh.Click += btnRefresh_Click;

            UiTheme.ConfigureStatCard(cardTotalBookings, lblTotalBookingsValue, "Total", "0", 30, 105, UiTheme.Primary, 170, 88, 16F);
            UiTheme.ConfigureStatCard(cardActiveBookings, lblActiveBookingsValue, "Active", "0", 220, 105, UiTheme.Warning, 170, 88, 16F);
            UiTheme.ConfigureStatCard(cardCompletedBookings, lblCompletedBookingsValue, "Completed", "0", 410, 105, UiTheme.Success, 170, 88, 16F);
            UiTheme.ConfigureStatCard(cardCancelledBookings, lblCancelledBookingsValue, "Cancelled", "0", 600, 105, UiTheme.Danger, 170, 88, 16F);
            UiTheme.ConfigureStatCard(cardPaidBookings, lblPaidBookingsValue, "Paid", "0", 790, 105, UiTheme.Purple, 170, 88, 16F);
            UiTheme.ConfigureStatCard(cardUnpaidBookings, lblUnpaidBookingsValue, "Unpaid", "0", 980, 105, UiTheme.Warning, 170, 88, 16F);

            dgvBookings.Location = new Point(30, 220);
            dgvBookings.Size = new Size(850, 485);
            UiTheme.StyleGrid(dgvBookings);
            dgvBookings.CellClick += dgvBookings_CellClick;

            actionPanel.Location = new Point(910, 220);
            actionPanel.Size = new Size(330, 485);
            UiTheme.StylePanel(actionPanel);

            lblPanelTitle.Text = "Booking Actions";
            lblPanelTitle.Font = UiTheme.HeaderFont();
            lblPanelTitle.ForeColor = UiTheme.TextDark;
            lblPanelTitle.AutoSize = true;
            lblPanelTitle.Location = new Point(20, 25);

            lblSelectedBooking.Text = "Selected Booking: None";
            lblSelectedBooking.Font = new Font("Segoe UI", 10F, FontStyle.Bold);
            lblSelectedBooking.ForeColor = UiTheme.TextDark;
            lblSelectedBooking.Location = new Point(20, 85);
            lblSelectedBooking.Size = new Size(280, 35);

            lblCustomer.Text = "Customer: -";
            lblCustomer.Font = UiTheme.NormalFont();
            lblCustomer.ForeColor = UiTheme.TextMuted;
            lblCustomer.Location = new Point(20, 130);
            lblCustomer.Size = new Size(280, 32);

            lblCar.Text = "Car: -";
            lblCar.Font = UiTheme.NormalFont();
            lblCar.ForeColor = UiTheme.TextMuted;
            lblCar.Location = new Point(20, 170);
            lblCar.Size = new Size(280, 32);

            lblStatus.Text = "Status: -";
            lblStatus.Font = UiTheme.NormalFont();
            lblStatus.ForeColor = UiTheme.TextMuted;
            lblStatus.Location = new Point(20, 210);
            lblStatus.Size = new Size(280, 32);

            lblFine.Text = "Fine: -";
            lblFine.Font = UiTheme.NormalFont();
            lblFine.ForeColor = UiTheme.TextMuted;
            lblFine.Location = new Point(20, 250);
            lblFine.Size = new Size(280, 45);

            btnCancelBooking.Text = "Cancel Active Booking";
            btnCancelBooking.Location = new Point(20, 330);
            btnCancelBooking.Size = new Size(280, 40);
            UiTheme.StyleDangerButton(btnCancelBooking);
            btnCancelBooking.Click += btnCancelBooking_Click;

            btnMarkFinePaid.Text = "Mark Fine Paid";
            btnMarkFinePaid.Location = new Point(20, 390);
            btnMarkFinePaid.Size = new Size(280, 40);
            UiTheme.StyleSuccessButton(btnMarkFinePaid);
            btnMarkFinePaid.Click += btnMarkFinePaid_Click;

            actionPanel.Controls.Add(lblPanelTitle);
            actionPanel.Controls.Add(lblSelectedBooking);
            actionPanel.Controls.Add(lblCustomer);
            actionPanel.Controls.Add(lblCar);
            actionPanel.Controls.Add(lblStatus);
            actionPanel.Controls.Add(lblFine);
            actionPanel.Controls.Add(btnCancelBooking);
            actionPanel.Controls.Add(btnMarkFinePaid);

            Controls.Add(lblTitle);
            Controls.Add(btnRefresh);
            Controls.Add(cardTotalBookings);
            Controls.Add(cardActiveBookings);
            Controls.Add(cardCompletedBookings);
            Controls.Add(cardCancelledBookings);
            Controls.Add(cardPaidBookings);
            Controls.Add(cardUnpaidBookings);
            Controls.Add(dgvBookings);
            Controls.Add(actionPanel);

            ((System.ComponentModel.ISupportInitialize)dgvBookings).EndInit();
            actionPanel.ResumeLayout(false);
            actionPanel.PerformLayout();
            ResumeLayout(false);
            PerformLayout();
        }
    }
}
