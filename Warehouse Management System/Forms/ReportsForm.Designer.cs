// Wms.WinForms/Forms/ReportsForm.Designer.cs

using Wms.WinForms.Common;

namespace Wms.WinForms.Forms
{
    partial class ReportsForm
    {
        private System.ComponentModel.IContainer components = null;
        private Panel pnlHeader;
        private Label lblTitle;
        private Panel pnlFilters;
        private Label lblFiltersTitle;
        private Label lblFromDate;
        private DateTimePicker dtpFromDate;
        private Label lblToDate;
        private DateTimePicker dtpToDate;
        private Label lblItemSku;
        private TextBox txtItemSku;
        private Label lblMovementType;
        private ComboBox cmbMovementType;
        private Panel pnlActions;
        private Button btnGenerateReport;
        private Button btnExport;
        private Panel pnlMain;
        private DataGridView dgvMovements;
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
            this.pnlHeader = new Panel();
            this.lblTitle = new Label();
            this.pnlFilters = new Panel();
            this.lblFiltersTitle = new Label();
            this.lblFromDate = new Label();
            this.dtpFromDate = new DateTimePicker();
            this.lblToDate = new Label();
            this.dtpToDate = new DateTimePicker();
            this.lblItemSku = new Label();
            this.txtItemSku = new TextBox();
            this.lblMovementType = new Label();
            this.cmbMovementType = new ComboBox();
            this.pnlActions = new Panel();
            this.btnGenerateReport = new Button();
            this.btnExport = new Button();
            this.pnlMain = new Panel();
            this.dgvMovements = new DataGridView();
            this.statusStrip = new StatusStrip();
            this.lblStatus = new ToolStripStatusLabel();

