using SessionAccounting.Common;
using SessionAccounting.Entities;
using SessionAccounting.Repositories;

namespace SessionAccounting.Forms.Operations;

public class FormSession : EditFormBase
{
    private readonly ISessionRepository _sessionRepository;
    private readonly ComboBox _student = Ui.CreateComboBox();
    private readonly DataGridView _grid;

    public FormSession(
        ISessionRepository sessionRepository,
        IStudentRepository studentRepository,
        IDisciplineRepository disciplineRepository,
        ITeacherRepository teacherRepository)
        : base("Новая сессия")
    {
        _sessionRepository = sessionRepository ?? throw new ArgumentNullException(nameof(sessionRepository));

        _student.DataSource = studentRepository.ReadStudents().ToList();
        _student.DisplayMember = nameof(Student.FullName);
        _student.ValueMember = nameof(Student.Id);

        _grid = Ui.CreateEditGrid();
        _grid.AutoGenerateColumns = false;
        _grid.EditMode = DataGridViewEditMode.EditOnEnter;
        _grid.DataError += (_, args) => args.ThrowException = false;

        var disciplineColumn = new DataGridViewComboBoxColumn
        {
            Name = "ColumnDiscipline",
            HeaderText = "Дисциплина",
            DataSource = disciplineRepository.ReadDisciplines().ToList(),
            DisplayMember = nameof(Discipline.Name),
            ValueMember = nameof(Discipline.Id)
        };

        var teacherColumn = new DataGridViewComboBoxColumn
        {
            Name = "ColumnTeacher",
            HeaderText = "Преподаватель",
            DataSource = teacherRepository.ReadTeachers().ToList(),
            DisplayMember = nameof(Teacher.FullName),
            ValueMember = nameof(Teacher.Id)
        };

        var gradeColumn = new DataGridViewTextBoxColumn
        {
            Name = "ColumnGrade",
            HeaderText = "Оценка"
        };

        _grid.Columns.AddRange(disciplineColumn, teacherColumn, gradeColumn);

        Ui.AddRow(FieldsLayout, "Студент", _student);
        Ui.AddRow(FieldsLayout, "Дата", Ui.CreateValueLabel(DateTime.Now.ToString("dd.MM.yyyy")));

        var groupBox = new GroupBox
        {
            Text = "Сдаваемые дисциплины (ровно пять)",
            Dock = DockStyle.Fill
        };

        groupBox.Controls.Add(_grid);
        Ui.AddSpanRow(FieldsLayout, groupBox, 280);
    }

    protected override void LoadEntity(int id)
    {
        throw new NotSupportedException("Редактирование сессии не предусмотрено");
    }

    protected override bool ValidateFields()
    {
        return _student.SelectedValue is int studentId && studentId > 0;
    }

    protected override void SaveEntity()
    {
        var disciplines = CreateDisciplinesFromGrid();
        if (disciplines.Count != 5)
        {
            throw new InvalidOperationException("В сессии должно быть ровно пять дисциплин");
        }

        _sessionRepository.CreateSession(
            Session.CreateOperation(0, (int)_student.SelectedValue!, disciplines));
    }

    private List<SessionDiscipline> CreateDisciplinesFromGrid()
    {
        var result = new List<SessionDiscipline>();
        var usedDisciplines = new HashSet<int>();

        foreach (DataGridViewRow row in _grid.Rows)
        {
            if (row.IsNewRow)
            {
                continue;
            }

            var disciplineValue = row.Cells["ColumnDiscipline"].Value;
            if (disciplineValue is null)
            {
                continue;
            }

            var teacherValue = row.Cells["ColumnTeacher"].Value;
            var gradeValue = row.Cells["ColumnGrade"].Value;

            if (teacherValue is null || gradeValue is null)
            {
                throw new InvalidOperationException("Заполните преподавателя и оценку для каждой дисциплины");
            }

            var disciplineId = Convert.ToInt32(disciplineValue);
            var teacherId = Convert.ToInt32(teacherValue);
            var grade = Convert.ToInt32(gradeValue);

            if (disciplineId <= 0 || teacherId <= 0)
            {
                throw new InvalidOperationException("Выберите дисциплину и преподавателя");
            }

            if (!usedDisciplines.Add(disciplineId))
            {
                throw new InvalidOperationException("Дисциплины в сессии не должны повторяться");
            }

            if (grade < 2 || grade > 5)
            {
                throw new InvalidOperationException("Оценка должна быть от 2 до 5");
            }

            result.Add(SessionDiscipline.CreateElement(0, 0, disciplineId, teacherId, grade));
        }

        return result;
    }
}
