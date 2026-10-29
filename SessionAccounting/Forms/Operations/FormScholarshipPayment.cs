using SessionAccounting.Common;
using SessionAccounting.Entities;
using SessionAccounting.Repositories;

namespace SessionAccounting.Forms.Operations;

public class FormScholarshipPayment : EditFormBase
{
    private readonly IScholarshipPaymentRepository _paymentRepository;
    private readonly ComboBox _student = Ui.CreateComboBox();
    private readonly NumericUpDown _amount = Ui.CreateNumeric(0m, 100000m, 2, 100m);

    public FormScholarshipPayment(
        IScholarshipPaymentRepository paymentRepository,
        IStudentRepository studentRepository)
        : base("Новая выплата стипендии")
    {
        _paymentRepository = paymentRepository ?? throw new ArgumentNullException(nameof(paymentRepository));

        _student.DataSource = studentRepository.ReadStudents().ToList();
        _student.DisplayMember = nameof(Student.FullName);
        _student.ValueMember = nameof(Student.Id);
        _student.SelectedIndexChanged += (_, _) => UpdateAmount();

        Ui.AddRow(FieldsLayout, "Студент", _student);
        Ui.AddRow(FieldsLayout, "Дата", Ui.CreateValueLabel(DateTime.Now.ToString("dd.MM.yyyy")));
        Ui.AddRow(FieldsLayout, "Сумма, руб.", _amount);
    }

    protected override void LoadEntity(int id)
    {
        throw new NotSupportedException("Редактирование выплаты не предусмотрено");
    }

    protected override bool ValidateFields()
    {
        return _student.SelectedValue is int studentId
            && studentId > 0
            && _amount.Value > 0m;
    }

    protected override void SaveEntity()
    {
        _paymentRepository.CreateScholarshipPayment(
            ScholarshipPayment.CreateOperation(
                0,
                (int)_student.SelectedValue!,
                _amount.Value));
    }

    private void UpdateAmount()
    {
        if (_student.SelectedItem is Student student && student.ScholarshipAmount > 0m)
        {
            _amount.Value = student.ScholarshipAmount;
        }
    }
}
