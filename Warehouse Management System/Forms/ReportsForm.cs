// Wms.WinForms/Forms/ReportsForm.cs

using System.Diagnostics;
using System.Text;
using Microsoft.Extensions.Logging;
using Wms.Application.UseCases.Reports;
using Wms.Domain.Enums;
using Wms.WinForms.Common;

namespace Wms.WinForms.Forms;

public partial class ReportsForm : Form
{
    private readonly ILogger<ReportsForm> _logger;
    private readonly IMovementReportUseCase _movementReportUseCase;

    public ReportsForm(IMovementReportUseCase movementReportUseCase, ILogger<ReportsForm> logger)
    {
        _movementReportUseCase = movementReportUseCase;
        _logger = logger;
        InitializeComponent();
        SetupEventHandlers();
        SetupForm();
    }

    private void SetupEventHandlers()
    {
        btnGenerateReport.Click += BtnGenerateReport_Click;
        btnExport.Click += BtnExport_Click;
        KeyDown += ReportsForm_KeyDown;
    }

    private void SetupForm()
    {
        ModernUIHelper.StyleForm(this);
        dtpFromDate.Value = DateTime.Today.AddDays(-30);
        dtpToDate.Value = DateTime.Today;

        // Setup movement type combo
        cmbMovementType.Items.Add("Todos los Tipos");
        cmbMovementType.Items.Add("Recepción");
        cmbMovementType.Items.Add("Almacenamiento");
        cmbMovementType.Items.Add("Picking");
        cmbMovementType.Items.Add("Ajuste");
        cmbMovementType.SelectedIndex = 0;

        KeyPreview = true;

        // Apply modern styling
        ModernUIHelper.StyleModernTextBox(txtItemSku);
        ModernUIHelper.StyleModernComboBox(cmbMovementType);
        ModernUIHelper.StylePrimaryButton(btnGenerateReport);
        ModernUIHelper.StyleSuccessButton(btnExport);
        ModernUIHelper.StyleModernDataGridView(dgvMovements);

        btnExport.Enabled = false;
    }

    private async void BtnGenerateReport_Click(object? sender, EventArgs e)
    {
        try
        {
            SetBusy(true);
            lblStatus.Text = "Generando reporte...";

            var request = new MovementReportRequest(
                dtpFromDate.Value.Date,
                dtpToDate.Value.Date.AddDays(1).AddSeconds(-1),
                string.IsNullOrWhiteSpace(txtItemSku.Text) ? null : txtItemSku.Text.Trim(),
                MovementType: GetSelectedMovementType()
            );

            var result = await _movementReportUseCase.ExecuteAsync(request);

            if (result.IsFailure)
            {
                ModernUIHelper.ShowModernError($"Error al generar el reporte: {result.Error}");
                lblStatus.Text = "Error al generar el reporte";
                return;
            }

            var reportData = result.Value.OrderByDescending(m => m.Timestamp).ToList();

            dgvMovements.DataSource = null;
            dgvMovements.DataSource = reportData;

            ConfigureGridColumns();

            lblStatus.Text = $"Reporte generado: {reportData.Count} movimientos encontrados";
            btnExport.Enabled = reportData.Count > 0;

            if (reportData.Count > 0)
            {
                ModernUIHelper.ShowModernSuccess(
                    $"¡Reporte generado exitosamente!\nSe encontraron {reportData.Count} registros de movimientos.");
            }
            else
            {
                ModernUIHelper.ShowModernWarning("No se encontraron registros de movimientos para los criterios seleccionados.");
            }
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Error generating movement report");
            ModernUIHelper.ShowModernError($"Error al generar el reporte: {ex.Message}");
            lblStatus.Text = "Error al generar el reporte";
        }
        finally
        {
            SetBusy(false);
        }
    }

    private void BtnExport_Click(object? sender, EventArgs e)
    {
        try
        {
            if (dgvMovements.DataSource == null)
            {
                ModernUIHelper.ShowModernError("No hay datos para exportar. Por favor genere un reporte primero.");
                return;
            }

            var saveDialog = new SaveFileDialog
            {
                Filter = "CSV files (*.csv)|*.csv|Excel files (*.xlsx)|*.xlsx|All files (*.*)|*.*",
                FileName = $"Movement_Report_{DateTime.Now:yyyyMMdd_HHmmss}.csv",
                Title = "Exportar Reporte de Movimientos"
            };

            if (saveDialog.ShowDialog() == DialogResult.OK)
            {
                ExportToCsv(saveDialog.FileName);
                ModernUIHelper.ShowModernSuccess($"¡Reporte exportado exitosamente a:\n{saveDialog.FileName}");

                // Ask if user wants to open the file
                var result = MessageBox.Show("¿Desea abrir el archivo exportado?", "Exportación Completada",
                    MessageBoxButtons.YesNo, MessageBoxIcon.Question);

                if (result == DialogResult.Yes)
                {
                    Process.Start(new ProcessStartInfo
                    {
                        FileName = saveDialog.FileName,
                        UseShellExecute = true
                    });
                }
            }
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Error exporting report");
            ModernUIHelper.ShowModernError($"Error al exportar el reporte: {ex.Message}");
        }
    }

