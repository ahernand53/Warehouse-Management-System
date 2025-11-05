// Wms.WinForms/Forms/ItemManagementForm.cs

using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using Wms.Application.UseCases.Items;
using Wms.WinForms.Common;

namespace Wms.WinForms.Forms;

public partial class ItemManagementForm : Form
{
    private const string CurrentUserId = "SYSTEM";
    private readonly IGetItemsUseCase _getItemsUseCase;
    private readonly ILogger<ItemManagementForm> _logger;

    public ItemManagementForm(IGetItemsUseCase getItemsUseCase, ILogger<ItemManagementForm> logger)
    {
        _getItemsUseCase = getItemsUseCase;
        _logger = logger;
        InitializeComponent();
        SetupEventHandlers();
        SetupForm();
        LoadItemsAsync();
    }

    private void SetupEventHandlers()
    {
        btnAdd.Click += BtnAdd_Click;
        btnEdit.Click += BtnEdit_Click;
        btnDelete.Click += BtnDelete_Click;
        btnRefresh.Click += BtnRefresh_Click;
        txtSearch.KeyPress += TxtSearch_KeyPress;
        dgvItems.SelectionChanged += DgvItems_SelectionChanged;
        dgvItems.CellDoubleClick += DgvItems_CellDoubleClick;
        KeyDown += ItemManagementForm_KeyDown;
    }

    private void SetupForm()
    {
        ModernUIHelper.StyleForm(this);
        KeyPreview = true;
        btnEdit.Enabled = false;
        btnDelete.Enabled = false;
    }

    private async void TxtSearch_KeyPress(object? sender, KeyPressEventArgs e)
    {
        if (e.KeyChar == (char)Keys.Enter)
        {
            e.Handled = true;
            await SearchItemsAsync();
        }
    }

    private async void BtnRefresh_Click(object? sender, EventArgs e)
    {
        await LoadItemsAsync();
    }

    private void BtnAdd_Click(object? sender, EventArgs e)
    {
        ShowItemDialog();
    }

    private void BtnEdit_Click(object? sender, EventArgs e)
    {
        if (dgvItems.SelectedRows.Count == 0) return;

        var selectedRow = dgvItems.SelectedRows[0];
        var itemId = Convert.ToInt32(selectedRow.Cells["Id"].Value);
        ShowItemDialog(itemId);
    }

    private async void BtnDelete_Click(object? sender, EventArgs e)
    {
        if (dgvItems.SelectedRows.Count == 0) return;

        var selectedRow = dgvItems.SelectedRows[0];
        var itemSku = selectedRow.Cells["Sku"].Value?.ToString() ?? "";
        var itemName = selectedRow.Cells["Name"].Value?.ToString() ?? "";

        var result = MessageBox.Show(
            $"¿Está seguro de que desea eliminar el artículo '{itemSku} - {itemName}'?\n\nEsta acción no se puede deshacer.",
            "Confirmar Eliminación",
            MessageBoxButtons.YesNo,
            MessageBoxIcon.Warning);

        if (result == DialogResult.Yes)
        {
            // TODO: Implement delete functionality
            ModernUIHelper.ShowModernWarning("La funcionalidad de eliminación aún no está implementada.");
        }
    }

    private void DgvItems_SelectionChanged(object? sender, EventArgs e)
    {
        var hasSelection = dgvItems.SelectedRows.Count > 0;
        btnEdit.Enabled = hasSelection;
        btnDelete.Enabled = hasSelection;
    }

    private void DgvItems_CellDoubleClick(object? sender, DataGridViewCellEventArgs e)
    {
        if (e.RowIndex >= 0)
        {
            BtnEdit_Click(sender, e);
        }
    }

    private void ItemManagementForm_KeyDown(object? sender, KeyEventArgs e)
    {
        switch (e.KeyCode)
        {
            case Keys.F1:
                btnAdd.PerformClick();
                break;
            case Keys.F2:
                if (btnEdit.Enabled)
                    btnEdit.PerformClick();
                break;
            case Keys.Delete:
                if (btnDelete.Enabled)
                    btnDelete.PerformClick();
                break;
            case Keys.F5:
                btnRefresh.PerformClick();
                break;
            case Keys.Escape:
                Close();
                break;
        }
    }

    private async Task LoadItemsAsync()
    {
        try
        {
            SetBusy(true);
            lblStatus.Text = "Cargando artículos...";

            var result = await _getItemsUseCase.ExecuteAsync();

            if (result.IsFailure)
            {
                ModernUIHelper.ShowModernError($"Error al cargar artículos: {result.Error}");
                return;
            }

            var items = result.Value.OrderBy(i => i.Sku).ToList();

            dgvItems.DataSource = null;
            dgvItems.DataSource = items;

            ConfigureGridColumns();

            lblStatus.Text = $"Cargados {items.Count} artículos";
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Error loading items");
            ModernUIHelper.ShowModernError($"Error al cargar artículos: {ex.Message}");
            lblStatus.Text = "Error al cargar artículos";
        }
        finally
        {
            SetBusy(false);
        }
    }

