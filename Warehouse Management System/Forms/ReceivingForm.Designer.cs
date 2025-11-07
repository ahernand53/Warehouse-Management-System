// Wms.WinForms/Forms/ReceivingForm.Designer.cs

using Wms.WinForms.Common;
using Wms.WinForms.Controls;

namespace Wms.WinForms.Forms
{
    partial class ReceivingForm
    {
        private System.ComponentModel.IContainer components = null;
        private Panel pnlHeader;
        private Label lblTitle;
        private Panel pnlMain;
        private Panel pnlItemInfo;
        private Label lblBarcodeTitle;
        private AutoCompleteTextBox txtBarcode;
        private Label lblItemInfo;
        private Panel pnlReceivingDetails;
        private Label lblLocationTitle;
        private AutoCompleteTextBox txtLocationCode;
        private Label lblQuantityTitle;
        private TextBox txtQuantity;
        private Label lblLotNumber;
        private AutoCompleteTextBox txtLotNumber;
        private Label lblExpiryDate;
        private DateTimePicker dtpExpiryDate;
        private Label lblReferenceTitle;
        private TextBox txtReferenceNumber;
        private Label lblNotesTitle;
        private TextBox txtNotes;
        private Panel pnlActions;
        private Button btnReceive;
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
            pnlReceivingDetails = new Panel();
            txtNotes = new TextBox();
            lblNotesTitle = new Label();
            txtReferenceNumber = new TextBox();
            lblReferenceTitle = new Label();
            dtpExpiryDate = new DateTimePicker();
            lblExpiryDate = new Label();
            txtLotNumber = new AutoCompleteTextBox();
            lblLotNumber = new Label();
            txtQuantity = new TextBox();
            lblQuantityTitle = new Label();
            txtLocationCode = new AutoCompleteTextBox();
            lblLocationTitle = new Label();
            pnlItemInfo = new Panel();
            lblItemInfo = new Label();
            txtBarcode = new AutoCompleteTextBox();
            lblBarcodeTitle = new Label();
            pnlActions = new Panel();
            btnClear = new Button();
            btnReceive = new Button();
            statusStrip = new StatusStrip();
            lblStatus = new ToolStripStatusLabel();
            pnlHeader.SuspendLayout();
            pnlMain.SuspendLayout();
            pnlReceivingDetails.SuspendLayout();
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
            lblTitle.ForeColor = Color.FromArgb(25, 135, 84);
            lblTitle.Location = new Point(25, 20);
            lblTitle.Name = "lblTitle";
            lblTitle.Size = new Size(200, 45);
            lblTitle.TabIndex = 0;
            lblTitle.Text = "📦 Recepción";
            // 
            // pnlMain
            // 
            pnlMain.BackColor = Color.FromArgb(248, 249, 250);
            pnlMain.Controls.Add(pnlReceivingDetails);
            pnlMain.Controls.Add(pnlItemInfo);
            pnlMain.Dock = DockStyle.Fill;
            pnlMain.Location = new Point(0, 70);
            pnlMain.Name = "pnlMain";
            pnlMain.Padding = new Padding(25, 20, 25, 20);
            pnlMain.Size = new Size(850, 440);
            pnlMain.TabIndex = 1;
            // 
            // pnlReceivingDetails
            // 
            pnlReceivingDetails.BackColor = Color.White;
            pnlReceivingDetails.Controls.Add(txtNotes);
            pnlReceivingDetails.Controls.Add(lblNotesTitle);
            pnlReceivingDetails.Controls.Add(txtReferenceNumber);
            pnlReceivingDetails.Controls.Add(lblReferenceTitle);
            pnlReceivingDetails.Controls.Add(dtpExpiryDate);
            pnlReceivingDetails.Controls.Add(lblExpiryDate);
            pnlReceivingDetails.Controls.Add(txtLotNumber);
            pnlReceivingDetails.Controls.Add(lblLotNumber);
            pnlReceivingDetails.Controls.Add(txtQuantity);
            pnlReceivingDetails.Controls.Add(lblQuantityTitle);
            pnlReceivingDetails.Controls.Add(txtLocationCode);
            pnlReceivingDetails.Controls.Add(lblLocationTitle);
            pnlReceivingDetails.Dock = DockStyle.Fill;
            pnlReceivingDetails.Location = new Point(25, 160);
            pnlReceivingDetails.Name = "pnlReceivingDetails";
            pnlReceivingDetails.Padding = new Padding(20);
            pnlReceivingDetails.Size = new Size(800, 260);
            pnlReceivingDetails.TabIndex = 1;
            // 
            // txtNotes
            // 
            txtNotes.Anchor = AnchorStyles.Top | AnchorStyles.Left | AnchorStyles.Right;
            txtNotes.Location = new Point(413, 48);
            txtNotes.Multiline = true;
            txtNotes.Name = "txtNotes";
            txtNotes.PlaceholderText = "Notas opcionales...";
            txtNotes.Size = new Size(364, 160);
            txtNotes.TabIndex = 11;
            // 
            // lblNotesTitle
            // 
            lblNotesTitle.AutoSize = true;
            lblNotesTitle.Font = new Font("Segoe UI", 14F, FontStyle.Bold);
            lblNotesTitle.ForeColor = Color.FromArgb(33, 37, 41);
            lblNotesTitle.Location = new Point(413, 20);
            lblNotesTitle.Name = "lblNotesTitle";
            lblNotesTitle.Size = new Size(69, 25);
            lblNotesTitle.TabIndex = 10;
            lblNotesTitle.Text = "Notas:";
            // 
            // txtReferenceNumber
            // 
            txtReferenceNumber.Location = new Point(20, 185);
            txtReferenceNumber.Name = "txtReferenceNumber";
            txtReferenceNumber.PlaceholderText = "OC, Recibo, etc.";
            txtReferenceNumber.Size = new Size(200, 23);
            txtReferenceNumber.TabIndex = 9;
            // 
            // lblReferenceTitle
            // 
            lblReferenceTitle.AutoSize = true;
            lblReferenceTitle.Font = new Font("Segoe UI", 14F, FontStyle.Bold);
            lblReferenceTitle.ForeColor = Color.FromArgb(33, 37, 41);
            lblReferenceTitle.Location = new Point(20, 160);
            lblReferenceTitle.Name = "lblReferenceTitle";
            lblReferenceTitle.Size = new Size(185, 25);
            lblReferenceTitle.TabIndex = 8;
            lblReferenceTitle.Text = "Número de Referencia:";
            // 
            // dtpExpiryDate
            // 
            dtpExpiryDate.Format = DateTimePickerFormat.Short;
            dtpExpiryDate.Location = new Point(240, 115);
            dtpExpiryDate.Name = "dtpExpiryDate";
            dtpExpiryDate.Size = new Size(150, 23);
            dtpExpiryDate.TabIndex = 7;
            dtpExpiryDate.Visible = false;
            // 
            // lblExpiryDate
            // 
            lblExpiryDate.AutoSize = true;
            lblExpiryDate.Font = new Font("Segoe UI", 14F, FontStyle.Bold);
            lblExpiryDate.ForeColor = Color.FromArgb(33, 37, 41);
            lblExpiryDate.Location = new Point(240, 90);
            lblExpiryDate.Name = "lblExpiryDate";
            lblExpiryDate.Size = new Size(120, 25);
            lblExpiryDate.TabIndex = 6;
            lblExpiryDate.Text = "Fecha de Vencimiento:";
            lblExpiryDate.Visible = false;
            // 
            // txtLotNumber
            // 
            txtLotNumber = new AutoCompleteTextBox();
            txtLotNumber.Location = new Point(20, 115);
            txtLotNumber.Name = "txtLotNumber";
            txtLotNumber.Size = new Size(200, 23);
            txtLotNumber.TabIndex = 5;
            txtLotNumber.Visible = false;
            // 
            // lblLotNumber
            // 
            lblLotNumber.AutoSize = true;
            lblLotNumber.Font = new Font("Segoe UI", 14F, FontStyle.Bold);
            lblLotNumber.ForeColor = Color.FromArgb(33, 37, 41);
            lblLotNumber.Location = new Point(20, 90);
            lblLotNumber.Name = "lblLotNumber";
            lblLotNumber.Size = new Size(126, 25);
            lblLotNumber.TabIndex = 4;
            lblLotNumber.Text = "Número de Lote:";
            lblLotNumber.Visible = false;
            // 
            // txtQuantity
            // 
            txtQuantity.Location = new Point(240, 45);
            txtQuantity.Name = "txtQuantity";
            txtQuantity.Size = new Size(150, 23);
            txtQuantity.TabIndex = 3;
            // 
            // lblQuantityTitle
            // 
            lblQuantityTitle.AutoSize = true;
            lblQuantityTitle.Font = new Font("Segoe UI", 14F, FontStyle.Bold);
            lblQuantityTitle.ForeColor = Color.FromArgb(33, 37, 41);
            lblQuantityTitle.Location = new Point(240, 20);
            lblQuantityTitle.Name = "lblQuantityTitle";
            lblQuantityTitle.Size = new Size(94, 25);
            lblQuantityTitle.TabIndex = 2;
            lblQuantityTitle.Text = "Cantidad:";
            // 
            // txtLocationCode
            // 
            txtLocationCode = new AutoCompleteTextBox();
            txtLocationCode.Location = new Point(20, 45);
            txtLocationCode.Name = "txtLocationCode";
            txtLocationCode.Size = new Size(200, 23);
            txtLocationCode.TabIndex = 1;
            // 
            // lblLocationTitle
            // 
            lblLocationTitle.AutoSize = true;
            lblLocationTitle.Font = new Font("Segoe UI", 14F, FontStyle.Bold);
            lblLocationTitle.ForeColor = Color.FromArgb(33, 37, 41);
            lblLocationTitle.Location = new Point(20, 20);
            lblLocationTitle.Name = "lblLocationTitle";
            lblLocationTitle.Size = new Size(145, 25);
            lblLocationTitle.TabIndex = 0;
            lblLocationTitle.Text = "Código de Ubicación:";
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
            lblItemInfo.Size = new Size(757, 30);
            lblItemInfo.TabIndex = 2;
            lblItemInfo.Text = "No hay artículo seleccionado";
            lblItemInfo.TextAlign = ContentAlignment.MiddleLeft;
            // 
            // txtBarcode
            // 
            txtBarcode = new AutoCompleteTextBox();
            txtBarcode.Anchor = AnchorStyles.Top | AnchorStyles.Left | AnchorStyles.Right;
            txtBarcode.Font = new Font("Segoe UI", 12F);
            txtBarcode.Location = new Point(20, 50);
            txtBarcode.Name = "txtBarcode";
            txtBarcode.PlaceholderText = "Escanee el código de barras aquí...";
            txtBarcode.Size = new Size(757, 29);
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
            pnlActions.Controls.Add(btnReceive);
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
            // btnReceive
            // 
            btnReceive.Anchor = AnchorStyles.Bottom | AnchorStyles.Right;
            btnReceive.Font = new Font("Segoe UI", 14F, FontStyle.Bold);
            btnReceive.Location = new Point(565, 20);
            btnReceive.Name = "btnReceive";
            btnReceive.Size = new Size(150, 40);
            btnReceive.TabIndex = 0;
            btnReceive.Text = "Recibir (F1)";
            btnReceive.UseVisualStyleBackColor = true;
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
            lblStatus.Size = new Size(112, 17);
            lblStatus.Text = "Listo para escanear artículos";
            // 
            // ReceivingForm
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
            Name = "ReceivingForm";
            StartPosition = FormStartPosition.CenterParent;
            Text = "SGA - Recepción";
            pnlHeader.ResumeLayout(false);
            pnlHeader.PerformLayout();
            pnlMain.ResumeLayout(false);
            pnlReceivingDetails.ResumeLayout(false);
            pnlReceivingDetails.PerformLayout();
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