            this.pnlHeader.SuspendLayout();
            this.pnlFilters.SuspendLayout();
            this.pnlActions.SuspendLayout();
            this.pnlMain.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dgvMovements)).BeginInit();
            this.statusStrip.SuspendLayout();
            this.SuspendLayout();

            // 
            // pnlHeader - Fixed header
            // 
            this.pnlHeader.BackColor = Color.White;
            this.pnlHeader.Controls.Add(this.lblTitle);
            this.pnlHeader.Dock = DockStyle.Top;
            this.pnlHeader.Height = 70;
            this.pnlHeader.Name = "pnlHeader";
            this.pnlHeader.Padding = new Padding(25, 15, 25, 15);
            this.pnlHeader.TabIndex = 0;

            // 
            // lblTitle
            // 
            this.lblTitle.AutoSize = true;
            this.lblTitle.Font = ModernUIHelper.Fonts.H3;
            this.lblTitle.ForeColor = ModernUIHelper.Colors.TextPrimary;
            this.lblTitle.Location = new Point(25, 20);
            this.lblTitle.Name = "lblTitle";
            this.lblTitle.Size = new Size(195, 32);
            this.lblTitle.TabIndex = 0;
            this.lblTitle.Text = "📊 Reportes de Movimientos";

            // 
            // pnlFilters - Filters card with fixed height
            // 
            this.pnlFilters.BackColor = Color.White;
            this.pnlFilters.Controls.Add(this.cmbMovementType);
            this.pnlFilters.Controls.Add(this.lblMovementType);
            this.pnlFilters.Controls.Add(this.txtItemSku);
            this.pnlFilters.Controls.Add(this.lblItemSku);
            this.pnlFilters.Controls.Add(this.dtpToDate);
            this.pnlFilters.Controls.Add(this.lblToDate);
            this.pnlFilters.Controls.Add(this.dtpFromDate);
            this.pnlFilters.Controls.Add(this.lblFromDate);
            this.pnlFilters.Controls.Add(this.lblFiltersTitle);
            this.pnlFilters.Dock = DockStyle.Top;
            this.pnlFilters.Height = 120;
            this.pnlFilters.Name = "pnlFilters";
            this.pnlFilters.Padding = new Padding(25, 15, 25, 15);
            this.pnlFilters.TabIndex = 1;

            // 
            // lblFiltersTitle
            // 
            this.lblFiltersTitle.AutoSize = true;
            this.lblFiltersTitle.Font = ModernUIHelper.Fonts.H5;
            this.lblFiltersTitle.ForeColor = ModernUIHelper.Colors.TextPrimary;
            this.lblFiltersTitle.Location = new Point(25, 15);
            this.lblFiltersTitle.Name = "lblFiltersTitle";
            this.lblFiltersTitle.Size = new Size(106, 21);
            this.lblFiltersTitle.TabIndex = 0;
            this.lblFiltersTitle.Text = "Filtros del Reporte";

            // 
            // lblFromDate
            // 
            this.lblFromDate.AutoSize = true;
            this.lblFromDate.Font = ModernUIHelper.Fonts.H6;
            this.lblFromDate.ForeColor = ModernUIHelper.Colors.TextPrimary;
            this.lblFromDate.Location = new Point(25, 45);
            this.lblFromDate.Name = "lblFromDate";
            this.lblFromDate.Size = new Size(83, 19);
            this.lblFromDate.TabIndex = 1;
            this.lblFromDate.Text = "Fecha Desde:";

            // 
            // dtpFromDate
            // 
            this.dtpFromDate.Format = DateTimePickerFormat.Short;
            this.dtpFromDate.Location = new Point(25, 70);
            this.dtpFromDate.Name = "dtpFromDate";
            this.dtpFromDate.Size = new Size(150, 27);
            this.dtpFromDate.TabIndex = 2;

            // 
            // lblToDate
            // 
            this.lblToDate.AutoSize = true;
            this.lblToDate.Font = ModernUIHelper.Fonts.H6;
            this.lblToDate.ForeColor = ModernUIHelper.Colors.TextPrimary;
            this.lblToDate.Location = new Point(190, 45);
            this.lblToDate.Name = "lblToDate";
            this.lblToDate.Size = new Size(66, 19);
            this.lblToDate.TabIndex = 3;
            this.lblToDate.Text = "Fecha Hasta:";

            // 
            // dtpToDate
            // 
            this.dtpToDate.Format = DateTimePickerFormat.Short;
            this.dtpToDate.Location = new Point(190, 70);
            this.dtpToDate.Name = "dtpToDate";
            this.dtpToDate.Size = new Size(150, 27);
            this.dtpToDate.TabIndex = 4;

            // 
            // lblItemSku
            // 
            this.lblItemSku.AutoSize = true;
            this.lblItemSku.Font = ModernUIHelper.Fonts.H6;
            this.lblItemSku.ForeColor = ModernUIHelper.Colors.TextPrimary;
            this.lblItemSku.Location = new Point(360, 45);
            this.lblItemSku.Name = "lblItemSku";
            this.lblItemSku.Size = new Size(72, 19);
            this.lblItemSku.TabIndex = 5;
            this.lblItemSku.Text = "SKU del Artículo:";

            // 
            // txtItemSku
            // 
            this.txtItemSku.Location = new Point(360, 70);
            this.txtItemSku.Name = "txtItemSku";
            this.txtItemSku.PlaceholderText = "Filtro opcional por SKU";
            this.txtItemSku.Size = new Size(200, 27);
            this.txtItemSku.TabIndex = 6;

            // 
            // lblMovementType
            // 
            this.lblMovementType.AutoSize = true;
            this.lblMovementType.Font = ModernUIHelper.Fonts.H6;
            this.lblMovementType.ForeColor = ModernUIHelper.Colors.TextPrimary;
            this.lblMovementType.Location = new Point(580, 45);
            this.lblMovementType.Name = "lblMovementType";
            this.lblMovementType.Size = new Size(124, 19);
            this.lblMovementType.TabIndex = 7;
            this.lblMovementType.Text = "Tipo de Movimiento:";

            // 
            // cmbMovementType
            // 
            this.cmbMovementType.DropDownStyle = ComboBoxStyle.DropDownList;
            this.cmbMovementType.FormattingEnabled = true;
            this.cmbMovementType.Location = new Point(580, 70);
            this.cmbMovementType.Name = "cmbMovementType";
            this.cmbMovementType.Size = new Size(180, 28);
            this.cmbMovementType.TabIndex = 8;

            // 
            // pnlActions - Action buttons panel
            // 
            this.pnlActions.BackColor = Color.White;
            this.pnlActions.Controls.Add(this.btnExport);
            this.pnlActions.Controls.Add(this.btnGenerateReport);
            this.pnlActions.Dock = DockStyle.Top;
            this.pnlActions.Height = 70;
            this.pnlActions.Name = "pnlActions";
            this.pnlActions.Padding = new Padding(25, 15, 25, 15);
            this.pnlActions.TabIndex = 2;

            // 
            // btnGenerateReport
            // 
            this.btnGenerateReport.Location = new Point(25, 20);
            this.btnGenerateReport.Name = "btnGenerateReport";
            this.btnGenerateReport.Size = new Size(150, 40);
            this.btnGenerateReport.TabIndex = 0;
            this.btnGenerateReport.Text = "Generar (F1)";
            this.btnGenerateReport.UseVisualStyleBackColor = true;

            // 
            // btnExport
            // 
            this.btnExport.Enabled = false;
            this.btnExport.Location = new Point(185, 20);
            this.btnExport.Name = "btnExport";
            this.btnExport.Size = new Size(120, 40);
            this.btnExport.TabIndex = 1;
            this.btnExport.Text = "Exportar (F2)";
            this.btnExport.UseVisualStyleBackColor = true;

            // 
            // pnlMain - Main content container
            // 
            this.pnlMain.BackColor = ModernUIHelper.Colors.BackgroundPrimary;
            this.pnlMain.Controls.Add(this.dgvMovements);
            this.pnlMain.Dock = DockStyle.Fill;
            this.pnlMain.Name = "pnlMain";
            this.pnlMain.Padding = new Padding(25, 0, 25, 15);
            this.pnlMain.TabIndex = 3;

            // 
            // dgvMovements
            // 
            this.dgvMovements.AllowUserToAddRows = false;
            this.dgvMovements.AllowUserToDeleteRows = false;
            this.dgvMovements.BackgroundColor = Color.White;
            this.dgvMovements.ColumnHeadersHeightSizeMode = DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dgvMovements.Dock = DockStyle.Fill;
            this.dgvMovements.Name = "dgvMovements";
            this.dgvMovements.ReadOnly = true;
            this.dgvMovements.SelectionMode = DataGridViewSelectionMode.FullRowSelect;
            this.dgvMovements.TabIndex = 0;

            // 
            // statusStrip
            // 
            this.statusStrip.Items.AddRange(new ToolStripItem[] { this.lblStatus });
            this.statusStrip.Location = new Point(0, 678);
            this.statusStrip.Name = "statusStrip";
            this.statusStrip.Size = new Size(800, 22);
            this.statusStrip.TabIndex = 4;

            // 
            // lblStatus
            // 
            this.lblStatus.Name = "lblStatus";
            this.lblStatus.Size = new Size(139, 17);
            this.lblStatus.Text = "Listo para generar reporte";

            // 
            // ReportsForm
            // 
            this.AutoScaleDimensions = new SizeF(7F, 15F);
            this.AutoScaleMode = AutoScaleMode.Font;
            this.BackColor = ModernUIHelper.Colors.BackgroundPrimary;
            this.ClientSize = new Size(800, 700);
            this.Controls.Add(this.pnlMain);
            this.Controls.Add(this.pnlActions);
            this.Controls.Add(this.pnlFilters);
            this.Controls.Add(this.pnlHeader);
            this.Controls.Add(this.statusStrip);
            this.KeyPreview = true;
            this.MinimumSize = new Size(800, 600);
            this.Name = "ReportsForm";
            this.StartPosition = FormStartPosition.CenterParent;
            this.Text = "SGA - Reportes de Movimientos";

            this.pnlHeader.ResumeLayout(false);
            this.pnlHeader.PerformLayout();
            this.pnlFilters.ResumeLayout(false);
            this.pnlFilters.PerformLayout();
            this.pnlActions.ResumeLayout(false);
            this.pnlMain.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.dgvMovements)).EndInit();
            this.statusStrip.ResumeLayout(false);
            this.statusStrip.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();
        }
    }
}