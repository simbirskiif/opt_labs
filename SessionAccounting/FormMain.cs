using System.Drawing;
using SessionAccounting.Forms.Operations;
using SessionAccounting.Forms.References;
using Unity;

namespace SessionAccounting;

public class FormMain : Form
{
    private readonly IUnityContainer _container;

    public FormMain(IUnityContainer container)
    {
        _container = container ?? throw new ArgumentNullException(nameof(container));

        Text = "Учет результатов сессии";
        ClientSize = new Size(1100, 700);
        MinimumSize = new Size(900, 600);
        StartPosition = FormStartPosition.CenterScreen;
        BackColor = Color.FromArgb(245, 247, 250);

        var hint = new Label
        {
            Dock = DockStyle.Fill,
            Text = "Выберите раздел в меню",
            TextAlign = ContentAlignment.MiddleCenter,
            Font = new Font("Segoe UI", 16F),
            ForeColor = Color.FromArgb(120, 130, 145)
        };

        Controls.Add(hint);
        Controls.Add(BuildMenu());
    }

    private MenuStrip BuildMenu()
    {
        var menu = new MenuStrip
        {
            Font = new Font("Segoe UI", 10F)
        };

        var references = new ToolStripMenuItem("Справочники");
        references.DropDownItems.Add(CreateMenuItem("Факультеты", () => _container.Resolve<FormFaculties>().ShowDialog()));
        references.DropDownItems.Add(CreateMenuItem("Кафедры", () => _container.Resolve<FormDepartments>().ShowDialog()));
        references.DropDownItems.Add(CreateMenuItem("Группы", () => _container.Resolve<FormGroups>().ShowDialog()));
        references.DropDownItems.Add(CreateMenuItem("Преподаватели", () => _container.Resolve<FormTeachers>().ShowDialog()));
        references.DropDownItems.Add(CreateMenuItem("Дисциплины", () => _container.Resolve<FormDisciplines>().ShowDialog()));
        references.DropDownItems.Add(CreateMenuItem("Студенты", () => _container.Resolve<FormStudents>().ShowDialog()));

        var operations = new ToolStripMenuItem("Операции");
        operations.DropDownItems.Add(CreateMenuItem("Сессии", () => _container.Resolve<FormSessions>().ShowDialog()));
        operations.DropDownItems.Add(CreateMenuItem("Выплаты стипендий", () => _container.Resolve<FormScholarshipPayments>().ShowDialog()));

        var reports = new ToolStripMenuItem("Отчеты");

        menu.Items.Add(references);
        menu.Items.Add(operations);
        menu.Items.Add(reports);
        MainMenuStrip = menu;

        return menu;
    }

    private static ToolStripMenuItem CreateMenuItem(string text, Action action)
    {
        var item = new ToolStripMenuItem(text);
        item.Click += (_, _) =>
        {
            try
            {
                action();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Ошибка при загрузке", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        };

        return item;
    }
}
