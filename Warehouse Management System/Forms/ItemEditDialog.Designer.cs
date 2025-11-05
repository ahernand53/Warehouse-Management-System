// Wms.WinForms/Forms/ItemEditDialog.Designer.cs

using Wms.WinForms.Common;

namespace Wms.WinForms.Forms
{
    partial class ItemEditDialog
    {
        private System.ComponentModel.IContainer components = null;
        private Panel pnlHeader;
        private Label lblFormTitle;
        private Panel pnlMain;
        private Panel pnlBasicInfo;
        private Label lblBasicInfoTitle;
        private Label lblSku;
        private TextBox txtSku;
        private Label lblName;
        private TextBox txtName;
        private Label lblDescription;
        private TextBox txtDescription;
        private Label lblUnitOfMeasure;
        private TextBox txtUnitOfMeasure;
        private Label lblPrice;
        private NumericUpDown numPrice;
        private Panel pnlOptions;
        private Label lblOptionsTitle;
        private CheckBox chkRequiresLot;
        private CheckBox chkRequiresSerial;
        private Label lblShelfLife;
        private NumericUpDown numShelfLifeDays;
        private Panel pnlBarcodes;
        private Label lblBarcodesTitle;
        private ListBox lstBarcodes;
        private Label lblNewBarcode;
        private TextBox txtNewBarcode;
        private Button btnAddBarcode;
        private Button btnRemoveBarcode;
        private Panel pnlActions;
        private Button btnSave;
        private Button btnCancel;

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
            lblFormTitle = new Label();
            pnlMain = new Panel();
            pnlBarcodes = new Panel();
            btnRemoveBarcode = new Button();
            btnAddBarcode = new Button();
            txtNewBarcode = new TextBox();
            lblNewBarcode = new Label();
            lstBarcodes = new ListBox();
            lblBarcodesTitle = new Label();
            pnlOptions = new Panel();
            numShelfLifeDays = new NumericUpDown();
            lblShelfLife = new Label();
            chkRequiresSerial = new CheckBox();
            chkRequiresLot = new CheckBox();
            lblOptionsTitle = new Label();
            pnlBasicInfo = new Panel();
            numPrice = new NumericUpDown();
            lblPrice = new Label();
            txtUnitOfMeasure = new TextBox();
            lblUnitOfMeasure = new Label();
            txtDescription = new TextBox();
            lblDescription = new Label();
            txtName = new TextBox();
            lblName = new Label();
            txtSku = new TextBox();
            lblSku = new Label();
            lblBasicInfoTitle = new Label();
            pnlActions = new Panel();
            btnCancel = new Button();
            btnSave = new Button();
            pnlHeader.SuspendLayout();
            pnlMain.SuspendLayout();
            pnlBarcodes.SuspendLayout();
            pnlOptions.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)numShelfLifeDays).BeginInit();
            pnlBasicInfo.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)numPrice).BeginInit();
            pnlActions.SuspendLayout();
            SuspendLayout();
            // 
            // pnlHeader
            // 
            pnlHeader.BackColor = Color.White;
            pnlHeader.Controls.Add(lblFormTitle);
            pnlHeader.Dock = DockStyle.Top;
            pnlHeader.Location = new Point(0, 0);
            pnlHeader.Name = "pnlHeader";
            pnlHeader.Padding = new Padding(25, 15, 25, 15);
            pnlHeader.Size = new Size(730, 70);
            pnlHeader.TabIndex = 0;
            // 
            // lblFormTitle
            // 
            lblFormTitle.AutoSize = true;
            lblFormTitle.Font = new Font("Segoe UI", 20F, FontStyle.Bold);
            lblFormTitle.ForeColor = Color.FromArgb(13, 110, 253);
            lblFormTitle.Location = new Point(25, 20);
            lblFormTitle.Name = "lblFormTitle";
            lblFormTitle.Size = new Size(137, 37);
            lblFormTitle.TabIndex = 0;
            lblFormTitle.Text = "Agregar Artículo";
            // 
            // pnlMain
            // 
            pnlMain.BackColor = Color.FromArgb(248, 249, 250);
            pnlMain.Controls.Add(pnlBarcodes);
            pnlMain.Controls.Add(pnlOptions);
            pnlMain.Controls.Add(pnlBasicInfo);
            pnlMain.Dock = DockStyle.Fill;
            pnlMain.Location = new Point(0, 70);
            pnlMain.Name = "pnlMain";
            pnlMain.Padding = new Padding(25, 20, 25, 20);
            pnlMain.Size = new Size(730, 562);
            pnlMain.TabIndex = 1;
            // 
            // pnlBarcodes
            // 
            pnlBarcodes.BackColor = Color.White;
            pnlBarcodes.Controls.Add(btnRemoveBarcode);
            pnlBarcodes.Controls.Add(btnAddBarcode);
            pnlBarcodes.Controls.Add(txtNewBarcode);
            pnlBarcodes.Controls.Add(lblNewBarcode);
            pnlBarcodes.Controls.Add(lstBarcodes);
            pnlBarcodes.Controls.Add(lblBarcodesTitle);
            pnlBarcodes.Dock = DockStyle.Fill;
            pnlBarcodes.Location = new Point(25, 390);
            pnlBarcodes.Name = "pnlBarcodes";
            pnlBarcodes.Padding = new Padding(20);
            pnlBarcodes.Size = new Size(680, 152);
            pnlBarcodes.TabIndex = 2;
            // 
            // btnRemoveBarcode
            // 
            btnRemoveBarcode.Anchor = AnchorStyles.Top | AnchorStyles.Right;
            btnRemoveBarcode.Location = new Point(570, 115);
            btnRemoveBarcode.Name = "btnRemoveBarcode";
            btnRemoveBarcode.Size = new Size(90, 30);
            btnRemoveBarcode.TabIndex = 5;
            btnRemoveBarcode.Text = "Eliminar";
            btnRemoveBarcode.UseVisualStyleBackColor = true;
            // 
            // btnAddBarcode
            // 
            btnAddBarcode.Anchor = AnchorStyles.Top | AnchorStyles.Right;
            btnAddBarcode.Location = new Point(570, 75);
            btnAddBarcode.Name = "btnAddBarcode";
            btnAddBarcode.Size = new Size(90, 30);
            btnAddBarcode.TabIndex = 4;
            btnAddBarcode.Text = "Agregar";
            btnAddBarcode.UseVisualStyleBackColor = true;
            // 
            // txtNewBarcode
            // 
            txtNewBarcode.Anchor = AnchorStyles.Top | AnchorStyles.Left | AnchorStyles.Right;
            txtNewBarcode.Location = new Point(340, 78);
            txtNewBarcode.Name = "txtNewBarcode";
            txtNewBarcode.PlaceholderText = "Ingrese código...";
            txtNewBarcode.Size = new Size(220, 23);
            txtNewBarcode.TabIndex = 3;
            // 
            // lblNewBarcode
            // 
            lblNewBarcode.AutoSize = true;
            lblNewBarcode.Font = new Font("Segoe UI", 14F, FontStyle.Bold);
            lblNewBarcode.ForeColor = Color.FromArgb(33, 37, 41);
            lblNewBarcode.Location = new Point(340, 50);
            lblNewBarcode.Name = "lblNewBarcode";
            lblNewBarcode.Size = new Size(132, 25);
            lblNewBarcode.TabIndex = 2;
            lblNewBarcode.Text = "Agregar Código:";
            // 
            // lstBarcodes
            // 
            lstBarcodes.Font = new Font("Segoe UI", 11F);
            lstBarcodes.FormattingEnabled = true;
            lstBarcodes.ItemHeight = 20;
            lstBarcodes.Location = new Point(20, 50);
            lstBarcodes.Name = "lstBarcodes";
            lstBarcodes.Size = new Size(300, 84);
            lstBarcodes.TabIndex = 1;
            // 
            // lblBarcodesTitle
            // 
            lblBarcodesTitle.AutoSize = true;
            lblBarcodesTitle.Font = new Font("Segoe UI", 16F, FontStyle.Bold);
            lblBarcodesTitle.ForeColor = Color.FromArgb(33, 37, 41);
            lblBarcodesTitle.Location = new Point(20, 20);
            lblBarcodesTitle.Name = "lblBarcodesTitle";
            lblBarcodesTitle.Size = new Size(108, 30);
            lblBarcodesTitle.TabIndex = 0;
            lblBarcodesTitle.Text = "Códigos de Barras";
            // 
            // pnlOptions
            // 
            pnlOptions.BackColor = Color.White;
            pnlOptions.Controls.Add(numShelfLifeDays);
            pnlOptions.Controls.Add(lblShelfLife);
            pnlOptions.Controls.Add(chkRequiresSerial);
            pnlOptions.Controls.Add(chkRequiresLot);
            pnlOptions.Controls.Add(lblOptionsTitle);
            pnlOptions.Dock = DockStyle.Top;
            pnlOptions.Location = new Point(25, 260);
            pnlOptions.Name = "pnlOptions";
            pnlOptions.Padding = new Padding(20);
            pnlOptions.Size = new Size(680, 130);
            pnlOptions.TabIndex = 1;
            // 
            // numShelfLifeDays
            // 
            numShelfLifeDays.Font = new Font("Segoe UI", 11F);
            numShelfLifeDays.Location = new Point(441, 55);
            numShelfLifeDays.Maximum = new decimal(new int[] { 9999, 0, 0, 0 });
            numShelfLifeDays.Name = "numShelfLifeDays";
            numShelfLifeDays.Size = new Size(120, 27);
            numShelfLifeDays.TabIndex = 4;
            // 
            // lblShelfLife
            // 
            lblShelfLife.AutoSize = true;
            lblShelfLife.Font = new Font("Segoe UI", 14F, FontStyle.Bold);
            lblShelfLife.ForeColor = Color.FromArgb(33, 37, 41);
            lblShelfLife.Location = new Point(441, 25);
            lblShelfLife.Name = "lblShelfLife";
            lblShelfLife.Size = new Size(159, 25);
            lblShelfLife.TabIndex = 3;
            lblShelfLife.Text = "Vida Útil (Días):";
            // 
            // chkRequiresSerial
            // 
            chkRequiresSerial.AutoSize = true;
            chkRequiresSerial.Font = new Font("Segoe UI", 11F);
            chkRequiresSerial.Location = new Point(200, 55);
            chkRequiresSerial.Name = "chkRequiresSerial";
            chkRequiresSerial.Size = new Size(179, 24);
            chkRequiresSerial.TabIndex = 2;
            chkRequiresSerial.Text = "Requiere Control de Serie";
            chkRequiresSerial.UseVisualStyleBackColor = true;
            // 
            // chkRequiresLot
            // 
            chkRequiresLot.AutoSize = true;
            chkRequiresLot.Font = new Font("Segoe UI", 11F);
            chkRequiresLot.Location = new Point(20, 55);
            chkRequiresLot.Name = "chkRequiresLot";
            chkRequiresLot.Size = new Size(163, 24);
            chkRequiresLot.TabIndex = 1;
            chkRequiresLot.Text = "Requiere Control de Lote";
            chkRequiresLot.UseVisualStyleBackColor = true;
            // 
            // lblOptionsTitle
            // 
            lblOptionsTitle.AutoSize = true;
            lblOptionsTitle.Font = new Font("Segoe UI", 16F, FontStyle.Bold);
            lblOptionsTitle.ForeColor = Color.FromArgb(33, 37, 41);
            lblOptionsTitle.Location = new Point(20, 20);
            lblOptionsTitle.Name = "lblOptionsTitle";
            lblOptionsTitle.Size = new Size(176, 30);
            lblOptionsTitle.TabIndex = 0;
            lblOptionsTitle.Text = "Propiedades del Artículo";
            // 
            // pnlBasicInfo
            // 
            pnlBasicInfo.BackColor = Color.White;
            pnlBasicInfo.Controls.Add(numPrice);
            pnlBasicInfo.Controls.Add(lblPrice);
            pnlBasicInfo.Controls.Add(txtUnitOfMeasure);
            pnlBasicInfo.Controls.Add(lblUnitOfMeasure);
            pnlBasicInfo.Controls.Add(txtDescription);
            pnlBasicInfo.Controls.Add(lblDescription);
            pnlBasicInfo.Controls.Add(txtName);
            pnlBasicInfo.Controls.Add(lblName);
            pnlBasicInfo.Controls.Add(txtSku);
            pnlBasicInfo.Controls.Add(lblSku);
            pnlBasicInfo.Controls.Add(lblBasicInfoTitle);
            pnlBasicInfo.Dock = DockStyle.Top;
            pnlBasicInfo.Location = new Point(25, 20);
            pnlBasicInfo.Name = "pnlBasicInfo";
            pnlBasicInfo.Padding = new Padding(20);
            pnlBasicInfo.Size = new Size(680, 240);
            pnlBasicInfo.TabIndex = 0;
            // 
            // numPrice
            // 
            numPrice.DecimalPlaces = 2;
            numPrice.Font = new Font("Segoe UI", 11F);
            numPrice.Location = new Point(490, 190);
            numPrice.Maximum = new decimal(new int[] { 999999999, 0, 0, 0 });
            numPrice.Name = "numPrice";
            numPrice.Size = new Size(170, 27);
            numPrice.TabIndex = 10;
            // 
            // lblPrice
            // 
            lblPrice.AutoSize = true;
            lblPrice.Font = new Font("Segoe UI", 14F, FontStyle.Bold);
            lblPrice.ForeColor = Color.FromArgb(33, 37, 41);
            lblPrice.Location = new Point(490, 165);
            lblPrice.Name = "lblPrice";
            lblPrice.Size = new Size(67, 25);
            lblPrice.TabIndex = 9;
            lblPrice.Text = "Precio:";
            // 
            // txtUnitOfMeasure
            // 
            txtUnitOfMeasure.Location = new Point(240, 190);
            txtUnitOfMeasure.Name = "txtUnitOfMeasure";
            txtUnitOfMeasure.Size = new Size(230, 23);
            txtUnitOfMeasure.TabIndex = 8;
            // 
            // lblUnitOfMeasure
            // 
            lblUnitOfMeasure.AutoSize = true;
            lblUnitOfMeasure.Font = new Font("Segoe UI", 14F, FontStyle.Bold);
            lblUnitOfMeasure.ForeColor = Color.FromArgb(33, 37, 41);
            lblUnitOfMeasure.Location = new Point(240, 165);
            lblUnitOfMeasure.Name = "lblUnitOfMeasure";
            lblUnitOfMeasure.Size = new Size(98, 25);
            lblUnitOfMeasure.TabIndex = 7;
            lblUnitOfMeasure.Text = "Unidad:";
            // 
            // txtDescription
            // 
            txtDescription.Location = new Point(20, 150);
            txtDescription.Name = "txtDescription";
            txtDescription.Size = new Size(450, 23);
            txtDescription.TabIndex = 6;
            // 
            // lblDescription
            // 
            lblDescription.AutoSize = true;
            lblDescription.Font = new Font("Segoe UI", 14F, FontStyle.Bold);
            lblDescription.ForeColor = Color.FromArgb(33, 37, 41);
            lblDescription.Location = new Point(20, 125);
            lblDescription.Name = "lblDescription";
            lblDescription.Size = new Size(119, 25);
            lblDescription.TabIndex = 5;
            lblDescription.Text = "Descripción:";
            // 
            // txtName
            // 
            txtName.Anchor = AnchorStyles.Top | AnchorStyles.Left | AnchorStyles.Right;
            txtName.Location = new Point(240, 80);
            txtName.Name = "txtName";
            txtName.Size = new Size(420, 23);
            txtName.TabIndex = 4;
            // 
            // lblName
            // 
            lblName.AutoSize = true;
            lblName.Font = new Font("Segoe UI", 14F, FontStyle.Bold);
            lblName.ForeColor = Color.FromArgb(33, 37, 41);
            lblName.Location = new Point(240, 55);
            lblName.Name = "lblName";
            lblName.Size = new Size(69, 25);
            lblName.TabIndex = 3;
            lblName.Text = "Nombre:";
            // 
            // txtSku
            // 
            txtSku.Location = new Point(20, 80);
            txtSku.Name = "txtSku";
            txtSku.Size = new Size(200, 23);
            txtSku.TabIndex = 2;
            // 
            // lblSku
            // 
            lblSku.AutoSize = true;
            lblSku.Font = new Font("Segoe UI", 14F, FontStyle.Bold);
            lblSku.ForeColor = Color.FromArgb(33, 37, 41);
            lblSku.Location = new Point(20, 55);
            lblSku.Name = "lblSku";
            lblSku.Size = new Size(54, 25);
            lblSku.TabIndex = 1;
            lblSku.Text = "SKU:";
            // 
            // lblBasicInfoTitle
            // 
            lblBasicInfoTitle.AutoSize = true;
            lblBasicInfoTitle.Font = new Font("Segoe UI", 16F, FontStyle.Bold);
            lblBasicInfoTitle.ForeColor = Color.FromArgb(33, 37, 41);
            lblBasicInfoTitle.Location = new Point(20, 20);
            lblBasicInfoTitle.Name = "lblBasicInfoTitle";
            lblBasicInfoTitle.Size = new Size(195, 30);
            lblBasicInfoTitle.TabIndex = 0;
            lblBasicInfoTitle.Text = "Información Básica";
            // 
            // pnlActions
            // 
            pnlActions.BackColor = Color.White;
            pnlActions.Controls.Add(btnCancel);
            pnlActions.Controls.Add(btnSave);
            pnlActions.Dock = DockStyle.Bottom;
            pnlActions.Location = new Point(0, 592);
            pnlActions.Name = "pnlActions";
            pnlActions.Padding = new Padding(25, 15, 25, 15);
            pnlActions.Size = new Size(730, 70);
            pnlActions.TabIndex = 3;
            // 
            // btnCancel
            // 
            btnCancel.Anchor = AnchorStyles.Bottom | AnchorStyles.Right;
            btnCancel.Font = new Font("Segoe UI", 14F, FontStyle.Bold);
            btnCancel.Location = new Point(605, 20);
            btnCancel.Name = "btnCancel";
            btnCancel.Size = new Size(100, 40);
            btnCancel.TabIndex = 1;
            btnCancel.Text = "Cancelar";
            btnCancel.UseVisualStyleBackColor = true;
            // 
            // btnSave
            // 
            btnSave.Anchor = AnchorStyles.Bottom | AnchorStyles.Right;
            btnSave.Font = new Font("Segoe UI", 14F, FontStyle.Bold);
            btnSave.Location = new Point(495, 20);
            btnSave.Name = "btnSave";
            btnSave.Size = new Size(100, 40);
            btnSave.TabIndex = 0;
            btnSave.Text = "Guardar (F1)";
            btnSave.UseVisualStyleBackColor = true;
            // 
            // ItemEditDialog
            // 
            AutoScaleDimensions = new SizeF(7F, 15F);
            AutoScaleMode = AutoScaleMode.Font;
            BackColor = Color.FromArgb(248, 249, 250);
            ClientSize = new Size(730, 662);
            Controls.Add(pnlMain);
            Controls.Add(pnlActions);
            Controls.Add(pnlHeader);
            FormBorderStyle = FormBorderStyle.FixedDialog;
            KeyPreview = true;
            MaximizeBox = false;
            MinimizeBox = false;
            MinimumSize = new Size(730, 640);
            Name = "ItemEditDialog";
            StartPosition = FormStartPosition.CenterParent;
            Text = "Detalles del Artículo";
            pnlHeader.ResumeLayout(false);
            pnlHeader.PerformLayout();
            pnlMain.ResumeLayout(false);
            pnlBarcodes.ResumeLayout(false);
            pnlBarcodes.PerformLayout();
            pnlOptions.ResumeLayout(false);
            pnlOptions.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)numShelfLifeDays).EndInit();
            pnlBasicInfo.ResumeLayout(false);
            pnlBasicInfo.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)numPrice).EndInit();
            pnlActions.ResumeLayout(false);
            ResumeLayout(false);
        }
    }
}