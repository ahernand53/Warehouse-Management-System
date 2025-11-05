// Wms.WinForms/Forms/MainForm.Designer.cs

using Wms.WinForms.Common;

namespace Wms.WinForms.Forms
{
    partial class MainForm
    {
        private System.ComponentModel.IContainer components = null;
        private Panel pnlHeader;
        private Label lblTitle;
        private Panel pnlNavigation;
        private Panel pnlOperations;
        private Label lblOperationsTitle;
        private Button btnDashboard;
        private Button btnReceiving;
        private Button btnPutaway;
        private Button btnPicking;
        private Button btnInventory;
        private Panel pnlManagement;
        private Label lblManagementTitle;
        private Button btnItemManagement;
        private Button btnLocationManagement;
        private Button btnReports;
        private Panel pnlContent;
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
            pnlNavigation = new Panel();
            pnlManagement = new Panel();
            btnReports = new Button();
            btnLocationManagement = new Button();
            btnItemManagement = new Button();
            lblManagementTitle = new Label();
            pnlOperations = new Panel();
            btnInventory = new Button();
            btnPicking = new Button();
            btnPutaway = new Button();
            btnReceiving = new Button();
            btnDashboard = new Button();
            lblOperationsTitle = new Label();
            pnlContent = new Panel();
            statusStrip = new StatusStrip();
            lblStatus = new ToolStripStatusLabel();
            pnlHeader.SuspendLayout();
            pnlNavigation.SuspendLayout();
            pnlManagement.SuspendLayout();
            pnlOperations.SuspendLayout();
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
            pnlHeader.Size = new Size(1400, 80);
            pnlHeader.TabIndex = 0;
            // 
            // lblTitle
            // 
            lblTitle.AutoSize = true;
            lblTitle.Font = new Font("Segoe UI", 28F, FontStyle.Bold);
            lblTitle.ForeColor = Color.FromArgb(13, 110, 253);
            lblTitle.Location = new Point(32, 14);
            lblTitle.Name = "lblTitle";
            lblTitle.Size = new Size(670, 51);
            lblTitle.TabIndex = 0;
            lblTitle.Text = "📦 Sistema de Gestión de Almacén";
            // 
            // pnlNavigation
            // 
            pnlNavigation.BackColor = Color.FromArgb(233, 236, 239);
            pnlNavigation.Controls.Add(pnlManagement);
            pnlNavigation.Controls.Add(pnlOperations);
            pnlNavigation.Dock = DockStyle.Left;
            pnlNavigation.Location = new Point(0, 80);
            pnlNavigation.Name = "pnlNavigation";
            pnlNavigation.Padding = new Padding(15);
            pnlNavigation.Size = new Size(250, 698);
            pnlNavigation.TabIndex = 1;
            // 
            // pnlManagement
            // 
            pnlManagement.BackColor = Color.White;
            pnlManagement.Controls.Add(btnReports);
            pnlManagement.Controls.Add(btnLocationManagement);
            pnlManagement.Controls.Add(btnItemManagement);
            pnlManagement.Controls.Add(lblManagementTitle);
            pnlManagement.Dock = DockStyle.Fill;
            pnlManagement.Location = new Point(15, 335);
            pnlManagement.Name = "pnlManagement";
            pnlManagement.Padding = new Padding(15);
            pnlManagement.Size = new Size(220, 348);
            pnlManagement.TabIndex = 1;
            // 
            // btnReports
            // 
            btnReports.Location = new Point(15, 140);
            btnReports.Name = "btnReports";
            btnReports.Size = new Size(190, 45);
            btnReports.TabIndex = 3;
            btnReports.Text = "📈 Reportes";
            btnReports.TextAlign = ContentAlignment.MiddleLeft;
            btnReports.UseVisualStyleBackColor = true;
            // 
            // btnLocationManagement
            // 
            btnLocationManagement.Location = new Point(15, 95);
            btnLocationManagement.Name = "btnLocationManagement";
            btnLocationManagement.Size = new Size(190, 45);
            btnLocationManagement.TabIndex = 2;
            btnLocationManagement.Text = "📍 Ubicaciones";
            btnLocationManagement.TextAlign = ContentAlignment.MiddleLeft;
            btnLocationManagement.UseVisualStyleBackColor = true;
            // 
            // btnItemManagement
            // 
            btnItemManagement.Location = new Point(15, 50);
            btnItemManagement.Name = "btnItemManagement";
            btnItemManagement.Size = new Size(190, 45);
            btnItemManagement.TabIndex = 1;
            btnItemManagement.Text = "🏷️ Artículos";
            btnItemManagement.TextAlign = ContentAlignment.MiddleLeft;
            btnItemManagement.UseVisualStyleBackColor = true;
            // 
            // lblManagementTitle
            // 
            lblManagementTitle.AutoSize = true;
            lblManagementTitle.Font = new Font("Segoe UI", 16F, FontStyle.Bold);
            lblManagementTitle.ForeColor = Color.FromArgb(33, 37, 41);
            lblManagementTitle.Location = new Point(15, 15);
            lblManagementTitle.Name = "lblManagementTitle";
            lblManagementTitle.Size = new Size(151, 30);
            lblManagementTitle.TabIndex = 0;
            lblManagementTitle.Text = "Gestión";
            // 
            // pnlOperations
            // 
            pnlOperations.BackColor = Color.White;
            pnlOperations.Controls.Add(btnInventory);
            pnlOperations.Controls.Add(btnPicking);
            pnlOperations.Controls.Add(btnPutaway);
            pnlOperations.Controls.Add(btnReceiving);
            pnlOperations.Controls.Add(btnDashboard);
            pnlOperations.Controls.Add(lblOperationsTitle);
            pnlOperations.Dock = DockStyle.Top;
            pnlOperations.Location = new Point(15, 15);
            pnlOperations.Name = "pnlOperations";
            pnlOperations.Padding = new Padding(15);
            pnlOperations.Size = new Size(220, 320);
            pnlOperations.TabIndex = 0;
            // 
            // btnInventory
            // 
            btnInventory.Location = new Point(15, 230);
            btnInventory.Name = "btnInventory";
            btnInventory.Size = new Size(190, 45);
            btnInventory.TabIndex = 5;
            btnInventory.Text = "📋 Inventario";
            btnInventory.TextAlign = ContentAlignment.MiddleLeft;
            btnInventory.UseVisualStyleBackColor = true;
            // 
            // btnPicking
            // 
            btnPicking.Location = new Point(15, 185);
            btnPicking.Name = "btnPicking";
            btnPicking.Size = new Size(190, 45);
            btnPicking.TabIndex = 4;
            btnPicking.Text = "🎯 Picking";
            btnPicking.TextAlign = ContentAlignment.MiddleLeft;
            btnPicking.UseVisualStyleBackColor = true;
            // 
            // btnPutaway
            // 
            btnPutaway.Location = new Point(15, 140);
            btnPutaway.Name = "btnPutaway";
            btnPutaway.Size = new Size(190, 45);
            btnPutaway.TabIndex = 3;
            btnPutaway.Text = "Almacenamiento";
            btnPutaway.TextAlign = ContentAlignment.MiddleLeft;
            btnPutaway.UseVisualStyleBackColor = true;
            // 
            // btnReceiving
            // 
            btnReceiving.Location = new Point(15, 95);
            btnReceiving.Name = "btnReceiving";
            btnReceiving.Size = new Size(190, 45);
            btnReceiving.TabIndex = 2;
            btnReceiving.Text = "📦 Recepción";
            btnReceiving.TextAlign = ContentAlignment.MiddleLeft;
            btnReceiving.UseVisualStyleBackColor = true;
            // 
            // btnDashboard
            // 
            btnDashboard.Location = new Point(15, 50);
            btnDashboard.Name = "btnDashboard";
            btnDashboard.Size = new Size(190, 45);
            btnDashboard.TabIndex = 1;
            btnDashboard.Text = "📊 Panel de Control";
            btnDashboard.TextAlign = ContentAlignment.MiddleLeft;
            btnDashboard.UseVisualStyleBackColor = true;
            // 
            // lblOperationsTitle
            // 
            lblOperationsTitle.AutoSize = true;
            lblOperationsTitle.Font = new Font("Segoe UI", 16F, FontStyle.Bold);
            lblOperationsTitle.ForeColor = Color.FromArgb(33, 37, 41);
            lblOperationsTitle.Location = new Point(15, 15);
            lblOperationsTitle.Name = "lblOperationsTitle";
            lblOperationsTitle.Size = new Size(128, 30);
            lblOperationsTitle.TabIndex = 0;
            lblOperationsTitle.Text = "Operaciones";
            // 
            // pnlContent
            // 
            pnlContent.BackColor = Color.FromArgb(248, 249, 250);
            pnlContent.Dock = DockStyle.Fill;
            pnlContent.Location = new Point(250, 80);
            pnlContent.Name = "pnlContent";
            pnlContent.Size = new Size(1150, 698);
            pnlContent.TabIndex = 2;
            // 
            // statusStrip
            // 
            statusStrip.Items.AddRange(new ToolStripItem[] { lblStatus });
            statusStrip.Location = new Point(0, 778);
            statusStrip.Name = "statusStrip";
            statusStrip.Size = new Size(1400, 22);
            statusStrip.TabIndex = 3;
            // 
            // lblStatus
            // 
            lblStatus.Name = "lblStatus";
            lblStatus.Size = new Size(70, 17);
            lblStatus.Text = "SGA Listo";
            // 
            // MainForm
            // 
            AutoScaleDimensions = new SizeF(7F, 15F);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(1400, 800);
            Controls.Add(pnlContent);
            Controls.Add(pnlNavigation);
            Controls.Add(pnlHeader);
            Controls.Add(statusStrip);
            KeyPreview = true;
            MinimumSize = new Size(1200, 700);
            Name = "MainForm";
            StartPosition = FormStartPosition.CenterScreen;
            Text = "Sistema de Gestión de Almacén";
            WindowState = FormWindowState.Maximized;
            pnlHeader.ResumeLayout(false);
            pnlHeader.PerformLayout();
            pnlNavigation.ResumeLayout(false);
            pnlManagement.ResumeLayout(false);
            pnlManagement.PerformLayout();
            pnlOperations.ResumeLayout(false);
            pnlOperations.PerformLayout();
            statusStrip.ResumeLayout(false);
            statusStrip.PerformLayout();
            ResumeLayout(false);
            PerformLayout();
        }
    }
}