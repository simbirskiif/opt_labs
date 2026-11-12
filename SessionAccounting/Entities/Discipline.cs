using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace SessionAccounting.Entities;

public class Discipline
{
    protected Discipline()
    {
    }

    [Key]
    public int Id { get; private set; }

    [Required]
    public int DepartmentId { get; private set; }

    [Required]
    [MaxLength(150)]
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
