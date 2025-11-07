// Wms.WinForms/Controls/AutoCompleteTextBox.cs

using System.Collections;
using System.Windows.Forms;

namespace Wms.WinForms.Controls;

/// <summary>
/// TextBox con funcionalidad de autocomplete que permite búsqueda incremental
/// con debounce y navegación por teclado.
/// </summary>
public class AutoCompleteTextBox : TextBox
{
    private System.Windows.Forms.Timer? _debounceTimer;
    private ListBox? _dropdownList;
    private Form? _dropdownForm;
    private Func<string, CancellationToken, Task<IEnumerable>>? _searchFunction;
    private Func<object, string>? _displayMember;
    private Func<object, string>? _valueMember;
    private CancellationTokenSource? _currentSearchCancellation;
    private const int DebounceMilliseconds = 400;
    private const int MinSearchLength = 2;
    private const int MaxResults = 20;
    private bool _isDropdownVisible;
    private int _selectedIndex = -1;

    public AutoCompleteTextBox()
    {
        _debounceTimer = new System.Windows.Forms.Timer();
        _debounceTimer.Interval = DebounceMilliseconds;
        _debounceTimer.Tick += DebounceTimer_Tick;
        
        TextChanged += AutoCompleteTextBox_TextChanged;
        KeyDown += AutoCompleteTextBox_KeyDown;
        KeyUp += AutoCompleteTextBox_KeyUp;
        LostFocus += AutoCompleteTextBox_LostFocus;
    }

    /// <summary>
    /// Función asíncrona que realiza la búsqueda. Debe retornar una colección de objetos.
    /// </summary>
    public Func<string, CancellationToken, Task<IEnumerable>>? SearchFunction
    {
        get => _searchFunction;
        set => _searchFunction = value;
    }

    /// <summary>
    /// Función que extrae el texto a mostrar de cada resultado.
    /// </summary>
    public Func<object, string>? DisplayMember
    {
        get => _displayMember;
        set => _displayMember = value;
    }

    /// <summary>
    /// Función que extrae el valor a asignar al TextBox cuando se selecciona un resultado.
    /// </summary>
    public Func<object, string>? ValueMember
    {
        get => _valueMember;
        set => _valueMember = value;
    }

    /// <summary>
    /// Evento que se dispara cuando el usuario selecciona un item del dropdown.
    /// </summary>
    public event EventHandler<ItemSelectedEventArgs>? ItemSelected;

    private void AutoCompleteTextBox_TextChanged(object? sender, EventArgs e)
    {
        if (_searchFunction == null) return;

        // Reiniciar el timer de debounce
        _debounceTimer?.Stop();
        
        if (Text.Length >= MinSearchLength)
        {
            _debounceTimer?.Start();
        }
        else
        {
            HideDropdown();
        }
    }

    private async void DebounceTimer_Tick(object? sender, EventArgs e)
    {
        _debounceTimer?.Stop();
        
        if (string.IsNullOrWhiteSpace(Text) || Text.Length < MinSearchLength)
        {
            HideDropdown();
            return;
        }

        await PerformSearchAsync(Text);
    }

    private async Task PerformSearchAsync(string searchText)
    {
        if (_searchFunction == null) return;

        // Cancelar búsqueda anterior si existe
        _currentSearchCancellation?.Cancel();
        _currentSearchCancellation = new CancellationTokenSource();

        try
        {
            var results = await _searchFunction(searchText, _currentSearchCancellation.Token);
            
            if (_currentSearchCancellation.Token.IsCancellationRequested)
                return;

            var resultsList = results.Cast<object>().Take(MaxResults).ToList();
            
            if (resultsList.Count > 0)
            {
                ShowDropdown(resultsList);
            }
            else
            {
                HideDropdown();
            }
        }
        catch (OperationCanceledException)
        {
            // Búsqueda cancelada, ignorar
        }
        catch (Exception ex)
        {
            // Log error pero no mostrar al usuario para no interrumpir el flujo
            System.Diagnostics.Debug.WriteLine($"Error en búsqueda autocomplete: {ex.Message}");
            HideDropdown();
        }
    }

    private void ShowDropdown(List<object> items)
    {
        if (items.Count == 0)
        {
            HideDropdown();
            return;
        }

        if (_dropdownList == null)
        {
            CreateDropdown();
        }

        _dropdownList.Items.Clear();
        foreach (var item in items)
        {
            _dropdownList.Items.Add(item);
        }

        _selectedIndex = -1;
        UpdateDropdownPosition();
        
        if (_dropdownForm != null && !_isDropdownVisible)
        {
            _dropdownForm.Show(this);
            _isDropdownVisible = true;
        }
    }

    private void CreateDropdown()
    {
        _dropdownList = new ListBox
        {
            BorderStyle = BorderStyle.FixedSingle,
            Font = Font,
            IntegralHeight = false,
            ItemHeight = 20,
            MaximumSize = new Size(Width, 200),
            MinimumSize = new Size(Width, 0),
            Size = new Size(Width, Math.Min(200, MaxResults * 20 + 4))
        };

        _dropdownList.MouseClick += DropdownList_MouseClick;
        _dropdownList.KeyDown += DropdownList_KeyDown;

        _dropdownForm = new Form
        {
            FormBorderStyle = FormBorderStyle.None,
            ShowInTaskbar = false,
            StartPosition = FormStartPosition.Manual,
            TopMost = true,
            BackColor = SystemColors.Window,
            Size = _dropdownList.Size
        };

        _dropdownForm.Controls.Add(_dropdownList);
        _dropdownList.Dock = DockStyle.Fill;
    }

