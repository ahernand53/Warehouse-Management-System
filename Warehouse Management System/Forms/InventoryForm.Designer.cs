// Wms.WinForms/Forms/InventoryForm.Designer.cs

using Wms.WinForms.Common;

namespace Wms.WinForms.Forms
{
    partial class InventoryForm
    {
        private System.ComponentModel.IContainer components = null;
        private Panel pnlHeader;
        private Label lblTitle;
        private Panel pnlSearch;
        private Label lblSearch;
        private TextBox txtSearch;
        private Button btnSearch;
        private Panel pnlActions;
        private Button btnRefresh;
        private Button btnAdjustment;
        private Button btnSummary;
        private Panel pnlMain;
        private DataGridView dgvStock;
        private StatusStrip statusStrip;
        private ToolStripStatusLabel lblStatus;

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
            pnlHeader = new Panel();
            lblTitle = new Label();
            pnlSearch = new Panel();
            btnSearch = new Button();
            txtSearch = new TextBox();
            lblSearch = new Label();
            pnlActions = new Panel();
            btnSummary = new Button();
            btnAdjustment = new Button();
            btnRefresh = new Button();
            pnlMain = new Panel();
            dgvStock = new DataGridView();
            statusStrip = new StatusStrip();
            lblStatus = new ToolStripStatusLabel();
            pnlHeader.SuspendLayout();
            pnlSearch.SuspendLayout();
            pnlActions.SuspendLayout();
            pnlMain.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)dgvStock).BeginInit();
            statusStrip.SuspendLayout();
            SuspendLayout();
            // 
            // pnlHeader
            // 
            pnlHeader.BackColor = Color.White;
            pnlHeader.Controls.Add(lblTitle);
            pnlHeader.Dock = DockStyle.Top;
            pnlHeader.Location = new Point(0, 0);
            pnlHeader.Name = "pnlHeader";
            pnlHeader.Padding = new Padding(30, 20, 30, 20);
            pnlHeader.Size = new Size(1200, 80);
            pnlHeader.TabIndex = 0;
            // 
            // lblTitle
            // 
            lblTitle.AutoSize = true;
            lblTitle.Font = new Font("Segoe UI", 24F, FontStyle.Bold);
            lblTitle.ForeColor = Color.FromArgb(33, 37, 41);
            lblTitle.Location = new Point(30, 25);
            lblTitle.Name = "lblTitle";
            lblTitle.Size = new Size(372, 45);
            lblTitle.TabIndex = 0;
            lblTitle.Text = "Gestión de Inventario";
            // 
            // pnlSearch
            // 
            pnlSearch.BackColor = Color.White;
            pnlSearch.Controls.Add(btnSearch);
            pnlSearch.Controls.Add(txtSearch);
            pnlSearch.Controls.Add(lblSearch);
            pnlSearch.Dock = DockStyle.Top;
            pnlSearch.Location = new Point(0, 80);
            pnlSearch.Name = "pnlSearch";
            pnlSearch.Padding = new Padding(30, 15, 30, 15);
            pnlSearch.Size = new Size(1200, 70);
            pnlSearch.TabIndex = 1;
            // 
            // btnSearch
            // 
            btnSearch.Anchor = AnchorStyles.Top | AnchorStyles.Right;
            btnSearch.Location = new Point(1070, 20);
            btnSearch.Name = "btnSearch";
            btnSearch.Size = new Size(100, 35);
            btnSearch.TabIndex = 2;
            btnSearch.Text = "Buscar";
            btnSearch.UseVisualStyleBackColor = true;
            // 
            // txtSearch
            // 
            txtSearch.Anchor = AnchorStyles.Top | AnchorStyles.Left | AnchorStyles.Right;
            txtSearch.Location = new Point(112, 29);
            txtSearch.Name = "txtSearch";
            txtSearch.PlaceholderText = "Buscar por SKU, nombre del artículo o código de barras...";
            txtSearch.Size = new Size(948, 23);
            txtSearch.TabIndex = 1;
            // 
            // lblSearch
            // 
            lblSearch.AutoSize = true;
            lblSearch.Font = new Font("Segoe UI", 14F, FontStyle.Bold);
            lblSearch.ForeColor = Color.FromArgb(108, 117, 125);
            lblSearch.Location = new Point(30, 25);
            lblSearch.Name = "lblSearch";
            lblSearch.Size = new Size(76, 25);
            lblSearch.TabIndex = 0;
            lblSearch.Text = "Buscar:";
            // 
            // pnlActions
            // 
            pnlActions.BackColor = Color.White;
            pnlActions.Controls.Add(btnSummary);
            pnlActions.Controls.Add(btnAdjustment);
            pnlActions.Controls.Add(btnRefresh);
            pnlActions.Dock = DockStyle.Top;
            pnlActions.Location = new Point(0, 150);
            pnlActions.Name = "pnlActions";
            pnlActions.Padding = new Padding(30, 15, 30, 15);
            pnlActions.Size = new Size(1200, 70);
            pnlActions.TabIndex = 2;
            // 
            // btnSummary
            // 
            btnSummary.Location = new Point(310, 20);
            btnSummary.Name = "btnSummary";
            btnSummary.Size = new Size(120, 40);
            btnSummary.TabIndex = 2;
            btnSummary.Text = "Summary (F3)";
            btnSummary.UseVisualStyleBackColor = true;
            // 
            // btnAdjustment
            // 
            btnAdjustment.Enabled = false;
            btnAdjustment.Location = new Point(160, 20);
            btnAdjustment.Name = "btnAdjustment";
            btnAdjustment.Size = new Size(140, 40);
            btnAdjustment.TabIndex = 1;
            btnAdjustment.Text = "Adjustment (F2)";
            btnAdjustment.UseVisualStyleBackColor = true;
            // 
            // btnRefresh
            // 
            btnRefresh.Location = new Point(30, 20);
            btnRefresh.Name = "btnRefresh";
            btnRefresh.Size = new Size(120, 40);
            btnRefresh.TabIndex = 0;
            btnRefresh.Text = "Refresh (F1)";
            btnRefresh.UseVisualStyleBackColor = true;
            // 
            // pnlMain
            // 
            pnlMain.BackColor = Color.FromArgb(248, 249, 250);
            pnlMain.Controls.Add(dgvStock);
            pnlMain.Dock = DockStyle.Fill;
            pnlMain.Location = new Point(0, 220);
            pnlMain.Name = "pnlMain";
            pnlMain.Padding = new Padding(30, 0, 30, 20);
            pnlMain.Size = new Size(1200, 458);
            pnlMain.TabIndex = 3;
            // 
            // dgvStock
            // 
            dgvStock.AllowUserToAddRows = false;
            dgvStock.AllowUserToDeleteRows = false;
            dgvStock.BackgroundColor = Color.White;
            dgvStock.ColumnHeadersHeightSizeMode = DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            dgvStock.Dock = DockStyle.Fill;
            dgvStock.Location = new Point(30, 0);
            dgvStock.MultiSelect = false;
            dgvStock.Name = "dgvStock";
            dgvStock.ReadOnly = true;
            dgvStock.SelectionMode = DataGridViewSelectionMode.FullRowSelect;
            dgvStock.Size = new Size(1140, 438);
            dgvStock.TabIndex = 0;
            // 
            // statusStrip
            // 
            statusStrip.Items.AddRange(new ToolStripItem[] { lblStatus });
            statusStrip.Location = new Point(0, 678);
            statusStrip.Name = "statusStrip";
            statusStrip.Size = new Size(1200, 22);
            statusStrip.TabIndex = 4;
            // 
            // lblStatus
            // 
            lblStatus.Name = "lblStatus";
            lblStatus.Size = new Size(39, 17);
            lblStatus.Text = "Ready";
            // 
            // InventoryForm
            // 
            AutoScaleDimensions = new SizeF(7F, 15F);
            AutoScaleMode = AutoScaleMode.Font;
            BackColor = Color.FromArgb(248, 249, 250);
            ClientSize = new Size(1200, 700);
            Controls.Add(pnlMain);
            Controls.Add(pnlActions);
            Controls.Add(pnlSearch);
            Controls.Add(pnlHeader);
            Controls.Add(statusStrip);
            FormBorderStyle = FormBorderStyle.None;
            KeyPreview = true;
            Name = "InventoryForm";
            Text = "WMS - Inventory Management";
            WindowState = FormWindowState.Maximized;
            pnlHeader.ResumeLayout(false);
            pnlHeader.PerformLayout();
            pnlSearch.ResumeLayout(false);
            pnlSearch.PerformLayout();
            pnlActions.ResumeLayout(false);
            pnlMain.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)dgvStock).EndInit();
            statusStrip.ResumeLayout(false);
            statusStrip.PerformLayout();
            ResumeLayout(false);
            PerformLayout();
        }
    }
}