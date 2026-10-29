namespace SessionAccounting.Entities;

public class StudentGroup
{
    public int Id { get; private set; }

    public int DepartmentId { get; private set; }

    public string Number { get; private set; } = string.Empty;

    public int Course { get; private set; }

    public static StudentGroup CreateEntity(int id, int departmentId, string number, int course)
    {
        return new StudentGroup
        {
            Id = id,
            DepartmentId = departmentId,
            Number = number ?? string.Empty,
            Course = course
        };
    }
}
