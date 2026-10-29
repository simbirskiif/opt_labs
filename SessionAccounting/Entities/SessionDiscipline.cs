namespace SessionAccounting.Entities;

public class SessionDiscipline
{
    public int Id { get; private set; }

    public int SessionId { get; private set; }

    public int DisciplineId { get; private set; }

    public int TeacherId { get; private set; }

    public int Grade { get; private set; }

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
