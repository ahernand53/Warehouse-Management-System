// Wms.WinForms/Common/ModernUIHelper.cs

namespace Wms.WinForms.Common;

public static class ModernUIHelper
{
    // Common styling methods
    public static void StylePrimaryButton(Button button)
    {
        button.BackColor = Colors.Primary;
        button.ForeColor = Color.White;
        button.FlatStyle = FlatStyle.Flat;
        button.FlatAppearance.BorderSize = 0;
        button.Font = Fonts.H6;
        button.Height = Layout.ButtonHeight;
        button.Cursor = Cursors.Hand;

        button.MouseEnter += (s, e) =>
        {
            var color = Colors.Primary;
            var darkerR = Math.Max(0, color.R - 20);
            var darkerG = Math.Max(0, color.G - 20);
            var darkerB = Math.Max(0, color.B - 20);
            button.BackColor = Color.FromArgb(darkerR, darkerG, darkerB);
        };
        button.MouseLeave += (s, e) => button.BackColor = Colors.Primary;
    }

    public static void StyleSecondaryButton(Button button)
    {
        button.BackColor = Colors.Secondary;
        button.ForeColor = Color.White;
        button.FlatStyle = FlatStyle.Flat;
        button.FlatAppearance.BorderSize = 0;
        button.Font = Fonts.H6;
        button.Height = Layout.ButtonHeight;
        button.Cursor = Cursors.Hand;

        button.MouseEnter += (s, e) =>
        {
            var color = Colors.Secondary;
            var darkerR = Math.Max(0, color.R - 20);
            var darkerG = Math.Max(0, color.G - 20);
            var darkerB = Math.Max(0, color.B - 20);
            button.BackColor = Color.FromArgb(darkerR, darkerG, darkerB);
        };
        button.MouseLeave += (s, e) => button.BackColor = Colors.Secondary;
    }

    public static void StyleSuccessButton(Button button)
    {
        button.BackColor = Colors.Success;
        button.ForeColor = Color.White;
        button.FlatStyle = FlatStyle.Flat;
        button.FlatAppearance.BorderSize = 0;
        button.Font = Fonts.H6;
        button.Height = Layout.ButtonHeight;
        button.Cursor = Cursors.Hand;

        button.MouseEnter += (s, e) =>
        {
            var color = Colors.Success;
            var darkerR = Math.Max(0, color.R - 20);
            var darkerG = Math.Max(0, color.G - 20);
            var darkerB = Math.Max(0, color.B - 20);
            button.BackColor = Color.FromArgb(darkerR, darkerG, darkerB);
        };
        button.MouseLeave += (s, e) => button.BackColor = Colors.Success;
    }

    public static void StyleDangerButton(Button button)
    {
        button.BackColor = Colors.Danger;
        button.ForeColor = Color.White;
        button.FlatStyle = FlatStyle.Flat;
        button.FlatAppearance.BorderSize = 0;
        button.Font = Fonts.H6;
        button.Height = Layout.ButtonHeight;
        button.Cursor = Cursors.Hand;

        button.MouseEnter += (s, e) =>
        {
            var color = Colors.Danger;
            var darkerR = Math.Max(0, color.R - 20);
            var darkerG = Math.Max(0, color.G - 20);
            var darkerB = Math.Max(0, color.B - 20);
            button.BackColor = Color.FromArgb(darkerR, darkerG, darkerB);
        };
        button.MouseLeave += (s, e) => button.BackColor = Colors.Danger;
    }

    public static void StyleInfoButton(Button button)
    {
        button.BackColor = Colors.Info;
        button.ForeColor = Color.White;
        button.FlatStyle = FlatStyle.Flat;
        button.FlatAppearance.BorderSize = 0;
        button.Font = Fonts.H6;
        button.Height = Layout.ButtonHeight;
        button.Cursor = Cursors.Hand;

        button.MouseEnter += (s, e) =>
        {
            // Safe color darkening - ensure values don't go below 0
            var color = Colors.Info;
            var darkerR = Math.Max(0, color.R - 20);
            var darkerG = Math.Max(0, color.G - 20);
            var darkerB = Math.Max(0, color.B - 20);
            button.BackColor = Color.FromArgb(darkerR, darkerG, darkerB);
        };
        button.MouseLeave += (s, e) => button.BackColor = Colors.Info;
    }

    public static void StyleWarningButton(Button button)
    {
        button.BackColor = Colors.Warning;
        button.ForeColor = Color.White;
        button.FlatStyle = FlatStyle.Flat;
        button.FlatAppearance.BorderSize = 0;
        button.Font = Fonts.H6;
        button.Height = Layout.ButtonHeight;
        button.Cursor = Cursors.Hand;

        button.MouseEnter += (s, e) =>
        {
            // Safe color darkening - ensure values don't go below 0
            var color = Colors.Warning;
            var darkerR = Math.Max(0, color.R - 20);
            var darkerG = Math.Max(0, color.G - 20);
            var darkerB = Math.Max(0, color.B - 20);
            button.BackColor = Color.FromArgb(darkerR, darkerG, darkerB);
        };
        button.MouseLeave += (s, e) => button.BackColor = Colors.Warning;
    }

