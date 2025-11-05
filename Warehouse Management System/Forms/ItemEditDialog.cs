// Wms.WinForms/Forms/ItemEditDialog.cs

using Microsoft.Extensions.Logging;
using Wms.Application.DTOs;
using Wms.Application.UseCases.Items;
using Wms.WinForms.Common;
using CreateItemDto = Wms.Application.UseCases.Items.CreateItemDto;
using UpdateItemDto = Wms.Application.UseCases.Items.UpdateItemDto;

namespace Wms.WinForms.Forms;

public partial class ItemEditDialog : Form
{
    private const string CurrentUserId = "SYSTEM";
    private readonly ICreateItemUseCase _createItemUseCase;
    private readonly IGetItemsUseCase _getItemsUseCase;

    private readonly int? _itemId;
    private readonly ILogger<ItemEditDialog> _logger;
    private readonly IUpdateItemUseCase _updateItemUseCase;
    private ItemDto? _originalItem;

    public ItemEditDialog(ICreateItemUseCase createItemUseCase, IUpdateItemUseCase updateItemUseCase,
        IGetItemsUseCase getItemsUseCase, ILogger<ItemEditDialog> logger, int? itemId = null)
    {
        _createItemUseCase = createItemUseCase;
        _updateItemUseCase = updateItemUseCase;
        _getItemsUseCase = getItemsUseCase;
        _logger = logger;
        _itemId = itemId;

        InitializeComponent();
        SetupEventHandlers();
        SetupForm();

        if (IsEditMode)
        {
            LoadItemDataAsync();
        }
    }

    public bool IsEditMode => _itemId.HasValue;

    private void SetupEventHandlers()
    {
        btnSave.Click += BtnSave_Click;
        btnCancel.Click += BtnCancel_Click;
        btnAddBarcode.Click += BtnAddBarcode_Click;
        btnRemoveBarcode.Click += BtnRemoveBarcode_Click;
        lstBarcodes.SelectedIndexChanged += LstBarcodes_SelectedIndexChanged;
        KeyDown += ItemEditDialog_KeyDown;
    }

    private void SetupForm()
    {
        ModernUIHelper.StyleForm(this);
        KeyPreview = true;

        // Apply modern styling
        ModernUIHelper.StyleModernTextBox(txtSku);
        ModernUIHelper.StyleModernTextBox(txtName);
        ModernUIHelper.StyleModernTextBox(txtDescription);
        ModernUIHelper.StyleModernTextBox(txtUnitOfMeasure);
        ModernUIHelper.StyleModernTextBox(txtNewBarcode);

        // Set up price numeric up-down styling
        numPrice.Font = ModernUIHelper.Fonts.Body;
        numPrice.Height = 35;

        ModernUIHelper.StylePrimaryButton(btnSave);
        ModernUIHelper.StyleSecondaryButton(btnCancel);
        ModernUIHelper.StyleSuccessButton(btnAddBarcode);
        ModernUIHelper.StyleDangerButton(btnRemoveBarcode);

        // Set up numeric up-down styling
        numShelfLifeDays.Font = ModernUIHelper.Fonts.Body;
        numShelfLifeDays.Height = 35;

        btnRemoveBarcode.Enabled = false;

        Text = IsEditMode ? "Editar Artículo" : "Agregar Nuevo Artículo";
        lblFormTitle.Text = IsEditMode ? "Editar Artículo" : "Agregar Nuevo Artículo";

        if (!IsEditMode)
        {
            txtSku.ReadOnly = false;
            txtSku.BackColor = Color.White;
        }
        else
        {
            txtSku.ReadOnly = true;
            txtSku.BackColor = ModernUIHelper.Colors.BackgroundSecondary;
        }
    }

    private async void LoadItemDataAsync()
    {
        if (!_itemId.HasValue) return;

        try
        {
            SetBusy(true);

            var result = await _getItemsUseCase.GetByIdAsync(_itemId.Value);
            if (result.IsFailure)
            {
            ModernUIHelper.ShowModernError($"Error al cargar el artículo: {result.Error}");
            DialogResult = DialogResult.Cancel;
            Close();
            return;
        }

        _originalItem = result.Value;
        PopulateForm(_originalItem);
    }
    catch (Exception ex)
    {
        _logger.LogError(ex, "Error loading item {ItemId}", _itemId);
        ModernUIHelper.ShowModernError($"Error al cargar el artículo: {ex.Message}");
            DialogResult = DialogResult.Cancel;
            Close();
        }
        finally
        {
            SetBusy(false);
        }
    }

    private void PopulateForm(ItemDto item)
    {
        txtSku.Text = item.Sku;
        txtName.Text = item.Name;
        txtDescription.Text = item.Description;
        txtUnitOfMeasure.Text = item.UnitOfMeasure;
        numPrice.Value = item.Price ?? 0;
        chkRequiresLot.Checked = item.RequiresLot;
        chkRequiresSerial.Checked = item.RequiresSerial;
        numShelfLifeDays.Value = item.ShelfLifeDays;

        lstBarcodes.Items.Clear();
        foreach (var barcode in item.Barcodes)
        {
            lstBarcodes.Items.Add(barcode);
        }
    }

