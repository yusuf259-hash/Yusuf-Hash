using Car_Rent_Managment.UI;
using System.Drawing;
using System.Windows.Forms;

namespace Car_Rent_Managment
{
    partial class ReviewForm
    {
        private System.ComponentModel.IContainer components = null;

        private Label lblTitle;
        private Label lblCustomer;
        private Button btnRefresh;
        private DataGridView dgvCompletedBookings;
        private Panel reviewPanel;
        private Label lblPanelTitle;
        private Label lblSelectedBooking;
        private Label lblCar;
        private Label lblRating;
        private ComboBox cmbRating;
        private Label lblComment;
        private TextBox txtComment;
        private Button btnSubmitReview;

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
            lblCustomer = new Label();
            btnRefresh = new Button();
            dgvCompletedBookings = new DataGridView();
            reviewPanel = new Panel();
            lblPanelTitle = new Label();
            lblSelectedBooking = new Label();
            lblCar = new Label();
            lblRating = new Label();
            cmbRating = new ComboBox();
            lblComment = new Label();
            txtComment = new TextBox();
            btnSubmitReview = new Button();

            ((System.ComponentModel.ISupportInitialize)dgvCompletedBookings).BeginInit();
            reviewPanel.SuspendLayout();
            SuspendLayout();

            BackColor = UiTheme.Background;
            ClientSize = new Size(1084, 611);
            StartPosition = FormStartPosition.CenterScreen;
            Text = "Reviews";

            lblTitle.Text = "Reviews & Ratings";
            lblTitle.Font = UiTheme.TitleFont();
            lblTitle.ForeColor = UiTheme.TextDark;
            lblTitle.AutoSize = true;
            lblTitle.Location = new Point(30, 25);

            lblCustomer.Text = "Customer:";
            lblCustomer.Font = UiTheme.NormalFont();
            lblCustomer.ForeColor = UiTheme.TextMuted;
            lblCustomer.AutoSize = true;
            lblCustomer.Location = new Point(35, 75);

            btnRefresh.Text = "Refresh";
            btnRefresh.Location = new Point(610, 70);
            btnRefresh.Size = new Size(120, 38);
            UiTheme.StylePrimaryButton(btnRefresh);
            btnRefresh.Click += btnRefresh_Click;

            dgvCompletedBookings.Location = new Point(30, 120);
            dgvCompletedBookings.Size = new Size(700, 450);
            dgvCompletedBookings.CellClick += dgvCompletedBookings_CellClick;
            UiTheme.StyleGrid(dgvCompletedBookings);

            reviewPanel.Location = new Point(760, 120);
            reviewPanel.Size = new Size(300, 450);
            UiTheme.StylePanel(reviewPanel);

            lblPanelTitle.Text = "Write Review";
            lblPanelTitle.Font = UiTheme.HeaderFont();
            lblPanelTitle.ForeColor = UiTheme.TextDark;
            lblPanelTitle.AutoSize = true;
            lblPanelTitle.Location = new Point(20, 25);

            lblSelectedBooking.Text = "Selected Booking: None";
            lblSelectedBooking.Font = new Font("Segoe UI", 10F, FontStyle.Bold);
            lblSelectedBooking.Location = new Point(20, 80);
            lblSelectedBooking.Size = new Size(250, 45);

            lblCar.Text = "Car: -";
            lblCar.Font = UiTheme.NormalFont();
            lblCar.AutoSize = true;
            lblCar.Location = new Point(20, 135);

            lblRating.Text = "Rating";
            lblRating.Font = UiTheme.NormalFont();
            lblRating.AutoSize = true;
            lblRating.Location = new Point(20, 185);

            cmbRating.DropDownStyle = ComboBoxStyle.DropDownList;
            cmbRating.Font = UiTheme.NormalFont();
            cmbRating.Location = new Point(20, 215);
            cmbRating.Size = new Size(250, 25);

            lblComment.Text = "Comment";
            lblComment.Font = UiTheme.NormalFont();
            lblComment.AutoSize = true;
            lblComment.Location = new Point(20, 260);

            txtComment.Font = UiTheme.NormalFont();
            txtComment.Location = new Point(20, 290);
            txtComment.Multiline = true;
            txtComment.Size = new Size(250, 85);

            btnSubmitReview.Text = "Submit Review";
            btnSubmitReview.Location = new Point(20, 390);
            btnSubmitReview.Size = new Size(250, 40);
            UiTheme.StylePurpleButton(btnSubmitReview);
            btnSubmitReview.Click += btnSubmitReview_Click;

            reviewPanel.Controls.Add(lblPanelTitle);
            reviewPanel.Controls.Add(lblSelectedBooking);
            reviewPanel.Controls.Add(lblCar);
            reviewPanel.Controls.Add(lblRating);
            reviewPanel.Controls.Add(cmbRating);
            reviewPanel.Controls.Add(lblComment);
            reviewPanel.Controls.Add(txtComment);
            reviewPanel.Controls.Add(btnSubmitReview);

            Controls.Add(lblTitle);
            Controls.Add(lblCustomer);
            Controls.Add(btnRefresh);
            Controls.Add(dgvCompletedBookings);
            Controls.Add(reviewPanel);

            ((System.ComponentModel.ISupportInitialize)dgvCompletedBookings).EndInit();
            reviewPanel.ResumeLayout(false);
            reviewPanel.PerformLayout();
            ResumeLayout(false);
            PerformLayout();
        }
    }
}