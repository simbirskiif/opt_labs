using SessionAccounting.Common;
using SessionAccounting.Entities;
using SessionAccounting.Repositories;

namespace SessionAccounting.Forms.References;

public class FormDiscipline : EditFormBase
{
    private readonly IDisciplineRepository _disciplineRepository;
    private readonly TextBox _name = Ui.CreateTextBox();
    private readonly ComboBox _department = Ui.CreateComboBox();
    private readonly NumericUpDown _semester = Ui.CreateNumeric(1, 2);

    public FormDiscipline(IDisciplineRepository disciplineRepository, IDepartmentRepository departmentRepository)
        : base("Дисциплина")
    {
        _disciplineRepository = disciplineRepository ?? throw new ArgumentNullException(nameof(disciplineRepository));

        _department.DataSource = departmentRepository.ReadDepartments().ToList();
        _department.DisplayMember = nameof(Department.Name);
        _department.ValueMember = nameof(Department.Id);
        _semester.Value = 1;

        Ui.AddRow(FieldsLayout, "Название", _name);
        Ui.AddRow(FieldsLayout, "Кафедра", _department);
        Ui.AddRow(FieldsLayout, "Семестр", _semester);
    }

    protected override void LoadEntity(int id)
    {
        var discipline = _disciplineRepository.ReadDisciplineById(id)
            ?? throw new InvalidOperationException("Дисциплина не найдена");

        _name.Text = discipline.Name;
        _department.SelectedValue = discipline.DepartmentId;
        _semester.Value = discipline.Semester;
    }

    protected override bool ValidateFields()
    {
        return !string.IsNullOrWhiteSpace(_name.Text)
            && _department.SelectedValue is int departmentId
            && departmentId > 0;
    }

    protected override void SaveEntity()
    {
        var discipline = Discipline.CreateEntity(
            EntityId ?? 0,
            (int)_department.SelectedValue!,
            _name.Text.Trim(),
            (int)_semester.Value);

        if (EntityId.HasValue)
        {
            _disciplineRepository.UpdateDiscipline(discipline);
        }
        else
        {
            _disciplineRepository.CreateDiscipline(discipline);
        }
    }
}
