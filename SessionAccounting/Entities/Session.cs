using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace SessionAccounting.Entities;

public class Session
{
    protected Session()
    {
    }

    [Key]
    public int Id { get; private set; }

    [Required]
    public int StudentId { get; private set; }

    public DateTime SessionDate { get; private set; }

    [Browsable(false)]
    public ICollection<SessionDiscipline> SessionDisciplines { get; private set; } = new List<SessionDiscipline>();

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
            SessionDisciplines = sessionDisciplines?.ToList() ?? new List<SessionDiscipline>()
        };
    }
}
