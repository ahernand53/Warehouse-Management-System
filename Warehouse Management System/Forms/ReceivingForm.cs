// Wms.WinForms/Forms/ReceivingForm.cs

using System.Media;
using Microsoft.Extensions.Logging;
using Wms.Application.DTOs;
using Wms.Application.UseCases.Items;
using Wms.Application.UseCases.Receiving;
using Wms.WinForms.Common;

namespace Wms.WinForms.Forms;

public partial class ReceivingForm : Form
{
    private const string CurrentUserId = "SYSTEM"; // TODO: Implement proper user management
    private readonly IGetItemsUseCase _getItemsUseCase;
    private readonly ILogger<ReceivingForm> _logger;
    private readonly IReceiveItemUseCase _receiveItemUseCase;

    public ReceivingForm(IReceiveItemUseCase receiveItemUseCase, IGetItemsUseCase getItemsUseCase,
        ILogger<ReceivingForm> logger)
    {
        _receiveItemUseCase = receiveItemUseCase;
        _getItemsUseCase = getItemsUseCase;
        _logger = logger;
        InitializeComponent();
        SetupEventHandlers();
        SetupForm();
    }

    private void SetupEventHandlers()
    {
        txtBarcode.KeyPress += TxtBarcode_KeyPress;
        txtQuantity.KeyPress += TxtQuantity_KeyPress;
        txtLotNumber.KeyPress += TxtLotNumber_KeyPress;
        btnReceive.Click += BtnReceive_Click;
        btnClear.Click += BtnClear_Click;
        KeyDown += ReceivingForm_KeyDown;
    }

    private void SetupForm()
    {
        ModernUIHelper.StyleForm(this);
        txtLocationCode.Text = "RECEIVE"; // Default receiving location
        txtBarcode.Focus();

        // Enable scanner-first workflow
        KeyPreview = true;

        // Apply modern styling
        ModernUIHelper.StyleModernTextBox(txtBarcode);
        ModernUIHelper.StyleModernTextBox(txtQuantity);
        ModernUIHelper.StyleModernTextBox(txtLotNumber);
        ModernUIHelper.StyleModernTextBox(txtLocationCode);
        ModernUIHelper.StyleModernTextBox(txtReferenceNumber);
        ModernUIHelper.StyleModernTextBox(txtNotes);

        ModernUIHelper.StyleSuccessButton(btnReceive);
        ModernUIHelper.StyleSecondaryButton(btnClear);
    }

    private async void TxtBarcode_KeyPress(object? sender, KeyPressEventArgs e)
    {
        if (e.KeyChar == (char)Keys.Enter)
        {
            e.Handled = true;
            await ProcessBarcodeAsync();
        }
    }

    private void TxtQuantity_KeyPress(object? sender, KeyPressEventArgs e)
    {
        // Allow only numbers and decimal point
        if (!char.IsControl(e.KeyChar) && !char.IsDigit(e.KeyChar) && e.KeyChar != '.')
        {
            e.Handled = true;
            return;
        }

        // Only allow one decimal point
        if (e.KeyChar == '.' && ((TextBox)sender!).Text.Contains('.'))
        {
            e.Handled = true;
            return;
        }

        if (e.KeyChar == (char)Keys.Enter)
        {
            e.Handled = true;
            txtLotNumber.Focus();
        }
    }

    private void TxtLotNumber_KeyPress(object? sender, KeyPressEventArgs e)
    {
        if (e.KeyChar == (char)Keys.Enter)
        {
            e.Handled = true;
            btnReceive.PerformClick();
        }
    }

    private async Task ProcessBarcodeAsync()
    {
        try
        {
            if (string.IsNullOrWhiteSpace(txtBarcode.Text))
            {
                PlayErrorSound();
                ModernUIHelper.ShowModernError("Por favor escanee o ingrese un código de barras");
                return;
            }

            SetBusy(true);

            var result = await _getItemsUseCase.GetByBarcodeAsync(txtBarcode.Text.Trim());

            if (result.IsFailure)
            {
                PlayErrorSound();
                ModernUIHelper.ShowModernError($"Artículo no encontrado: {result.Error}");
                txtBarcode.SelectAll();
                return;
            }

            // Populate item information
            var item = result.Value;
            lblItemInfo.Text = $"SKU: {item.Sku}\nNombre: {item.Name}\nUOM: {item.UnitOfMeasure}";
            lblItemInfo.ForeColor = ModernUIHelper.Colors.Success;

            // Set focus to quantity
            txtQuantity.Focus();
            txtQuantity.SelectAll();

            // Show lot field if required
            if (item.RequiresLot)
            {
                lblLotNumber.Visible = true;
                txtLotNumber.Visible = true;
                lblExpiryDate.Visible = true;
                dtpExpiryDate.Visible = true;
            }
            else
            {
                lblLotNumber.Visible = false;
                txtLotNumber.Visible = false;
                lblExpiryDate.Visible = false;
                dtpExpiryDate.Visible = false;
            }

            PlaySuccessSound();
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Error processing barcode {Barcode}", txtBarcode.Text);
            PlayErrorSound();
            ModernUIHelper.ShowModernError($"Error al procesar el código de barras: {ex.Message}");
        }
        finally
        {
            SetBusy(false);
        }
    }

