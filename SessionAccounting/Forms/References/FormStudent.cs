using SessionAccounting.Common;
using SessionAccounting.Entities;
using SessionAccounting.Entities.Enums;
using SessionAccounting.Repositories;

namespace SessionAccounting.Forms.References;

public class FormStudent : EditFormBase
{
    private readonly IStudentRepository _studentRepository;
    private readonly TextBox _lastName = Ui.CreateTextBox();
    private readonly TextBox _firstName = Ui.CreateTextBox();
    private readonly TextBox _middleName = Ui.CreateTextBox();
    private readonly TextBox _recordBookNumber = Ui.CreateTextBox();
    private readonly ComboBox _group = Ui.CreateComboBox();
    private readonly CheckBox _married = new() { Text = "Семейный", AutoSize = true, Margin = new Padding(0, 6, 16, 0) };
    private readonly CheckBox _dormitory = new() { Text = "Проживает в общежитии", AutoSize = true, Margin = new Padding(0, 6, 0, 0) };
    private readonly NumericUpDown _averageGrade = Ui.CreateNumeric(2.0m, 5.0m, 2, 0.01m);
    private readonly Label _scholarship = Ui.CreateValueLabel();
    private readonly Label _dormitoryPayment = Ui.CreateValueLabel();

    public FormStudent(IStudentRepository studentRepository, IStudentGroupRepository groupRepository)
        : base("Студент")
    {
        _studentRepository = studentRepository ?? throw new ArgumentNullException(nameof(studentRepository));

        _group.DataSource = groupRepository.ReadStudentGroups().ToList();
        _group.DisplayMember = nameof(StudentGroup.Number);
        _group.ValueMember = nameof(StudentGroup.Id);
        _averageGrade.Value = 4.0m;

        _averageGrade.ValueChanged += (_, _) => UpdateScholarshipPreview();
        _married.CheckedChanged += (_, _) => UpdateScholarshipPreview();
        _dormitory.CheckedChanged += (_, _) => UpdateScholarshipPreview();

        var statusPanel = new FlowLayoutPanel
        {
            Dock = DockStyle.Fill,
            AutoSize = true,
            WrapContents = false
        };

        statusPanel.Controls.Add(_married);
        statusPanel.Controls.Add(_dormitory);

        Ui.AddRow(FieldsLayout, "Фамилия", _lastName);
        Ui.AddRow(FieldsLayout, "Имя", _firstName);
        Ui.AddRow(FieldsLayout, "Отчество", _middleName);
        Ui.AddRow(FieldsLayout, "Номер зачётной книжки", _recordBookNumber);
        Ui.AddRow(FieldsLayout, "Группа", _group);
        Ui.AddRow(FieldsLayout, "Социальный статус", statusPanel);
        Ui.AddRow(FieldsLayout, "Средний балл", _averageGrade);
        Ui.AddRow(FieldsLayout, "Стипендия, руб.", _scholarship);
        Ui.AddRow(FieldsLayout, "Оплата общежития, руб.", _dormitoryPayment);

        UpdateScholarshipPreview();
    }

    protected override void LoadEntity(int id)
    {
        var student = _studentRepository.ReadStudentById(id)
            ?? throw new InvalidOperationException("Студент не найден");

        _lastName.Text = student.LastName;
        _firstName.Text = student.FirstName;
        _middleName.Text = student.MiddleName;
        _recordBookNumber.Text = student.RecordBookNumber;
        _group.SelectedValue = student.GroupId;
        _married.Checked = student.SocialStatus.HasFlag(StudentSocialStatus.Married);
        _dormitory.Checked = student.SocialStatus.HasFlag(StudentSocialStatus.Dormitory);
        _averageGrade.Value = student.AverageGrade;

        UpdateScholarshipPreview();
    }

    protected override bool ValidateFields()
    {
        return !string.IsNullOrWhiteSpace(_lastName.Text)
            && !string.IsNullOrWhiteSpace(_firstName.Text)
            && !string.IsNullOrWhiteSpace(_recordBookNumber.Text)
            && _group.SelectedValue is int groupId
            && groupId > 0;
    }

    protected override void SaveEntity()
    {
        var student = Student.CreateEntity(
            EntityId ?? 0,
            (int)_group.SelectedValue!,
            _recordBookNumber.Text.Trim(),
            _lastName.Text.Trim(),
            _firstName.Text.Trim(),
            _middleName.Text.Trim(),
            GetSocialStatus(),
            _averageGrade.Value);

        if (EntityId.HasValue)
        {
            _studentRepository.UpdateStudent(student);
        }
        else
        {
            _studentRepository.CreateStudent(student);
        }
    }

    private StudentSocialStatus GetSocialStatus()
    {
        var status = StudentSocialStatus.None;

        if (_married.Checked)
        {
            status |= StudentSocialStatus.Married;
        }

        if (_dormitory.Checked)
        {
            status |= StudentSocialStatus.Dormitory;
        }

        return status;
    }

    private void UpdateScholarshipPreview()
    {
        var status = GetSocialStatus();
        var scholarship = Student.CalculateScholarship(_averageGrade.Value, status);
        var dormitoryPayment = status.HasFlag(StudentSocialStatus.Dormitory) && scholarship > 0 ? 100m : 0m;

        _scholarship.Text = scholarship.ToString("0.00");
        _dormitoryPayment.Text = dormitoryPayment.ToString("0.00");
    }
}
