using SessionAccounting.Common;
using SessionAccounting.Entities;
using SessionAccounting.Entities.Enums;
using SessionAccounting.Repositories;

namespace SessionAccounting.Forms.References;

public class FormTeacher : EditFormBase
{
    private readonly ITeacherRepository _teacherRepository;
    private readonly TextBox _lastName = Ui.CreateTextBox();
    private readonly TextBox _firstName = Ui.CreateTextBox();
    private readonly TextBox _middleName = Ui.CreateTextBox();
    private readonly ComboBox _position = Ui.CreateComboBox();
    private readonly ComboBox _department = Ui.CreateComboBox();

    public FormTeacher(ITeacherRepository teacherRepository, IDepartmentRepository departmentRepository)
        : base("Преподаватель")
    {
        _teacherRepository = teacherRepository ?? throw new ArgumentNullException(nameof(teacherRepository));

        _position.BindEnum<TeacherPosition>();
        _department.DataSource = departmentRepository.ReadDepartments().ToList();
        _department.DisplayMember = nameof(Department.Name);
        _department.ValueMember = nameof(Department.Id);

        Ui.AddRow(FieldsLayout, "Фамилия", _lastName);
        Ui.AddRow(FieldsLayout, "Имя", _firstName);
        Ui.AddRow(FieldsLayout, "Отчество", _middleName);
        Ui.AddRow(FieldsLayout, "Должность", _position);
        Ui.AddRow(FieldsLayout, "Кафедра", _department);
    }

    protected override void LoadEntity(int id)
    {
        var teacher = _teacherRepository.ReadTeacherById(id)
            ?? throw new InvalidOperationException("Преподаватель не найден");

        _lastName.Text = teacher.LastName;
        _firstName.Text = teacher.FirstName;
        _middleName.Text = teacher.MiddleName;
        _position.SelectedValue = teacher.Position;
        _department.SelectedValue = teacher.DepartmentId;
    }

    protected override bool ValidateFields()
    {
        return !string.IsNullOrWhiteSpace(_lastName.Text)
            && !string.IsNullOrWhiteSpace(_firstName.Text)
            && _position.SelectedValue is TeacherPosition position
            && position != TeacherPosition.None
            && _department.SelectedValue is int departmentId
            && departmentId > 0;
    }

    protected override void SaveEntity()
    {
        var teacher = Teacher.CreateEntity(
            EntityId ?? 0,
            (int)_department.SelectedValue!,
            _lastName.Text.Trim(),
            _firstName.Text.Trim(),
            _middleName.Text.Trim(),
            (TeacherPosition)_position.SelectedValue!);

        if (EntityId.HasValue)
        {
            _teacherRepository.UpdateTeacher(teacher);
        }
        else
        {
            _teacherRepository.CreateTeacher(teacher);
        }
    }
}
