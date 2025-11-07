// Wms.WinForms/Forms/PickingForm.cs

using Microsoft.Extensions.Logging;
using Wms.Application.DTOs;
using Wms.Application.UseCases.Items;
using Wms.Application.UseCases.Locations;
using Wms.Application.UseCases.Picking;
using Wms.WinForms.Common;
using Wms.WinForms.Controls;

namespace Wms.WinForms.Forms;

public partial class PickingForm : Form
{
    private const string CurrentUserId = "SYSTEM";
    private readonly IGetItemsUseCase _getItemsUseCase;
    private readonly IGetLocationsUseCase _getLocationsUseCase;
    private readonly ILogger<PickingForm> _logger;
    private readonly IPickOrderUseCase _pickOrderUseCase;

    public PickingForm(IPickOrderUseCase pickOrderUseCase, IGetItemsUseCase getItemsUseCase,
        IGetLocationsUseCase getLocationsUseCase, ILogger<PickingForm> logger)
    {
        _pickOrderUseCase = pickOrderUseCase;
        _getItemsUseCase = getItemsUseCase;
        _getLocationsUseCase = getLocationsUseCase;
        _logger = logger;
        InitializeComponent();
        SetupEventHandlers();
        SetupForm();
    }

    private void SetupEventHandlers()
    {
        txtBarcode.KeyPress += TxtBarcode_KeyPress;
        txtQuantity.KeyPress += TxtQuantity_KeyPress;
        txtFromLocation.KeyPress += TxtFromLocation_KeyPress;
        btnPick.Click += BtnPick_Click;
        btnClear.Click += BtnClear_Click;
        KeyDown += PickingForm_KeyDown;
    }

    private void SetupForm()
    {
        ModernUIHelper.StyleForm(this);
        KeyPreview = true;

        // Apply modern styling
        ModernUIHelper.StyleModernTextBox(txtBarcode);
        ModernUIHelper.StyleModernTextBox(txtQuantity);
        ModernUIHelper.StyleModernTextBox(txtFromLocation);
        ModernUIHelper.StyleModernTextBox(txtOrderNumber);
        ModernUIHelper.StyleModernTextBox(txtNotes);

        ModernUIHelper.StyleDangerButton(btnPick);
        ModernUIHelper.StyleSecondaryButton(btnClear);

        txtBarcode.Focus();

        // Setup autocomplete
        SetupAutocomplete();
    }

    private void SetupAutocomplete()
    {
        // Autocomplete para barcode (productos)
        SetupItemAutocomplete(txtBarcode);

        // Autocomplete para fromLocation (ubicaciones pickables)
        SetupLocationAutocomplete(txtFromLocation);
    }

    private void SetupItemAutocomplete(AutoCompleteTextBox textBox)
    {
        textBox.SearchFunction = async (searchTerm, ct) =>
        {
            var result = await _getItemsUseCase.ExecuteAsync(searchTerm, ct);
            return result.IsSuccess ? result.Value : Enumerable.Empty<ItemDto>();
        };

        textBox.DisplayMember = (item) =>
        {
            if (item is ItemDto dto)
                return $"{dto.Sku} - {dto.Name}";
            return item.ToString() ?? string.Empty;
        };

        textBox.ValueMember = (item) =>
        {
            if (item is ItemDto dto)
                return dto.Sku;
            return item.ToString() ?? string.Empty;
        };

        textBox.ItemSelected += (sender, e) =>
        {
            // Cuando se selecciona un item, buscar por barcode para obtener toda la info
            _ = Task.Run(async () =>
            {
                if (InvokeRequired)
                {
                    Invoke(async () => await ProcessBarcodeAsync());
                }
                else
                {
                    await ProcessBarcodeAsync();
                }
            });
        };
    }

    private void SetupLocationAutocomplete(AutoCompleteTextBox textBox)
    {
        textBox.SearchFunction = async (searchTerm, ct) =>
        {
            var result = await _getLocationsUseCase.GetPickableLocationsAsync(ct);
            if (result.IsSuccess)
            {
                // Filtrar localmente por el término de búsqueda
                var locations = result.Value.Where(l =>
                    l.Code.Contains(searchTerm, StringComparison.OrdinalIgnoreCase) ||
                    l.Name.Contains(searchTerm, StringComparison.OrdinalIgnoreCase));
                return locations;
            }
            return Enumerable.Empty<LocationDto>();
        };

        textBox.DisplayMember = (item) =>
        {
            if (item is LocationDto dto)
                return $"{dto.Code} - {dto.Name}";
            return item.ToString() ?? string.Empty;
        };

        textBox.ValueMember = (item) =>
        {
            if (item is LocationDto dto)
                return dto.Code;
            return item.ToString() ?? string.Empty;
        };
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
            txtFromLocation.Focus();
        }
    }

    private void TxtFromLocation_KeyPress(object? sender, KeyPressEventArgs e)
    {
        if (e.KeyChar == (char)Keys.Enter)
        {
            e.Handled = true;
            btnPick.PerformClick();
        }
    }

    private async Task ProcessBarcodeAsync()
    {
        try
        {
            if (string.IsNullOrWhiteSpace(txtBarcode.Text))
            {
                ModernUIHelper.ShowModernError("Por favor escanee o ingrese un código de barras");
                return;
            }

            SetBusy(true);

            var result = await _getItemsUseCase.GetByBarcodeAsync(txtBarcode.Text.Trim());

            if (result.IsFailure)
            {
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
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Error processing barcode {Barcode}", txtBarcode.Text);
            ModernUIHelper.ShowModernError($"Error al procesar el código de barras: {ex.Message}");
        }
        finally
        {
            SetBusy(false);
        }
    }

    private async void BtnPick_Click(object? sender, EventArgs e)
    {
        try
        {
            if (!ValidateInput())
                return;

            SetBusy(true);

            var request = new PickItemDto(
                ExtractSkuFromLabel(),
                txtFromLocation.Text.Trim(),
                decimal.Parse(txtQuantity.Text),
                txtOrderNumber.Text.Trim(),
                null, // LotNumber
                null, // SerialNumber
                txtNotes.Text.Trim()
            );

            var result = await _pickOrderUseCase.ExecuteAsync(request, CurrentUserId);

            if (result.IsFailure)
            {
                ModernUIHelper.ShowModernError(result.Error);
                return;
            }

            ModernUIHelper.ShowModernSuccess($"¡Artículo picking exitoso!\nID de Movimiento: {result.Value.MovementId}");

            ClearForm();
            txtBarcode.Focus();
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Error picking item");
            ModernUIHelper.ShowModernError($"Error al hacer picking del artículo: {ex.Message}");
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

    private void PickingForm_KeyDown(object? sender, KeyEventArgs e)
    {
        switch (e.KeyCode)
        {
            case Keys.F1:
                if (btnPick.Enabled)
                    btnPick.PerformClick();
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

        if (string.IsNullOrWhiteSpace(txtFromLocation.Text))
        {
            ModernUIHelper.ShowModernError("Por favor ingrese un código de ubicación de origen");
            txtFromLocation.Focus();
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
        txtFromLocation.Clear();
        txtOrderNumber.Clear();
        txtNotes.Clear();
        lblItemInfo.Text = "No hay artículo seleccionado";
        lblItemInfo.ForeColor = ModernUIHelper.Colors.TextMuted;
    }

    private void SetBusy(bool isBusy)
    {
        Cursor = isBusy ? Cursors.WaitCursor : Cursors.Default;
        btnPick.Enabled = !isBusy;
        btnClear.Enabled = !isBusy;
    }
}