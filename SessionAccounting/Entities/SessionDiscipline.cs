using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace SessionAccounting.Entities;

public class SessionDiscipline
{
    protected SessionDiscipline()
    {
    }

    [Key]
    public int Id { get; private set; }

    [Required]
    public int SessionId { get; private set; }

    [Required]
    public int DisciplineId { get; private set; }

    [Required]
    public int TeacherId { get; private set; }

    public int Grade { get; private set; }

    [ForeignKey(nameof(SessionId))]
    [Browsable(false)]
    public Session? Session { get; private set; }

    [ForeignKey(nameof(DisciplineId))]
    [Browsable(false)]
    public Discipline? Discipline { get; private set; }

    [ForeignKey(nameof(TeacherId))]
    [Browsable(false)]
    public Teacher? Teacher { get; private set; }

    public static SessionDiscipline CreateElement(
        int id,
        int sessionId,
        int disciplineId,
        int teacherId,
        int grade)
    {
        return new SessionDiscipline
        {
            Id = id,
            SessionId = sessionId,
            DisciplineId = disciplineId,
            TeacherId = teacherId,
            Grade = grade
        };
    }
}
