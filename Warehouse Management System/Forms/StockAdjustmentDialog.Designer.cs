// Wms.WinForms/Forms/StockAdjustmentDialog.Designer.cs
namespace Wms.WinForms.Forms
{
    partial class StockAdjustmentDialog
    {
        private System.ComponentModel.IContainer components = null;
        private Label lblTitle;
        private Label lblItemInfo;
        private Label lblNewQuantity;
        private TextBox txtNewQuantity;
        private Label lblReason;
        private TextBox txtReason;
        private Button btnOK;
        private Button btnCancel;
        private GroupBox grpAdjustment;

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
            this.lblTitle = new Label();
            this.grpAdjustment = new GroupBox();
            this.lblItemInfo = new Label();
            this.lblNewQuantity = new Label();
            this.txtNewQuantity = new TextBox();
            this.lblReason = new Label();
            this.txtReason = new TextBox();
            this.btnOK = new Button();
            this.btnCancel = new Button();

            this.grpAdjustment.SuspendLayout();
            this.SuspendLayout();

            // 
            // lblTitle
            // 
            this.lblTitle.AutoSize = true;
            this.lblTitle.Font = new Font("Segoe UI", 14F, FontStyle.Bold);
            this.lblTitle.Location = new Point(12, 9);
            this.lblTitle.Name = "lblTitle";
            this.lblTitle.Size = new Size(170, 25);
            this.lblTitle.TabIndex = 0;
            this.lblTitle.Text = "Ajuste de Inventario";

            // 
            // grpAdjustment
            // 
            this.grpAdjustment.Controls.Add(this.lblItemInfo);
            this.grpAdjustment.Controls.Add(this.lblNewQuantity);
            this.grpAdjustment.Controls.Add(this.txtNewQuantity);
            this.grpAdjustment.Controls.Add(this.lblReason);
            this.grpAdjustment.Controls.Add(this.txtReason);
            this.grpAdjustment.Font = new Font("Segoe UI", 10F, FontStyle.Bold);
            this.grpAdjustment.Location = new Point(12, 45);
            this.grpAdjustment.Name = "grpAdjustment";
            this.grpAdjustment.Size = new Size(460, 280);
            this.grpAdjustment.TabIndex = 1;
            this.grpAdjustment.TabStop = false;
            this.grpAdjustment.Text = "Detalles del Ajuste";

            // 
            // lblItemInfo
            // 
            this.lblItemInfo.Font = new Font("Segoe UI", 10F, FontStyle.Regular);
            this.lblItemInfo.Location = new Point(15, 25);
            this.lblItemInfo.Name = "lblItemInfo";
            this.lblItemInfo.Size = new Size(430, 60);
            this.lblItemInfo.TabIndex = 0;
            this.lblItemInfo.Text = "Artículo: SKU\nUbicación: LOC\nCantidad Actual: 0";

            // 
            // lblNewQuantity
            // 
            this.lblNewQuantity.AutoSize = true;
            this.lblNewQuantity.Font = new Font("Segoe UI", 10F, FontStyle.Regular);
            this.lblNewQuantity.Location = new Point(15, 100);
            this.lblNewQuantity.Name = "lblNewQuantity";
            this.lblNewQuantity.Size = new Size(95, 19);
            this.lblNewQuantity.TabIndex = 1;
            this.lblNewQuantity.Text = "Nueva Cantidad:";

            // 
            // txtNewQuantity
            // 
            this.txtNewQuantity.Font = new Font("Segoe UI", 12F, FontStyle.Regular);
            this.txtNewQuantity.Location = new Point(130, 97);
            this.txtNewQuantity.Name = "txtNewQuantity";
            this.txtNewQuantity.Size = new Size(120, 29);
            this.txtNewQuantity.TabIndex = 2;

            // 
            // lblReason
            // 
            this.lblReason.AutoSize = true;
            this.lblReason.Font = new Font("Segoe UI", 10F, FontStyle.Regular);
            this.lblReason.Location = new Point(15, 140);
            this.lblReason.Name = "lblReason";
            this.lblReason.Size = new Size(58, 19);
            this.lblReason.TabIndex = 3;
            this.lblReason.Text = "Razón:";

            // 
            // txtReason
            // 
            this.txtReason.Font = new Font("Segoe UI", 10F, FontStyle.Regular);
            this.txtReason.Location = new Point(15, 165);
            this.txtReason.Multiline = true;
            this.txtReason.Name = "txtReason";
            this.txtReason.PlaceholderText = "Ingrese la razón del ajuste (requerido)...";
            this.txtReason.Size = new Size(430, 100);
            this.txtReason.TabIndex = 4;

            // 
            // btnOK
            // 
            this.btnOK.BackColor = Color.LightGreen;
            this.btnOK.Font = new Font("Segoe UI", 11F, FontStyle.Bold);
            this.btnOK.Location = new Point(290, 340);
            this.btnOK.Name = "btnOK";
            this.btnOK.Size = new Size(90, 35);
            this.btnOK.TabIndex = 5;
            this.btnOK.Text = "Aceptar";
            this.btnOK.UseVisualStyleBackColor = false;

            // 
            // btnCancel
            // 
            this.btnCancel.BackColor = Color.LightCoral;
            this.btnCancel.Font = new Font("Segoe UI", 11F, FontStyle.Bold);
            this.btnCancel.Location = new Point(390, 340);
            this.btnCancel.Name = "btnCancel";
            this.btnCancel.Size = new Size(90, 35);
            this.btnCancel.TabIndex = 6;
            this.btnCancel.Text = "Cancelar";
            this.btnCancel.UseVisualStyleBackColor = false;

            // 
            // StockAdjustmentDialog
            // 
            this.AcceptButton = this.btnOK;
            this.AutoScaleDimensions = new SizeF(7F, 15F);
            this.AutoScaleMode = AutoScaleMode.Font;
            this.CancelButton = this.btnCancel;
            this.ClientSize = new Size(494, 387);
            this.Controls.Add(this.btnCancel);
            this.Controls.Add(this.btnOK);
            this.Controls.Add(this.grpAdjustment);
            this.Controls.Add(this.lblTitle);
            this.FormBorderStyle = FormBorderStyle.FixedDialog;
            this.KeyPreview = true;
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "StockAdjustmentDialog";
            this.StartPosition = FormStartPosition.CenterParent;
            this.Text = "Ajuste de Inventario";

            this.grpAdjustment.ResumeLayout(false);
            this.grpAdjustment.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();
        }
    }
}