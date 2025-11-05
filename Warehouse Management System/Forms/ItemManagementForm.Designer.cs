// Wms.WinForms/Forms/ItemManagementForm.Designer.cs

using Wms.WinForms.Common;

namespace Wms.WinForms.Forms
{
    partial class ItemManagementForm
    {
        private System.ComponentModel.IContainer components = null;
        private Panel pnlHeader;
        private Label lblTitle;
        private Panel pnlSearch;
        private Label lblSearch;
        private TextBox txtSearch;
        private Button btnSearch;
        private Panel pnlActions;
        private Button btnAdd;
        private Button btnEdit;
        private Button btnDelete;
        private Button btnRefresh;
        private Panel pnlMain;
        private DataGridView dgvItems;
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
            btnRefresh = new Button();
            btnDelete = new Button();
            btnEdit = new Button();
            btnAdd = new Button();
            pnlMain = new Panel();
            dgvItems = new DataGridView();
            statusStrip = new StatusStrip();
            lblStatus = new ToolStripStatusLabel();
            pnlHeader.SuspendLayout();
            pnlSearch.SuspendLayout();
            pnlActions.SuspendLayout();
            pnlMain.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)dgvItems).BeginInit();
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
            lblTitle.Size = new Size(295, 45);
            lblTitle.TabIndex = 0;
            lblTitle.Text = "Gestión de Artículos";
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
            txtSearch.Location = new Point(115, 29);
            txtSearch.Name = "txtSearch";
            txtSearch.PlaceholderText = "Buscar por SKU, nombre, descripción o código de barras...";
            txtSearch.Size = new Size(945, 23);
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
            pnlActions.Controls.Add(btnRefresh);
            pnlActions.Controls.Add(btnDelete);
            pnlActions.Controls.Add(btnEdit);
            pnlActions.Controls.Add(btnAdd);
            pnlActions.Dock = DockStyle.Top;
            pnlActions.Location = new Point(0, 150);
            pnlActions.Name = "pnlActions";
            pnlActions.Padding = new Padding(30, 15, 30, 15);
            pnlActions.Size = new Size(1200, 70);
            pnlActions.TabIndex = 2;
            // 
            // btnRefresh
            // 
            btnRefresh.Location = new Point(360, 20);
            btnRefresh.Name = "btnRefresh";
            btnRefresh.Size = new Size(120, 40);
            btnRefresh.TabIndex = 3;
            btnRefresh.Text = "Actualizar (F5)";
            btnRefresh.UseVisualStyleBackColor = true;
            // 
            // btnDelete
            // 
            btnDelete.Enabled = false;
            btnDelete.Location = new Point(250, 20);
            btnDelete.Name = "btnDelete";
            btnDelete.Size = new Size(100, 40);
            btnDelete.TabIndex = 2;
            btnDelete.Text = "Eliminar (Del)";
            btnDelete.UseVisualStyleBackColor = true;
            // 
            // btnEdit
            // 
            btnEdit.Enabled = false;
            btnEdit.Location = new Point(140, 20);
            btnEdit.Name = "btnEdit";
            btnEdit.Size = new Size(100, 40);
            btnEdit.TabIndex = 1;
            btnEdit.Text = "Editar (F2)";
            btnEdit.UseVisualStyleBackColor = true;
            // 
            // btnAdd
            // 
            btnAdd.Location = new Point(30, 20);
            btnAdd.Name = "btnAdd";
            btnAdd.Size = new Size(100, 40);
            btnAdd.TabIndex = 0;
            btnAdd.Text = "Agregar (F1)";
            btnAdd.UseVisualStyleBackColor = true;
            // 
            // pnlMain
            // 
            pnlMain.BackColor = Color.FromArgb(248, 249, 250);
            pnlMain.Controls.Add(dgvItems);
            pnlMain.Dock = DockStyle.Fill;
            pnlMain.Location = new Point(0, 220);
            pnlMain.Name = "pnlMain";
            pnlMain.Padding = new Padding(30, 0, 30, 20);
            pnlMain.Size = new Size(1200, 458);
            pnlMain.TabIndex = 3;
            // 
            // dgvItems
            // 
            dgvItems.AllowUserToAddRows = false;
            dgvItems.AllowUserToDeleteRows = false;
            dgvItems.BackgroundColor = Color.White;
            dgvItems.ColumnHeadersHeightSizeMode = DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            dgvItems.Dock = DockStyle.Fill;
            dgvItems.Location = new Point(30, 0);
            dgvItems.MultiSelect = false;
            dgvItems.Name = "dgvItems";
            dgvItems.ReadOnly = true;
            dgvItems.SelectionMode = DataGridViewSelectionMode.FullRowSelect;
            dgvItems.Size = new Size(1140, 438);
            dgvItems.TabIndex = 0;
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
            // ItemManagementForm
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
            Name = "ItemManagementForm";
            Text = "WMS - Item Management";
            WindowState = FormWindowState.Maximized;
            pnlHeader.ResumeLayout(false);
            pnlHeader.PerformLayout();
            pnlSearch.ResumeLayout(false);
            pnlSearch.PerformLayout();
            pnlActions.ResumeLayout(false);
            pnlMain.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)dgvItems).EndInit();
            statusStrip.ResumeLayout(false);
            statusStrip.PerformLayout();
            ResumeLayout(false);
            PerformLayout();
        }
    }
}