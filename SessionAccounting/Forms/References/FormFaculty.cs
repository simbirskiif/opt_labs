using SessionAccounting.Common;
using SessionAccounting.Entities;
using SessionAccounting.Repositories;

namespace SessionAccounting.Forms.References;

public class FormFaculty : EditFormBase
{
    private readonly IFacultyRepository _facultyRepository;
    private readonly TextBox _name = Ui.CreateTextBox();
    private readonly TextBox _shortName = Ui.CreateTextBox();

    public FormFaculty(IFacultyRepository facultyRepository)
        : base("Факультет")
    {
        _facultyRepository = facultyRepository ?? throw new ArgumentNullException(nameof(facultyRepository));

        Ui.AddRow(FieldsLayout, "Название", _name);
        Ui.AddRow(FieldsLayout, "Сокращение", _shortName);
    }

    protected override void LoadEntity(int id)
    {
        var faculty = _facultyRepository.ReadFacultyById(id)
            ?? throw new InvalidOperationException("Факультет не найден");

        _name.Text = faculty.Name;
        _shortName.Text = faculty.ShortName;
    }

    protected override bool ValidateFields()
    {
        return !string.IsNullOrWhiteSpace(_name.Text)
            && !string.IsNullOrWhiteSpace(_shortName.Text);
    }

    protected override void SaveEntity()
    {
        var faculty = Faculty.CreateEntity(
            EntityId ?? 0,
            _name.Text.Trim(),
            _shortName.Text.Trim());

        if (EntityId.HasValue)
        {
            _facultyRepository.UpdateFaculty(faculty);
        }
        else
        {
            _facultyRepository.CreateFaculty(faculty);
        }
    }
}