    private async Task SearchItemsAsync()
    {
        try
        {
            if (string.IsNullOrWhiteSpace(txtSearch.Text))
            {
                await LoadItemsAsync();
                return;
            }

            SetBusy(true);
            lblStatus.Text = "Buscando artículos...";

            var result = await _getItemsUseCase.ExecuteAsync(txtSearch.Text.Trim());

            if (result.IsFailure)
            {
                ModernUIHelper.ShowModernError($"Error en la búsqueda: {result.Error}");
                return;
            }

            var items = result.Value.OrderBy(i => i.Sku).ToList();

            dgvItems.DataSource = null;
            dgvItems.DataSource = items;

            ConfigureGridColumns();

            lblStatus.Text = $"Se encontraron {items.Count} artículos";
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Error searching items");
            ModernUIHelper.ShowModernError($"Error al buscar: {ex.Message}");
        }
        finally
        {
            SetBusy(false);
        }
    }

    private void ConfigureGridColumns()
    {
        if (dgvItems?.Columns == null || dgvItems.Columns.Count == 0) return;

        try
        {
            ModernUIHelper.StyleModernDataGridView(dgvItems);

            // Hide ID columns safely
            SetColumnPropertySafely("Id", col => col.Visible = false);
            SetColumnPropertySafely("CreatedAt", col => col.Visible = false);
            SetColumnPropertySafely("UpdatedAt", col => col.Visible = false);

            // Configure visible columns using only FillWeight for better responsiveness
            SetColumnPropertySafely("Sku", col =>
            {
                if (col.Visible)
                {
                    col.HeaderText = "SKU";
                    col.FillWeight = 12;
                }
            });

            SetColumnPropertySafely("Name", col =>
            {
                if (col.Visible)
                {
                    col.HeaderText = "Nombre";
                    col.FillWeight = 25;
                }
            });

            SetColumnPropertySafely("Description", col =>
            {
                if (col.Visible)
                {
                    col.HeaderText = "Descripción";
                    col.FillWeight = 30;
                }
            });

            SetColumnPropertySafely("Price", col =>
            {
                if (col.Visible)
                {
                    col.HeaderText = "Precio";
                    col.FillWeight = 12;
                    col.DefaultCellStyle.Format = "C2";
                    col.DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight;
                }
            });

            SetColumnPropertySafely("UnitOfMeasure", col =>
            {
                if (col.Visible)
                {
                    col.HeaderText = "UOM";
                    col.FillWeight = 8;
                    col.DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter;
                }
            });

            SetColumnPropertySafely("IsActive", col =>
            {
                if (col.Visible)
                {
                    col.HeaderText = "Active";
                    col.FillWeight = 8;
                    col.DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter;
                }
            });

            SetColumnPropertySafely("RequiresLot", col =>
            {
                if (col.Visible)
                {
                    col.HeaderText = "Lot Ctrl";
                    col.FillWeight = 8;
                    col.DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter;
                }
            });

            // Use Fill mode for responsive behavior
            dgvItems.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill;
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Error configuring grid columns");
            // Fallback configuration
            try
            {
                dgvItems.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.AllCells;
            }
            catch
            {
                // If even basic auto-sizing fails, leave as-is
            }
        }
    }

    private void SetColumnPropertySafely(string columnName, Action<DataGridViewColumn> configureAction)
    {
        try
        {
            if (dgvItems?.Columns?.Contains(columnName) == true)
            {
                var column = dgvItems.Columns[columnName];
                if (column != null && !column.IsDataBound) // Check column state
                {
                    configureAction(column);
                }
            }
        }
        catch (Exception ex)
        {
            _logger.LogWarning(ex, "Error configuring column {ColumnName}", columnName);
        }
    }

    private void ShowItemDialog(int? itemId = null)
    {
        try
        {
            var createItemUseCase = Program.ServiceProvider.GetRequiredService<ICreateItemUseCase>();
            var updateItemUseCase = Program.ServiceProvider.GetRequiredService<IUpdateItemUseCase>();
            var logger = Program.ServiceProvider.GetRequiredService<ILogger<ItemEditDialog>>();

            var dialog = new ItemEditDialog(createItemUseCase, updateItemUseCase, _getItemsUseCase, logger, itemId);

            if (dialog.ShowDialog(this) == DialogResult.OK)
            {
                // Reload the items grid
                _ = LoadItemsAsync();
            }
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Error showing item dialog");
            ModernUIHelper.ShowModernError($"Error al abrir el diálogo de artículo: {ex.Message}");
        }
    }

    private void SetBusy(bool isBusy)
    {
        Cursor = isBusy ? Cursors.WaitCursor : Cursors.Default;
        btnAdd.Enabled = !isBusy;
        btnEdit.Enabled = !isBusy && dgvItems.SelectedRows.Count > 0;
        btnDelete.Enabled = !isBusy && dgvItems.SelectedRows.Count > 0;
        btnRefresh.Enabled = !isBusy;
        dgvItems.Enabled = !isBusy;
    }
}