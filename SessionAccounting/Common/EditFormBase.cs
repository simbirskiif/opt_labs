using System.Drawing;
using System.Windows.Forms;

namespace SessionAccounting.Common;

public abstract class EditFormBase : Form
{
    protected TableLayoutPanel FieldsLayout { get; }

    protected int? EntityId { get; private set; }

    protected EditFormBase(string title)
    {
        Text = title;
        ClientSize = new Size(600, 470);
        MinimumSize = new Size(520, 380);
        StartPosition = FormStartPosition.CenterParent;
        FormBorderStyle = FormBorderStyle.FixedDialog;
        MaximizeBox = false;
        MinimizeBox = false;

        FieldsLayout = new TableLayoutPanel
        {
            Dock = DockStyle.Top,
            AutoSize = true,
            AutoSizeMode = AutoSizeMode.GrowAndShrink,
            ColumnCount = 2,
            Padding = new Padding(16)
        };

        FieldsLayout.ColumnStyles.Add(new ColumnStyle(SizeType.AutoSize));
        FieldsLayout.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 100));

        var saveButton = Ui.CreateButton("Сохранить", ButtonSave_Click);
        var cancelButton = Ui.CreateButton("Отмена", (_, _) => Close());
        cancelButton.DialogResult = DialogResult.Cancel;

        var buttonPanel = new FlowLayoutPanel
        {
            Dock = DockStyle.Fill,
            FlowDirection = FlowDirection.RightToLeft,
            AutoSize = true,
            Padding = new Padding(8)
        };

        buttonPanel.Controls.Add(cancelButton);
        buttonPanel.Controls.Add(saveButton);

        var contentPanel = new Panel
        {
            Dock = DockStyle.Fill,
            AutoScroll = true
        };

        contentPanel.Controls.Add(FieldsLayout);

        var root = new TableLayoutPanel
        {
            Dock = DockStyle.Fill,
            ColumnCount = 1,
            RowCount = 2
        };

        root.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 100));
        root.RowStyles.Add(new RowStyle(SizeType.Percent, 100));
        root.RowStyles.Add(new RowStyle(SizeType.AutoSize));
        root.Controls.Add(contentPanel, 0, 0);
        root.Controls.Add(buttonPanel, 0, 1);

        Controls.Add(root);

        AcceptButton = saveButton;
        CancelButton = cancelButton;
    }

    public int Id
    {
        set
        {
            EntityId = value;
            LoadEntity(value);
        }
    }

    protected abstract void LoadEntity(int id);

    protected abstract void SaveEntity();

    protected virtual bool ValidateFields()
    {
        return true;
    }

    private void ButtonSave_Click(object? sender, EventArgs e)
    {
        try
        {
            if (!ValidateFields())
            {
                throw new InvalidOperationException("Имеются незаполненные поля");
            }

            SaveEntity();
            Close();
        }
        catch (Exception ex)
        {
            Ui.ShowError(ex.Message, "Ошибка при сохранении");
        }
    }
}
