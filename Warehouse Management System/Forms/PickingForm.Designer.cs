// Wms.WinForms/Forms/PickingForm.Designer.cs

using Wms.WinForms.Common;

namespace Wms.WinForms.Forms
{
    partial class PickingForm
    {
        private System.ComponentModel.IContainer components = null;
        private Panel pnlHeader;
        private Label lblTitle;
        private Panel pnlMain;
        private Panel pnlItemInfo;
        private Label lblBarcodeTitle;
        private TextBox txtBarcode;
        private Label lblItemInfo;
        private Panel pnlPickingDetails;
        private Label lblFromLocationTitle;
        private TextBox txtFromLocation;
        private Label lblQuantityTitle;
        private TextBox txtQuantity;
        private Label lblOrderNumberTitle;
        private TextBox txtOrderNumber;
        private Label lblNotesTitle;
        private TextBox txtNotes;
        private Panel pnlActions;
        private Button btnPick;
        private Button btnClear;
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
            pnlMain = new Panel();
            pnlPickingDetails = new Panel();
            txtNotes = new TextBox();
            lblNotesTitle = new Label();
            txtOrderNumber = new TextBox();
            lblOrderNumberTitle = new Label();
            txtQuantity = new TextBox();
            lblQuantityTitle = new Label();
            txtFromLocation = new TextBox();
            lblFromLocationTitle = new Label();
            pnlItemInfo = new Panel();
            lblItemInfo = new Label();
            txtBarcode = new TextBox();
            lblBarcodeTitle = new Label();
            pnlActions = new Panel();
            btnClear = new Button();
            btnPick = new Button();
            statusStrip = new StatusStrip();
            lblStatus = new ToolStripStatusLabel();
            pnlHeader.SuspendLayout();
            pnlMain.SuspendLayout();
            pnlPickingDetails.SuspendLayout();
            pnlItemInfo.SuspendLayout();
            pnlActions.SuspendLayout();
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
            pnlHeader.Padding = new Padding(25, 15, 25, 15);
            pnlHeader.Size = new Size(850, 70);
            pnlHeader.TabIndex = 0;
            // 
            // lblTitle
            // 
            lblTitle.AutoSize = true;
            lblTitle.Font = new Font("Segoe UI", 24F, FontStyle.Bold);
            lblTitle.ForeColor = Color.FromArgb(220, 53, 69);
            lblTitle.Location = new Point(25, 20);
            lblTitle.Name = "lblTitle";
            lblTitle.Size = new Size(167, 45);
            lblTitle.TabIndex = 0;
            lblTitle.Text = "🎯 Picking";
            // 
            // pnlMain
            // 
            pnlMain.BackColor = Color.FromArgb(248, 249, 250);
            pnlMain.Controls.Add(pnlPickingDetails);
            pnlMain.Controls.Add(pnlItemInfo);
            pnlMain.Dock = DockStyle.Fill;
            pnlMain.Location = new Point(0, 70);
            pnlMain.Name = "pnlMain";
            pnlMain.Padding = new Padding(25, 20, 25, 20);
            pnlMain.Size = new Size(850, 440);
            pnlMain.TabIndex = 1;
            // 
            // pnlPickingDetails
            // 
            pnlPickingDetails.BackColor = Color.White;
            pnlPickingDetails.Controls.Add(txtNotes);
            pnlPickingDetails.Controls.Add(lblNotesTitle);
            pnlPickingDetails.Controls.Add(txtOrderNumber);
            pnlPickingDetails.Controls.Add(lblOrderNumberTitle);
            pnlPickingDetails.Controls.Add(txtQuantity);
            pnlPickingDetails.Controls.Add(lblQuantityTitle);
            pnlPickingDetails.Controls.Add(txtFromLocation);
            pnlPickingDetails.Controls.Add(lblFromLocationTitle);
            pnlPickingDetails.Dock = DockStyle.Fill;
            pnlPickingDetails.Location = new Point(25, 160);
            pnlPickingDetails.Name = "pnlPickingDetails";
            pnlPickingDetails.Padding = new Padding(20);
            pnlPickingDetails.Size = new Size(800, 260);
            pnlPickingDetails.TabIndex = 1;
            // 
            // txtNotes
            // 
            txtNotes.Anchor = AnchorStyles.Top | AnchorStyles.Left | AnchorStyles.Right;
            txtNotes.Location = new Point(20, 115);
            txtNotes.Multiline = true;
            txtNotes.Name = "txtNotes";
            txtNotes.PlaceholderText = "Notas opcionales para el picking...";
            txtNotes.Size = new Size(760, 125);
            txtNotes.TabIndex = 7;
            // 
            // lblNotesTitle
            // 
            lblNotesTitle.AutoSize = true;
            lblNotesTitle.Font = new Font("Segoe UI", 14F, FontStyle.Bold);
            lblNotesTitle.ForeColor = Color.FromArgb(33, 37, 41);
            lblNotesTitle.Location = new Point(20, 90);
            lblNotesTitle.Name = "lblNotesTitle";
            lblNotesTitle.Size = new Size(69, 25);
            lblNotesTitle.TabIndex = 6;
            lblNotesTitle.Text = "Notas:";
            // 
            // txtOrderNumber
            // 
            txtOrderNumber.Anchor = AnchorStyles.Top | AnchorStyles.Left | AnchorStyles.Right;
            txtOrderNumber.Location = new Point(420, 45);
            txtOrderNumber.Name = "txtOrderNumber";
            txtOrderNumber.PlaceholderText = "Número de orden opcional";
            txtOrderNumber.Size = new Size(360, 23);
            txtOrderNumber.TabIndex = 5;
            // 
            // lblOrderNumberTitle
            // 
            lblOrderNumberTitle.AutoSize = true;
            lblOrderNumberTitle.Font = new Font("Segoe UI", 14F, FontStyle.Bold);
            lblOrderNumberTitle.ForeColor = Color.FromArgb(33, 37, 41);
            lblOrderNumberTitle.Location = new Point(420, 20);
            lblOrderNumberTitle.Name = "lblOrderNumberTitle";
            lblOrderNumberTitle.Size = new Size(149, 25);
            lblOrderNumberTitle.TabIndex = 4;
            lblOrderNumberTitle.Text = "Número de Orden:";
            // 
            // txtQuantity
            // 
            txtQuantity.Location = new Point(250, 45);
            txtQuantity.Name = "txtQuantity";
            txtQuantity.Size = new Size(150, 23);
            txtQuantity.TabIndex = 3;
            // 
            // lblQuantityTitle
            // 
            lblQuantityTitle.AutoSize = true;
            lblQuantityTitle.Font = new Font("Segoe UI", 14F, FontStyle.Bold);
            lblQuantityTitle.ForeColor = Color.FromArgb(33, 37, 41);
            lblQuantityTitle.Location = new Point(250, 20);
            lblQuantityTitle.Name = "lblQuantityTitle";
            lblQuantityTitle.Size = new Size(94, 25);
            lblQuantityTitle.TabIndex = 2;
            lblQuantityTitle.Text = "Cantidad:";
            // 
            // txtFromLocation
            // 
            txtFromLocation.Location = new Point(20, 45);
            txtFromLocation.Name = "txtFromLocation";
            txtFromLocation.Size = new Size(200, 23);
            txtFromLocation.TabIndex = 1;
            // 
            // lblFromLocationTitle
            // 
            lblFromLocationTitle.AutoSize = true;
            lblFromLocationTitle.Font = new Font("Segoe UI", 14F, FontStyle.Bold);
            lblFromLocationTitle.ForeColor = Color.FromArgb(33, 37, 41);
            lblFromLocationTitle.Location = new Point(20, 20);
            lblFromLocationTitle.Name = "lblFromLocationTitle";
            lblFromLocationTitle.Size = new Size(146, 25);
            lblFromLocationTitle.TabIndex = 0;
            lblFromLocationTitle.Text = "Ubicación de Origen:";
            // 
            // pnlItemInfo
            // 
            pnlItemInfo.BackColor = Color.White;
            pnlItemInfo.Controls.Add(lblItemInfo);
            pnlItemInfo.Controls.Add(txtBarcode);
            pnlItemInfo.Controls.Add(lblBarcodeTitle);
            pnlItemInfo.Dock = DockStyle.Top;
            pnlItemInfo.Location = new Point(25, 20);
            pnlItemInfo.Name = "pnlItemInfo";
            pnlItemInfo.Padding = new Padding(20);
            pnlItemInfo.Size = new Size(800, 140);
            pnlItemInfo.TabIndex = 0;
            // 
            // lblItemInfo
            // 
            lblItemInfo.Anchor = AnchorStyles.Top | AnchorStyles.Bottom | AnchorStyles.Left | AnchorStyles.Right;
            lblItemInfo.BackColor = Color.FromArgb(233, 236, 239);
            lblItemInfo.Font = new Font("Segoe UI", 11F);
            lblItemInfo.ForeColor = Color.FromArgb(173, 181, 189);
            lblItemInfo.Location = new Point(20, 90);
            lblItemInfo.Name = "lblItemInfo";
            lblItemInfo.Padding = new Padding(15);
            lblItemInfo.Size = new Size(760, 30);
            lblItemInfo.TabIndex = 2;
            lblItemInfo.Text = "No hay artículo seleccionado";
            lblItemInfo.TextAlign = ContentAlignment.MiddleLeft;
            // 
            // txtBarcode
            // 
            txtBarcode.Anchor = AnchorStyles.Top | AnchorStyles.Left | AnchorStyles.Right;
            txtBarcode.Font = new Font("Segoe UI", 12F);
            txtBarcode.Location = new Point(20, 50);
            txtBarcode.Name = "txtBarcode";
            txtBarcode.PlaceholderText = "Escanee el código de barras aquí...";
            txtBarcode.Size = new Size(760, 29);
            txtBarcode.TabIndex = 1;
            // 
            // lblBarcodeTitle
            // 
            lblBarcodeTitle.AutoSize = true;
            lblBarcodeTitle.Font = new Font("Segoe UI", 16F, FontStyle.Bold);
            lblBarcodeTitle.ForeColor = Color.FromArgb(33, 37, 41);
            lblBarcodeTitle.Location = new Point(20, 20);
            lblBarcodeTitle.Name = "lblBarcodeTitle";
            lblBarcodeTitle.Size = new Size(247, 30);
            lblBarcodeTitle.TabIndex = 0;
            lblBarcodeTitle.Text = "Escanear o Ingresar Código de Barras:";
            // 
            // pnlActions
            // 
            pnlActions.BackColor = Color.White;
            pnlActions.Controls.Add(btnClear);
            pnlActions.Controls.Add(btnPick);
            pnlActions.Dock = DockStyle.Bottom;
            pnlActions.Location = new Point(0, 510);
            pnlActions.Name = "pnlActions";
            pnlActions.Padding = new Padding(25, 15, 25, 15);
            pnlActions.Size = new Size(850, 70);
            pnlActions.TabIndex = 2;
            // 
            // btnClear
            // 
            btnClear.Anchor = AnchorStyles.Bottom | AnchorStyles.Right;
            btnClear.Font = new Font("Segoe UI", 14F, FontStyle.Bold);
            btnClear.Location = new Point(725, 20);
            btnClear.Name = "btnClear";
            btnClear.Size = new Size(100, 40);
            btnClear.TabIndex = 1;
            btnClear.Text = "Limpiar (F2)";
            btnClear.UseVisualStyleBackColor = true;
            // 
            // btnPick
            // 
            btnPick.Anchor = AnchorStyles.Bottom | AnchorStyles.Right;
            btnPick.Font = new Font("Segoe UI", 14F, FontStyle.Bold);
            btnPick.Location = new Point(575, 20);
            btnPick.Name = "btnPick";
            btnPick.Size = new Size(140, 40);
            btnPick.TabIndex = 0;
            btnPick.Text = "Picking (F1)";
            btnPick.UseVisualStyleBackColor = true;
            // 
            // statusStrip
            // 
            statusStrip.Items.AddRange(new ToolStripItem[] { lblStatus });
            statusStrip.Location = new Point(0, 580);
            statusStrip.Name = "statusStrip";
            statusStrip.Size = new Size(850, 22);
            statusStrip.TabIndex = 3;
            // 
            // lblStatus
            // 
            lblStatus.Name = "lblStatus";
            lblStatus.Size = new Size(110, 17);
            lblStatus.Text = "Listo para hacer picking de artículos";
            // 
            // PickingForm
            // 
            AutoScaleDimensions = new SizeF(7F, 15F);
            AutoScaleMode = AutoScaleMode.Font;
            BackColor = Color.FromArgb(248, 249, 250);
            ClientSize = new Size(850, 602);
            Controls.Add(pnlMain);
            Controls.Add(pnlActions);
            Controls.Add(pnlHeader);
            Controls.Add(statusStrip);
            KeyPreview = true;
            MinimumSize = new Size(800, 550);
            Name = "PickingForm";
            StartPosition = FormStartPosition.CenterParent;
            Text = "SGA - Picking";
            pnlHeader.ResumeLayout(false);
            pnlHeader.PerformLayout();
            pnlMain.ResumeLayout(false);
            pnlPickingDetails.ResumeLayout(false);
            pnlPickingDetails.PerformLayout();
            pnlItemInfo.ResumeLayout(false);
            pnlItemInfo.PerformLayout();
            pnlActions.ResumeLayout(false);
            statusStrip.ResumeLayout(false);
            statusStrip.PerformLayout();
            ResumeLayout(false);
            PerformLayout();
        }
    }
}