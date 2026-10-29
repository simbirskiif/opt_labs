using System.ComponentModel;

namespace SessionAccounting.Entities;

public class Discipline
{
    public int Id { get; private set; }

    public int DepartmentId { get; private set; }

    public string Name { get; private set; } = string.Empty;

    public int Semester { get; private set; }

    [Browsable(false)]
    public string DisplayName => $"{Name} ({Semester} семестр)";

    public static Discipline CreateEntity(int id, int departmentId, string name, int semester)
    {
        return new Discipline
        {
            Id = id,
            DepartmentId = departmentId,
            Name = name ?? string.Empty,
            Semester = semester
        };
    }
}