    private async void BtnSave_Click(object? sender, EventArgs e)
    {
        try
        {
            if (!ValidateInput()) return;

            SetBusy(true);

            var barcodes = lstBarcodes.Items.Cast<string>().ToList();

            if (IsEditMode)
            {
                var request = new UpdateItemDto(
                    _itemId!.Value,
                    txtName.Text.Trim(),
                    txtDescription.Text.Trim(),
                    (int)numShelfLifeDays.Value,
                    numPrice.Value > 0 ? (decimal?)numPrice.Value : null,
                    barcodes
                );

                var result = await _updateItemUseCase.ExecuteAsync(request, CurrentUserId);
                if (result.IsFailure)
                {
                    ModernUIHelper.ShowModernError(result.Error);
                    return;
                }
            }
            else
            {
                var request = new CreateItemDto(
                    txtSku.Text.Trim(),
                    txtName.Text.Trim(),
                    txtDescription.Text.Trim(),
                    txtUnitOfMeasure.Text.Trim(),
                    chkRequiresLot.Checked,
                    chkRequiresSerial.Checked,
                    (int)numShelfLifeDays.Value,
                    numPrice.Value > 0 ? (decimal?)numPrice.Value : null,
                    barcodes
                );

                var result = await _createItemUseCase.ExecuteAsync(request, CurrentUserId);
                if (result.IsFailure)
                {
                    ModernUIHelper.ShowModernError(result.Error);
                    return;
                }
            }

            DialogResult = DialogResult.OK;
            Close();
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Error saving item");
            ModernUIHelper.ShowModernError($"Error al guardar el artículo: {ex.Message}");
        }
        finally
        {
            SetBusy(false);
        }
    }

    private void BtnCancel_Click(object? sender, EventArgs e)
    {
        DialogResult = DialogResult.Cancel;
        Close();
    }

    private void BtnAddBarcode_Click(object? sender, EventArgs e)
    {
        var barcode = txtNewBarcode.Text.Trim();
        if (string.IsNullOrWhiteSpace(barcode))
        {
            ModernUIHelper.ShowModernError("Por favor ingrese un código de barras");
            txtNewBarcode.Focus();
            return;
        }

        if (barcode.Length < 3)
        {
            ModernUIHelper.ShowModernError("El código de barras debe tener al menos 3 caracteres");
            txtNewBarcode.Focus();
            return;
        }

        if (lstBarcodes.Items.Contains(barcode))
        {
            ModernUIHelper.ShowModernWarning("Este código de barras ya está agregado");
            txtNewBarcode.Focus();
            return;
        }

        lstBarcodes.Items.Add(barcode);
        txtNewBarcode.Clear();
        txtNewBarcode.Focus();
    }

    private void BtnRemoveBarcode_Click(object? sender, EventArgs e)
    {
        if (lstBarcodes.SelectedIndex >= 0)
        {
            lstBarcodes.Items.RemoveAt(lstBarcodes.SelectedIndex);
        }
    }

    private void LstBarcodes_SelectedIndexChanged(object? sender, EventArgs e)
    {
        btnRemoveBarcode.Enabled = lstBarcodes.SelectedIndex >= 0;
    }

    private void ItemEditDialog_KeyDown(object? sender, KeyEventArgs e)
    {
        switch (e.KeyCode)
        {
            case Keys.F1:
                btnSave.PerformClick();
                break;
            case Keys.Escape:
                btnCancel.PerformClick();
                break;
            case Keys.Enter:
                if (txtNewBarcode.Focused)
                {
                    e.Handled = true;
                    btnAddBarcode.PerformClick();
                }

                break;
        }
    }

    private bool ValidateInput()
    {
        if (string.IsNullOrWhiteSpace(txtSku.Text))
        {
            ModernUIHelper.ShowModernError("El SKU es requerido");
            txtSku.Focus();
            return false;
        }

        if (string.IsNullOrWhiteSpace(txtName.Text))
        {
            ModernUIHelper.ShowModernError("El nombre es requerido");
            txtName.Focus();
            return false;
        }

        if (string.IsNullOrWhiteSpace(txtUnitOfMeasure.Text))
        {
            ModernUIHelper.ShowModernError("La unidad de medida es requerida");
            txtUnitOfMeasure.Focus();
            return false;
        }

        return true;
    }

    private void SetBusy(bool isBusy)
    {
        Cursor = isBusy ? Cursors.WaitCursor : Cursors.Default;
        btnSave.Enabled = !isBusy;
        btnCancel.Enabled = !isBusy;
        pnlMain.Enabled = !isBusy;
    }
}