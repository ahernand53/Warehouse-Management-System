// Wms.WinForms/Forms/LocationEditDialog.Designer.cs

using Wms.WinForms.Common;

namespace Wms.WinForms.Forms
{
    partial class LocationEditDialog
    {
        private System.ComponentModel.IContainer components = null;
        private Panel pnlHeader;
        private Label lblFormTitle;
        private Panel pnlMain;
        private Panel pnlBasicInfo;
        private Label lblBasicInfoTitle;
        private Label lblCode;
        private TextBox txtCode;
        private Label lblName;
        private TextBox txtName;
        private Label lblParentLocation;
        private ComboBox cmbParentLocation;
        private Panel pnlProperties;
        private Label lblPropertiesTitle;
        private CheckBox chkIsPickable;
        private CheckBox chkIsReceivable;
        private Label lblCapacity;
        private NumericUpDown numCapacity;
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
            pnlProperties = new Panel();
            numCapacity = new NumericUpDown();
            lblCapacity = new Label();
            chkIsReceivable = new CheckBox();
            chkIsPickable = new CheckBox();
            lblPropertiesTitle = new Label();
            pnlBasicInfo = new Panel();
            cmbParentLocation = new ComboBox();
            lblParentLocation = new Label();
            txtName = new TextBox();
            lblName = new Label();
            txtCode = new TextBox();
            lblCode = new Label();
            lblBasicInfoTitle = new Label();
            pnlActions = new Panel();
            btnCancel = new Button();
            btnSave = new Button();
            pnlHeader.SuspendLayout();
            pnlMain.SuspendLayout();
            pnlProperties.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)numCapacity).BeginInit();
            pnlBasicInfo.SuspendLayout();
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
            pnlHeader.Size = new Size(650, 70);
            pnlHeader.TabIndex = 0;
            // 
            // lblFormTitle
            // 
            lblFormTitle.AutoSize = true;
            lblFormTitle.Font = new Font("Segoe UI", 20F, FontStyle.Bold);
            lblFormTitle.ForeColor = Color.FromArgb(13, 110, 253);
            lblFormTitle.Location = new Point(25, 20);
            lblFormTitle.Name = "lblFormTitle";
            lblFormTitle.Size = new Size(188, 37);
            lblFormTitle.TabIndex = 0;
            lblFormTitle.Text = "Agregar Ubicación";
            // 
            // pnlMain
            // 
            pnlMain.BackColor = Color.FromArgb(248, 249, 250);
            pnlMain.Controls.Add(pnlProperties);
            pnlMain.Controls.Add(pnlBasicInfo);
            pnlMain.Dock = DockStyle.Fill;
            pnlMain.Location = new Point(0, 70);
            pnlMain.Name = "pnlMain";
            pnlMain.Padding = new Padding(25, 20, 25, 20);
            pnlMain.Size = new Size(650, 344);
            pnlMain.TabIndex = 1;
            // 
            // pnlProperties
            // 
            pnlProperties.BackColor = Color.White;
            pnlProperties.Controls.Add(numCapacity);
            pnlProperties.Controls.Add(lblCapacity);
            pnlProperties.Controls.Add(chkIsReceivable);
            pnlProperties.Controls.Add(chkIsPickable);
            pnlProperties.Controls.Add(lblPropertiesTitle);
            pnlProperties.Dock = DockStyle.Fill;
            pnlProperties.Location = new Point(25, 220);
            pnlProperties.Name = "pnlProperties";
            pnlProperties.Padding = new Padding(20);
            pnlProperties.Size = new Size(600, 104);
            pnlProperties.TabIndex = 1;
            // 
            // numCapacity
            // 
            numCapacity.Font = new Font("Segoe UI", 11F);
            numCapacity.Location = new Point(418, 55);
            numCapacity.Maximum = new decimal(new int[] { 999999, 0, 0, 0 });
            numCapacity.Name = "numCapacity";
            numCapacity.Size = new Size(150, 27);
            numCapacity.TabIndex = 4;
            numCapacity.Value = new decimal(new int[] { 1000, 0, 0, 0 });
            // 
            // lblCapacity
            // 
            lblCapacity.AutoSize = true;
            lblCapacity.Font = new Font("Segoe UI", 14F, FontStyle.Bold);
            lblCapacity.ForeColor = Color.FromArgb(33, 37, 41);
            lblCapacity.Location = new Point(441, 25);
            lblCapacity.Name = "lblCapacity";
            lblCapacity.Size = new Size(92, 25);
            lblCapacity.TabIndex = 3;
            lblCapacity.Text = "Capacidad:";
            // 
            // chkIsReceivable
            // 
            chkIsReceivable.AutoSize = true;
            chkIsReceivable.Font = new Font("Segoe UI", 11F);
            chkIsReceivable.Location = new Point(200, 55);
            chkIsReceivable.Name = "chkIsReceivable";
            chkIsReceivable.Size = new Size(161, 24);
            chkIsReceivable.TabIndex = 2;
            chkIsReceivable.Text = "Ubicación de Recepción";
            chkIsReceivable.UseVisualStyleBackColor = true;
            // 
            // chkIsPickable
            // 
            chkIsPickable.AutoSize = true;
            chkIsPickable.Font = new Font("Segoe UI", 11F);
            chkIsPickable.Location = new Point(20, 55);
            chkIsPickable.Name = "chkIsPickable";
            chkIsPickable.Size = new Size(144, 24);
            chkIsPickable.TabIndex = 1;
            chkIsPickable.Text = "Ubicación de Picking";
            chkIsPickable.UseVisualStyleBackColor = true;
            // 
            // lblPropertiesTitle
            // 
            lblPropertiesTitle.AutoSize = true;
            lblPropertiesTitle.Font = new Font("Segoe UI", 16F, FontStyle.Bold);
            lblPropertiesTitle.ForeColor = Color.FromArgb(33, 37, 41);
            lblPropertiesTitle.Location = new Point(20, 20);
            lblPropertiesTitle.Name = "lblPropertiesTitle";
            lblPropertiesTitle.Size = new Size(216, 30);
            lblPropertiesTitle.TabIndex = 0;
            lblPropertiesTitle.Text = "Propiedades de la Ubicación";
            // 
            // pnlBasicInfo
            // 
            pnlBasicInfo.BackColor = Color.White;
            pnlBasicInfo.Controls.Add(cmbParentLocation);
            pnlBasicInfo.Controls.Add(lblParentLocation);
            pnlBasicInfo.Controls.Add(txtName);
            pnlBasicInfo.Controls.Add(lblName);
            pnlBasicInfo.Controls.Add(txtCode);
            pnlBasicInfo.Controls.Add(lblCode);
            pnlBasicInfo.Controls.Add(lblBasicInfoTitle);
            pnlBasicInfo.Dock = DockStyle.Top;
            pnlBasicInfo.Location = new Point(25, 20);
            pnlBasicInfo.Name = "pnlBasicInfo";
            pnlBasicInfo.Padding = new Padding(20);
            pnlBasicInfo.Size = new Size(600, 200);
            pnlBasicInfo.TabIndex = 0;
            // 
            // cmbParentLocation
            // 
            cmbParentLocation.Anchor = AnchorStyles.Top | AnchorStyles.Left | AnchorStyles.Right;
            cmbParentLocation.DropDownStyle = ComboBoxStyle.DropDownList;
            cmbParentLocation.FormattingEnabled = true;
            cmbParentLocation.Location = new Point(20, 150);
            cmbParentLocation.Name = "cmbParentLocation";
            cmbParentLocation.Size = new Size(569, 23);
            cmbParentLocation.TabIndex = 6;
            // 
            // lblParentLocation
            // 
            lblParentLocation.AutoSize = true;
            lblParentLocation.Font = new Font("Segoe UI", 14F, FontStyle.Bold);
            lblParentLocation.ForeColor = Color.FromArgb(33, 37, 41);
            lblParentLocation.Location = new Point(20, 125);
            lblParentLocation.Name = "lblParentLocation";
            lblParentLocation.Size = new Size(158, 25);
            lblParentLocation.TabIndex = 5;
            lblParentLocation.Text = "Ubicación Padre:";
            // 
            // txtName
            // 
            txtName.Anchor = AnchorStyles.Top | AnchorStyles.Left | AnchorStyles.Right;
            txtName.Location = new Point(190, 80);
            txtName.Name = "txtName";
            txtName.Size = new Size(399, 23);
            txtName.TabIndex = 4;
            // 
            // lblName
            // 
            lblName.AutoSize = true;
            lblName.Font = new Font("Segoe UI", 14F, FontStyle.Bold);
            lblName.ForeColor = Color.FromArgb(33, 37, 41);
            lblName.Location = new Point(190, 55);
            lblName.Name = "lblName";
            lblName.Size = new Size(69, 25);
            lblName.TabIndex = 3;
            lblName.Text = "Nombre:";
            // 
            // txtCode
            // 
            txtCode.Location = new Point(20, 80);
            txtCode.Name = "txtCode";
            txtCode.Size = new Size(150, 23);
            txtCode.TabIndex = 2;
            // 
            // lblCode
            // 
            lblCode.AutoSize = true;
            lblCode.Font = new Font("Segoe UI", 14F, FontStyle.Bold);
            lblCode.ForeColor = Color.FromArgb(33, 37, 41);
            lblCode.Location = new Point(20, 55);
            lblCode.Name = "lblCode";
            lblCode.Size = new Size(63, 25);
            lblCode.TabIndex = 1;
            lblCode.Text = "Código:";
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
            pnlActions.Location = new Point(0, 414);
            pnlActions.Name = "pnlActions";
            pnlActions.Padding = new Padding(25, 15, 25, 15);
            pnlActions.Size = new Size(650, 70);
            pnlActions.TabIndex = 2;
            // 
            // btnCancel
            // 
            btnCancel.Anchor = AnchorStyles.Bottom | AnchorStyles.Right;
            btnCancel.Font = new Font("Segoe UI", 14F, FontStyle.Bold);
            btnCancel.Location = new Point(525, 20);
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
            btnSave.Location = new Point(415, 20);
            btnSave.Name = "btnSave";
            btnSave.Size = new Size(100, 40);
            btnSave.TabIndex = 0;
            btnSave.Text = "Guardar (F1)";
            btnSave.UseVisualStyleBackColor = true;
            // 
            // LocationEditDialog
            // 
            AutoScaleDimensions = new SizeF(7F, 15F);
            AutoScaleMode = AutoScaleMode.Font;
            BackColor = Color.FromArgb(248, 249, 250);
            ClientSize = new Size(650, 484);
            Controls.Add(pnlMain);
            Controls.Add(pnlActions);
            Controls.Add(pnlHeader);
            FormBorderStyle = FormBorderStyle.FixedDialog;
            KeyPreview = true;
            MaximizeBox = false;
            MinimizeBox = false;
            MinimumSize = new Size(650, 500);
            Name = "LocationEditDialog";
            StartPosition = FormStartPosition.CenterParent;
            Text = "Detalles de Ubicación";
            pnlHeader.ResumeLayout(false);
            pnlHeader.PerformLayout();
            pnlMain.ResumeLayout(false);
            pnlProperties.ResumeLayout(false);
            pnlProperties.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)numCapacity).EndInit();
            pnlBasicInfo.ResumeLayout(false);
            pnlBasicInfo.PerformLayout();
            pnlActions.ResumeLayout(false);
            ResumeLayout(false);
        }
    }
}