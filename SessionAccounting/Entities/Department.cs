namespace SessionAccounting.Entities;

public class Department
{
    public int Id { get; private set; }

    public int FacultyId { get; private set; }

    public string Name { get; private set; } = string.Empty;

    public static Department CreateEntity(int id, int facultyId, string name)
    {
        return new Department
        {
            Id = id,
            FacultyId = facultyId,
            Name = name ?? string.Empty
        };
    }
}