    private void UpdateDropdownPosition()
    {
        if (_dropdownForm == null || _dropdownList == null) return;

        var screenPoint = PointToScreen(new Point(0, Height));
        _dropdownForm.Location = screenPoint;
        _dropdownForm.Width = Width;
    }

    private void HideDropdown()
    {
        if (_dropdownForm != null && _isDropdownVisible)
        {
            _dropdownForm.Hide();
            _isDropdownVisible = false;
        }
        _selectedIndex = -1;
    }

    private void DropdownList_MouseClick(object? sender, MouseEventArgs e)
    {
        if (_dropdownList == null) return;

        var index = _dropdownList.IndexFromPoint(e.Location);
        if (index >= 0 && index < _dropdownList.Items.Count)
        {
            SelectItem(index);
        }
    }

    private void DropdownList_KeyDown(object? sender, KeyEventArgs e)
    {
        if (_dropdownList == null) return;

        switch (e.KeyCode)
        {
            case Keys.Enter:
                if (_selectedIndex >= 0 && _selectedIndex < _dropdownList.Items.Count)
                {
                    SelectItem(_selectedIndex);
                    e.Handled = true;
                }
                break;
            case Keys.Escape:
                HideDropdown();
                Focus();
                e.Handled = true;
                break;
        }
    }

    private void AutoCompleteTextBox_KeyDown(object? sender, KeyEventArgs e)
    {
        if (!_isDropdownVisible || _dropdownList == null)
        {
            // Permitir que el evento se propague normalmente si no hay dropdown
            return;
        }

        switch (e.KeyCode)
        {
            case Keys.Down:
                if (_selectedIndex < _dropdownList.Items.Count - 1)
                {
                    _selectedIndex++;
                    _dropdownList.SelectedIndex = _selectedIndex;
                    _dropdownList.TopIndex = Math.Max(0, _selectedIndex - 5);
                }
                e.Handled = true;
                break;

            case Keys.Up:
                if (_selectedIndex > 0)
                {
                    _selectedIndex--;
                    _dropdownList.SelectedIndex = _selectedIndex;
                    _dropdownList.TopIndex = Math.Max(0, _selectedIndex - 5);
                }
                else if (_selectedIndex == 0)
                {
                    _selectedIndex = -1;
                    _dropdownList.SelectedIndex = -1;
                }
                e.Handled = true;
                break;

            case Keys.Enter:
                if (_selectedIndex >= 0 && _selectedIndex < _dropdownList.Items.Count)
                {
                    SelectItem(_selectedIndex);
                    e.Handled = true;
                }
                // Si no hay selección, permitir que el evento se propague (para procesar barcode)
                break;

            case Keys.Escape:
                HideDropdown();
                e.Handled = true;
                break;
        }
    }

    private void AutoCompleteTextBox_KeyUp(object? sender, KeyEventArgs e)
    {
        // No hacer nada especial en KeyUp
    }

    private void AutoCompleteTextBox_LostFocus(object? sender, EventArgs e)
    {
        // Ocultar dropdown cuando pierde el foco, pero con un pequeño delay
        // para permitir que el click en el dropdown se procese primero
        var timer = new System.Windows.Forms.Timer { Interval = 150 };
        timer.Tick += (s, args) =>
        {
            timer.Stop();
            timer.Dispose();
            
            // Verificar si el foco está en el dropdown
            if (_dropdownForm != null && !_dropdownForm.ContainsFocus && !Focused)
            {
                HideDropdown();
            }
        };
        timer.Start();
    }

    private void SelectItem(int index)
    {
        if (_dropdownList == null || index < 0 || index >= _dropdownList.Items.Count)
            return;

        var selectedItem = _dropdownList.Items[index];
        
        // Obtener el valor a mostrar
        string displayValue;
        if (_displayMember != null)
        {
            displayValue = _displayMember(selectedItem);
        }
        else
        {
            displayValue = selectedItem.ToString() ?? string.Empty;
        }

        // Obtener el valor a asignar
        string valueToSet;
        if (_valueMember != null)
        {
            valueToSet = _valueMember(selectedItem);
        }
        else
        {
            valueToSet = displayValue;
        }

        // Asignar el valor
        Text = valueToSet;
        SelectAll();
        
        HideDropdown();
        
        // Disparar evento
        ItemSelected?.Invoke(this, new ItemSelectedEventArgs(selectedItem, displayValue, valueToSet));
    }

    protected override void Dispose(bool disposing)
    {
        if (disposing)
        {
            _debounceTimer?.Stop();
            _debounceTimer?.Dispose();
            _currentSearchCancellation?.Cancel();
            _currentSearchCancellation?.Dispose();
            _dropdownForm?.Dispose();
            _dropdownList?.Dispose();
        }
        base.Dispose(disposing);
    }
}

/// <summary>
/// Argumentos del evento ItemSelected.
/// </summary>
public class ItemSelectedEventArgs : EventArgs
{
    public object Item { get; }
    public string DisplayValue { get; }
    public string Value { get; }

    public ItemSelectedEventArgs(object item, string displayValue, string value)
    {
        Item = item;
        DisplayValue = displayValue;
        Value = value;
    }
}

