using System.ComponentModel.DataAnnotations;

namespace SessionAccounting.Entities;

public class Department
{
    protected Department()
    {
    }

    [Key]
    public int Id { get; private set; }

    [Required]
    public int FacultyId { get; private set; }

    [Required]
    [MaxLength(150)]
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
