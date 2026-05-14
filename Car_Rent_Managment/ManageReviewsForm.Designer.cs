using Car_Rent_Managment.UI;
using System.Drawing;
using System.Windows.Forms;

namespace Car_Rent_Managment
{
    partial class ManageReviewsForm
    {
        private System.ComponentModel.IContainer components = null;

        private Label lblTitle = null!;
        private Label lblSubtitle = null!;
        private Label lblUser = null!;
        private Panel cardTotalReviews = null!;
        private Panel cardAverageRating = null!;
        private Panel cardLowRatings = null!;
        private Panel cardFiveStarReviews = null!;
        private Label lblTotalReviewsValue = null!;
        private Label lblAverageRatingValue = null!;
        private Label lblLowRatingsValue = null!;
        private Label lblFiveStarReviewsValue = null!;
        private Panel filterPanel = null!;
        private Label lblFilterTitle = null!;
        private Label lblRating = null!;
        private ComboBox cmbRating = null!;
        private Label lblCustomerName = null!;
        private TextBox txtCustomerName = null!;
        private Label lblOwnerName = null!;
        private TextBox txtOwnerName = null!;
        private Label lblCarName = null!;
        private TextBox txtCarName = null!;
        private Button btnApplyFilter = null!;
        private Button btnClearFilter = null!;
        private Button btnRefresh = null!;
        private DataGridView dgvReviews = null!;
        private Panel actionPanel = null!;
        private Label lblActionTitle = null!;
        private Label lblSelectedReview = null!;
        private Label lblSelectedOwner = null!;
        private Button btnDeleteReview = null!;
        private Button btnSuspendOwner = null!;

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
            lblSubtitle = new Label();
            lblUser = new Label();
            cardTotalReviews = new Panel();
            cardAverageRating = new Panel();
            cardLowRatings = new Panel();
            cardFiveStarReviews = new Panel();
            lblTotalReviewsValue = new Label();
            lblAverageRatingValue = new Label();
            lblLowRatingsValue = new Label();
            lblFiveStarReviewsValue = new Label();
            filterPanel = new Panel();
            lblFilterTitle = new Label();
            lblRating = new Label();
            cmbRating = new ComboBox();
            lblCustomerName = new Label();
            txtCustomerName = new TextBox();
            lblOwnerName = new Label();
            txtOwnerName = new TextBox();
            lblCarName = new Label();
            txtCarName = new TextBox();
            btnApplyFilter = new Button();
            btnClearFilter = new Button();
            btnRefresh = new Button();
            dgvReviews = new DataGridView();
            actionPanel = new Panel();
            lblActionTitle = new Label();
            lblSelectedReview = new Label();
            lblSelectedOwner = new Label();
            btnDeleteReview = new Button();
            btnSuspendOwner = new Button();