    private async void BtnReceive_Click(object? sender, EventArgs e)
    {
        try
        {
            if (!ValidateInput())
                return;

            SetBusy(true);

            var request = new ReceiveItemDto(
                ExtractSkuFromLabel(),
                txtLocationCode.Text.Trim(),
                decimal.Parse(txtQuantity.Text),
                string.IsNullOrWhiteSpace(txtLotNumber.Text) ? null : txtLotNumber.Text.Trim(),
                null,
                txtReferenceNumber.Text.Trim(),
                txtNotes.Text.Trim(),
                dtpExpiryDate.Visible ? dtpExpiryDate.Value.Date : null
            );

            var result = await _receiveItemUseCase.ExecuteAsync(request, CurrentUserId);

            if (result.IsFailure)
            {
                PlayErrorSound();
                ModernUIHelper.ShowModernError(result.Error);
                return;
            }

            PlaySuccessSound();
            ModernUIHelper.ShowModernSuccess($"¡Artículo recibido exitosamente!\nID de Movimiento: {result.Value.MovementId}");

            ClearForm();
            txtBarcode.Focus();
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Error receiving item");
            PlayErrorSound();
            ModernUIHelper.ShowModernError($"Error al recibir el artículo: {ex.Message}");
        }
        finally
        {
            SetBusy(false);
        }
    }

    private void BtnClear_Click(object? sender, EventArgs e)
    {
        ClearForm();
        txtBarcode.Focus();
    }

    private void ReceivingForm_KeyDown(object? sender, KeyEventArgs e)
    {
        switch (e.KeyCode)
        {
            case Keys.F1:
                if (btnReceive.Enabled)
                    btnReceive.PerformClick();
                break;
            case Keys.F2:
                btnClear.PerformClick();
                break;
            case Keys.Escape:
                Close();
                break;
        }
    }

    private bool ValidateInput()
    {
        if (string.IsNullOrWhiteSpace(lblItemInfo.Text) || lblItemInfo.Text == "No hay artículo seleccionado")
        {
            ModernUIHelper.ShowModernError("Por favor escanee un código de barras válido primero");
            txtBarcode.Focus();
            return false;
        }

        if (string.IsNullOrWhiteSpace(txtQuantity.Text) || !decimal.TryParse(txtQuantity.Text, out var quantity) ||
            quantity <= 0)
        {
            ModernUIHelper.ShowModernError("Por favor ingrese una cantidad válida");
            txtQuantity.Focus();
            return false;
        }

        if (string.IsNullOrWhiteSpace(txtLocationCode.Text))
        {
            ModernUIHelper.ShowModernError("Por favor ingrese un código de ubicación");
            txtLocationCode.Focus();
            return false;
        }

        if (txtLotNumber.Visible && string.IsNullOrWhiteSpace(txtLotNumber.Text))
        {
            ModernUIHelper.ShowModernError("El número de lote es requerido para este artículo");
            txtLotNumber.Focus();
            return false;
        }

        return true;
    }

    private string ExtractSkuFromLabel()
    {
        // Extract SKU from "SKU: WIDGET-001\nName: ..." format
        var lines = lblItemInfo.Text.Split('\n');
        if (lines.Length > 0 && lines[0].StartsWith("SKU: "))
        {
            return lines[0].Substring(5);
        }

        return string.Empty;
    }

    private void ClearForm()
    {
        txtBarcode.Clear();
        txtQuantity.Clear();
        txtLotNumber.Clear();
        txtReferenceNumber.Clear();
        txtNotes.Clear();
        lblItemInfo.Text = "No hay artículo seleccionado";
        lblItemInfo.ForeColor = ModernUIHelper.Colors.TextMuted;
        lblLotNumber.Visible = false;
        txtLotNumber.Visible = false;
        lblExpiryDate.Visible = false;
        dtpExpiryDate.Visible = false;
        dtpExpiryDate.Value = DateTime.Today.AddDays(30); // Default 30 days from today
    }

    private void SetBusy(bool isBusy)
    {
        Cursor = isBusy ? Cursors.WaitCursor : Cursors.Default;
        btnReceive.Enabled = !isBusy;
        btnClear.Enabled = !isBusy;
    }

    private void PlaySuccessSound()
    {
        try
        {
            SystemSounds.Beep.Play();
        }
        catch
        {
            // Ignore sound errors
        }
    }

    private void PlayErrorSound()
    {
        try
        {
            SystemSounds.Hand.Play();
        }
        catch
        {
            // Ignore sound errors
        }
    }
}