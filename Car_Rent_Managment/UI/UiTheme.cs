using System.Drawing;
using System.Windows.Forms;

namespace Car_Rent_Managment.UI
{
    public static class UiTheme
    {
        public static readonly Color Sidebar = Color.FromArgb(15, 23, 42);
        public static readonly Color SidebarButton = Color.FromArgb(30, 41, 59);
        public static readonly Color Background = Color.FromArgb(248, 250, 252);
        public static readonly Color Surface = Color.White;
        public static readonly Color Border = Color.FromArgb(226, 232, 240);

        public static readonly Color Primary = Color.FromArgb(37, 99, 235);
        public static readonly Color Success = Color.FromArgb(22, 163, 74);
        public static readonly Color Warning = Color.FromArgb(217, 119, 6);
        public static readonly Color Danger = Color.FromArgb(220, 38, 38);
        public static readonly Color Purple = Color.FromArgb(124, 58, 237);

        public static readonly Color TextDark = Color.FromArgb(15, 23, 42);
        public static readonly Color TextMuted = Color.FromArgb(100, 116, 139);
        public static readonly Color White = Color.White;

        public static Font TitleFont()
        {
            return new Font("Segoe UI", 24F, FontStyle.Bold);
        }

        public static Font HeaderFont()
        {
            return new Font("Segoe UI", 15F, FontStyle.Bold);
        }

        public static Font NormalFont()
        {
            return new Font("Segoe UI", 10F, FontStyle.Regular);
        }

        public static Font SmallFont()
        {
            return new Font("Segoe UI", 9F, FontStyle.Regular);
        }

        public static Font ButtonFont()
        {
            return new Font("Segoe UI", 10F, FontStyle.Bold);
        }

        public static void StylePrimaryButton(Button button)
        {
            StyleButton(button, Primary);
        }

        public static void StyleSuccessButton(Button button)
        {
            StyleButton(button, Success);
        }

        public static void StyleWarningButton(Button button)
        {
            StyleButton(button, Warning);
        }

        public static void StyleDangerButton(Button button)
        {
            StyleButton(button, Danger);
        }

        public static void StylePurpleButton(Button button)
        {
            StyleButton(button, Purple);
        }

        public static void StyleSlateButton(Button button)
        {
            StyleButton(button, SidebarButton);
        }

        public static void StyleButton(Button button, Color backColor)
        {
            button.BackColor = backColor;
            button.ForeColor = White;
            button.FlatStyle = FlatStyle.Flat;
            button.FlatAppearance.BorderSize = 0;
            button.Font = ButtonFont();
            button.Cursor = Cursors.Hand;
            button.UseVisualStyleBackColor = false;
        }

        public static void StyleSidebarButton(Button button)
        {
            button.BackColor = SidebarButton;
            button.ForeColor = White;
            button.FlatStyle = FlatStyle.Flat;
            button.FlatAppearance.BorderSize = 0;
            button.Font = ButtonFont();
            button.Cursor = Cursors.Hand;
            button.TextAlign = ContentAlignment.MiddleLeft;
            button.Padding = new Padding(14, 0, 0, 0);
            button.UseVisualStyleBackColor = false;
        }

        public static void StylePanel(Panel panel)
        {
            panel.BackColor = Surface;
            panel.BorderStyle = BorderStyle.FixedSingle;
        }

        public static void StyleDataGridView(DataGridView grid)
        {
            StyleGrid(grid);
        }

        public static void StyleGrid(DataGridView grid)
        {
            grid.BackgroundColor = Surface;
            grid.BorderStyle = BorderStyle.None;
            grid.AllowUserToAddRows = false;
            grid.AllowUserToDeleteRows = false;
            grid.ReadOnly = true;
            grid.MultiSelect = false;
            grid.SelectionMode = DataGridViewSelectionMode.FullRowSelect;
            grid.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill;
            grid.RowTemplate.Height = 30;

            grid.EnableHeadersVisualStyles = false;
            grid.ColumnHeadersDefaultCellStyle.BackColor = Sidebar;
            grid.ColumnHeadersDefaultCellStyle.ForeColor = White;
            grid.ColumnHeadersDefaultCellStyle.Font = new Font("Segoe UI", 10F, FontStyle.Bold);
            grid.ColumnHeadersHeight = 38;

            grid.DefaultCellStyle.Font = new Font("Segoe UI", 9F);
            grid.DefaultCellStyle.SelectionBackColor = Color.FromArgb(219, 234, 254);
            grid.DefaultCellStyle.SelectionForeColor = TextDark;
        }