    public static void StyleModernTextBox(TextBox textBox)
    {
        textBox.BorderStyle = BorderStyle.FixedSingle;
        textBox.Font = Fonts.Body;
        textBox.BackColor = Color.White;
        textBox.ForeColor = Colors.TextPrimary;

        // Add padding effect
        textBox.Height = 35;

        textBox.Enter += (s, e) =>
        {
            textBox.BackColor = Color.FromArgb(255, 255, 255);
            ((TextBox)s!).SelectAll();
        };
        textBox.Leave += (s, e) => { textBox.BackColor = Color.White; };
    }

    public static void StyleModernComboBox(ComboBox comboBox)
    {
        comboBox.FlatStyle = FlatStyle.Flat;
        comboBox.Font = Fonts.Body;
        comboBox.BackColor = Color.White;
        comboBox.ForeColor = Colors.TextPrimary;
        comboBox.Height = 35;
    }

    public static void StyleModernDataGridView(DataGridView dgv)
    {
        dgv.BorderStyle = BorderStyle.None;
        dgv.AlternatingRowsDefaultCellStyle.BackColor = Color.FromArgb(248, 249, 250);
        dgv.BackgroundColor = Color.White;
        dgv.GridColor = Colors.BorderLight;
        dgv.DefaultCellStyle.SelectionBackColor = Colors.PrimaryLight;
        dgv.DefaultCellStyle.SelectionForeColor = Colors.TextPrimary;
        dgv.ColumnHeadersDefaultCellStyle.BackColor = Colors.BackgroundSecondary;
        dgv.ColumnHeadersDefaultCellStyle.ForeColor = Colors.TextPrimary;
        dgv.ColumnHeadersDefaultCellStyle.Font = Fonts.H6;
        dgv.DefaultCellStyle.Font = Fonts.Body;
        dgv.EnableHeadersVisualStyles = false;
        dgv.RowHeadersVisible = false;
        dgv.SelectionMode = DataGridViewSelectionMode.FullRowSelect;
        dgv.MultiSelect = false;
        dgv.AllowUserToAddRows = false;
        dgv.AllowUserToDeleteRows = false;
        dgv.ReadOnly = true;
        dgv.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill;
    }

    public static void StyleCard(Panel panel)
    {
        panel.BackColor = Colors.CardBackground;
        panel.ForeColor = Colors.TextPrimary;

        // Add shadow effect using a border
        panel.Paint += (sender, e) =>
        {
            var rect = panel.ClientRectangle;
            rect.Width -= 1;
            rect.Height -= 1;

            using var shadowPen = new Pen(Color.FromArgb(30, 0, 0, 0), 1);
            e.Graphics.DrawRectangle(shadowPen, rect);
        };

        // Set consistent spacing
        panel.Padding = new Padding(Spacing.Large);
        panel.Margin = new Padding(Spacing.Medium);
    }

    public static void StyleForm(Form form)
    {
        form.BackColor = Colors.BackgroundPrimary;
        form.Font = Fonts.Body;
        form.ForeColor = Colors.TextPrimary;
    }

    public static Panel CreateCard(int padding = 20)
    {
        var panel = new Panel
        {
            BackColor = Color.White,
            Padding = new Padding(padding)
        };
        StyleCard(panel);
        return panel;
    }

    public static Label CreateHeading(string text, int level = 1)
    {
        var label = new Label
        {
            Text = text,
            AutoSize = true,
            ForeColor = Colors.TextPrimary
        };

        label.Font = level switch
        {
            1 => Fonts.H1,
            2 => Fonts.H2,
            3 => Fonts.H3,
            4 => Fonts.H4,
            5 => Fonts.H5,
            6 => Fonts.H6,
            _ => Fonts.H1
        };

        return label;
    }

    public static void ShowModernMessageBox(string message, string title, MessageBoxIcon icon)
    {
        MessageBox.Show(message, title, MessageBoxButtons.OK, icon);
    }

    public static void ShowModernError(string message)
    {
        ShowModernMessageBox(message, "Error", MessageBoxIcon.Error);
    }

    public static void ShowModernSuccess(string message)
    {
        ShowModernMessageBox(message, "Éxito", MessageBoxIcon.Information);
    }

    public static void ShowModernWarning(string message)
    {
        ShowModernMessageBox(message, "Advertencia", MessageBoxIcon.Warning);
    }

    public static void StyleResponsivePanel(Panel panel)
    {
        panel.Padding = new Padding(Spacing.Large);
        panel.Margin = new Padding(0);
    }

    public static void StyleFormHeader(Panel headerPanel)
    {
        headerPanel.BackColor = Colors.CardBackground;
        headerPanel.Height = Layout.HeaderHeight;
        headerPanel.Padding = new Padding(Spacing.XLarge, Spacing.Large, Spacing.XLarge, Spacing.Large);
        headerPanel.Dock = DockStyle.Top;
    }

