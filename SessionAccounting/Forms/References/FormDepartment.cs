using SessionAccounting.Common;
using SessionAccounting.Entities;
using SessionAccounting.Repositories;

namespace SessionAccounting.Forms.References;

public class FormDepartment : EditFormBase
{
    private readonly IDepartmentRepository _departmentRepository;
    private readonly TextBox _name = Ui.CreateTextBox();
    private readonly ComboBox _faculty = Ui.CreateComboBox();

    public FormDepartment(IDepartmentRepository departmentRepository, IFacultyRepository facultyRepository)
        : base("Кафедра")
    {
        _departmentRepository = departmentRepository ?? throw new ArgumentNullException(nameof(departmentRepository));

        _faculty.DataSource = facultyRepository.ReadFaculties().ToList();
        _faculty.DisplayMember = nameof(Faculty.Name);
        _faculty.ValueMember = nameof(Faculty.Id);

        Ui.AddRow(FieldsLayout, "Название", _name);
        Ui.AddRow(FieldsLayout, "Факультет", _faculty);
    }

    protected override void LoadEntity(int id)
    {
        var department = _departmentRepository.ReadDepartmentById(id)
            ?? throw new InvalidOperationException("Кафедра не найдена");

        _name.Text = department.Name;
        _faculty.SelectedValue = department.FacultyId;
    }

    protected override bool ValidateFields()
    {
        return !string.IsNullOrWhiteSpace(_name.Text)
            && _faculty.SelectedValue is int facultyId
            && facultyId > 0;
    }

    protected override void SaveEntity()
    {
        var department = Department.CreateEntity(
            EntityId ?? 0,
            (int)_faculty.SelectedValue!,
            _name.Text.Trim());

        if (EntityId.HasValue)
        {
            _departmentRepository.UpdateDepartment(department);
        }
        else
        {
            _departmentRepository.CreateDepartment(department);
        }
    }
}
