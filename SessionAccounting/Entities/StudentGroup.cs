using System.ComponentModel.DataAnnotations;

namespace SessionAccounting.Entities;

public class StudentGroup
{
    protected StudentGroup()
    {
    }

    [Key]
    public int Id { get; private set; }

    [Required]
    public int DepartmentId { get; private set; }

    [Required]
    [MaxLength(20)]
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