        public static Panel CreateSidebar(string title, string subtitle)
        {
            Panel sidebar = new Panel();
            sidebar.BackColor = Sidebar;
            sidebar.Dock = DockStyle.Left;
            sidebar.Width = 260;

            Label lblTitle = new Label();
            lblTitle.Text = title;
            lblTitle.Font = new Font("Segoe UI", 20F, FontStyle.Bold);
            lblTitle.ForeColor = White;
            lblTitle.AutoSize = false;
            lblTitle.Size = new Size(220, 80);
            lblTitle.Location = new Point(24, 34);

            Label lblSub = new Label();
            lblSub.Text = subtitle;
            lblSub.Font = new Font("Segoe UI", 10F);
            lblSub.ForeColor = Color.FromArgb(191, 219, 254);
            lblSub.AutoSize = false;
            lblSub.Size = new Size(215, 40);
            lblSub.Location = new Point(28, 100);

            sidebar.Controls.Add(lblTitle);
            sidebar.Controls.Add(lblSub);

            return sidebar;
        }

        public static Button CreateSidebarButton(string text, int y)
        {
            Button button = new Button();
            button.Text = text;
            button.Size = new Size(210, 42);
            button.Location = new Point(25, y);
            StyleSidebarButton(button);
            return button;
        }

        public static Panel CreateStatCard(string title, string value, int x, int y, Color accent)
        {
            Panel card = new Panel();
            Label valueLabel = new Label();
            ConfigureStatCard(card, valueLabel, title, value, x, y, accent);
            return card;
        }

        public static void ConfigureStatCard(
            Panel card,
            Label valueLabel,
            string title,
            string value,
            int x,
            int y,
            Color accent,
            int width = 205,
            int height = 100,
            float valueFontSize = 18F)
        {
            card.Controls.Clear();
            card.BackColor = Surface;
            card.BorderStyle = BorderStyle.FixedSingle;
            card.Location = new Point(x, y);
            card.Size = new Size(width, height);

            Panel stripe = new Panel();
            stripe.BackColor = accent;
            stripe.Dock = DockStyle.Left;
            stripe.Width = 8;

            Label lblTitle = new Label();
            lblTitle.Text = title;
            lblTitle.Font = new Font("Segoe UI", 9F, FontStyle.Regular);
            lblTitle.ForeColor = TextMuted;
            lblTitle.AutoSize = false;
            lblTitle.Location = new Point(22, 15);
            lblTitle.Size = new Size(width - 35, 22);

            valueLabel.Text = value;
            valueLabel.Font = new Font("Segoe UI", valueFontSize, FontStyle.Bold);
            valueLabel.ForeColor = TextDark;
            valueLabel.AutoSize = false;
            valueLabel.Location = new Point(22, 42);
            valueLabel.Size = new Size(width - 35, height - 48);
            valueLabel.TextAlign = ContentAlignment.TopLeft;

            card.Controls.Add(stripe);
            card.Controls.Add(lblTitle);
            card.Controls.Add(valueLabel);
        }

        public static Panel CreateInfoPanel(string title, string body, int x, int y, int width, int height)
        {
            Panel panel = new Panel();
            panel.BackColor = Surface;
            panel.BorderStyle = BorderStyle.FixedSingle;
            panel.Location = new Point(x, y);
            panel.Size = new Size(width, height);

            Label lblTitle = new Label();
            lblTitle.Text = title;
            lblTitle.Font = HeaderFont();
            lblTitle.ForeColor = TextDark;
            lblTitle.AutoSize = true;
            lblTitle.Location = new Point(20, 18);

            Label lblBody = new Label();
            lblBody.Text = body;
            lblBody.Font = NormalFont();
            lblBody.ForeColor = TextMuted;
            lblBody.AutoSize = false;
            lblBody.Location = new Point(22, 55);
            lblBody.Size = new Size(width - 45, height - 75);

            panel.Controls.Add(lblTitle);
            panel.Controls.Add(lblBody);

            return panel;
        }
    }
}
