using System.Drawing;
using System.Windows.Forms;

namespace SessionAccounting.Common;

public static class Ui
{
    public static Button CreateButton(string text, EventHandler onClick)
    {
        var button = new Button
        {
            Text = text,
            AutoSize = true,
            MinimumSize = new Size(110, 34),
            Padding = new Padding(8, 4, 8, 4),
            Margin = new Padding(4)
        };

        button.Click += onClick;
        return button;
    }

    public static TextBox CreateTextBox()
    {
        return new TextBox
        {
            Dock = DockStyle.Fill
        };
    }

    public static ComboBox CreateComboBox()
    {
        return new ComboBox
        {
            Dock = DockStyle.Fill,
            DropDownStyle = ComboBoxStyle.DropDownList
        };
    }

    public static NumericUpDown CreateNumeric(decimal minimum, decimal maximum, int decimals = 0, decimal increment = 1)
    {
        return new NumericUpDown
        {
            Dock = DockStyle.Fill,
            Minimum = minimum,
            Maximum = maximum,
            DecimalPlaces = decimals,
            Increment = increment,
            ThousandsSeparator = true
        };
    }

    public static Label CreateValueLabel(string text = "")
    {
        return new Label
        {
            Text = text,
            Dock = DockStyle.Fill,
            TextAlign = ContentAlignment.MiddleLeft,
            BackColor = Color.White,
            BorderStyle = BorderStyle.FixedSingle,
            Padding = new Padding(6, 0, 6, 0)
        };
    }

    public static DataGridView CreateGrid()
    {
        return new DataGridView
        {
            Dock = DockStyle.Fill,
            AllowUserToAddRows = false,
            AllowUserToDeleteRows = false,
            AllowUserToResizeRows = false,
            ReadOnly = true,
            MultiSelect = false,
            SelectionMode = DataGridViewSelectionMode.FullRowSelect,
            RowHeadersVisible = false,
            AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.AllCells,
            ColumnHeadersHeightSizeMode = DataGridViewColumnHeadersHeightSizeMode.AutoSize,
            BackgroundColor = Color.White,
            BorderStyle = BorderStyle.None
        };
    }

    public static DataGridView CreateEditGrid()
    {
        return new DataGridView
        {
            Dock = DockStyle.Fill,
            AllowUserToAddRows = true,
            AllowUserToDeleteRows = true,
            AllowUserToResizeRows = false,
            ReadOnly = false,
            MultiSelect = false,
            SelectionMode = DataGridViewSelectionMode.CellSelect,
            RowHeadersVisible = false,
            AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill,
            ColumnHeadersHeightSizeMode = DataGridViewColumnHeadersHeightSizeMode.AutoSize,
            BackgroundColor = Color.White,
            BorderStyle = BorderStyle.None
        };
    }

    public static void AddRow(TableLayoutPanel layout, string labelText, Control control)
    {
        var row = layout.RowCount;
        layout.RowCount = row + 1;
        layout.RowStyles.Add(new RowStyle(SizeType.AutoSize));

        var label = new Label
        {
            Text = labelText,
            AutoSize = true,
            Anchor = AnchorStyles.Left,
            Margin = new Padding(3, 9, 12, 3)
        };

        layout.Controls.Add(label, 0, row);

        control.Dock = DockStyle.Fill;
        control.Margin = new Padding(3, 5, 3, 5);
        layout.Controls.Add(control, 1, row);
    }

    public static void AddSpanRow(TableLayoutPanel layout, Control control, int height = 0)
    {
        var row = layout.RowCount;
        layout.RowCount = row + 1;
        layout.RowStyles.Add(height > 0
            ? new RowStyle(SizeType.Absolute, height)
            : new RowStyle(SizeType.AutoSize));

        control.Dock = DockStyle.Fill;
        control.Margin = new Padding(3, 8, 3, 3);
        layout.Controls.Add(control, 0, row);
        layout.SetColumnSpan(control, 2);
    }

    public static void ShowError(string message, string caption)
    {
        MessageBox.Show(message, caption, MessageBoxButtons.OK, MessageBoxIcon.Error);
    }
}
