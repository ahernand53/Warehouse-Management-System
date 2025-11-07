// Wms.WinForms/Forms/PutawayForm.Designer.cs

using Wms.WinForms.Common;
using Wms.WinForms.Controls;

namespace Wms.WinForms.Forms
{
    partial class PutawayForm
    {
        private System.ComponentModel.IContainer components = null;
        private Panel pnlHeader;
        private Label lblTitle;
        private Panel pnlMain;
        private Panel pnlItemInfo;
        private Label lblBarcodeTitle;
        private AutoCompleteTextBox txtBarcode;
        private Label lblItemInfo;
        private Panel pnlPutawayDetails;
        private Label lblFromLocationTitle;
        private AutoCompleteTextBox txtFromLocation;
        private Label lblToLocationTitle;
        private AutoCompleteTextBox txtToLocation;
        private Label lblQuantityTitle;
        private TextBox txtQuantity;
        private Label lblNotesTitle;
        private TextBox txtNotes;
        private Panel pnlActions;
        private Button btnPutaway;
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
            pnlPutawayDetails = new Panel();
            txtNotes = new TextBox();
            lblNotesTitle = new Label();
            txtQuantity = new TextBox();
            lblQuantityTitle = new Label();
            txtToLocation = new AutoCompleteTextBox();
            lblToLocationTitle = new Label();
            txtFromLocation = new AutoCompleteTextBox();
            lblFromLocationTitle = new Label();
            pnlItemInfo = new Panel();
            lblItemInfo = new Label();
            txtBarcode = new AutoCompleteTextBox();
            lblBarcodeTitle = new Label();
            pnlActions = new Panel();
            btnClear = new Button();
            btnPutaway = new Button();
            statusStrip = new StatusStrip();
            lblStatus = new ToolStripStatusLabel();
            pnlHeader.SuspendLayout();
            pnlMain.SuspendLayout();
            pnlPutawayDetails.SuspendLayout();
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
            lblTitle.ForeColor = Color.FromArgb(13, 202, 240);
            lblTitle.Location = new Point(25, 20);
            lblTitle.Name = "lblTitle";
            lblTitle.Size = new Size(185, 45);
            lblTitle.TabIndex = 0;
            lblTitle.Text = "📤 Almacenamiento";
            // 
            // pnlMain
            // 
            pnlMain.BackColor = Color.FromArgb(248, 249, 250);
            pnlMain.Controls.Add(pnlPutawayDetails);
            pnlMain.Controls.Add(pnlItemInfo);
            pnlMain.Dock = DockStyle.Fill;
            pnlMain.Location = new Point(0, 70);
            pnlMain.Name = "pnlMain";
            pnlMain.Padding = new Padding(25, 20, 25, 20);
            pnlMain.Size = new Size(850, 440);
            pnlMain.TabIndex = 1;
            // 
            // pnlPutawayDetails
            // 
            pnlPutawayDetails.BackColor = Color.White;
            pnlPutawayDetails.Controls.Add(txtNotes);
            pnlPutawayDetails.Controls.Add(lblNotesTitle);
            pnlPutawayDetails.Controls.Add(txtQuantity);
            pnlPutawayDetails.Controls.Add(lblQuantityTitle);
            pnlPutawayDetails.Controls.Add(txtToLocation);
            pnlPutawayDetails.Controls.Add(lblToLocationTitle);
            pnlPutawayDetails.Controls.Add(txtFromLocation);
            pnlPutawayDetails.Controls.Add(lblFromLocationTitle);
            pnlPutawayDetails.Dock = DockStyle.Fill;
            pnlPutawayDetails.Location = new Point(25, 160);
            pnlPutawayDetails.Name = "pnlPutawayDetails";
            pnlPutawayDetails.Padding = new Padding(20);
            pnlPutawayDetails.Size = new Size(800, 260);
            pnlPutawayDetails.TabIndex = 1;
            // 
            // txtNotes
            // 
            txtNotes.Anchor = AnchorStyles.Top | AnchorStyles.Left | AnchorStyles.Right;
            txtNotes.Location = new Point(20, 115);
            txtNotes.Multiline = true;
            txtNotes.Name = "txtNotes";
            txtNotes.PlaceholderText = "Notas opcionales para almacenamiento...";
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
            // txtQuantity
            // 
            txtQuantity.Location = new Point(480, 45);
            txtQuantity.Name = "txtQuantity";
            txtQuantity.Size = new Size(150, 23);
            txtQuantity.TabIndex = 5;
            // 
            // lblQuantityTitle
            // 
            lblQuantityTitle.AutoSize = true;
            lblQuantityTitle.Font = new Font("Segoe UI", 14F, FontStyle.Bold);
            lblQuantityTitle.ForeColor = Color.FromArgb(33, 37, 41);
            lblQuantityTitle.Location = new Point(480, 20);
            lblQuantityTitle.Name = "lblQuantityTitle";
            lblQuantityTitle.Size = new Size(94, 25);
            lblQuantityTitle.TabIndex = 4;
            lblQuantityTitle.Text = "Cantidad:";
            // 
            // txtToLocation
            // 
            txtToLocation.Location = new Point(250, 45);
            txtToLocation.Name = "txtToLocation";
            txtToLocation.Size = new Size(200, 23);
            txtToLocation.TabIndex = 3;
            // 
            // lblToLocationTitle
            // 
            lblToLocationTitle.AutoSize = true;
            lblToLocationTitle.Font = new Font("Segoe UI", 14F, FontStyle.Bold);
            lblToLocationTitle.ForeColor = Color.FromArgb(33, 37, 41);
            lblToLocationTitle.Location = new Point(250, 20);
            lblToLocationTitle.Name = "lblToLocationTitle";
            lblToLocationTitle.Size = new Size(120, 25);
            lblToLocationTitle.TabIndex = 2;
            lblToLocationTitle.Text = "Ubicación Destino:";
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
            lblFromLocationTitle.Text = "Ubicación Origen:";
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
            pnlActions.Controls.Add(btnPutaway);
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
            // btnPutaway
            // 
            btnPutaway.Anchor = AnchorStyles.Bottom | AnchorStyles.Right;
            btnPutaway.Font = new Font("Segoe UI", 14F, FontStyle.Bold);
            btnPutaway.Location = new Point(565, 20);
            btnPutaway.Name = "btnPutaway";
            btnPutaway.Size = new Size(150, 40);
            btnPutaway.TabIndex = 0;
            btnPutaway.Text = "Almacenar (F1)";
            btnPutaway.UseVisualStyleBackColor = true;
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
            // PutawayForm
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
            Name = "PutawayForm";
            StartPosition = FormStartPosition.CenterParent;
            Text = "SGA - Almacenamiento";
            pnlHeader.ResumeLayout(false);
            pnlHeader.PerformLayout();
            pnlMain.ResumeLayout(false);
            pnlPutawayDetails.ResumeLayout(false);
            pnlPutawayDetails.PerformLayout();
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