using System.Drawing;
using System.Windows.Forms;
using Unity;

namespace SessionAccounting.Common;

public abstract class ListFormBase : Form
{
    protected IUnityContainer IocContainer { get; }

    protected DataGridView Grid { get; }

    protected ListFormBase(IUnityContainer container, string title, bool allowEdit, bool allowDelete)
    {
        IocContainer = container ?? throw new ArgumentNullException(nameof(container));

        Text = title;
        ClientSize = new Size(980, 560);
        MinimumSize = new Size(760, 420);
        StartPosition = FormStartPosition.CenterParent;

        Grid = Ui.CreateGrid();
        Grid.DataError += (_, args) => args.ThrowException = false;

        var buttonPanel = new FlowLayoutPanel
        {
            Dock = DockStyle.Right,
            Width = 140,
            FlowDirection = FlowDirection.TopDown,
            WrapContents = false,
            Padding = new Padding(8),
            BackColor = Color.FromArgb(245, 247, 250)
        };

        buttonPanel.Controls.Add(Ui.CreateButton("Добавить", (_, _) => AddNew()));
        if (allowEdit)
        {
            buttonPanel.Controls.Add(Ui.CreateButton("Изменить", (_, _) => EditSelected()));
        }

        if (allowDelete)
        {
            buttonPanel.Controls.Add(Ui.CreateButton("Удалить", (_, _) => DeleteSelected()));
        }

        Controls.Add(Grid);
        Controls.Add(buttonPanel);
        Load += (_, _) => Reload();
    }

    protected abstract object LoadDataSource();

    protected abstract void AddItem();

    protected virtual void EditItem(int id)
    {
        throw new NotSupportedException("Изменение для этой сущности не предусмотрено");
    }

    protected virtual void DeleteItem(int id)
    {
        throw new NotSupportedException("Удаление для этой сущности не предусмотрено");
    }

    protected void Reload()
    {
        try
        {
            Grid.DataSource = LoadDataSource();
        }
        catch (Exception ex)
        {
            Ui.ShowError(ex.Message, "Ошибка при загрузке");
        }
    }

    private void AddNew()
    {
        try
        {
            AddItem();
            Reload();
        }
        catch (Exception ex)
        {
            Ui.ShowError(ex.Message, "Ошибка при добавлении");
        }
    }

    private void EditSelected()
    {
        if (!TryGetSelectedId(out var id))
        {
            return;
        }

        try
        {
            EditItem(id);
            Reload();
        }
        catch (Exception ex)
        {
            Ui.ShowError(ex.Message, "Ошибка при изменении");
        }
    }

    private void DeleteSelected()
    {
        if (!TryGetSelectedId(out var id))
        {
            return;
        }

        if (MessageBox.Show("Удалить запись?", "Удаление", MessageBoxButtons.YesNo, MessageBoxIcon.Question) != DialogResult.Yes)
        {
            return;
        }

        try
        {
            DeleteItem(id);
            Reload();
        }
        catch (Exception ex)
        {
            Ui.ShowError(ex.Message, "Ошибка при удалении");
        }
    }

    private bool TryGetSelectedId(out int id)
    {
        id = 0;

        if (Grid.SelectedRows.Count < 1)
        {
            Ui.ShowError("Нет выбранной записи", "Ошибка");
            return false;
        }

        var value = Grid.SelectedRows[0].Cells["Id"].Value;
        if (value is null)
        {
            Ui.ShowError("Не удалось определить идентификатор записи", "Ошибка");
            return false;
        }

        id = Convert.ToInt32(value);
        return true;
    }
}