    private void BtnClear_Click(object? sender, EventArgs e)
    {
        dgvMovements.DataSource = null;
        txtItemSku.Clear();
        cmbMovementType.SelectedIndex = 0;
        dtpFromDate.Value = DateTime.Today.AddDays(-30);
        dtpToDate.Value = DateTime.Today;
        btnExport.Enabled = false;
        lblStatus.Text = "Listo";
    }

    private void ReportsForm_KeyDown(object? sender, KeyEventArgs e)
    {
        switch (e.KeyCode)
        {
            case Keys.F1:
                btnGenerateReport.PerformClick();
                break;
            case Keys.F2:
                if (btnExport.Enabled)
                    btnExport.PerformClick();
                break;
            case Keys.Escape:
                Close();
                break;
        }
    }

    private MovementType? GetSelectedMovementType()
    {
        return cmbMovementType.SelectedIndex switch
        {
            1 => MovementType.Receipt,
            2 => MovementType.Putaway,
            3 => MovementType.Pick,
            4 => MovementType.Adjustment,
            _ => null
        };
    }

    private void ConfigureGridColumns()
    {
        if (dgvMovements?.Columns == null || dgvMovements.Columns.Count == 0) return;

        try
        {
            SetColumnPropertySafely("Id", col =>
            {
                col.HeaderText = "ID";
                col.Width = 60;
            });
            SetColumnPropertySafely("Type", col =>
            {
                col.HeaderText = "Tipo";
                col.Width = 80;
            });
            SetColumnPropertySafely("ItemSku", col =>
            {
                col.HeaderText = "SKU del Artículo";
                col.Width = 100;
            });
            SetColumnPropertySafely("ItemName", col =>
            {
                col.HeaderText = "Nombre del Artículo";
                col.Width = 150;
            });
            SetColumnPropertySafely("FromLocationCode", col =>
            {
                col.HeaderText = "Desde";
                col.Width = 80;
            });
            SetColumnPropertySafely("ToLocationCode", col =>
            {
                col.HeaderText = "Hasta";
                col.Width = 80;
            });
            SetColumnPropertySafely("Quantity", col =>
            {
                col.HeaderText = "Cantidad";
                col.Width = 80;
                col.DefaultCellStyle.Format = "N2";
                col.DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight;
                col.DefaultCellStyle.ForeColor = ModernUIHelper.Colors.Primary;
            });
            SetColumnPropertySafely("LotNumber", col =>
            {
                col.HeaderText = "Lote";
                col.Width = 80;
            });
            SetColumnPropertySafely("SerialNumber", col =>
            {
                col.HeaderText = "Serie";
                col.Width = 100;
            });
            SetColumnPropertySafely("UserId", col =>
            {
                col.HeaderText = "Usuario";
                col.Width = 80;
            });
            SetColumnPropertySafely("ReferenceNumber", col =>
            {
                col.HeaderText = "Referencia";
                col.Width = 100;
            });
            SetColumnPropertySafely("Notes", col =>
            {
                col.HeaderText = "Notas";
                col.Width = 150;
            });
            SetColumnPropertySafely("Timestamp", col =>
            {
                col.HeaderText = "Fecha/Hora";
                col.Width = 130;
                col.DefaultCellStyle.Format = "yyyy-MM-dd HH:mm";
                col.DefaultCellStyle.ForeColor = ModernUIHelper.Colors.TextSecondary;
            });

            dgvMovements.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill;
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Error configuring grid columns");
        }
    }

    // Helper method for safe column configuration
    private void SetColumnPropertySafely(string columnName, Action<DataGridViewColumn> configureAction)
    {
        try
        {
            if (dgvMovements?.Columns?.Contains(columnName) == true)
            {
                var column = dgvMovements.Columns[columnName];
                if (column != null)
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

    private void ExportToCsv(string fileName)
    {
        var reportData = (IEnumerable<MovementReportDto>)dgvMovements.DataSource;

        var csv = new StringBuilder();
        csv.AppendLine(
            "ID,Type,ItemSku,ItemName,FromLocation,ToLocation,Quantity,LotNumber,SerialNumber,UserId,ReferenceNumber,Notes,Timestamp");

        foreach (var row in reportData)
        {
            csv.AppendLine(
                $"{row.Id},{row.Type},{row.ItemSku},\"{row.ItemName}\",{row.FromLocationCode},{row.ToLocationCode},{row.Quantity},{row.LotNumber},{row.SerialNumber},{row.UserId},{row.ReferenceNumber},\"{row.Notes}\",{row.Timestamp:yyyy-MM-dd HH:mm:ss}");
        }

        File.WriteAllText(fileName, csv.ToString());
    }

    private void SetBusy(bool isBusy)
    {
        Cursor = isBusy ? Cursors.WaitCursor : Cursors.Default;
        btnGenerateReport.Enabled = !isBusy;
        btnExport.Enabled = !isBusy && dgvMovements.DataSource != null;
        dgvMovements.Enabled = !isBusy;
    }
}