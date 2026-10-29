using SessionAccounting.Common;
using SessionAccounting.Entities;
using SessionAccounting.Repositories;

namespace SessionAccounting.Forms.References;

public class FormGroup : EditFormBase
{
    private readonly IStudentGroupRepository _groupRepository;
    private readonly TextBox _number = Ui.CreateTextBox();
    private readonly ComboBox _department = Ui.CreateComboBox();
    private readonly NumericUpDown _course = Ui.CreateNumeric(1, 6);

    public FormGroup(IStudentGroupRepository groupRepository, IDepartmentRepository departmentRepository)
        : base("Группа")
    {
        _groupRepository = groupRepository ?? throw new ArgumentNullException(nameof(groupRepository));

        _department.DataSource = departmentRepository.ReadDepartments().ToList();
        _department.DisplayMember = nameof(Department.Name);
        _department.ValueMember = nameof(Department.Id);
        _course.Value = 1;

        Ui.AddRow(FieldsLayout, "Номер группы", _number);
        Ui.AddRow(FieldsLayout, "Кафедра", _department);
        Ui.AddRow(FieldsLayout, "Курс", _course);
    }

    protected override void LoadEntity(int id)
    {
        var group = _groupRepository.ReadStudentGroupById(id)
            ?? throw new InvalidOperationException("Группа не найдена");

        _number.Text = group.Number;
        _department.SelectedValue = group.DepartmentId;
        _course.Value = group.Course;
    }

    protected override bool ValidateFields()
    {
        return !string.IsNullOrWhiteSpace(_number.Text)
            && _department.SelectedValue is int departmentId
            && departmentId > 0;
    }

    protected override void SaveEntity()
    {
        var group = StudentGroup.CreateEntity(
            EntityId ?? 0,
            (int)_department.SelectedValue!,
            _number.Text.Trim(),
            (int)_course.Value);

        if (EntityId.HasValue)
        {
            _groupRepository.UpdateStudentGroup(group);
        }
        else
        {
            _groupRepository.CreateStudentGroup(group);
        }
    }
}