    public static void StyleFormContent(Panel contentPanel)
    {
        contentPanel.Padding = new Padding(Spacing.XLarge, Spacing.Large, Spacing.XLarge, Spacing.Large);
        contentPanel.Dock = DockStyle.Fill;
    }

    public static void StyleFormActions(Panel actionsPanel)
    {
        actionsPanel.BackColor = Colors.CardBackground;
        actionsPanel.Height = Layout.FooterHeight;
        actionsPanel.Padding = new Padding(Spacing.XLarge, Spacing.Medium, Spacing.XLarge, Spacing.Medium);
        actionsPanel.Dock = DockStyle.Bottom;
    }

    public static void ApplyResponsiveLayout(Form form, int minWidth = 800, int minHeight = 600)
    {
        form.MinimumSize = new Size(minWidth, minHeight);
        form.StartPosition = FormStartPosition.CenterScreen;
        form.WindowState = FormWindowState.Normal;
    }

    // Bootstrap 5 inspired color palette
    public static class Colors
    {
        // Primary colors
        public static readonly Color Primary = Color.FromArgb(13, 110, 253); // Blue
        public static readonly Color Secondary = Color.FromArgb(108, 117, 125); // Gray
        public static readonly Color Success = Color.FromArgb(25, 135, 84); // Green
        public static readonly Color Danger = Color.FromArgb(220, 53, 69); // Red
        public static readonly Color Warning = Color.FromArgb(255, 193, 7); // Yellow
        public static readonly Color Info = Color.FromArgb(13, 202, 240); // Cyan

        // Light variants
        public static readonly Color PrimaryLight = Color.FromArgb(180, 198, 231);
        public static readonly Color SuccessLight = Color.FromArgb(204, 232, 207);
        public static readonly Color DangerLight = Color.FromArgb(248, 215, 218);
        public static readonly Color WarningLight = Color.FromArgb(255, 243, 205);
        public static readonly Color InfoLight = Color.FromArgb(186, 230, 253);

        // Background colors
        public static readonly Color BackgroundPrimary = Color.FromArgb(248, 249, 250);
        public static readonly Color BackgroundSecondary = Color.FromArgb(233, 236, 239);
        public static readonly Color BackgroundDark = Color.FromArgb(52, 58, 64);

        // Text colors
        public static readonly Color TextPrimary = Color.FromArgb(33, 37, 41);
        public static readonly Color TextSecondary = Color.FromArgb(108, 117, 125);
        public static readonly Color TextMuted = Color.FromArgb(173, 181, 189);
        public static readonly Color TextWhite = Color.White;

        // Border colors
        public static readonly Color BorderLight = Color.FromArgb(222, 226, 230);
        public static readonly Color BorderDark = Color.FromArgb(173, 181, 189);

        // Card specific colors
        public static readonly Color CardBackground = Color.White;
    }

    // Typography
    public static class Fonts
    {
        public static readonly Font H1 = new("Segoe UI", 32F, FontStyle.Bold);
        public static readonly Font H2 = new("Segoe UI", 28F, FontStyle.Bold);
        public static readonly Font H3 = new("Segoe UI", 24F, FontStyle.Bold);
        public static readonly Font H4 = new("Segoe UI", 20F, FontStyle.Bold);
        public static readonly Font H5 = new("Segoe UI", 16F, FontStyle.Bold);
        public static readonly Font H6 = new("Segoe UI", 14F, FontStyle.Bold);
        public static readonly Font Body = new("Segoe UI", 11F);
        public static readonly Font BodyLarge = new("Segoe UI", 12F);
        public static readonly Font Small = new("Segoe UI", 10F);
        public static readonly Font Caption = new("Segoe UI", 9F);
    }

    // Spacing constants
    public static class Spacing
    {
        public const int XSmall = 5;
        public const int Small = 10;
        public const int Medium = 15;
        public const int Large = 20;
        public const int XLarge = 30;
        public const int XXLarge = 40;

        // Bootstrap-inspired spacing scale
        public const int Space1 = 4; // 0.25rem
        public const int Space2 = 8; // 0.5rem  
        public const int Space3 = 12; // 0.75rem
        public const int Space4 = 16; // 1rem
        public const int Space5 = 20; // 1.25rem
        public const int Space6 = 24; // 1.5rem
        public const int Space8 = 32; // 2rem
        public const int Space10 = 40; // 2.5rem
        public const int Space12 = 48; // 3rem
    }

    // Layout constants
    public static class Layout
    {
        public const int HeaderHeight = 80;
        public const int FooterHeight = 60;
        public const int SidebarWidth = 250;
        public const int CardMinHeight = 120;
        public const int ButtonHeight = 40;
        public const int InputHeight = 35;
        public const int GridRowHeight = 32;
    }
}