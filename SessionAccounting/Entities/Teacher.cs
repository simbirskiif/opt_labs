using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using SessionAccounting.Entities.Enums;

namespace SessionAccounting.Entities;

public class Teacher
{
    protected Teacher()
    {
    }

    [Key]
    public int Id { get; private set; }

    [Required]
    public int DepartmentId { get; private set; }

    [Required]
    [MaxLength(50)]
    public string LastName { get; private set; } = string.Empty;

    [Required]
    [MaxLength(50)]
    public string FirstName { get; private set; } = string.Empty;

    [MaxLength(50)]
    public string MiddleName { get; private set; } = string.Empty;

    public TeacherPosition Position { get; private set; }

    [Browsable(false)]
    public string FullName => $"{LastName} {FirstName} {MiddleName}".Trim();

    public static Teacher CreateEntity(
        int id,
        int departmentId,
        string lastName,
        string firstName,
        string middleName,
        TeacherPosition position)
    {
        return new Teacher
        {
            Id = id,
            DepartmentId = departmentId,
            LastName = lastName ?? string.Empty,
            FirstName = firstName ?? string.Empty,
            MiddleName = middleName ?? string.Empty,
            Position = position
        };
    }
}
