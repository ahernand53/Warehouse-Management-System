// Wms.WinForms/Forms/DashboardForm.Designer.cs

using System.ComponentModel;

namespace Wms.WinForms.Forms
{
    partial class DashboardForm
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        private Panel pnlHeader;
        private Label lblTitle;
        private Label lblLastRefresh;
        private Button btnRefresh;
        private Panel pnlKPIs;
        private Panel pnlTotalItems;
        private Label lblTotalItemsTitle;
        private Label lblTotalItems;
        private Label lblActiveItems;
        private Panel pnlTotalSKUs;
        private Label lblTotalSKUsTitle;
        private Label lblTotalSKUs;
        private Panel pnlStockValue;
        private Label lblStockValueTitle;
        private Label lblTotalStockValue;
        private Panel pnlStockLocations;
        private Label lblStockLocationsTitle;
        private Label lblStockLocations;
        private Panel pnlContent;
        private Panel pnlRecentMovements;
        private Label lblRecentMovementsTitle;
        private DataGridView dgvRecentMovements;
        private Panel pnlLowStock;
        private Label lblLowStockTitle;
        private Label lblLowStockCount;
        private DataGridView dgvLowStock;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            components = new Container();
            pnlHeader = new Panel();
            btnRefresh = new Button();
            lblLastRefresh = new Label();
            lblTitle = new Label();
            pnlKPIs = new Panel();
            pnlStockLocations = new Panel();
            lblStockLocations = new Label();
            lblStockLocationsTitle = new Label();
            pnlStockValue = new Panel();
            lblTotalStockValue = new Label();
            lblStockValueTitle = new Label();
            pnlTotalSKUs = new Panel();
            lblTotalSKUs = new Label();
            lblTotalSKUsTitle = new Label();
            pnlTotalItems = new Panel();
            lblActiveItems = new Label();
            lblTotalItems = new Label();
            lblTotalItemsTitle = new Label();
            pnlContent = new Panel();
            pnlLowStock = new Panel();
            dgvLowStock = new DataGridView();
            lblLowStockCount = new Label();
            lblLowStockTitle = new Label();
            pnlRecentMovements = new Panel();
            dgvRecentMovements = new DataGridView();
            lblRecentMovementsTitle = new Label();
            pnlHeader.SuspendLayout();
            pnlKPIs.SuspendLayout();
            pnlStockLocations.SuspendLayout();
            pnlStockValue.SuspendLayout();
            pnlTotalSKUs.SuspendLayout();
            pnlTotalItems.SuspendLayout();
            pnlContent.SuspendLayout();
            pnlLowStock.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)dgvLowStock).BeginInit();
            pnlRecentMovements.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)dgvRecentMovements).BeginInit();
            SuspendLayout();
            // 
            // pnlHeader
            // 
            pnlHeader.BackColor = Color.White;
            pnlHeader.Controls.Add(btnRefresh);
            pnlHeader.Controls.Add(lblLastRefresh);
            pnlHeader.Controls.Add(lblTitle);
            pnlHeader.Dock = DockStyle.Top;
            pnlHeader.Location = new Point(0, 0);
            pnlHeader.Name = "pnlHeader";
            pnlHeader.Padding = new Padding(30, 15, 30, 15);
            pnlHeader.Size = new Size(1200, 80);
            pnlHeader.TabIndex = 0;
            // 
            // btnRefresh
            // 
            btnRefresh.Anchor = AnchorStyles.Top | AnchorStyles.Right;
            btnRefresh.Location = new Point(1050, 12);
            btnRefresh.Name = "btnRefresh";
            btnRefresh.Size = new Size(120, 40);
            btnRefresh.TabIndex = 2;
            btnRefresh.Text = "Actualizar (F5)";
            btnRefresh.UseVisualStyleBackColor = true;
            btnRefresh.Click += BtnRefresh_Click;
            // 
            // lblLastRefresh
            // 
            lblLastRefresh.Anchor = AnchorStyles.Top | AnchorStyles.Right;
            lblLastRefresh.AutoSize = true;
            lblLastRefresh.Font = new Font("Segoe UI", 10F);
            lblLastRefresh.ForeColor = Color.FromArgb(173, 181, 189);
            lblLastRefresh.Location = new Point(773, 55);
            lblLastRefresh.Name = "lblLastRefresh";
            lblLastRefresh.Size = new Size(117, 19);
            lblLastRefresh.TabIndex = 1;
            lblLastRefresh.Text = "Última actualización: --";
            // 
            // lblTitle
            // 
            lblTitle.AutoSize = true;
            lblTitle.Font = new Font("Segoe UI", 28F, FontStyle.Bold);
            lblTitle.ForeColor = Color.FromArgb(33, 37, 41);
            lblTitle.Location = new Point(30, 12);
            lblTitle.Name = "lblTitle";
            lblTitle.Size = new Size(216, 51);
            lblTitle.TabIndex = 0;
            lblTitle.Text = "Panel de Control";
            // 
            // pnlKPIs
            // 
            pnlKPIs.BackColor = Color.FromArgb(248, 249, 250);
            pnlKPIs.Controls.Add(pnlStockLocations);
            pnlKPIs.Controls.Add(pnlStockValue);
            pnlKPIs.Controls.Add(pnlTotalSKUs);
            pnlKPIs.Controls.Add(pnlTotalItems);
            pnlKPIs.Dock = DockStyle.Top;
            pnlKPIs.Location = new Point(0, 80);
            pnlKPIs.Name = "pnlKPIs";
            pnlKPIs.Padding = new Padding(30, 20, 30, 20);
            pnlKPIs.Size = new Size(1200, 160);
            pnlKPIs.TabIndex = 1;
            // 
            // pnlStockLocations
            // 
            pnlStockLocations.BackColor = Color.White;
            pnlStockLocations.Controls.Add(lblStockLocations);
            pnlStockLocations.Controls.Add(lblStockLocationsTitle);
            pnlStockLocations.Location = new Point(900, 20);
            pnlStockLocations.Name = "pnlStockLocations";
            pnlStockLocations.Padding = new Padding(20);
            pnlStockLocations.Size = new Size(270, 120);
            pnlStockLocations.TabIndex = 3;
            // 
            // lblStockLocations
            // 
            lblStockLocations.AutoSize = true;
            lblStockLocations.Font = new Font("Segoe UI", 28F, FontStyle.Bold);
            lblStockLocations.ForeColor = Color.FromArgb(255, 193, 7);
            lblStockLocations.Location = new Point(20, 45);
            lblStockLocations.Name = "lblStockLocations";
            lblStockLocations.Size = new Size(44, 51);
            lblStockLocations.TabIndex = 1;
            lblStockLocations.Text = "0";
            // 
            // lblStockLocationsTitle
            // 
            lblStockLocationsTitle.AutoSize = true;
            lblStockLocationsTitle.Font = new Font("Segoe UI", 14F, FontStyle.Bold);
            lblStockLocationsTitle.ForeColor = Color.FromArgb(108, 117, 125);
            lblStockLocationsTitle.Location = new Point(20, 20);
            lblStockLocationsTitle.Name = "lblStockLocationsTitle";
            lblStockLocationsTitle.Size = new Size(156, 25);
            lblStockLocationsTitle.TabIndex = 0;
            lblStockLocationsTitle.Text = "Ubicaciones Activas";
            // 
            // pnlStockValue
            // 
            pnlStockValue.BackColor = Color.White;
            pnlStockValue.Controls.Add(lblTotalStockValue);
            pnlStockValue.Controls.Add(lblStockValueTitle);
            pnlStockValue.Location = new Point(620, 20);
            pnlStockValue.Name = "pnlStockValue";
            pnlStockValue.Padding = new Padding(20);
            pnlStockValue.Size = new Size(270, 120);
            pnlStockValue.TabIndex = 2;
            // 
            // lblTotalStockValue
            // 
            lblTotalStockValue.AutoSize = true;
            lblTotalStockValue.Font = new Font("Segoe UI", 28F, FontStyle.Bold);
            lblTotalStockValue.ForeColor = Color.FromArgb(13, 202, 240);
            lblTotalStockValue.Location = new Point(20, 45);
            lblTotalStockValue.Name = "lblTotalStockValue";
            lblTotalStockValue.Size = new Size(44, 51);
            lblTotalStockValue.TabIndex = 1;
            lblTotalStockValue.Text = "0";
            // 
            // lblStockValueTitle
            // 
            lblStockValueTitle.AutoSize = true;
            lblStockValueTitle.Font = new Font("Segoe UI", 14F, FontStyle.Bold);
            lblStockValueTitle.ForeColor = Color.FromArgb(108, 117, 125);
            lblStockValueTitle.Location = new Point(20, 20);
            lblStockValueTitle.Name = "lblStockValueTitle";
            lblStockValueTitle.Size = new Size(146, 25);
            lblStockValueTitle.TabIndex = 0;
            lblStockValueTitle.Text = "Valor Total";
            // 
            // pnlTotalSKUs
            // 
            pnlTotalSKUs.BackColor = Color.White;
            pnlTotalSKUs.Controls.Add(lblTotalSKUs);
            pnlTotalSKUs.Controls.Add(lblTotalSKUsTitle);
            pnlTotalSKUs.Location = new Point(325, 20);
            pnlTotalSKUs.Name = "pnlTotalSKUs";
            pnlTotalSKUs.Padding = new Padding(20);
            pnlTotalSKUs.Size = new Size(270, 120);
            pnlTotalSKUs.TabIndex = 1;
            // 
            // lblTotalSKUs
            // 
            lblTotalSKUs.AutoSize = true;
            lblTotalSKUs.Font = new Font("Segoe UI", 28F, FontStyle.Bold);
            lblTotalSKUs.ForeColor = Color.FromArgb(25, 135, 84);
            lblTotalSKUs.Location = new Point(20, 45);
            lblTotalSKUs.Name = "lblTotalSKUs";
            lblTotalSKUs.Size = new Size(44, 51);
            lblTotalSKUs.TabIndex = 1;
            lblTotalSKUs.Text = "0";
            // 
            // lblTotalSKUsTitle
            // 
            lblTotalSKUsTitle.AutoSize = true;
            lblTotalSKUsTitle.Font = new Font("Segoe UI", 14F, FontStyle.Bold);
            lblTotalSKUsTitle.ForeColor = Color.FromArgb(108, 117, 125);
            lblTotalSKUsTitle.Location = new Point(20, 20);
            lblTotalSKUsTitle.Name = "lblTotalSKUsTitle";
            lblTotalSKUsTitle.Size = new Size(155, 25);
            lblTotalSKUsTitle.TabIndex = 0;
            lblTotalSKUsTitle.Text = "SKUs con Stock";
            // 
            // pnlTotalItems
            // 
            pnlTotalItems.BackColor = Color.White;
            pnlTotalItems.Controls.Add(lblActiveItems);
            pnlTotalItems.Controls.Add(lblTotalItems);
            pnlTotalItems.Controls.Add(lblTotalItemsTitle);
            pnlTotalItems.Location = new Point(30, 20);
            pnlTotalItems.Name = "pnlTotalItems";
            pnlTotalItems.Padding = new Padding(20);
            pnlTotalItems.Size = new Size(270, 120);
            pnlTotalItems.TabIndex = 0;
            // 
            // lblActiveItems
            // 
            lblActiveItems.AutoSize = true;
            lblActiveItems.Font = new Font("Segoe UI", 10F);
            lblActiveItems.ForeColor = Color.FromArgb(173, 181, 189);
            lblActiveItems.Location = new Point(23, 92);
            lblActiveItems.Name = "lblActiveItems";
            lblActiveItems.Size = new Size(58, 19);
            lblActiveItems.TabIndex = 2;
            lblActiveItems.Text = "0 Activos";
            // 
            // lblTotalItems
            // 
            lblTotalItems.AutoSize = true;
            lblTotalItems.Font = new Font("Segoe UI", 28F, FontStyle.Bold);
            lblTotalItems.ForeColor = Color.FromArgb(13, 110, 253);
            lblTotalItems.Location = new Point(20, 45);
            lblTotalItems.Name = "lblTotalItems";
            lblTotalItems.Size = new Size(44, 51);
            lblTotalItems.TabIndex = 1;
            lblTotalItems.Text = "0";
            // 
            // lblTotalItemsTitle
            // 
            lblTotalItemsTitle.AutoSize = true;
            lblTotalItemsTitle.Font = new Font("Segoe UI", 14F, FontStyle.Bold);
            lblTotalItemsTitle.ForeColor = Color.FromArgb(108, 117, 125);
            lblTotalItemsTitle.Location = new Point(20, 20);
            lblTotalItemsTitle.Name = "lblTotalItemsTitle";
            lblTotalItemsTitle.Size = new Size(108, 25);
            lblTotalItemsTitle.TabIndex = 0;
            lblTotalItemsTitle.Text = "Total de Artículos";
            // 
            // pnlContent
            // 
            pnlContent.BackColor = Color.FromArgb(248, 249, 250);
            pnlContent.Controls.Add(pnlLowStock);
            pnlContent.Controls.Add(pnlRecentMovements);
            pnlContent.Dock = DockStyle.Fill;
            pnlContent.Location = new Point(0, 240);
            pnlContent.Name = "pnlContent";
            pnlContent.Padding = new Padding(30, 20, 30, 20);
            pnlContent.Size = new Size(1200, 460);
            pnlContent.TabIndex = 2;
            // 
            // pnlLowStock
            // 
            pnlLowStock.BackColor = Color.White;
            pnlLowStock.Controls.Add(dgvLowStock);
            pnlLowStock.Controls.Add(lblLowStockCount);
            pnlLowStock.Controls.Add(lblLowStockTitle);
            pnlLowStock.Dock = DockStyle.Fill;
            pnlLowStock.Location = new Point(610, 20);
            pnlLowStock.Margin = new Padding(15, 0, 0, 0);
            pnlLowStock.Name = "pnlLowStock";
            pnlLowStock.Padding = new Padding(20);
            pnlLowStock.Size = new Size(560, 420);
            pnlLowStock.TabIndex = 1;
            // 
            // dgvLowStock
            // 
            dgvLowStock.AllowUserToAddRows = false;
            dgvLowStock.AllowUserToDeleteRows = false;
            dgvLowStock.ColumnHeadersHeightSizeMode = DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            dgvLowStock.Dock = DockStyle.Fill;
            dgvLowStock.Location = new Point(20, 20);
            dgvLowStock.Name = "dgvLowStock";
            dgvLowStock.ReadOnly = true;
            dgvLowStock.SelectionMode = DataGridViewSelectionMode.FullRowSelect;
            dgvLowStock.Size = new Size(520, 380);
            dgvLowStock.TabIndex = 2;
            // 
            // lblLowStockCount
            // 
            lblLowStockCount.AutoSize = true;
            lblLowStockCount.Font = new Font("Segoe UI", 14F, FontStyle.Bold);
            lblLowStockCount.ForeColor = Color.FromArgb(220, 53, 69);
            lblLowStockCount.Location = new Point(211, 22);
            lblLowStockCount.Name = "lblLowStockCount";
            lblLowStockCount.Size = new Size(23, 25);
            lblLowStockCount.TabIndex = 1;
            lblLowStockCount.Text = "0";
            // 
            // lblLowStockTitle
            // 
            lblLowStockTitle.AutoSize = true;
            lblLowStockTitle.Font = new Font("Segoe UI", 16F, FontStyle.Bold);
            lblLowStockTitle.ForeColor = Color.FromArgb(33, 37, 41);
            lblLowStockTitle.Location = new Point(20, 20);
            lblLowStockTitle.Name = "lblLowStockTitle";
            lblLowStockTitle.Padding = new Padding(0, 0, 0, 15);
            lblLowStockTitle.Size = new Size(185, 45);
            lblLowStockTitle.TabIndex = 0;
            lblLowStockTitle.Text = "Alertas de Stock Bajo";
            // 
            // pnlRecentMovements
            // 
            pnlRecentMovements.BackColor = Color.White;
            pnlRecentMovements.Controls.Add(dgvRecentMovements);
            pnlRecentMovements.Controls.Add(lblRecentMovementsTitle);
            pnlRecentMovements.Dock = DockStyle.Left;
            pnlRecentMovements.Location = new Point(30, 20);
            pnlRecentMovements.Name = "pnlRecentMovements";
            pnlRecentMovements.Padding = new Padding(20);
            pnlRecentMovements.Size = new Size(580, 420);
            pnlRecentMovements.TabIndex = 0;
            // 
            // dgvRecentMovements
            // 
            dgvRecentMovements.AllowUserToAddRows = false;
            dgvRecentMovements.AllowUserToDeleteRows = false;
            dgvRecentMovements.ColumnHeadersHeightSizeMode = DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            dgvRecentMovements.Dock = DockStyle.Fill;
            dgvRecentMovements.Location = new Point(20, 65);
            dgvRecentMovements.Name = "dgvRecentMovements";
            dgvRecentMovements.ReadOnly = true;
            dgvRecentMovements.SelectionMode = DataGridViewSelectionMode.FullRowSelect;
            dgvRecentMovements.Size = new Size(540, 335);
            dgvRecentMovements.TabIndex = 1;
            // 
            // lblRecentMovementsTitle
            // 
            lblRecentMovementsTitle.AutoSize = true;
            lblRecentMovementsTitle.Dock = DockStyle.Top;
            lblRecentMovementsTitle.Font = new Font("Segoe UI", 16F, FontStyle.Bold);
            lblRecentMovementsTitle.ForeColor = Color.FromArgb(33, 37, 41);
            lblRecentMovementsTitle.Location = new Point(20, 20);
            lblRecentMovementsTitle.Name = "lblRecentMovementsTitle";
            lblRecentMovementsTitle.Padding = new Padding(0, 0, 0, 15);
            lblRecentMovementsTitle.Size = new Size(211, 45);
            lblRecentMovementsTitle.TabIndex = 0;
            lblRecentMovementsTitle.Text = "Movimientos Recientes";
            // 
            // DashboardForm
            // 
            AutoScaleDimensions = new SizeF(7F, 15F);
            AutoScaleMode = AutoScaleMode.Font;
            BackColor = Color.FromArgb(248, 249, 250);
            ClientSize = new Size(1200, 700);
            Controls.Add(pnlContent);
            Controls.Add(pnlKPIs);
            Controls.Add(pnlHeader);
            FormBorderStyle = FormBorderStyle.None;
            KeyPreview = true;
            Name = "DashboardForm";
            Text = "SGA - Panel de Control";
            WindowState = FormWindowState.Maximized;
            KeyDown += DashboardForm_KeyDown;
            pnlHeader.ResumeLayout(false);
            pnlHeader.PerformLayout();
            pnlKPIs.ResumeLayout(false);
            pnlStockLocations.ResumeLayout(false);
            pnlStockLocations.PerformLayout();
            pnlStockValue.ResumeLayout(false);
            pnlStockValue.PerformLayout();
            pnlTotalSKUs.ResumeLayout(false);
            pnlTotalSKUs.PerformLayout();
            pnlTotalItems.ResumeLayout(false);
            pnlTotalItems.PerformLayout();
            pnlContent.ResumeLayout(false);
            pnlLowStock.ResumeLayout(false);
            pnlLowStock.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)dgvLowStock).EndInit();
            pnlRecentMovements.ResumeLayout(false);
            pnlRecentMovements.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)dgvRecentMovements).EndInit();
            ResumeLayout(false);
        }

        #endregion
    }
}