            filterPanel.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)dgvReviews).BeginInit();
            actionPanel.SuspendLayout();
            SuspendLayout();

            BackColor = UiTheme.Background;
            ClientSize = new Size(1220, 760);
            StartPosition = FormStartPosition.CenterScreen;
            Text = "Manage Reviews";

            lblTitle.Text = "Manage Reviews";
            lblTitle.Font = UiTheme.TitleFont();
            lblTitle.ForeColor = UiTheme.TextDark;
            lblTitle.AutoSize = true;
            lblTitle.Location = new Point(30, 25);

            lblSubtitle.Text = "Review customer feedback and moderate inappropriate comments.";
            lblSubtitle.Font = UiTheme.NormalFont();
            lblSubtitle.ForeColor = UiTheme.TextMuted;
            lblSubtitle.AutoSize = true;
            lblSubtitle.Location = new Point(35, 75);

            lblUser.Text = "User:";
            lblUser.Font = new Font("Segoe UI", 10F, FontStyle.Bold);
            lblUser.ForeColor = UiTheme.TextDark;
            lblUser.AutoSize = true;
            lblUser.Location = new Point(35, 100);

            UiTheme.ConfigureStatCard(cardTotalReviews, lblTotalReviewsValue, "Total Reviews", "0", 30, 135, UiTheme.Primary, 200, 85, 16F);
            UiTheme.ConfigureStatCard(cardAverageRating, lblAverageRatingValue, "Average Rating", "0.00", 250, 135, UiTheme.Success, 200, 85, 16F);
            UiTheme.ConfigureStatCard(cardLowRatings, lblLowRatingsValue, "Low Ratings", "0", 470, 135, UiTheme.Warning, 200, 85, 16F);
            UiTheme.ConfigureStatCard(cardFiveStarReviews, lblFiveStarReviewsValue, "Five-Star Reviews", "0", 690, 135, UiTheme.Purple, 205, 85, 16F);

            filterPanel.Location = new Point(30, 245);
            filterPanel.Size = new Size(865, 105);
            UiTheme.StylePanel(filterPanel);

            lblFilterTitle.Text = "Search / Filter";
            lblFilterTitle.Font = new Font("Segoe UI", 12F, FontStyle.Bold);
            lblFilterTitle.ForeColor = UiTheme.TextDark;
            lblFilterTitle.AutoSize = true;
            lblFilterTitle.Location = new Point(15, 12);

            lblRating.Text = "Rating";
            lblRating.Font = UiTheme.NormalFont();
            lblRating.ForeColor = UiTheme.TextDark;
            lblRating.AutoSize = true;
            lblRating.Location = new Point(15, 50);

            cmbRating.DropDownStyle = ComboBoxStyle.DropDownList;
            cmbRating.Font = UiTheme.NormalFont();
            cmbRating.Location = new Point(70, 48);
            cmbRating.Size = new Size(85, 25);

            lblCustomerName.Text = "Customer";
            lblCustomerName.Font = UiTheme.NormalFont();
            lblCustomerName.ForeColor = UiTheme.TextDark;
            lblCustomerName.AutoSize = true;
            lblCustomerName.Location = new Point(175, 50);

            txtCustomerName.Font = UiTheme.NormalFont();
            txtCustomerName.Location = new Point(250, 48);
            txtCustomerName.Size = new Size(125, 25);

            lblOwnerName.Text = "Owner";
            lblOwnerName.Font = UiTheme.NormalFont();
            lblOwnerName.ForeColor = UiTheme.TextDark;
            lblOwnerName.AutoSize = true;
            lblOwnerName.Location = new Point(395, 50);

            txtOwnerName.Font = UiTheme.NormalFont();
            txtOwnerName.Location = new Point(450, 48);
            txtOwnerName.Size = new Size(125, 25);

            lblCarName.Text = "Car";
            lblCarName.Font = UiTheme.NormalFont();
            lblCarName.ForeColor = UiTheme.TextDark;
            lblCarName.AutoSize = true;
            lblCarName.Location = new Point(595, 50);

            txtCarName.Font = UiTheme.NormalFont();
            txtCarName.Location = new Point(630, 48);
            txtCarName.Size = new Size(120, 25);

            btnApplyFilter.Text = "Apply";
            btnApplyFilter.Location = new Point(765, 18);
            btnApplyFilter.Size = new Size(80, 32);
            UiTheme.StylePrimaryButton(btnApplyFilter);
            btnApplyFilter.Click += btnApplyFilter_Click;

            btnClearFilter.Text = "Clear";
            btnClearFilter.Location = new Point(765, 58);
            btnClearFilter.Size = new Size(80, 32);
            UiTheme.StyleSlateButton(btnClearFilter);
            btnClearFilter.Click += btnClearFilter_Click;

            btnRefresh.Text = "Refresh";
            btnRefresh.Location = new Point(1010, 70);
            btnRefresh.Size = new Size(120, 38);
            UiTheme.StylePrimaryButton(btnRefresh);
            btnRefresh.Click += btnRefresh_Click;

            dgvReviews.Location = new Point(30, 375);
            dgvReviews.Size = new Size(865, 330);
            UiTheme.StyleGrid(dgvReviews);
            dgvReviews.CellClick += dgvReviews_CellClick;

            actionPanel.Location = new Point(925, 245);
            actionPanel.Size = new Size(265, 460);
            UiTheme.StylePanel(actionPanel);

            lblActionTitle.Text = "Actions";
            lblActionTitle.Font = UiTheme.HeaderFont();
            lblActionTitle.ForeColor = UiTheme.TextDark;
            lblActionTitle.AutoSize = true;
            lblActionTitle.Location = new Point(20, 22);

            lblSelectedReview.Text = "Selected Review: None";
            lblSelectedReview.Font = new Font("Segoe UI", 10F, FontStyle.Bold);
            lblSelectedReview.ForeColor = UiTheme.TextDark;
            lblSelectedReview.Location = new Point(20, 75);
            lblSelectedReview.Size = new Size(220, 45);

            lblSelectedOwner.Text = "Owner: -";
            lblSelectedOwner.Font = UiTheme.NormalFont();
            lblSelectedOwner.ForeColor = UiTheme.TextMuted;
            lblSelectedOwner.Location = new Point(20, 125);
            lblSelectedOwner.Size = new Size(220, 45);

            btnDeleteReview.Text = "Delete Review";
            btnDeleteReview.Location = new Point(20, 205);
            btnDeleteReview.Size = new Size(220, 40);
            UiTheme.StyleDangerButton(btnDeleteReview);
            btnDeleteReview.Click += btnDeleteReview_Click;

            btnSuspendOwner.Text = "Suspend Owner";
            btnSuspendOwner.Location = new Point(20, 260);
            btnSuspendOwner.Size = new Size(220, 40);
            UiTheme.StyleWarningButton(btnSuspendOwner);
            btnSuspendOwner.Click += btnSuspendOwner_Click;

            filterPanel.Controls.Add(lblFilterTitle);
            filterPanel.Controls.Add(lblRating);
            filterPanel.Controls.Add(cmbRating);
            filterPanel.Controls.Add(lblCustomerName);
            filterPanel.Controls.Add(txtCustomerName);
            filterPanel.Controls.Add(lblOwnerName);
            filterPanel.Controls.Add(txtOwnerName);
            filterPanel.Controls.Add(lblCarName);
            filterPanel.Controls.Add(txtCarName);
            filterPanel.Controls.Add(btnApplyFilter);
            filterPanel.Controls.Add(btnClearFilter);

            actionPanel.Controls.Add(lblActionTitle);
            actionPanel.Controls.Add(lblSelectedReview);
            actionPanel.Controls.Add(lblSelectedOwner);
            actionPanel.Controls.Add(btnDeleteReview);
            actionPanel.Controls.Add(btnSuspendOwner);

            Controls.Add(lblTitle);
            Controls.Add(lblSubtitle);
            Controls.Add(lblUser);
            Controls.Add(cardTotalReviews);
            Controls.Add(cardAverageRating);
            Controls.Add(cardLowRatings);
            Controls.Add(cardFiveStarReviews);
            Controls.Add(btnRefresh);
            Controls.Add(filterPanel);
            Controls.Add(dgvReviews);
            Controls.Add(actionPanel);

            filterPanel.ResumeLayout(false);
            filterPanel.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)dgvReviews).EndInit();
            actionPanel.ResumeLayout(false);
            actionPanel.PerformLayout();
            ResumeLayout(false);
            PerformLayout();
        }
    }
}
