using System.ComponentModel;

namespace SessionAccounting.Entities;

public class Session
{
    public int Id { get; private set; }

    public int StudentId { get; private set; }

    public DateTime SessionDate { get; private set; }

    [Browsable(false)]
    public IEnumerable<SessionDiscipline> SessionDisciplines { get; private set; } = [];

    public decimal AverageGrade => SessionDisciplines.Any()
        ? Math.Round(SessionDisciplines.Average(item => (decimal)item.Grade), 2)
        : 0m;

    public static Session CreateOperation(
        int id,
        int studentId,
        IEnumerable<SessionDiscipline> sessionDisciplines)
    {
        return new Session
        {
            Id = id,
            StudentId = studentId,
            SessionDate = DateTime.Now,
            SessionDisciplines = sessionDisciplines ?? []
        };
    }
}
