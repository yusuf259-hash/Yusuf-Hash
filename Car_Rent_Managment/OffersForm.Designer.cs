using Car_Rent_Managment.UI;
using System.Drawing;
using System.Windows.Forms;

namespace Car_Rent_Managment
{
    partial class OffersForm
    {
        private System.ComponentModel.IContainer components = null;

        private Label lblTitle;
        private Label lblSubtitle;
        private DataGridView dgvOffers;
        private Button btnRefresh;
        private Button btnClose;

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
            dgvOffers = new DataGridView();
            btnRefresh = new Button();
            btnClose = new Button();

            ((System.ComponentModel.ISupportInitialize)dgvOffers).BeginInit();
            SuspendLayout();

            BackColor = UiTheme.Background;
            ClientSize = new Size(1000, 600);
            StartPosition = FormStartPosition.CenterScreen;
            Text = "Special Offers";

            lblTitle.Text = "Special Offers";
            lblTitle.Font = UiTheme.TitleFont();
            lblTitle.ForeColor = UiTheme.TextDark;
            lblTitle.AutoSize = true;
            lblTitle.Location = new Point(30, 25);

            lblSubtitle.Text = "Active discounts and promotional offers";
            lblSubtitle.Font = UiTheme.NormalFont();
            lblSubtitle.ForeColor = UiTheme.TextMuted;
            lblSubtitle.AutoSize = true;
            lblSubtitle.Location = new Point(35, 75);

            btnRefresh.Text = "Refresh";
            btnRefresh.Location = new Point(720, 40);
            btnRefresh.Size = new Size(110, 38);
            UiTheme.StylePrimaryButton(btnRefresh);
            btnRefresh.Click += btnRefresh_Click;

            btnClose.Text = "Close";
            btnClose.Location = new Point(850, 40);
            btnClose.Size = new Size(110, 38);
            UiTheme.StyleSlateButton(btnClose);
            btnClose.Click += btnClose_Click;

            dgvOffers.Location = new Point(30, 120);
            dgvOffers.Size = new Size(930, 420);
            UiTheme.StyleGrid(dgvOffers);

            Controls.Add(lblTitle);
            Controls.Add(lblSubtitle);
            Controls.Add(btnRefresh);
            Controls.Add(btnClose);
            Controls.Add(dgvOffers);

            ((System.ComponentModel.ISupportInitialize)dgvOffers).EndInit();
            ResumeLayout(false);
            PerformLayout();
        }
    }
}